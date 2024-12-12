namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class ScalingFactorInfo : ICloneable<ScalingFactorInfo>
    {
        private readonly int horizontal;
        private readonly int vertical;

        public ScalingFactorInfo(int horizontal, int vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }

        public ScalingFactorInfo Clone() => 
            new ScalingFactorInfo(this.horizontal, this.vertical);

        public override bool Equals(object obj)
        {
            ScalingFactorInfo info = obj as ScalingFactorInfo;
            return ((info != null) ? ((this.horizontal == info.horizontal) && (this.vertical == info.vertical)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.horizontal.GetHashCode()) ^ this.vertical.GetHashCode();

        public int Horizontal =>
            this.horizontal;

        public int Vertical =>
            this.vertical;
    }
}

