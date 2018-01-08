using Android.Content;
using Android.Widget;
using HxForms.Cells;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace HxForms.Droid.CellRenderers
{
    public class CheckBoxCellView: BaseCellView, CompoundButton.IOnCheckedChangeListener
    {
        public CheckBoxCell Cell;

        public CheckBoxCellView(Context context, Xamarin.Forms.Cell cell) : base(context, cell)
        {
            var cb = new global::Android.Widget.CheckBox(context);
            cb.SetOnCheckedChangeListener(this);
            SetAccessoryView(cb);
            SetImageVisible(false);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            Cell.On = isChecked;
        }
    }
}