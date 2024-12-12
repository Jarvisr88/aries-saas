namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Drawing;
    using System;

    public class DrawingLineArrowSize : OfficeDrawingIntPropertyBase
    {
        public const OutlineHeadTailSize DefaultSize = OutlineHeadTailSize.Medium;

        public DrawingLineArrowSize()
        {
            base.Value = 1;
        }

        public DrawingLineArrowSize(OutlineHeadTailSize headTailSize)
        {
            base.Value = (int) headTailSize;
        }

        public override bool Complex =>
            false;

        public OutlineHeadTailSize HeadTailSize
        {
            get => 
                (OutlineHeadTailSize) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

