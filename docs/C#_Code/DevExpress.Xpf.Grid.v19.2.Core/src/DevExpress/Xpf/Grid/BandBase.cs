namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public abstract class BandBase : BaseColumn, IBandsOwner, ICollectionOwner
    {
        public static readonly DependencyProperty GridColumnProperty;
        public static readonly DependencyProperty GridRowProperty;
        public static readonly DependencyProperty PrintBandHeaderStyleProperty;
        public static readonly DependencyProperty ActualPrintBandHeaderStyleProperty;
        private static readonly DependencyPropertyKey ActualPrintBandHeaderStylePropertyKey;
        public static readonly DependencyProperty OverlayHeaderByChildrenProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualColumnGeneratorStyleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualColumnGeneratorTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualColumnGeneratorTemplateSelectorProperty;
        public static readonly DependencyProperty ColumnGeneratorStyleProperty;
        public static readonly DependencyProperty ColumnGeneratorTemplateProperty;
        public static readonly DependencyProperty ColumnGeneratorTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ColumnsItemsAttachedBehaviorProperty;
        public static readonly DependencyProperty ColumnsSourceProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualBandGeneratorStyleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualBandGeneratorTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualBandGeneratorTemplateSelectorProperty;
        public static readonly DependencyProperty BandGeneratorStyleProperty;
        public static readonly DependencyProperty BandGeneratorTemplateProperty;
        public static readonly DependencyProperty BandGeneratorTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BandsItemsAttachedBehaviorProperty;
        public static readonly DependencyProperty BandsSourceProperty;
        public static readonly DependencyProperty BandSerializationNameProperty;
        internal const string BandSerializationNamePropertyName = "BandSerializationName";
        public static readonly DependencyProperty BandSeparatorWidthProperty;
        public static readonly DependencyProperty BandCellSeparatorColorProperty;
        public static readonly DependencyProperty BandHeaderSeparatorColorProperty;
        private Locker lockBandsSourceUpdate = new Locker();
        private Locker lockColumnsSourceUpdate = new Locker();
        private IBandsOwner ownerCore;
        private BandsLayoutBase bandsLayout;
        private IBandsCollection bandsCore;
        private IBandColumnsCollection columnsCore;
        private ObservableCollection<BandRowDefinition> rowDefinitions;
        private ObservableCollection<BandColumnDefinition> columnDefinitions;
        private BandsHelper bandsHelper;

        static BandBase()
        {
            Type ownerType = typeof(BandBase);
            GridColumnProperty = DependencyProperty.RegisterAttached("GridColumn", typeof(int), ownerType, new PropertyMetadata(0, new PropertyChangedCallback(BandBase.OnGridColumnChanged)));
            GridRowProperty = DependencyProperty.RegisterAttached("GridRow", typeof(int), ownerType, new PropertyMetadata(0, new PropertyChangedCallback(BandBase.OnGridRowChanged)));
            PrintBandHeaderStyleProperty = DependencyProperty.Register("PrintBandHeaderStyle", typeof(Style), ownerType, new PropertyMetadata(null, (d, e) => ((BandBase) d).UpdateActualPrintBandHeaderStyle()));
            ActualPrintBandHeaderStylePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPrintBandHeaderStyle", typeof(Style), ownerType, new FrameworkPropertyMetadata(null));
            ActualPrintBandHeaderStyleProperty = ActualPrintBandHeaderStylePropertyKey.DependencyProperty;
            OverlayHeaderByChildrenProperty = DependencyProperty.Register("OverlayHeaderByChildren", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((BandBase) d).OnShowInBandsPanelChanged()));
            ActualColumnGeneratorStyleProperty = DependencyProperty.Register("ActualColumnGeneratorStyle", typeof(Style), ownerType, new PropertyMetadata(new PropertyChangedCallback(BandBase.OnColumnsItemsGeneratorTemplatePropertyChanged)));
            ActualColumnGeneratorTemplateProperty = DependencyProperty.Register("ActualColumnGeneratorTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(new PropertyChangedCallback(BandBase.OnColumnsItemsGeneratorTemplatePropertyChanged)));
            ActualColumnGeneratorTemplateSelectorProperty = DependencyProperty.Register("ActualColumnGeneratorTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(new PropertyChangedCallback(BandBase.OnColumnsItemsGeneratorTemplatePropertyChanged)));
            ColumnGeneratorStyleProperty = DependencyProperty.Register("ColumnGeneratorStyle", typeof(Style), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).RefreshColumnsSource()));
            ColumnGeneratorTemplateProperty = DependencyProperty.Register("ColumnGeneratorTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).RefreshColumnsSource()));
            ColumnGeneratorTemplateSelectorProperty = DependencyProperty.Register("ColumnGeneratorTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).RefreshColumnsSource()));
            ColumnsItemsAttachedBehaviorProperty = DependencyProperty.Register("ColumnsItemsAttachedBehavior", typeof(ItemsAttachedBehaviorExtendedLock<BandBase, ColumnBase>), ownerType, new PropertyMetadata(null));
            ColumnsSourceProperty = DependencyProperty.Register("ColumnsSource", typeof(IEnumerable), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).lockColumnsSourceUpdate.DoLockedAction(delegate {
                Func<BandBase, IList> getTargetFunction = <>c.<>9__27_7;
                if (<>c.<>9__27_7 == null)
                {
                    Func<BandBase, IList> local1 = <>c.<>9__27_7;
                    getTargetFunction = <>c.<>9__27_7 = band => band.ColumnsCore;
                }
                ItemsAttachedBehaviorExtendedLock<BandBase, ColumnBase>.OnItemsSourceExtLockPropertyChanged(d, e, ColumnsItemsAttachedBehaviorProperty, ActualColumnGeneratorTemplateProperty, ActualColumnGeneratorTemplateSelectorProperty, ActualColumnGeneratorStyleProperty, getTargetFunction, <>c.<>9__27_8 ??= band => band.CreateColumn(), null, null, null, null, true, true, null, false);
            })));
            ActualBandGeneratorStyleProperty = DependencyProperty.Register("ActualBandGeneratorStyle", typeof(Style), ownerType, new PropertyMetadata(new PropertyChangedCallback(BandBase.OnBandsItemsGeneratorTemplatePropertyChanged)));
            ActualBandGeneratorTemplateProperty = DependencyProperty.Register("ActualBandGeneratorTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(new PropertyChangedCallback(BandBase.OnBandsItemsGeneratorTemplatePropertyChanged)));
            ActualBandGeneratorTemplateSelectorProperty = DependencyProperty.Register("ActualBandGeneratorTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(new PropertyChangedCallback(BandBase.OnBandsItemsGeneratorTemplatePropertyChanged)));
            BandGeneratorStyleProperty = DependencyProperty.Register("BandGeneratorStyle", typeof(Style), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).RefreshBandsSource()));
            BandGeneratorTemplateProperty = DependencyProperty.Register("BandGeneratorTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).RefreshBandsSource()));
            BandGeneratorTemplateSelectorProperty = DependencyProperty.Register("BandGeneratorTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).RefreshBandsSource()));
            BandsItemsAttachedBehaviorProperty = DependencyProperty.Register("BandsItemsAttachedBehavior", typeof(ItemsAttachedBehaviorExtendedLock<BandBase, BandBase>), ownerType, new PropertyMetadata(null));
            BandsSourceProperty = DependencyProperty.Register("BandsSource", typeof(IEnumerable), ownerType, new PropertyMetadata((d, e) => ((BandBase) d).lockBandsSourceUpdate.DoLockedAction(delegate {
                Func<BandBase, IList> getTargetFunction = <>c.<>9__27_14;
                if (<>c.<>9__27_14 == null)
                {
                    Func<BandBase, IList> local1 = <>c.<>9__27_14;
                    getTargetFunction = <>c.<>9__27_14 = band => band.BandsCore;
                }
                ItemsAttachedBehaviorExtendedLock<BandBase, BandBase>.OnItemsSourceExtLockPropertyChanged(d, e, BandsItemsAttachedBehaviorProperty, ActualBandGeneratorTemplateProperty, ActualBandGeneratorTemplateSelectorProperty, ActualBandGeneratorStyleProperty, getTargetFunction, <>c.<>9__27_15 ??= band => band.CreateBand(), null, null, null, null, true, true, null, false);
            })));
            BandSerializationNameProperty = DependencyProperty.Register("BandSerializationName", typeof(string), ownerType, new PropertyMetadata(null));
            BandSeparatorWidthProperty = DependencyProperty.Register("BandSeparatorWidth", typeof(double?), ownerType, new PropertyMetadata(null, (d, e) => ((BandBase) d).OnBandSeparatorChangedCore()));
            BandCellSeparatorColorProperty = DependencyProperty.Register("BandCellSeparatorColor", typeof(Brush), ownerType, new PropertyMetadata(null, (d, e) => ((BaseColumn) d).OnBandSeparatorChanged()));
            BandHeaderSeparatorColorProperty = DependencyProperty.Register("BandHeaderSeparatorColor", typeof(Brush), ownerType, new PropertyMetadata(null, (d, e) => ((BaseColumn) d).OnBandSeparatorChanged()));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.FindCollectionItemEvent, (s, e) => ((BandBase) s).OnDeserializeFindCollectionItem(e));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.StartDeserializingEvent, (s, e) => ((BandBase) s).OnDeserializeStart());
            EventManager.RegisterClassHandler(ownerType, DXSerializer.EndDeserializingEvent, (s, e) => ((BandBase) s).OnDeserializeEnd());
            EventManager.RegisterClassHandler(ownerType, DXSerializer.ClearCollectionEvent, (s, e) => ((BandBase) s).OnDeserializeClearCollection(e));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.CreateCollectionItemEvent, (s, e) => ((BandBase) s).OnDeserializeCreateCollectionItem(e));
            EventManager.RegisterClassHandler(ownerType, DXSerializer.AllowPropertyEvent, new AllowPropertyEventHandler(BandBase.OnDeserializeAllowProperty));
            CloneDetailHelper.RegisterKnownAttachedProperty(GridRowProperty);
        }

        public BandBase()
        {
            this.bandsHelper = new BandsHelper(this, true);
            this.ColumnsCore.CollectionChanged += new NotifyCollectionChangedEventHandler(this.columns_CollectionChanged);
            this.BandsCore.CollectionChanged += new NotifyCollectionChangedEventHandler(this.bands_CollectionChanged);
            this.rowDefinitions = new ObservableCollection<BandRowDefinition>();
            this.columnDefinitions = new ObservableCollection<BandColumnDefinition>();
            this.ActualRows = new List<BandRow>();
            this.VisibleBands = new List<BandBase>();
            base.HasBottomElement = true;
        }

        private void bands_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((this.DataControl == null) || !this.DataControl.BandsSourceSyncLocker.IsLocked)
            {
                this.lockBandsSourceUpdate.DoIfNotLocked(() => this.SyncBandsColectionWithSource(e, false));
            }
        }

        protected internal override bool CanDropTo(BaseColumn target)
        {
            for (BandBase base2 = target.ParentBandInternal; base2 != null; base2 = base2.Owner as BandBase)
            {
                if (ReferenceEquals(base2, this))
                {
                    return false;
                }
            }
            return true;
        }

        private void columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Action action = delegate {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    foreach (ColumnBase base2 in this.ColumnsCore)
                    {
                        base2.ParentBand = this;
                    }
                }
                else
                {
                    if (e.OldItems != null)
                    {
                        foreach (ColumnBase base3 in e.OldItems)
                        {
                            base3.ParentBand = null;
                        }
                    }
                    if (e.NewItems != null)
                    {
                        foreach (ColumnBase base4 in e.NewItems)
                        {
                            base4.ParentBand = this;
                        }
                    }
                }
                ((IBandsOwner) this).OnColumnsChanged(e);
            };
            if (this.DataControl == null)
            {
                action();
            }
            else
            {
                this.DataControl.GetOriginationDataControl().syncPropertyLocker.DoLockedAction(action);
                Func<DataControlBase, BaseColumn> cloneAccessor = this.CreateCloneAccessor();
                Func<object, object> convertAction = <>c.<>9__173_2;
                if (<>c.<>9__173_2 == null)
                {
                    Func<object, object> local1 = <>c.<>9__173_2;
                    convertAction = <>c.<>9__173_2 = column => CloneDetailHelper.CloneElement<BaseColumn>((ColumnBase) column, (Action<BaseColumn>) null, (Func<BaseColumn, Locker>) null, (object[]) null);
                }
                this.DataControl.GetDataControlOriginationElement().NotifyCollectionChanged(this.DataControl, dc => ((BandBase) cloneAccessor(dc)).ColumnsCore, convertAction, e);
            }
            if ((this.DataControl == null) || !this.DataControl.BandsSourceSyncLocker.IsLocked)
            {
                this.lockColumnsSourceUpdate.DoIfNotLocked(() => this.SyncColumnsColectionWithSource(e));
            }
        }

        protected abstract BandBase CreateBand();
        internal abstract IBandsCollection CreateBands();
        internal override Func<DataControlBase, BaseColumn> CreateCloneAccessor() => 
            BandWalker.CreateBandCloneAccessor(this);

        protected abstract ColumnBase CreateColumn();
        internal abstract IBandColumnsCollection CreateColumns();
        IBandsOwner IBandsOwner.FindClone(DataControlBase dataControl) => 
            this.CreateCloneAccessor()(dataControl) as IBandsOwner;

        void IBandsOwner.OnBandsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.OnBandsChanged(e);
            }
        }

        void IBandsOwner.OnColumnsChanged(NotifyCollectionChangedEventArgs e)
        {
            if ((this.Owner != null) && ((this.DataControl == null) || !this.DataControl.lockBandsSourceUpdate.IsLocked))
            {
                this.Owner.OnColumnsChanged(e);
            }
        }

        void IBandsOwner.OnLayoutPropertyChanged()
        {
            this.OnLayoutPropertyChanged();
        }

        void ICollectionOwner.OnInsertItem(object item)
        {
            if ((base.View == null) || !ReferenceEquals(base.View.CheckBoxSelectorColumn, item))
            {
                base.AddLogicalChild(item);
            }
        }

        void ICollectionOwner.OnRemoveItem(object item)
        {
            if ((base.View == null) || !ReferenceEquals(base.View.CheckBoxSelectorColumn, item))
            {
                base.RemoveLogicalChild(item);
                BaseColumn column = item as BaseColumn;
                if (column != null)
                {
                    column.ParentBand = null;
                }
            }
        }

        private Style GetActualBandGeneratorStyle() => 
            (this.BandGeneratorStyle == null) ? ((base.ParentBand == null) ? ((this.DataControl == null) ? this.BandGeneratorStyle : this.DataControl.BandGeneratorStyle) : base.ParentBand.GetActualBandGeneratorStyle()) : this.BandGeneratorStyle;

        private DataTemplate GetActualBandGeneratorTemplate() => 
            (this.BandGeneratorTemplate == null) ? ((base.ParentBand == null) ? ((this.DataControl == null) ? this.BandGeneratorTemplate : this.DataControl.BandGeneratorTemplate) : base.ParentBand.GetActualBandGeneratorTemplate()) : this.BandGeneratorTemplate;

        private DataTemplateSelector GetActualBandGeneratorTemplateSelector() => 
            (this.BandGeneratorTemplateSelector == null) ? ((base.ParentBand == null) ? ((this.DataControl == null) ? this.BandGeneratorTemplateSelector : this.DataControl.BandGeneratorTemplateSelector) : base.ParentBand.GetActualBandGeneratorTemplateSelector()) : this.BandGeneratorTemplateSelector;

        private Style GetActualColumnGeneratorStyle() => 
            (this.ColumnGeneratorStyle == null) ? ((base.ParentBand == null) ? ((this.DataControl == null) ? this.ColumnGeneratorStyle : this.DataControl.ColumnGeneratorStyle) : base.ParentBand.GetActualColumnGeneratorStyle()) : this.ColumnGeneratorStyle;

        private DataTemplate GetActualColumnGeneratorTemplate() => 
            (this.ColumnGeneratorTemplate == null) ? ((base.ParentBand == null) ? ((this.DataControl == null) ? this.ColumnGeneratorTemplate : this.DataControl.ColumnGeneratorTemplate) : base.ParentBand.GetActualColumnGeneratorTemplate()) : this.ColumnGeneratorTemplate;

        private DataTemplateSelector GetActualColumnGeneratorTemplateSelector() => 
            (this.ColumnGeneratorTemplateSelector == null) ? ((base.ParentBand == null) ? ((this.DataControl == null) ? this.ColumnGeneratorTemplateSelector : this.DataControl.ColumnGeneratorTemplateSelector) : base.ParentBand.GetActualColumnGeneratorTemplateSelector()) : this.ColumnGeneratorTemplateSelector;

        protected internal override DataTemplate GetActualTemplate() => 
            ((base.HeaderTemplate != null) || (this.BandsLayout == null)) ? base.HeaderTemplate : this.BandsLayout.BandHeaderTemplate;

        protected internal override DataTemplateSelector GetActualTemplateSelector() => 
            ((base.HeaderTemplateSelector != null) || (this.BandsLayout == null)) ? base.HeaderTemplateSelector : this.BandsLayout.BandHeaderTemplateSelector;

        public static int GetGridColumn(DependencyObject obj) => 
            (int) obj.GetValue(GridColumnProperty);

        public static int GetGridRow(DependencyObject obj) => 
            (int) obj.GetValue(GridRowProperty);

        internal override DataControlBase GetNotifySourceControl() => 
            this.DataControl;

        private BandBase GetRightParentBand()
        {
            IBandsCollection bandsCore = null;
            if (base.ParentBand != null)
            {
                bandsCore = base.ParentBand.BandsCore;
            }
            else if (this.Owner != null)
            {
                bandsCore = this.Owner.BandsCore;
            }
            if ((bandsCore == null) || (bandsCore.Count == 0))
            {
                return null;
            }
            int index = bandsCore.IndexOf(this);
            if ((index < 0) || ((index + 1) == bandsCore.Count))
            {
                return null;
            }
            BandBase base2 = bandsCore[index + 1] as BandBase;
            return base2?.ParentBand;
        }

        private Brush GetSpecifiedSeparatorColor(bool header = false)
        {
            ITableView view = base.View as ITableView;
            if (view == null)
            {
                return null;
            }
            BandBase parentBand = base.ParentBand;
            if (header)
            {
                while (parentBand != null)
                {
                    if (parentBand.BandHeaderSeparatorColor != null)
                    {
                        return parentBand.BandHeaderSeparatorColor;
                    }
                    parentBand = parentBand.ParentBand;
                }
            }
            else
            {
                while (parentBand != null)
                {
                    if (parentBand.BandCellSeparatorColor != null)
                    {
                        return parentBand.BandCellSeparatorColor;
                    }
                    parentBand = parentBand.ParentBand;
                }
            }
            return (header ? view.BandHeaderSeparatorColor : view.BandCellSeparatorColor);
        }

        private double GetSpecifiedSeparatorWidth()
        {
            ITableView view = base.View as ITableView;
            if (view == null)
            {
                return 0.0;
            }
            for (BandBase base2 = base.ParentBand; base2 != null; base2 = base2.ParentBand)
            {
                if (base2.BandSeparatorWidth != null)
                {
                    return base2.BandSeparatorWidth.Value;
                }
            }
            return view.BandSeparatorWidth;
        }

        internal static Type GetTypeFromObject<T>(T obj) => 
            typeof(T);

        private bool HasRightColumnChildren()
        {
            if ((this.BandsCore.Count == 0) && (this.ColumnsCore.Count == 0))
            {
                return false;
            }
            if (this.BandsCore.Count == 0)
            {
                return (this.ColumnsCore.Count != 0);
            }
            Func<BandBase, bool> predicate = <>c.<>9__91_0;
            if (<>c.<>9__91_0 == null)
            {
                Func<BandBase, bool> local1 = <>c.<>9__91_0;
                predicate = <>c.<>9__91_0 = x => x.Visible;
            }
            IEnumerable<BandBase> source = this.BandsCore.Cast<BandBase>().Where<BandBase>(predicate);
            if ((source == null) || !source.Any<BandBase>())
            {
                return false;
            }
            Func<BandBase, int> keySelector = <>c.<>9__91_1;
            if (<>c.<>9__91_1 == null)
            {
                Func<BandBase, int> local2 = <>c.<>9__91_1;
                keySelector = <>c.<>9__91_1 = x => x.ActualVisibleIndex;
            }
            BandBase base2 = source.MaxBy<BandBase, int>(keySelector);
            return ((base2 != null) && base2.HasRightColumnChildren());
        }

        private bool IsSetLeftSeparator()
        {
            ColumnBase base2 = null;
            BandBase base3 = this;
            if ((base.View == null) || (base.View.VisibleColumnsCore.Count == 0))
            {
                return false;
            }
            while (true)
            {
                if (base3 != null)
                {
                    if ((base3.ColumnsCore.Count <= 0) || (base3.BandsCore.Count != 0))
                    {
                        base3 = (base3.BandsCore.Count > 0) ? (base3.BandsCore[0] as BandBase) : null;
                        continue;
                    }
                    base2 = base3.ColumnsCore[0] as ColumnBase;
                }
                if ((base2 != null) && base2.Visible)
                {
                    BandBase parentBand = base2.ParentBand;
                    if (parentBand == null)
                    {
                        return false;
                    }
                    for (int i = base2.ActualVisibleIndex; i > 0; i--)
                    {
                        ColumnBase column = base.View.VisibleColumnsCore[i - 1];
                        if (column == null)
                        {
                            return false;
                        }
                        BandBase objA = column.ParentBand;
                        if (objA == null)
                        {
                            return false;
                        }
                        if (!ReferenceEquals(objA, parentBand))
                        {
                            bool flag = false;
                            ITableView view = base.View as ITableView;
                            if (view != null)
                            {
                                flag = view.IsCheckBoxSelectorColumn(column);
                            }
                            return ((column.ActualBandRightSeparatorWidthCore == 0.0) && !flag);
                        }
                    }
                }
                return false;
            }
        }

        protected override void OnActualHeaderWidthChanged()
        {
            base.OnActualHeaderWidthChanged();
            base.UpdateContentLayout();
        }

        protected internal override bool OnBandSeparatorChanged()
        {
            bool flag3;
            ITableView view = base.View as ITableView;
            if ((!base.Visible || ((base.ActualVisibleIndex == -1) || ((view == null) || (base.View.VisibleColumnsCore.Count == 0)))) || ((view.UseLightweightTemplates != null) && (((UseLightweightTemplates) view.UseLightweightTemplates.Value) != UseLightweightTemplates.All)))
            {
                return base.OnBandSeparatorChanged();
            }
            double actualBandLeftSeparatorWidthCore = base.ActualBandLeftSeparatorWidthCore;
            double actualBandRightSeparatorWidthCore = base.ActualBandRightSeparatorWidthCore;
            bool flag = !this.HasRightColumnChildren();
            if (base.ParentBand != null)
            {
                List<BandBase> list1;
                if ((base.ParentBand.BandsCore == null) || (base.ParentBand.BandsCore.Count <= 0))
                {
                    list1 = null;
                }
                else
                {
                    Func<BandBase, bool> predicate = <>c.<>9__92_0;
                    if (<>c.<>9__92_0 == null)
                    {
                        Func<BandBase, bool> local2 = <>c.<>9__92_0;
                        predicate = <>c.<>9__92_0 = x => x.Visible;
                    }
                    list1 = base.ParentBand.BandsCore.Cast<BandBase>().Where<BandBase>(predicate).ToList<BandBase>();
                }
                List<BandBase> source = list1;
                base.ActualBandLeftSeparatorWidth = 0.0;
                flag3 = false;
                if (source != null)
                {
                    Func<BandBase, int> keySelector = <>c.<>9__92_1;
                    if (<>c.<>9__92_1 == null)
                    {
                        Func<BandBase, int> local3 = <>c.<>9__92_1;
                        keySelector = <>c.<>9__92_1 = x => x.ActualVisibleIndex;
                    }
                    if (this == source.MaxBy<BandBase, int>(keySelector))
                    {
                        double num1;
                        if (base.ParentBand.ActualBandRightSeparatorWidthCore > 0.0)
                        {
                            num1 = base.ParentBand.ActualBandRightSeparatorWidthCore;
                        }
                        else
                        {
                            double? bandSeparatorWidth = base.ParentBand.BandSeparatorWidth;
                            num1 = (bandSeparatorWidth != null) ? bandSeparatorWidth.GetValueOrDefault() : 0.0;
                        }
                        double val = num1;
                        this.ActualBandRightSeparatorWidth = flag ? 0.0 : base.ValidateActualSeparatorWidth(val, true);
                        if (source != null)
                        {
                            Func<BandBase, int> func4 = <>c.<>9__92_2;
                            if (<>c.<>9__92_2 == null)
                            {
                                Func<BandBase, int> local4 = <>c.<>9__92_2;
                                func4 = <>c.<>9__92_2 = x => x.ActualVisibleIndex;
                            }
                            if ((this == source.MinBy<BandBase, int>(func4)) && this.IsSetLeftSeparator())
                            {
                                base.ActualBandLeftSeparatorWidth = base.ValidateActualSeparatorWidth(this.GetSpecifiedSeparatorWidth(), false);
                            }
                        }
                        flag3 = true;
                        goto TR_0035;
                    }
                }
                this.ActualBandRightSeparatorWidth = flag ? 0.0 : base.ValidateActualSeparatorWidth(this.GetSpecifiedSeparatorWidth(), true);
                if (source != null)
                {
                    Func<BandBase, int> keySelector = <>c.<>9__92_3;
                    if (<>c.<>9__92_3 == null)
                    {
                        Func<BandBase, int> local5 = <>c.<>9__92_3;
                        keySelector = <>c.<>9__92_3 = x => x.ActualVisibleIndex;
                    }
                    if ((this == source.MinBy<BandBase, int>(keySelector)) && this.IsSetLeftSeparator())
                    {
                        base.ActualBandLeftSeparatorWidth = base.ValidateActualSeparatorWidth(this.GetSpecifiedSeparatorWidth(), false);
                    }
                }
            }
            else
            {
                this.ActualBandRightSeparatorWidth = flag ? 0.0 : base.ValidateActualSeparatorWidth(view.BandSeparatorWidth, true);
                this.ActualBandLeftSeparatorWidth = this.IsSetLeftSeparator() ? base.ValidateActualSeparatorWidth(this.GetSpecifiedSeparatorWidth(), false) : 0.0;
                base.ActualBandCellRightSeparatorColor = view.BandCellSeparatorColor;
                Brush bandHeaderSeparatorColor = view.BandHeaderSeparatorColor;
                Brush bandCellSeparatorColor = bandHeaderSeparatorColor;
                if (bandHeaderSeparatorColor == null)
                {
                    Brush local1 = bandHeaderSeparatorColor;
                    bandCellSeparatorColor = view.BandCellSeparatorColor;
                }
                this.ActualBandHeaderRightSeparatorColor = bandCellSeparatorColor;
                base.ActualBandCellLeftSeparatorColor = base.ActualBandCellRightSeparatorColorCore;
                base.ActualBandHeaderLeftSeparatorColor = base.ActualBandHeaderRightSeparatorColorCore;
                goto TR_001A;
            }
            goto TR_0035;
        TR_001A:
            if (base.ActualBandRightSeparatorWidthCore == 0.0)
            {
                BandBase rightParentBand = this.GetRightParentBand();
                if (rightParentBand != null)
                {
                    this.ActualBandRightSeparatorWidth = flag ? 0.0 : ((rightParentBand.BandSeparatorWidth != null) ? rightParentBand.BandSeparatorWidth.Value : 0.0);
                    base.ActualBandCellRightSeparatorColor = rightParentBand.ActualBandHeaderRightSeparatorColorCore;
                    base.ActualBandHeaderRightSeparatorColor = rightParentBand.ActualBandHeaderRightSeparatorColorCore;
                }
            }
            if (base.ActualBandCellLeftSeparatorColorCore == null)
            {
                base.ActualBandCellLeftSeparatorColor = this.BandCellSeparatorColor;
            }
            base.ActualBandHeaderLeftSeparatorColor ??= this.BandHeaderSeparatorColor;
            bool flag2 = false;
            foreach (BandBase base3 in this.BandsCore)
            {
                flag2 |= base3.OnBandSeparatorChanged();
            }
            foreach (BandRow row in this.ActualRows)
            {
                foreach (ColumnBase base4 in row.Columns)
                {
                    flag2 |= base4.OnBandSeparatorChanged();
                }
            }
            return (flag2 || ((actualBandLeftSeparatorWidthCore != base.ActualBandLeftSeparatorWidthCore) || !(actualBandRightSeparatorWidthCore == base.ActualBandRightSeparatorWidthCore)));
        TR_0035:
            if (!flag3)
            {
                base.ActualBandCellRightSeparatorColor = this.GetSpecifiedSeparatorColor(false);
                Brush specifiedSeparatorColor = this.GetSpecifiedSeparatorColor(true);
                Brush actualBandCellRightSeparatorColorCore = specifiedSeparatorColor;
                if (specifiedSeparatorColor == null)
                {
                    Brush local9 = specifiedSeparatorColor;
                    actualBandCellRightSeparatorColorCore = base.ActualBandCellRightSeparatorColorCore;
                }
                this.ActualBandHeaderRightSeparatorColor = actualBandCellRightSeparatorColorCore;
            }
            else
            {
                base.ActualBandCellRightSeparatorColor = base.ParentBand.ActualBandCellRightSeparatorColorCore;
                if ((base.ActualBandCellRightSeparatorColor == null) && (base.ParentBand.ActualBandRightSeparatorWidthCore == 0.0))
                {
                    base.ActualBandCellRightSeparatorColor = this.GetSpecifiedSeparatorColor(false);
                }
                Brush actualBandHeaderRightSeparatorColor = base.ParentBand.ActualBandHeaderRightSeparatorColor;
                Brush actualBandCellRightSeparatorColorCore = actualBandHeaderRightSeparatorColor;
                if (actualBandHeaderRightSeparatorColor == null)
                {
                    Brush local6 = actualBandHeaderRightSeparatorColor;
                    actualBandCellRightSeparatorColorCore = base.ParentBand.ActualBandCellRightSeparatorColorCore;
                }
                this.ActualBandHeaderRightSeparatorColor = actualBandCellRightSeparatorColorCore;
                if ((base.ActualBandHeaderRightSeparatorColor == null) && (base.ParentBand.ActualBandRightSeparatorWidthCore == 0.0))
                {
                    Brush specifiedSeparatorColor = this.GetSpecifiedSeparatorColor(true);
                    Brush actualBandCellRightSeparatorColorCore = specifiedSeparatorColor;
                    if (specifiedSeparatorColor == null)
                    {
                        Brush local7 = specifiedSeparatorColor;
                        Brush actualBandHeaderRightSeparatorColorCore = base.ParentBand.ActualBandHeaderRightSeparatorColorCore;
                        actualBandCellRightSeparatorColorCore = actualBandHeaderRightSeparatorColorCore;
                        if (actualBandHeaderRightSeparatorColorCore == null)
                        {
                            Brush local8 = actualBandHeaderRightSeparatorColorCore;
                            actualBandCellRightSeparatorColorCore = base.ActualBandCellRightSeparatorColorCore;
                        }
                    }
                    this.ActualBandHeaderRightSeparatorColor = actualBandCellRightSeparatorColorCore;
                }
            }
            base.ActualBandCellLeftSeparatorColor = base.ParentBand.ActualBandCellLeftSeparatorColorCore;
            Brush actualBandHeaderLeftSeparatorColor = base.ParentBand.ActualBandHeaderLeftSeparatorColor;
            Brush actualBandCellLeftSeparatorColorCore = actualBandHeaderLeftSeparatorColor;
            if (actualBandHeaderLeftSeparatorColor == null)
            {
                Brush local10 = actualBandHeaderLeftSeparatorColor;
                actualBandCellLeftSeparatorColorCore = base.ActualBandCellLeftSeparatorColorCore;
            }
            this.ActualBandHeaderLeftSeparatorColor = actualBandCellLeftSeparatorColorCore;
            goto TR_001A;
        }

        private static void OnBandsItemsGeneratorTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsAttachedBehaviorCore<BandBase, BandBase>.OnItemsGeneratorTemplatePropertyChanged(d, e, BandsItemsAttachedBehaviorProperty);
        }

        internal void OnBandsLayoutChanged()
        {
            base.UpdateActualHeaderTemplateSelector();
            this.UpdateActualHeaderToolTipTemplate();
            this.UpdateActualPrintBandHeaderStyle();
            this.UpdateActualHeaderImageStyle();
        }

        private static void OnColumnsItemsGeneratorTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsAttachedBehaviorCore<BandBase, ColumnBase>.OnItemsGeneratorTemplatePropertyChanged(d, e, ColumnsItemsAttachedBehaviorProperty);
        }

        protected virtual bool OnDeserializeAllowProperty(AllowPropertyEventArgs e) => 
            (this.DataControl != null) ? this.DataControl.OnDeserializeAllowProperty(e) : false;

        private static void OnDeserializeAllowProperty(object sender, AllowPropertyEventArgs e)
        {
            ((BandBase) sender).OnDeserializeAllowPropertyInternal(e);
        }

        private void OnDeserializeAllowPropertyInternal(AllowPropertyEventArgs e)
        {
            e.Allow = this.OnDeserializeAllowProperty(e);
        }

        private void OnDeserializeClearCollection(XtraItemRoutedEventArgs e)
        {
            string name = e.Item.Name;
            if (((name == "Columns") || (name == "Bands")) && !this.BandSerializationHelper.CanAddNewColumns)
            {
                this.BandSerializationHelper.ClearCollection(e);
            }
        }

        private void OnDeserializeCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
        {
            string collectionName = e.CollectionName;
            if (collectionName == "Columns")
            {
                if (!this.BandSerializationHelper.CanRemoveOldColumns)
                {
                    this.DataControl.OnDeserializeCreateColumn(e);
                }
            }
            else if ((collectionName == "Bands") && !this.BandSerializationHelper.CanRemoveOldColumns)
            {
                this.DataControl.OnDeserializeCreateBand(e);
            }
        }

        private void OnDeserializeEnd()
        {
            this.ColumnsCore.EndUpdate();
            this.BandsCore.EndUpdate();
        }

        private void OnDeserializeFindCollectionItem(XtraFindCollectionItemEventArgs e)
        {
            string collectionName = e.CollectionName;
            if (collectionName == "Columns")
            {
                this.BandSerializationHelper.FindColumn(e);
            }
            else if (collectionName == "Bands")
            {
                this.BandSerializationHelper.FindBand(e, this);
            }
        }

        private void OnDeserializeStart()
        {
            this.ColumnsCore.BeginUpdate();
            this.BandsCore.BeginUpdate();
        }

        private static void OnGridColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnGridRowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColumnBase base2 = d as ColumnBase;
            if ((base2 != null) && ((base2.OwnerControl != null) && (!base2.OwnerControl.ColumnsCore.IsLockUpdate && (base2.OwnerControl.BandsLayoutCore != null))))
            {
                base2.OwnerControl.BandsLayoutCore.UpdateBandsLayout();
            }
        }

        protected override void OnLayoutPropertyChanged()
        {
            if (this.Owner != null)
            {
                this.Owner.OnLayoutPropertyChanged();
            }
        }

        private void OnShowInBandsPanelChanged()
        {
            foreach (ColumnBase base2 in this.ColumnsCore)
            {
                base2.UpdateHasTopElement();
            }
            base.UpdateContentLayout();
        }

        private void OwnerChanged()
        {
            this.lockColumnsSourceUpdate.DoLockedAction(delegate {
                if ((this.Owner == null) || (this.Owner.DataControl == null))
                {
                    this.RefreshSource();
                }
                else if (!this.Owner.DataControl.BandsSourceChangedLocker.IsLocked)
                {
                    this.DataControl.BandsSourceSyncLocker.DoIfNotLocked(() => this.RefreshSource());
                }
            });
        }

        internal void RefreshBandsSource()
        {
            this.ActualBandGeneratorTemplateSelector = this.GetActualBandGeneratorTemplateSelector();
            this.ActualBandGeneratorTemplate = this.GetActualBandGeneratorTemplate();
            this.ActualBandGeneratorStyle = this.GetActualBandGeneratorStyle();
            foreach (object obj2 in this.BandsCore)
            {
                ((BandBase) obj2).RefreshBandsSource();
            }
        }

        internal void RefreshColumnsSource()
        {
            this.ActualColumnGeneratorTemplateSelector = this.GetActualColumnGeneratorTemplateSelector();
            this.ActualColumnGeneratorTemplate = this.GetActualColumnGeneratorTemplate();
            this.ActualColumnGeneratorStyle = this.GetActualColumnGeneratorStyle();
            foreach (object obj2 in this.BandsCore)
            {
                ((BandBase) obj2).RefreshColumnsSource();
            }
        }

        internal void RefreshSource()
        {
            this.RefreshBandsSource();
            this.RefreshColumnsSource();
        }

        internal void ResetBand()
        {
            if ((this.DataControl != null) && !this.DataControl.BandsSourceSyncLocker.IsLocked)
            {
                ((IBandsOwner) this).OnColumnsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public static void SetGridColumn(DependencyObject obj, int value)
        {
            obj.SetValue(GridColumnProperty, value);
        }

        public static void SetGridRow(DependencyObject obj, int value)
        {
            obj.SetValue(GridRowProperty, value);
        }

        internal void SyncBandsColectionWithSource(bool full = false)
        {
            this.SyncBandsColectionWithSource(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset), full);
            if (full)
            {
                foreach (object obj2 in this.bandsCore)
                {
                    BandBase base2 = obj2 as BandBase;
                    if (base2 != null)
                    {
                        base2.SyncBandsColectionWithSource(full);
                    }
                }
                this.SyncColumnsColectionWithSource();
            }
        }

        private void SyncBandsColectionWithSource(NotifyCollectionChangedEventArgs e, bool full = false)
        {
            if (this.Owner != null)
            {
                ItemsAttachedBehaviorExtendedLock<BandBase, BandBase> @lock = (ItemsAttachedBehaviorExtendedLock<BandBase, BandBase>) base.GetValue(BandsItemsAttachedBehaviorProperty);
                if ((@lock != null) && ((this.BandsSource is IList) && (full || (((IList) this.BandsSource).Count != this.bandsCore.Count))))
                {
                    @lock.SetLockSynchronization(true);
                    Func<object, object> convertItemAction = <>c.<>9__168_0;
                    if (<>c.<>9__168_0 == null)
                    {
                        Func<object, object> local1 = <>c.<>9__168_0;
                        convertItemAction = <>c.<>9__168_0 = source => DependencyObjectExtensions.GetDataContext((DependencyObject) source);
                    }
                    SyncCollectionHelper.SyncCollection(e, (IList) this.BandsSource, this.bandsCore, convertItemAction, null, null, null);
                    @lock.SetLockSynchronization(false);
                }
            }
        }

        private void SyncColumnsColectionWithSource()
        {
            this.SyncColumnsColectionWithSource(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void SyncColumnsColectionWithSource(NotifyCollectionChangedEventArgs e)
        {
            if (this.Owner != null)
            {
                ItemsAttachedBehaviorExtendedLock<BandBase, ColumnBase> @lock = (ItemsAttachedBehaviorExtendedLock<BandBase, ColumnBase>) base.GetValue(ColumnsItemsAttachedBehaviorProperty);
                if ((@lock != null) && ((this.ColumnsSource is IList) && (((IList) this.ColumnsSource).Count != this.columnsCore.Count)))
                {
                    @lock.SetLockSynchronization(true);
                    Func<object, object> convertItemAction = <>c.<>9__176_0;
                    if (<>c.<>9__176_0 == null)
                    {
                        Func<object, object> local1 = <>c.<>9__176_0;
                        convertItemAction = <>c.<>9__176_0 = source => DependencyObjectExtensions.GetDataContext((DependencyObject) source);
                    }
                    SyncCollectionHelper.SyncCollection(e, (IList) this.ColumnsSource, this.columnsCore, convertItemAction, null, null, null);
                    @lock.SetLockSynchronization(false);
                }
            }
        }

        internal override void UpdateActualHeaderImageStyle()
        {
            base.UpdateActualHeaderImageStyle();
            if (base.ActualHeaderImageStyle == null)
            {
                Func<BandsLayoutBase, Style> evaluator = <>c.<>9__213_0;
                if (<>c.<>9__213_0 == null)
                {
                    Func<BandsLayoutBase, Style> local1 = <>c.<>9__213_0;
                    evaluator = <>c.<>9__213_0 = x => x.HeaderImageStyle;
                }
                this.ActualHeaderImageStyle = this.BandsLayout.With<BandsLayoutBase, Style>(evaluator);
            }
        }

        protected internal override void UpdateActualHeaderToolTipTemplate()
        {
            if (base.HeaderToolTipTemplate != null)
            {
                base.ActualHeaderToolTipTemplate = base.HeaderToolTipTemplate;
            }
            else if (this.BandsLayout != null)
            {
                base.ActualHeaderToolTipTemplate = this.BandsLayout.BandHeaderToolTipTemplate;
            }
        }

        internal void UpdateActualPrintBandHeaderStyle()
        {
            if (this.PrintBandHeaderStyle != null)
            {
                this.ActualPrintBandHeaderStyle = this.PrintBandHeaderStyle;
            }
            else if (this.BandsLayout != null)
            {
                this.ActualPrintBandHeaderStyle = this.BandsLayout.PrintBandHeaderStyle;
            }
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public IEnumerable ColumnsSource
        {
            get => 
                (IEnumerable) base.GetValue(ColumnsSourceProperty);
            set => 
                base.SetValue(ColumnsSourceProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public DataTemplateSelector ColumnGeneratorTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ColumnGeneratorTemplateSelectorProperty);
            set => 
                base.SetValue(ColumnGeneratorTemplateSelectorProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public DataTemplate ColumnGeneratorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ColumnGeneratorTemplateProperty);
            set => 
                base.SetValue(ColumnGeneratorTemplateProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public Style ColumnGeneratorStyle
        {
            get => 
                (Style) base.GetValue(ColumnGeneratorStyleProperty);
            set => 
                base.SetValue(ColumnGeneratorStyleProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        private DataTemplateSelector ActualColumnGeneratorTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ActualColumnGeneratorTemplateSelectorProperty);
            set => 
                base.SetValue(ActualColumnGeneratorTemplateSelectorProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        private DataTemplate ActualColumnGeneratorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualColumnGeneratorTemplateProperty);
            set => 
                base.SetValue(ActualColumnGeneratorTemplateProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        private Style ActualColumnGeneratorStyle
        {
            get => 
                (Style) base.GetValue(ActualColumnGeneratorStyleProperty);
            set => 
                base.SetValue(ActualColumnGeneratorStyleProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public IEnumerable BandsSource
        {
            get => 
                (IEnumerable) base.GetValue(BandsSourceProperty);
            set => 
                base.SetValue(BandsSourceProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public DataTemplateSelector BandGeneratorTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(BandGeneratorTemplateSelectorProperty);
            set => 
                base.SetValue(BandGeneratorTemplateSelectorProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public DataTemplate BandGeneratorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(BandGeneratorTemplateProperty);
            set => 
                base.SetValue(BandGeneratorTemplateProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        public Style BandGeneratorStyle
        {
            get => 
                (Style) base.GetValue(BandGeneratorStyleProperty);
            set => 
                base.SetValue(BandGeneratorStyleProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        private DataTemplateSelector ActualBandGeneratorTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ActualBandGeneratorTemplateSelectorProperty);
            set => 
                base.SetValue(ActualBandGeneratorTemplateSelectorProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        private DataTemplate ActualBandGeneratorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualBandGeneratorTemplateProperty);
            set => 
                base.SetValue(ActualBandGeneratorTemplateProperty, value);
        }

        [CloneDetailMode(CloneDetailMode.Skip)]
        private Style ActualBandGeneratorStyle
        {
            get => 
                (Style) base.GetValue(ActualBandGeneratorStyleProperty);
            set => 
                base.SetValue(ActualBandGeneratorStyleProperty, value);
        }

        public double? BandSeparatorWidth
        {
            get => 
                (double?) base.GetValue(BandSeparatorWidthProperty);
            set => 
                base.SetValue(BandSeparatorWidthProperty, value);
        }

        public Brush BandCellSeparatorColor
        {
            get => 
                (Brush) base.GetValue(BandCellSeparatorColorProperty);
            set => 
                base.SetValue(BandCellSeparatorColorProperty, value);
        }

        public Brush BandHeaderSeparatorColor
        {
            get => 
                (Brush) base.GetValue(BandHeaderSeparatorColorProperty);
            set => 
                base.SetValue(BandHeaderSeparatorColorProperty, value);
        }

        [Description("Gets or sets the style applied to band headers when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style PrintBandHeaderStyle
        {
            get => 
                (Style) base.GetValue(PrintBandHeaderStyleProperty);
            set => 
                base.SetValue(PrintBandHeaderStyleProperty, value);
        }

        [Description("Gets the actual style applied to band headers when the grid is printed. This is a dependency property."), Category("Appearance Print")]
        public Style ActualPrintBandHeaderStyle
        {
            get => 
                (Style) base.GetValue(ActualPrintBandHeaderStyleProperty);
            private set => 
                base.SetValue(ActualPrintBandHeaderStylePropertyKey, value);
        }

        [Description("Get or sets the value that indicates whether the band children overlay the band header. This is a dependency property."), Category("Layout"), XtraSerializableProperty, GridSerializeAlwaysProperty]
        public bool OverlayHeaderByChildren
        {
            get => 
                (bool) base.GetValue(OverlayHeaderByChildrenProperty);
            set => 
                base.SetValue(OverlayHeaderByChildrenProperty, value);
        }

        [Description("Gets or sets the band name used when bands are serialized. This is a dependency property."), Category("Layout"), XtraSerializableProperty, GridSerializeAlwaysProperty]
        public string BandSerializationName
        {
            get => 
                (string) base.GetValue(BandSerializationNameProperty);
            set => 
                base.SetValue(BandSerializationNameProperty, value);
        }

        internal bool ActualShowInBandsPanel =>
            !this.OverlayHeaderByChildren || (((this.ActualRows.Count == 0) && (this.VisibleBands.Count == 0)) || ((this.BandsLayout != null) && !this.BandsLayout.ShowBandsPanel));

        internal IBandsOwner Owner
        {
            get => 
                this.ownerCore;
            set
            {
                bool flag = true;
                if (value != null)
                {
                    flag = !ReferenceEquals(this.ownerCore, value);
                }
                else if ((this.ownerCore != null) && (this.ownerCore.DataControl != null))
                {
                    flag = !this.ownerCore.DataControl.BandsSourceSyncLocker.IsLocked;
                }
                this.ownerCore = value;
                base.ParentBand = value as BandBase;
                if (flag)
                {
                    this.OwnerChanged();
                }
            }
        }

        DataControlBase IBandsOwner.DataControl =>
            this.Owner?.DataControl;

        internal List<BandBase> VisibleBands { get; private set; }

        internal IEnumerable<BandBase> PrintableBands
        {
            get
            {
                Func<BandBase, bool> predicate = <>c.<>9__131_0;
                if (<>c.<>9__131_0 == null)
                {
                    Func<BandBase, bool> local1 = <>c.<>9__131_0;
                    predicate = <>c.<>9__131_0 = band => band.AllowPrinting;
                }
                return this.VisibleBands.Where<BandBase>(predicate);
            }
        }

        List<BandBase> IBandsOwner.VisibleBands =>
            this.VisibleBands;

        internal BandsLayoutBase BandsLayout
        {
            get => 
                this.bandsLayout;
            set
            {
                if (!ReferenceEquals(this.bandsLayout, value))
                {
                    this.bandsLayout = value;
                    this.OnBandsLayoutChanged();
                }
            }
        }

        internal override BandBase ParentBandInternal =>
            this;

        internal IBandsCollection BandsCore
        {
            get
            {
                this.bandsCore ??= this.CreateBands();
                return this.bandsCore;
            }
        }

        IBandsCollection IBandsOwner.BandsCore =>
            this.BandsCore;

        internal IBandColumnsCollection ColumnsCore
        {
            get
            {
                this.columnsCore ??= this.CreateColumns();
                return this.columnsCore;
            }
        }

        internal List<BandRow> ActualRows { get; private set; }

        internal ObservableCollection<BandRowDefinition> RowDefinitions =>
            this.rowDefinitions;

        internal ObservableCollection<BandColumnDefinition> ColumnDefinitions =>
            this.columnDefinitions;

        protected internal int Level
        {
            get
            {
                int num = 0;
                for (BandBase base2 = this; base2.ParentBand != null; base2 = base2.ParentBand)
                {
                    num++;
                }
                return num;
            }
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { this.BandsCore.GetEnumerator(), this.ColumnsCore.GetEnumerator() };
                return new MergedEnumerator(args);
            }
        }

        private BandedViewSerializationHelper BandSerializationHelper =>
            this.DataControl.BandSerializationHelper;

        protected internal DataControlBase DataControl =>
            this.Owner?.DataControl;

        protected internal override IColumnOwnerBase ResizeOwner =>
            this.DataControl?.DataView;

        protected internal override bool IsBand =>
            true;

        protected override bool OwnerAllowResizing =>
            (this.BandsLayout != null) ? this.BandsLayout.AllowBandResizing : true;

        protected override bool OwnerAllowMoving =>
            (this.BandsLayout != null) ? this.BandsLayout.AllowBandMoving : true;

        protected internal override bool CanStartDragSingleColumn =>
            !ReferenceEquals(this.Owner, this.DataControl.BandsLayoutCore) || ((base.Fixed != FixedStyle.None) || (this.DataControl.BandsLayoutCore.FixedNoneVisibleBands.Count > 1));

        protected internal override bool AllowChangeParent =>
            this.BandsLayout.AllowChangeBandParent;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandBase.<>c <>9 = new BandBase.<>c();
            public static Func<BandBase, IList> <>9__27_7;
            public static Func<BandBase, ColumnBase> <>9__27_8;
            public static Func<BandBase, IList> <>9__27_14;
            public static Func<BandBase, BandBase> <>9__27_15;
            public static Func<BandBase, bool> <>9__91_0;
            public static Func<BandBase, int> <>9__91_1;
            public static Func<BandBase, bool> <>9__92_0;
            public static Func<BandBase, int> <>9__92_1;
            public static Func<BandBase, int> <>9__92_2;
            public static Func<BandBase, int> <>9__92_3;
            public static Func<BandBase, bool> <>9__131_0;
            public static Func<object, object> <>9__168_0;
            public static Func<object, object> <>9__173_2;
            public static Func<object, object> <>9__176_0;
            public static Func<BandsLayoutBase, Style> <>9__213_0;

            internal void <.cctor>b__27_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).UpdateActualPrintBandHeaderStyle();
            }

            internal void <.cctor>b__27_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).OnShowInBandsPanelChanged();
            }

            internal void <.cctor>b__27_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).RefreshBandsSource();
            }

            internal void <.cctor>b__27_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).RefreshBandsSource();
            }

            internal void <.cctor>b__27_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).lockBandsSourceUpdate.DoLockedAction(delegate {
                    Func<BandBase, IList> getTargetFunction = <>9__27_14;
                    if (<>9__27_14 == null)
                    {
                        Func<BandBase, IList> local1 = <>9__27_14;
                        getTargetFunction = <>9__27_14 = band => band.BandsCore;
                    }
                    ItemsAttachedBehaviorExtendedLock<BandBase, BandBase>.OnItemsSourceExtLockPropertyChanged(d, e, BandBase.BandsItemsAttachedBehaviorProperty, BandBase.ActualBandGeneratorTemplateProperty, BandBase.ActualBandGeneratorTemplateSelectorProperty, BandBase.ActualBandGeneratorStyleProperty, getTargetFunction, <>9__27_15 ??= band => band.CreateBand(), null, null, null, null, true, true, null, false);
                });
            }

            internal IList <.cctor>b__27_14(BandBase band) => 
                band.BandsCore;

            internal BandBase <.cctor>b__27_15(BandBase band) => 
                band.CreateBand();

            internal void <.cctor>b__27_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).OnBandSeparatorChangedCore();
            }

            internal void <.cctor>b__27_17(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseColumn) d).OnBandSeparatorChanged();
            }

            internal void <.cctor>b__27_18(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseColumn) d).OnBandSeparatorChanged();
            }

            internal void <.cctor>b__27_19(object s, XtraFindCollectionItemEventArgs e)
            {
                ((BandBase) s).OnDeserializeFindCollectionItem(e);
            }

            internal void <.cctor>b__27_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).RefreshColumnsSource();
            }

            internal void <.cctor>b__27_20(object s, StartDeserializingEventArgs e)
            {
                ((BandBase) s).OnDeserializeStart();
            }

            internal void <.cctor>b__27_21(object s, EndDeserializingEventArgs e)
            {
                ((BandBase) s).OnDeserializeEnd();
            }

            internal void <.cctor>b__27_22(object s, XtraItemRoutedEventArgs e)
            {
                ((BandBase) s).OnDeserializeClearCollection(e);
            }

            internal void <.cctor>b__27_23(object s, XtraCreateCollectionItemEventArgs e)
            {
                ((BandBase) s).OnDeserializeCreateCollectionItem(e);
            }

            internal void <.cctor>b__27_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).RefreshColumnsSource();
            }

            internal void <.cctor>b__27_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).RefreshColumnsSource();
            }

            internal void <.cctor>b__27_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).lockColumnsSourceUpdate.DoLockedAction(delegate {
                    Func<BandBase, IList> getTargetFunction = <>9__27_7;
                    if (<>9__27_7 == null)
                    {
                        Func<BandBase, IList> local1 = <>9__27_7;
                        getTargetFunction = <>9__27_7 = band => band.ColumnsCore;
                    }
                    ItemsAttachedBehaviorExtendedLock<BandBase, ColumnBase>.OnItemsSourceExtLockPropertyChanged(d, e, BandBase.ColumnsItemsAttachedBehaviorProperty, BandBase.ActualColumnGeneratorTemplateProperty, BandBase.ActualColumnGeneratorTemplateSelectorProperty, BandBase.ActualColumnGeneratorStyleProperty, getTargetFunction, <>9__27_8 ??= band => band.CreateColumn(), null, null, null, null, true, true, null, false);
                });
            }

            internal IList <.cctor>b__27_7(BandBase band) => 
                band.ColumnsCore;

            internal ColumnBase <.cctor>b__27_8(BandBase band) => 
                band.CreateColumn();

            internal void <.cctor>b__27_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandBase) d).RefreshBandsSource();
            }

            internal object <columns_CollectionChanged>b__173_2(object column) => 
                CloneDetailHelper.CloneElement<BaseColumn>((ColumnBase) column, (Action<BaseColumn>) null, (Func<BaseColumn, Locker>) null, (object[]) null);

            internal bool <get_PrintableBands>b__131_0(BandBase band) => 
                band.AllowPrinting;

            internal bool <HasRightColumnChildren>b__91_0(BandBase x) => 
                x.Visible;

            internal int <HasRightColumnChildren>b__91_1(BandBase x) => 
                x.ActualVisibleIndex;

            internal bool <OnBandSeparatorChanged>b__92_0(BandBase x) => 
                x.Visible;

            internal int <OnBandSeparatorChanged>b__92_1(BandBase x) => 
                x.ActualVisibleIndex;

            internal int <OnBandSeparatorChanged>b__92_2(BandBase x) => 
                x.ActualVisibleIndex;

            internal int <OnBandSeparatorChanged>b__92_3(BandBase x) => 
                x.ActualVisibleIndex;

            internal object <SyncBandsColectionWithSource>b__168_0(object source) => 
                DependencyObjectExtensions.GetDataContext((DependencyObject) source);

            internal object <SyncColumnsColectionWithSource>b__176_0(object source) => 
                DependencyObjectExtensions.GetDataContext((DependencyObject) source);

            internal Style <UpdateActualHeaderImageStyle>b__213_0(BandsLayoutBase x) => 
                x.HeaderImageStyle;
        }
    }
}

