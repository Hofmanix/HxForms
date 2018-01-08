using Xamarin.Forms;

namespace HxForms.Cells
{
    public class Cell: Xamarin.Forms.Cell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty = 
            BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(Cell), Color.Default);
        public static readonly BindableProperty SelectedTextColorProperty =
            BindableProperty.Create("SelectedTextColor", typeof(Color), typeof(Cell), Color.Default);

        public Color SelectedBackgroundColor
        {
            get => (Color) GetValue(SelectedBackgroundColorProperty);
            set => SetValue(SelectedBackgroundColorProperty, value);
        }

        public Color SelectedTextColor
        {
            get => (Color) GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }
    }
}