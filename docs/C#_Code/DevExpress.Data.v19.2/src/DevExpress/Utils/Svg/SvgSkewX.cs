namespace DevExpress.Utils.Svg
{
    using System;

    [SvgTransformNameAlias("skewX")]
    public sealed class SvgSkewX : SvgSkew
    {
        public SvgSkewX(double[] data) : base(data)
        {
        }

        public override SvgTransform DeepCopy() => 
            new SvgSkewX(base.Data);

        protected override void Initialize(double[] data)
        {
            if (data.Length == 1)
            {
                base.AngleX = data[0];
            }
        }
    }
}

