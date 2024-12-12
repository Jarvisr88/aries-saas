namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class ReflectionOpacityInfo : ICloneable<ReflectionOpacityInfo>
    {
        private readonly int startOpacity;
        private readonly int startPosition;
        private readonly int endOpacity;
        private readonly int endPosition;
        private readonly int fadeDirection;

        public ReflectionOpacityInfo(int startOpacity, int startPosition, int endOpacity, int endPosition, int fadeDirection)
        {
            this.startOpacity = startOpacity;
            this.endOpacity = endOpacity;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.fadeDirection = fadeDirection;
        }

        public ReflectionOpacityInfo Clone() => 
            new ReflectionOpacityInfo(this.startOpacity, this.startPosition, this.endOpacity, this.endPosition, this.fadeDirection);

        public override bool Equals(object obj)
        {
            ReflectionOpacityInfo info = obj as ReflectionOpacityInfo;
            return ((info != null) ? ((this.startOpacity == info.startOpacity) && ((this.endOpacity == info.endOpacity) && ((this.startPosition == info.startPosition) && ((this.endPosition == info.endPosition) && (this.fadeDirection == info.fadeDirection))))) : false);
        }

        public override int GetHashCode() => 
            ((((base.GetType().GetHashCode() ^ this.startOpacity.GetHashCode()) ^ this.endOpacity.GetHashCode()) ^ this.startPosition.GetHashCode()) ^ this.endPosition.GetHashCode()) ^ this.fadeDirection.GetHashCode();

        public int StartOpacity =>
            this.startOpacity;

        public int EndOpacity =>
            this.endOpacity;

        public int StartPosition =>
            this.startPosition;

        public int EndPosition =>
            this.endPosition;

        public int FadeDirection =>
            this.fadeDirection;
    }
}

