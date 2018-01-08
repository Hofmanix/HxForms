using System;
using System.Collections;
using System.IO;
using Xamarin.Forms;

namespace HxForms.Layouts
{
    public class RepetitiveLayout: StackLayout
    {
        public static readonly BindableProperty ItemTemplateProperty
            = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(RepetitiveLayout), default(DataTemplate));
        public static readonly BindableProperty ItemsSourceProperty
            = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(RepetitiveLayout), default(IEnumerable), BindingMode.OneWay, null, ItemsChanged);  


        public static void ItemsChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            if (bindableObject is RepetitiveLayout repetitiveLayout)
            {
                repetitiveLayout.ItemsChanged();
            }
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public event EventHandler<ItemTappedEventArgs> ItemTapped;

        public RepetitiveLayout()
        {
        }

        protected virtual void ItemsChanged()
        {
            Children.Clear();
            foreach (var item in ItemsSource)
            {
                Children.Add(CreateViewFor(item));
            }
        }

        protected virtual View CreateViewFor(object item)
        {
            var content = ItemTemplate.CreateContent();
            View view;
            if (content is View)
            {
                view = content as View;
            }
            else if (content is ViewCell viewCell)
            {
                view = viewCell.View;
            }
            else
            {
                throw new InvalidDataException("Data template is neither view nor viewCell");
            }

            view.BindingContext = item;
            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    ItemTapped?.Invoke(this, new ItemTappedEventArgs(ItemsSource, item));
                })
            });
            return view;
        }
    }
}