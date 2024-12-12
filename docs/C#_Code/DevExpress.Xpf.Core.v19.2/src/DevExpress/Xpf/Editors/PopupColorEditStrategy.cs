namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public class PopupColorEditStrategy : PopupBaseEditStrategy
    {
        public PopupColorEditStrategy(PopupColorEdit edit) : base(edit)
        {
        }

        public virtual void AcceptPopupValue()
        {
            if ((this.ColorEditControl != null) && !this.Editor.IsReadOnly)
            {
                base.ValueContainer.SetEditValue(this.ColorEditControl.Color, UpdateEditorSource.ValueChanging);
                this.UpdateDisplayText();
            }
        }

        public virtual object CoerceColor(Color color) => 
            this.CoerceValue(PopupColorEdit.ColorProperty, color);

        protected internal override object ConvertEditValueForFormatDisplayText(object convertedValue) => 
            !string.IsNullOrEmpty(this.Editor.DisplayFormatString) ? base.ConvertEditValueForFormatDisplayText(convertedValue) : this.Editor.GetColorNameCore(ColorEditHelper.GetColorFromValue(convertedValue));

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new DevExpress.Xpf.Editors.Services.PopupColorEditValidator(this.Editor);

        public virtual void DisplayModeChanged(PopupColorEditDisplayMode oldValue, PopupColorEditDisplayMode newValue)
        {
            if (!base.IsInSupportInitialize)
            {
                this.PropertyProvider.SetIsTextEditable(this.Editor);
            }
        }

        public virtual void OnAddCustomColor(Color color)
        {
            this.Editor.RecentColors.Add(color);
            base.ValueContainer.SetEditValue(color, UpdateEditorSource.ValueChanging);
        }

        public virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(PopupColorEdit.ColorProperty, oldValue, newValue);
            }
        }

        public override bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource)
        {
            provideValue = base.Validator.ProcessConversion(value, updateSource);
            return true;
        }

        public override void RaiseValueChangedEvents(object oldValue, object newValue)
        {
            base.RaiseValueChangedEvents(oldValue, newValue);
            if (!base.ShouldLockRaiseEvents && (oldValue != newValue))
            {
                this.Editor.RaiseColorChanged();
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__12_0;
                getBaseValueHandler = <>c.<>9__12_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(PopupColorEdit.ColorProperty, getBaseValueHandler, <>c.<>9__12_1 ??= ((PropertyCoercionHandler) (baseValue => ColorEditHelper.GetColorFromValue(baseValue))));
        }

        public virtual void SyncProperties()
        {
            if (this.ColorEditControl != null)
            {
                this.ColorEditControl.Color = ColorEditHelper.GetColorFromValue(base.EditValue);
                this.ColorEditControl.ColumnCount = this.Editor.ColumnCount;
                this.ColorEditControl.RecentColors.Assign(this.Editor.RecentColors);
                this.ColorEditControl.DefaultColor = this.Editor.DefaultColor;
                this.ColorEditControl.ToolTipColorDisplayFormat = this.Editor.ColorDisplayFormat;
                this.ColorEditControl.ShowDefaultColorButton = this.Editor.ShowDefaultColorButton;
                this.ColorEditControl.ShowNoColorButton = this.Editor.ShowNoColorButton;
                this.ColorEditControl.ShowMoreColorsButton = this.Editor.ShowMoreColorsButton;
                object defaultColorButtonContent = this.Editor.DefaultColorButtonContent;
                object obj4 = defaultColorButtonContent;
                if (defaultColorButtonContent == null)
                {
                    object local1 = defaultColorButtonContent;
                    obj4 = EditorLocalizer.GetString(EditorStringId.ColorEdit_AutomaticButtonCaption);
                }
                this.ColorEditControl.DefaultColorButtonContent = obj4;
                object noColorButtonContent = this.Editor.NoColorButtonContent;
                object obj5 = noColorButtonContent;
                if (noColorButtonContent == null)
                {
                    object local2 = noColorButtonContent;
                    obj5 = EditorLocalizer.GetString(EditorStringId.ColorEdit_NoColorButtonCaption);
                }
                this.ColorEditControl.NoColorButtonContent = obj5;
                object moreColorsButtonContent = this.Editor.MoreColorsButtonContent;
                object obj6 = moreColorsButtonContent;
                if (moreColorsButtonContent == null)
                {
                    object local3 = moreColorsButtonContent;
                    obj6 = EditorLocalizer.GetString(EditorStringId.ColorEdit_MoreColorsButtonCaption);
                }
                this.ColorEditControl.MoreColorsButtonContent = obj6;
                this.ColorEditControl.ChipMargin = this.Editor.ChipMargin;
                this.ColorEditControl.ChipSize = this.Editor.ChipSize;
                this.ColorEditControl.ChipBorderBrush = this.Editor.ChipBorderBrush;
                this.ColorEditControl.Palettes = this.Editor.Palettes;
                this.ColorEditControl.IsReadOnly = this.Editor.IsReadOnly;
            }
        }

        protected override void SyncWithValueInternal()
        {
            this.SyncProperties();
            base.SyncWithValueInternal();
        }

        protected override void UpdateDisplayTextInternal()
        {
            if (!base.AllowEditing)
            {
                base.UpdateDisplayTextInternal();
            }
            else
            {
                CursorPositionSnapshot snapshot = new CursorPositionSnapshot(base.EditBox.SelectionStart, base.EditBox.SelectionLength, base.EditBox.Text, false);
                base.UpdateDisplayTextInternal();
                snapshot.ApplyToEdit(this.Editor);
            }
        }

        private PopupColorEditPropertyProvider PropertyProvider =>
            (PopupColorEditPropertyProvider) base.PropertyProvider;

        protected PopupColorEdit Editor =>
            (PopupColorEdit) base.Editor;

        protected ColorEdit ColorEditControl =>
            this.Editor.ColorEditControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupColorEditStrategy.<>c <>9 = new PopupColorEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__12_0;
            public static PropertyCoercionHandler <>9__12_1;

            internal object <RegisterUpdateCallbacks>b__12_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__12_1(object baseValue) => 
                ColorEditHelper.GetColorFromValue(baseValue);
        }
    }
}

