namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Runtime.CompilerServices;

    public class BarCodeData : IBarCodeData
    {
        public BarCodeData(IBarCodeData barCodeData)
        {
            this.Module = barCodeData.Module;
            this.AutoModule = barCodeData.AutoModule;
            this.ShowText = barCodeData.ShowText;
            this.Text = barCodeData.Text;
            this.Alignment = barCodeData.Alignment;
            this.Orientation = barCodeData.Orientation;
            this.Style = barCodeData.Style;
        }

        public double Module { get; set; }

        public bool AutoModule { get; set; }

        public bool ShowText { get; set; }

        public string Text { get; set; }

        public TextAlignment Alignment { get; set; }

        public BarCodeOrientation Orientation { get; set; }

        public BrickStyle Style { get; set; }
    }
}

