namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingCxStyle : OfficeDrawingIntPropertyBase
    {
        public const MsoCxStyle DefaultStyle = MsoCxStyle.None;

        public override bool Complex =>
            false;

        public MsoCxStyle Style
        {
            get => 
                (MsoCxStyle) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

