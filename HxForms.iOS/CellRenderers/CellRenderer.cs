using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CellRenderer = HxForms.iOS.CellRenderers.CellRenderer;

[assembly: ExportRenderer(typeof(HxForms.Cells.Cell), typeof(CellRenderer))]
namespace HxForms.iOS.CellRenderers
{
    public class CellRenderer: Xamarin.Forms.Platform.iOS.CellRenderer
    {
        public static void Init()
        {
            var tmp = DateTime.Now;
        }

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as Cells.Cell;
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = view.SelectedBackgroundColor.ToUIColor()
            };

            return cell;
        }
    }
}