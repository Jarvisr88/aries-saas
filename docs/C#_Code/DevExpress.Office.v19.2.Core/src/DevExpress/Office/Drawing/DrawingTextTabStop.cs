namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextTabStop : ICloneable<DrawingTextTabStop>
    {
        private DrawingTextTabAlignmentType alignment;
        private float position;
        private bool hasPosition;

        public DrawingTextTabStop()
        {
        }

        public DrawingTextTabStop(DrawingTextTabAlignmentType alignment)
        {
            this.alignment = alignment;
        }

        public DrawingTextTabStop(DrawingTextTabAlignmentType alignment, float position) : this(alignment)
        {
            this.position = position;
            this.hasPosition = true;
        }

        public DrawingTextTabStop Clone() => 
            this.hasPosition ? new DrawingTextTabStop(this.alignment, this.position) : new DrawingTextTabStop(this.alignment);

        protected bool Equals(DrawingTextTabStop other) => 
            (this.alignment == other.alignment) && (this.position.Equals(other.position) && (this.hasPosition == other.hasPosition));

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((DrawingTextTabStop) obj) : false) : true) : false;

        public override int GetHashCode() => 
            ((int) (((this.alignment * ((DrawingTextTabAlignmentType) 0x18d)) ^ this.position.GetHashCode()) * ((DrawingTextTabAlignmentType) 0x18d))) ^ this.hasPosition.GetHashCode();

        public DrawingTextTabAlignmentType Alignment =>
            this.alignment;

        public float Position =>
            this.position;

        public bool HasPosition =>
            this.hasPosition;
    }
}

