namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Helpers;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Text;

    public class BrickStringFormat : ICloneable, IDisposable
    {
        private static readonly StringFormat DefaultStringFormat = new StringFormat();
        private static readonly StringAlignment DefaultAlignment = DefaultStringFormat.Alignment;
        private static readonly StringAlignment DefaultLineAlignment = DefaultStringFormat.LineAlignment;
        private static readonly StringFormatFlags DefaultFormatFlags = DefaultStringFormat.FormatFlags;
        private static readonly System.Drawing.Text.HotkeyPrefix DefaultHotkeyPrefix = DefaultStringFormat.HotkeyPrefix;
        private static readonly StringTrimming DefaultTrimming = DefaultStringFormat.Trimming;
        private static readonly StringFormat[] prototypes = new StringFormat[] { DefaultStringFormat, StringFormat.GenericDefault, StringFormat.GenericTypographic };
        private static readonly int AlignmentOffset;
        private static readonly int LineAlignmentOffset;
        private static readonly int FormatFlagsOffset;
        private static readonly int HotkeyPrefixOffset;
        private static readonly int TrimmingOffset;
        private static readonly int PrototypeKindOffset;
        private StringFormat value;
        private StringAlignment alignment;
        private StringAlignment lineAlignment;
        private StringFormatFlags formatFlags;
        private System.Drawing.Text.HotkeyPrefix hotkeyPrefix;
        private StringTrimming trimming;
        private BrickStringFormatPrototypeKind prototypeKind;
        private float[] tabStops;

        static BrickStringFormat()
        {
            BitVector32.Section previous = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(StringAlignment)));
            AlignmentOffset = previous.Offset;
            previous = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(StringAlignment)), previous);
            LineAlignmentOffset = previous.Offset;
            previous = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(StringFormatFlags)), previous);
            FormatFlagsOffset = previous.Offset;
            previous = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(System.Drawing.Text.HotkeyPrefix)), previous);
            HotkeyPrefixOffset = previous.Offset;
            previous = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(StringTrimming)), previous);
            TrimmingOffset = previous.Offset;
            PrototypeKindOffset = BitVector32.CreateSection(ReportSpecificEnumHelper.GetEnumMaxValue(typeof(BrickStringFormatPrototypeKind)), previous).Offset;
        }

        public BrickStringFormat()
        {
            this.alignment = DefaultAlignment;
            this.lineAlignment = DefaultLineAlignment;
            this.formatFlags = DefaultFormatFlags;
            this.hotkeyPrefix = DefaultHotkeyPrefix;
            this.trimming = DefaultTrimming;
        }

        public BrickStringFormat(BrickStringFormat source) : this(source.Alignment, source.LineAlignment, source.FormatFlags, source.HotkeyPrefix, source.Trimming)
        {
            this.PrototypeKind = source.PrototypeKind;
        }

        public BrickStringFormat(StringAlignment alignment) : this(alignment, DefaultLineAlignment, DefaultFormatFlags, DefaultHotkeyPrefix, DefaultTrimming)
        {
        }

        public BrickStringFormat(StringFormat source) : this(source.Alignment, source.LineAlignment, source.FormatFlags, source.HotkeyPrefix, source.Trimming)
        {
        }

        public BrickStringFormat(StringFormatFlags options) : this(DefaultAlignment, DefaultLineAlignment, options, DefaultHotkeyPrefix, DefaultTrimming)
        {
        }

        public BrickStringFormat(BrickStringFormat source, StringFormatFlags options) : this(source.Alignment, source.LineAlignment, options, source.HotkeyPrefix, source.Trimming)
        {
            this.PrototypeKind = source.PrototypeKind;
        }

        public BrickStringFormat(BrickStringFormat source, StringTrimming trimming) : this(source.Alignment, source.LineAlignment, source.FormatFlags, source.HotkeyPrefix, trimming)
        {
            this.PrototypeKind = source.PrototypeKind;
        }

        public BrickStringFormat(StringAlignment alignment, StringAlignment lineAlignment) : this(alignment, lineAlignment, DefaultFormatFlags, DefaultHotkeyPrefix, DefaultTrimming)
        {
        }

        public BrickStringFormat(BrickStringFormat source, StringAlignment alignment, StringAlignment lineAlignment) : this(alignment, lineAlignment, source.FormatFlags, source.HotkeyPrefix, source.Trimming)
        {
            this.PrototypeKind = source.PrototypeKind;
        }

        public BrickStringFormat(StringFormatFlags options, StringAlignment alignment, StringAlignment lineAlignment) : this(alignment, lineAlignment, options, DefaultHotkeyPrefix, DefaultTrimming)
        {
        }

        public BrickStringFormat(StringFormatFlags options, StringAlignment alignment, StringAlignment lineAlignment, StringTrimming trimming) : this(alignment, lineAlignment, options, DefaultHotkeyPrefix, trimming)
        {
        }

        public BrickStringFormat(StringAlignment alignment, StringAlignment lineAlignment, StringFormatFlags formatFlags, System.Drawing.Text.HotkeyPrefix hotkeyPrefix, StringTrimming trimming)
        {
            this.alignment = DefaultAlignment;
            this.lineAlignment = DefaultLineAlignment;
            this.formatFlags = DefaultFormatFlags;
            this.hotkeyPrefix = DefaultHotkeyPrefix;
            this.trimming = DefaultTrimming;
            this.alignment = alignment;
            this.lineAlignment = lineAlignment;
            this.formatFlags = formatFlags;
            this.hotkeyPrefix = hotkeyPrefix;
            this.trimming = trimming;
        }

        public BrickStringFormat ChangeAlignment(StringAlignment newAlignment) => 
            new BrickStringFormat(this, newAlignment, this.LineAlignment);

        public BrickStringFormat ChangeAlignment(StringAlignment newAlignment, StringAlignment newLineAlignment) => 
            new BrickStringFormat(this, newAlignment, newLineAlignment);

        public BrickStringFormat ChangeFormatFlags(StringFormatFlags options) => 
            new BrickStringFormat(this, options);

        public BrickStringFormat ChangeLineAlignment(StringAlignment newLineAlignment) => 
            new BrickStringFormat(this, this.Alignment, newLineAlignment);

        private void CheckNullValue()
        {
            if (this.value != null)
            {
                throw new InvalidOperationException();
            }
        }

        internal void ClearTabStops()
        {
            this.SetTabStops(new float[0]);
        }

        public virtual object Clone() => 
            new BrickStringFormat(this);

        public static BrickStringFormat Create(TextAlignment textAlignment, bool wordWrap) => 
            Create(textAlignment, wordWrap, StringTrimming.Character);

        public static BrickStringFormat Create(TextAlignment textAlignment, bool wordWrap, StringTrimming trimming) => 
            Create(textAlignment, wordWrap, trimming, false);

        public static BrickStringFormat Create(TextAlignment textAlignment, StringFormatFlags formatFlags, StringTrimming trimming) => 
            new BrickStringFormat(formatFlags, GraphicsConvertHelper.ToHorzStringAlignment(textAlignment), GraphicsConvertHelper.ToVertStringAlignment(textAlignment), trimming) { PrototypeKind = BrickStringFormatPrototypeKind.GenericTypographic };

        public static BrickStringFormat Create(TextAlignment textAlignment, bool wordWrap, StringTrimming trimming, bool rightToLeft)
        {
            StringFormatFlags formatFlags = StringFormatFlags.NoClip | StringFormatFlags.LineLimit | StringFormatFlags.FitBlackBox;
            if (!wordWrap)
            {
                formatFlags |= StringFormatFlags.NoWrap;
            }
            if (rightToLeft)
            {
                formatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            return Create(textAlignment, formatFlags, trimming);
        }

        public virtual void Dispose()
        {
            if (this.value != null)
            {
                this.value.Dispose();
                this.value = null;
            }
            GC.SuppressFinalize(this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BrickStringFormat))
            {
                return false;
            }
            BrickStringFormat format = (BrickStringFormat) obj;
            return ((this.Alignment == format.Alignment) && ((this.LineAlignment == format.LineAlignment) && ((this.FormatFlags == format.FormatFlags) && ((this.HotkeyPrefix == format.HotkeyPrefix) && ((this.Trimming == format.Trimming) && (this.prototypeKind == format.PrototypeKind))))));
        }

        public override int GetHashCode() => 
            (((((((int) this.alignment) << (AlignmentOffset & 0x1f)) | (((int) this.lineAlignment) << (LineAlignmentOffset & 0x1f))) | (((int) this.formatFlags) << (FormatFlagsOffset & 0x1f))) | (((int) this.hotkeyPrefix) << (HotkeyPrefixOffset & 0x1f))) | (((int) this.trimming) << (TrimmingOffset & 0x1f))) | (((int) this.prototypeKind) << (PrototypeKindOffset & 0x1f));

        internal void SetTabStops(float[] tabStops)
        {
            if ((this.tabStops == null) || !ArrayHelper.ArraysEqual<float>(this.tabStops, tabStops))
            {
                this.tabStops = tabStops;
                this.Value.SetTabStops(0f, tabStops);
            }
        }

        [Browsable(false)]
        public bool WordWrap =>
            (this.FormatFlags & StringFormatFlags.NoWrap) == 0;

        [Browsable(false)]
        public bool RightToLeft =>
            (this.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0;

        [Description("Gets or sets text alignment information.")]
        public StringAlignment Alignment =>
            (this.value != null) ? this.value.Alignment : this.alignment;

        [Description("Gets or sets line alignment.")]
        public StringAlignment LineAlignment =>
            (this.value != null) ? this.value.LineAlignment : this.lineAlignment;

        [Description("Gets or sets a StringFormatFlags enumeration that contains formatting information.")]
        public StringFormatFlags FormatFlags =>
            (this.value != null) ? this.value.FormatFlags : this.formatFlags;

        [Description("Gets the HotkeyPrefix object for this BrickStringFormat object.")]
        public System.Drawing.Text.HotkeyPrefix HotkeyPrefix =>
            (this.value != null) ? this.value.HotkeyPrefix : this.hotkeyPrefix;

        [Description("Gets text trimming mode.")]
        public StringTrimming Trimming =>
            (this.value != null) ? this.value.Trimming : this.trimming;

        [Description("Gets the StringFormat instance representing current text formatting.")]
        public StringFormat Value
        {
            get
            {
                if (this.value == null)
                {
                    this.value = new StringFormat(prototypes[(int) this.prototypeKind]);
                    this.value.FormatFlags = this.formatFlags;
                    this.value.Alignment = this.alignment;
                    this.value.LineAlignment = this.lineAlignment;
                    this.value.HotkeyPrefix = this.hotkeyPrefix;
                    this.value.Trimming = this.trimming;
                }
                return this.value;
            }
        }

        [Description("Gets or sets a prototype string format for the current string format.")]
        public BrickStringFormatPrototypeKind PrototypeKind
        {
            get => 
                this.prototypeKind;
            set
            {
                this.CheckNullValue();
                this.prototypeKind = value;
            }
        }
    }
}

