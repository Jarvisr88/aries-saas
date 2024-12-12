namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineCompoundType : OfficeDrawingIntPropertyBase
    {
        public override bool Complex =>
            false;

        public OutlineCompoundType CompoundType
        {
            get => 
                (OutlineCompoundType) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

