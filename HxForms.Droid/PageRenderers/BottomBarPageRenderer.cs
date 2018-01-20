using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Android.Support.V7.View.Menu;
using Android.Views;
using Android.Widget;
using Com.Ittianyu.Bottomnavigationviewex;
using HxForms.Droid.Utils;
using HxForms.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace HxForms.Droid.PageRenderers
{
    public class BottomBarPageRenderer: VisualElementRenderer<BottomBarPage>, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private bool _disposed;
        private BottomBarPage _element;
        private Android.Widget.RelativeLayout _rootLayout;
        private BottomNavigationViewEx _bottomNavigationView;
        private IPageController _pageController => Element as IPageController;
        private Page _currentPage;
        private FrameLayout _frameLayout;
        private Dictionary<IMenuItem, Page> _menuPages = new Dictionary<IMenuItem, Page>();

        [Obsolete]
        public BottomBarPageRenderer()
        {
            AutoPackage = false;
        }

        public BottomBarPageRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            var page = _menuPages[item];
            if (page != null)
            {
                ChangeCurrentPage(page);
                return true;
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                RemoveAllViews();
                foreach (Page pageToRemove in Element.Children)
                {
                    IVisualElementRenderer pageRenderer = Platform.GetRenderer(pageToRemove);
                    if (pageRenderer != null)
                    {
                        pageRenderer.View.RemoveFromParent();
                        pageRenderer.Dispose();
                    }
                }

                if (_bottomNavigationView != null)
                {
                    _bottomNavigationView.SetOnNavigationItemSelectedListener(null);
                    _bottomNavigationView.Dispose();
                    _bottomNavigationView = null;
                }

                if (_frameLayout != null)
                {
                    _frameLayout.Dispose();
                    _frameLayout = null;
                }
                
            }

            base.Dispose(disposing);
        }

        protected override async void OnElementChanged(ElementChangedEventArgs<BottomBarPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _element = e.NewElement;
                if (_bottomNavigationView == null)
                {
                    _rootLayout = new Android.Widget.RelativeLayout(Context)
                    {
                        LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent)
                    };
                    AddView(_rootLayout);

                    _frameLayout = new FrameLayout(Context)
                    {
                        LayoutParameters = new ActionBar.LayoutParams(LayoutParams.MatchParent,
                            LayoutParams.MatchParent, GravityFlags.Fill)
                    };
                    _rootLayout.AddView(_frameLayout);

                    _bottomNavigationView = new BottomNavigationViewEx(Context)
                    {
                        LayoutParameters =
                            new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent,
                                LayoutParams.WrapContent)
                    };
                    (_bottomNavigationView.LayoutParameters as Android.Widget.RelativeLayout.LayoutParams)?.AddRule(LayoutRules.AlignParentBottom);
                    _bottomNavigationView.SetOnNavigationItemSelectedListener(this);

                    if (_element.BarTextColor != default(Color))
                    {
                        _bottomNavigationView.ItemTextColor = ColorStateList.ValueOf(_element.BarTextColor.ToAndroid());
                        _bottomNavigationView.ItemIconTintList = ColorStateList.ValueOf(_element.BarTextColor.ToAndroid());
                    }

                    if (_element.BarBackgroundColor != default(Color))
                    {
                        _bottomNavigationView.SetBackgroundColor(_element.BarBackgroundColor.ToAndroid());
                    }
                    
                    foreach (var page in Element.Children)
                    {
                        var menuItem = _bottomNavigationView.Menu.Add(page.Title);
                        if (page.Icon != null)
                        {
                            var fileImageSourceHandler = page.Icon.GetHandler();
                            var bitmap = await fileImageSourceHandler.LoadImageAsync(page.Icon, Context);
                            var bitmapDrawable = new BitmapDrawable(bitmap);
                            menuItem.SetIcon(bitmapDrawable);
                        }
                        _menuPages[menuItem] = page;
                    }

                    _rootLayout.AddView(_bottomNavigationView, 1);
                    _bottomNavigationView.EnableShiftingMode(false);
                    _bottomNavigationView.EnableItemShiftingMode(false);
                    ChangeCurrentPage(Element.Children[0]);
                }
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            int width = r - l;
            int height = b - t;

            if (width > 0 && height > 0)
            {
                var context = Context;
                _rootLayout.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.AtMost), MeasureSpecFactory.MakeMeasureSpec(height, MeasureSpecMode.AtMost));
                _rootLayout.Layout(0, 0, _rootLayout.MeasuredWidth, _rootLayout.MeasuredHeight);

                _bottomNavigationView.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(height, MeasureSpecMode.AtMost));
                int tabsHeight = Math.Min(height, Math.Max(_bottomNavigationView.MeasuredHeight, _bottomNavigationView.MinimumHeight));

                _frameLayout.Layout(0, 0, width, height - tabsHeight);

                _pageController.ContainerArea = new Rectangle(0, 0, context.FromPixels(width), context.FromPixels(_frameLayout.Height));

                _bottomNavigationView.Layout(0, height - tabsHeight, width, height);
            }

            base.OnLayout(changed, l, t, r, b);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == TabbedPage.BarBackgroundColorProperty.PropertyName)
            {
                _bottomNavigationView.SetBackgroundColor(Element.BarBackgroundColor.ToAndroid());
            }
            else if (e.PropertyName == TabbedPage.BarTextColorProperty.PropertyName)
            {
                _bottomNavigationView.ItemTextColor = ColorStateList.ValueOf(Element.BarTextColor.ToAndroid());
            }
        }

        private void ChangeCurrentPage(Page page)
        {
            Context.HideKeyboard(this);
            _frameLayout.RemoveAllViews();

            if (Platform.GetRenderer(page) == null)
            {
                Platform.SetRenderer(page, Platform.CreateRendererWithContext(page, Context));
            }
            _frameLayout.AddView(Platform.GetRenderer(page).View);
            _currentPage = page;
            Element.CurrentPage = page;
        }
        
    }
}