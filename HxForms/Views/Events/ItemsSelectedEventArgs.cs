using System;
using System.Collections;
using System.Collections.Generic;
namespace HxForms.Views.Events
{
    public class ItemsSelectedEventArgs: EventArgs
    {
        public IList AllItems { get; }
        public IList SelectedItems { get; }

        public ItemsSelectedEventArgs(IList allItems, IList selectedItems)
        {
            AllItems = allItems;
            SelectedItems = selectedItems;
        }
    }
}
