namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class ButtonEditPropertyProvider : TextEditPropertyProvider
    {
        public static readonly DependencyProperty NullValueButtonPlacementProperty;
        public static readonly DependencyProperty IsTextEditableProperty;
        public static readonly DependencyProperty IsNullValueButtonVisibleProperty;

        static ButtonEditPropertyProvider()
        {
            Type ownerType = typeof(ButtonEditPropertyProvider);
            NullValueButtonPlacementProperty = DependencyPropertyManager.Register("NullValueButtonPlacement", typeof(EditorPlacement), ownerType, new PropertyMetadata(null));
            IsTextEditableProperty = DependencyProperty.Register("IsTextEditable", typeof(bool), ownerType, new PropertyMetadata(true));
            IsNullValueButtonVisibleProperty = DependencyProperty.Register("IsNullValueButtonVisible", typeof(bool), ownerType, new PropertyMetadata(false));
        }

        public ButtonEditPropertyProvider(TextEdit edit) : base(edit)
        {
        }

        public virtual bool GetActualAllowDefaultButton(ButtonEdit editor)
        {
            bool? allowDefaultButton = editor.AllowDefaultButton;
            return ((allowDefaultButton != null) ? allowDefaultButton.GetValueOrDefault() : this.StyleSettings.GetActualAllowDefaultButton(editor));
        }

        public override EditorPlacement GetNullValueButtonPlacement()
        {
            EditorPlacement? nullValueButtonPlacement = this.Editor.NullValueButtonPlacement;
            return ((nullValueButtonPlacement != null) ? nullValueButtonPlacement.GetValueOrDefault() : EditorPlacement.None);
        }

        public virtual void SetIsTextEditable(ButtonEdit editor)
        {
            bool? isTextEditable = this.Editor.IsTextEditable;
            this.IsTextEditable = (isTextEditable != null) ? isTextEditable.GetValueOrDefault() : this.StyleSettings.GetIsTextEditable(editor);
        }

        private ButtonEditStyleSettings StyleSettings =>
            base.StyleSettings as ButtonEditStyleSettings;

        private ButtonEdit Editor =>
            (ButtonEdit) base.Editor;

        public bool IsNullValueButtonVisible
        {
            get => 
                (bool) base.GetValue(IsNullValueButtonVisibleProperty);
            set => 
                base.SetValue(IsNullValueButtonVisibleProperty, value);
        }

        public bool IsTextEditable
        {
            get => 
                (bool) base.GetValue(IsTextEditableProperty);
            set => 
                base.SetValue(IsTextEditableProperty, value);
        }

        public EditorPlacement NullValueButtonPlacement
        {
            get => 
                (EditorPlacement) base.GetValue(NullValueButtonPlacementProperty);
            set => 
                base.SetValue(NullValueButtonPlacementProperty, value);
        }
    }
}

