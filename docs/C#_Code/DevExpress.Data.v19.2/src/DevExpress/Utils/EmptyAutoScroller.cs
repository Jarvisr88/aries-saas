namespace DevExpress.Utils
{
    using System;

    public class EmptyAutoScroller : AutoScroller
    {
        public EmptyAutoScroller(MouseHandler mouseHandler) : base(mouseHandler)
        {
        }

        protected override void PopulateHotZones()
        {
        }
    }
}

