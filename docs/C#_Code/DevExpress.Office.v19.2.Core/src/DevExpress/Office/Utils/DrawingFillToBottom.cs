namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillToBottom : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillToBottom()
        {
            base.Value = 0.0;
        }

        public DrawingFillToBottom(double value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

