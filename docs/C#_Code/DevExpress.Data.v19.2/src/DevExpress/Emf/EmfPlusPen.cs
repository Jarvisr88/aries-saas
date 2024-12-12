namespace DevExpress.Emf
{
    using System;
    using System.IO;

    public class EmfPlusPen : EmfPlusObject, IDisposable
    {
        private readonly DXPen pen;

        public EmfPlusPen(DXPen pen)
        {
            this.pen = pen;
        }

        public EmfPlusPen(EmfPlusReader reader)
        {
            Stream baseStream = reader.BaseStream;
            baseStream.Position += 8L;
            EmfPlusPenDataFlags flags = (EmfPlusPenDataFlags) reader.ReadInt32();
            reader.ReadInt32();
            this.pen = new DXPen(ARGBColor.FromArgb(0, 0, 0), (double) reader.ReadSingle());
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataTransform))
            {
                reader.ReadBytes(0x18);
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataStartCap))
            {
                this.pen.StartCap = (DXLineCap) reader.ReadInt32();
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataEndCap))
            {
                this.pen.EndCap = (DXLineCap) reader.ReadInt32();
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataJoin))
            {
                this.pen.LineJoin = (DXLineJoin) reader.ReadInt32();
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataMiterLimit))
            {
                this.pen.MiterLimit = reader.ReadSingle();
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataLineStyle))
            {
                this.pen.DashStyle = (DXDashStyle) reader.ReadInt32();
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataDashedLineCap))
            {
                this.pen.DashCap = (DXDashCap) reader.ReadInt32();
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataDashedLineOffset))
            {
                this.pen.DashOffset = reader.ReadSingle();
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataDashedLine))
            {
                int num = reader.ReadInt32();
                float[] numArray = new float[num];
                int index = 0;
                while (true)
                {
                    if (index >= num)
                    {
                        this.pen.DashPattern = numArray;
                        break;
                    }
                    numArray[index] = reader.ReadSingle();
                    index++;
                }
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataNonCenter))
            {
                int num3 = reader.ReadInt32();
                this.pen.Alignment = (num3 == 2) ? DXPenAlignment.Left : ((num3 == 3) ? DXPenAlignment.Outset : ((DXPenAlignment) num3));
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataCompoundLine))
            {
                reader.ReadBytes(reader.ReadInt32() * 4);
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataCustomStartCap))
            {
                reader.ReadBytes(reader.ReadInt32());
            }
            if (flags.HasFlag(EmfPlusPenDataFlags.PenDataCustomEndCap))
            {
                reader.ReadBytes(reader.ReadInt32());
            }
            this.pen.Brush = EmfPlusBrush.Create(reader).Brush;
        }

        public void Dispose()
        {
            this.pen.Dispose();
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(-608169982);
            bool shouldWriteStartCap = this.ShouldWriteStartCap;
            bool shouldWriteEndCap = this.ShouldWriteEndCap;
            bool shouldWriteLineJoing = this.ShouldWriteLineJoing;
            bool shouldWriteDashStyle = this.ShouldWriteDashStyle;
            bool shouldWriteAlignment = this.ShouldWriteAlignment;
            EmfPlusPenDataFlags flags = 0;
            if (shouldWriteStartCap)
            {
                flags |= EmfPlusPenDataFlags.PenDataStartCap;
            }
            if (shouldWriteEndCap)
            {
                flags |= EmfPlusPenDataFlags.PenDataEndCap;
            }
            if (shouldWriteLineJoing)
            {
                flags |= EmfPlusPenDataFlags.PenDataJoin;
            }
            if (shouldWriteDashStyle)
            {
                flags |= EmfPlusPenDataFlags.PenDataLineStyle;
            }
            if (shouldWriteAlignment)
            {
                flags |= EmfPlusPenDataFlags.PenDataNonCenter;
            }
            writer.Write(0);
            writer.Write((int) flags);
            writer.Write(0);
            writer.Write((float) this.pen.Width);
            if (shouldWriteStartCap)
            {
                writer.Write((int) this.pen.StartCap);
            }
            if (shouldWriteEndCap)
            {
                writer.Write((int) this.pen.EndCap);
            }
            if (shouldWriteLineJoing)
            {
                writer.Write((int) this.pen.LineJoin);
            }
            if (shouldWriteDashStyle)
            {
                writer.Write((int) this.pen.DashStyle);
            }
            if (shouldWriteAlignment)
            {
                DXPenAlignment alignment = this.pen.Alignment;
                if (alignment == DXPenAlignment.Outset)
                {
                    writer.Write(3);
                }
                else if (alignment != DXPenAlignment.Left)
                {
                    writer.Write((int) this.pen.Alignment);
                }
                else
                {
                    writer.Write(2);
                }
            }
            new EmfPlusBrush(this.pen.Brush).Write(writer);
        }

        public DXPen Pen =>
            this.pen;

        public override EmfPlusObjectType Type =>
            EmfPlusObjectType.ObjectTypePen;

        public override int Size
        {
            get
            {
                int num = (20 + this.pen.Brush.DataSize) + 4;
                if (this.ShouldWriteStartCap)
                {
                    num += 4;
                }
                if (this.ShouldWriteEndCap)
                {
                    num += 4;
                }
                if (this.ShouldWriteLineJoing)
                {
                    num += 4;
                }
                if (this.ShouldWriteDashStyle)
                {
                    num += 4;
                }
                if (this.ShouldWriteAlignment)
                {
                    num += 4;
                }
                return num;
            }
        }

        private bool ShouldWriteStartCap =>
            this.pen.StartCap != DXLineCap.Flat;

        private bool ShouldWriteEndCap =>
            this.pen.EndCap != DXLineCap.Flat;

        private bool ShouldWriteLineJoing =>
            this.pen.LineJoin != DXLineJoin.Miter;

        private bool ShouldWriteDashStyle =>
            this.pen.DashStyle != DXDashStyle.Solid;

        private bool ShouldWriteAlignment =>
            this.pen.Alignment != DXPenAlignment.Center;
    }
}

