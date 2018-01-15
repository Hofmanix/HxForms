using System.ComponentModel;
using System.Linq;
using Android.OS;
using Android.Text;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using LabelRenderer = HxForms.Droid.ViewRenderers.LabelRenderer;
using Android.Content;
using System;

namespace HxForms.Droid.ViewRenderers
{
    public class LabelRenderer: Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public new Views.Label Element { get; private set; }

        public LabelRenderer(Context context): base(context) {}

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is Views.Label hxLabel)
            {
                Element = hxLabel;

                if (hxLabel.MaxLength > 0)
                {
                    Control.Text = CropText(Element.Text);
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
                else 
                {
                    Control.Text = CropText(Element.Text);
                }
            }
            if (e.PropertyName == Views.Label.IsHtmlProperty.PropertyName)
            {
                SetHtmlText();
            }
            if (e.PropertyName == Views.Label.MaxLengthProperty.PropertyName)
            {
                Control.SetMaxEms(Element.MaxLength);
            }
        }

        private void SetHtmlText()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                Control?.SetText(CropText(Html.FromHtml(Element.Text, FromHtmlOptions.ModeCompact)),
                    TextView.BufferType.Spannable);
            }
            else
            {
                Control?.SetText(CropText(Html.FromHtml(Element.Text)),
                    TextView.BufferType.Spannable);
            }
        }

        private string CropText(string text) 
        {
            if (Element.MaxLength > 0) 
            {
                return text.Substring(0, Math.Min(Element.MaxLength, text.Length));
            }
            else 
            {
                return text;
            }
        }

        private ISpanned CropText(ISpanned text)
        {
            if (Element.MaxLength > 0) 
            {
                return (ISpanned) text.SubSequenceFormatted(0, Math.Min(Element.MaxLength, text.Length()));
            }
            else
            {
                return text;
            }
        }


    }
}