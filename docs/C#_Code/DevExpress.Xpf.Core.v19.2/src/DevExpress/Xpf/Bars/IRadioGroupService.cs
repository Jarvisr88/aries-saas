namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;

    public interface IRadioGroupService
    {
        bool CanUncheck(IBarCheckItem element);
        void OnChecked(IBarCheckItem element);
    }
}

