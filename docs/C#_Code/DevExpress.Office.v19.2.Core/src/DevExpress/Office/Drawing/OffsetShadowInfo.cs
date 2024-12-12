namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class OffsetShadowInfo : ICloneable<OffsetShadowInfo>
    {
        private readonly int direction;
        private readonly long distance;

        public OffsetShadowInfo(long distance, int direction)
        {
            this.direction = direction;
            this.distance = distance;
        }

        public OffsetShadowInfo Clone() => 
            new OffsetShadowInfo(this.distance, this.direction);

        public override bool Equals(object obj)
        {
            OffsetShadowInfo info = obj as OffsetShadowInfo;
            return ((info != null) ? ((this.distance == info.distance) && (this.direction == info.direction)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.distance.GetHashCode()) ^ this.direction.GetHashCode();

        public long Distance =>
            this.distance;

        public int Direction =>
            this.direction;
    }
}

