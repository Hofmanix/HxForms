using Xamarin.Forms;

namespace HxForms.Views
{
    public class Label: Xamarin.Forms.Label
    {
        public static readonly BindableProperty IsHtmlProperty
            = BindableProperty.Create("IsHtml", typeof(bool), typeof(Label), false);
        public static readonly BindableProperty MaxLengthProperty
            = BindableProperty.Create("MaxLength", typeof(int), typeof(Label), int.MinValue);
        
        public bool IsHtml
        {
            get => (bool) GetValue(IsHtmlProperty);
            set
            {
                SetValue(IsHtmlProperty, value);
                OnPropertyChanged(nameof(IsHtml));
            }
        }

        public int MaxLength
        {
            get => (int) GetValue(MaxLengthProperty);
            set
            {
                SetValue(MaxLengthProperty, value);
                OnPropertyChanged(nameof(MaxLength));
            }
        }
    }
}