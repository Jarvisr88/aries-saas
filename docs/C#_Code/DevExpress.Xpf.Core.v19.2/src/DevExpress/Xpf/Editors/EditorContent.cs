namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class EditorContent
    {
        protected bool Equals(EditorContent other) => 
            this.ShowEditorButtons.Equals(other.ShowEditorButtons) && (this.ShowBorder.Equals(other.ShowBorder) && ((this.EditMode == other.EditMode) && (this.Settings.IsCompatibleWith(other.Settings) && (ReferenceEquals(this.Error, other.Error) && ((this.HasDisplayTemplate == other.HasDisplayTemplate) && (this.ShowText == other.ShowText))))));

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((EditorContent) obj) : false) : true) : false;

        public override int GetHashCode() => 
            (((this.ShowEditorButtons.GetHashCode() * 0x18d) ^ this.ShowBorder.GetHashCode()) * 0x18d) ^ ((int) this.EditMode);

        public bool HasDisplayTemplate =>
            this.DisplayTemplate != null;

        public bool ShowEditorButtons { get; set; }

        public bool ShowBorder { get; set; }

        public bool ShowText { get; set; }

        public ControlTemplate DisplayTemplate { get; set; }

        public BaseValidationError Error { get; set; }

        public bool HasValidationError =>
            this.Error != null;

        public DevExpress.Xpf.Editors.EditMode EditMode { get; set; }

        public BaseEditSettings Settings { get; set; }
    }
}

