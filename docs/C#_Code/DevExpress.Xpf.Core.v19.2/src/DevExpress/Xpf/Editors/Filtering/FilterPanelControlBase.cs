namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public abstract class FilterPanelControlBase : Control, IWeakEventListener
    {
        public static readonly DependencyProperty ClearFilterCommandProperty;
        public static readonly DependencyProperty ShowFilterEditorCommandProperty;
        public static readonly DependencyProperty IsFilterEnabledProperty;
        public static readonly DependencyProperty IsCanEnableFilterProperty;
        public static readonly DependencyProperty AllowFilterEditorProperty;
        public static readonly DependencyProperty MRUFiltersProperty;
        public static readonly DependencyProperty ActiveFilterInfoProperty;
        public static readonly DependencyProperty AllowMRUFilterListProperty;
        public static readonly DependencyProperty FilterPanelContentProperty;
        protected ComboBoxEdit mruListCombo;

        static FilterPanelControlBase()
        {
            Type ownerType = typeof(FilterPanelControlBase);
            ClearFilterCommandProperty = DependencyPropertyManager.Register("ClearFilterCommand", typeof(ICommand), ownerType, new PropertyMetadata(null));
            ShowFilterEditorCommandProperty = DependencyPropertyManager.Register("ShowFilterEditorCommand", typeof(ICommand), ownerType, new PropertyMetadata(null));
            IsFilterEnabledProperty = DependencyPropertyManager.Register("IsFilterEnabled", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(FilterPanelControlBase.OnIsFilterEnabledChanged)));
            IsCanEnableFilterProperty = DependencyPropertyManager.Register("IsCanEnableFilter", typeof(bool), ownerType, new PropertyMetadata(true));
            AllowFilterEditorProperty = DependencyPropertyManager.Register("AllowFilterEditor", typeof(bool), ownerType, new PropertyMetadata(true));
            MRUFiltersProperty = DependencyPropertyManager.Register("MRUFilters", typeof(ReadOnlyObservableCollection<CriteriaOperatorInfo>), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(FilterPanelControlBase.OnMRUFiltersChanged)));
            ActiveFilterInfoProperty = DependencyPropertyManager.Register("ActiveFilterInfo", typeof(CriteriaOperatorInfo), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(FilterPanelControlBase.OnActiveFilterInfoChanged)));
            AllowMRUFilterListProperty = DependencyPropertyManager.Register("AllowMRUFilterList", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(FilterPanelControlBase.OAllowMRUFilterListChanged)));
            FilterPanelContentProperty = DependencyPropertyManager.Register("FilterPanelContent", typeof(object), ownerType);
        }

        public FilterPanelControlBase()
        {
            this.SetDefaultStyleKey(typeof(FilterPanelControlBase));
        }

        protected virtual void FilterPanelMRUComboBoxSelectedIndexChanged(object sender, RoutedEventArgs args)
        {
        }

        private ComboBoxEdit GetMRUListCombo() => 
            base.GetTemplateChild("PART_FilterPanelMRUComboBox") as ComboBoxEdit;

        protected CriteriaOperatorInfo GetSelectedFilter() => 
            (this.mruListCombo == null) ? null : (this.mruListCombo.SelectedItem as CriteriaOperatorInfo);

        private void mruListCombo_MouseEnter(object sender, MouseEventArgs e)
        {
            ComboBoxEdit edit = sender as ComboBoxEdit;
            if ((edit != null) && this.IsMRUComboPopupActive)
            {
                edit.TextDecorations = TextDecorations.Underline;
            }
        }

        private void mruListCombo_MouseLeave(object sender, MouseEventArgs e)
        {
            ComboBoxEdit edit = sender as ComboBoxEdit;
            if (edit != null)
            {
                edit.TextDecorations = null;
            }
        }

        private void mruListCombo_PopupClosed(object sender, ClosePopupEventArgs e)
        {
            this.OnMruComboBoxPopupClosed();
        }

        private static void OAllowMRUFilterListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FilterPanelControlBase) d).UpdateMRUComboBoxEditorButtonsVisibility();
        }

        private static void OnActiveFilterInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FilterPanelControlBase base2 = (FilterPanelControlBase) d;
            base2.UpdateMRUComboEditValue(e.NewValue as CriteriaOperatorInfo);
            base2.UpdateMRUComboBoxEditorButtonsVisibility();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.mruListCombo != null)
            {
                this.mruListCombo.SelectedIndexChanged -= new RoutedEventHandler(this.FilterPanelMRUComboBoxSelectedIndexChanged);
                this.mruListCombo.MouseEnter -= new MouseEventHandler(this.mruListCombo_MouseEnter);
                this.mruListCombo.MouseLeave -= new MouseEventHandler(this.mruListCombo_MouseLeave);
                this.mruListCombo.PopupClosed -= new ClosePopupEventHandler(this.mruListCombo_PopupClosed);
            }
            this.mruListCombo = this.GetMRUListCombo();
            if (this.mruListCombo != null)
            {
                this.mruListCombo.SelectedIndexChanged += new RoutedEventHandler(this.FilterPanelMRUComboBoxSelectedIndexChanged);
                this.mruListCombo.MouseEnter += new MouseEventHandler(this.mruListCombo_MouseEnter);
                this.mruListCombo.MouseLeave += new MouseEventHandler(this.mruListCombo_MouseLeave);
                this.mruListCombo.PopupClosed += new ClosePopupEventHandler(this.mruListCombo_PopupClosed);
                this.UpdateMRUComboBoxEditorButtonsVisibilityCore();
                this.mruListCombo.EditValue = this.ActiveFilterInfo;
            }
            this.IsFilterEnabledButton = base.GetTemplateChild("PART_FilterPanelIsActiveButton");
            this.UpdateIsFilterEnabledButton();
        }

        private static void OnIsFilterEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FilterPanelControlBase) d).UpdateIsFilterEnabledButton();
        }

        protected virtual void OnMruComboBoxPopupClosed()
        {
        }

        private void OnMRUFiltersChanged(ReadOnlyObservableCollection<CriteriaOperatorInfo> oldValue)
        {
            if (oldValue != null)
            {
                CollectionChangedEventManager.RemoveListener(oldValue, this);
            }
            if (this.MRUFilters != null)
            {
                CollectionChangedEventManager.AddListener(this.MRUFilters, this);
            }
        }

        private static void OnMRUFiltersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FilterPanelControlBase) d).OnMRUFiltersChanged((ReadOnlyObservableCollection<CriteriaOperatorInfo>) e.OldValue);
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(CollectionChangedEventManager)))
            {
                return false;
            }
            this.UpdateMRUComboBoxEditorButtonsVisibility();
            return true;
        }

        private void UpdateIsFilterEnabledButton()
        {
            if (this.IsFilterEnabledButton != null)
            {
                ToolTipService.SetToolTip(this.IsFilterEnabledButton, EditorLocalizer.GetString(this.IsFilterEnabled ? EditorStringId.FilterPanelDisableFilter : EditorStringId.FilterPanelEnableFilter));
            }
        }

        private void UpdateMRUComboBoxEditorButtonsVisibility()
        {
            ComboBoxEdit mRUListCombo = this.GetMRUListCombo();
            if (mRUListCombo != null)
            {
                mRUListCombo.IsHitTestVisible = this.AllowMRUFilterList;
                this.UpdateMRUComboBoxEditorButtonsVisibilityCore();
            }
        }

        private void UpdateMRUComboBoxEditorButtonsVisibilityCore()
        {
            if (this.MRUFilters != null)
            {
                this.mruListCombo.ShowEditorButtons = (this.MRUFilters.Count > 0) && this.AllowMRUFilterList;
            }
        }

        private void UpdateMRUComboEditValue(CriteriaOperatorInfo selectedFilter)
        {
            base.ApplyTemplate();
            ComboBoxEdit mRUListCombo = this.GetMRUListCombo();
            if (mRUListCombo != null)
            {
                mRUListCombo.EditValue = selectedFilter;
            }
        }

        private bool IsMRUComboPopupActive =>
            (this.MRUFilters != null) && ((this.MRUFilters.Count > 0) && this.AllowMRUFilterList);

        public ICommand ClearFilterCommand
        {
            get => 
                (ICommand) base.GetValue(ClearFilterCommandProperty);
            set => 
                base.SetValue(ClearFilterCommandProperty, value);
        }

        public ICommand ShowFilterEditorCommand
        {
            get => 
                (ICommand) base.GetValue(ShowFilterEditorCommandProperty);
            set => 
                base.SetValue(ShowFilterEditorCommandProperty, value);
        }

        public bool IsFilterEnabled
        {
            get => 
                (bool) base.GetValue(IsFilterEnabledProperty);
            set => 
                base.SetValue(IsFilterEnabledProperty, value);
        }

        public bool IsCanEnableFilter
        {
            get => 
                (bool) base.GetValue(IsCanEnableFilterProperty);
            set => 
                base.SetValue(IsCanEnableFilterProperty, value);
        }

        public bool AllowFilterEditor
        {
            get => 
                (bool) base.GetValue(AllowFilterEditorProperty);
            set => 
                base.SetValue(AllowFilterEditorProperty, value);
        }

        public object FilterPanelContent
        {
            get => 
                base.GetValue(FilterPanelContentProperty);
            set => 
                base.SetValue(FilterPanelContentProperty, value);
        }

        public bool AllowMRUFilterList
        {
            get => 
                (bool) base.GetValue(AllowMRUFilterListProperty);
            set => 
                base.SetValue(AllowMRUFilterListProperty, value);
        }

        public ReadOnlyObservableCollection<CriteriaOperatorInfo> MRUFilters
        {
            get => 
                (ReadOnlyObservableCollection<CriteriaOperatorInfo>) base.GetValue(MRUFiltersProperty);
            set => 
                base.SetValue(MRUFiltersProperty, value);
        }

        public CriteriaOperatorInfo ActiveFilterInfo
        {
            get => 
                (CriteriaOperatorInfo) base.GetValue(ActiveFilterInfoProperty);
            set => 
                base.SetValue(ActiveFilterInfoProperty, value);
        }

        private DependencyObject IsFilterEnabledButton { get; set; }
    }
}

