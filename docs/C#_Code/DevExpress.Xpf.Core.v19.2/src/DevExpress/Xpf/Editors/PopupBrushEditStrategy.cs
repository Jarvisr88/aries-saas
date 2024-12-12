namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Media;

    public class PopupBrushEditStrategy : PopupBaseEditStrategy
    {
        public PopupBrushEditStrategy(PopupBrushEditBase editor) : base(editor)
        {
            this.AcceptPopupValueAction = new PostponedAction(() => this.Editor.IsPopupOpen);
        }

        public virtual void AcceptPopupValue()
        {
            this.AcceptPopupValueAction.PerformPostpone(() => this.AcceptPopupValueInternal(this.VisualClient.GetSelectedItem()));
        }

        protected virtual void AcceptPopupValueInternal(object editValue)
        {
            PopupBrushValue value2 = editValue as PopupBrushValue;
            if (value2 != null)
            {
                this.UpdateStyleSettings(value2.BrushType);
                Brush brush = DevExpress.Xpf.Editors.Internal.BrushConverter.ToBrushType(value2.Brush, value2.BrushType);
                base.ValueContainer.SetEditValue(brush, UpdateEditorSource.ValueChanging);
                this.UpdateDisplayText();
                this.SelectAll();
            }
        }

        public void BrushTypeChanged(BrushType oldValue, BrushType newValue)
        {
            base.SyncWithValue();
        }

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new DevExpress.Xpf.Editors.Services.PopupBrushEditValidator(this.Editor);

        protected override BaseEditingSettingsService CreateTextInputSettingsService() => 
            new PopupBrushEditSettingsService(this.Editor);

        public virtual void DestroyPopup()
        {
            this.AcceptPopupValueAction.PerformForce();
        }

        public virtual PopupBrushValue GetPopupEditValue(BrushType brushType)
        {
            BrushType type = DevExpress.Xpf.Editors.Internal.BrushConverter.GetBrushType(base.ValueContainer.EditValue, brushType);
            object obj2 = DevExpress.Xpf.Editors.Internal.BrushConverter.ToBrushType(base.ValueContainer.EditValue, type);
            if (type == BrushType.SolidColorBrush)
            {
                PopupSolidColorBrushValue value1 = new PopupSolidColorBrushValue();
                value1.BrushType = type;
                value1.Brush = obj2;
                return value1;
            }
            if (type == BrushType.LinearGradientBrush)
            {
                PopupLinearGradientBrushValue value2 = new PopupLinearGradientBrushValue();
                value2.BrushType = type;
                value2.Brush = obj2;
                return value2;
            }
            if (type == BrushType.RadialGradientBrush)
            {
                PopupRadialGradientBrushValue value3 = new PopupRadialGradientBrushValue();
                value3.BrushType = type;
                value3.Brush = obj2;
                return value3;
            }
            PopupBrushValue value4 = new PopupBrushValue();
            value4.BrushType = type;
            value4.Brush = obj2;
            return value4;
        }

        public virtual bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            key == Key.Return;

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.PropertyProvider.Brush = DevExpress.Xpf.Editors.Internal.BrushConverter.ToBrushType(base.ValueContainer.EditValue, this.Editor.BrushType);
        }

        protected virtual void UpdateStyleSettings(BrushType brushType)
        {
            if (this.Editor.BrushType != BrushType.AutoDetect)
            {
                switch (brushType)
                {
                    case BrushType.None:
                    case BrushType.SolidColorBrush:
                        this.Editor.StyleSettings = new PopupSolidColorBrushEditStyleSettings();
                        return;

                    case BrushType.LinearGradientBrush:
                        this.Editor.StyleSettings = new PopupLinearGradientBrushEditStyleSettings();
                        return;

                    case BrushType.RadialGradientBrush:
                        this.Editor.StyleSettings = new PopupRadialGradientBrushEditStyleSettings();
                        return;
                }
            }
        }

        private PopupBrushEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as PopupBrushEditPropertyProvider;

        private SelectorVisualClientOwner VisualClient =>
            this.Editor.VisualClient as SelectorVisualClientOwner;

        private PopupBrushEditBase Editor =>
            base.Editor as PopupBrushEditBase;

        private PostponedAction AcceptPopupValueAction { get; set; }
    }
}

