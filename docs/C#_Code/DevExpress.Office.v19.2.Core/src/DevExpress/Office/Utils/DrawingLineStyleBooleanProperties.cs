namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public class DrawingLineStyleBooleanProperties : OfficeDrawingBooleanPropertyBase
    {
        public const bool DefaultLineValue = true;
        private DrawingLineStyle lineStyle = DrawingLineStyle.UseLine;

        public override void Execute(OfficeArtPropertiesBase owner)
        {
            IOfficeArtProperties properties = owner as IOfficeArtProperties;
            if (properties != null)
            {
                properties.Line = this.Line;
                properties.UseLine = this.UseLine;
            }
        }

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);
            this.lineStyle = (DrawingLineStyle) base.Value;
        }

        public override void Write(BinaryWriter writer)
        {
            base.Value = (int) this.lineStyle;
            base.Write(writer);
        }

        public override bool Complex =>
            false;

        public bool Line
        {
            get => 
                (this.lineStyle & DrawingLineStyle.Line) == DrawingLineStyle.Line;
            set
            {
                if (value)
                {
                    this.lineStyle |= DrawingLineStyle.Line;
                }
                else
                {
                    this.lineStyle &= ~DrawingLineStyle.Line;
                }
            }
        }

        public bool UseLine
        {
            get => 
                (this.lineStyle & DrawingLineStyle.UseLine) == DrawingLineStyle.UseLine;
            set
            {
                if (value)
                {
                    this.lineStyle |= DrawingLineStyle.UseLine;
                }
                else
                {
                    this.lineStyle &= ~DrawingLineStyle.UseLine;
                }
            }
        }

        [Flags]
        public enum DrawingLineStyle
        {
            Line = 8,
            ArrowheadOK = 0x10,
            UseLine = 0x80000,
            UseArrowheadOK = 0x100000
        }
    }
}

