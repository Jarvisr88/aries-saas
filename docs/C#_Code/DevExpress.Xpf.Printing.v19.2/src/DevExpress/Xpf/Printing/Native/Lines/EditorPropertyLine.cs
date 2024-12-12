namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.ComponentModel;

    internal abstract class EditorPropertyLine : EditorPropertyLineBase
    {
        protected readonly IStringConverter converter;

        protected EditorPropertyLine(BaseEdit editor, IStringConverter converter, PropertyDescriptor property, object obj) : base(editor, property, obj)
        {
            this.converter = converter;
            editor.ValidateOnTextInput = true;
            editor.ValidateOnEnterKeyPressed = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.editor.Validate -= new ValidateEventHandler(this.ValidateEditor);
            }
        }

        private void OnUpdateValueError(ValidationEventArgs e, Exception exception)
        {
            e.ErrorContent = exception.Message;
            e.IsValid = false;
            e.Handled = true;
        }

        protected override void OnValueSet()
        {
            this.SetEditText(base.Value);
            base.OnValueSet();
        }

        public override void RefreshContent()
        {
            base.RefreshContent();
            this.SetEditText(base.Value);
        }

        protected abstract void SetEditText(object value);
        protected void UpdateValue(string text)
        {
            if (this.converter.CanConvertFromString())
            {
                base.Value = this.converter.ConvertFromString(text);
            }
        }

        protected virtual void ValidateEditor(object sender, ValidationEventArgs e)
        {
            try
            {
                if (this.converter.CanConvertFromString())
                {
                    this.converter.ConvertFromString(e.Value?.ToString());
                }
            }
            catch (NotSupportedException exception)
            {
                this.OnUpdateValueError(e, exception);
            }
            catch (InvalidOperationException exception2)
            {
                this.OnUpdateValueError(e, exception2);
            }
        }

        protected string ValueToString(object value) => 
            this.converter.ConvertToString(value);
    }
}

