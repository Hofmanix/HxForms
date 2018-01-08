using System.ComponentModel;
using System.Linq;
using Android.OS;
using Android.Text;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using LabelRenderer = HxForms.Droid.ViewRenderers.LabelRenderer;

namespace HxForms.Droid.ViewRenderers
{
    public class LabelRenderer: Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public new Views.Label Element { get; private set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is Views.Label hxLabel)
            {
                Element = hxLabel;

                if (hxLabel.MaxLength > 0)
                {
                    SetMaxLengthFilter();
                }

                if (hxLabel.IsHtml)
                {
                    SetHtmlText();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                if (sender is Views.Label hxLabel && hxLabel.IsHtml)
                {

                    SetHtmlText();
                }
            }
            if (e.PropertyName == Views.Label.IsHtmlProperty.PropertyName)
            {
                SetHtmlText();
            }
            if (e.PropertyName == Views.Label.MaxLengthProperty.PropertyName)
            {
                SetMaxLengthFilter();
            }
        }

        private void SetHtmlText()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                Control?.SetText(Html.FromHtml(Element.Text, FromHtmlOptions.ModeCompact),
                    TextView.BufferType.Spannable);
            }
            else
            {
                Control?.SetText(Html.FromHtml(Element.Text),
                    TextView.BufferType.Spannable);
            }
        }

        private void SetMaxLengthFilter()
        {
            if (Control.GetFilters().Any(f => f is InputFilterLengthFilter))
            {
                Control.SetFilters(Control.GetFilters().Where(f => !(f is InputFilterLengthFilter)).ToArray());
            }
            if (Element.MaxLength > 0)
            {
                var filters = Control.GetFilters().ToList();
                filters.Add(new InputFilterLengthFilter(Element.MaxLength));
                Control.SetFilters(filters.ToArray());
            }
        }
    }
}