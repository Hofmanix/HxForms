using System;
using Xamarin.Forms.Platform.iOS;
using SaturdayMP.XPlugins.iOS;
using System.Drawing;
using HxForms.Cells;
using System.ComponentModel;
using Xamarin.Forms;
using UIKit;

namespace HxForms.iOS.CellRenderers
{
    public class CheckBoxCellRenderer: CellRenderer
    {
        static readonly BindableProperty RealCellProperty = BindableProperty.CreateAttached("RealCell", typeof(UITableViewCell), typeof(Xamarin.Forms.Cell), null);

        public static void Init()
        {
            var tmp = DateTime.Now;
        }

        const string CellName = "HxForms.CheckBoxCell";

        public override UIKit.UITableViewCell GetCell(Xamarin.Forms.Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var tvc = reusableCell as CellTableViewCell;
            BEMCheckBox bemCheckBox = null;
            if (tvc == null) 
            {
                tvc = new CellTableViewCell(UIKit.UITableViewCellStyle.Value1, CellName);
            }
            else 
            {
                bemCheckBox = tvc.AccessoryView as BEMCheckBox;
                tvc.Cell.PropertyChanged -= OnCellPropertyChanged;
            }

            SetRealCell(item, tvc);

            if (bemCheckBox == null) {
                bemCheckBox = new BEMCheckBox(Constants.CheckBoxSize);
                bemCheckBox.ValueChanged += OnCheckBoxValueChanged;
                tvc.AccessoryView = bemCheckBox;
            }

            var boolCell = (CheckBoxCell)item;

            tvc.Cell = item;
            tvc.Cell.PropertyChanged += OnCellPropertyChanged;
            tvc.AccessoryView = bemCheckBox;
            tvc.TextLabel.Text = boolCell.Text;

            bemCheckBox.On = boolCell.On;

            WireUpForceUpdateSizeRequested(item, tvc, tv);

            UpdateBackground(tvc, item);
            UpdateIsEnabled(tvc, boolCell);

            return tvc;
        }

        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            var boolCell = (CheckBoxCell)sender;
            var realCell = (CellTableViewCell)GetRealCell(boolCell);

            if (e.PropertyName == CheckBoxCell.OnProperty.PropertyName) 
            {
                ((BEMCheckBox)realCell.AccessoryView).SetOn(boolCell.On, true);
            }
            else if (e.PropertyName == CheckBoxCell.TextProperty.PropertyName)
            {
                realCell.TextLabel.Text = boolCell.Text;
            }
            else if (e.PropertyName == Xamarin.Forms.Cell.IsEnabledProperty.PropertyName)
            {
                UpdateIsEnabled(realCell, boolCell);
            }
        }

        private void OnCheckBoxValueChanged(object sender, EventArgs eventArgs) 
        {
            var view = (UIView)sender;
            var cb = (BEMCheckBox)view;

            CellTableViewCell realCell = null;
            while (view.Superview != null && realCell == null) 
            {
                view = view.Superview;
                realCell = view as CellTableViewCell;
            }

            if (realCell != null) 
            {
                ((CheckBoxCell)realCell.Cell).On = cb.On;
            }
        }

        private void UpdateIsEnabled(CellTableViewCell cell, CheckBoxCell checkBoxCell)
        {
            cell.UserInteractionEnabled = checkBoxCell.IsEnabled;
            cell.TextLabel.Enabled = checkBoxCell.IsEnabled;
            cell.DetailTextLabel.Enabled = checkBoxCell.IsEnabled;
            var bemCheckBox = cell.AccessoryView as BEMCheckBox;
            if (bemCheckBox != null) 
            {
                bemCheckBox.Enabled = checkBoxCell.IsEnabled;
            }
        }

        static UITableViewCell GetRealCell(BindableObject cell)
        {
            return (UITableViewCell)cell.GetValue(RealCellProperty);
        }

        void SetRealCell(BindableObject cell, UITableViewCell renderer)
        {
            cell.SetValue(RealCellProperty, renderer);
        }
    }
}
