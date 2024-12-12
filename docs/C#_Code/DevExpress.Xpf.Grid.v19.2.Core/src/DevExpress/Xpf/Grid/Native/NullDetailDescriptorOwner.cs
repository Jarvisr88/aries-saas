namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class NullDetailDescriptorOwner : IDetailDescriptorOwner
    {
        public static readonly IDetailDescriptorOwner Instance = new NullDetailDescriptorOwner();

        private NullDetailDescriptorOwner()
        {
        }

        bool IDetailDescriptorOwner.CanAssignTo(DataControlBase dataControl) => 
            true;

        void IDetailDescriptorOwner.EnumerateOwnerDataControls(Action<DataControlBase> action)
        {
        }

        void IDetailDescriptorOwner.EnumerateOwnerDetailDescriptors(Action<DetailDescriptorBase> action)
        {
        }

        void IDetailDescriptorOwner.InvalidateIndents()
        {
        }

        void IDetailDescriptorOwner.InvalidateTree()
        {
        }
    }
}

