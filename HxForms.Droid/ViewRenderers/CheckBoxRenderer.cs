using System;
using Android.Content;
using Android.Widget;
using HxForms.Droid.ViewRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ACheckBox = Android.Widget.CheckBox;
using CheckBox = HxForms.Views.CheckBox;

namespace HxForms.Droid.ViewRenderers
{
    public class CheckBoxRenderer: ViewRenderer<CheckBox, ACheckBox>, CompoundButton.IOnCheckedChangeListener
    {
        public CheckBoxRenderer()
        {
            AutoPackage = false;
        }
        

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            ((IViewController)Element).SetValueFromRenderer(CheckBox.IsCheckedProperty, isChecked);
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            var sizeConstraint = base.GetDesiredSize(widthConstraint, heightConstraint);

            if (sizeConstraint.Request.Width == 0)
            {
                int width = widthConstraint;
                if (widthConstraint <= 0)
                {
                    width = 25;
                }

                sizeConstraint = new SizeRequest(new Size(width, sizeConstraint.Request.Height), new Size(width, sizeConstraint.Minimum.Height));
            }

            return sizeConstraint;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Control != null)
            {
                if (Element != null)
                    Element.Checked -= HandleChecked;
                Control.SetOnCheckedChangeListener(null);
            }
            base.Dispose(disposing);
        }

        protected override ACheckBox CreateNativeControl()
        {
            return new ACheckBox(Context);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.Checked -= HandleChecked;
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var aCheckBox = CreateNativeControl();
                    aCheckBox.SetOnCheckedChangeListener(this);
                    SetNativeControl(aCheckBox);
                }
                else
                {
                    UpdateEnabled();
                }

                e.NewElement.Checked += HandleChecked;
                Control.Checked = e.NewElement.IsChecked;
            }
        }

        private void HandleChecked(object sender, EventArgs e)
        {
            Control.Checked = Element.IsChecked;
        }

        private void UpdateEnabled()
        {
            Control.Enabled = Element.IsEnabled;
        }
    }
}