namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DocumentPreviewMouseEventArgs : RoutedEventArgs
    {
        public DocumentPreviewMouseEventArgs(int pageIndex, DevExpress.XtraPrinting.Brick brick)
        {
            this.PageIndex = pageIndex;
            this.Brick = brick;
            brick.Do<DevExpress.XtraPrinting.Brick>(x => this.ElementTag = brick.Value);
        }

        public object ElementTag { get; private set; }

        public DevExpress.XtraPrinting.Brick Brick { get; private set; }

        public int PageIndex { get; private set; }
    }
}

