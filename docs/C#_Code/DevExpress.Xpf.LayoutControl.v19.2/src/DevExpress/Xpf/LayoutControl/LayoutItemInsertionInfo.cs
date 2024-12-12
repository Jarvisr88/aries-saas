namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    [StructLayout(LayoutKind.Sequential)]
    public struct LayoutItemInsertionInfo
    {
        public FrameworkElement DestinationItem;
        public LayoutItemInsertionKind InsertionKind;
        public LayoutItemInsertionPoint InsertionPoint;
        public LayoutItemInsertionInfo(FrameworkElement destinationItem, LayoutItemInsertionKind insertionKind, LayoutItemInsertionPoint insertionPoint)
        {
            this.DestinationItem = destinationItem;
            this.InsertionKind = insertionKind;
            this.InsertionPoint = insertionPoint;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LayoutItemInsertionInfo))
            {
                return this.Equals(obj);
            }
            LayoutItemInsertionInfo info = (LayoutItemInsertionInfo) obj;
            return (ReferenceEquals(this.DestinationItem, info.DestinationItem) && ((this.InsertionKind == info.InsertionKind) && (((this.InsertionPoint != null) || (info.InsertionPoint != null)) ? ((this.InsertionPoint != null) && this.InsertionPoint.Equals(info.InsertionPoint)) : true)));
        }

        public override int GetHashCode() => 
            this.GetHashCode();
    }
}

