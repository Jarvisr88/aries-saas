namespace DevExpress.Emf
{
    using System;
    using System.IO;

    public class EmfPlusReader : BinaryReader
    {
        private const short relativeLocationFlagMask = 0x800;
        private const short compressedFlagMask = 0x4000;

        public EmfPlusReader(Stream stream) : base(stream)
        {
        }

        public ARGBColor ReadArgbColor() => 
            ARGBColor.FromArgb(this.ReadInt32());

        public DXBlend ReadBlend()
        {
            int num = this.ReadInt32();
            double[] positions = new double[num];
            double[] factors = new double[num];
            for (int i = 0; i < num; i++)
            {
                positions[i] = this.ReadSingle();
            }
            for (int j = 0; j < num; j++)
            {
                factors[j] = this.ReadSingle();
            }
            return new DXBlend(positions, factors);
        }

        public DXColorBlend ReadColorBlend()
        {
            int count = this.ReadInt32();
            DXColorBlend blend = new DXColorBlend(count);
            double[] positions = blend.Positions;
            for (int i = 0; i < count; i++)
            {
                positions[i] = this.ReadSingle();
            }
            ARGBColor[] colors = blend.Colors;
            for (int j = 0; j < count; j++)
            {
                colors[j] = this.ReadArgbColor();
            }
            return blend;
        }

        public DXPointF ReadDxPointF() => 
            this.ReadDxPointF(false);

        public DXPointF ReadDxPointF(bool compressed) => 
            !compressed ? new DXPointF(this.ReadSingle(), this.ReadSingle()) : new DXPointF((float) this.ReadInt16(), (float) this.ReadInt16());

        public DXRectangleF ReadDXRectangleF(bool compressed) => 
            !compressed ? new DXRectangleF(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle()) : new DXRectangleF((float) this.ReadInt16(), (float) this.ReadInt16(), (float) this.ReadInt16(), (float) this.ReadInt16());

        private int ReadEmfPlusInt()
        {
            byte num = this.ReadByte();
            byte num2 = (byte) (num >> 1);
            if ((num & 1) == 0)
            {
                return (((num & 0x80) == 0) ? num2 : (num2 | -128));
            }
            int num3 = num2 | (this.ReadByte() << 7);
            return (((num3 & 0x4000) == 0) ? num3 : (num3 | -16384));
        }

        public DXPointF[] ReadPoints(int count, short flags)
        {
            DXPointF[] tfArray = new DXPointF[count];
            if ((flags & 0x800) == 0)
            {
                bool compressed = (flags & 0x4000) != 0;
                for (int i = 0; i < count; i++)
                {
                    tfArray[i] = this.ReadDxPointF(compressed);
                }
            }
            else
            {
                DXPointF tf = new DXPointF((float) this.ReadEmfPlusInt(), (float) this.ReadEmfPlusInt());
                tfArray[0] = tf;
                for (int i = 1; i < count; i++)
                {
                    tfArray[i] = new DXPointF(tf.X + this.ReadEmfPlusInt(), tf.Y + this.ReadEmfPlusInt());
                }
            }
            return tfArray;
        }

        public DXTransformationMatrix ReadTransformMatrix() => 
            new DXTransformationMatrix(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());
    }
}

