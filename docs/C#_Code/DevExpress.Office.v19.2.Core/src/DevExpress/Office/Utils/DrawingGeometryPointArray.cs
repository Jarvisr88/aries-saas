namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class DrawingGeometryPointArray : OfficeDrawingTypedMsoArrayPropertyBase<DrawingGeometryPoint>, IDrawingGeometryPointArray, IOfficeDrawingTypedMsoArrayPropertyBase<DrawingGeometryPoint>
    {
        public override DrawingGeometryPoint ReadElement(int offset, int size)
        {
            DrawingGeometryPoint point = new DrawingGeometryPoint();
            if (size == 4)
            {
                point.X = DrawingGeometryCoordinate.FromBytes(base.ComplexData, offset, 2);
                point.Y = DrawingGeometryCoordinate.FromBytes(base.ComplexData, offset + 2, 2);
            }
            else if (size == 8)
            {
                point.X = DrawingGeometryCoordinate.FromBytes(base.ComplexData, offset, 4);
                point.Y = DrawingGeometryCoordinate.FromBytes(base.ComplexData, offset + 4, 4);
            }
            return point;
        }

        public override void WriteElement(DrawingGeometryPoint element, List<byte> data)
        {
            data.AddRange(element.X.GetBytes());
            data.AddRange(element.Y.GetBytes());
        }

        public override int ElementSize =>
            8;
    }
}

