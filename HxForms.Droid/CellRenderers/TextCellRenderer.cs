﻿using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TextCell = HxForms.Cells.TextCell;
using TextCellRenderer = HxForms.Droid.CellRenderers.TextCellRenderer;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(TextCell), typeof(TextCellRenderer))]
namespace HxForms.Droid.CellRenderers
{
    public class TextCellRenderer: Xamarin.Forms.Platform.Android.TextCellRenderer
    {
        private View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override View GetCellCore(Xamarin.Forms.Cell item, View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            _selected = false;
            _unselectedBackground = _cellCore.Background;

            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if (e.PropertyName == "IsSelected")
            {
                _selected = !_selected;

                if (_selected)
                {
                    var cell = sender as Cells.TextCell;
                    _cellCore.SetBackgroundColor(cell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }
}