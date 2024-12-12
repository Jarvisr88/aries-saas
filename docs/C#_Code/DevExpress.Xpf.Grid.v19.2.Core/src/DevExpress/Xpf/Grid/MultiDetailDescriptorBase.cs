namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Extensions;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class MultiDetailDescriptorBase : ContentDetailDescriptor, IDetailDescriptorOwner
    {
        private readonly ObservableCollectionCore<DetailDescriptorContainer> dataControlDescriptors = new ObservableCollectionCore<DetailDescriptorContainer>();

        static MultiDetailDescriptorBase()
        {
            EventManager.RegisterClassHandler(typeof(MultiDetailDescriptorBase), DXSerializer.CreateCollectionItemEvent, (s, e) => ((MultiDetailDescriptorBase) s).OnCreateCollectionItem(e));
        }

        protected MultiDetailDescriptorBase()
        {
            this.DetailDescriptorsCore = new DetailDescriptorCollection(this);
        }

        protected override IEnumerable<DetailDescriptorContainer> CreateDataControlDetailDescriptors() => 
            this.dataControlDescriptors;

        bool IDetailDescriptorOwner.CanAssignTo(DataControlBase dataControl)
        {
            throw new InvalidOperationException("Specified detail descriptor is already the child of another detail descriptor.");
        }

        void IDetailDescriptorOwner.EnumerateOwnerDataControls(Action<DataControlBase> action)
        {
            base.Owner.EnumerateOwnerDataControls(action);
        }

        void IDetailDescriptorOwner.EnumerateOwnerDetailDescriptors(Action<DetailDescriptorBase> action)
        {
            action(this);
            base.Owner.EnumerateOwnerDetailDescriptors(action);
        }

        void IDetailDescriptorOwner.InvalidateIndents()
        {
            base.Owner.InvalidateIndents();
        }

        void IDetailDescriptorOwner.InvalidateTree()
        {
            base.InvalidateTree();
        }

        internal override DataControlBase GetChildDataControl(DataControlBase parent, int parentRowHandle, object detailRow)
        {
            Func<DataControlBase, bool> predicate = <>c.<>9__21_1;
            if (<>c.<>9__21_1 == null)
            {
                Func<DataControlBase, bool> local1 = <>c.<>9__21_1;
                predicate = <>c.<>9__21_1 = x => x != null;
            }
            return (from x in this.DetailDescriptorsCore select x.GetChildDataControl(parent, parentRowHandle, detailRow)).FirstOrDefault<DataControlBase>(predicate);
        }

        private string GetSerializationName(XtraPropertyInfo item)
        {
            if (item.ChildProperties == null)
            {
                return null;
            }
            string str = null;
            XtraPropertyInfo info = item.ChildProperties["Name"];
            if ((info != null) && (info.Value != null))
            {
                str = info.Value.ToString();
            }
            return str;
        }

        protected internal override bool HasDataControlDetailDescriptor()
        {
            Func<DetailDescriptorBase, bool> predicate = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<DetailDescriptorBase, bool> local1 = <>c.<>9__0_0;
                predicate = <>c.<>9__0_0 = x => x.HasDataControlDetailDescriptor();
            }
            return this.DetailDescriptorsCore.Any<DetailDescriptorBase>(predicate);
        }

        protected virtual void OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
        {
            if (e.Item != null)
            {
                string name = this.GetSerializationName(e.Item);
                int num = -1;
                num = !string.IsNullOrEmpty(name) ? this.DetailDescriptorsCore.FindIndex<DetailDescriptorBase>(x => (x.Name == name)) : e.Index;
                if ((this.DetailDescriptorsCore != null) && ((num >= 0) && ((num < this.DetailDescriptorsCore.Count) && !(this.DetailDescriptorsCore[num] is ContentDetailDescriptor))))
                {
                    e.CollectionItem = this.DetailDescriptorsCore[num];
                }
            }
        }

        internal void OnDescriptorAdded(DetailDescriptorBase descriptor)
        {
            base.AddLogicalChild(descriptor);
            descriptor.Owner = this;
            this.UpdateNestedDetailDescriptorsCache();
        }

        internal void OnDescriptorRemoved(DetailDescriptorBase descriptor)
        {
            descriptor.Owner = null;
            base.RemoveLogicalChild(descriptor);
            this.UpdateNestedDetailDescriptorsCache();
        }

        internal override void OnDetach()
        {
            Action<DetailDescriptorBase> updateAction = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Action<DetailDescriptorBase> local1 = <>c.<>9__19_0;
                updateAction = <>c.<>9__19_0 = detailDescriptor => detailDescriptor.OnDetach();
            }
            this.UpdateChildDetailDescriptors(updateAction);
        }

        internal override void SynchronizeDetailTree()
        {
            base.SynchronizeDetailTree();
            Action<DetailDescriptorBase> updateAction = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Action<DetailDescriptorBase> local1 = <>c.<>9__10_0;
                updateAction = <>c.<>9__10_0 = detailDescriptor => detailDescriptor.SynchronizeDetailTree();
            }
            this.UpdateChildDetailDescriptors(updateAction);
        }

        private void UpdateChildDetailDescriptors(Action<DetailDescriptorBase> updateAction)
        {
            foreach (DetailDescriptorBase base2 in this.DetailDescriptorsCore)
            {
                updateAction(base2);
            }
        }

        internal override void UpdateDetailDataControls(Action<DataControlBase> updateOpenDetailMethod, Action<DataControlBase> updateClosedDetailMethod = null)
        {
            this.UpdateChildDetailDescriptors(detailDescriptor => detailDescriptor.UpdateDetailDataControls(updateOpenDetailMethod, updateClosedDetailMethod));
        }

        internal override void UpdateDetailViewIndents(ObservableCollection<DetailIndent> ownerIndents, Thickness margin)
        {
            base.UpdateDetailViewIndents(ownerIndents, margin);
            this.UpdateChildDetailDescriptors(delegate (DetailDescriptorBase detailDescriptor) {
                Thickness thickness = new Thickness();
                detailDescriptor.UpdateDetailViewIndents(base.DetailViewIndents, thickness);
            });
        }

        internal override void UpdateMasterControl()
        {
            Action<DetailDescriptorBase> updateAction = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Action<DetailDescriptorBase> local1 = <>c.<>9__18_0;
                updateAction = <>c.<>9__18_0 = detailDescriptor => detailDescriptor.UpdateMasterControl();
            }
            this.UpdateChildDetailDescriptors(updateAction);
        }

        private void UpdateNestedDetailDescriptorsCache()
        {
            this.dataControlDescriptors.BeginUpdate();
            this.dataControlDescriptors.Clear();
            foreach (DetailDescriptorBase base2 in this.DetailDescriptorsCore)
            {
                foreach (DetailDescriptorContainer container in base2.DataControlDetailDescriptors)
                {
                    this.dataControlDescriptors.Add(container);
                }
            }
            this.dataControlDescriptors.EndUpdate();
            MultiDetailDescriptor owner = base.Owner as MultiDetailDescriptor;
            if (owner != null)
            {
                owner.UpdateNestedDetailDescriptorsCache();
            }
        }

        internal override void UpdateOriginationDataControls(Action<DataControlBase> updateMethod)
        {
            this.UpdateChildDetailDescriptors(detailDescriptor => detailDescriptor.UpdateOriginationDataControls(updateMethod));
        }

        internal DetailDescriptorCollection DetailDescriptorsCore { get; private set; }

        protected override IEnumerator LogicalChildren =>
            this.DetailDescriptorsCore.GetEnumerator();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultiDetailDescriptorBase.<>c <>9 = new MultiDetailDescriptorBase.<>c();
            public static Func<DetailDescriptorBase, bool> <>9__0_0;
            public static Action<DetailDescriptorBase> <>9__10_0;
            public static Action<DetailDescriptorBase> <>9__18_0;
            public static Action<DetailDescriptorBase> <>9__19_0;
            public static Func<DataControlBase, bool> <>9__21_1;

            internal void <.cctor>b__1_0(object s, XtraCreateCollectionItemEventArgs e)
            {
                ((MultiDetailDescriptorBase) s).OnCreateCollectionItem(e);
            }

            internal bool <GetChildDataControl>b__21_1(DataControlBase x) => 
                x != null;

            internal bool <HasDataControlDetailDescriptor>b__0_0(DetailDescriptorBase x) => 
                x.HasDataControlDetailDescriptor();

            internal void <OnDetach>b__19_0(DetailDescriptorBase detailDescriptor)
            {
                detailDescriptor.OnDetach();
            }

            internal void <SynchronizeDetailTree>b__10_0(DetailDescriptorBase detailDescriptor)
            {
                detailDescriptor.SynchronizeDetailTree();
            }

            internal void <UpdateMasterControl>b__18_0(DetailDescriptorBase detailDescriptor)
            {
                detailDescriptor.UpdateMasterControl();
            }
        }
    }
}

