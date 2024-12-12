namespace DevExpress.XtraExport.Implementation
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlGraphicFrame
    {
        private const double minRotation = -3600.0;
        private const double maxRotation = 3600.0;
        private double rotation;

        internal bool IsDefault() => 
            (this.rotation == 0.0) && (!this.FlipHorizontal && !this.FlipVertical);

        public bool FlipHorizontal { get; set; }

        public bool FlipVertical { get; set; }

        public double Rotation
        {
            get => 
                this.rotation;
            set
            {
                if ((value < -3600.0) || (value > 3600.0))
                {
                    throw new ArgumentOutOfRangeException($"Rotation angle out of range {-3600.0}...{3600.0}");
                }
                this.rotation = value;
            }
        }
    }
}

