namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Settings;
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
    using System.Windows.Markup;

    [ContentProperty("Bands")]
    public abstract class BandsLayoutBase : DXFrameworkContentElement, IBandsOwner
    {
        public static readonly DependencyProperty ShowBandsPanelProperty;
        public static readonly DependencyProperty BandHeaderTemplateProperty;
        public static readonly DependencyProperty BandHeaderTemplateSelectorProperty;
        public static readonly DependencyProperty BandHeaderToolTipTemplateProperty;
        public static readonly DependencyProperty AllowChangeColumnParentProperty;
        public static readonly DependencyProperty AllowChangeBandParentProperty;
        public static readonly DependencyProperty ShowBandsInCustomizationFormProperty;
        public static readonly DependencyProperty AllowBandMovingProperty;
        public static readonly DependencyProperty AllowBandResizingProperty;
        private static readonly DependencyPropertyKey ColumnChooserBandsPropertyKey;
        public static readonly DependencyProperty ColumnChooserBandsProperty;
        public static readonly DependencyProperty ColumnChooserBandsSortOrderComparerProperty;
        public static readonly DependencyProperty AllowAdvancedVerticalNavigationProperty;
        public static readonly DependencyProperty AllowAdvancedHorizontalNavigationProperty;
        public static readonly DependencyProperty PrintBandHeaderStyleProperty;
        private static readonly DependencyPropertyKey FixedLeftVisibleBandsPropertyKey;
        public static readonly DependencyProperty FixedLeftVisibleBandsProperty;
        private static readonly DependencyPropertyKey FixedRightVisibleBandsPropertyKey;
        public static readonly DependencyProperty FixedRightVisibleBandsProperty;
        private static readonly DependencyPropertyKey FixedNoneVisibleBandsPropertyKey;
        public static readonly DependencyProperty FixedNoneVisibleBandsProperty;
        private static readonly DependencyPropertyKey ShowIndicatorPropertyKey;
        public static readonly DependencyProperty ShowIndicatorProperty;
        public static readonly DependencyProperty AllowBandMultiRowProperty;
        private Style headerImageStyleCore;
        private BandsHelper bandsHelper;
        private DataControlBase dataControl;
        private bool useLegacyColumnVisibleIndexes;
        private RebuildBandsLayoutHelperBase rebuildBandsLayoutHelper;
        private CollectionModificationListener<ColumnBase, ColumnBase> columnsPlainSyncListener;
        private CollectionModificationListener<BandBase, BandBase> bandsPlainSyncListener;

        static BandsLayoutBase()
        {
            Type ownerType = typeof(BandsLayoutBase);
            ShowBandsPanelProperty = DependencyProperty.Register("ShowBandsPanel", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((BandsLayoutBase) d).OnShowBandsPanelChanged()));
            BandHeaderTemplateProperty = DependencyProperty.Register("BandHeaderTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).UpdateBandActualHeaderTemplateSelector()));
            BandHeaderTemplateSelectorProperty = DependencyProperty.Register("BandHeaderTemplateSelector", typeof(DataTemplateSelector), ownerType, new PropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).UpdateBandActualHeaderTemplateSelector()));
            BandHeaderToolTipTemplateProperty = DependencyProperty.Register("BandHeaderToolTipTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).UpdateBandHeaderToolTipTemplate()));
            AllowChangeColumnParentProperty = DependencyProperty.Register("AllowChangeColumnParent", typeof(bool), ownerType, new PropertyMetadata(false));
            AllowChangeBandParentProperty = DependencyProperty.Register("AllowChangeBandParent", typeof(bool), ownerType, new PropertyMetadata(false));
            ShowBandsInCustomizationFormProperty = DependencyProperty.Register("ShowBandsInCustomizationForm", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((BandsLayoutBase) d).OnShowBandsInCustomizationFormChanged()));
            AllowBandMovingProperty = DependencyProperty.Register("AllowBandMoving", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((BandsLayoutBase) d).UpdateViewInfo()));
            AllowBandResizingProperty = DependencyProperty.Register("AllowBandResizing", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((BandsLayoutBase) d).UpdateViewInfo()));
            ColumnChooserBandsPropertyKey = DependencyPropertyManager.RegisterReadOnly("ColumnChooserBands", typeof(ReadOnlyCollection<BandBase>), ownerType, new FrameworkPropertyMetadata(null));
            ColumnChooserBandsProperty = ColumnChooserBandsPropertyKey.DependencyProperty;
            ColumnChooserBandsSortOrderComparerProperty = DependencyProperty.Register("ColumnChooserBandsSortOrderComparer", typeof(IComparer<BandBase>), ownerType, new PropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).RebuildColumnChooserColumns()));
            AllowAdvancedVerticalNavigationProperty = DependencyProperty.Register("AllowAdvancedVerticalNavigation", typeof(bool), ownerType, new PropertyMetadata(true));
            AllowAdvancedHorizontalNavigationProperty = DependencyProperty.Register("AllowAdvancedHorizontalNavigation", typeof(bool), ownerType, new PropertyMetadata(true));
            PrintBandHeaderStyleProperty = DependencyProperty.Register("PrintBandHeaderStyle", typeof(Style), ownerType, new PropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).UpdatePrintBandHeaderStyle()));
            FixedLeftVisibleBandsPropertyKey = DependencyPropertyManager.RegisterReadOnly("FixedLeftVisibleBands", typeof(ReadOnlyCollection<BandBase>), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).OnFixedLeftVisibleBandsChanged()));
            FixedLeftVisibleBandsProperty = FixedLeftVisibleBandsPropertyKey.DependencyProperty;
            FixedRightVisibleBandsPropertyKey = DependencyPropertyManager.RegisterReadOnly("FixedRightVisibleBands", typeof(ReadOnlyCollection<BandBase>), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).OnFixedRightVisibleBandsChanged()));
            FixedRightVisibleBandsProperty = FixedRightVisibleBandsPropertyKey.DependencyProperty;
            FixedNoneVisibleBandsPropertyKey = DependencyPropertyManager.RegisterReadOnly("FixedNoneVisibleBands", typeof(ReadOnlyCollection<BandBase>), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BandsLayoutBase) d).OnFixedNoneVisibleBandsChanged()));
            FixedNoneVisibleBandsProperty = FixedNoneVisibleBandsPropertyKey.DependencyProperty;
            ShowIndicatorPropertyKey = DependencyProperty.RegisterReadOnly("ShowIndicator", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowIndicatorProperty = ShowIndicatorPropertyKey.DependencyProperty;
            AllowBandMultiRowProperty = DependencyProperty.Register("AllowBandMultiRow", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((BandsLayoutBase) d).UpdateBandsLayout()));
        }

        public BandsLayoutBase()
        {
            Func<ColumnBase, ColumnBase> mapInsertion = <>c.<>9__96_0;
            if (<>c.<>9__96_0 == null)
            {
                Func<ColumnBase, ColumnBase> local1 = <>c.<>9__96_0;
                mapInsertion = <>c.<>9__96_0 = x => x;
            }
            this.columnsPlainSyncListener = new CollectionModificationListener<ColumnBase, ColumnBase>(mapInsertion);
            Func<BandBase, BandBase> func2 = <>c.<>9__96_1;
            if (<>c.<>9__96_1 == null)
            {
                Func<BandBase, BandBase> local2 = <>c.<>9__96_1;
                func2 = <>c.<>9__96_1 = x => x;
            }
            this.bandsPlainSyncListener = new CollectionModificationListener<BandBase, BandBase>(func2);
            this.VisibleBands = new List<BandBase>();
            this.ColumnChooserBands = new ReadOnlyCollection<BandBase>(new BandBase[0]);
            this.FixedLeftVisibleBands = new ReadOnlyCollection<BandBase>(new BandBase[0]);
            this.FixedRightVisibleBands = new ReadOnlyCollection<BandBase>(new BandBase[0]);
            this.FixedNoneVisibleBands = new ReadOnlyCollection<BandBase>(new BandBase[0]);
        }

        private void AddColumnsToGrid(IList source)
        {
            if ((this.DataControl != null) && (source != null))
            {
                foreach (ColumnBase base2 in source)
                {
                    if (this.ShouldAddColumnToDataControl(base2))
                    {
                        this.DataControl.ColumnsCore.Add(base2);
                    }
                }
            }
        }

        internal void ApplyColumnVisibleIndex(BaseColumn baseColumn, int oldVisibleIndex)
        {
            if (!this.View.IsLockUpdateColumnsLayout && (baseColumn != null))
            {
                this.DoMoveAction(() => this.RebuildBandsLayoutHelper.ApplyColumnVisibleIndex(baseColumn, oldVisibleIndex));
            }
        }

        private void CloneActualRows(BandBase source, BandBase clones)
        {
            foreach (BandRow row in source.ActualRows)
            {
                BandRow row1 = new BandRow();
                row1.Columns = new List<ColumnBase>();
                BandRow item = row1;
                foreach (ColumnBase base2 in row.Columns)
                {
                    if (base2.AllowPrinting)
                    {
                        item.Columns.Add(base2);
                    }
                }
                clones.ActualRows.Add(item);
            }
        }

        internal virtual BandsLayoutBase CloneAndFillEmptyBands()
        {
            BandsLayoutBase base2 = (BandsLayoutBase) Activator.CreateInstance(base.GetType());
            base2.VisibleBands = this.ClonePrintBands();
            return base2;
        }

        internal static BandBase CloneBand(BandBase source)
        {
            BandBase destination = (BandBase) CloneDetailHelper.CloneElement<BaseColumn>(source, (Action<BaseColumn>) null, (Func<BaseColumn, Locker>) null, (object[]) null);
            CloneInnerBandsCollection(source, destination, new Action<IList, IList>(CloneDetailHelper.CloneCollection<BaseColumn>));
            return destination;
        }

        internal void CloneBandsCollection(IBandsCollection destination)
        {
            CloneBandsCollection(this.BandsCore, destination, new Action<IList, IList>(CloneDetailHelper.CloneCollection<BaseColumn>));
        }

        private static void CloneBandsCollection(IBandsCollection source, IBandsCollection destination, Action<IList, IList> cloneAction)
        {
            if (source.Count != 0)
            {
                cloneAction(source, destination);
                for (int i = 0; i < source.Count; i++)
                {
                    CloneInnerBandsCollection((BandBase) source[i], (BandBase) destination[i], cloneAction);
                }
            }
        }

        private static void CloneInnerBandsCollection(BandBase source, BandBase destination, Action<IList, IList> cloneAction)
        {
            CloneBandsCollection(source.BandsCore, destination.BandsCore, cloneAction);
            cloneAction(source.ColumnsCore, destination.ColumnsCore);
        }

        private BandBase ClonePrintBand(BandBase source)
        {
            BandBase clones = Activator.CreateInstance(source.GetType()) as BandBase;
            clones.ActualHeaderWidth = source.ActualHeaderWidth;
            if (source.PrintableBands.Any<BandBase>())
            {
                this.ClonePrintBands(source.PrintableBands, clones.VisibleBands);
            }
            else if (this.HasColumns(source))
            {
                this.CloneActualRows(source, clones);
            }
            else
            {
                this.CreateFakeColumn(source, clones);
            }
            return clones;
        }

        private List<BandBase> ClonePrintBands()
        {
            List<BandBase> cloneBands = new List<BandBase>();
            this.ClonePrintBands(this.PrintableBands, cloneBands);
            return cloneBands;
        }

        private void ClonePrintBands(IEnumerable<BandBase> sourceBands, IList<BandBase> cloneBands)
        {
            foreach (BandBase base2 in sourceBands)
            {
                cloneBands.Add(this.ClonePrintBand(base2));
            }
        }

        internal void CopyBandCollection(IBandsCollection destination)
        {
            CloneBandsCollection(this.BandsCore, destination, new Action<IList, IList>(CloneDetailHelper.CopyToCollection<BaseColumn>));
        }

        private void CreateFakeColumn(BandBase sourceBand, BandBase cloneBand)
        {
            BandRow row1 = new BandRow();
            row1.Columns = new List<ColumnBase>();
            BandRow item = row1;
            ColumnBase column = this.DataControl.CreateColumn();
            column.EditSettings = new TextEditSettings();
            column.HasLeftSibling = sourceBand.HasLeftSibling;
            column.HasRightSibling = sourceBand.HasRightSibling;
            column.UpdateActualPrintProperties(this.View);
            this.SetPrintWidth(column, sourceBand.ActualHeaderWidth);
            item.Columns.Add(column);
            cloneBand.ActualRows.Add(item);
        }

        IBandsOwner IBandsOwner.FindClone(DataControlBase dataControl) => 
            dataControl.BandsLayoutCore;

        void IBandsOwner.OnBandsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.bandsPlainSyncListener.IsDirty = true;
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (BandBase base2 in e.OldItems)
                    {
                        this.OnBandRemoved(base2);
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (BandBase base3 in e.NewItems)
                    {
                        this.OnBandAdded(base3);
                    }
                }
            }
            this.SyncWithPlainBandCollection(false);
        }

        void IBandsOwner.OnColumnsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.columnsPlainSyncListener.IsDirty = true;
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (ColumnBase base2 in e.OldItems)
                    {
                        this.columnsPlainSyncListener.Remove(base2);
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (ColumnBase base3 in e.NewItems)
                    {
                        this.columnsPlainSyncListener.Add(base3);
                    }
                }
            }
            this.SyncWithPlainColumnCollection(false);
        }

        void IBandsOwner.OnLayoutPropertyChanged()
        {
            this.UpdateBandsLayout();
        }

        internal void DoMoveAction(Action action)
        {
            try
            {
                Action<DataViewBase> updateMethod = <>c.<>9__156_0;
                if (<>c.<>9__156_0 == null)
                {
                    Action<DataViewBase> local1 = <>c.<>9__156_0;
                    updateMethod = <>c.<>9__156_0 = view => view.BeginUpdateColumnsLayout();
                }
                this.View.UpdateAllDependentViews(updateMethod);
                action();
            }
            finally
            {
                Action<DataViewBase> updateMethod = <>c.<>9__156_1;
                if (<>c.<>9__156_1 == null)
                {
                    Action<DataViewBase> local2 = <>c.<>9__156_1;
                    updateMethod = <>c.<>9__156_1 = view => view.EndUpdateColumnsLayout();
                }
                this.View.UpdateAllDependentViews(updateMethod);
            }
            this.View.UpdateContentLayout();
        }

        internal void FillColumns()
        {
            this.columnsPlainSyncListener.Reset();
            this.DataControl.ColumnsCore.BeginUpdate();
            this.DataControl.ColumnsCore.Clear();
            this.ForeachBand(delegate (BandBase band) {
                band.BandsLayout = this;
                bool flag = band.GetValue(BandBase.ColumnsSourceProperty) != null;
                foreach (ColumnBase base2 in band.ColumnsCore)
                {
                    if (!base2.IsServiceColumn())
                    {
                        this.DataControl.ColumnsCore.Add(base2);
                        if (flag)
                        {
                            DependencyObjectExtensions.SetDataContext(base2, base2.DataContext);
                        }
                    }
                }
            });
            this.DataControl.ColumnsCore.EndUpdate();
        }

        private void ForceSyncPlainCollections()
        {
            this.SyncWithPlainBandCollection(true);
            this.SyncWithPlainColumnCollection(true);
        }

        internal virtual void ForeachBand(Action<BandBase> action)
        {
            foreach (BandBase base2 in new BandIterator<BandBase>(this.BandsCore))
            {
                action(base2);
            }
        }

        internal void ForeachVisibleBand(Action<BandBase> action)
        {
            this.ForeachVisibleBand(this, action);
        }

        private void ForeachVisibleBand(IBandsOwner owner, Action<BandBase> action)
        {
            ForeachVisibleBand(owner.VisibleBands, action);
        }

        internal static void ForeachVisibleBand(IEnumerable bands, Action<BandBase> action)
        {
            foreach (BandBase base2 in bands)
            {
                action(base2);
                ForeachVisibleBand(base2.VisibleBands, action);
            }
        }

        internal IList GetBands(BandBase band, bool isLeft, bool skipNotFixedBands = false) => 
            this.GetBands(band, new List<BandBase>(), isLeft, skipNotFixedBands);

        private IList GetBands(BandBase band, IList bands, bool isLeft, bool skipNotFixedBands)
        {
            if (DesignerProperties.GetIsInDesignMode(this) && (band.Owner == null))
            {
                return bands;
            }
            int index = band.Owner.VisibleBands.IndexOf(band);
            for (int i = 0; i < band.Owner.VisibleBands.Count; i++)
            {
                if (!this.SkipItem(index, i, isLeft) && (!skipNotFixedBands || this.IsFixedBand(band.Owner.VisibleBands[i])))
                {
                    bands.Add(band.Owner.VisibleBands[i]);
                }
            }
            return (!(band.Owner is BandBase) ? bands : this.GetBands((BandBase) band.Owner, bands, isLeft, skipNotFixedBands));
        }

        internal FrameworkElement GetBandsContainerControl()
        {
            if (this.DataControl != null)
            {
                if (ReferenceEquals(this.DataControl, this.DataControl.GetRootDataControl()))
                {
                    return this.DataControl.viewCore.RootBandsContainer;
                }
                RowDataBase headersRowData = this.DataControl.DataControlParent.GetHeadersRowData();
                if ((headersRowData != null) && (headersRowData.WholeRowElement != null))
                {
                    return LayoutHelper.FindElementByName(headersRowData.WholeRowElement, "PART_BandsContainer");
                }
            }
            return null;
        }

        private ColumnPosition GetColumnPosition(bool isLeft) => 
            (!isLeft || this.GetShowIndicator()) ? ColumnPosition.Middle : ColumnPosition.Left;

        internal BandBase GetRootBand(BandBase band)
        {
            BandBase owner = band.Owner as BandBase;
            return ((owner == null) ? band : this.GetRootBand(owner));
        }

        private bool GetShowIndicator()
        {
            if (this.DataControl == null)
            {
                return false;
            }
            ITableView view = this.View as ITableView;
            return ((view != null) ? view.ShowIndicator : false);
        }

        internal List<ColumnBase> GetVisibleColumns()
        {
            List<ColumnBase> visibleColumns = new List<ColumnBase>();
            this.ForeachVisibleBand(delegate (BandBase band) {
                foreach (BandRow row in band.ActualRows)
                {
                    foreach (ColumnBase base2 in row.Columns)
                    {
                        visibleColumns.Add(base2);
                    }
                }
            });
            return visibleColumns;
        }

        private bool HasColumns(BandBase band)
        {
            Func<BandRow, IEnumerable<ColumnBase>> selector = <>c.<>9__177_0;
            if (<>c.<>9__177_0 == null)
            {
                Func<BandRow, IEnumerable<ColumnBase>> local1 = <>c.<>9__177_0;
                selector = <>c.<>9__177_0 = row => row.Columns;
            }
            Func<ColumnBase, bool> predicate = <>c.<>9__177_1;
            if (<>c.<>9__177_1 == null)
            {
                Func<ColumnBase, bool> local2 = <>c.<>9__177_1;
                predicate = <>c.<>9__177_1 = column => column.AllowPrinting;
            }
            return band.ActualRows.SelectMany<BandRow, ColumnBase>(selector).Any<ColumnBase>(predicate);
        }

        private bool IsFixedBand(BandBase band) => 
            !ReferenceEquals(band.Owner, this) || (band.Fixed != FixedStyle.None);

        private void Move(BaseColumn source, BaseColumn target, BandedViewDropPlace dropPlace, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            this.DataControl.GetOriginationDataControl().syncPropertyLocker.DoLockedAction(delegate {
                Func<DataControlBase, BaseColumn> getCloneSource = source.CreateCloneAccessor();
                Func<DataControlBase, BaseColumn> getCloneTarget = target.CreateCloneAccessor();
                Func<DataControlBase, DataControlBase> getTarget = <>c.<>9__158_1;
                if (<>c.<>9__158_1 == null)
                {
                    Func<DataControlBase, DataControlBase> local1 = <>c.<>9__158_1;
                    getTarget = <>c.<>9__158_1 = dc => dc;
                }
                Action<DataControlBase> targetInClosedDetailHandler = <>c.<>9__158_3;
                if (<>c.<>9__158_3 == null)
                {
                    Action<DataControlBase> local2 = <>c.<>9__158_3;
                    targetInClosedDetailHandler = <>c.<>9__158_3 = dc => dc.BandsCore.Clear();
                }
                DataControlOriginationElementHelper.EnumerateDependentElementsIncludingSource<DataControlBase>(this.DataControl, getTarget, delegate (DataControlBase dc) {
                    BandsMover mover = new BandsMover(dc);
                    this.Move(mover, getCloneSource(dc), getCloneTarget(dc), dropPlace, moveFrom, useLegacyColumnVisibleIndexes);
                }, targetInClosedDetailHandler);
            });
        }

        private void Move(BandsMover mover, BaseColumn source, BaseColumn target, BandedViewDropPlace dropPlace, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            BandBase base2 = source as BandBase;
            BandBase base3 = target as BandBase;
            if ((base2 != null) && (base3 != null))
            {
                mover.MoveBand(base2, base3, dropPlace, moveFrom, useLegacyColumnVisibleIndexes);
            }
            else
            {
                ColumnBase base4 = source as ColumnBase;
                if ((base4 != null) && (base3 != null))
                {
                    mover.MoveColumnToBand(base4, base3, moveFrom, useLegacyColumnVisibleIndexes);
                }
                else
                {
                    ColumnBase base5 = target as ColumnBase;
                    if ((base4 != null) && (base5 != null))
                    {
                        mover.MoveColumnToColumn(base4, base5, dropPlace, moveFrom, useLegacyColumnVisibleIndexes);
                    }
                }
            }
        }

        protected internal virtual void MoveBandTo(BandBase source, BandBase target, BandedViewDropPlace dropPlace, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            this.Move(source, target, dropPlace, moveFrom, useLegacyColumnVisibleIndexes);
        }

        protected internal virtual void MoveColumnTo(BaseColumn source, BaseColumn target, BandedViewDropPlace dropPlace, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            this.Move(source, target, dropPlace, moveFrom, useLegacyColumnVisibleIndexes);
        }

        private void OnBandAdded(BandBase band)
        {
            this.bandsPlainSyncListener.Add(band);
            foreach (BandBase base2 in band.BandsCore)
            {
                this.OnBandAdded(base2);
            }
        }

        private void OnBandRemoved(BandBase band)
        {
            this.bandsPlainSyncListener.Remove(band);
            foreach (BandBase base2 in band.BandsCore)
            {
                this.OnBandRemoved(base2);
            }
        }

        private void OnFixedLeftVisibleBandsChanged()
        {
            Action<DataViewBase> action = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Action<DataViewBase> local1 = <>c.<>9__27_0;
                action = <>c.<>9__27_0 = x => x.ViewBehavior.NotifyFixedLeftBandsChanged();
            }
            this.View.Do<DataViewBase>(action);
        }

        private void OnFixedNoneVisibleBandsChanged()
        {
            Action<DataViewBase> action = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Action<DataViewBase> local1 = <>c.<>9__25_0;
                action = <>c.<>9__25_0 = x => x.ViewBehavior.NotifyFixedNoneBandsChanged();
            }
            this.View.Do<DataViewBase>(action);
        }

        private void OnFixedRightVisibleBandsChanged()
        {
            Action<DataViewBase> action = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Action<DataViewBase> local1 = <>c.<>9__26_0;
                action = <>c.<>9__26_0 = x => x.ViewBehavior.NotifyFixedRightBandsChanged();
            }
            this.View.Do<DataViewBase>(action);
        }

        internal void OnGridControlBandsChanged(NotifyCollectionChangedEventArgs e)
        {
            this.bandsHelper.OnBandsChanged(e);
        }

        private void OnHeaderImageStyleChanged()
        {
            Action<BandBase> action = <>c.<>9__93_0;
            if (<>c.<>9__93_0 == null)
            {
                Action<BandBase> local1 = <>c.<>9__93_0;
                action = <>c.<>9__93_0 = b => b.UpdateActualHeaderImageStyle();
            }
            this.ForeachBand(action);
        }

        private void OnShowBandsInCustomizationFormChanged()
        {
            this.DataControl.UpdateViewActualColumnChooserTemplate();
        }

        private void OnShowBandsPanelChanged()
        {
            this.UpdateBandsContainer(this.GetBandsContainerControl());
        }

        internal virtual void PatchBands(List<BandBase> bands, bool hasFixedLeftBands)
        {
        }

        internal IList<ColumnBase> RebuildBandsVisibleColumns()
        {
            DataViewBase view = this.View;
            Action<DataViewBase> action = <>c.<>9__121_0;
            if (<>c.<>9__121_0 == null)
            {
                Action<DataViewBase> local1 = <>c.<>9__121_0;
                action = <>c.<>9__121_0 = delegate (DataViewBase x) {
                    x.BeginUpdateColumnsLayout();
                };
            }
            view.Do<DataViewBase>(action);
            this.ForceSyncPlainCollections();
            Action<DataViewBase> action2 = <>c.<>9__121_1;
            if (<>c.<>9__121_1 == null)
            {
                Action<DataViewBase> local2 = <>c.<>9__121_1;
                action2 = <>c.<>9__121_1 = delegate (DataViewBase x) {
                    x.EndUpdateColumnsLayout(false);
                };
            }
            view.Do<DataViewBase>(action2);
            Action<DataViewBase> action3 = <>c.<>9__121_2;
            if (<>c.<>9__121_2 == null)
            {
                Action<DataViewBase> local3 = <>c.<>9__121_2;
                action3 = <>c.<>9__121_2 = delegate (DataViewBase x) {
                    x.UpdateVisibleIndexesLocker.Lock();
                };
            }
            view.Do<DataViewBase>(action3);
            IList<ColumnBase> visibleColumns = this.RebuildBandsLayoutHelper.RebuildVisibleColumns();
            LayoutAssigner layoutAssigner = new LayoutAssigner();
            this.UpdateFixedBands(layoutAssigner);
            this.UpdateVisibleColumnsAndBands(visibleColumns, layoutAssigner);
            Action<DataViewBase> action4 = <>c.<>9__121_3;
            if (<>c.<>9__121_3 == null)
            {
                Action<DataViewBase> local4 = <>c.<>9__121_3;
                action4 = <>c.<>9__121_3 = delegate (DataViewBase x) {
                    x.UpdateVisibleIndexesLocker.Unlock();
                };
            }
            view.Do<DataViewBase>(action4);
            return visibleColumns;
        }

        internal void RebuildColumnChooserColumns()
        {
            List<ColumnBase> columnChooserColumns = new List<ColumnBase>();
            List<BandBase> columnChooserBands = new List<BandBase>();
            this.ForeachBand(delegate (BandBase band) {
                if (!band.Visible)
                {
                    columnChooserBands.Add(band);
                }
                foreach (ColumnBase base2 in band.ColumnsCore)
                {
                    if (!base2.Visible && base2.ShowInColumnChooser)
                    {
                        columnChooserColumns.Add(base2);
                    }
                }
            });
            columnChooserColumns.Sort(this.View.ColumnChooserColumnsSortOrderComparer);
            if (this.ColumnChooserBandsSortOrderComparer != null)
            {
                columnChooserBands.Sort(this.ColumnChooserBandsSortOrderComparer);
            }
            if (!ListHelper.AreEqual<ColumnBase>(this.View.ColumnChooserColumns, columnChooserColumns))
            {
                this.View.ColumnChooserColumns = new ReadOnlyCollection<ColumnBase>(columnChooserColumns);
            }
            if (!ListHelper.AreEqual<BandBase>(this.ColumnChooserBands, columnChooserBands))
            {
                this.ColumnChooserBands = new ReadOnlyCollection<BandBase>(columnChooserBands);
            }
        }

        private void RemoveColumnsFromGrid(IList source)
        {
            if ((this.DataControl != null) && (source != null))
            {
                foreach (ColumnBase base2 in source)
                {
                    if (this.DataControl.ColumnsCore.Contains(base2))
                    {
                        this.DataControl.ColumnsCore.Remove(base2);
                    }
                }
            }
        }

        protected virtual void SetPrintWidth(BaseColumn column, double width)
        {
        }

        private bool ShouldAddColumnToDataControl(ColumnBase column) => 
            !this.DataControl.ColumnsCore.Contains(column) && !column.IsServiceColumn();

        [Browsable(false)]
        public bool ShouldSerializeColumnChooserBands(XamlDesignerSerializationManager manager) => 
            false;

        [Browsable(false)]
        public bool ShouldSerializeColumnChooserBandsSortOrderComparer(XamlDesignerSerializationManager manager) => 
            false;

        [Browsable(false)]
        public bool ShouldSerializeFixedLeftVisibleBands(XamlDesignerSerializationManager manager) => 
            false;

        [Browsable(false)]
        public bool ShouldSerializeFixedNoneVisibleBands(XamlDesignerSerializationManager manager) => 
            false;

        [Browsable(false)]
        public bool ShouldSerializeFixedRightVisibleBands(XamlDesignerSerializationManager manager) => 
            false;

        internal bool SkipItem(int itemIndex, int currentIndex, bool isLeft) => 
            (itemIndex != -1) ? (!isLeft ? (currentIndex <= itemIndex) : (currentIndex >= itemIndex)) : false;

        private void SyncWithPlainBandCollection(bool force = false)
        {
            if (force || this.AllowSyncWithPlainColumnCollection)
            {
                ReadOnlyCollection<BandBase> removedItems = null;
                ReadOnlyCollection<BandBase> addedItems = null;
                if (!this.bandsPlainSyncListener.IsDirty)
                {
                    removedItems = this.bandsPlainSyncListener.RemovedItems;
                    addedItems = this.bandsPlainSyncListener.AddedItems;
                }
                else
                {
                    removedItems = new ReadOnlyCollection<BandBase>(new BandBase[0]);
                    List<BandBase> allBands = new List<BandBase>();
                    this.ForeachBand(b => allBands.Add(b));
                    addedItems = new ReadOnlyCollection<BandBase>(allBands);
                }
                this.bandsPlainSyncListener.Reset();
                if ((addedItems.Count != 0) || (removedItems.Count != 0))
                {
                    Func<DataControlBase, IColumnCollection> evaluator = <>c.<>9__143_1;
                    if (<>c.<>9__143_1 == null)
                    {
                        Func<DataControlBase, IColumnCollection> local1 = <>c.<>9__143_1;
                        evaluator = <>c.<>9__143_1 = x => x.ColumnsCore;
                    }
                    IColumnCollection input = this.DataControl.With<DataControlBase, IColumnCollection>(evaluator);
                    Action<IColumnCollection> action = <>c.<>9__143_2;
                    if (<>c.<>9__143_2 == null)
                    {
                        Action<IColumnCollection> local2 = <>c.<>9__143_2;
                        action = <>c.<>9__143_2 = x => x.BeginUpdate();
                    }
                    input.Do<IColumnCollection>(action);
                    foreach (BandBase base2 in addedItems)
                    {
                        base2.BandsLayout = this;
                        this.AddColumnsToGrid(base2.ColumnsCore);
                    }
                    foreach (BandBase base3 in removedItems)
                    {
                        base3.BandsLayout = null;
                        this.RemoveColumnsFromGrid(base3.ColumnsCore);
                    }
                    Action<IColumnCollection> action2 = <>c.<>9__143_3;
                    if (<>c.<>9__143_3 == null)
                    {
                        Action<IColumnCollection> local3 = <>c.<>9__143_3;
                        action2 = <>c.<>9__143_3 = x => x.EndUpdate();
                    }
                    input.Do<IColumnCollection>(action2);
                }
                this.UpdateBandsLayout();
            }
        }

        private void SyncWithPlainColumnCollection(bool force = false)
        {
            if (force || this.AllowSyncWithPlainColumnCollection)
            {
                if (this.columnsPlainSyncListener.IsDirty)
                {
                    this.FillColumns();
                }
                else
                {
                    ReadOnlyCollection<ColumnBase> removedItems = this.columnsPlainSyncListener.RemovedItems;
                    ReadOnlyCollection<ColumnBase> addedItems = this.columnsPlainSyncListener.AddedItems;
                    this.columnsPlainSyncListener.Reset();
                    if ((addedItems.Count != 0) || (removedItems.Count != 0))
                    {
                        this.RemoveColumnsFromGrid(removedItems);
                        this.AddColumnsToGrid(addedItems);
                    }
                }
            }
        }

        private void UpdateBandActualHeaderTemplateSelector()
        {
            Action<BandBase> action = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Action<BandBase> local1 = <>c.<>9__28_0;
                action = <>c.<>9__28_0 = band => band.UpdateActualHeaderTemplateSelector();
            }
            this.ForeachBand(action);
        }

        private void UpdateBandHeaderToolTipTemplate()
        {
            Action<BandBase> action = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Action<BandBase> local1 = <>c.<>9__29_0;
                action = <>c.<>9__29_0 = band => band.UpdateActualHeaderToolTipTemplate();
            }
            this.ForeachBand(action);
        }

        private bool UpdateBandPosition(IList<BandBase> bands, bool hasTopElement, bool ownerIsLeft, bool ownerIsRight, bool emptyLeftSibling, LayoutAssigner layoutAssigner)
        {
            for (int i = 0; i < bands.Count; i++)
            {
                BandBase band = bands[i];
                bool isLeft = (i == 0) & ownerIsLeft;
                bool flag2 = (i == (bands.Count - 1)) & ownerIsRight;
                band.ColumnPosition = this.GetColumnPosition(isLeft);
                band.HasTopElement = hasTopElement;
                band.HasRightSibling = !flag2;
                band.HasLeftSibling = !isLeft;
                if (band.ActualRows.Count > 0)
                {
                    this.UpdateColumnsPositions(band, isLeft, flag2, emptyLeftSibling, layoutAssigner);
                    emptyLeftSibling = false;
                }
                else if (band.VisibleBands.Count == 0)
                {
                    emptyLeftSibling = true;
                }
                emptyLeftSibling = this.UpdateBandPosition(band.VisibleBands, true, isLeft, flag2, emptyLeftSibling, layoutAssigner);
            }
            return emptyLeftSibling;
        }

        internal void UpdateBandsContainer(FrameworkElement bandsContainer)
        {
            if (bandsContainer != null)
            {
                bandsContainer.Visibility = this.ShowBandsPanel ? Visibility.Visible : Visibility.Collapsed;
                if (this.DataControl != null)
                {
                    foreach (ColumnBase base2 in this.DataControl.ColumnsCore)
                    {
                        base2.RaiseHasTopElementChanged();
                    }
                }
            }
        }

        internal void UpdateBandsLayout()
        {
            if ((this.View != null) && !this.View.IsLockUpdateColumnsLayout)
            {
                this.View.RebuildColumns();
                this.View.UpdateContentLayout();
            }
        }

        internal void UpdateBandsPositions(LayoutAssigner layoutAssigner)
        {
            this.UpdateBandPosition(this.FixedLeftVisibleBands, false, true, true, false, layoutAssigner);
            this.UpdateBandPosition(this.FixedNoneVisibleBands, false, this.FixedLeftVisibleBands.Count == 0, true, false, layoutAssigner);
            this.UpdateBandPosition(this.FixedRightVisibleBands, false, false, true, false, layoutAssigner);
        }

        private void UpdateColumnsPositions(BandBase band, bool ownerIsLeft, bool ownerIsRight, bool hasEmptyLeftSibling, LayoutAssigner layoutAssigner)
        {
            foreach (BandRow row in band.ActualRows)
            {
                for (int i = 0; i < row.Columns.Count; i++)
                {
                    ColumnBase column = row.Columns[i];
                    layoutAssigner.SetColumnPosition(column, (!hasEmptyLeftSibling || (i != 0)) ? this.GetColumnPosition((i == 0) & ownerIsLeft) : ColumnPosition.Left);
                    layoutAssigner.SetHasRightSibling(column, !((i == (row.Columns.Count - 1)) & ownerIsRight));
                    layoutAssigner.SetHasLeftSibling(column, !((i == 0) & ownerIsLeft));
                }
            }
        }

        internal void UpdateFixedBands(LayoutAssigner layoutAssigner)
        {
            List<BandBase> list = new List<BandBase>();
            List<BandBase> list2 = new List<BandBase>();
            List<BandBase> list3 = new List<BandBase>();
            foreach (BandBase base2 in this.VisibleBands)
            {
                if (base2.Visible)
                {
                    if (base2.Fixed == FixedStyle.Left)
                    {
                        list.Add(base2);
                        continue;
                    }
                    if (base2.Fixed == FixedStyle.Right)
                    {
                        list2.Add(base2);
                        continue;
                    }
                    list3.Add(base2);
                }
            }
            if (!ListHelper.AreEqual<BandBase>(this.FixedLeftVisibleBands, list))
            {
                this.FixedLeftVisibleBands = new ReadOnlyCollection<BandBase>(list);
            }
            if (!ListHelper.AreEqual<BandBase>(this.FixedRightVisibleBands, list2))
            {
                this.FixedRightVisibleBands = new ReadOnlyCollection<BandBase>(list2);
            }
            if (!ListHelper.AreEqual<BandBase>(this.FixedNoneVisibleBands, list3))
            {
                this.FixedNoneVisibleBands = new ReadOnlyCollection<BandBase>(list3);
            }
        }

        private void UpdatePrintBandHeaderStyle()
        {
            Action<BandBase> action = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Action<BandBase> local1 = <>c.<>9__30_0;
                action = <>c.<>9__30_0 = band => band.UpdateActualPrintBandHeaderStyle();
            }
            this.ForeachBand(action);
        }

        internal void UpdateShowIndicator(bool showIndicator)
        {
            this.ShowIndicator = ReferenceEquals(this.DataControl.GetRootDataControl(), this.DataControl) & showIndicator;
        }

        private void UpdateViewInfo()
        {
            if (this.View != null)
            {
                this.View.UpdateColumnsViewInfo(false);
            }
        }

        internal void UpdateVisibleColumnsAndBands(IList<ColumnBase> visibleColumns, LayoutAssigner layoutAssigner = null)
        {
            LayoutAssigner assigner1 = layoutAssigner;
            if (layoutAssigner == null)
            {
                LayoutAssigner local1 = layoutAssigner;
                assigner1 = new LayoutAssigner();
            }
            this.UpdateBandsPositions(assigner1);
            this.RebuildBandsLayoutHelper.UpdateVisibleColumnsPositions(visibleColumns);
        }

        public DataTemplate BandHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(BandHeaderTemplateProperty);
            set => 
                base.SetValue(BandHeaderTemplateProperty, value);
        }

        public DataTemplateSelector BandHeaderTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(BandHeaderTemplateSelectorProperty);
            set => 
                base.SetValue(BandHeaderTemplateSelectorProperty, value);
        }

        public DataTemplate BandHeaderToolTipTemplate
        {
            get => 
                (DataTemplate) base.GetValue(BandHeaderToolTipTemplateProperty);
            set => 
                base.SetValue(BandHeaderToolTipTemplateProperty, value);
        }

        public bool ShowBandsPanel
        {
            get => 
                (bool) base.GetValue(ShowBandsPanelProperty);
            set => 
                base.SetValue(ShowBandsPanelProperty, value);
        }

        public bool AllowChangeColumnParent
        {
            get => 
                (bool) base.GetValue(AllowChangeColumnParentProperty);
            set => 
                base.SetValue(AllowChangeColumnParentProperty, value);
        }

        public bool AllowChangeBandParent
        {
            get => 
                (bool) base.GetValue(AllowChangeBandParentProperty);
            set => 
                base.SetValue(AllowChangeBandParentProperty, value);
        }

        public bool ShowBandsInCustomizationForm
        {
            get => 
                (bool) base.GetValue(ShowBandsInCustomizationFormProperty);
            set => 
                base.SetValue(ShowBandsInCustomizationFormProperty, value);
        }

        public bool AllowBandMoving
        {
            get => 
                (bool) base.GetValue(AllowBandMovingProperty);
            set => 
                base.SetValue(AllowBandMovingProperty, value);
        }

        public bool AllowBandResizing
        {
            get => 
                (bool) base.GetValue(AllowBandResizingProperty);
            set => 
                base.SetValue(AllowBandResizingProperty, value);
        }

        public bool AllowBandMultiRow
        {
            get => 
                (bool) base.GetValue(AllowBandMultiRowProperty);
            set => 
                base.SetValue(AllowBandMultiRowProperty, value);
        }

        [Browsable(false)]
        public ReadOnlyCollection<BandBase> ColumnChooserBands
        {
            get => 
                (ReadOnlyCollection<BandBase>) base.GetValue(ColumnChooserBandsProperty);
            protected set => 
                base.SetValue(ColumnChooserBandsPropertyKey, value);
        }

        [Browsable(false), CloneDetailMode(CloneDetailMode.Skip)]
        public IComparer<BandBase> ColumnChooserBandsSortOrderComparer
        {
            get => 
                (IComparer<BandBase>) base.GetValue(ColumnChooserBandsSortOrderComparerProperty);
            set => 
                base.SetValue(ColumnChooserBandsSortOrderComparerProperty, value);
        }

        public bool AllowAdvancedVerticalNavigation
        {
            get => 
                (bool) base.GetValue(AllowAdvancedVerticalNavigationProperty);
            set => 
                base.SetValue(AllowAdvancedVerticalNavigationProperty, value);
        }

        public bool AllowAdvancedHorizontalNavigation
        {
            get => 
                (bool) base.GetValue(AllowAdvancedHorizontalNavigationProperty);
            set => 
                base.SetValue(AllowAdvancedHorizontalNavigationProperty, value);
        }

        public Style PrintBandHeaderStyle
        {
            get => 
                (Style) base.GetValue(PrintBandHeaderStyleProperty);
            set => 
                base.SetValue(PrintBandHeaderStyleProperty, value);
        }

        [Browsable(false)]
        public ReadOnlyCollection<BandBase> FixedLeftVisibleBands
        {
            get => 
                (ReadOnlyCollection<BandBase>) base.GetValue(FixedLeftVisibleBandsProperty);
            private set => 
                base.SetValue(FixedLeftVisibleBandsPropertyKey, value);
        }

        [Browsable(false)]
        public ReadOnlyCollection<BandBase> FixedRightVisibleBands
        {
            get => 
                (ReadOnlyCollection<BandBase>) base.GetValue(FixedRightVisibleBandsProperty);
            private set => 
                base.SetValue(FixedRightVisibleBandsPropertyKey, value);
        }

        [Browsable(false)]
        public ReadOnlyCollection<BandBase> FixedNoneVisibleBands
        {
            get => 
                (ReadOnlyCollection<BandBase>) base.GetValue(FixedNoneVisibleBandsProperty);
            private set => 
                base.SetValue(FixedNoneVisibleBandsPropertyKey, value);
        }

        [Browsable(false)]
        public bool ShowIndicator
        {
            get => 
                (bool) base.GetValue(ShowIndicatorProperty);
            private set => 
                base.SetValue(ShowIndicatorPropertyKey, value);
        }

        internal Style HeaderImageStyle
        {
            get => 
                this.headerImageStyleCore;
            set
            {
                if (!ReferenceEquals(this.headerImageStyleCore, value))
                {
                    this.headerImageStyleCore = value;
                    this.OnHeaderImageStyleChanged();
                }
            }
        }

        public IBandsCollection BandsCore =>
            this.DataControl.BandsCore;

        internal DataControlBase DataControl
        {
            get => 
                this.dataControl;
            set
            {
                if (!ReferenceEquals(this.dataControl, value))
                {
                    if ((this.dataControl != null) && !this.dataControl.IsDeserializing)
                    {
                        this.dataControl.ColumnsCore.Clear();
                    }
                    this.dataControl = value;
                    if (this.dataControl != null)
                    {
                        this.bandsHelper = new BandsHelper(this, false);
                        if (!this.dataControl.IsDeserializing)
                        {
                            this.FillColumns();
                        }
                        else
                        {
                            this.dataControl.RemoveColumnsFromLogicalChildren();
                        }
                    }
                }
            }
        }

        DataControlBase IBandsOwner.DataControl =>
            this.DataControl;

        public List<BandBase> VisibleBands { get; private set; }

        internal IEnumerable<BandBase> PrintableBands
        {
            get
            {
                Func<BandBase, bool> predicate = <>c.<>9__110_0;
                if (<>c.<>9__110_0 == null)
                {
                    Func<BandBase, bool> local1 = <>c.<>9__110_0;
                    predicate = <>c.<>9__110_0 = band => band.AllowPrinting;
                }
                return this.VisibleBands.Where<BandBase>(predicate);
            }
        }

        List<BandBase> IBandsOwner.VisibleBands =>
            this.VisibleBands;

        private DataViewBase View =>
            (this.DataControl != null) ? this.DataControl.viewCore : null;

        private RebuildBandsLayoutHelperBase RebuildBandsLayoutHelper
        {
            get
            {
                if ((this.View.UseLegacyColumnVisibleIndexes != this.useLegacyColumnVisibleIndexes) || (this.rebuildBandsLayoutHelper == null))
                {
                    this.useLegacyColumnVisibleIndexes = this.View.UseLegacyColumnVisibleIndexes;
                    this.rebuildBandsLayoutHelper = this.useLegacyColumnVisibleIndexes ? ((RebuildBandsLayoutHelperBase) new RebuildBandsLayoutHelperLegacy(this)) : ((RebuildBandsLayoutHelperBase) new DevExpress.Xpf.Grid.Native.RebuildBandsLayoutHelper(this));
                }
                return this.rebuildBandsLayoutHelper;
            }
        }

        private bool AllowSyncWithPlainColumnCollection =>
            (this.DataControl != null) && (((this.View == null) || !this.View.IsLockUpdateColumnsLayout) && !this.IsRootBandsCollectionLocked);

        private bool IsRootBandsCollectionLocked =>
            (this.DataControl != null) && ((this.DataControl.BandsCore != null) && this.DataControl.BandsCore.IsLockUpdate);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandsLayoutBase.<>c <>9 = new BandsLayoutBase.<>c();
            public static Action<DataViewBase> <>9__25_0;
            public static Action<DataViewBase> <>9__26_0;
            public static Action<DataViewBase> <>9__27_0;
            public static Action<BandBase> <>9__28_0;
            public static Action<BandBase> <>9__29_0;
            public static Action<BandBase> <>9__30_0;
            public static Action<BandBase> <>9__93_0;
            public static Func<ColumnBase, ColumnBase> <>9__96_0;
            public static Func<BandBase, BandBase> <>9__96_1;
            public static Func<BandBase, bool> <>9__110_0;
            public static Action<DataViewBase> <>9__121_0;
            public static Action<DataViewBase> <>9__121_1;
            public static Action<DataViewBase> <>9__121_2;
            public static Action<DataViewBase> <>9__121_3;
            public static Func<DataControlBase, IColumnCollection> <>9__143_1;
            public static Action<IColumnCollection> <>9__143_2;
            public static Action<IColumnCollection> <>9__143_3;
            public static Action<DataViewBase> <>9__156_0;
            public static Action<DataViewBase> <>9__156_1;
            public static Func<DataControlBase, DataControlBase> <>9__158_1;
            public static Action<DataControlBase> <>9__158_3;
            public static Func<BandRow, IEnumerable<ColumnBase>> <>9__177_0;
            public static Func<ColumnBase, bool> <>9__177_1;

            internal void <.cctor>b__24_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).OnShowBandsPanelChanged();
            }

            internal void <.cctor>b__24_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).UpdateBandActualHeaderTemplateSelector();
            }

            internal void <.cctor>b__24_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).OnFixedRightVisibleBandsChanged();
            }

            internal void <.cctor>b__24_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).OnFixedNoneVisibleBandsChanged();
            }

            internal void <.cctor>b__24_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).UpdateBandsLayout();
            }

            internal void <.cctor>b__24_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).UpdateBandActualHeaderTemplateSelector();
            }

            internal void <.cctor>b__24_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).UpdateBandHeaderToolTipTemplate();
            }

            internal void <.cctor>b__24_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).OnShowBandsInCustomizationFormChanged();
            }

            internal void <.cctor>b__24_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).UpdateViewInfo();
            }

            internal void <.cctor>b__24_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).UpdateViewInfo();
            }

            internal void <.cctor>b__24_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).RebuildColumnChooserColumns();
            }

            internal void <.cctor>b__24_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).UpdatePrintBandHeaderStyle();
            }

            internal void <.cctor>b__24_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandsLayoutBase) d).OnFixedLeftVisibleBandsChanged();
            }

            internal ColumnBase <.ctor>b__96_0(ColumnBase x) => 
                x;

            internal BandBase <.ctor>b__96_1(BandBase x) => 
                x;

            internal void <DoMoveAction>b__156_0(DataViewBase view)
            {
                view.BeginUpdateColumnsLayout();
            }

            internal void <DoMoveAction>b__156_1(DataViewBase view)
            {
                view.EndUpdateColumnsLayout();
            }

            internal bool <get_PrintableBands>b__110_0(BandBase band) => 
                band.AllowPrinting;

            internal IEnumerable<ColumnBase> <HasColumns>b__177_0(BandRow row) => 
                row.Columns;

            internal bool <HasColumns>b__177_1(ColumnBase column) => 
                column.AllowPrinting;

            internal DataControlBase <Move>b__158_1(DataControlBase dc) => 
                dc;

            internal void <Move>b__158_3(DataControlBase dc)
            {
                dc.BandsCore.Clear();
            }

            internal void <OnFixedLeftVisibleBandsChanged>b__27_0(DataViewBase x)
            {
                x.ViewBehavior.NotifyFixedLeftBandsChanged();
            }

            internal void <OnFixedNoneVisibleBandsChanged>b__25_0(DataViewBase x)
            {
                x.ViewBehavior.NotifyFixedNoneBandsChanged();
            }

            internal void <OnFixedRightVisibleBandsChanged>b__26_0(DataViewBase x)
            {
                x.ViewBehavior.NotifyFixedRightBandsChanged();
            }

            internal void <OnHeaderImageStyleChanged>b__93_0(BandBase b)
            {
                b.UpdateActualHeaderImageStyle();
            }

            internal void <RebuildBandsVisibleColumns>b__121_0(DataViewBase x)
            {
                x.BeginUpdateColumnsLayout();
            }

            internal void <RebuildBandsVisibleColumns>b__121_1(DataViewBase x)
            {
                x.EndUpdateColumnsLayout(false);
            }

            internal void <RebuildBandsVisibleColumns>b__121_2(DataViewBase x)
            {
                x.UpdateVisibleIndexesLocker.Lock();
            }

            internal void <RebuildBandsVisibleColumns>b__121_3(DataViewBase x)
            {
                x.UpdateVisibleIndexesLocker.Unlock();
            }

            internal IColumnCollection <SyncWithPlainBandCollection>b__143_1(DataControlBase x) => 
                x.ColumnsCore;

            internal void <SyncWithPlainBandCollection>b__143_2(IColumnCollection x)
            {
                x.BeginUpdate();
            }

            internal void <SyncWithPlainBandCollection>b__143_3(IColumnCollection x)
            {
                x.EndUpdate();
            }

            internal void <UpdateBandActualHeaderTemplateSelector>b__28_0(BandBase band)
            {
                band.UpdateActualHeaderTemplateSelector();
            }

            internal void <UpdateBandHeaderToolTipTemplate>b__29_0(BandBase band)
            {
                band.UpdateActualHeaderToolTipTemplate();
            }

            internal void <UpdatePrintBandHeaderStyle>b__30_0(BandBase band)
            {
                band.UpdateActualPrintBandHeaderStyle();
            }
        }
    }
}

