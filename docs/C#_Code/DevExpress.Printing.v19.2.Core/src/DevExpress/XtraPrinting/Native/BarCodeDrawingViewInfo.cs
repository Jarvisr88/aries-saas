namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BarCodeDrawingViewInfo
    {
        public BarCodeDrawingViewInfo(RectangleF bounds, IBarCodeData data)
        {
            this.Data = data;
            this.Bounds = bounds;
        }

        public bool Calculate(BarCodeGeneratorBase generator, IGraphicsBase gr) => 
            generator.CalculateDrawingViewInfo(this, gr);

        public string GetErrorText()
        {
            switch (this.BarCodeError)
            {
                case DevExpress.XtraPrinting.BarCode.BarCodeError.InvalidText:
                    return PreviewLocalizer.GetString(PreviewStringId.Msg_InvalidBarcodeText);

                case DevExpress.XtraPrinting.BarCode.BarCodeError.InvalidTextFormat:
                    return PreviewLocalizer.GetString(PreviewStringId.Msg_InvalidBarcodeTextFormat);

                case DevExpress.XtraPrinting.BarCode.BarCodeError.InvalidData:
                    return string.Format(PreviewLocalizer.GetString(PreviewStringId.Msg_InvalidBarcodeData), this.ErrorInfo);

                case DevExpress.XtraPrinting.BarCode.BarCodeError.ControlBoundsTooSmall:
                    return PreviewLocalizer.GetString(PreviewStringId.Msg_CantFitBarcodeToControlBounds);

                case DevExpress.XtraPrinting.BarCode.BarCodeError.IncompatibleSettings:
                    return string.Format(PreviewLocalizer.GetString(PreviewStringId.Msg_IncompatibleBarcodeSettings), this.ErrorInfo);
            }
            return string.Empty;
        }

        public static void SetConvertSpacingUnits(BarCodeGeneratorBase generator, bool value)
        {
            EAN13Generator generator2 = generator as EAN13Generator;
            if (generator2 != null)
            {
                generator2.ConvertSpacingUnits = value;
            }
        }

        public static void SetDoNotClip(BarCodeGeneratorBase generator, bool value)
        {
            generator.DoNotClip = value;
        }

        public static void SetForceEnoughSpace(BarCodeGeneratorBase generator, bool value)
        {
            generator.ForceEnoughSpace = value;
        }

        public IBarCodeData Data { get; private set; }

        public RectangleF Bounds { get; private set; }

        public RectangleF BarBounds { get; set; }

        public RectangleF TextBounds { get; set; }

        public float XModule { get; set; }

        public float YModule { get; set; }

        public DevExpress.XtraPrinting.BarCode.BarCodeError BarCodeError { get; set; }

        public object ErrorInfo { get; set; }

        public SizeF BestSize { get; set; }
    }
}

