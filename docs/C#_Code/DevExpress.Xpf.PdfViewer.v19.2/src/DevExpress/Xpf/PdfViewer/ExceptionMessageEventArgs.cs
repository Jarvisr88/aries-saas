namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ExceptionMessageEventArgs : RoutedEventArgs
    {
        public ExceptionMessageEventArgs(ExceptionMessageOrigin messageOrigin, System.Exception exception, string message, ExceptionMessageAction messageAction, MessageBoxImage messageImage) : base(PdfViewerControl.ExceptionMessageEvent)
        {
            this.MessageOrigin = messageOrigin;
            this.Exception = exception;
            this.Message = message;
            this.MessageAction = messageAction;
            this.MessageImage = messageImage;
        }

        public ExceptionMessageOrigin MessageOrigin { get; private set; }

        public System.Exception Exception { get; private set; }

        public string Message { get; set; }

        public ExceptionMessageAction MessageAction { get; set; }

        public MessageBoxImage MessageImage { get; set; }
    }
}

