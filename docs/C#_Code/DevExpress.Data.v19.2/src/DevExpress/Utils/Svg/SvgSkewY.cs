namespace DevExpress.Utils.Svg
{
    using System;

    [SvgTransformNameAlias("skewY")]
    public sealed class SvgSkewY : SvgSkew
    {
        public SvgSkewY(double[] data) : base(data)
        {
        }

        public override SvgTransform DeepCopy() => 
            new SvgSkewY(base.Data);

        protected override void Initialize(double[] data)
        {
            if (data.Length == 1)
            {
                base.AngleY = data[0];
            }
        }
    }
}

