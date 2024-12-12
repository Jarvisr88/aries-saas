namespace DevExpress.Xpf.Printing
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ZoomValueItem : ZoomItemBase
    {
        public ZoomValueItem(double zoomValue)
        {
            this.ZoomValue = zoomValue;
        }

        public override bool Equals(object obj) => 
            (obj is ZoomValueItem) && (((ZoomValueItem) obj).ZoomValue == this.ZoomValue);

        public override int GetHashCode() => 
            base.GetHashCode();

        [Description("Specifies the zoom factor value to which the ZoomValueItem corresponds.")]
        public double ZoomValue { get; private set; }

        [Description("Specifies the caption for the zoom value item as it appears in Print Preview.")]
        public override string DisplayedText =>
            string.Format(PrintingLocalizer.GetString(PrintingStringId.ZoomValueItemFormat), this.ZoomValue);
    }
}

