using System;
using Foundation;
using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HxForms.Views.Label), typeof(HxForms.iOS.ViewRenderers.LabelRenderer))]
namespace HxForms.iOS.ViewRenderers
{
    public class LabelRenderer: Xamarin.Forms.Platform.iOS.LabelRenderer
    {

        public static void InitRenderer()
        {
            var tmp = DateTime.Now;
        }

        public new Views.Label Element { get; private set; }

        public LabelRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is Views.Label hxLabel)
            {
                Element = hxLabel;

                if (hxLabel.IsHtml)
                {
                    SetHtmlText();
                } else {
                    Control.Text = CropText(Element.Text);
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
            if (e.PropertyName == Views.Label.MaxLengthProperty.PropertyName) {
                if (Element.IsHtml) {
                    SetHtmlText();
                }
                else 
                {
                    Control.Text = CropText(Element.Text);
                }
            }
        }

        private void SetHtmlText() 
        {
            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;

            var htmlData = NSData.FromString(Element.Text, NSStringEncoding.Unicode);
            Control.Lines = 0;
            var attributedString = new NSAttributedString(htmlData, attr, ref nsError);
            Control.AttributedText = CropText(attributedString);
        }

        private NSAttributedString CropText(NSAttributedString text) {
            if (Element.MaxLength > 0) 
            {
                return text.Substring(0, Convert.ToInt32(Math.Min(Element.MaxLength, text.Length)));
            } 
            else 
            {
                return text;
            }
        }

        private string CropText(string text) {
            if (Element.MaxLength > 0)
            {
                return text.Substring(0, Math.Min(Element.MaxLength, text.Length));
            }
            else
            {
                return text;
            }
        }
    }
}
