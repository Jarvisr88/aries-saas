namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class DrawingFillShadeColors : OfficeDrawingTypedMsoArrayPropertyBase<OfficeShadeColor>
    {
        public override OfficeShadeColor ReadElement(int offset, int size) => 
            OfficeShadeColor.FromBytes(base.ComplexData, offset);

        public override void WriteElement(OfficeShadeColor element, List<byte> data)
        {
            data.AddRange(element.ColorRecord.GetBytes());
            FixedPoint point1 = new FixedPoint();
            point1.Value = element.Position;
            data.AddRange(point1.GetBytes());
        }

        public override bool Complex =>
            true;

        public override int ElementSize =>
            8;
    }
}

