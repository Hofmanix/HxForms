using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HxForms.Cells;
using Xamarin.Forms;

namespace HxForms.Views
{
    public class MultiPickerItemsModal : ContentPage
    {
        public string CloseSelectText
        {
            get => _cancelToolbarItem.Text;
            set => _cancelToolbarItem.Text = value;
        }

        private ListView _pickerItems;
        private ToolbarItem _cancelToolbarItem;
        private MultiPicker _multiPicker;
        private List<ItemBindingContext> _itemsList;

        public MultiPickerItemsModal(MultiPicker multiPicker)
        {
            _multiPicker = multiPicker;
            InitToolbarItems();
            InitListView();
            Content = new StackLayout
            {
                Children = {
                    _pickerItems
                }
            };
        }

        public void ResetItems()
        {
            _itemsList.Clear();
            _multiPicker.SelectedItems.Clear();
            foreach (var item in _multiPicker.ItemsSource)
            {
                var itemBindingContext = new ItemBindingContext
                {
                    Item = item
                };
                
                _itemsList.Add(itemBindingContext);
            }
        }

        public void SelectedItemsChanged()
        {
            foreach (var item in _itemsList)
            {
                if (_multiPicker.SelectedItems.Contains(item.Item))
                {
                    item.IsChecked = true;
                }
                else
                {
                    item.IsChecked = false;
                }
            }
        }

        private void SetBinding()
        {
            _pickerItems.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new CheckBoxCell();
                cell.SetBinding(CheckBoxCell.TextProperty, new Binding("ItemText"));
                cell.SetBinding(CheckBoxCell.OnProperty, new Binding("IsChecked"));
                cell.OnChanged += (sender, args) =>
                {
                    if (sender is CheckBoxCell selectedCell && selectedCell.BindingContext is ItemBindingContext itemBinding)
                    {
                        if (args.Value)
                        {
                            _multiPicker.SelectedItems.Add(itemBinding.Item);
                        }
                        else
                        {
                            _multiPicker.SelectedItems.Remove(itemBinding.Item);
                        }
                    }
                };
                return cell;
            });
        }

        private void InitToolbarItems()
        {
            _cancelToolbarItem = new ToolbarItem
            {
                Text = _multiPicker.CloseSelectText,
                Command = new Command(async () => { await Navigation.PopModalAsync(); })
            };
            ToolbarItems.Add(_cancelToolbarItem);
        }

        private void InitListView()
        {
            _pickerItems = new ListView();
            _pickerItems.ItemsSource = _itemsList = new List<ItemBindingContext>();
            _pickerItems.ItemSelected += PickerItemsOnItemSelected;
            SetBinding();
        }

        private void PickerItemsOnItemSelected(object o, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            if (selectedItemChangedEventArgs.SelectedItem != null)
            {
                var item = _pickerItems.TemplatedItems[_itemsList.IndexOf(selectedItemChangedEventArgs.SelectedItem as ItemBindingContext)];
                if (item is CheckBoxCell cbCell)
                {
                    cbCell.On = !cbCell.On;
                }

                _pickerItems.SelectedItem = null;
            }
        }

        public class ItemBindingContext : INotifyPropertyChanged
        {
            public object Item
            {
                get => _item;
                set
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));
                }
            }

            public object ItemText
            {
                get => Item.ToString();
            }

            public bool IsChecked
            {
                get => _isChecked;
                set
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }

            private object _item;
            private bool _isChecked;

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

