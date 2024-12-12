namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public abstract class SvgTransform
    {
        private System.Drawing.Drawing2D.Matrix matrixCore;
        private double[] dataCore;

        public SvgTransform(double[] data)
        {
            this.dataCore = data;
            this.Initialize(data);
        }

        public abstract SvgTransform DeepCopy();
        public abstract System.Drawing.Drawing2D.Matrix GetMatrix(double scale);
        protected abstract void Initialize(double[] data);

        protected double[] Data =>
            this.dataCore;

        public System.Drawing.Drawing2D.Matrix Matrix
        {
            get
            {
                this.matrixCore ??= this.GetMatrix(1.0);
                return this.matrixCore;
            }
        }
    }
}

