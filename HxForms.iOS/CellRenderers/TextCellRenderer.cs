using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TextCell = HxForms.Cells.TextCell;
using TextCellRenderer = HxForms.iOS.CellRenderers.TextCellRenderer;

[assembly: ExportRenderer(typeof(TextCell), typeof(TextCellRenderer))]
namespace HxForms.iOS.CellRenderers
{
    public class TextCellRenderer: Xamarin.Forms.Platform.iOS.TextCellRenderer
    {
        public static void Init()
        {
            var tmp = DateTime.Now;
        }

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as Cells.TextCell;
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = view.SelectedBackgroundColor.ToUIColor()
            };

            return cell;
        }
    }
}