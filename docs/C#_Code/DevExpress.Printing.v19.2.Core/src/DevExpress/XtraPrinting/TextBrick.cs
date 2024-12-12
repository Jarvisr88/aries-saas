namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraExport;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(TextBrickExporter))]
    public class TextBrick : TextBrickBase, ITextBrick, IVisualBrick, IBaseBrick, IBrick, ITableCell
    {
        private const DefaultBoolean DefaultXlsExportNativeFormat = DefaultBoolean.Default;
        private object textValue;
        private string textValueFormatString;

        public TextBrick()
        {
            this.XlsExportNativeFormat = DefaultBoolean.Default;
        }

        public TextBrick(BrickStyle style) : base(style)
        {
            this.XlsExportNativeFormat = DefaultBoolean.Default;
        }

        public TextBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.XlsExportNativeFormat = DefaultBoolean.Default;
        }

        protected internal TextBrick(TextBrick brick) : base(brick)
        {
            this.textValue = brick.textValue;
            this.textValueFormatString = brick.textValueFormatString;
            this.XlsxFormatString = brick.XlsxFormatString;
            this.XlsExportNativeFormat = brick.XlsExportNativeFormat;
        }

        public TextBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor) : base(sides, borderWidth, borderColor, backColor, foreColor)
        {
            this.XlsExportNativeFormat = DefaultBoolean.Default;
        }

        public override object Clone() => 
            new TextBrick(this);

        protected void OnTextValueChanged()
        {
            base.UpdateObjectProperty(PSUpdatedObjects.TextValueProperty, this.textValue);
        }

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF brickRect, IPrintingSystemContext context)
        {
            float num;
            RectangleF rect = base.Padding.Deflate(this.GetClientRectangle(brickRect, 300f), 300f);
            System.Drawing.StringFormat sf = this.StringFormat.Value;
            StringFormatFlags formatFlags = sf.FormatFlags;
            sf.SetTabStops(0f, base.Style.CalculateTabStops(context.Measurer));
            try
            {
                sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                num = new LineSplitter(this.Text, base.Style.Font, sf).SplitRectangle(rect, pageBottom, brickRect.Top, GraphicsUnit.Document);
            }
            finally
            {
                sf.FormatFlags = formatFlags;
            }
            return num;
        }

        object ITableCell.TextValue =>
            this.TextValue;

        string ITableCell.FormatString =>
            this.TextValueFormatString;

        string ITableCell.XlsxFormatString =>
            this.XlsxFormatString;

        [Description("Defines the font used to draw text within the current brick.")]
        public System.Drawing.Font Font
        {
            get => 
                base.Style.Font;
            set => 
                base.Style = BrickStyleHelper.Instance.ChangeFont(base.Style, value);
        }

        [Description("Gets or sets the formatting string applied to the brick's text.")]
        public BrickStringFormat StringFormat
        {
            get => 
                base.Style.StringFormat;
            set => 
                base.Style = BrickStyleHelper.Instance.ChangeStringFormat(base.Style, value);
        }

        [Description("Gets or sets the horizontal alignment of the text.")]
        public DevExpress.Utils.HorzAlignment HorzAlignment
        {
            get => 
                PSConvert.ToHorzAlignment(this.StringFormat.Alignment);
            set => 
                this.StringFormat = this.StringFormat.ChangeAlignment(PSConvert.ToStringAlignment(value));
        }

        [Description("Gets or sets the vertical alignment of the text.")]
        public DevExpress.Utils.VertAlignment VertAlignment
        {
            get => 
                PSConvert.ToVertAlignment(this.StringFormat.LineAlignment);
            set => 
                this.StringFormat = this.StringFormat.ChangeLineAlignment(PSConvert.ToStringAlignment(value));
        }

        [Description("For internal use. Specifies the format settings that are applied to a document when it is exported to XLS format."), XtraSerializableProperty, DefaultValue(2)]
        public DefaultBoolean XlsExportNativeFormat
        {
            get => 
                (DefaultBoolean) base.flags[BrickBase.XlsExportNativeFormatSection];
            set => 
                base.flags[BrickBase.XlsExportNativeFormatSection] = (short) value;
        }

        [Description("Gets an object which represents the value that will be shown as the brick's text."), XtraSerializableProperty, DefaultValue((string) null), EditorBrowsable(EditorBrowsableState.Always)]
        public override object TextValue
        {
            get => 
                (this.textValue is DateTimeOffset) ? ((DateTimeOffset) this.textValue).DateTime : this.textValue;
            set
            {
                if (this.textValue != value)
                {
                    this.textValue = value;
                    this.OnTextValueChanged();
                }
            }
        }

        internal virtual DevExpress.XtraPrinting.Native.ConvertHelper ConvertHelper =>
            base.BrickOwner.ConvertHelper;

        [Description("Gets or sets the format string which is applied to the TextBrick.TextValue."), XtraSerializableProperty, DefaultValue((string) null)]
        public override string TextValueFormatString
        {
            get => 
                this.textValueFormatString;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.textValueFormatString = value;
                }
            }
        }

        [Description("Gets or sets the format settings used when a document is exported to an Excel file (XLS or XLSX)."), XtraSerializableProperty, DefaultValue((string) null)]
        public override string XlsxFormatString
        {
            get
            {
                string str = base.GetValue<string>(BrickAttachedProperties.XlsxFormatString, null);
                return ((!string.IsNullOrEmpty(str) || !(this.textValue is DateTimeOffset)) ? str : ExportUtils.GetDateTimeFormatString(((DateTimeOffset) this.textValue).Offset));
            }
            set => 
                base.SetAttachedValue<string>(BrickAttachedProperties.XlsxFormatString, value, null);
        }

        protected internal override bool ShouldApplyPaddingInternal =>
            true;

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Text";
    }
}

