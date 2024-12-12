namespace DevExpress.XtraEditors
{
    using System;
    using System.Runtime.CompilerServices;

    public class RangeControlClientRangeEventArgs : RangeControlRangeEventArgs
    {
        public bool InvalidateContent { get; set; }

        public bool MakeRangeVisible { get; set; }

        public bool AnimatedViewport { get; set; }
    }
}

