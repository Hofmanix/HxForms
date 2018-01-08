using System;
using HxForms.iOS.ViewRenderers;
using HxForms.Views;
using SaturdayMP.XPlugins.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Rectangle = System.Drawing.Rectangle;

namespace HxForms.iOS.ViewRenderers
{
    public class CheckBoxRenderer: ViewRenderer<CheckBox, BEMCheckBox>
    {
        public static void InitRenderer()
        {
            var tmp = DateTime.Now;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.ValueChanged -= OnControlValueChanged;
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            if (e.OldElement != null)
            {
                e.OldElement.Checked -= OnElementChecked;
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new BEMCheckBox(Constants.CheckBoxSize));
                    Control.ValueChanged += OnControlValueChanged;
                }

                Control.On = Element.IsChecked;
                e.NewElement.Checked += OnElementChecked;
            }

            base.OnElementChanged(e);
        }

        private void OnControlValueChanged(object sender, EventArgs e)
        {
            ((IElementController)Element).SetValueFromRenderer(CheckBox.IsCheckedProperty, Control.On);
        }

        private void OnElementChecked(object sender, EventArgs e)
        {
            Control.SetOn(Element.IsChecked, true);
        }
    }
}