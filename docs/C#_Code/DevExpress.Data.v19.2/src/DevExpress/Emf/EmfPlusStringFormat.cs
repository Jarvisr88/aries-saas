namespace DevExpress.Emf
{
    using System;

    public class EmfPlusStringFormat : EmfPlusObject
    {
        private const float defaultMarginFactor = 0.1666667f;
        private const float defaultTracking = 1.03f;
        private readonly EmfPlusStringFormatFlags formatFlags;
        private readonly EmfPlusStringAlignment alignment;
        private readonly EmfPlusStringAlignment lineAlignment;
        private readonly EmfPlusHotkeyPrefix hotkeyPrefix;
        private readonly float leadingMarginFactor;
        private readonly float trailingMarginFactor;
        private readonly EmfPlusStringTrimming trimming;
        private readonly float firstTabOffset;
        private readonly float[] tabStops;

        public EmfPlusStringFormat(EmfPlusReader reader)
        {
            reader.ReadInt32();
            this.formatFlags = (EmfPlusStringFormatFlags) reader.ReadInt32();
            reader.ReadInt32();
            this.alignment = (EmfPlusStringAlignment) reader.ReadInt32();
            this.lineAlignment = (EmfPlusStringAlignment) reader.ReadInt32();
            reader.ReadBytes(8);
            this.firstTabOffset = reader.ReadSingle();
            this.hotkeyPrefix = (EmfPlusHotkeyPrefix) reader.ReadInt32();
            this.leadingMarginFactor = reader.ReadSingle();
            this.trailingMarginFactor = reader.ReadSingle();
            reader.ReadSingle();
            this.trimming = (EmfPlusStringTrimming) reader.ReadInt32();
            int num = reader.ReadInt32();
            reader.ReadInt32();
            if (num > 0)
            {
                this.tabStops = new float[num];
                for (int i = 0; i < num; i++)
                {
                    this.tabStops[i] = reader.ReadSingle();
                }
            }
        }

        public EmfPlusStringFormat(EmfPlusStringFormatFlags formatFlags, EmfPlusStringAlignment alignment, EmfPlusStringAlignment lineAlignment, EmfPlusStringTrimming trimming, float firstTabOffset, float[] tabStops)
        {
            this.formatFlags = formatFlags;
            this.alignment = alignment;
            this.lineAlignment = lineAlignment;
            this.firstTabOffset = firstTabOffset;
            this.hotkeyPrefix = EmfPlusHotkeyPrefix.None;
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                this.leadingMarginFactor = 0f;
                this.trailingMarginFactor = 0f;
            }
            else
            {
                this.leadingMarginFactor = 0.1666667f;
                this.trailingMarginFactor = 0.1666667f;
            }
            this.trimming = trimming;
            this.tabStops = tabStops;
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(-608169982);
            writer.Write((int) this.formatFlags);
            writer.Write(0);
            writer.Write((int) this.alignment);
            writer.Write((int) this.lineAlignment);
            writer.Write(new byte[8]);
            writer.Write(this.firstTabOffset);
            writer.Write((int) this.hotkeyPrefix);
            writer.Write(this.leadingMarginFactor);
            writer.Write(this.trailingMarginFactor);
            writer.Write((float) 1.03f);
            writer.Write((int) this.trimming);
            int num = (this.tabStops != null) ? this.tabStops.Length : 0;
            writer.Write(num);
            writer.Write(0);
            for (int i = 0; i < num; i++)
            {
                writer.Write(this.tabStops[i]);
            }
        }

        public EmfPlusStringFormatFlags FormatFlags =>
            this.formatFlags;

        public EmfPlusStringAlignment Alignment =>
            this.alignment;

        public EmfPlusStringAlignment LineAlignment =>
            this.lineAlignment;

        public EmfPlusHotkeyPrefix HotkeyPrefix =>
            this.hotkeyPrefix;

        public float LeadingMarginFactor =>
            this.leadingMarginFactor;

        public float TrailingMarginFactor =>
            this.trailingMarginFactor;

        public EmfPlusStringTrimming Trimming =>
            this.trimming;

        public float FirstTabOffset =>
            this.firstTabOffset;

        public float[] TabStops =>
            this.tabStops;

        public override EmfPlusObjectType Type =>
            EmfPlusObjectType.ObjectTypeStringFormat;

        public override int Size =>
            4 * (15 + ((this.tabStops != null) ? this.tabStops.Length : 0));
    }
}

