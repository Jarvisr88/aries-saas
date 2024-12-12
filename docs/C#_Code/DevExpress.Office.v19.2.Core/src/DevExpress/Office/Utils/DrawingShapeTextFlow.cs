namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeTextFlow : OfficeDrawingIntPropertyBase
    {
        public override bool Complex =>
            false;

        public MsoTextFlow TextFlow
        {
            get => 
                (MsoTextFlow) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

