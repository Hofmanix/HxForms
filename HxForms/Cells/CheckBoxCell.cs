using System;
using HxForms.Views;
using Xamarin.Forms;

namespace HxForms.Cells
{
    public class CheckBoxCell: Cell
    {
        public static readonly BindableProperty OnProperty = BindableProperty.Create("On", typeof(bool), typeof(CheckBoxCell), false, propertyChanged:
            (bindable, value, newValue) =>
            {
                var checkBoxCell = (CheckBoxCell) bindable;
                EventHandler<CheckedEventArgs> handler = checkBoxCell.OnChanged;
                handler?.Invoke(bindable, new CheckedEventArgs((bool) newValue));
            }, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(CheckBoxCell), default(string));

        public bool On
        {
            get => (bool) GetValue(OnProperty);
            set => SetValue(OnProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public event EventHandler<CheckedEventArgs> OnChanged;
    }
}