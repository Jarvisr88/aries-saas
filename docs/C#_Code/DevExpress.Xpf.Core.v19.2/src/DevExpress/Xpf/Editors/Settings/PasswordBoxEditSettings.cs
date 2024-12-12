namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PasswordBoxEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty PasswordCharProperty;
        public static readonly DependencyProperty MaxLengthProperty;

        static PasswordBoxEditSettings()
        {
            Type ownerType = typeof(PasswordBoxEditSettings);
            BaseEditSettings.NullValueProperty.OverrideMetadata(typeof(PasswordBoxEditSettings), new FrameworkPropertyMetadata(string.Empty));
            PasswordCharProperty = DependencyPropertyManager.Register("PasswordChar", typeof(char), ownerType, new FrameworkPropertyMetadata('●'));
            MaxLengthProperty = DependencyPropertyManager.Register("MaxLength", typeof(int), ownerType, new FrameworkPropertyMetadata(0));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            PasswordBoxEdit editor = edit as PasswordBoxEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(PasswordCharProperty, () => editor.PasswordChar = this.PasswordChar);
                base.SetValueFromSettings(MaxLengthProperty, () => editor.MaxLength = this.MaxLength);
            }
        }

        public override string GetDisplayTextFromEditor(object editValue)
        {
            string displayTextFromEditor = base.GetDisplayTextFromEditor(editValue);
            return (string.IsNullOrEmpty(displayTextFromEditor) ? displayTextFromEditor : new string(this.PasswordChar, displayTextFromEditor.Length));
        }

        protected internal override bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            !this.IsPasteGesture(key, modifiers) ? base.IsActivatingKey(key, modifiers) : true;

        public char PasswordChar
        {
            get => 
                (char) base.GetValue(PasswordCharProperty);
            set => 
                base.SetValue(PasswordCharProperty, value);
        }

        public int MaxLength
        {
            get => 
                (int) base.GetValue(MaxLengthProperty);
            set => 
                base.SetValue(MaxLengthProperty, value);
        }
    }
}

