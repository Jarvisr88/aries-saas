namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct GradientStopInfo
    {
        private System.Drawing.Color color;
        private int position;
        public System.Drawing.Color Color
        {
            get => 
                this.color;
            set => 
                this.color = value;
        }
        public int Position
        {
            get => 
                this.position;
            set => 
                this.position = value;
        }
        public DrawingGradientStop CreateStop(IDocumentModel documentModel) => 
            DrawingGradientStop.Create(documentModel, this.color, this.position);
    }
}

