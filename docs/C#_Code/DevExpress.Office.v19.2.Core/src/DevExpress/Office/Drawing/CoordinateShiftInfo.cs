namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class CoordinateShiftInfo : ICloneable<CoordinateShiftInfo>
    {
        private readonly long horizontal;
        private readonly long vertical;

        public CoordinateShiftInfo(long horizontal, long vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }

        public CoordinateShiftInfo Clone() => 
            new CoordinateShiftInfo(this.horizontal, this.vertical);

        public override bool Equals(object obj)
        {
            CoordinateShiftInfo info = obj as CoordinateShiftInfo;
            return ((info != null) ? ((this.horizontal == info.horizontal) && (this.vertical == info.vertical)) : false);
        }

        public override int GetHashCode() => 
            (base.GetType().GetHashCode() ^ this.horizontal.GetHashCode()) ^ this.vertical.GetHashCode();

        public long Horizontal =>
            this.horizontal;

        public long Vertical =>
            this.vertical;
    }
}

