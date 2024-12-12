namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class DateEditStrategy : RangeEditorStrategy<DateTime>
    {
        public DateEditStrategy(DateEdit editor) : base(editor)
        {
        }

        public virtual void AcceptPopupValue()
        {
        }

        public virtual object CoerceDateTime(object baseValue) => 
            this.CoerceValue(DateEdit.DateTimeProperty, base.CreateValueConverter(baseValue).Value);

        public override object CoerceMaskType(MaskType maskType) => 
            ((maskType == MaskType.DateTime) || (maskType == MaskType.DateTimeAdvancingCaret)) ? ((object) maskType) : ((object) 1);

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new DateEditValidator(this.Editor);

        protected override MinMaxUpdateHelper CreateMinMaxHelper() => 
            new MinMaxUpdateHelper(this.Editor, DateEdit.MinValueProperty, DateEdit.MaxValueProperty);

        protected override RangeEditorService CreateRangeEditService() => 
            new DateEditRangeService(this.Editor);

        protected override BaseEditingSettingsService CreateTextInputSettingsService() => 
            new PopupBaseEditSettingsService(this.Editor);

        public virtual void DateTimeChanged(DateTime oldDateTime, DateTime dateTime)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(DateEdit.DateTimeProperty, oldDateTime, dateTime);
            }
        }

        protected override object GetDefaultValue() => 
            DateTime.Today;

        protected internal override DateTime GetMaxValue() => 
            (this.Editor.MaxValue == null) ? DateTime.MaxValue : this.Editor.MaxValue.Value;

        protected internal override DateTime GetMinValue() => 
            (this.Editor.MinValue == null) ? DateTime.MinValue : this.Editor.MinValue.Value;

        public virtual bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            key == Key.Return;

        public virtual bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            false;

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__6_0;
                getBaseValueHandler = <>c.<>9__6_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(DateEdit.DateTimeProperty, getBaseValueHandler, new PropertyCoercionHandler(this.Correct));
        }

        public void SetDateTime(DateTime editValue, UpdateEditorSource updateEditorSource)
        {
            base.ValueContainer.SetEditValue(editValue, updateEditorSource);
            bool isSelectAll = base.IsSelectAll;
            base.TextInputService.SetInitialEditValue(editValue);
            if (isSelectAll)
            {
                this.SelectAll();
            }
        }

        public void UpdateCalendarShowClearButton()
        {
            if (this.Editor.Calendar != null)
            {
                this.Editor.Calendar.ShowClearButton = (base.AllowKeyHandling && !this.Editor.IsReadOnly) && (this.Editor.ShowClearButton && this.Editor.AllowNullInput);
            }
        }

        public override bool ShouldRoundToBounds =>
            this.Editor.AllowRoundOutOfRangeValue;

        private DateEdit Editor =>
            (DateEdit) base.Editor;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateEditStrategy.<>c <>9 = new DateEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__6_0;

            internal object <RegisterUpdateCallbacks>b__6_0(object baseValue) => 
                baseValue;
        }
    }
}

