namespace DevExpress.Xpf.Editors.DataPager
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class DataPager : Control
    {
        public static readonly DependencyProperty AutoEllipsisProperty;
        public static readonly DependencyProperty CurrentPageParamsProperty;
        public static readonly DependencyProperty DataPagerProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        public static readonly DependencyProperty IsTotalItemCountFixedProperty;
        public static readonly DependencyProperty ItemCountProperty;
        public static readonly DependencyProperty NumericButtonCountProperty;
        public static readonly DependencyProperty PageIndexProperty;
        public static readonly DependencyProperty PageSizeProperty;
        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty ShowTotalPageCountProperty;
        public static readonly DependencyProperty ActualNumericButtonCountProperty;
        private static readonly DependencyPropertyKey ActualNumericButtonCountPropertyKey;
        public static readonly DependencyProperty ActualPageIndexProperty;
        private static readonly DependencyPropertyKey ActualPageIndexPropertyKey;
        public static readonly DependencyProperty ActualPageSizeProperty;
        private static readonly DependencyPropertyKey ActualPageSizePropertyKey;
        public static readonly DependencyProperty CanChangePageProperty;
        private static readonly DependencyPropertyKey CanChangePagePropertyKey;
        public static readonly DependencyProperty CanMoveToFirstPageProperty;
        private static readonly DependencyPropertyKey CanMoveToFirstPagePropertyKey;
        public static readonly DependencyProperty CanMoveToLastPageProperty;
        private static readonly DependencyPropertyKey CanMoveToLastPagePropertyKey;
        public static readonly DependencyProperty CanMoveToNextPageProperty;
        private static readonly DependencyPropertyKey CanMoveToNextPagePropertyKey;
        public static readonly DependencyProperty CanMoveToPreviousPageProperty;
        private static readonly DependencyPropertyKey CanMoveToPreviousPagePropertyKey;
        public static readonly DependencyProperty ContainerFirstButtonPageNumberProperty;
        private static readonly DependencyPropertyKey ContainerFirstButtonPageNumberPropertyKey;
        public static readonly DependencyProperty ContainerSecondButtonPageNumberProperty;
        private static readonly DependencyPropertyKey ContainerSecondButtonPageNumberPropertyKey;
        public static readonly DependencyProperty IsAutoNumericButtonCountProperty;
        private static readonly DependencyPropertyKey IsAutoNumericButtonCountPropertyKey;
        public static readonly DependencyProperty PageCountProperty;
        private static readonly DependencyPropertyKey PageCountPropertyKey;
        public static readonly DependencyProperty PagedSourceProperty;
        private static readonly DependencyPropertyKey PagedSourcePropertyKey;
        internal DataPagerNumericButtonContainer container;
        private int beforeAutoNumericButtonCount;
        private bool leftScroll = true;
        private bool rightScroll = true;
        private Locker lockerPagedSourcePageSize = new Locker();

        public event EventHandler<DataPagerPageIndexChangedEventArgs> PageIndexChanged;

        public event EventHandler<DataPagerPageIndexChangingEventArgs> PageIndexChanging;

        static DataPager()
        {
            Type ownerType = typeof(DevExpress.Xpf.Editors.DataPager.DataPager);
            ActualNumericButtonCountPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualNumericButtonCount", typeof(int), ownerType, new PropertyMetadata(0, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnActualNumericButtonCountChanged((int) e.NewValue)));
            ActualNumericButtonCountProperty = ActualNumericButtonCountPropertyKey.DependencyProperty;
            ActualPageIndexPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPageIndex", typeof(int), ownerType, new PropertyMetadata(-1, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnActualPageIndexChanged((int) e.OldValue, (int) e.NewValue)));
            ActualPageIndexProperty = ActualPageIndexPropertyKey.DependencyProperty;
            ActualPageSizePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualPageSize", typeof(int), ownerType, new PropertyMetadata(0, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnActualPageSizeChanged((int) e.NewValue)));
            ActualPageSizeProperty = ActualPageSizePropertyKey.DependencyProperty;
            AutoEllipsisProperty = DependencyPropertyManager.Register("AutoEllipsis", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnAutoEllipsisChanged((bool) e.NewValue)));
            CanChangePagePropertyKey = DependencyPropertyManager.RegisterReadOnly("CanChangePage", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanChangePageChanged((bool) e.NewValue)));
            CanChangePageProperty = CanChangePagePropertyKey.DependencyProperty;
            CanMoveToFirstPagePropertyKey = DependencyPropertyManager.RegisterReadOnly("CanMoveToFirstPage", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToFirstPageChanged((bool) e.NewValue)));
            CanMoveToFirstPageProperty = CanMoveToFirstPagePropertyKey.DependencyProperty;
            CanMoveToLastPagePropertyKey = DependencyPropertyManager.RegisterReadOnly("CanMoveToLastPage", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToLastPageChanged((bool) e.NewValue)));
            CanMoveToLastPageProperty = CanMoveToLastPagePropertyKey.DependencyProperty;
            CanMoveToNextPagePropertyKey = DependencyPropertyManager.RegisterReadOnly("CanMoveToNextPage", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToNextPageChanged((bool) e.NewValue)));
            CanMoveToNextPageProperty = CanMoveToNextPagePropertyKey.DependencyProperty;
            CanMoveToPreviousPagePropertyKey = DependencyPropertyManager.RegisterReadOnly("CanMoveToPreviousPage", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToPreviousPageChanged((bool) e.NewValue)));
            CanMoveToPreviousPageProperty = CanMoveToPreviousPagePropertyKey.DependencyProperty;
            ContainerFirstButtonPageNumberPropertyKey = DependencyPropertyManager.RegisterReadOnly("ContainerFirstButtonPageNumber", typeof(int), ownerType, new PropertyMetadata(1, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerFirstButtonPageNumberChanged((int) e.NewValue), (d, baseValue) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerFirstButtonPageNumberCoerce(baseValue)));
            ContainerFirstButtonPageNumberProperty = ContainerFirstButtonPageNumberPropertyKey.DependencyProperty;
            ContainerSecondButtonPageNumberPropertyKey = DependencyPropertyManager.RegisterReadOnly("ContainerSecondButtonPageNumber", typeof(int), ownerType, new PropertyMetadata(2, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerSecondButtonPageNumberChanged((int) e.NewValue), (d, baseValue) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerSecondButtonPageNumberCoerce(baseValue)));
            ContainerSecondButtonPageNumberProperty = ContainerSecondButtonPageNumberPropertyKey.DependencyProperty;
            CurrentPageParamsProperty = DependencyPropertyManager.Register("CurrentPageParams", typeof(DataPagerCurrentPageParams), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCurrentPageParamsChanged()));
            DataPagerProperty = DependencyPropertyManager.RegisterAttached("DataPager", typeof(DevExpress.Xpf.Editors.DataPager.DataPager), typeof(DevExpress.Xpf.Editors.DataPager.DataPager), new PropertyMetadata(null));
            DisplayModeProperty = DependencyPropertyManager.Register("DisplayMode", typeof(DataPagerDisplayMode), ownerType, new PropertyMetadata(DataPagerDisplayMode.FirstLastPreviousNextNumeric, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnDisplayModeChanged((DataPagerDisplayMode) e.NewValue)));
            IsAutoNumericButtonCountPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsAutoNumericButtonCount", typeof(bool), ownerType, new PropertyMetadata(false));
            IsAutoNumericButtonCountProperty = IsAutoNumericButtonCountPropertyKey.DependencyProperty;
            IsTotalItemCountFixedProperty = DependencyPropertyManager.Register("IsTotalItemCountFixed", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnIsTotalItemCountFixedChanged((bool) e.NewValue)));
            ItemCountProperty = DependencyPropertyManager.Register("ItemCount", typeof(int), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnItemCountChanged((int) e.NewValue)));
            NumericButtonCountProperty = DependencyPropertyManager.Register("NumericButtonCount", typeof(int), ownerType, new PropertyMetadata(5, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnNumericButtonCountChanged((int) e.OldValue, (int) e.NewValue), (d, baseValue) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnNumericButtonCountCoerce(baseValue)));
            PageCountPropertyKey = DependencyPropertyManager.RegisterReadOnly("PageCount", typeof(int), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageCountChanged((int) e.NewValue)));
            PageCountProperty = PageCountPropertyKey.DependencyProperty;
            PageIndexProperty = DependencyPropertyManager.Register("PageIndex", typeof(int), ownerType, new PropertyMetadata(-1, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageIndexChanged((int) e.OldValue, (int) e.NewValue), (d, baseValue) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageIndexCoerce(baseValue)));
            PagedSourcePropertyKey = DependencyPropertyManager.RegisterReadOnly("PagedSource", typeof(IPagedCollectionView), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPagedSourceChanged((IPagedCollectionView) e.OldValue, (IPagedCollectionView) e.NewValue)));
            PagedSourceProperty = PagedSourcePropertyKey.DependencyProperty;
            PageSizeProperty = DependencyPropertyManager.Register("PageSize", typeof(int), ownerType, new PropertyMetadata(1, (d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageSizeChanged((int) e.NewValue), (d, baseValue) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageSizeCoerce(baseValue)));
            SourceProperty = DependencyPropertyManager.Register("Source", typeof(IEnumerable), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue)));
            ShowTotalPageCountProperty = DependencyPropertyManager.Register("ShowTotalPageCount", typeof(bool), ownerType, new PropertyMetadata((d, e) => ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnShowTotalPageCountChanged((bool) e.NewValue)));
        }

        public DataPager()
        {
            this.SetDefaultStyleKey(typeof(DevExpress.Xpf.Editors.DataPager.DataPager));
            SetDataPager(this, this);
            this.SetCommands();
            this.UpdateSourceLocker = new Locker();
        }

        private void CheckOnShowFirstLastPage(ref bool leftScroll, ref bool rightScroll)
        {
            if (this.ContainerFirstButtonPageNumber == 1)
            {
                leftScroll = false;
            }
            if (((this.ContainerFirstButtonPageNumber + this.ActualNumericButtonCount) - 1) == this.PageCount)
            {
                rightScroll = false;
            }
        }

        public static DevExpress.Xpf.Editors.DataPager.DataPager GetDataPager(DependencyObject obj) => 
            (DevExpress.Xpf.Editors.DataPager.DataPager) obj.GetValue(DataPagerProperty);

        private int GetDelta(int oldPageIndex, int newPageIndex, int offset) => 
            (newPageIndex - oldPageIndex) + offset;

        private int GetRelativeDelta(int firstPageNumber, int secondPageNumber, int absoluteDelta, int offset) => 
            ((firstPageNumber + absoluteDelta) + offset) - secondPageNumber;

        public void MoveToFirstPage()
        {
            this.PageIndex = 0;
        }

        public void MoveToLastPage()
        {
            this.PageIndex = this.PageCount - 1;
        }

        public void MoveToNextPage()
        {
            if (this.CanMoveToNextPage && (this.PageIndex < (this.PageCount - 1)))
            {
                this.PageIndex++;
            }
        }

        public void MoveToPage(int pageNumber)
        {
            if (this.PageIndex != (pageNumber - 1))
            {
                this.PageIndex = pageNumber - 1;
            }
            else if ((this.PagedSource != null) && (this.PagedSource.PageIndex != (pageNumber - 1)))
            {
                this.PagedSource.MoveToPage(pageNumber - 1);
            }
        }

        public void MoveToPreviousPage()
        {
            this.PageIndex--;
        }

        protected virtual void OnActualNumericButtonCountChanged(int newValue)
        {
        }

        protected virtual void OnActualPageIndexChanged(int oldValue, int newValue)
        {
            this.beforeAutoNumericButtonCount = ((this.container == null) || (this.container.Panel == null)) ? this.NumericButtonCount : this.container.Panel.Children.Count;
            this.SetMoveToPageFlags();
            if (this.PagedSource != null)
            {
                this.MoveToPage(newValue + 1);
            }
            if (!this.TryScrolling(oldValue, newValue) && (this.container != null))
            {
                this.container.UpdateButtons();
            }
            this.UpdateCurrentPageParams();
            this.RaisePageIndexChanged(oldValue, newValue);
        }

        protected virtual void OnActualPageSizeChanged(int newValue)
        {
            this.UpdatePageCount();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.ButtonContainer != null)
            {
                this.ButtonContainer.NumericButtonContainerChanged -= new EventHandler(this.OnButtonContainerNumericButtonContainerChanged);
            }
            this.ButtonContainer = base.GetTemplateChild("PART_ButtonContainer") as DataPagerButtonContainer;
            if (this.ButtonContainer != null)
            {
                this.ButtonContainer.NumericButtonContainerChanged += new EventHandler(this.OnButtonContainerNumericButtonContainerChanged);
            }
        }

        protected virtual void OnAutoEllipsisChanged(bool newValue)
        {
        }

        private void OnButtonContainerNumericButtonContainerChanged(object sender, EventArgs e)
        {
            this.container = this.ButtonContainer.NumericButtonContainer;
        }

        protected virtual void OnCanChangePageChanged(bool newValue)
        {
            (this.NumericPageCommand as DelegateCommand<object>).RaiseCanExecuteChanged();
        }

        protected virtual void OnCanMoveToFirstPageChanged(bool newValue)
        {
            (this.FirstPageCommand as DelegateCommand<object>).RaiseCanExecuteChanged();
        }

        protected virtual void OnCanMoveToLastPageChanged(bool newValue)
        {
            (this.LastPageCommand as DelegateCommand<object>).RaiseCanExecuteChanged();
        }

        protected virtual void OnCanMoveToNextPageChanged(bool newValue)
        {
            (this.NextPageCommand as DelegateCommand<object>).RaiseCanExecuteChanged();
        }

        protected virtual void OnCanMoveToPreviousPageChanged(bool newValue)
        {
            (this.PreviousPageCommand as DelegateCommand<object>).RaiseCanExecuteChanged();
        }

        protected virtual void OnContainerFirstButtonPageNumberChanged(int newValue)
        {
        }

        protected object OnContainerFirstButtonPageNumberCoerce(object baseValue) => 
            (this.ActualNumericButtonCount != 1) ? Math.Max(1, Math.Min((int) baseValue, (this.PageCount - this.ActualNumericButtonCount) + 1)) : Math.Max(1, Math.Min((int) baseValue, this.PageCount));

        protected virtual void OnContainerSecondButtonPageNumberChanged(int newValue)
        {
        }

        protected virtual object OnContainerSecondButtonPageNumberCoerce(object baseValue) => 
            Math.Max(2, Math.Min((int) baseValue, this.PageCount));

        protected virtual void OnCurrentPageParamsChanged()
        {
            if (base.IsInitialized)
            {
                if (this.CurrentPageParams.PageCount != -1)
                {
                    this.PageIndex = this.CurrentPageParams.PageIndex;
                }
                else
                {
                    DataPagerCurrentPageParams @params = new DataPagerCurrentPageParams {
                        PageCount = this.PageCount,
                        PageIndex = this.CurrentPageParams.PageIndex
                    };
                    this.CurrentPageParams = @params;
                }
            }
        }

        protected virtual void OnDisplayModeChanged(DataPagerDisplayMode newValue)
        {
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.UpdateCurrentPageParams();
            this.UpdateIsAutoNumericButtonCount();
        }

        protected virtual void OnIsTotalItemCountFixedChanged(bool newValue)
        {
            this.SetMoveToPageFlags();
        }

        protected virtual void OnItemCountChanged(int newValue)
        {
            this.UpdateSource();
        }

        protected virtual void OnNumericButtonCountChanged(int oldValue, int newValue)
        {
            this.UpdateActualNumericButtonCount(newValue - oldValue);
            this.UpdateIsAutoNumericButtonCount();
        }

        protected virtual object OnNumericButtonCountCoerce(object baseValue) => 
            Math.Max(0, (int) baseValue);

        protected virtual void OnPageCountChanged(int newValue)
        {
            this.UpdateActualPageIndex();
            this.UpdateActualNumericButtonCount(0);
            this.SetMoveToPageFlags();
            this.UpdateCurrentPageParams();
        }

        protected virtual void OnPagedSourceChanged(IPagedCollectionView oldValue, IPagedCollectionView newValue)
        {
            INotifyCollectionChanged changed = oldValue as INotifyCollectionChanged;
            if (changed != null)
            {
                changed.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.PagedSourceCollectionChanged);
            }
            INotifyCollectionChanged changed2 = newValue as INotifyCollectionChanged;
            if (changed2 != null)
            {
                changed2.CollectionChanged += new NotifyCollectionChangedEventHandler(this.PagedSourceCollectionChanged);
            }
        }

        protected virtual void OnPageIndexChanged(int oldValue, int newValue)
        {
            this.UpdateActualPageIndex();
        }

        protected virtual object OnPageIndexCoerce(object baseValue)
        {
            int coerceValue = (int) DependencyObjectHelper.GetCoerceValue(this, PageIndexProperty);
            return (!this.RaisePageIndexChanging(coerceValue, (int) baseValue) ? Math.Max(0, (int) baseValue) : coerceValue);
        }

        protected virtual void OnPageSizeChanged(int newValue)
        {
            if ((this.PagedSource != null) && !this.lockerPagedSourcePageSize.IsLocked)
            {
                this.PagedSource.PageSize = newValue;
            }
            this.SetMoveToPageFlags();
        }

        protected virtual object OnPageSizeCoerce(object baseValue) => 
            Math.Max(1, (int) baseValue);

        protected virtual void OnShowTotalPageCountChanged(bool newValue)
        {
        }

        protected virtual void OnSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                this.PagedSource = !(newValue is IPagedCollectionView) ? new PagedCollectionView(newValue) : (newValue as IPagedCollectionView);
                this.UpdateSource();
            }
            else
            {
                this.PagedSource = null;
                this.ItemCount = 0;
            }
            if (oldValue != null)
            {
                this.UnsubscribeOnSourcePropertiesChanged(oldValue);
            }
            if (newValue != null)
            {
                this.SubscribeOnSourcePropertiesChanged(newValue);
            }
        }

        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PagedSource != null)
            {
                if ((e.PropertyName == "Count") && !ReferenceEquals(this.Source, this.PagedSource))
                {
                    Action<PagedCollectionView> action = <>c.<>9__189_0;
                    if (<>c.<>9__189_0 == null)
                    {
                        Action<PagedCollectionView> local1 = <>c.<>9__189_0;
                        action = <>c.<>9__189_0 = x => x.Refresh();
                    }
                    (this.PagedSource as PagedCollectionView).Do<PagedCollectionView>(action);
                    this.UpdateSource();
                }
                if ((e.PropertyName == "ItemCount") && (this.ItemCount != this.PagedSource.ItemCount))
                {
                    this.lockerPagedSourcePageSize.DoLockedAction(() => this.UpdateSource());
                }
                if (e.PropertyName == "PageIndex")
                {
                    this.PageIndex = this.PagedSource.PageIndex;
                }
                if (e.PropertyName == "PageSize")
                {
                    this.UpdateActualPageSize();
                }
            }
        }

        protected virtual void PagedSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateSource();
        }

        protected virtual void RaisePageIndexChanged(int oldValue, int newValue)
        {
            if (this.PageIndexChanged != null)
            {
                this.PageIndexChanged(this, new DataPagerPageIndexChangedEventArgs(oldValue, newValue));
            }
        }

        protected virtual bool RaisePageIndexChanging(int oldValue, int newValue)
        {
            if ((this.PageIndexChanging == null) || (oldValue == newValue))
            {
                return false;
            }
            DataPagerPageIndexChangingEventArgs e = new DataPagerPageIndexChangingEventArgs(oldValue, newValue);
            this.PageIndexChanging(this, e);
            return e.IsCancel;
        }

        private bool ScrollNumericButton(int delta, int deltaSecond)
        {
            this.ContainerSecondButtonPageNumber += deltaSecond;
            this.ContainerFirstButtonPageNumber += delta;
            if (this.container == null)
            {
                return false;
            }
            this.container.UpdateButtons();
            return true;
        }

        private void SetBtnType(DataPagerButton btn, int i, int countButton)
        {
            if (i == 0)
            {
                btn.ButtonType = DataPagerButtonType.PageFirst;
            }
            else if (i == 1)
            {
                btn.ButtonType = DataPagerButtonType.PagePrevious;
            }
            else if (i == (countButton - 2))
            {
                btn.ButtonType = DataPagerButtonType.PageNext;
            }
            else if (i == (countButton - 1))
            {
                btn.ButtonType = DataPagerButtonType.PageLast;
            }
            else
            {
                btn.ButtonType = DataPagerButtonType.PageNumeric;
            }
        }

        private void SetButtonType(DataPagerButton btn, int i, int countButton)
        {
            switch (this.DisplayMode)
            {
                case DataPagerDisplayMode.FirstLast:
                    btn.ButtonType = (i == 0) ? DataPagerButtonType.PageFirst : DataPagerButtonType.PageLast;
                    return;

                case DataPagerDisplayMode.FirstLastNumeric:
                case DataPagerDisplayMode.FirstLastPreviousNext:
                case DataPagerDisplayMode.FirstLastPreviousNextNumeric:
                    this.SetBtnType(btn, i, countButton);
                    return;

                case DataPagerDisplayMode.Numeric:
                    btn.ButtonType = DataPagerButtonType.PageNumeric;
                    return;

                case DataPagerDisplayMode.PreviousNextNumeric:
                    if (i == 0)
                    {
                        btn.ButtonType = DataPagerButtonType.PagePrevious;
                        return;
                    }
                    if (i == (countButton - 1))
                    {
                        btn.ButtonType = DataPagerButtonType.PageNext;
                        return;
                    }
                    btn.ButtonType = DataPagerButtonType.PageNumeric;
                    return;

                case DataPagerDisplayMode.PreviousNext:
                    btn.ButtonType = (i == 0) ? DataPagerButtonType.PagePrevious : DataPagerButtonType.PageNext;
                    return;
            }
        }

        private void SetCommands()
        {
            this.FirstPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToFirstPage(), parameter => this.CanMoveToFirstPage, false);
            this.LastPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToLastPage(), parameter => this.CanMoveToLastPage, false);
            this.NextPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToNextPage(), parameter => this.CanMoveToNextPage, false);
            this.NumericPageCommand = DelegateCommandFactory.Create<object>(pageNumber => this.MoveToPage((int) pageNumber), parameter => this.CanChangePage, false);
            this.PreviousPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToPreviousPage(), parameter => this.CanMoveToPreviousPage, false);
        }

        public static void SetDataPager(DependencyObject obj, DevExpress.Xpf.Editors.DataPager.DataPager value)
        {
            obj.SetValue(DataPagerProperty, value);
        }

        private void SetMoveToPageFlags()
        {
            this.CanChangePage = this.PageCount >= 2;
            if (!this.CanChangePage)
            {
                this.CanMoveToFirstPage = false;
                this.CanMoveToLastPage = false;
                this.CanMoveToNextPage = false;
                this.CanMoveToPreviousPage = false;
            }
            else
            {
                this.CanMoveToPreviousPage = true;
                this.CanMoveToFirstPage = true;
                this.CanMoveToNextPage = true;
                this.CanMoveToLastPage = true;
                if (this.ActualPageIndex == 0)
                {
                    this.CanMoveToPreviousPage = false;
                    this.CanMoveToFirstPage = false;
                }
                else if (this.ActualPageIndex == (this.PageCount - 1))
                {
                    if (this.IsTotalItemCountFixed)
                    {
                        this.CanMoveToNextPage = false;
                    }
                    this.CanMoveToLastPage = false;
                }
            }
        }

        private void SubscribeOnSourcePropertiesChanged(IEnumerable newValue)
        {
            if (newValue is INotifyPropertyChanged)
            {
                (newValue as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(this.OnSourcePropertyChanged);
            }
        }

        private bool TryScrolling(int oldPageIndex, int newPageIndex)
        {
            int absoluteDelta = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            if ((this.DisplayMode != DataPagerDisplayMode.PreviousNext) && (this.DisplayMode != DataPagerDisplayMode.FirstLast))
            {
                if ((this.ActualNumericButtonCount != 2) && (this.beforeAutoNumericButtonCount != 2))
                {
                    absoluteDelta = newPageIndex - ((this.ContainerFirstButtonPageNumber - 1) + (this.beforeAutoNumericButtonCount / 2));
                }
                else if (oldPageIndex < newPageIndex)
                {
                    num2 = (newPageIndex == (this.PageCount - 1)) ? this.GetDelta(oldPageIndex, newPageIndex, -1) : this.GetDelta(oldPageIndex, newPageIndex, 0);
                    if ((newPageIndex > 1) && (newPageIndex < (this.PageCount - 1)))
                    {
                        absoluteDelta = this.GetRelativeDelta(this.ContainerSecondButtonPageNumber, this.ContainerFirstButtonPageNumber, num2, -2);
                    }
                    else if (newPageIndex == (this.PageCount - 1))
                    {
                        absoluteDelta = this.GetRelativeDelta(this.ContainerSecondButtonPageNumber, this.ContainerFirstButtonPageNumber, num2, 0);
                    }
                }
                else
                {
                    absoluteDelta = (newPageIndex == 0) ? this.GetDelta(oldPageIndex, newPageIndex, 1) : this.GetDelta(oldPageIndex, newPageIndex, 0);
                    if ((newPageIndex > 0) && (newPageIndex < (this.PageCount - 1)))
                    {
                        num2 = this.GetRelativeDelta(this.ContainerFirstButtonPageNumber, this.ContainerSecondButtonPageNumber, absoluteDelta, 2);
                    }
                    else if (newPageIndex == 0)
                    {
                        num2 = this.GetRelativeDelta(this.ContainerFirstButtonPageNumber, this.ContainerSecondButtonPageNumber, absoluteDelta, 0);
                    }
                }
                this.leftScroll = true;
                this.rightScroll = true;
                this.CheckOnShowFirstLastPage(ref this.leftScroll, ref this.rightScroll);
                if ((absoluteDelta <= 0) || !this.rightScroll)
                {
                    if ((this.beforeAutoNumericButtonCount != 2) || (num2 <= 0))
                    {
                        if (((absoluteDelta < 0) && this.leftScroll) || ((this.beforeAutoNumericButtonCount == 2) && (num2 < 0)))
                        {
                            num3 = 1 - (this.ContainerFirstButtonPageNumber + absoluteDelta);
                            num4 = 2 - (this.ContainerSecondButtonPageNumber + num2);
                            if (num3 > 0)
                            {
                                absoluteDelta += num3;
                            }
                            if (num4 > 0)
                            {
                                num2 += num4;
                            }
                            return this.ScrollNumericButton(absoluteDelta, num2);
                        }
                    }
                    else
                    {
                        goto TR_0004;
                    }
                }
                else
                {
                    goto TR_0004;
                }
            }
            return false;
        TR_0004:
            num3 = (((this.ContainerFirstButtonPageNumber + this.beforeAutoNumericButtonCount) - 1) + absoluteDelta) - this.PageCount;
            num4 = (this.ContainerSecondButtonPageNumber + num2) - this.PageCount;
            if (num3 > 0)
            {
                absoluteDelta -= num3;
            }
            if (num4 > 0)
            {
                num2 -= num4;
            }
            return this.ScrollNumericButton(absoluteDelta, num2);
        }

        private void UnsubscribeOnSourcePropertiesChanged(IEnumerable oldValue)
        {
            if (oldValue is INotifyPropertyChanged)
            {
                (oldValue as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(this.OnSourcePropertyChanged);
            }
        }

        protected virtual void UpdateActualNumericButtonCount(int deltaCount)
        {
            int num = (this.ContainerFirstButtonPageNumber + this.ActualNumericButtonCount) - 1;
            this.ActualNumericButtonCount = Math.Min(this.NumericButtonCount, this.PageCount);
            int pageNumber = (this.ContainerFirstButtonPageNumber + this.ActualNumericButtonCount) - 1;
            if (this.ActualNumericButtonCount != 2)
            {
                if ((deltaCount > 0) && (this.ContainerFirstButtonPageNumber > 1))
                {
                    this.ContainerFirstButtonPageNumber -= deltaCount;
                }
                else if ((deltaCount < 0) && ((this.ContainerFirstButtonPageNumber > 1) && (pageNumber == (this.PageCount - 1))))
                {
                    this.ContainerFirstButtonPageNumber -= deltaCount;
                }
                if ((deltaCount < 0) && ((this.PageIndex >= pageNumber) && (pageNumber != (this.PageCount - 1))))
                {
                    this.MoveToPage(pageNumber);
                }
            }
            else if (this.ActualNumericButtonCount == 2)
            {
                if (num == this.PageCount)
                {
                    if ((deltaCount < 0) && (this.PageIndex < this.ContainerFirstButtonPageNumber))
                    {
                        this.PageIndex = this.ContainerFirstButtonPageNumber - 1;
                    }
                    this.UpdateContainerButtonPageNumber(1);
                }
                else
                {
                    if ((deltaCount < 0) && (this.PageIndex >= pageNumber))
                    {
                        this.PageIndex = pageNumber - 1;
                    }
                    if (pageNumber != (this.PageCount - 1))
                    {
                        this.UpdateContainerButtonPageNumber(1);
                    }
                }
            }
            if (this.container != null)
            {
                this.container.UpdateButtons();
            }
        }

        protected virtual void UpdateActualPageIndex()
        {
            this.ActualPageIndex = Math.Min(this.PageCount - 1, this.PageIndex);
        }

        protected virtual void UpdateActualPageSize()
        {
            if (this.ItemCount == 0)
            {
                this.ActualPageSize = this.PageSize;
            }
            else
            {
                this.ActualPageSize = Math.Min(this.PageSize, this.ItemCount);
            }
        }

        private void UpdateContainerButtonPageNumber(int offset)
        {
            this.ContainerFirstButtonPageNumber = this.PageIndex;
            this.ContainerSecondButtonPageNumber = this.ContainerFirstButtonPageNumber + offset;
        }

        protected void UpdateCurrentPageParams()
        {
            DataPagerCurrentPageParams @params = new DataPagerCurrentPageParams {
                PageCount = this.PageCount,
                PageIndex = this.ActualPageIndex
            };
            this.CurrentPageParams = @params;
        }

        protected void UpdateIsAutoNumericButtonCount()
        {
            this.IsAutoNumericButtonCount = this.NumericButtonCount == 0;
        }

        protected virtual void UpdatePageCount()
        {
            this.PageCount = (this.ActualPageSize == 0) ? 0 : ((int) Math.Ceiling((double) (((double) this.ItemCount) / ((double) this.ActualPageSize))));
        }

        protected virtual void UpdatePagedSourceProperties()
        {
            if (this.PagedSource != null)
            {
                if (!this.lockerPagedSourcePageSize.IsLocked)
                {
                    this.PagedSource.PageSize = this.PageSize;
                }
                this.PagedSource.MoveToPage(this.ActualPageIndex);
            }
        }

        protected virtual void UpdateSource()
        {
            this.UpdateSourceLocker.DoLockedActionIfNotLocked(delegate {
                if (this.PagedSource != null)
                {
                    this.ItemCount = Math.Max(0, this.PagedSource.ItemCount);
                }
                this.UpdateActualPageSize();
                this.UpdatePageCount();
                this.UpdateActualPageIndex();
                this.UpdateActualNumericButtonCount(0);
                if (this.Source != null)
                {
                    if ((this.PageIndex == -1) && (this.PageCount > 0))
                    {
                        this.MoveToFirstPage();
                    }
                    else if (this.PageIndex >= this.PageCount)
                    {
                        this.MoveToPage(this.PageCount - 1);
                    }
                }
                this.UpdatePagedSourceProperties();
                this.SetMoveToPageFlags();
            });
        }

        private Locker UpdateSourceLocker { get; set; }

        public IPagedCollectionView PagedSource
        {
            get => 
                (IPagedCollectionView) base.GetValue(PagedSourceProperty);
            private set => 
                base.SetValue(PagedSourcePropertyKey, value);
        }

        public int ActualNumericButtonCount
        {
            get => 
                (int) base.GetValue(ActualNumericButtonCountProperty);
            private set => 
                base.SetValue(ActualNumericButtonCountPropertyKey, value);
        }

        public int ActualPageIndex
        {
            get => 
                (int) base.GetValue(ActualPageIndexProperty);
            private set => 
                base.SetValue(ActualPageIndexPropertyKey, value);
        }

        public int ActualPageSize
        {
            get => 
                (int) base.GetValue(ActualPageSizeProperty);
            private set => 
                base.SetValue(ActualPageSizePropertyKey, value);
        }

        public bool AutoEllipsis
        {
            get => 
                (bool) base.GetValue(AutoEllipsisProperty);
            set => 
                base.SetValue(AutoEllipsisProperty, value);
        }

        public bool CanChangePage
        {
            get => 
                (bool) base.GetValue(CanChangePageProperty);
            private set => 
                base.SetValue(CanChangePagePropertyKey, value);
        }

        public bool CanMoveToFirstPage
        {
            get => 
                (bool) base.GetValue(CanMoveToFirstPageProperty);
            private set => 
                base.SetValue(CanMoveToFirstPagePropertyKey, value);
        }

        public bool CanMoveToLastPage
        {
            get => 
                (bool) base.GetValue(CanMoveToLastPageProperty);
            private set => 
                base.SetValue(CanMoveToLastPagePropertyKey, value);
        }

        public bool CanMoveToNextPage
        {
            get => 
                (bool) base.GetValue(CanMoveToNextPageProperty);
            private set => 
                base.SetValue(CanMoveToNextPagePropertyKey, value);
        }

        public bool CanMoveToPreviousPage
        {
            get => 
                (bool) base.GetValue(CanMoveToPreviousPageProperty);
            private set => 
                base.SetValue(CanMoveToPreviousPagePropertyKey, value);
        }

        public int ContainerFirstButtonPageNumber
        {
            get => 
                (int) base.GetValue(ContainerFirstButtonPageNumberProperty);
            private set => 
                base.SetValue(ContainerFirstButtonPageNumberPropertyKey, value);
        }

        public int ContainerSecondButtonPageNumber
        {
            get => 
                (int) base.GetValue(ContainerSecondButtonPageNumberProperty);
            private set => 
                base.SetValue(ContainerSecondButtonPageNumberPropertyKey, value);
        }

        public DataPagerCurrentPageParams CurrentPageParams
        {
            get => 
                (DataPagerCurrentPageParams) base.GetValue(CurrentPageParamsProperty);
            set => 
                base.SetValue(CurrentPageParamsProperty, value);
        }

        public DataPagerDisplayMode DisplayMode
        {
            get => 
                (DataPagerDisplayMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
        }

        public bool IsAutoNumericButtonCount
        {
            get => 
                (bool) base.GetValue(IsAutoNumericButtonCountProperty);
            private set => 
                base.SetValue(IsAutoNumericButtonCountPropertyKey, value);
        }

        public int NumericButtonCount
        {
            get => 
                (int) base.GetValue(NumericButtonCountProperty);
            set => 
                base.SetValue(NumericButtonCountProperty, value);
        }

        public bool IsTotalItemCountFixed
        {
            get => 
                (bool) base.GetValue(IsTotalItemCountFixedProperty);
            set => 
                base.SetValue(IsTotalItemCountFixedProperty, value);
        }

        public int ItemCount
        {
            get => 
                (int) base.GetValue(ItemCountProperty);
            set => 
                base.SetValue(ItemCountProperty, value);
        }

        public int PageCount
        {
            get => 
                (int) base.GetValue(PageCountProperty);
            private set => 
                base.SetValue(PageCountPropertyKey, value);
        }

        public int PageIndex
        {
            get => 
                (int) base.GetValue(PageIndexProperty);
            set => 
                base.SetValue(PageIndexProperty, value);
        }

        public int PageSize
        {
            get => 
                (int) base.GetValue(PageSizeProperty);
            set => 
                base.SetValue(PageSizeProperty, value);
        }

        public IEnumerable Source
        {
            get => 
                base.GetValue(SourceProperty) as IEnumerable;
            set => 
                base.SetValue(SourceProperty, value);
        }

        public bool ShowTotalPageCount
        {
            get => 
                (bool) base.GetValue(ShowTotalPageCountProperty);
            set => 
                base.SetValue(ShowTotalPageCountProperty, value);
        }

        public ICommand FirstPageCommand { get; private set; }

        public ICommand LastPageCommand { get; private set; }

        public ICommand NextPageCommand { get; private set; }

        public ICommand NumericPageCommand { get; private set; }

        public ICommand PreviousPageCommand { get; private set; }

        protected DataPagerButtonContainer ButtonContainer { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Editors.DataPager.DataPager.<>c <>9 = new DevExpress.Xpf.Editors.DataPager.DataPager.<>c();
            public static Action<PagedCollectionView> <>9__189_0;

            internal void <.cctor>b__37_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnActualNumericButtonCountChanged((int) e.NewValue);
            }

            internal void <.cctor>b__37_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnActualPageIndexChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal object <.cctor>b__37_10(DependencyObject d, object baseValue) => 
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerFirstButtonPageNumberCoerce(baseValue);

            internal void <.cctor>b__37_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerSecondButtonPageNumberChanged((int) e.NewValue);
            }

            internal object <.cctor>b__37_12(DependencyObject d, object baseValue) => 
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerSecondButtonPageNumberCoerce(baseValue);

            internal void <.cctor>b__37_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCurrentPageParamsChanged();
            }

            internal void <.cctor>b__37_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnDisplayModeChanged((DataPagerDisplayMode) e.NewValue);
            }

            internal void <.cctor>b__37_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnIsTotalItemCountFixedChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnItemCountChanged((int) e.NewValue);
            }

            internal void <.cctor>b__37_17(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnNumericButtonCountChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal object <.cctor>b__37_18(DependencyObject d, object baseValue) => 
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnNumericButtonCountCoerce(baseValue);

            internal void <.cctor>b__37_19(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageCountChanged((int) e.NewValue);
            }

            internal void <.cctor>b__37_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnActualPageSizeChanged((int) e.NewValue);
            }

            internal void <.cctor>b__37_20(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageIndexChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal object <.cctor>b__37_21(DependencyObject d, object baseValue) => 
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageIndexCoerce(baseValue);

            internal void <.cctor>b__37_22(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPagedSourceChanged((IPagedCollectionView) e.OldValue, (IPagedCollectionView) e.NewValue);
            }

            internal void <.cctor>b__37_23(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageSizeChanged((int) e.NewValue);
            }

            internal object <.cctor>b__37_24(DependencyObject d, object baseValue) => 
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnPageSizeCoerce(baseValue);

            internal void <.cctor>b__37_25(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue);
            }

            internal void <.cctor>b__37_26(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnShowTotalPageCountChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnAutoEllipsisChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanChangePageChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToFirstPageChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToLastPageChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToNextPageChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnCanMoveToPreviousPageChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__37_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Editors.DataPager.DataPager) d).OnContainerFirstButtonPageNumberChanged((int) e.NewValue);
            }

            internal void <OnSourcePropertyChanged>b__189_0(PagedCollectionView x)
            {
                x.Refresh();
            }
        }
    }
}

