namespace DevExpress.XtraExport.Implementation
{
    using System;

    public class XlDrawingFill : XlDrawingFillBase
    {
        private XlDrawingFillType fillType;

        public XlDrawingFillType FillType
        {
            get => 
                this.fillType;
            set => 
                this.fillType = value;
        }
    }
}

