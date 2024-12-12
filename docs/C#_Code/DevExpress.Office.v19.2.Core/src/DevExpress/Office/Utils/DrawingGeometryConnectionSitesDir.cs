namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class DrawingGeometryConnectionSitesDir : OfficeDrawingTypedMsoArrayPropertyBase<FixedPoint>, IDrawingGeometryConnectionSitesDir, IOfficeDrawingTypedMsoArrayPropertyBase<FixedPoint>
    {
        public override FixedPoint ReadElement(int offset, int size) => 
            (size == 4) ? FixedPoint.FromBytes(base.ComplexData, offset) : new FixedPoint();

        public override void WriteElement(FixedPoint element, List<byte> data)
        {
            data.AddRange(element.GetBytes());
        }

        public override int ElementSize =>
            4;
    }
}

