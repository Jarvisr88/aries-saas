namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.DataPager;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PagerControl : Control
    {
        public static readonly DependencyProperty PageIndexProperty;
        public static readonly DependencyProperty PageSizeProperty;
        public static readonly DependencyProperty ItemCountProperty;
        public static readonly DependencyProperty PageCountProperty;
        private static readonly DependencyPropertyKey PageCountPropertyKey;
        public static readonly DependencyProperty NumericButtonCountProperty;
        public static readonly DependencyProperty EllipsisModeProperty;
        public static readonly DependencyProperty ShowNumericButtonsProperty;
        public static readonly DependencyProperty ShowDisabledButtonsProperty;
        public static readonly DependencyProperty ShowSummaryTextProperty;
        public static readonly DependencyProperty ShowFirstPageButtonProperty;
        public static readonly DependencyProperty ShowLastPageButtonProperty;
        public static readonly DependencyProperty ShowPrevPageButtonProperty;
        public static readonly DependencyProperty ShowNextPageButtonProperty;
        public static readonly DependencyProperty AllowFirstPageButtonProperty;
        public static readonly DependencyProperty AllowNextPageButtonProperty;
        public static readonly DependencyProperty AllowPrevPageButtonProperty;
        public static readonly DependencyProperty AllowLastPageButtonProperty;
        public static readonly DependencyProperty ShowRefreshPageButtonProperty;
        public static readonly DependencyProperty SummaryTextProperty;
        private static readonly DependencyPropertyKey SummaryTextPropertyKey;
        public static readonly DependencyProperty ActualShowFirstPageButtonProperty;
        private static readonly DependencyPropertyKey ActualShowFirstPageButtonPropertyKey;
        public static readonly DependencyProperty ActualShowLastPageButtonProperty;
        private static readonly DependencyPropertyKey ActualShowLastPageButtonPropertyKey;
        public static readonly DependencyProperty ActualShowPrevPageButtonProperty;
        private static readonly DependencyPropertyKey ActualShowPrevPageButtonPropertyKey;
        public static readonly DependencyProperty ActualShowNextPageButtonProperty;
        private static readonly DependencyPropertyKey ActualShowNextPageButtonPropertyKey;
        public static readonly DependencyProperty HasMorePagesProperty;
        public static readonly DependencyProperty PagerControlProperty;
        private readonly Locker supportInitializeLocker = new Locker();

        public event EventHandler<DataPagerPageIndexChangedEventArgs> PageIndexChanged;

        public event EventHandler<DataPagerPageIndexChangingEventArgs> PageIndexChanging;

        public event EventHandler<EventArgs> RefreshPage;

        static PagerControl()
        {
            Type ownerType = typeof(PagerControl);
            PageIndexProperty = DependencyPropertyManager.Register("PageIndex", typeof(int), ownerType, new FrameworkPropertyMetadata(0, (d, e) => ((PagerControl) d).OnPageIndexChanged((int) e.OldValue, (int) e.NewValue), (d, value) => ((PagerControl) d).CoercePageIndex((int) value)));
            PageSizeProperty = DependencyPropertyManager.Register("PageSize", typeof(int), ownerType, new FrameworkPropertyMetadata(1, (d, e) => ((PagerControl) d).OnPageSizeChanged(), (CoerceValueCallback) ((d, value) => ((PagerControl) d).CoercePositiveValue((int) value, 1))));
            ItemCountProperty = DependencyPropertyManager.Register("ItemCount", typeof(int), ownerType, new FrameworkPropertyMetadata((d, e) => ((PagerControl) d).OnPageSizeChanged(), (CoerceValueCallback) ((d, value) => ((PagerControl) d).CoercePositiveValue((int) value, 0))));
            NumericButtonCountProperty = DependencyPropertyManager.Register("NumericButtonCount", typeof(int), ownerType, new FrameworkPropertyMetadata((int) 10, (d, e) => ((PagerControl) d).UpdateCore(false), (CoerceValueCallback) ((d, value) => ((PagerControl) d).CoercePositiveValue((int) value, 0))));
            EllipsisModeProperty = DependencyPropertyManager.Register("EllipsisMode", typeof(PagerControlEllipsisMode), ownerType, new FrameworkPropertyMetadata(PagerControlEllipsisMode.OutsideNumeric, (d, e) => ((PagerControl) d).UpdateCore(false)));
            ShowNumericButtonsProperty = DependencyPropertyManager.Register("ShowNumericButtons", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowDisabledButtonsProperty = DependencyPropertyManager.Register("ShowDisabledButtons", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowSummaryTextProperty = DependencyPropertyManager.Register("ShowSummaryText", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowFirstPageButtonProperty = DependencyPropertyManager.Register("ShowFirstPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((PagerControl) d).UpdateActualNavigationButtonsVisibility(ActualShowFirstPageButtonPropertyKey, ShowFirstPageButtonProperty, () => ((PagerControl) d).CanMoveToFirstPage())));
            ShowLastPageButtonProperty = DependencyPropertyManager.Register("ShowLastPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((PagerControl) d).UpdateActualNavigationButtonsVisibility(ActualShowLastPageButtonPropertyKey, ShowLastPageButtonProperty, () => ((PagerControl) d).CanMoveToLastPage())));
            ShowPrevPageButtonProperty = DependencyPropertyManager.Register("ShowPrevPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((PagerControl) d).UpdateActualNavigationButtonsVisibility(ActualShowPrevPageButtonPropertyKey, ShowPrevPageButtonProperty, () => ((PagerControl) d).CanMoveToPreviousPage())));
            ShowNextPageButtonProperty = DependencyPropertyManager.Register("ShowNextPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((PagerControl) d).UpdateActualNavigationButtonsVisibility(ActualShowNextPageButtonPropertyKey, ShowNextPageButtonProperty, () => ((PagerControl) d).CanMoveToNextPage())));
            ShowRefreshPageButtonProperty = DependencyPropertyManager.Register("ShowRefreshPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            AllowFirstPageButtonProperty = DependencyPropertyManager.Register("AllowFirstPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            AllowLastPageButtonProperty = DependencyPropertyManager.Register("AllowLastPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            AllowPrevPageButtonProperty = DependencyPropertyManager.Register("AllowPrevPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            AllowNextPageButtonProperty = DependencyPropertyManager.Register("AllowNextPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PageCountPropertyKey = DependencyPropertyManager.RegisterReadOnly("PageCount", typeof(int), ownerType, new FrameworkPropertyMetadata());
            PageCountProperty = PageCountPropertyKey.DependencyProperty;
            SummaryTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("SummaryText", typeof(string), ownerType, new FrameworkPropertyMetadata());
            SummaryTextProperty = SummaryTextPropertyKey.DependencyProperty;
            ActualShowFirstPageButtonPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowFirstPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata());
            ActualShowFirstPageButtonProperty = ActualShowFirstPageButtonPropertyKey.DependencyProperty;
            ActualShowLastPageButtonPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowLastPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata());
            ActualShowLastPageButtonProperty = ActualShowLastPageButtonPropertyKey.DependencyProperty;
            ActualShowPrevPageButtonPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowPrevPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata());
            ActualShowPrevPageButtonProperty = ActualShowPrevPageButtonPropertyKey.DependencyProperty;
            ActualShowNextPageButtonPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowNextPageButton", typeof(bool), ownerType, new FrameworkPropertyMetadata());
            ActualShowNextPageButtonProperty = ActualShowNextPageButtonPropertyKey.DependencyProperty;
            HasMorePagesProperty = DependencyPropertyManager.Register("HasMorePages", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((PagerControl) d).OnHasMorePagesChanged()));
            PagerControlProperty = DependencyPropertyManager.RegisterAttached("PagerControl", ownerType, ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        }

        public PagerControl()
        {
            this.SetDefaultStyleKey(typeof(PagerControl));
            this.FirstPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToFirstPage(), parameter => this.CanMoveToFirstPage(), true);
            this.LastPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToLastPage(), parameter => this.CanMoveToLastPage(), true);
            this.NextPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToNextPage(), parameter => this.CanMoveToNextPage(), true);
            this.PreviousPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToPreviousPage(), parameter => this.CanMoveToPreviousPage(), true);
            this.MoveToPageCommand = DelegateCommandFactory.Create<object>(parameter => this.MoveToPage((int) parameter), parameter => this.CanMoveToPage(parameter), true);
            Func<object, bool> canExecuteMethod = <>c.<>9__44_11;
            if (<>c.<>9__44_11 == null)
            {
                Func<object, bool> local1 = <>c.<>9__44_11;
                canExecuteMethod = <>c.<>9__44_11 = parameter => true;
            }
            this.RefreshPageCommand = DelegateCommandFactory.Create<object>(parameter => this.OnRefreshPage(), canExecuteMethod, true);
            SetPagerControl(this, this);
        }

        public override void BeginInit()
        {
            this.supportInitializeLocker.Lock();
            base.BeginInit();
        }

        protected bool CanChangePage() => 
            this.PageCount > 1;

        protected virtual bool CanMoveToFirstPage() => 
            this.AllowFirstPageButton ? (this.CanChangePage() ? (this.PageIndex > 0) : false) : false;

        protected virtual bool CanMoveToLastPage() => 
            this.AllowLastPageButton ? (this.CanChangePage() && (this.PageIndex < (this.PageCount - 1))) : false;

        protected virtual bool CanMoveToNextPage() => 
            this.AllowNextPageButton ? (!this.HasMorePages ? this.CanMoveToLastPage() : true) : false;

        protected virtual bool CanMoveToPage(object value) => 
            (value is int) && (((int) value) >= 0);

        protected virtual bool CanMoveToPreviousPage() => 
            this.AllowPrevPageButton ? this.CanMoveToFirstPage() : false;

        protected void CheckPageCount()
        {
            if (this.HasMorePages)
            {
                this.PageCount = Math.Max(this.PageCount, this.PageIndex + 1);
            }
        }

        protected object CoercePageIndex(int value)
        {
            value = this.CoercePositiveValue(value, 0);
            int coerceValue = (int) DependencyObjectHelper.GetCoerceValue(this, PageIndexProperty);
            return (((coerceValue == value) || !this.RaisePageIndexChanging(coerceValue, value)) ? coerceValue : value);
        }

        protected int CoercePositiveValue(int value, int minValue = 0) => 
            Math.Max(minValue, value);

        public override void EndInit()
        {
            base.EndInit();
            this.supportInitializeLocker.Unlock();
            this.supportInitializeLocker.DoIfNotLocked(() => this.UpdateCore(true));
        }

        private List<PageControlNumericItem> GetNumberItemsControlSource() => 
            (this.NumericButtonCount >= 1) ? (((this.EllipsisMode == PagerControlEllipsisMode.None) || !this.CanChangePage()) ? this.GetNumberItemsWithOutterEllipsis(false) : (((this.EllipsisMode == PagerControlEllipsisMode.OutsideNumeric) || this.HasMorePages) ? this.GetNumberItemsWithOutterEllipsis(true) : this.GetNumberItemsWithInnerEllipsis())) : null;

        protected List<PageControlNumericItem> GetNumberItemsWithInnerEllipsis()
        {
            if (this.NumericButtonCount < 3)
            {
                return this.GetNumberItemsWithOutterEllipsis(true);
            }
            List<PageControlNumericItem> list = new List<PageControlNumericItem>();
            int num = this.NumericButtonCount / 3;
            int num2 = (this.NumericButtonCount - num) - num;
            int num3 = 0;
            int num4 = num3 + num;
            int num5 = (this.PageIndex - (num2 / 2)) + (1 - (num2 % 2));
            int num6 = num5 + num2;
            int num7 = this.PageCount - num;
            int num8 = num7 + num;
            bool flag = true;
            bool flag2 = true;
            if ((num4 + 1) > num5)
            {
                flag = false;
                num5 = num3;
                num6 = (num5 + num) + num2;
            }
            if ((num7 - 1) < num6)
            {
                flag2 = false;
                num6 = num8;
                num5 = (num6 - num) - num2;
            }
            if (flag)
            {
                int num9 = num3;
                while (true)
                {
                    if (num9 >= num4)
                    {
                        PageControlNumericItem item2 = new PageControlNumericItem();
                        item2.ShowEllipsis = true;
                        list.Add(item2);
                        break;
                    }
                    PageControlNumericItem item = new PageControlNumericItem();
                    item.Number = num9;
                    item.IsSelected = this.PageIndex == num9;
                    list.Add(item);
                    num9++;
                }
            }
            for (int i = num5; i < num6; i++)
            {
                PageControlNumericItem item = new PageControlNumericItem();
                item.Number = i;
                item.IsSelected = this.PageIndex == i;
                list.Add(item);
            }
            if (flag2)
            {
                PageControlNumericItem item = new PageControlNumericItem();
                item.ShowEllipsis = true;
                list.Add(item);
                for (int j = num7; j < num8; j++)
                {
                    PageControlNumericItem item5 = new PageControlNumericItem();
                    item5.Number = j;
                    item5.IsSelected = this.PageIndex == j;
                    list.Add(item5);
                }
            }
            return list;
        }

        protected List<PageControlNumericItem> GetNumberItemsWithOutterEllipsis(bool ellipsis = true)
        {
            List<PageControlNumericItem> list = new List<PageControlNumericItem>();
            if (this.PageCount <= this.NumericButtonCount)
            {
                for (int i = 0; i < this.PageCount; i++)
                {
                    PageControlNumericItem item = new PageControlNumericItem();
                    item.Number = i;
                    item.IsSelected = this.PageIndex == i;
                    list.Add(item);
                }
            }
            else
            {
                int num2 = (this.PageIndex - (this.NumericButtonCount / 2)) + (1 - (this.NumericButtonCount % 2));
                int pageCount = num2 + this.NumericButtonCount;
                if (num2 < 0)
                {
                    num2 = 0;
                    pageCount = num2 + this.NumericButtonCount;
                }
                if (pageCount > this.PageCount)
                {
                    pageCount = this.PageCount;
                    num2 = pageCount - this.NumericButtonCount;
                }
                if (ellipsis && (num2 > 0))
                {
                    PageControlNumericItem item = new PageControlNumericItem();
                    item.ShowEllipsis = true;
                    list.Add(item);
                }
                int num4 = num2;
                while (true)
                {
                    if (num4 >= pageCount)
                    {
                        if (ellipsis && (pageCount < this.PageCount))
                        {
                            PageControlNumericItem item4 = new PageControlNumericItem();
                            item4.ShowEllipsis = true;
                            list.Add(item4);
                        }
                        break;
                    }
                    PageControlNumericItem item = new PageControlNumericItem();
                    item.Number = num4;
                    item.IsSelected = this.PageIndex == num4;
                    list.Add(item);
                    num4++;
                }
            }
            return list;
        }

        public static PagerControl GetPagerControl(DependencyObject obj) => 
            (PagerControl) obj.GetValue(PagerControlProperty);

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
            if ((this.PageIndex < (this.PageCount - 1)) || this.HasMorePages)
            {
                this.PageIndex++;
            }
        }

        public void MoveToPage(int value)
        {
            this.PageIndex = value;
        }

        public void MoveToPreviousPage()
        {
            this.PageIndex--;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.NumberItemsControl = base.GetTemplateChild("PART_NumberItemsControl") as ItemsControl;
            this.UpdateCore(true);
        }

        protected virtual void OnHasMorePagesChanged()
        {
            if (this.HasMorePages)
            {
                this.UpdateCore(true);
            }
        }

        protected virtual void OnPageIndexChanged(int oldValue, int newValue)
        {
            this.CheckPageCount();
            this.UpdateCore(false);
            this.ScrollCurrentItemIntoView();
            this.RaisePageIndexChanged(oldValue, newValue);
        }

        protected virtual void OnPageSizeChanged()
        {
            this.UpdateCore(true);
            if (this.PageIndex > (this.PageCount - 1))
            {
                this.PageIndex = this.PageCount - 1;
            }
        }

        protected virtual void OnRefreshPage()
        {
            if (this.RefreshPage != null)
            {
                this.RefreshPage(this, EventArgs.Empty);
            }
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
            if (this.PageIndexChanging == null)
            {
                return true;
            }
            DataPagerPageIndexChangingEventArgs e = new DataPagerPageIndexChangingEventArgs(oldValue, newValue);
            this.PageIndexChanging(this, e);
            return !e.IsCancel;
        }

        protected void ScrollCurrentItemIntoView()
        {
            if (this.NumberItemsControl != null)
            {
                List<PageControlNumericItem> itemsSource = this.NumberItemsControl.ItemsSource as List<PageControlNumericItem>;
                if (itemsSource != null)
                {
                    Predicate<PageControlNumericItem> match = <>c.<>9__155_0;
                    if (<>c.<>9__155_0 == null)
                    {
                        Predicate<PageControlNumericItem> local1 = <>c.<>9__155_0;
                        match = <>c.<>9__155_0 = item => item.IsSelected;
                    }
                    PageControlNumericItem item = itemsSource.Find(match);
                    if (item != null)
                    {
                        FrameworkElement element = this.NumberItemsControl.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                        if (element != null)
                        {
                            element.BringIntoView();
                        }
                    }
                }
            }
        }

        public static void SetPagerControl(DependencyObject obj, PagerControl value)
        {
            obj.SetValue(PagerControlProperty, value);
        }

        [Browsable(false)]
        public void Update()
        {
            this.UpdateCore(true);
        }

        private void UpdateActualNavigationButtonsVisibility(DependencyPropertyKey propertyKey, DependencyProperty property, Func<bool> getIsEnabled)
        {
            this.SetValue(propertyKey, !((bool) base.GetValue(property)) ? ((object) 0) : (this.ShowDisabledButtons ? ((object) 1) : (this.ShowDisabledButtons ? ((object) 0) : ((object) getIsEnabled()))));
        }

        protected virtual void UpdateCore(bool updatePageCount = false)
        {
            if (!this.IsInitializing)
            {
                if (updatePageCount)
                {
                    this.UpdatePageCount();
                }
                if (this.NumberItemsControl != null)
                {
                    this.NumberItemsControl.ItemsSource = this.GetNumberItemsControlSource();
                }
                this.UpdateNavigationButtonsVisibility();
                this.UpdateSummaryText();
            }
        }

        protected void UpdateNavigationButtonsVisibility()
        {
            this.UpdateActualNavigationButtonsVisibility(ActualShowFirstPageButtonPropertyKey, ShowFirstPageButtonProperty, () => this.CanMoveToFirstPage());
            this.UpdateActualNavigationButtonsVisibility(ActualShowLastPageButtonPropertyKey, ShowLastPageButtonProperty, () => this.CanMoveToLastPage());
            this.UpdateActualNavigationButtonsVisibility(ActualShowPrevPageButtonPropertyKey, ShowPrevPageButtonProperty, () => this.CanMoveToPreviousPage());
            this.UpdateActualNavigationButtonsVisibility(ActualShowNextPageButtonPropertyKey, ShowNextPageButtonProperty, () => this.CanMoveToNextPage());
        }

        protected void UpdatePageCount()
        {
            if (!this.IsInitializing)
            {
                this.PageCount = (this.ItemCount > 0) ? ((this.ItemCount / this.PageSize) + (((this.ItemCount % this.PageSize) != 0) ? 1 : 0)) : 1;
                this.CheckPageCount();
            }
        }

        protected virtual void UpdateSummaryText()
        {
            this.SummaryText = $"{EditorLocalizer.Active.GetLocalizedString(EditorStringId.Page)} {(this.PageIndex + 1)}" + " " + string.Format(EditorLocalizer.Active.GetLocalizedString(EditorStringId.Of), this.PageCount);
        }

        public ICommand FirstPageCommand { get; private set; }

        public ICommand LastPageCommand { get; private set; }

        public ICommand NextPageCommand { get; private set; }

        public ICommand PreviousPageCommand { get; private set; }

        public ICommand MoveToPageCommand { get; private set; }

        public ICommand RefreshPageCommand { get; private set; }

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

        public int ItemCount
        {
            get => 
                (int) base.GetValue(ItemCountProperty);
            set => 
                base.SetValue(ItemCountProperty, value);
        }

        public int NumericButtonCount
        {
            get => 
                (int) base.GetValue(NumericButtonCountProperty);
            set => 
                base.SetValue(NumericButtonCountProperty, value);
        }

        public int PageCount
        {
            get => 
                (int) base.GetValue(PageCountProperty);
            protected set => 
                base.SetValue(PageCountPropertyKey, value);
        }

        public PagerControlEllipsisMode EllipsisMode
        {
            get => 
                (PagerControlEllipsisMode) base.GetValue(EllipsisModeProperty);
            set => 
                base.SetValue(EllipsisModeProperty, value);
        }

        public bool ShowNumericButtons
        {
            get => 
                (bool) base.GetValue(ShowNumericButtonsProperty);
            set => 
                base.SetValue(ShowNumericButtonsProperty, value);
        }

        public bool ShowDisabledButtons
        {
            get => 
                (bool) base.GetValue(ShowDisabledButtonsProperty);
            set => 
                base.SetValue(ShowDisabledButtonsProperty, value);
        }

        public bool ShowSummaryText
        {
            get => 
                (bool) base.GetValue(ShowSummaryTextProperty);
            set => 
                base.SetValue(ShowSummaryTextProperty, value);
        }

        public string SummaryText
        {
            get => 
                (string) base.GetValue(SummaryTextProperty);
            protected set => 
                base.SetValue(SummaryTextPropertyKey, value);
        }

        public bool ShowFirstPageButton
        {
            get => 
                (bool) base.GetValue(ShowFirstPageButtonProperty);
            set => 
                base.SetValue(ShowFirstPageButtonProperty, value);
        }

        public bool ShowLastPageButton
        {
            get => 
                (bool) base.GetValue(ShowLastPageButtonProperty);
            set => 
                base.SetValue(ShowLastPageButtonProperty, value);
        }

        public bool ShowNextPageButton
        {
            get => 
                (bool) base.GetValue(ShowNextPageButtonProperty);
            set => 
                base.SetValue(ShowNextPageButtonProperty, value);
        }

        public bool ShowPrevPageButton
        {
            get => 
                (bool) base.GetValue(ShowPrevPageButtonProperty);
            set => 
                base.SetValue(ShowPrevPageButtonProperty, value);
        }

        public bool ShowRefreshPageButton
        {
            get => 
                (bool) base.GetValue(ShowRefreshPageButtonProperty);
            set => 
                base.SetValue(ShowRefreshPageButtonProperty, value);
        }

        public bool AllowNextPageButton
        {
            get => 
                (bool) base.GetValue(AllowNextPageButtonProperty);
            set => 
                base.SetValue(AllowNextPageButtonProperty, value);
        }

        public bool AllowPrevPageButton
        {
            get => 
                (bool) base.GetValue(AllowPrevPageButtonProperty);
            set => 
                base.SetValue(AllowPrevPageButtonProperty, value);
        }

        public bool AllowFirstPageButton
        {
            get => 
                (bool) base.GetValue(AllowFirstPageButtonProperty);
            set => 
                base.SetValue(AllowFirstPageButtonProperty, value);
        }

        public bool AllowLastPageButton
        {
            get => 
                (bool) base.GetValue(AllowLastPageButtonProperty);
            set => 
                base.SetValue(AllowLastPageButtonProperty, value);
        }

        public bool ActualShowFirstPageButton
        {
            get => 
                (bool) base.GetValue(ActualShowFirstPageButtonProperty);
            protected set => 
                base.SetValue(ActualShowFirstPageButtonPropertyKey, value);
        }

        public bool ActualShowLastPageButton
        {
            get => 
                (bool) base.GetValue(ActualShowLastPageButtonProperty);
            protected set => 
                base.SetValue(ActualShowLastPageButtonPropertyKey, value);
        }

        public bool ActualShowPrevPageButton
        {
            get => 
                (bool) base.GetValue(ActualShowPrevPageButtonProperty);
            protected set => 
                base.SetValue(ActualShowPrevPageButtonPropertyKey, value);
        }

        public bool ActualShowNextPageButton
        {
            get => 
                (bool) base.GetValue(ActualShowNextPageButtonProperty);
            protected set => 
                base.SetValue(ActualShowNextPageButtonPropertyKey, value);
        }

        public bool HasMorePages
        {
            get => 
                (bool) base.GetValue(HasMorePagesProperty);
            set => 
                base.SetValue(HasMorePagesProperty, value);
        }

        protected ItemsControl NumberItemsControl { get; private set; }

        protected bool IsInitializing =>
            this.supportInitializeLocker.IsLocked;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PagerControl.<>c <>9 = new PagerControl.<>c();
            public static Func<object, bool> <>9__44_11;
            public static Predicate<PageControlNumericItem> <>9__155_0;

            internal void <.cctor>b__40_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).OnPageIndexChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal object <.cctor>b__40_1(DependencyObject d, object value) => 
                ((PagerControl) d).CoercePageIndex((int) value);

            internal void <.cctor>b__40_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).UpdateActualNavigationButtonsVisibility(PagerControl.ActualShowLastPageButtonPropertyKey, PagerControl.ShowLastPageButtonProperty, () => ((PagerControl) d).CanMoveToLastPage());
            }

            internal void <.cctor>b__40_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).UpdateActualNavigationButtonsVisibility(PagerControl.ActualShowPrevPageButtonPropertyKey, PagerControl.ShowPrevPageButtonProperty, () => ((PagerControl) d).CanMoveToPreviousPage());
            }

            internal void <.cctor>b__40_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).UpdateActualNavigationButtonsVisibility(PagerControl.ActualShowNextPageButtonPropertyKey, PagerControl.ShowNextPageButtonProperty, () => ((PagerControl) d).CanMoveToNextPage());
            }

            internal void <.cctor>b__40_17(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).OnHasMorePagesChanged();
            }

            internal void <.cctor>b__40_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).OnPageSizeChanged();
            }

            internal object <.cctor>b__40_3(DependencyObject d, object value) => 
                ((PagerControl) d).CoercePositiveValue((int) value, 1);

            internal void <.cctor>b__40_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).OnPageSizeChanged();
            }

            internal object <.cctor>b__40_5(DependencyObject d, object value) => 
                ((PagerControl) d).CoercePositiveValue((int) value, 0);

            internal void <.cctor>b__40_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).UpdateCore(false);
            }

            internal object <.cctor>b__40_7(DependencyObject d, object value) => 
                ((PagerControl) d).CoercePositiveValue((int) value, 0);

            internal void <.cctor>b__40_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).UpdateCore(false);
            }

            internal void <.cctor>b__40_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PagerControl) d).UpdateActualNavigationButtonsVisibility(PagerControl.ActualShowFirstPageButtonPropertyKey, PagerControl.ShowFirstPageButtonProperty, () => ((PagerControl) d).CanMoveToFirstPage());
            }

            internal bool <.ctor>b__44_11(object parameter) => 
                true;

            internal bool <ScrollCurrentItemIntoView>b__155_0(PageControlNumericItem item) => 
                item.IsSelected;
        }
    }
}

