namespace DevExpress.Office.Drawing
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct OuterShadowTransformationInfo
    {
        private float offsetX;
        private float offsetY;
        private float scaleX;
        private float scaleY;
        private float skewX;
        private float skewY;
        public OuterShadowTransformationInfo(float offsetX, float offsetY, float scaleX, float scaleY, float skewX, float skewY)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.skewX = skewX;
            this.skewY = skewY;
        }

        public float OffsetX =>
            this.offsetX;
        public float OffsetY =>
            this.offsetY;
        public float ScaleX =>
            this.scaleX;
        public float ScaleY =>
            this.scaleY;
        public float SkewX =>
            this.skewX;
        public float SkewY =>
            this.skewY;
    }
}

