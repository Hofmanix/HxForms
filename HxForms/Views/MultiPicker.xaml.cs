using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HxForms.Views.Events;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HxForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiPicker : ContentView
    {
        public static readonly BindableProperty PlaceholderProperty
            = BindableProperty.Create("Placeholder", typeof(string), typeof(MultiPicker), string.Empty, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var picker = bindable as MultiPicker;
                    if (picker != null)
                    {
                        picker.PlaceholderLabel.Text = (string) newValue;
                    }
                });
        public static readonly BindableProperty SelectPageTitleProperty
            = BindableProperty.Create("SelectPageTitle", typeof(string), typeof(MultiPicker), string.Empty, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var picker = bindable as MultiPicker;
                    if (picker != null)
                    {
                        picker.ItemsPickerModal.Title = (string) newValue;
                    }
                });
        public static readonly BindableProperty CloseSelectTextProperty
            = BindableProperty.Create("CloseSelectText", typeof(string), typeof(MultiPicker), "Close", propertyChanged: 
                (bindable, oldValue, newValue) =>
                {
                    var picker = bindable as MultiPicker;
                    if (picker != null) 
                    {
                        picker.ItemsPickerModal.CloseSelectText = (string)newValue;
                    }
                });
        public static readonly BindableProperty ItemsSourceProperty
            = BindableProperty.Create("ItemsSource", typeof(IList), typeof(MultiPicker), propertyChanged:
                (bindable, value, newValue) =>
                {
                    var picker = bindable as MultiPicker;
                    if (picker != null)
                    {
                        picker.ItemsPickerModal.ResetItems();
                    }
                });

        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set
            {
                SetValue(PlaceholderProperty, value);
                OnPropertyChanged();
            }
        }

        public IList ItemsSource
        {
            get => (IList) GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                OnPropertyChanged();
            }
        }

        public string CloseSelectText
        {
            get => (string) GetValue(CloseSelectTextProperty);
            set
            {
                SetValue(CloseSelectTextProperty, value);
                OnPropertyChanged();
            }
        }

        public string SelectPageTitle
        {
            get => (string) GetValue(SelectPageTitleProperty);
            set
            {
                SetValue(SelectPageTitleProperty, value);
                OnPropertyChanged();
            }
        }

        public IList SelectedItems => _selectedItems;

        public event EventHandler<ItemsSelectedEventArgs> SelectionChanged; 

        protected MultiPickerItemsModal ItemsPickerModal;

        private BindingBase _itemDisplayBinding;
        private readonly ObservableCollection<object> _selectedItems = new ObservableCollection<object>();

        public MultiPicker()
        {
            InitializeComponent();
            ItemsPickerModal = new MultiPickerItemsModal(this);
            _selectedItems.CollectionChanged += SelectedItemsCollectionChanged;
        }

        private void SelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            SelectedItemsLabel.Text = string.Join(", ", _selectedItems);
            if (_selectedItems.Count == 0)
            {
                PlaceholderLabel.IsVisible = true;
                SelectedItemsLabel.IsVisible = false;
            }
            else
            {
                PlaceholderLabel.IsVisible = false;
                SelectedItemsLabel.IsVisible = true;
            }
            SelectionChanged?.Invoke(this, new ItemsSelectedEventArgs(ItemsSource, SelectedItems));

        }

        private async void MultiPickerClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(ItemsPickerModal));
        }
    }
}