namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public class DrawingGeometryGuides : OfficeDrawingTypedMsoArrayPropertyBase<ShapeGuide>, IDrawingGeometryGuides, IOfficeDrawingTypedMsoArrayPropertyBase<ShapeGuide>
    {
        public override ShapeGuide ReadElement(int offset, int size)
        {
            if ((size != 8) || ((offset + size) > base.ComplexData.Length))
            {
                return new ShapeGuide();
            }
            ShapeGuide guide = new ShapeGuide();
            ushort num = BitConverter.ToUInt16(base.ComplexData, offset);
            guide.CalculatedParam1 = (num & 0x2000) != 0;
            guide.CalculatedParam2 = (num & 0x4000) != 0;
            guide.CalculatedParam3 = (num & 0x8000) != 0;
            guide.Formula = ((ShapeGuideFormula) num) & ((ShapeGuideFormula) 0x1fff);
            guide.Param1 = BitConverter.ToUInt16(base.ComplexData, offset + 2);
            guide.Param2 = BitConverter.ToUInt16(base.ComplexData, offset + 4);
            guide.Param3 = BitConverter.ToUInt16(base.ComplexData, offset + 6);
            return guide;
        }

        public override void WriteElement(ShapeGuide element, List<byte> data)
        {
            ushort formula = (ushort) element.Formula;
            if (element.CalculatedParam1)
            {
                formula = (ushort) (formula | 0x2000);
            }
            if (element.CalculatedParam2)
            {
                formula = (ushort) (formula | 0x4000);
            }
            if (element.CalculatedParam3)
            {
                formula = (ushort) (formula | 0x8000);
            }
            data.AddRange(BitConverter.GetBytes(formula));
            data.AddRange(BitConverter.GetBytes((ushort) element.Param1));
            data.AddRange(BitConverter.GetBytes((ushort) element.Param2));
            data.AddRange(BitConverter.GetBytes((ushort) element.Param3));
        }

        public override int ElementSize =>
            8;
    }
}

