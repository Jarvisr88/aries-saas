namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class SkewAnglesInfo : ICloneable<SkewAnglesInfo>
    {
        private readonly int horizontal;
        private readonly int vertical;

        public SkewAnglesInfo(int horizontal, int vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }

        public SkewAnglesInfo Clone() => 
            new SkewAnglesInfo(this.horizontal, this.vertical);

        public override bool Equals(object obj)
        {
            SkewAnglesInfo info = obj as SkewAnglesInfo;
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

