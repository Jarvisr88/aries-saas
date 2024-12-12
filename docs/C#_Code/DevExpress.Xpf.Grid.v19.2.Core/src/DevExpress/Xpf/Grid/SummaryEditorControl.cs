namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Summary;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Shapes;

    public class SummaryEditorControl : UserControl, IDialogContent, IComponentConnector
    {
        private DataViewBase view;
        private GridSummaryItemsEditorController summaryController;
        private SummaryEditorOrderListHelper orderListHelper;
        private Dictionary<ISummaryItem, string> displayFormatListSource = new Dictionary<ISummaryItem, string>();
        internal DXTabControl tbControl;
        internal DXTabItem tabItems;
        internal ListBox itemList;
        internal StackPanel summaryTypePanel;
        internal CheckEdit maxCheck;
        internal CheckEdit minCheck;
        internal CheckEdit averageCheck;
        internal CheckEdit sumCheck;
        internal CheckEdit countCheck;
        internal CheckEdit globalCountCheck;
        internal DXTabItem tabOrder;
        internal Grid gridTabOrder;
        internal TextBlock tbLeftSideCaption;
        internal ListBox orderList;
        internal Button upButton;
        internal Path upPath;
        internal Button downButton;
        internal Path downPath;
        internal Button leftButton;
        internal Path leftPath;
        internal Button rightButton;
        internal Path rightPath;
        internal Grid gridRightSide;
        internal TextBlock tbRightSideCaption;
        internal ListBox orderListRight;
        internal DisplayFormatTextControl displayFormatControl;
        private bool _contentLoaded;

        public SummaryEditorControl(GridSummaryItemsEditorController controller, DevExpress.Xpf.Grid.SummaryEditorType summaryEditorType)
        {
            this.summaryController = controller;
            this.SummaryEditorType = summaryEditorType;
            this.orderListHelper = new SummaryEditorOrderListHelper(this.Controller);
            this.SetView();
            this.InitializeComponent();
            this.itemList.ItemsSource = this.Controller.UIItems;
            this.InitializeUIElements((this.Controller.Owner as GridSummaryHelper).view);
            this.UpdateOrderItems();
            this.UpdateButtonsState();
            this.itemList.SelectedIndex = 0;
            base.DataContext = this;
        }

        private void ApplyDisplayFormats()
        {
            foreach (ISummaryItem item in this.Controller.Items)
            {
                if (this.displayFormatListSource.ContainsKey(item))
                {
                    item.DisplayFormat = this.displayFormatListSource[item];
                }
            }
        }

        bool IDialogContent.CanCloseWithOKResult() => 
            true;

        void IDialogContent.OnApply()
        {
            throw new NotImplementedException();
        }

        void IDialogContent.OnOk()
        {
            this.orderListHelper.ApplyAlignments();
            this.ApplyDisplayFormats();
            this.Controller.Apply();
        }

        private void displayFormatControl_CurrentDisplayFormatChanged(object sender, EditValueChangedEventArgs e)
        {
            if (this.ActiveOrderUIItem != null)
            {
                ISummaryItem key = this.ActiveOrderUIItem.Item;
                string newValue = (string) e.NewValue;
                if (key.DisplayFormat == newValue)
                {
                    if (this.displayFormatListSource.ContainsKey(key))
                    {
                        this.displayFormatListSource.Remove(key);
                    }
                }
                else if (this.displayFormatListSource.ContainsKey(key))
                {
                    this.displayFormatListSource[key] = newValue;
                }
                else
                {
                    this.displayFormatListSource.Add(key, newValue);
                }
            }
        }

        private SummaryEditorOrderUIItem GetActiveOrderUIItem() => 
            (this.orderList.SelectedItem == null) ? ((SummaryEditorOrderUIItem) this.orderListRight.SelectedItem) : ((SummaryEditorOrderUIItem) this.orderList.SelectedItem);

        private Visibility GetAlignmentListBoxVisibility(IList addedItems)
        {
            GridSummaryItemAlignment alignment;
            if (this.SummaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.TotalSummaryPanel)
            {
                return Visibility.Collapsed;
            }
            if (addedItems.Count == 0)
            {
                return Visibility.Collapsed;
            }
            IAlignmentItem alignmentItem = ((SummaryEditorOrderUIItem) addedItems[0]).Item as IAlignmentItem;
            if (alignmentItem == null)
            {
                return Visibility.Collapsed;
            }
            if ((this.Controller.Owner as GridSummaryHelper).view.ShowFixedTotalSummary)
            {
                return Visibility.Visible;
            }
            if (!this.orderListHelper.TryGetAlignment(alignmentItem, out alignment))
            {
                alignment = alignmentItem.Alignment;
            }
            return ((alignment != GridSummaryItemAlignment.Default) ? Visibility.Visible : Visibility.Collapsed);
        }

        private Type GetColumnValueType(DevExpress.Xpf.Grid.SummaryItemBase item) => 
            ((item.SummaryType == SummaryItemType.Count) || string.IsNullOrEmpty(item.FieldName)) ? typeof(int) : this.View.GetColumnType(item.FieldName, null);

        private string GetDisplayFormatEditSettigsModifier(ISummaryItem item)
        {
            if (this.View == null)
            {
                return string.Empty;
            }
            ColumnBase base2 = this.View.ColumnsCore[item.FieldName];
            return (((base2 == null) || (base2.ActualEditSettings == null)) ? string.Empty : base2.DisplayFormat);
        }

        internal string GetNullValueDisplayFormat(ISummaryItem item)
        {
            string nullValueDisplayFormatFromColumn = this.GetNullValueDisplayFormatFromColumn(item);
            string displayFormatEditSettigsModifier = this.GetDisplayFormatEditSettigsModifier(item);
            return (!string.IsNullOrEmpty(displayFormatEditSettigsModifier) ? string.Format(string.Format(nullValueDisplayFormatFromColumn, "{0}", "{1}"), displayFormatEditSettigsModifier, "{1}") : nullValueDisplayFormatFromColumn);
        }

        private string GetNullValueDisplayFormatFromColumn(ISummaryItem item)
        {
            DevExpress.Xpf.Grid.SummaryItemBase base2 = item as DevExpress.Xpf.Grid.SummaryItemBase;
            if (base2 == null)
            {
                return string.Empty;
            }
            DevExpress.Xpf.Grid.SummaryEditorType summaryEditorType = this.SummaryEditorType;
            return ((summaryEditorType == DevExpress.Xpf.Grid.SummaryEditorType.TotalSummaryPanel) ? base2.GetFooterDisplayFormat(DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType.Total) : ((summaryEditorType == DevExpress.Xpf.Grid.SummaryEditorType.GroupSummary) ? base2.GetGroupDisplayFormat() : ((string.IsNullOrEmpty(base2.FieldName) || (base2.ActualShowInColumn == base2.FieldName)) ? base2.GetFooterDisplayFormatSameColumn(DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType.Group) : base2.GetFooterDisplayFormat(DevExpress.Xpf.Grid.SummaryItemBase.ColumnSummaryType.Group))));
        }

        private bool GetSummaryTypeCheckBoxState(SummaryItemType summaryType) => 
            this.ActiveUIItem[summaryType];

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Grid.v19.2.Core;component/grid/gridsummaryeditor/summaryeditorcontrol.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void InitializeCountCheckEdit()
        {
            if ((this.SummaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.GroupSummary) && ((this.View != null) && !this.View.ActualAllowCountTotalSummary))
            {
                this.countCheck.Visibility = Visibility.Collapsed;
                this.globalCountCheck.Visibility = Visibility.Collapsed;
            }
            else if (this.IsGlobalCountCheckEnabled())
            {
                this.InitializeGlobalCountSummaryCheckEdit();
                this.globalCountCheck.IsChecked = new bool?(this.Controller.HasFixedCountSummary());
                this.countCheck.Visibility = Visibility.Collapsed;
                this.globalCountCheck.Visibility = Visibility.Visible;
            }
        }

        private void InitializeGlobalCountSummaryCheckEdit()
        {
            this.globalCountCheck.Content = GridSummaryItemsEditorController.GetGlobalCountSummaryName();
            this.globalCountCheck.Tag = SummaryItemType.Count;
        }

        private void InitializeOrderLists()
        {
            if (this.SummaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.TotalSummaryPanel)
            {
                this.gridTabOrder.ColumnDefinitions.Remove(this.gridTabOrder.ColumnDefinitions[2]);
            }
            else
            {
                this.rightButton.Visibility = Visibility.Visible;
                this.leftButton.Visibility = Visibility.Visible;
                this.tbLeftSideCaption.Visibility = Visibility.Visible;
                this.gridRightSide.Visibility = Visibility.Visible;
            }
        }

        private void InitializeSummaryCheckEdit(CheckEdit edit, SummaryItemType type)
        {
            edit.Content = GridSummaryItemsEditorController.GetNameBySummaryType(type);
            edit.Tag = type;
        }

        private void InitializeUIElements(DataViewBase ownerView)
        {
            this.tabItems.Header = ownerView.GetLocalizedString(GridControlStringId.SummaryEditorFormItemsTabCaption);
            this.tabOrder.Header = (this.SummaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.TotalSummaryPanel) ? ownerView.GetLocalizedString(GridControlStringId.SummaryEditorFormOrderTabCaption) : ownerView.GetLocalizedString(GridControlStringId.SummaryEditorFormOrderAndAlignmentTabCaption);
            this.tbLeftSideCaption.Text = ownerView.GetLocalizedString(GridControlStringId.SummaryEditorFormOrderLeftSide);
            this.tbRightSideCaption.Text = ownerView.GetLocalizedString(GridControlStringId.SummaryEditorFormOrderRightSide);
            this.InitializeSummaryCheckEdit(this.maxCheck, SummaryItemType.Max);
            this.InitializeSummaryCheckEdit(this.minCheck, SummaryItemType.Min);
            this.InitializeSummaryCheckEdit(this.averageCheck, SummaryItemType.Average);
            this.InitializeSummaryCheckEdit(this.sumCheck, SummaryItemType.Sum);
            this.InitializeSummaryCheckEdit(this.countCheck, SummaryItemType.Count);
            this.InitializeCountCheckEdit();
            this.InitializeOrderLists();
        }

        private bool IsForseShowColumnName() => 
            this.IsOneCountCase();

        private bool IsGlobalCountCheckEnabled() => 
            this.IsOneCountCase();

        private bool IsOneCountCase()
        {
            DevExpress.Xpf.Grid.SummaryEditorType summaryEditorType = this.SummaryEditorType;
            if (summaryEditorType == DevExpress.Xpf.Grid.SummaryEditorType.TotalSummaryPanel)
            {
                return true;
            }
            if (summaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.GroupSummary)
            {
                return false;
            }
            IGroupSummaryDisplayMode view = this.view as IGroupSummaryDisplayMode;
            return ((view != null) ? (view.GroupSummaryDisplayMode != GroupSummaryDisplayMode.AlignByColumns) : false);
        }

        internal void OnAlignmentButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ActiveOrderUIItem != null)
            {
                GridSummaryItemAlignment alignment = (sender == this.leftButton) ? GridSummaryItemAlignment.Left : GridSummaryItemAlignment.Right;
                GridSummaryItemAlignment? summaryItemAlignment = this.orderListHelper.GetSummaryItemAlignment(this.ActiveOrderUIItem.Item);
                GridSummaryItemAlignment alignment2 = alignment;
                if (!((((GridSummaryItemAlignment) summaryItemAlignment.GetValueOrDefault()) == alignment2) ? (summaryItemAlignment != null) : false))
                {
                    this.orderListHelper.SetSummaryItemAlignment(this.ActiveOrderUIItem.Item, alignment);
                    ListBox list = (sender == this.leftButton) ? this.orderListRight : this.orderList;
                    this.UpdateOrderItems();
                    this.SetOrderlistSelectedIndex(list, list.SelectedIndex);
                }
            }
        }

        private void OnCountSummaryTypeChanged(object sender, RoutedEventArgs e)
        {
            this.Controller.SetSummary("", SummaryItemType.Count, this.globalCountCheck.IsChecked.Value);
            this.UpdateOrderItems();
        }

        private void OnItemListSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.ActiveUIItem != null)
            {
                Func<DataViewBase, DataControlBase> evaluator = <>c.<>9__27_0;
                if (<>c.<>9__27_0 == null)
                {
                    Func<DataViewBase, DataControlBase> local1 = <>c.<>9__27_0;
                    evaluator = <>c.<>9__27_0 = v => v.DataControl;
                }
                DataControlBase input = this.View.With<DataViewBase, DataControlBase>(evaluator);
                ColumnBase base3 = input.With<DataControlBase, ColumnBase>(dc => dc.ColumnsCore[this.ActiveUIItem.FieldName]);
                bool flag = ((input != null) && (input.DataProviderBase != null)) && input.DataProviderBase.IsVirtualSource;
                foreach (UIElement element in this.summaryTypePanel.Children)
                {
                    CheckEdit edit = element as CheckEdit;
                    if (element != null)
                    {
                        SummaryItemType tag = (SummaryItemType) edit.Tag;
                        if ((tag != SummaryItemType.Count) || (this.SummaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.TotalSummaryPanel))
                        {
                            if (this.SummaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.GroupSummary)
                            {
                                edit.Visibility = ((base3 == null) || base3.ActualAllowTotalSummary(tag)) ? Visibility.Visible : Visibility.Collapsed;
                            }
                            edit.IsChecked = new bool?(this.GetSummaryTypeCheckBoxState(tag));
                            edit.IsEnabled = flag || this.ActiveUIItem.CanDoSummary(tag);
                        }
                    }
                }
            }
        }

        private void OnMoveButtonClick(object sender, RoutedEventArgs e)
        {
            SummaryEditorOrderUIItem activeOrderUIItem = this.ActiveOrderUIItem;
            ListBox box = this.orderList.Items.Contains(activeOrderUIItem) ? this.orderList : this.orderListRight;
            int index = box.Items.IndexOf(activeOrderUIItem);
            if (activeOrderUIItem != null)
            {
                if (ReferenceEquals(sender as Button, this.upButton))
                {
                    activeOrderUIItem.MoveUp();
                    index--;
                }
                else
                {
                    activeOrderUIItem.MoveDown();
                    index++;
                }
                this.UpdateOrderItems();
                this.UpdateButtonsState();
                box.SelectedIndex = index;
                box.ScrollIntoView(box.Items[index]);
            }
        }

        private void OnOrderListSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.displayFormatControl.CurrentDisplayFormat = null;
            this.UpdateButtonsState();
            if (e.AddedItems.Count != 0)
            {
                if (sender == this.orderList)
                {
                    this.orderListRight.SelectedItem = null;
                }
                else
                {
                    this.orderList.SelectedItem = null;
                }
                ISummaryItem item = ((SummaryEditorOrderUIItem) e.AddedItems[0]).Item;
                this.SetCurrentDisplayFormat(item);
            }
        }

        private void OnSummaryTypeChanged(object sender, RoutedEventArgs e)
        {
            if (this.ActiveUIItem != null)
            {
                CheckEdit edit = sender as CheckEdit;
                SummaryItemType tag = (SummaryItemType) edit.Tag;
                this.Controller[this.ActiveUIItem.FieldName][tag] = (edit.IsChecked != null) ? edit.IsChecked.Value : false;
                this.UpdateOrderItems();
            }
        }

        private void SetCurrentDisplayFormat(ISummaryItem item)
        {
            this.displayFormatControl.NullValueDisplayFormat = this.GetNullValueDisplayFormat(item);
            this.displayFormatControl.SecondParameterName = item.FieldName;
            this.displayFormatControl.SourceValueType = this.GetColumnValueType((DevExpress.Xpf.Grid.SummaryItemBase) item);
            this.displayFormatControl.CurrentDisplayFormat = this.displayFormatListSource.ContainsKey(item) ? this.displayFormatListSource[item] : item.DisplayFormat;
        }

        private void SetOrderlistSelectedIndex(ListBox list, int index)
        {
            list.SelectedIndex = (index < list.Items.Count) ? index : (list.Items.Count - 1);
        }

        private void SetView()
        {
            GridSummaryHelper owner = this.Controller.Owner as GridSummaryHelper;
            this.view = owner.view;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.tbControl = (DXTabControl) target;
                    return;

                case 2:
                    this.tabItems = (DXTabItem) target;
                    return;

                case 3:
                    this.itemList = (ListBox) target;
                    this.itemList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OnItemListSelectionChanged);
                    return;

                case 4:
                    this.summaryTypePanel = (StackPanel) target;
                    return;

                case 5:
                    this.maxCheck = (CheckEdit) target;
                    this.maxCheck.Unchecked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    this.maxCheck.Checked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    return;

                case 6:
                    this.minCheck = (CheckEdit) target;
                    this.minCheck.Unchecked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    this.minCheck.Checked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    return;

                case 7:
                    this.averageCheck = (CheckEdit) target;
                    this.averageCheck.Unchecked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    this.averageCheck.Checked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    return;

                case 8:
                    this.sumCheck = (CheckEdit) target;
                    this.sumCheck.Unchecked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    this.sumCheck.Checked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    return;

                case 9:
                    this.countCheck = (CheckEdit) target;
                    this.countCheck.Unchecked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    this.countCheck.Checked += new RoutedEventHandler(this.OnSummaryTypeChanged);
                    return;

                case 10:
                    this.globalCountCheck = (CheckEdit) target;
                    this.globalCountCheck.Unchecked += new RoutedEventHandler(this.OnCountSummaryTypeChanged);
                    this.globalCountCheck.Checked += new RoutedEventHandler(this.OnCountSummaryTypeChanged);
                    return;

                case 11:
                    this.tabOrder = (DXTabItem) target;
                    return;

                case 12:
                    this.gridTabOrder = (Grid) target;
                    return;

                case 13:
                    this.tbLeftSideCaption = (TextBlock) target;
                    return;

                case 14:
                    this.orderList = (ListBox) target;
                    this.orderList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OnOrderListSelectionChanged);
                    return;

                case 15:
                    this.upButton = (Button) target;
                    this.upButton.Click += new RoutedEventHandler(this.OnMoveButtonClick);
                    return;

                case 0x10:
                    this.upPath = (Path) target;
                    return;

                case 0x11:
                    this.downButton = (Button) target;
                    this.downButton.Click += new RoutedEventHandler(this.OnMoveButtonClick);
                    return;

                case 0x12:
                    this.downPath = (Path) target;
                    return;

                case 0x13:
                    this.leftButton = (Button) target;
                    this.leftButton.Click += new RoutedEventHandler(this.OnAlignmentButtonClick);
                    return;

                case 20:
                    this.leftPath = (Path) target;
                    return;

                case 0x15:
                    this.rightButton = (Button) target;
                    this.rightButton.Click += new RoutedEventHandler(this.OnAlignmentButtonClick);
                    return;

                case 0x16:
                    this.rightPath = (Path) target;
                    return;

                case 0x17:
                    this.gridRightSide = (Grid) target;
                    return;

                case 0x18:
                    this.tbRightSideCaption = (TextBlock) target;
                    return;

                case 0x19:
                    this.orderListRight = (ListBox) target;
                    this.orderListRight.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OnOrderListSelectionChanged);
                    return;

                case 0x1a:
                    this.displayFormatControl = (DisplayFormatTextControl) target;
                    this.displayFormatControl.CurrentDisplayFormatChanged += new EditValueChangedEventHandler(this.displayFormatControl_CurrentDisplayFormatChanged);
                    return;
            }
            this._contentLoaded = true;
        }

        private void UpdateButtonsState()
        {
            this.leftButton.IsEnabled = false;
            this.rightButton.IsEnabled = false;
            if (this.ActiveOrderUIItem == null)
            {
                this.upButton.IsEnabled = false;
                this.downButton.IsEnabled = false;
            }
            else
            {
                this.upButton.IsEnabled = this.ActiveOrderUIItem.CanUp;
                this.downButton.IsEnabled = this.ActiveOrderUIItem.CanDown;
                if (this.orderList.Items.Contains(this.ActiveOrderUIItem))
                {
                    this.rightButton.IsEnabled = true;
                }
                if (this.orderListRight.Items.Contains(this.ActiveOrderUIItem))
                {
                    this.leftButton.IsEnabled = true;
                }
            }
        }

        private void UpdateOrderItems()
        {
            this.displayFormatControl.CurrentDisplayFormat = null;
            List<SummaryEditorOrderUIItem> orderItems = this.Controller.CreateOrderItems();
            if (this.SummaryEditorType != DevExpress.Xpf.Grid.SummaryEditorType.TotalSummaryPanel)
            {
                this.orderList.ItemsSource = this.orderListHelper.GetOrderListSource(orderItems);
            }
            else
            {
                this.orderList.ItemsSource = this.orderListHelper.GetOrderListSource(orderItems, GridSummaryItemAlignment.Left);
                this.orderListRight.ItemsSource = this.orderListHelper.GetOrderListSource(orderItems, GridSummaryItemAlignment.Right);
            }
        }

        public DevExpress.Xpf.Grid.SummaryEditorType SummaryEditorType { get; set; }

        public GridSummaryItemsEditorController Controller =>
            this.summaryController;

        protected SummaryEditorUIItem ActiveUIItem =>
            this.itemList.SelectedItem as SummaryEditorUIItem;

        protected SummaryEditorOrderUIItem ActiveOrderUIItem =>
            this.GetActiveOrderUIItem();

        public DataViewBase View =>
            this.view;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SummaryEditorControl.<>c <>9 = new SummaryEditorControl.<>c();
            public static Func<DataViewBase, DataControlBase> <>9__27_0;

            internal DataControlBase <OnItemListSelectionChanged>b__27_0(DataViewBase v) => 
                v.DataControl;
        }
    }
}

