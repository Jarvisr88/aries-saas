namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public abstract class TextBrickBase : VisualBrick
    {
        protected string fText;

        protected TextBrickBase()
        {
            this.fText = string.Empty;
        }

        protected TextBrickBase(BrickStyle style) : base(style)
        {
            this.fText = string.Empty;
        }

        protected TextBrickBase(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.fText = string.Empty;
        }

        internal TextBrickBase(TextBrickBase brick) : base(brick)
        {
            this.fText = string.Empty;
            this.fText = brick.Text;
        }

        protected TextBrickBase(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : base(sides, borderWidth, borderColor, backColor)
        {
            this.fText = string.Empty;
            base.Style.ForeColor = foreColor;
        }

        private string GetText(object value, bool isFormatStringAllowed) => 
            (value != null) ? ((!isFormatStringAllowed || string.IsNullOrWhiteSpace(this.TextValueFormatString)) ? value.ToString() : string.Format(this.TextValueFormatString, value)) : string.Empty;

        private PSUpdatedObjects GetUpdatedObjects(out StoredID id)
        {
            id = this.StoredID;
            if (id.IsUndefined || (this.PrintingSystem == null))
            {
                return null;
            }
            PrintingDocument document = this.PrintingSystem.Document as PrintingDocument;
            IStreamingDocument document2 = this.PrintingSystem.Document as IStreamingDocument;
            return document2?.UpdatedObjects;
        }

        protected virtual void OnTextChanged()
        {
            this.UpdateStringProperty(PSUpdatedObjects.TextProperty, this.fText);
        }

        private void SetEditValue(object value)
        {
            if (this.TextValue == null)
            {
                this.SetTextOnly(value);
            }
            else
            {
                object obj2;
                if (this.TryConvertValue(value, this.TextValue.GetType(), out obj2))
                {
                    this.TextValue = obj2;
                    this.Text = this.GetText(obj2, true);
                }
                else
                {
                    this.TextValue = null;
                    this.SetTextOnly(value);
                }
            }
        }

        private void SetTextOnly(object value)
        {
            this.Text = this.GetText(value, false);
        }

        private bool TryConvertValue(object value, Type valueType, out object convertedValue)
        {
            try
            {
                convertedValue = Convert.ChangeType(value, valueType);
                return true;
            }
            catch
            {
                convertedValue = null;
                return false;
            }
        }

        protected void UpdateObjectProperty(string property, object value)
        {
            StoredID did;
            PSUpdatedObjects updatedObjects = this.GetUpdatedObjects(out did);
            if (updatedObjects != null)
            {
                updatedObjects.UpdateProperty(did, property, value);
            }
        }

        private void UpdateStringProperty(string property, string value)
        {
            StoredID did;
            PSUpdatedObjects updatedObjects = this.GetUpdatedObjects(out did);
            if (updatedObjects != null)
            {
                updatedObjects.UpdateProperty(did, property, value);
            }
        }

        [Description("Gets or sets the color of the text displayed in the current brick.")]
        public Color ForeColor
        {
            get => 
                base.Style.ForeColor;
            set => 
                base.Style = BrickStyleHelper.Instance.ChangeForeColor(base.Style, value);
        }

        [Description("Gets or sets the text to be displayed within the current brick."), XtraSerializableProperty, DefaultValue("")]
        public override string Text
        {
            get => 
                (this.fText != null) ? this.fText : string.Empty;
            set
            {
                if (value != this.fText)
                {
                    this.fText = value;
                    this.OnTextChanged();
                }
            }
        }

        internal override object EditValue
        {
            get => 
                (this.TextValue != null) ? this.TextValue : this.Text;
            set => 
                this.SetEditValue(value);
        }
    }
}

