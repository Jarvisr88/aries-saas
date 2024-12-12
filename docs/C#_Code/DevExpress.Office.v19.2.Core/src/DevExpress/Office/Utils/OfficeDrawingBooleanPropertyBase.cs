namespace DevExpress.Office.Utils
{
    using System;

    public abstract class OfficeDrawingBooleanPropertyBase : OfficeDrawingIntPropertyBase
    {
        private const uint useMask = 0xffff0000;

        protected OfficeDrawingBooleanPropertyBase()
        {
        }

        public override void Merge(IOfficeDrawingProperty other)
        {
            OfficeDrawingBooleanPropertyBase base2 = other as OfficeDrawingBooleanPropertyBase;
            if (base2 != null)
            {
                uint num2 = (uint) base.Value;
                uint num = ((uint) base2.Value) & ~(num2 & 0xffff0000);
                uint num3 = (uint) ((num & -65536) >> 0x10);
                num2 = (num2 & ~num3) | (num & (0xffff0000 | num3));
                base.Value = (int) num2;
            }
        }
    }
}

