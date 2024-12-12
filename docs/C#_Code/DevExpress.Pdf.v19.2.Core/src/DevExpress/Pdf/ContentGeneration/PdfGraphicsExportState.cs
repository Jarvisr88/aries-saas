namespace DevExpress.Pdf.ContentGeneration
{
    using System;

    public class PdfGraphicsExportState
    {
        private double lineWidth;
        private double[] strokingColorComponents;
        private double strokingAlpha;
        private double[] nonStrokingColorComponents;
        private double nonStrokingAlpha;
        private DXDashStyle? dashStyle;
        private DXDashCap? dashCap;
        private DXLineCap? lineCap;
        private DXLineJoin? lineJoin;
        private double miterLimit;
        private IPdfLineCapPainter startCapPainter;
        private IPdfLineCapPainter endCapPainter;
        private PdfBrushContainer currentBrush;
        private bool isInsetPen;
        private double actualLineWidth;

        public PdfGraphicsExportState()
        {
            this.lineWidth = -1.0;
            this.strokingAlpha = 1.0;
            this.nonStrokingAlpha = 1.0;
        }

        public PdfGraphicsExportState(PdfGraphicsExportState s)
        {
            this.lineWidth = -1.0;
            this.strokingAlpha = 1.0;
            this.nonStrokingAlpha = 1.0;
            double lineWidth = s.lineWidth;
            this.strokingColorComponents = s.strokingColorComponents;
            this.strokingAlpha = s.strokingAlpha;
            this.nonStrokingColorComponents = s.nonStrokingColorComponents;
            this.nonStrokingAlpha = s.nonStrokingAlpha;
            this.dashStyle = s.dashStyle;
            this.lineCap = s.lineCap;
            this.lineJoin = s.lineJoin;
            this.startCapPainter = s.startCapPainter;
            this.endCapPainter = s.endCapPainter;
            this.currentBrush = s.currentBrush;
            this.isInsetPen = s.isInsetPen;
            this.actualLineWidth = s.actualLineWidth;
        }

        public double LineWidth
        {
            get => 
                this.lineWidth;
            set => 
                this.lineWidth = value;
        }

        public double[] StrokingColorComponents
        {
            get => 
                this.strokingColorComponents;
            set => 
                this.strokingColorComponents = value;
        }

        public double StrokingAlpha
        {
            get => 
                this.strokingAlpha;
            set => 
                this.strokingAlpha = value;
        }

        public double[] NonStrokingColorComponents
        {
            get => 
                this.nonStrokingColorComponents;
            set => 
                this.nonStrokingColorComponents = value;
        }

        public double NonStrokingAlpha
        {
            get => 
                this.nonStrokingAlpha;
            set => 
                this.nonStrokingAlpha = value;
        }

        public DXDashStyle? DashStyle
        {
            get => 
                this.dashStyle;
            set => 
                this.dashStyle = value;
        }

        public DXDashCap? DashCap
        {
            get => 
                this.dashCap;
            set => 
                this.dashCap = value;
        }

        public DXLineCap? LineCap
        {
            get => 
                this.lineCap;
            set => 
                this.lineCap = value;
        }

        public DXLineJoin? LineJoin
        {
            get => 
                this.lineJoin;
            set => 
                this.lineJoin = value;
        }

        public double MiterLimit
        {
            get => 
                this.miterLimit;
            set => 
                this.miterLimit = value;
        }

        public IPdfLineCapPainter StartCapPainter
        {
            get => 
                this.startCapPainter;
            set => 
                this.startCapPainter = value;
        }

        public IPdfLineCapPainter EndCapPainter
        {
            get => 
                this.endCapPainter;
            set => 
                this.endCapPainter = value;
        }

        public PdfBrushContainer CurrentBrush
        {
            get => 
                this.currentBrush;
            set => 
                this.currentBrush = value;
        }

        public bool IsInsetPen
        {
            get => 
                this.isInsetPen;
            set => 
                this.isInsetPen = value;
        }

        public double ActualLineWidth
        {
            get => 
                this.actualLineWidth;
            set => 
                this.actualLineWidth = value;
        }
    }
}

