namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineArrowhead : OfficeDrawingIntPropertyBase
    {
        public const MsoLineEnd DefaultArrowhead = MsoLineEnd.NoEnd;

        public DrawingLineArrowhead()
        {
            base.Value = 0;
        }

        public DrawingLineArrowhead(MsoLineEnd arrowhead)
        {
            base.Value = (int) arrowhead;
        }

        public override bool Complex =>
            false;

        public MsoLineEnd Arrowhead
        {
            get => 
                (MsoLineEnd) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

