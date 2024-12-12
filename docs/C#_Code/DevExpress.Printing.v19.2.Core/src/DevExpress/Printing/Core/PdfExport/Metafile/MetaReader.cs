namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MetaReader : BinaryReader
    {
        public MetaReader(Stream stream) : base(stream)
        {
        }

        public string ReadANSIString(int maxLength)
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            while (true)
            {
                if (num < maxLength)
                {
                    byte num2 = this.ReadByte();
                    if (num2 != 0)
                    {
                        builder.Append((char) num2);
                        num++;
                        continue;
                    }
                }
                return builder.ToString();
            }
        }

        public Color ReadColorBGR(bool alpha = false)
        {
            int blue = this.ReadByte();
            int green = this.ReadByte();
            int red = this.ReadByte();
            return Color.FromArgb(alpha ? this.ReadByte() : 0xff, red, green, blue);
        }

        public Color ReadColorRGB()
        {
            Color color = Color.FromArgb(0xff, this.ReadByte(), this.ReadByte(), this.ReadByte());
            this.ReadByte();
            return color;
        }

        public Color ReadEmfPlusARGB() => 
            this.ReadColorBGR(true);

        public Matrix ReadMatrix() => 
            new Matrix(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());

        public PointF ReadPointF() => 
            new PointF(this.ReadSingle(), this.ReadSingle());

        public PointF[] ReadPoints(long numberOfPoints, bool compressed)
        {
            PointF[] tfArray = new PointF[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                tfArray[i] = !compressed ? new PointF(this.ReadSingle(), this.ReadSingle()) : ((PointF) this.ReadPointXY());
            }
            return tfArray;
        }

        public Point ReadPointXY() => 
            new Point(this.ReadInt16(), this.ReadInt16());

        public Point ReadPointYX() => 
            new Point(this.ReadInt16(), this.ReadInt16());

        public RectangleF ReadRect() => 
            new RectangleF((float) this.ReadInt16(), (float) this.ReadInt16(), (float) this.ReadInt16(), (float) this.ReadInt16());

        public RectangleF[] ReadRectangles(bool compressed, long count)
        {
            RectangleF[] efArray = new RectangleF[count];
            for (int i = 0; i < count; i++)
            {
                efArray[i] = compressed ? this.ReadRect() : this.ReadRectF();
            }
            return efArray;
        }

        public RectangleF ReadRectF() => 
            new RectangleF(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());

        public float[] ReadSingleArray(int count)
        {
            float[] numArray = new float[count];
            for (int i = 0; i < count; i++)
            {
                numArray[i] = this.ReadSingle();
            }
            return numArray;
        }

        public byte[] ReadToEnd()
        {
            long num = this.BaseStream.Length - this.BaseStream.Position;
            return this.ReadBytes((int) num);
        }
    }
}

