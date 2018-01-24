using System;
using HxForms.iOS.ViewRenderers;
using HxForms.iOS.CellRenderers;
namespace HxForms.iOS
{
    public static class HxForms
    {
        public static void Init() 
        {
            CheckBoxRenderer.InitRenderer();
            LabelRenderer.InitRenderer();
            CheckBoxCellRenderer.Init();
        }
    }
}
