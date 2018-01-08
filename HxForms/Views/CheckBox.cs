using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform;

namespace HxForms.Views
{
    public class CheckBox: View
    {
        public static readonly BindableProperty IsCheckedProperty
            = BindableProperty.Create("IsChecked", typeof(bool), typeof(CheckBox), false, propertyChanged:
                (bindable, value, newValue) =>
                {
                    EventHandler<CheckedEventArgs> eh = ((CheckBox) bindable).Checked;
                    if (eh != null)
                    {
                        eh(bindable, new CheckedEventArgs((bool) newValue));
                    }
                }, defaultBindingMode: BindingMode.TwoWay);

        public bool IsChecked
        {
            get => (bool) GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public event EventHandler<CheckedEventArgs> Checked; 

        public CheckBox()
        {
            
        }
    }
}