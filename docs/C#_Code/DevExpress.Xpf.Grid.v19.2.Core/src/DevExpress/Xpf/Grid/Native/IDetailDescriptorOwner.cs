namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public interface IDetailDescriptorOwner
    {
        bool CanAssignTo(DataControlBase dataControl);
        void EnumerateOwnerDataControls(Action<DataControlBase> action);
        void EnumerateOwnerDetailDescriptors(Action<DetailDescriptorBase> action);
        void InvalidateIndents();
        void InvalidateTree();
    }
}

