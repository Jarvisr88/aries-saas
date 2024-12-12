namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PasswordBoxEditStrategy : TextEditStrategy
    {
        public PasswordBoxEditStrategy(PasswordBoxEdit editor) : base(editor)
        {
        }

        public override void AfterOnGotFocus()
        {
            base.AfterOnGotFocus();
            if (this.ShowInternalToolTip())
            {
                base.CoerceToolTip();
                base.ForceShowToolTip();
            }
        }

        private PasswordStrength CalcPasswordStrength(string password)
        {
            PasswordStrengthEventArgs args1 = new PasswordStrengthEventArgs(PasswordBoxEdit.CustomPasswordStrengthEvent);
            args1.Password = password;
            PasswordStrengthEventArgs e = args1;
            this.Editor.RaiseEvent(e);
            return (!e.Handled ? PasswordStrengthCalculator.Calculate(password) : e.PasswordStrength);
        }

        public override bool CanPaste() => 
            base.CanPaste() && base.AllowEditing;

        public virtual object CoercePassword(object value) => 
            this.CoerceValue(PasswordBoxEdit.PasswordProperty, value);

        protected override bool CreateInternalToolTipAsToolTip(object tooltip) => 
            true;

        private object GetBaseValue(object baseValue)
        {
            if ((baseValue == null) || (baseValue is string))
            {
                return baseValue;
            }
            try
            {
                object objB = Convert.ChangeType(Convert.ChangeType(baseValue, typeof(string), CultureInfo.CurrentCulture), baseValue.GetType(), CultureInfo.CurrentCulture);
                return (Equals(baseValue, objB) ? baseValue : string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }

        protected override object GetInternalToolTipContent() => 
            new BaseValidationError(EditorLocalizer.GetString(EditorStringId.PasswordBoxEditToolTipContent));

        protected override DataTemplate GetInternalToolTipTemplate() => 
            this.Editor.CapsLockWarningToolTipTemplate;

        public override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!base.AllowEditing)
            {
                e.Handled = true;
            }
            base.OnPreviewTextInput(e);
        }

        public virtual void PasswordChanged(string oldValue, string value)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(PasswordBoxEdit.PasswordProperty, oldValue, value);
            }
        }

        public virtual void PasswordCharChanged(char value)
        {
            this.UpdateDisplayText();
        }

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDownInternal(e);
            if (!base.AllowEditing && ((e.Key == Key.Space) || ((e.Key == Key.Back) || (e.Key == Key.Delete))))
            {
                e.Handled = true;
            }
            base.CoerceToolTip();
        }

        protected override void RegisterUpdateCallbacks()
        {
            // Unresolved stack state at '000000B9'
        }

        protected override bool ShowInternalToolTip() => 
            CapsLockHelper.IsCapsLockToggled && this.Editor.ShowCapsLockWarningToolTip;

        protected override void UpdateEditCoreTextInternal(string displayText)
        {
            base.UpdateEditCoreTextInternal(displayText);
            base.EditBox.EditValue = displayText;
        }

        protected internal override bool ShouldApplyNullTextToDisplayText =>
            false;

        protected PasswordBoxEdit Editor =>
            base.Editor as PasswordBoxEdit;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PasswordBoxEditStrategy.<>c <>9 = new PasswordBoxEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__7_1;
            public static PropertyCoercionHandler <>9__7_2;
            public static PropertyCoercionHandler <>9__7_3;
            public static PropertyCoercionHandler <>9__7_4;

            internal object <RegisterUpdateCallbacks>b__7_1(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__7_2(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__7_3(object baseValue) => 
                (baseValue != null) ? baseValue.ToString() : string.Empty;

            internal object <RegisterUpdateCallbacks>b__7_4(object baseValue) => 
                baseValue;
        }
    }
}

