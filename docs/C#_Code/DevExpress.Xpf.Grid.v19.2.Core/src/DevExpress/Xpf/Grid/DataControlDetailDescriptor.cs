namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("DataControl")]
    public class DataControlDetailDescriptor : DetailDescriptorBase, IDataControlOwner
    {
        internal const string DataControlPropertyName = "DataControl";
        public static readonly DependencyProperty DataControlProperty;
        public static readonly DependencyProperty ItemsSourceValueConverterProperty;
        private static readonly DependencyPropertyKey HeaderContentPropertyKey;
        public static readonly DependencyProperty HeaderContentProperty;
        public static readonly DependencyProperty ItemsSourcePathProperty;
        public static readonly DependencyProperty ParentPathProperty;
        private readonly DetailDescriptorContainer detailDescriptorContainer;
        private CustomGetParentEventEventHandler customGetParentEvent;
        private PropertyChangeListener headerContentListener;
        private bool isColumnsPopulated;

        public event CustomGetParentEventEventHandler CustomGetParent
        {
            add
            {
                this.customGetParentEvent += value;
            }
            remove
            {
                this.customGetParentEvent -= value;
            }
        }

        static DataControlDetailDescriptor()
        {
            Type ownerType = typeof(DataControlDetailDescriptor);
            DataControlProperty = DependencyPropertyManager.Register("DataControl", typeof(DataControlBase), ownerType, new PropertyMetadata(null, (d, e) => ((DataControlDetailDescriptor) d).OnDataControlChanged((DataControlBase) e.OldValue)));
            HeaderContentPropertyKey = DependencyPropertyManager.RegisterReadOnly("HeaderContent", typeof(object), ownerType, new PropertyMetadata(null));
            HeaderContentProperty = HeaderContentPropertyKey.DependencyProperty;
            ItemsSourceValueConverterProperty = DependencyPropertyManager.Register("ItemsSourceValueConverter", typeof(IValueConverter), ownerType, new PropertyMetadata(null));
            ItemsSourcePathProperty = DependencyPropertyManager.Register("ItemsSourcePath", typeof(string), ownerType, new PropertyMetadata(string.Empty));
            ParentPathProperty = DependencyPropertyManager.Register("ParentPath", typeof(string), ownerType, new PropertyMetadata(string.Empty));
        }

        public DataControlDetailDescriptor()
        {
            this.detailDescriptorContainer = new DetailDescriptorContainer(this);
            Binding binding = new Binding(DataControlProperty.GetName() + ".View." + DataViewBase.DetailHeaderContentProperty.GetName());
            binding.Source = this;
            this.headerContentListener = PropertyChangeListener.Create(binding, obj => base.SetValue(HeaderContentPropertyKey, obj));
        }

        protected override IEnumerable<DetailDescriptorContainer> CreateDataControlDetailDescriptors()
        {
            ObservableCollection<DetailDescriptorContainer> collection1 = new ObservableCollection<DetailDescriptorContainer>();
            collection1.Add(this.detailDescriptorContainer);
            return collection1;
        }

        internal override DetailInfoWithContent CreateRowDetailInfo(RowDetailContainer container) => 
            new DataControlDetailInfo(this, container);

        bool IDataControlOwner.CanGroupColumn(ColumnBase column) => 
            column.GetActualAllowGroupingCore();

        bool IDataControlOwner.CanSortColumn(ColumnBase column) => 
            column.GetActualAllowSorting();

        void IDataControlOwner.EnumerateOwnerDataControls(Action<DataControlBase> action)
        {
            base.Owner.EnumerateOwnerDataControls(action);
        }

        void IDataControlOwner.EnumerateOwnerDetailDescriptors(Action<DetailDescriptorBase> action)
        {
            action(this);
            base.Owner.EnumerateOwnerDetailDescriptors(action);
        }

        DataControlBase IDataControlOwner.FindDetailDataControlByRow(object detailRow) => 
            this.FindDetailDataControlByRow(detailRow);

        object IDataControlOwner.GetParentRow(object detailRow) => 
            this.GetParentRow(detailRow);

        void IDataControlOwner.ValidateMasterDetailConsistency()
        {
            this.ValidateMasterDetailConsistency();
        }

        private DataControlBase FindDetailDataControlByRow(object detailRow)
        {
            DataControlBase base3;
            Stack<object> parentRows = new Stack<object>();
            this.DataControl.EnumerateThisAndOwnerDataControls(delegate (DataControlBase dataControl) {
                parentRows.Push(detailRow);
                if (!ReferenceEquals(dataControl, this.DataControl.GetRootDataControl()))
                {
                    detailRow = dataControl.DataControlOwner.GetParentRow(detailRow);
                }
            });
            DataControlBase rootDataControl = this.DataControl.GetRootDataControl();
            int rowHandle = -2147483648;
            using (Stack<object>.Enumerator enumerator = parentRows.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (rowHandle != -2147483648)
                        {
                            rootDataControl = rootDataControl.DetailDescriptorCore.GetChildDataControl(rootDataControl, rowHandle, current);
                        }
                        rowHandle = rootDataControl.DataProviderBase.FindRowByRowValue(current);
                        if (rowHandle != -2147483648)
                        {
                            continue;
                        }
                        base3 = null;
                    }
                    else
                    {
                        return rootDataControl;
                    }
                    break;
                }
            }
            return base3;
        }

        internal override DataControlBase GetChildDataControl(DataControlBase parent, int parentRowHandle, object detailRow)
        {
            parent.MasterDetailProvider.SetMasterRowExpanded(parentRowHandle, true, this);
            DataControlBase base2 = parent.MasterDetailProvider.FindDetailDataControl(parentRowHandle, this);
            return ((base2.DataProviderBase.FindRowByRowValue(detailRow) == -2147483648) ? null : base2);
        }

        [IteratorStateMachine(typeof(<GetDetailDescriptors>d__58))]
        protected internal override IEnumerable<DataControlDetailDescriptor> GetDetailDescriptors(DataTreeBuilder treeBuilder, int rowHandle)
        {
            <GetDetailDescriptors>d__58 d__1 = new <GetDetailDescriptors>d__58(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        internal BindingBase GetItemsSourceBinding()
        {
            if (string.IsNullOrEmpty(this.ItemsSourcePath) && (this.ItemsSourceValueConverter == null))
            {
                return this.ItemsSourceBinding;
            }
            Binding binding1 = new Binding(this.ItemsSourcePath ?? string.Empty);
            binding1.Converter = this.ItemsSourceValueConverter;
            return binding1;
        }

        private object GetParentRow(object detailRow)
        {
            CustomGetParentEventArgs e = new CustomGetParentEventArgs(detailRow);
            if (this.customGetParentEvent != null)
            {
                this.customGetParentEvent(this.DataControl, e);
            }
            return (!e.Handled ? ((string.IsNullOrEmpty(this.ParentPath) || (detailRow == null)) ? null : CriteriaCompiler.ToUntypedDelegate(CriteriaOperator.Parse(this.ParentPath, new object[0]), new CriteriaCompiledContextDescriptorDescripted(TypeDescriptor.GetProperties(detailRow.GetType())))(detailRow)) : e.Parent);
        }

        protected internal override bool HasDataControlDetailDescriptor() => 
            true;

        private void OnDataControlChanged(DataControlBase oldValue)
        {
            if (oldValue != null)
            {
                base.RemoveLogicalChild(oldValue);
                oldValue.DataControlOwner = null;
            }
            if (this.DataControl != null)
            {
                base.AddLogicalChild(this.DataControl);
                this.DataControl.DataControlOwner = this;
                this.DataControl.UpdateOwnerDetailDescriptor();
            }
            this.ValidateMasterDetailConsistency();
            this.UpdateMasterControl();
        }

        internal override void OnDetach()
        {
            base.SynchronizationQueues.Clear();
        }

        internal void PopulateColumnsIfNeeded(DataProviderBase dataProvider)
        {
            if (!this.isColumnsPopulated)
            {
                if (this.DataControl.ShouldPopulateColumns())
                {
                    this.DataControl.PopulateColumnsIfNeeded(dataProvider);
                }
                this.DataControl.syncPropertyLocker.DoLockedAction(delegate {
                    foreach (ColumnBase base2 in this.DataControl.ColumnsCore)
                    {
                        base2.UpdateColumnTypeProperties(dataProvider);
                    }
                });
                this.isColumnsPopulated = true;
            }
        }

        internal override void SynchronizeDetailTree()
        {
            base.SynchronizeDetailTree();
            if (this.DataControl != null)
            {
                this.DataControl.MasterDetailProvider.SynchronizeDetailTree();
            }
        }

        internal override void UpdateDetailDataControls(Action<DataControlBase> updateOpenDetailMethod, Action<DataControlBase> updateClosedDetailMethod = null)
        {
            if (this.DataControl != null)
            {
                Func<DataControlBase, DataControlBase> getTarget = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Func<DataControlBase, DataControlBase> local1 = <>c.<>9__39_0;
                    getTarget = <>c.<>9__39_0 = dataControl => dataControl;
                }
                DataControlOriginationElementHelper.EnumerateDependentElementsSkipOriginationControl<DataControlBase>(this.DataControl, getTarget, updateOpenDetailMethod, updateClosedDetailMethod);
                this.DataControl.MasterDetailProvider.UpdateDetailDataControls(updateOpenDetailMethod, updateClosedDetailMethod);
            }
        }

        internal override void UpdateDetailViewIndents(ObservableCollection<DetailIndent> ownerIndents, Thickness margin)
        {
            base.UpdateDetailViewIndents(ownerIndents, margin);
            if (this.DataControl != null)
            {
                this.DataControl.UpdateChildrenDetailViewIndents(base.DetailViewIndents);
            }
        }

        internal override void UpdateMasterControl()
        {
            if ((this.DataControl != null) && (this.DataControl.DataView != null))
            {
                this.DataControl.DataView.UpdateMasterDetailViewProperties();
            }
        }

        internal override void UpdateOriginationDataControls(Action<DataControlBase> updateMethod)
        {
            if (this.DataControl != null)
            {
                updateMethod(this.DataControl);
                this.DataControl.MasterDetailProvider.UpdateOriginationDataControls(updateMethod);
            }
        }

        private void ValidateMasterDetailConsistency()
        {
            if (this.DataControl != null)
            {
                this.DataControl.ThrowNotSupportedInDetailException();
                this.DataControl.ThrowNotSupportedInMasterDetailException();
                if (this.DataControl.DataView != null)
                {
                    this.DataControl.DataView.ThrowNotSupportedInMasterDetailException();
                    this.DataControl.DataView.ThrowNotSupportedInDetailException();
                }
            }
        }

        protected override IEnumerator LogicalChildren =>
            DataControlBase.GetSingleObjectEnumerator(this.DataControl);

        public object HeaderContent =>
            base.GetValue(HeaderContentProperty);

        [Description(""), Category("Master-Detail"), XtraSerializableProperty(XtraSerializationVisibility.Content), GridStoreAlwaysProperty]
        public DataControlBase DataControl
        {
            get => 
                (DataControlBase) base.GetValue(DataControlProperty);
            set => 
                base.SetValue(DataControlProperty, value);
        }

        public BindingBase ItemsSourceBinding { get; set; }

        [Description(""), Category("Master-Detail"), XtraSerializableProperty(-1)]
        public string ItemsSourcePath
        {
            get => 
                (string) base.GetValue(ItemsSourcePathProperty);
            set => 
                base.SetValue(ItemsSourcePathProperty, value);
        }

        public IValueConverter ItemsSourceValueConverter
        {
            get => 
                (IValueConverter) base.GetValue(ItemsSourceValueConverterProperty);
            set => 
                base.SetValue(ItemsSourceValueConverterProperty, value);
        }

        [Description(""), Category("Master-Detail"), XtraSerializableProperty]
        public string ParentPath
        {
            get => 
                (string) base.GetValue(ParentPathProperty);
            set => 
                base.SetValue(ParentPathProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControlDetailDescriptor.<>c <>9 = new DataControlDetailDescriptor.<>c();
            public static Func<DataControlBase, DataControlBase> <>9__39_0;

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataControlDetailDescriptor) d).OnDataControlChanged((DataControlBase) e.OldValue);
            }

            internal DataControlBase <UpdateDetailDataControls>b__39_0(DataControlBase dataControl) => 
                dataControl;
        }

        [CompilerGenerated]
        private sealed class <GetDetailDescriptors>d__58 : IEnumerable<DataControlDetailDescriptor>, IEnumerable, IEnumerator<DataControlDetailDescriptor>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DataControlDetailDescriptor <>2__current;
            private int <>l__initialThreadId;
            public DataControlDetailDescriptor <>4__this;

            [DebuggerHidden]
            public <GetDetailDescriptors>d__58(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>2__current = this.<>4__this;
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<DataControlDetailDescriptor> IEnumerable<DataControlDetailDescriptor>.GetEnumerator()
            {
                DataControlDetailDescriptor.<GetDetailDescriptors>d__58 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new DataControlDetailDescriptor.<GetDetailDescriptors>d__58(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Grid.DataControlDetailDescriptor>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            DataControlDetailDescriptor IEnumerator<DataControlDetailDescriptor>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

