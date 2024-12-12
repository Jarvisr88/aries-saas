namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeHyperlink : OfficeDrawingIntPropertyBase
    {
        private void SetHyperlinkData(byte[] value)
        {
            base.SetComplexData(value);
            base.Value = base.ComplexData.Length;
        }

        public override bool Complex =>
            true;

        public byte[] HyperlinkData
        {
            get => 
                base.ComplexData;
            set => 
                this.SetHyperlinkData(value);
        }
    }
}

