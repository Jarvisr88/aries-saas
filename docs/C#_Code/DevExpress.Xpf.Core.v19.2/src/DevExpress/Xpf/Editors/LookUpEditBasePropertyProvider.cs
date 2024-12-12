namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LookUpEditBasePropertyProvider : PopupBaseEditPropertyProvider, ISelectorEditPropertyProvider
    {
        private static int currentHandle;
        private static readonly object locker = new object();
        public static readonly DependencyProperty SelectionViewModelProperty;
        public static readonly DependencyProperty ShowWaitIndicatorProperty;
        public static readonly DependencyProperty HasItemTemplateProperty;

        static LookUpEditBasePropertyProvider()
        {
            Type ownerType = typeof(LookUpEditBasePropertyProvider);
            SelectionViewModelProperty = DependencyPropertyManager.Register("SelectionViewModel", typeof(DevExpress.Xpf.Editors.Popups.SelectionViewModel), ownerType, new FrameworkPropertyMetadata(null));
            ShowWaitIndicatorProperty = DependencyPropertyManager.Register("ShowWaitIndicator", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HasItemTemplateProperty = DependencyPropertyManager.Register("HasItemTemplate", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
        }

        public LookUpEditBasePropertyProvider(LookUpEditBase editor) : base(editor)
        {
            this.SelectionViewModel = new DevExpress.Xpf.Editors.Popups.SelectionViewModel(() => (ISelectorEditInnerListBox) LookUpEditHelper.GetVisualClient(this.Editor).InnerEditor);
            this.IncrementalFilteringHandle = this.GenerateHandle();
        }

        public override bool CalcSuppressFeatures() => 
            !this.ApplyItemTemplateToSelectedItem && base.CalcSuppressFeatures();

        public object GenerateHandle()
        {
            object locker = LookUpEditBasePropertyProvider.locker;
            lock (locker)
            {
                currentHandle++;
                return currentHandle;
            }
        }

        public override EditorPlacement GetAddNewButtonPlacement()
        {
            EditorPlacement? addNewButtonPlacement = this.Editor.AddNewButtonPlacement;
            return ((addNewButtonPlacement != null) ? addNewButtonPlacement.GetValueOrDefault() : this.StyleSettings.GetAddNewButtonPlacement(this.Editor));
        }

        public override EditorPlacement GetFindButtonPlacement()
        {
            EditorPlacement? findButtonPlacement = this.Editor.FindButtonPlacement;
            return ((findButtonPlacement != null) ? findButtonPlacement.GetValueOrDefault() : this.StyleSettings.GetFindButtonPlacement(this.Editor));
        }

        protected internal virtual IEnumerable<string> GetHighlightedColumns()
        {
            List<string> list1 = new List<string>();
            list1.Add(this.Editor.DisplayMember);
            return list1;
        }

        public virtual Style GetItemContainerStyle() => 
            null;

        public override EditorPlacement GetNullValueButtonPlacement()
        {
            EditorPlacement? nullValueButtonPlacement = this.Editor.NullValueButtonPlacement;
            return ((nullValueButtonPlacement != null) ? nullValueButtonPlacement.GetValueOrDefault() : this.StyleSettings.GetNullValueButtonPlacement(this.Editor));
        }

        public void SetApplyItemTemplateToSelectedItem(bool value)
        {
            this.ApplyItemTemplateToSelectedItem = value;
        }

        public virtual bool ShowCustomItems() => 
            this.StyleSettings.ShowCustomItem(this.Editor);

        public object IncrementalFilteringHandle { get; private set; }

        public object TokenFilterDataViewHandle { get; set; }

        public object TokenCurrentDataViewHandle { get; set; }

        public bool HasItemTemplate
        {
            get => 
                (bool) base.GetValue(HasItemTemplateProperty);
            set => 
                base.SetValue(HasItemTemplateProperty, value);
        }

        public bool ShowWaitIndicator
        {
            get => 
                (bool) base.GetValue(ShowWaitIndicatorProperty);
            set => 
                base.SetValue(ShowWaitIndicatorProperty, value);
        }

        public DevExpress.Xpf.Editors.Popups.SelectionViewModel SelectionViewModel
        {
            get => 
                (DevExpress.Xpf.Editors.Popups.SelectionViewModel) base.GetValue(SelectionViewModelProperty);
            set => 
                base.SetValue(SelectionViewModelProperty, value);
        }

        public bool ApplyItemTemplateToSelectedItem { get; private set; }

        public System.Windows.Controls.SelectionMode SelectionMode =>
            this.StyleSettings.GetSelectionMode(this.Editor);

        public DevExpress.Xpf.Editors.FindMode FindMode
        {
            get
            {
                DevExpress.Xpf.Editors.FindMode? findMode = this.Editor.FindMode;
                return ((findMode != null) ? findMode.GetValueOrDefault() : this.StyleSettings.GetFindMode(this.Editor));
            }
        }

        public DevExpress.Data.Filtering.FilterCondition FilterCondition
        {
            get
            {
                DevExpress.Data.Filtering.FilterCondition? filterCondition = this.Editor.FilterCondition;
                return ((filterCondition != null) ? filterCondition.GetValueOrDefault() : this.StyleSettings.FilterCondition);
            }
        }

        public DevExpress.Xpf.Editors.FilterByColumnsMode FilterByColumnsMode =>
            this.StyleSettings.FilterByColumnsMode;

        public bool EnableTokenWrapping =>
            this.StyleSettings.GetEnableTokenWrapping();

        public bool IsSingleSelection =>
            this.SelectionMode == System.Windows.Controls.SelectionMode.Single;

        private LookUpEditBase Editor =>
            (LookUpEditBase) base.Editor;

        private BaseLookUpStyleSettings StyleSettings =>
            (BaseLookUpStyleSettings) base.StyleSettings;

        public bool IsServerMode =>
            this.Editor.ItemsProvider.IsAsyncServerMode || this.Editor.ItemsProvider.IsSyncServerMode;

        public bool IncrementalFiltering
        {
            get
            {
                bool? incrementalFiltering = this.Editor.IncrementalFiltering;
                return ((incrementalFiltering != null) ? incrementalFiltering.GetValueOrDefault() : this.StyleSettings.GetIncrementalFiltering());
            }
        }

        public DevExpress.Xpf.Editors.TokenEditorBehavior TokenEditorBehavior { get; set; }

        public bool SelectAllOnAcceptPopup
        {
            get
            {
                bool? selectAllOnAcceptPopup = this.Editor.SelectAllOnAcceptPopup;
                return ((selectAllOnAcceptPopup != null) ? selectAllOnAcceptPopup.GetValueOrDefault() : this.StyleSettings.GetSelectAllOnAcceptPopup(this.Editor));
            }
        }

        public bool SyncValuesWithPopup =>
            this.StyleSettings.GetSyncValuesWithPopup(this.Editor);

        public bool FilterOutSelectedTokens =>
            this.Editor.IsTokenMode ? ((ITokenStyleSettings) this.StyleSettings).GetFilterOutSelectedTokens() : false;
    }
}

