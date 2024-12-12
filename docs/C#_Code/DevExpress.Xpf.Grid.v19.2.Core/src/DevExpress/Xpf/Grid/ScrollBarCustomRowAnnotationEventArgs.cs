namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class ScrollBarCustomRowAnnotationEventArgs : EventArgs
    {
        public ScrollBarCustomRowAnnotationEventArgs(int rowHandle, object row)
        {
            this.RowHandle = rowHandle;
            this.Row = row;
        }

        public int RowHandle { get; private set; }

        public object Row { get; private set; }

        public DevExpress.Xpf.Grid.ScrollBarAnnotationInfo ScrollBarAnnotationInfo { get; set; }
    }
}

