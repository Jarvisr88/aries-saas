namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutItemInsertionPoint
    {
        public LayoutItemInsertionPoint(FrameworkElement element, bool isInternalInsertion)
        {
            this.Element = element;
            this.IsInternalInsertion = isInternalInsertion;
        }

        public override bool Equals(object obj) => 
            (obj is LayoutItemInsertionPoint) && (ReferenceEquals(((LayoutItemInsertionPoint) obj).Element, this.Element) && (((LayoutItemInsertionPoint) obj).IsInternalInsertion == this.IsInternalInsertion));

        public override int GetHashCode() => 
            base.GetHashCode();

        public FrameworkElement Element { get; set; }

        public bool IsInternalInsertion { get; set; }
    }
}

