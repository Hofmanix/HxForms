using System.ComponentModel;
using Android.Content;
using Android.Views;
using HxForms.Cells;
using HxForms.Droid.CellRenderers;
using HxForms.Droid.ViewRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;
using ACheckBox = Android.Widget.CheckBox;

[assembly: ExportRenderer(typeof(HxForms.Cells.Cell), typeof(HxForms.Droid.CellRenderers.CellRenderer))]
namespace HxForms.Droid.CellRenderers
{
    public class CheckBoxCellRenderer: CellRenderer
    {
        private CheckBoxCellView _view;

        protected override AView GetCellCore(Xamarin.Forms.Cell item, AView contentView, ViewGroup parent, Context context)
        {
            var cell = (CheckBoxCell) Cell;

            if ((_view = contentView as CheckBoxCellView) == null)
            {
                _view = new CheckBoxCellView(context, item);
            }

            _view.Cell = cell;

            UpdateText();
            UpdateChecked();
            UpdateHeight();
            UpdateIsEnabled(_view, cell);

            return _view;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == CheckBoxCell.TextProperty.PropertyName)
            {
                UpdateText();
            }
            else if (args.PropertyName == CheckBoxCell.OnProperty.PropertyName)
            {
                UpdateChecked();
            }
            else if (args.PropertyName == "RenderHeight")
            {
                UpdateHeight();
            }
            else if (args.PropertyName == Xamarin.Forms.Cell.IsEnabledProperty.PropertyName)
            {
                UpdateIsEnabled(_view, (CheckBoxCell) sender);
            }
        }

        private void UpdateChecked()
        {
            ((ACheckBox)_view.AccessoryView).Checked = ((CheckBoxCell)Cell).On;
        }

        private void UpdateIsEnabled(CheckBoxCellView cell, CheckBoxCell checkBoxCell)
        {
            cell.Enabled = checkBoxCell.IsEnabled;
            if (cell.AccessoryView is ACheckBox aCheckBox)
            {
                aCheckBox.Enabled = checkBoxCell.IsEnabled;
            }
        }

        private void UpdateHeight()
        {
            _view.SetRenderHeight(Cell.RenderHeight);
        }

        private void UpdateText()
        {
            _view.MainText = ((CheckBoxCell) Cell).Text;
        }
    }
}