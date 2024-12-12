namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PopupMenuShowingEventArgs : RoutedEventArgs
    {
        public PopupMenuShowingEventArgs() : base(PdfViewerControl.PopupMenuShowingEvent)
        {
            this.Actions = new List<IControllerAction>();
        }

        public IList<IControllerAction> Actions { get; private set; }

        public bool Cancel { get; set; }
    }
}

