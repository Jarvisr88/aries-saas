namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ToggleSwitchEditStrategy : EditStrategyBase
    {
        public ToggleSwitchEditStrategy(BaseEdit edit) : base(edit)
        {
        }

        protected override string FormatDisplayTextFast(object editValue) => 
            this.GetCheckText(editValue);

        private string GetCheckedStateText() => 
            (this.Editor.CheckedStateContent != null) ? this.Editor.CheckedStateContent.ToString() : EditorLocalizer.GetString(EditorStringId.CheckChecked);

        internal string GetCheckText(object editValue)
        {
            bool? booleanFromEditValue = CheckEditSettings.GetBooleanFromEditValue(editValue, this.Editor.IsThreeState);
            return ((booleanFromEditValue != null) ? (!booleanFromEditValue.Value ? this.GetUncheckedStateText() : this.GetCheckedStateText()) : string.Empty);
        }

        private string GetUncheckedStateText() => 
            (this.Editor.UncheckedStateContent != null) ? this.Editor.UncheckedStateContent.ToString() : EditorLocalizer.GetString(EditorStringId.CheckUnchecked);

        public void OnIsCheckedChanged(bool? oldValue, bool? newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(ToggleSwitchEdit.IsCheckedProperty, oldValue, newValue);
            }
        }

        public void OnIsThreeStateChanged()
        {
        }

        public override void RaiseValueChangedEvents(object oldValue, object newValue)
        {
            base.RaiseValueChangedEvents(oldValue, newValue);
            if (!base.ShouldLockRaiseEvents)
            {
                bool? isChecked = this.Editor.IsChecked;
                bool flag = true;
                if ((isChecked.GetValueOrDefault() == flag) ? (isChecked != null) : false)
                {
                    this.Editor.OnChecked(new RoutedEventArgs(ToggleSwitchEdit.CheckedEvent));
                }
                else
                {
                    isChecked = this.Editor.IsChecked;
                    flag = false;
                    if ((isChecked.GetValueOrDefault() == flag) ? (isChecked != null) : false)
                    {
                        this.Editor.OnUnchecked(new RoutedEventArgs(ToggleSwitchEdit.UncheckedEvent));
                    }
                    else
                    {
                        this.Editor.OnIndeterminate(new RoutedEventArgs(ToggleSwitchEdit.IndeterminateEvent));
                    }
                }
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__17_0;
                getBaseValueHandler = <>c.<>9__17_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(ToggleSwitchEdit.IsCheckedProperty, getBaseValueHandler, (PropertyCoercionHandler) (baseValue => CheckEditSettings.GetBooleanFromEditValue(baseValue, this.Editor.IsThreeState)));
        }

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            this.UpdateToggleSwitchValue();
        }

        protected override void SyncWithEditorInternal()
        {
            this.SyncWithToggleSwitch();
            base.SyncWithEditorInternal();
        }

        private void SyncWithToggleSwitch()
        {
            base.ValueContainer.SetEditValue(this.ToggleSwitchValue, UpdateEditorSource.ValueChanging);
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.UpdateToggleSwitchValue();
        }

        public void ToggleSwitchToggled()
        {
            this.SyncWithToggleSwitch();
        }

        protected virtual void UpdateToggleSwitchValue()
        {
            this.ToggleSwitchValue = CheckEditSettings.GetBooleanFromEditValue(base.ValueContainer.EditValue, this.Editor.IsThreeState);
        }

        private ToggleSwitchEdit Editor =>
            (ToggleSwitchEdit) base.Editor;

        private ToggleSwitchEditSettings Settings =>
            (ToggleSwitchEditSettings) this.Editor.Settings;

        private DevExpress.Xpf.Editors.ToggleSwitch ToggleSwitch =>
            this.Editor.EditCore as DevExpress.Xpf.Editors.ToggleSwitch;

        private bool? ToggleSwitchValue
        {
            get
            {
                Func<DevExpress.Xpf.Editors.ToggleSwitch, bool?> evaluator = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<DevExpress.Xpf.Editors.ToggleSwitch, bool?> local1 = <>c.<>9__8_0;
                    evaluator = <>c.<>9__8_0 = x => x.IsChecked;
                }
                return this.ToggleSwitch.Return<DevExpress.Xpf.Editors.ToggleSwitch, bool?>(evaluator, (<>c.<>9__8_1 ??= ((Func<bool?>) (() => null))));
            }
            set => 
                this.ToggleSwitch.Do<DevExpress.Xpf.Editors.ToggleSwitch>(x => x.SetValueInternal(value));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ToggleSwitchEditStrategy.<>c <>9 = new ToggleSwitchEditStrategy.<>c();
            public static Func<ToggleSwitch, bool?> <>9__8_0;
            public static Func<bool?> <>9__8_1;
            public static PropertyCoercionHandler <>9__17_0;

            internal bool? <get_ToggleSwitchValue>b__8_0(ToggleSwitch x) => 
                x.IsChecked;

            internal bool? <get_ToggleSwitchValue>b__8_1() => 
                null;

            internal object <RegisterUpdateCallbacks>b__17_0(object baseValue) => 
                baseValue;
        }
    }
}

