namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeAnchorText : OfficeDrawingIntPropertyBase
    {
        public override bool Complex =>
            false;

        public MsoAnchor Anchor
        {
            get => 
                (MsoAnchor) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

