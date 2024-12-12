namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public abstract class SvgTransformBase : ISvgInstance
    {
        protected SvgTransformBase();
        public abstract void FillTransform(string[] values);
        public void FillTransform(string content);
        public string GetTransform();
        public abstract string GetTransform(IFormatProvider provider);
        public abstract System.Drawing.Drawing2D.Matrix GetTransformMatrix();

        public System.Drawing.Drawing2D.Matrix Matrix { get; }

        public virtual bool IsIdentity { get; }

        public virtual bool IgnoreChildren { get; }
    }
}

