namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class CheckEditStrategy : EditStrategyBase
    {
        private bool isToggleKeyDown;

        public CheckEditStrategy(CheckEdit editor) : base(editor)
        {
            this.IsEnabledCore = true;
        }

        private void CaptureMouse()
        {
            this.Editor.CaptureMouse();
        }

        public virtual void ExecuteCommand()
        {
            ICommand command = this.Editor.Command;
            if ((command != null) && command.CanExecute(this.Editor.CommandParameter))
            {
                command.Execute(this.Editor.CommandParameter);
            }
        }

        protected override string FormatDisplayTextFast(object editValue)
        {
            bool? booleanFromEditValue = CheckEditSettings.GetBooleanFromEditValue(editValue, this.Editor.IsThreeState);
            return ((booleanFromEditValue != null) ? (booleanFromEditValue.Value ? EditorLocalizer.GetString(EditorStringId.CheckChecked) : EditorLocalizer.GetString(EditorStringId.CheckUnchecked)) : EditorLocalizer.GetString(EditorStringId.CheckIndeterminate));
        }

        protected virtual bool GetIsInsideListBoxItem() => 
            LayoutHelper.FindParentObject<ListBoxEditItem>(this.Editor.Parent as Visual) != null;

        protected internal virtual void IsCheckedChanged(bool? oldValue, bool? value)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(CheckEdit.IsCheckedProperty, oldValue, value);
            }
        }

        private bool IsToggleCheckGesture(Key key) => 
            !this.Editor.IsReadOnly && (this.Editor.IsEnabled && ((key == Key.Space) || (((bool) this.Editor.GetValue(KeyboardNavigation.AcceptsReturnProperty)) && (key == Key.Return))));

        protected virtual void OnClick()
        {
            this.PerformToggle();
            this.RaiseClickEvent();
            this.ExecuteCommand();
        }

        public virtual void PerformToggle()
        {
            if (base.AllowEditing && base.AllowKeyHandling)
            {
                bool? booleanFromEditValue = CheckEditSettings.GetBooleanFromEditValue(base.ValueContainer.EditValue, this.Editor.IsThreeState);
                bool? nullable2 = booleanFromEditValue;
                bool flag = true;
                if (!((nullable2.GetValueOrDefault() == flag) ? (nullable2 != null) : false))
                {
                    base.ValueContainer.SetEditValue(!this.Editor.IsThreeState ? ((object) 1) : ((object) (booleanFromEditValue != null)), UpdateEditorSource.TextInput);
                }
                else
                {
                    bool? nullable1;
                    if (!this.Editor.IsThreeState)
                    {
                        nullable1 = false;
                    }
                    else
                    {
                        nullable2 = null;
                        nullable1 = nullable2;
                    }
                    base.ValueContainer.SetEditValue(nullable1, UpdateEditorSource.TextInput);
                }
                this.UpdateCheckBoxValue();
            }
        }

        protected internal override void ProcessEditModeChanged(EditMode oldValue, EditMode newValue)
        {
            base.ProcessEditModeChanged(oldValue, newValue);
            if (newValue == EditMode.InplaceInactive)
            {
                this.UncaptureMouse();
            }
        }

        private bool ProcessIsMouseOverChanged()
        {
            if ((this.Editor.ClickMode != ClickMode.Hover) || !this.Editor.IsMouseOver)
            {
                return false;
            }
            this.OnClick();
            return true;
        }

        public bool ProcessKeyDown(Key key)
        {
            bool flag = false;
            if ((this.Editor.ClickMode != ClickMode.Hover) && (this.IsMouseLeftButtonReleased && this.IsToggleCheckGesture(key)))
            {
                flag = true;
                if (!this.IsToggleKeyDown)
                {
                    this.IsToggleKeyDown = true;
                    this.CaptureMouse();
                    if (this.Editor.ClickMode == ClickMode.Press)
                    {
                        this.OnClick();
                    }
                }
            }
            return flag;
        }

        public bool ProcessKeyUp(Key key)
        {
            bool flag = false;
            if ((this.Editor.ClickMode != ClickMode.Hover) && (this.IsMouseLeftButtonReleased && this.IsToggleCheckGesture(key)))
            {
                flag = true;
                this.IsToggleKeyDown = false;
                this.UncaptureMouse();
                if (this.Editor.ClickMode == ClickMode.Release)
                {
                    this.OnClick();
                }
            }
            return flag;
        }

        public bool ProcessMouseEnter() => 
            this.ProcessIsMouseOverChanged();

        public bool ProcessMouseLeave() => 
            this.ProcessIsMouseOverChanged();

        public bool ProcessMouseLeftButtonDown()
        {
            bool flag = false;
            if ((this.Editor.EditMode != EditMode.InplaceInactive) && (this.Editor.ClickMode != ClickMode.Hover))
            {
                flag = !this.IsInsideListBoxItem;
                this.CaptureMouse();
                if (this.Editor.ClickMode == ClickMode.Press)
                {
                    this.OnClick();
                }
            }
            return flag;
        }

        public bool ProcessMouseLeftButtonUp()
        {
            if (this.Editor.EditMode == EditMode.InplaceInactive)
            {
                return false;
            }
            bool flag = false;
            if (this.Editor.ClickMode != ClickMode.Hover)
            {
                flag = !this.IsInsideListBoxItem;
                bool isMouseCaptured = this.IsMouseCaptured;
                if (!this.IsToggleKeyDown)
                {
                    this.UncaptureMouse();
                }
                if (((this.Editor.ClickMode == ClickMode.Release) && this.Editor.IsMouseOver) & isMouseCaptured)
                {
                    this.OnClick();
                }
            }
            return flag;
        }

        private void RaiseClickEvent()
        {
            this.Editor.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, this.Editor));
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
                    this.Editor.OnChecked(new RoutedEventArgs(CheckEdit.CheckedEvent));
                }
                else
                {
                    isChecked = this.Editor.IsChecked;
                    flag = false;
                    if ((isChecked.GetValueOrDefault() == flag) ? (isChecked != null) : false)
                    {
                        this.Editor.OnUnchecked(new RoutedEventArgs(CheckEdit.UncheckedEvent));
                    }
                    else
                    {
                        this.Editor.OnIndeterminate(new RoutedEventArgs(CheckEdit.IndeterminateEvent));
                    }
                }
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__20_0;
                getBaseValueHandler = <>c.<>9__20_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(CheckEdit.IsCheckedProperty, getBaseValueHandler, (PropertyCoercionHandler) (baseValue => CheckEditSettings.GetBooleanFromEditValue(baseValue, this.Editor.IsThreeState)));
        }

        protected internal override bool ShouldProcessNullInput(KeyEventArgs e) => 
            base.ShouldProcessNullInput(e) && this.Editor.IsThreeState;

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            this.UpdateCheckBoxValue();
        }

        protected override void SyncWithEditorInternal()
        {
            base.ValueContainer.SetEditValue(this.CheckBox.IsChecked, UpdateEditorSource.ValueChanging);
            base.SyncWithEditorInternal();
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.UpdateCheckBoxValue();
        }

        private void UncaptureMouse()
        {
            this.Editor.ReleaseMouseCapture();
        }

        public virtual void UpdateActualGlyph()
        {
            bool? booleanFromEditValue = CheckEditSettings.GetBooleanFromEditValue(base.ValueContainer.EditValue, this.Editor.IsThreeState);
            if (booleanFromEditValue != null)
            {
                bool valueOrDefault = booleanFromEditValue.GetValueOrDefault();
                if (!valueOrDefault)
                {
                    this.Editor.ActualGlyph = this.Editor.UncheckedGlyph;
                    return;
                }
                if (valueOrDefault)
                {
                    this.Editor.ActualGlyph = this.Editor.CheckedGlyph;
                    return;
                }
            }
            this.Editor.ActualGlyph = this.Editor.IndeterminateGlyph;
        }

        public virtual void UpdateCanExecute(ICommand newValue)
        {
            this.IsEnabledCore = (newValue == null) || CommandHelper.CanExecuteCommandSource(this.Editor);
            this.Editor.UpdateIsEnabledCore();
        }

        public virtual void UpdateCheckBoxValue()
        {
            this.UpdateActualGlyph();
            if (this.CheckBox != null)
            {
                this.CheckBox.IsChecked = CheckEditSettings.GetBooleanFromEditValue(base.ValueContainer.EditValue, this.Editor.IsThreeState);
                this.CheckBox.Glyph = this.Editor.ActualGlyph;
            }
        }

        private CheckEditSettings Settings =>
            base.Settings as CheckEditSettings;

        private CheckEditBox CheckBox =>
            this.Editor.CheckBox;

        private CheckEdit Editor =>
            (CheckEdit) base.Editor;

        public bool IsEnabledCore { virtual get; private set; }

        public bool IsInsideListBoxItem =>
            BaseEditHelper.GetAllowCheckListBoxItem(this.Editor) && this.GetIsInsideListBoxItem();

        private bool IsMouseLeftButtonReleased =>
            MouseHelper.IsMouseLeftButtonReleased(null);

        protected internal bool IsToggleKeyDown
        {
            get => 
                this.isToggleKeyDown;
            set
            {
                if (value != this.isToggleKeyDown)
                {
                    this.isToggleKeyDown = value;
                }
            }
        }

        private bool IsMouseCaptured =>
            this.Editor.IsMouseCaptured;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckEditStrategy.<>c <>9 = new CheckEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__20_0;

            internal object <RegisterUpdateCallbacks>b__20_0(object baseValue) => 
                baseValue;
        }
    }
}

