namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Services;
    using System;
    using System.ComponentModel.Design;

    public class SpinEditEx : SpinEdit
    {
        public SpinEditEx()
        {
            SpinEditRangeServiceEx serviceInstance = new SpinEditRangeServiceEx(this);
            ((IServiceContainer) base.PropertyProvider).AddService(typeof(RangeEditorService), serviceInstance);
        }
    }
}

