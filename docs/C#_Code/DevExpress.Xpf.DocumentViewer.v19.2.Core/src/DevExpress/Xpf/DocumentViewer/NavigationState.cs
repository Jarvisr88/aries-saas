namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class NavigationState : ICloneable
    {
        public NavigationState Clone() => 
            (NavigationState) ((ICloneable) this).Clone();

        protected bool Equals(NavigationState other) => 
            (this.PageIndex == other.PageIndex) && (this.OffsetY.Equals(other.OffsetY) && (this.OffsetX.Equals(other.OffsetX) && (this.Angle.Equals(other.Angle) && this.ZoomFactor.AreClose(other.ZoomFactor))));

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((NavigationState) obj) : false) : true) : false;

        public override int GetHashCode() => 
            (((((this.PageIndex * 0x18d) ^ this.OffsetY.GetHashCode()) * 0x18d) ^ this.OffsetX.GetHashCode()) * 0x18d) ^ this.Angle.GetHashCode();

        object ICloneable.Clone()
        {
            NavigationState state1 = new NavigationState();
            state1.PageIndex = this.PageIndex;
            state1.OffsetX = this.OffsetX;
            state1.OffsetY = this.OffsetY;
            state1.Angle = this.Angle;
            state1.ZoomFactor = this.ZoomFactor;
            state1.ZoomMode = this.ZoomMode;
            return state1;
        }

        public int PageIndex { get; set; }

        public double OffsetX { get; set; }

        public double OffsetY { get; set; }

        public double Angle { get; set; }

        public double ZoomFactor { get; set; }

        public DevExpress.Xpf.DocumentViewer.ZoomMode ZoomMode { get; set; }
    }
}

