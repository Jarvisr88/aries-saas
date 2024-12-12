namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class DetailDescriptorSelector : MultiDetailDescriptorBase
    {
        public DetailDescriptorSelector()
        {
            this.Items = new FreezableCollection<DetailDescriptorTrigger>();
        }

        internal override DetailInfoWithContent CreateRowDetailInfo(RowDetailContainer container) => 
            new DetailDescriptorSelectorInfo(this, container);

        protected internal override IEnumerable<DataControlDetailDescriptor> GetDetailDescriptors(DataTreeBuilder treeBuilder, int rowHandle)
        {
            BindingValueHelper helper = new BindingValueHelper(null);
            helper.ApplyBindings(this, treeBuilder.View.DataControl.GetRow(rowHandle));
            DetailDescriptorBase input = (DetailDescriptorBase) helper.Value;
            helper.Clear();
            Func<IEnumerable<DataControlDetailDescriptor>> fallback = <>c.<>9__13_1;
            if (<>c.<>9__13_1 == null)
            {
                Func<IEnumerable<DataControlDetailDescriptor>> local1 = <>c.<>9__13_1;
                fallback = <>c.<>9__13_1 = (Func<IEnumerable<DataControlDetailDescriptor>>) (() => DetailDescriptorBase.EmptyDetailDescriptors);
            }
            return input.Return<DetailDescriptorBase, IEnumerable<DataControlDetailDescriptor>>(x => x.GetDetailDescriptors(treeBuilder, rowHandle), fallback);
        }

        protected override void OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
        {
            if ((e.Index >= 0) && (e.Index < this.Items.Count))
            {
                e.CollectionItem = this.Items[e.Index];
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.PopulateDetailDescriptors();
        }

        protected override void OnMasterControlChanged()
        {
            this.PopulateDetailDescriptors();
            base.OnMasterControlChanged();
        }

        private void PopulateDetailDescriptors()
        {
            if (base.IsInitialized && !this.Items.IsFrozen)
            {
                if (!DesignerProperties.GetIsInDesignMode(this.Items))
                {
                    this.Items.Freeze();
                }
                if (this.DefaultValue != null)
                {
                    base.DetailDescriptorsCore.Add(this.DefaultValue);
                }
                foreach (DetailDescriptorTrigger trigger in this.Items)
                {
                    base.DetailDescriptorsCore.Add(trigger.DetailDescriptor);
                }
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Content), GridStoreAlwaysProperty, XtraResetProperty(ResetPropertyMode.None)]
        public DetailDescriptorBase DefaultValue { get; set; }

        [XtraSerializableProperty(true, false, false), GridStoreAlwaysProperty, XtraResetProperty(ResetPropertyMode.None)]
        public FreezableCollection<DetailDescriptorTrigger> Items { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DetailDescriptorSelector.<>c <>9 = new DetailDescriptorSelector.<>c();
            public static Func<IEnumerable<DataControlDetailDescriptor>> <>9__13_1;

            internal IEnumerable<DataControlDetailDescriptor> <GetDetailDescriptors>b__13_1() => 
                DetailDescriptorBase.EmptyDetailDescriptors;
        }
    }
}

