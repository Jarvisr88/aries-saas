namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class SearchControl : Control
    {
        public static readonly DependencyProperty ShowMRUButtonProperty;
        public static readonly DependencyProperty ShowFindButtonProperty;
        public static readonly DependencyProperty ShowClearButtonProperty;
        public static readonly DependencyProperty ShowCloseButtonProperty;
        public static readonly DependencyProperty SearchTextProperty;
        public static readonly DependencyProperty FindModeProperty;
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty FilterConditionProperty;
        public static readonly DependencyProperty ParseModeProperty;
        public static readonly DependencyProperty FilterByColumnsModeProperty;
        public static readonly DependencyProperty ColumnProviderProperty;
        public static readonly DependencyProperty CriteriaOperatorTypeProperty;
        public static readonly DependencyProperty PostModeProperty;
        public static readonly DependencyProperty CloseCommandProperty;
        public static readonly DependencyProperty MRUProperty;
        public static readonly DependencyProperty MRULengthProperty;
        public static readonly DependencyProperty SearchTextPostDelayProperty;
        public static readonly DependencyProperty ImmediateMRUPopupProperty;
        public static readonly DependencyProperty NullTextProperty;
        public static readonly DependencyProperty NullTextForegroundProperty;
        public static readonly DependencyProperty SourceControlProperty;
        public static readonly DependencyProperty IsEditorTabStopProperty;
        public static readonly DependencyProperty SearchControlPropertyProviderProperty;
        public static readonly DependencyProperty ShowSearchPanelNavigationButtonsProperty;
        public static readonly DependencyProperty NextCommandProperty;
        public static readonly DependencyProperty PrevCommandProperty;
        public static readonly DependencyProperty SearchTextBoxMinWidthProperty;
        public static readonly DependencyProperty ShowResultInfoProperty;
        public static readonly DependencyProperty ResultCountProperty;
        public static readonly DependencyProperty ResultIndexProperty;
        public static readonly DependencyProperty AllowAnimationProperty;
        private ButtonEdit editor;
        private bool popupClosed;
        private bool isSearchResultHaveValue;
        private AnimationTimeline showAnimation;
        private AnimationTimeline fadeAnimation;

        static SearchControl()
        {
            Type ownerType = typeof(SearchControl);
            ShowMRUButtonProperty = DependencyPropertyManager.Register("ShowMRUButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControl) d).OnActualImmediateMRUPopupChanged()));
            ShowClearButtonProperty = DependencyPropertyManager.Register("ShowClearButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControl) d).ShowClearButtonChanged()));
            ShowCloseButtonProperty = DependencyPropertyManager.Register("ShowCloseButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));
            ShowFindButtonProperty = DependencyPropertyManager.Register("ShowFindButton", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, (d, e) => ((SearchControl) d).ShowFindButtonChanged((bool) e.OldValue, (bool) e.NewValue)));
            SearchTextProperty = DependencyPropertyManager.Register("SearchText", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControl) d).SearchTextChanged((string) e.OldValue, (string) e.NewValue)));
            FindModeProperty = DependencyPropertyManager.Register("FindMode", typeof(DevExpress.Xpf.Editors.FindMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.FindMode.Always, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControl) d).FindModeChanged((DevExpress.Xpf.Editors.FindMode) e.NewValue)));
            FilterConditionProperty = DependencyPropertyManager.Register("FilterCondition", typeof(DevExpress.Data.Filtering.FilterCondition), ownerType, new FrameworkPropertyMetadata(DevExpress.Data.Filtering.FilterCondition.StartsWith, (d, e) => ((SearchControl) d).FilterConditionChanged((DevExpress.Data.Filtering.FilterCondition) e.NewValue)));
            ParseModeProperty = DependencyPropertyManager.Register("ParseMode", typeof(SearchPanelParseMode), ownerType, new FrameworkPropertyMetadata(SearchPanelParseMode.Mixed, (d, e) => ((SearchControl) d).FilterConditionChanged((DevExpress.Data.Filtering.FilterCondition) e.NewValue)));
            FilterCriteriaProperty = DependencyPropertyManager.Register("FilterCriteria", typeof(CriteriaOperator), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControl) d).FilterCriteriaChanged((CriteriaOperator) e.OldValue, (CriteriaOperator) e.NewValue)));
            FilterByColumnsModeProperty = DependencyPropertyManager.Register("FilterByColumnsMode", typeof(DevExpress.Xpf.Editors.FilterByColumnsMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.FilterByColumnsMode.Default, (d, e) => ((SearchControl) d).FilterByColumnsModeChanged((DevExpress.Xpf.Editors.FilterByColumnsMode) e.NewValue)));
            ColumnProviderProperty = DependencyPropertyManager.Register("ColumnProvider", typeof(ISearchPanelColumnProviderBase), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((SearchControl) d).ColumnProviderChanged((ISearchPanelColumnProviderBase) e.NewValue)));
            CriteriaOperatorTypeProperty = DependencyPropertyManager.Register("CriteriaOperatorType", typeof(DevExpress.Xpf.Editors.CriteriaOperatorType), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.CriteriaOperatorType.Or, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControl) d).CriteriaOperatorTypeChanged()));
            PostModeProperty = DependencyPropertyManager.Register("PostMode", typeof(DevExpress.Xpf.Editors.PostMode?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((SearchControl) d).OnPostModeChanged()));
            CloseCommandProperty = DependencyPropertyManager.Register("CloseCommand", typeof(ICommand), ownerType, new FrameworkPropertyMetadata(null));
            MRUProperty = DependencyPropertyManager.Register("MRU", typeof(IList<string>), ownerType, new FrameworkPropertyMetadata(null));
            MRULengthProperty = DependencyPropertyManager.Register("MRULength", typeof(int), ownerType, new FrameworkPropertyMetadata(7, (d, e) => ((SearchControl) d).OnMRULengthChanged()));
            SearchControlPropertyProviderProperty = DependencyPropertyManager.Register("SearchControlPropertyProvider", typeof(DevExpress.Xpf.Editors.SearchControlPropertyProvider), ownerType, new FrameworkPropertyMetadata(null));
            SearchTextPostDelayProperty = DependencyPropertyManager.Register("SearchTextPostDelay", typeof(int), ownerType, new FrameworkPropertyMetadata(0x3e8));
            ImmediateMRUPopupProperty = DependencyPropertyManager.Register("ImmediateMRUPopup", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((SearchControl) d).OnActualImmediateMRUPopupChanged()));
            NullTextProperty = DependencyPropertyManager.Register("NullText", typeof(string), typeof(SearchControl), new FrameworkPropertyMetadata(null));
            NullTextForegroundProperty = DependencyProperty.Register("NullTextForeground", typeof(Brush), ownerType, new PropertyMetadata(null));
            IsEditorTabStopProperty = DependencyPropertyManager.Register("IsEditorTabStop", typeof(bool), typeof(SearchControl), new FrameworkPropertyMetadata(true));
            SourceControlProperty = DependencyPropertyManager.Register("SourceControl", typeof(object), typeof(SearchControl), new FrameworkPropertyMetadata(null, (d, e) => ((SearchControl) d).OnSourceControlChanged()));
            ShowSearchPanelNavigationButtonsProperty = DependencyPropertyManager.Register("ShowSearchPanelNavigationButtons", typeof(bool), typeof(SearchControl), new FrameworkPropertyMetadata(false, (d, e) => ((SearchControl) d).ShowSearchPanelNavigationButtonsChanged()));
            NextCommandProperty = DependencyPropertyManager.Register("NextCommand", typeof(ICommand), ownerType, new FrameworkPropertyMetadata(null));
            PrevCommandProperty = DependencyPropertyManager.Register("PrevCommand", typeof(ICommand), ownerType, new FrameworkPropertyMetadata(null));
            SearchTextBoxMinWidthProperty = DependencyProperty.Register("SearchTextBoxMinWidth", typeof(double), typeof(SearchControl), new PropertyMetadata(0.0));
            ShowResultInfoProperty = DependencyPropertyManager.Register("ShowResultInfo", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (d, e) => ((SearchControl) d).ShowResultInfoChanged()));
            ResultCountProperty = DependencyPropertyManager.Register("ResultCount", typeof(int), ownerType, new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None));
            ResultIndexProperty = DependencyPropertyManager.Register("ResultIndex", typeof(int), ownerType, new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None));
            AllowAnimationProperty = DependencyProperty.Register("AllowAnimation", typeof(bool), ownerType, new PropertyMetadata(false));
        }

        public SearchControl()
        {
            this.SetDefaultStyleKey(typeof(SearchControl));
            this.MRU = new ObservableCollection<string>();
            this.SearchControlPropertyProvider = new DevExpress.Xpf.Editors.SearchControlPropertyProvider(this);
            this.SearchControlPropertyProvider.DisplayTextChanged += (o, e) => this.DisplayTextChanged();
            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.SearchControl_IsVisibleChanged);
        }

        protected void BeginFadeAnimation(Action completed)
        {
            if (!this.AllowAnimation || ((this.FadeAnimation == null) || (this.editor == null)))
            {
                if (completed != null)
                {
                    completed();
                }
            }
            else
            {
                AnimationTimeline animation = this.FadeAnimation.Clone();
                if (completed != null)
                {
                    animation.Completed += (s, e) => completed();
                }
                this.editor.BeginAnimation(UIElement.OpacityProperty, animation);
            }
        }

        private void BeginShowAnimation()
        {
            if (this.AllowAnimation && ((this.ShowAnimation != null) && (this.editor != null)))
            {
                this.editor.BeginAnimation(UIElement.OpacityProperty, this.showAnimation);
            }
        }

        protected virtual void ColumnProviderChanged(ISearchPanelColumnProviderBase columnProvider)
        {
            this.UpdateColumnProvider();
        }

        private void CriteriaOperatorTypeChanged()
        {
            this.ParseMode = (this.CriteriaOperatorType == DevExpress.Xpf.Editors.CriteriaOperatorType.And) ? SearchPanelParseMode.And : SearchPanelParseMode.Mixed;
        }

        protected virtual void DisplayTextChanged()
        {
            this.UpdateClearButtonVisibility();
        }

        public bool DoValidate() => 
            (this.EditorControl != null) ? this.EditorControl.DoValidate() : true;

        protected virtual void FilterByColumnsChanged(IEnumerable<string> oldValue, IEnumerable<string> newValue)
        {
            this.UpdateFilterCriteria();
        }

        protected virtual void FilterByColumnsModeChanged(DevExpress.Xpf.Editors.FilterByColumnsMode filterByColumnsMode)
        {
            this.UpdateColumnProvider();
        }

        protected virtual void FilterConditionChanged(DevExpress.Data.Filtering.FilterCondition filterCondition)
        {
            this.UpdateFilterCriteria();
        }

        protected virtual void FilterCriteriaChanged(CriteriaOperator oldValue, CriteriaOperator newValue)
        {
            if (this.ColumnProvider != null)
            {
                this.UpdateColumnProviderFilter(newValue);
            }
        }

        protected virtual void FindModeChanged(DevExpress.Xpf.Editors.FindMode value)
        {
            this.SearchControlPropertyProvider.ActualShowFindButton = (this.ShowFindButton && !this.ShowSearchPanelNavigationButtons) && (value == DevExpress.Xpf.Editors.FindMode.FindClick);
            this.SearchControlPropertyProvider.UpdatePostMode(this);
            if (value != DevExpress.Xpf.Editors.FindMode.FindClick)
            {
                this.UpdateFilterCriteria();
            }
        }

        private CriteriaOperator GetCriteriaOperator() => 
            !(this.ColumnProvider is ISearchPanelColumnProviderEx) ? SearchControlHelper.GetCriteriaOperator(this.ColumnProvider as ISearchPanelColumnProvider, this.FilterCondition, this.SearchText, this.ParseMode) : SearchControlHelper.GetCriteriaOperator((ISearchPanelColumnProviderEx) this.ColumnProvider, this.FilterCondition, this.SearchText, this.ParseMode);

        protected virtual ButtonEdit GetEditorControl() => 
            base.GetTemplateChild("editor") as ButtonEdit;

        protected virtual bool GetIsFindCommandCanExecute() => 
            this.FindMode != DevExpress.Xpf.Editors.FindMode.Always;

        private void OnActualImmediateMRUPopupChanged()
        {
            if (this.SearchControlPropertyProvider != null)
            {
                this.SearchControlPropertyProvider.UpdateActualImmediateMRUPopup(this);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.editor = this.GetEditorControl();
            if (this.editor != null)
            {
                this.SearchControlPropertyProvider.BindEditorProperties(this, this.editor);
                if (this.editor is PopupBaseEdit)
                {
                    ((PopupBaseEdit) this.editor).PopupClosed += new ClosePopupEventHandler(this.SearchControl_PopupClosed);
                }
                this.UpdateClearButtonVisibility();
                if (base.IsVisible)
                {
                    this.BeginShowAnimation();
                }
            }
        }

        protected internal virtual void OnClearSearchTextCommandExecuted()
        {
            if (this.editor == null)
            {
                base.Dispatcher.BeginInvoke(delegate {
                    this.SearchText = null;
                    if (this.FindMode == DevExpress.Xpf.Editors.FindMode.FindClick)
                    {
                        this.UpdateFilterCriteria();
                    }
                }, new object[0]);
            }
            else
            {
                this.editor.EditValue = null;
                if (this.FindMode == DevExpress.Xpf.Editors.FindMode.FindClick)
                {
                    this.UpdateFilterCriteria();
                }
            }
        }

        protected internal virtual void OnFindCommandExecuted()
        {
            if (this.GetIsFindCommandCanExecute())
            {
                this.UpdateFilterCriteria();
                if (this.ColumnProvider != null)
                {
                    this.UpdateColumnProviderFilter(this.FilterCriteria);
                }
                this.UpdateMRUIfHaveResults();
            }
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            if (!((bool) e.NewValue))
            {
                if ((this.SearchControlPropertyProvider != null) && (this.SearchControlPropertyProvider.ActualPostMode == DevExpress.Xpf.Editors.PostMode.Immediate))
                {
                    this.SearchControlPropertyProvider.FindCommand.Execute(null);
                }
                this.UpdateMRUIfHaveResults();
            }
        }

        private void OnMRULengthChanged()
        {
            if (this.MRULength <= 0)
            {
                this.MRU.Clear();
            }
            else
            {
                while (this.MRU.Count > this.MRULength)
                {
                    this.MRU.RemoveAt(this.MRU.Count - 1);
                }
            }
        }

        private void OnParseModeChanged(DevExpress.Xpf.Editors.CriteriaOperatorType criteriaOperatorType)
        {
            this.UpdateFilterCriteria();
        }

        private void OnPostModeChanged()
        {
            this.SearchControlPropertyProvider.UpdatePostMode(this);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (ModifierKeysHelper.NoModifiers(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                Key key = e.Key;
                if (key == Key.Return)
                {
                    string searchText = this.SearchText;
                    this.editor.DoValidate();
                    if (this.FindMode == DevExpress.Xpf.Editors.FindMode.Always)
                    {
                        e.Handled = searchText != this.SearchText;
                    }
                    else
                    {
                        CriteriaOperator filterCriteria = this.FilterCriteria;
                        this.UpdateFilterCriteria();
                        if (this.SearchControlPropertyProvider != null)
                        {
                            this.SearchControlPropertyProvider.FindCommand.Execute(null);
                        }
                        if ((filterCriteria != null) || (this.FilterCriteria != null))
                        {
                            e.Handled = (filterCriteria != null) ? !filterCriteria.Equals(this.FilterCriteria) : true;
                        }
                    }
                }
                else if (key == Key.Escape)
                {
                    if (this.editor != null)
                    {
                        if (this.popupClosed)
                        {
                            e.Handled = true;
                            this.popupClosed = false;
                            return;
                        }
                        this.editor.DoValidate();
                    }
                    if (!string.IsNullOrEmpty(this.SearchText))
                    {
                        this.SearchText = null;
                        if (this.FindMode == DevExpress.Xpf.Editors.FindMode.FindClick)
                        {
                            this.UpdateFilterCriteria();
                        }
                        e.Handled = true;
                    }
                }
                this.popupClosed = false;
            }
        }

        private void OnSourceControlChanged()
        {
            if (this.SourceControl == null)
            {
                this.ColumnProvider = null;
            }
            else
            {
                if (!(this.SourceControl is ISelectorEdit))
                {
                    throw new NotImplementedException();
                }
                SelectorEditColumnProvider provider = new SelectorEditColumnProvider {
                    OwnerEdit = this.SourceControl as ISelectorEdit,
                    AllowFilter = true
                };
                this.ColumnProvider = provider;
            }
        }

        private void SearchControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                this.BeginShowAnimation();
            }
        }

        private void SearchControl_PopupClosed(object sender, ClosePopupEventArgs e)
        {
            this.popupClosed = (e.CloseMode == PopupCloseMode.Cancel) || (e.EditValue == null);
        }

        protected virtual void SearchTextChanged(string oldValue, string newValue)
        {
            this.SearchControlPropertyProvider.SearchText = newValue;
            this.UpdateClearButtonVisibility();
            if (!this.SearchControlPropertyProvider.IsSearchTextPosting || (this.FindMode != DevExpress.Xpf.Editors.FindMode.FindClick))
            {
                this.UpdateFilterCriteria();
            }
        }

        private void ShowClearButtonChanged()
        {
            this.UpdateClearButtonVisibility();
        }

        private void ShowFindButtonChanged(bool oldValue, bool newValue)
        {
            if (this.FindMode == DevExpress.Xpf.Editors.FindMode.FindClick)
            {
                if (this.ShowSearchPanelNavigationButtons)
                {
                    this.SearchControlPropertyProvider.ActualShowFindButton = false;
                }
                else
                {
                    this.SearchControlPropertyProvider.ActualShowFindButton = newValue;
                }
            }
        }

        private void ShowResultInfoChanged()
        {
            this.SearchControlPropertyProvider.ActualShowResultInfo = this.ShowResultInfo;
        }

        private void ShowSearchPanelNavigationButtonsChanged()
        {
            if (this.ShowSearchPanelNavigationButtons)
            {
                this.SearchControlPropertyProvider.ActualShowFindButton = false;
            }
            else
            {
                this.ShowFindButtonChanged(false, this.ShowFindButton);
            }
        }

        protected virtual void UpdateClearButtonVisibility()
        {
            if (!this.ShowClearButton)
            {
                this.SearchControlPropertyProvider.ActualShowClearButton = false;
            }
            else if (this.editor != null)
            {
                this.SearchControlPropertyProvider.ActualShowClearButton = !this.SearchControlPropertyProvider.IsNullTextVisible && !string.IsNullOrEmpty(this.SearchControlPropertyProvider.DisplayText);
            }
            else
            {
                this.SearchControlPropertyProvider.ActualShowClearButton = !string.IsNullOrEmpty(this.SearchText);
            }
        }

        public virtual void UpdateColumnProvider()
        {
            if (this.ColumnProvider != null)
            {
                this.ColumnProvider.UpdateColumns(this.FilterByColumnsMode);
            }
            this.UpdateFilterCriteria();
        }

        private void UpdateColumnProviderFilter(CriteriaOperator filterCriteria)
        {
            this.isSearchResultHaveValue = this.ColumnProvider.UpdateFilter(this.SearchText, this.FilterCondition, filterCriteria);
        }

        protected virtual void UpdateFilterCriteria()
        {
            if (!string.IsNullOrEmpty(this.SearchText) && (this.ColumnProvider != null))
            {
                this.FilterCriteria = this.GetCriteriaOperator();
            }
            else
            {
                if (this.FilterCriteria != null)
                {
                    this.FilterCriteria = null;
                }
                if ((this.ColumnProvider != null) && !string.IsNullOrEmpty(this.ColumnProvider.GetSearchText()))
                {
                    this.FilterCriteriaChanged(null, null);
                }
            }
        }

        public void UpdateMRU()
        {
            if (this.MRULength != 0)
            {
                if (this.MRU.Count > this.MRULength)
                {
                    this.OnMRULengthChanged();
                }
                if (!string.IsNullOrWhiteSpace(this.SearchText))
                {
                    string item = this.SearchText.Trim();
                    if (this.MRU.Count == 0)
                    {
                        this.MRU.Add(item);
                    }
                    else
                    {
                        int index = this.MRU.IndexOf(item);
                        int num2 = this.MRU.IndexOf(item);
                        if (num2 == -1)
                        {
                            if (this.MRU.Count >= this.MRULength)
                            {
                                this.MRU.RemoveAt(this.MRU.Count - 1);
                            }
                            this.MRU.Insert(0, item);
                        }
                        else if (num2 != 0)
                        {
                            this.MRU.Remove(item);
                            this.MRU.Insert(0, item);
                        }
                    }
                }
            }
        }

        protected internal void UpdateMRUIfHaveResults()
        {
            if (this.isSearchResultHaveValue)
            {
                this.UpdateMRU();
            }
            this.isSearchResultHaveValue = false;
        }

        public bool ShowMRUButton
        {
            get => 
                (bool) base.GetValue(ShowMRUButtonProperty);
            set => 
                base.SetValue(ShowMRUButtonProperty, value);
        }

        public IList<string> MRU
        {
            get => 
                (IList<string>) base.GetValue(MRUProperty);
            set => 
                base.SetValue(MRUProperty, value);
        }

        public int MRULength
        {
            get => 
                (int) base.GetValue(MRULengthProperty);
            set => 
                base.SetValue(MRULengthProperty, value);
        }

        public bool ShowClearButton
        {
            get => 
                (bool) base.GetValue(ShowClearButtonProperty);
            set => 
                base.SetValue(ShowClearButtonProperty, value);
        }

        public bool ShowResultInfo
        {
            get => 
                (bool) base.GetValue(ShowResultInfoProperty);
            set => 
                base.SetValue(ShowResultInfoProperty, value);
        }

        public bool ShowCloseButton
        {
            get => 
                (bool) base.GetValue(ShowCloseButtonProperty);
            set => 
                base.SetValue(ShowCloseButtonProperty, value);
        }

        public bool ShowFindButton
        {
            get => 
                (bool) base.GetValue(ShowFindButtonProperty);
            set => 
                base.SetValue(ShowFindButtonProperty, value);
        }

        public DevExpress.Xpf.Editors.FilterByColumnsMode FilterByColumnsMode
        {
            get => 
                (DevExpress.Xpf.Editors.FilterByColumnsMode) base.GetValue(FilterByColumnsModeProperty);
            set => 
                base.SetValue(FilterByColumnsModeProperty, value);
        }

        public DevExpress.Data.Filtering.FilterCondition FilterCondition
        {
            get => 
                (DevExpress.Data.Filtering.FilterCondition) base.GetValue(FilterConditionProperty);
            set => 
                base.SetValue(FilterConditionProperty, value);
        }

        public SearchPanelParseMode ParseMode
        {
            get => 
                (SearchPanelParseMode) base.GetValue(ParseModeProperty);
            set => 
                base.SetValue(ParseModeProperty, value);
        }

        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        public string SearchText
        {
            get => 
                (string) base.GetValue(SearchTextProperty);
            set => 
                base.SetValue(SearchTextProperty, value);
        }

        public DevExpress.Xpf.Editors.FindMode FindMode
        {
            get => 
                (DevExpress.Xpf.Editors.FindMode) base.GetValue(FindModeProperty);
            set => 
                base.SetValue(FindModeProperty, value);
        }

        public ISearchPanelColumnProviderBase ColumnProvider
        {
            get => 
                (ISearchPanelColumnProviderBase) base.GetValue(ColumnProviderProperty);
            set => 
                base.SetValue(ColumnProviderProperty, value);
        }

        [Obsolete("Use the ParseMode property instead"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.Xpf.Editors.CriteriaOperatorType CriteriaOperatorType
        {
            get => 
                (DevExpress.Xpf.Editors.CriteriaOperatorType) base.GetValue(CriteriaOperatorTypeProperty);
            set => 
                base.SetValue(CriteriaOperatorTypeProperty, value);
        }

        public DevExpress.Xpf.Editors.PostMode? PostMode
        {
            get => 
                (DevExpress.Xpf.Editors.PostMode?) base.GetValue(PostModeProperty);
            set => 
                base.SetValue(PostModeProperty, value);
        }

        public ICommand CloseCommand
        {
            get => 
                (ICommand) base.GetValue(CloseCommandProperty);
            set => 
                base.SetValue(CloseCommandProperty, value);
        }

        public int SearchTextPostDelay
        {
            get => 
                (int) base.GetValue(SearchTextPostDelayProperty);
            set => 
                base.SetValue(SearchTextPostDelayProperty, value);
        }

        public bool? ImmediateMRUPopup
        {
            get => 
                (bool?) base.GetValue(ImmediateMRUPopupProperty);
            set => 
                base.SetValue(ImmediateMRUPopupProperty, value);
        }

        public string NullText
        {
            get => 
                (string) base.GetValue(NullTextProperty);
            set => 
                base.SetValue(NullTextProperty, value);
        }

        public Brush NullTextForeground
        {
            get => 
                (Brush) base.GetValue(NullTextForegroundProperty);
            set => 
                base.SetValue(NullTextForegroundProperty, value);
        }

        public object SourceControl
        {
            get => 
                base.GetValue(SourceControlProperty);
            set => 
                base.SetValue(SourceControlProperty, value);
        }

        public bool IsEditorTabStop
        {
            get => 
                (bool) base.GetValue(IsEditorTabStopProperty);
            set => 
                base.SetValue(IsEditorTabStopProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public double SearchTextBoxMinWidth
        {
            get => 
                (double) base.GetValue(SearchTextBoxMinWidthProperty);
            set => 
                base.SetValue(SearchTextBoxMinWidthProperty, value);
        }

        public int ResultCount
        {
            get => 
                (int) base.GetValue(ResultCountProperty);
            set => 
                base.SetValue(ResultCountProperty, value);
        }

        public int ResultIndex
        {
            get => 
                (int) base.GetValue(ResultIndexProperty);
            set => 
                base.SetValue(ResultIndexProperty, value);
        }

        public DevExpress.Xpf.Editors.SearchControlPropertyProvider SearchControlPropertyProvider
        {
            get => 
                (DevExpress.Xpf.Editors.SearchControlPropertyProvider) base.GetValue(SearchControlPropertyProviderProperty);
            set => 
                base.SetValue(SearchControlPropertyProviderProperty, value);
        }

        public bool ShowSearchPanelNavigationButtons
        {
            get => 
                (bool) base.GetValue(ShowSearchPanelNavigationButtonsProperty);
            set => 
                base.SetValue(ShowSearchPanelNavigationButtonsProperty, value);
        }

        public ICommand NextCommand
        {
            get => 
                (ICommand) base.GetValue(NextCommandProperty);
            set => 
                base.SetValue(NextCommandProperty, value);
        }

        public ICommand PrevCommand
        {
            get => 
                (ICommand) base.GetValue(PrevCommandProperty);
            set => 
                base.SetValue(PrevCommandProperty, value);
        }

        public bool AllowAnimation
        {
            get => 
                (bool) base.GetValue(AllowAnimationProperty);
            set => 
                base.SetValue(AllowAnimationProperty, value);
        }

        protected ButtonEdit EditorControl =>
            this.editor;

        protected internal virtual bool SaveMRUOnStringChanged =>
            true;

        private AnimationTimeline ShowAnimation
        {
            get
            {
                this.showAnimation ??= new DoubleAnimation(0.0, 1.0, new TimeSpan(0, 0, 0, 0, 200), FillBehavior.Stop);
                return this.showAnimation;
            }
        }

        private AnimationTimeline FadeAnimation
        {
            get
            {
                this.fadeAnimation ??= new DoubleAnimation(1.0, 0.0, new TimeSpan(0, 0, 0, 0, 200), FillBehavior.Stop);
                return this.fadeAnimation;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchControl.<>c <>9 = new SearchControl.<>c();

            internal void <.cctor>b__31_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).OnActualImmediateMRUPopupChanged();
            }

            internal void <.cctor>b__31_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).ShowClearButtonChanged();
            }

            internal void <.cctor>b__31_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).CriteriaOperatorTypeChanged();
            }

            internal void <.cctor>b__31_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).OnPostModeChanged();
            }

            internal void <.cctor>b__31_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).OnMRULengthChanged();
            }

            internal void <.cctor>b__31_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).OnActualImmediateMRUPopupChanged();
            }

            internal void <.cctor>b__31_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).OnSourceControlChanged();
            }

            internal void <.cctor>b__31_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).ShowSearchPanelNavigationButtonsChanged();
            }

            internal void <.cctor>b__31_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).ShowResultInfoChanged();
            }

            internal void <.cctor>b__31_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).ShowFindButtonChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__31_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).SearchTextChanged((string) e.OldValue, (string) e.NewValue);
            }

            internal void <.cctor>b__31_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).FindModeChanged((FindMode) e.NewValue);
            }

            internal void <.cctor>b__31_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).FilterConditionChanged((FilterCondition) e.NewValue);
            }

            internal void <.cctor>b__31_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).FilterConditionChanged((FilterCondition) e.NewValue);
            }

            internal void <.cctor>b__31_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).FilterCriteriaChanged((CriteriaOperator) e.OldValue, (CriteriaOperator) e.NewValue);
            }

            internal void <.cctor>b__31_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).FilterByColumnsModeChanged((FilterByColumnsMode) e.NewValue);
            }

            internal void <.cctor>b__31_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControl) d).ColumnProviderChanged((ISearchPanelColumnProviderBase) e.NewValue);
            }
        }
    }
}

