namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class DrawingGeometrySegmentInfo : OfficeDrawingTypedMsoArrayPropertyBase<MsoPathInfo>, IDrawingGeometrySegmentInfo, IOfficeDrawingTypedMsoArrayPropertyBase<MsoPathInfo>
    {
        private const int PathEscapeMask = 0x1f;
        private const int PathTypeMask = 0xe000;

        public override MsoPathInfo ReadElement(int offset, int size)
        {
            if (size != 2)
            {
                return new MsoPathInfo(MsoPathType.End, 0);
            }
            MsoPathType pathType = (MsoPathType) (base.ComplexData[offset + 1] >> 5);
            return (((pathType == MsoPathType.ClientEscape) || (pathType == MsoPathType.Escape)) ? new MsoPathInfo(((MsoPathEscape) base.ComplexData[offset + 1]) & (MsoPathEscape.LineColor | MsoPathEscape.QuadraticBezier), base.ComplexData[offset]) : new MsoPathInfo(pathType, BitConverter.ToInt16(base.ComplexData, offset) & -57345));
        }

        public override void WriteElement(MsoPathInfo element, List<byte> data)
        {
            ushort num = (ushort) (((ushort) element.PathType) << 13);
            if ((element.PathType == MsoPathType.ClientEscape) || (element.PathType == MsoPathType.Escape))
            {
                num = (ushort) (num | ((ushort) (((int) element.PathEscape) << 8)));
            }
            num = (ushort) (num | ((ushort) element.Segments));
            data.AddRange(BitConverter.GetBytes(num));
        }

        public override int ElementSize =>
            2;
    }
}

