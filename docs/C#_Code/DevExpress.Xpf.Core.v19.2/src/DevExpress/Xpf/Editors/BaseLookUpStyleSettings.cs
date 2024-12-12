namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class BaseLookUpStyleSettings : BaseItemsControlStyleSettings<LookUpEditBase>, ISelectorEditStyleSettings, ITokenStyleSettings
    {
        protected BaseLookUpStyleSettings()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            LookUpEditBase base2 = (LookUpEditBase) editor;
            base2.IsTokenMode = this.IsTokenStyleSettings();
            TokenEditor editCore = base2.EditCore as TokenEditor;
            if (editCore != null)
            {
                editCore.ShowTokenButtons = this.GetShowTokenButtons();
                editCore.EnableTokenWrapping = this.GetEnableTokenWrapping();
                editCore.TokenBorderTemplate = this.GetTokenBorderTemplate();
                editCore.NewTokenPosition = this.GetNewTokenPosition();
                editCore.TokenTextTrimming = this.GetTokenTextTrimming();
                editCore.TokenMaxWidth = this.GetTokenMaxWidth();
                editCore.AllowEditTokens = this.GetAllowEditTokens();
                editCore.TokenStyle = this.GetTokenStyle();
                editCore.NewTokenText = this.GetNewTokenText();
            }
        }

        Style ISelectorEditStyleSettings.GetItemContainerStyle(ISelectorEdit editor) => 
            this.GetItemContainerStyle((LookUpEditBase) editor);

        bool ITokenStyleSettings.GetFilterOutSelectedTokens() => 
            true;

        internal bool GetActualScrollToSelectionOnPopup(LookUpEditBase editor)
        {
            bool? scrollToSelectionOnPopup = this.ScrollToSelectionOnPopup;
            return ((scrollToSelectionOnPopup != null) ? scrollToSelectionOnPopup.GetValueOrDefault() : (this.GetSelectionMode(editor) == SelectionMode.Single));
        }

        protected internal virtual EditorPlacement GetAddNewButtonPlacement(LookUpEditBase editor) => 
            EditorPlacement.None;

        protected internal virtual bool GetAllowEditTokens()
        {
            bool? allowEditTokens = ((ITokenStyleSettings) this).AllowEditTokens;
            return ((allowEditTokens != null) ? allowEditTokens.GetValueOrDefault() : true);
        }

        protected internal virtual bool GetClosePopupOnMouseUp(LookUpEditBase editor) => 
            editor.PropertyProvider.GetPopupFooterButtons() != PopupFooterButtons.OkCancel;

        protected internal virtual bool GetEnableTokenWrapping()
        {
            bool? enableTokenWrapping = ((ITokenStyleSettings) this).EnableTokenWrapping;
            return ((enableTokenWrapping != null) ? enableTokenWrapping.GetValueOrDefault() : false);
        }

        protected internal virtual EditorPlacement GetFindButtonPlacement(LookUpEditBase editor) => 
            EditorPlacement.None;

        protected internal virtual FindMode GetFindMode(LookUpEditBase editor) => 
            FindMode.Always;

        protected internal virtual bool GetIncrementalFiltering() => 
            false;

        public override bool GetIsTextEditable(ButtonEdit editor) => 
            !this.IsTokenStyleSettings() ? ((ISelectorEditStrategy) editor.EditStrategy).IsSingleSelection : base.GetIsTextEditable(editor);

        protected internal virtual NewTokenPosition GetNewTokenPosition()
        {
            NewTokenPosition? newTokenPosition = ((ITokenStyleSettings) this).NewTokenPosition;
            return ((newTokenPosition != null) ? newTokenPosition.GetValueOrDefault() : NewTokenPosition.Near);
        }

        protected internal virtual string GetNewTokenText() => 
            ((ITokenStyleSettings) this).NewTokenText ?? EditorLocalizer.GetString(EditorStringId.TokenEditorNewTokenText);

        protected internal virtual EditorPlacement GetNullValueButtonPlacement(LookUpEditBase editor) => 
            EditorPlacement.None;

        protected virtual object GetPropertyValue(DependencyProperty property) => 
            !this.IsDefaultValue(property) ? base.GetValue(property) : null;

        public virtual bool GetSelectAllOnAcceptPopup(LookUpEditBase editor) => 
            true;

        protected internal abstract SelectionMode GetSelectionMode(LookUpEditBase editor);
        protected internal virtual bool GetShowTokenButtons()
        {
            bool? showTokenButtons = ((ITokenStyleSettings) this).ShowTokenButtons;
            return ((showTokenButtons != null) ? showTokenButtons.GetValueOrDefault() : true);
        }

        public virtual bool GetSyncValuesWithPopup(LookUpEditBase editor) => 
            false;

        protected internal virtual ControlTemplate GetTokenBorderTemplate() => 
            (ControlTemplate) (((ITokenStyleSettings) this).TokenBorderTemplate ?? null);

        protected internal virtual ButtonInfoCollection GetTokenButtons() => 
            null;

        protected internal virtual double GetTokenMaxWidth()
        {
            double? tokenMaxWidth = ((ITokenStyleSettings) this).TokenMaxWidth;
            return ((tokenMaxWidth != null) ? tokenMaxWidth.GetValueOrDefault() : 0.0);
        }

        protected internal virtual Style GetTokenStyle() => 
            (Style) (((ITokenStyleSettings) this).TokenStyle ?? null);

        protected internal virtual TextTrimming GetTokenTextTrimming()
        {
            TextTrimming? tokenTextTrimming = ((ITokenStyleSettings) this).TokenTextTrimming;
            return ((tokenTextTrimming != null) ? tokenTextTrimming.GetValueOrDefault() : TextTrimming.None);
        }

        protected virtual bool IsDefaultValue(DependencyProperty property)
        {
            object defaultValue = property.GetMetadata(base.GetType()).DefaultValue;
            defaultValue = (defaultValue == DependencyProperty.UnsetValue) ? null : defaultValue;
            return (base.GetValue(property) == defaultValue);
        }

        public virtual bool IsTokenStyleSettings() => 
            false;

        [Description("")]
        protected internal virtual bool ProcessContentSelectionChanged(FrameworkElement sender, SelectionChangedEventArgs e) => 
            true;

        [Description("")]
        protected internal virtual bool? ScrollToSelectionOnPopup { get; set; }

        protected internal virtual bool CloseUsingDispatcher =>
            false;

        protected internal virtual DevExpress.Data.Filtering.FilterCondition FilterCondition =>
            DevExpress.Data.Filtering.FilterCondition.StartsWith;

        protected internal virtual DevExpress.Xpf.Editors.FilterByColumnsMode FilterByColumnsMode =>
            DevExpress.Xpf.Editors.FilterByColumnsMode.Default;

        bool? ITokenStyleSettings.AllowEditTokens =>
            null;

        bool? ITokenStyleSettings.EnableTokenWrapping =>
            null;

        ControlTemplate ITokenStyleSettings.TokenBorderTemplate =>
            null;

        bool? ITokenStyleSettings.ShowTokenButtons =>
            null;

        NewTokenPosition? ITokenStyleSettings.NewTokenPosition =>
            null;

        TextTrimming? ITokenStyleSettings.TokenTextTrimming =>
            null;

        double? ITokenStyleSettings.TokenMaxWidth =>
            null;

        Style ITokenStyleSettings.TokenStyle =>
            null;

        string ITokenStyleSettings.NewTokenText =>
            null;
    }
}

