namespace DevExpress.Emf
{
    using System;
    using System.IO;

    public class EmfPlusPath : EmfPlusObject
    {
        private const short windingFlagMask = 0x2000;
        private readonly DXGraphicsPathData pathData;

        public EmfPlusPath(DXGraphicsPathData pathData)
        {
            this.pathData = pathData;
        }

        public EmfPlusPath(EmfPlusReader reader)
        {
            reader.ReadInt32();
            int count = reader.ReadInt32();
            DXPathPointTypes[] types = new DXPathPointTypes[count];
            short flags = reader.ReadInt16();
            reader.ReadInt16();
            DXPointF[] points = reader.ReadPoints(count, flags);
            int num4 = 0;
            while (num4 < count)
            {
                types[num4++] = (DXPathPointTypes) reader.ReadByte();
            }
            this.pathData = new DXGraphicsPathData(points, types, (flags & 0x2000) != 0);
            Stream baseStream = reader.BaseStream;
            baseStream.Position = ((baseStream.Position + 3L) >> 2) << 2;
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(-608169982);
            DXPointF[] points = this.pathData.Points;
            int length = points.Length;
            writer.Write(length);
            writer.Write(this.pathData.IsWindingFillMode ? 0x2000 : 0);
            writer.Write(points);
            foreach (DXPathPointTypes types in this.pathData.PathTypes)
            {
                writer.Write((byte) types);
            }
            int num2 = 4 - (length % 4);
            if (num2 != 4)
            {
                writer.Write(new byte[num2]);
            }
        }

        public DXGraphicsPathData PathData =>
            this.pathData;

        public override int Size
        {
            get
            {
                int length = this.pathData.Points.Length;
                return ((((length * 2) + 3) + ((int) Math.Ceiling((double) (((double) length) / 4.0)))) * 4);
            }
        }

        public override EmfPlusObjectType Type =>
            EmfPlusObjectType.ObjectTypePath;
    }
}

