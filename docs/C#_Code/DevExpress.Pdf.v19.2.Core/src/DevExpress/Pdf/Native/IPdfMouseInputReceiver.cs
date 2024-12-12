namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfMouseInputReceiver
    {
        void MouseDown(PdfMouseAction action);
        void MouseMove(PdfMouseAction action);
        void MouseUp(PdfMouseAction action);
    }
}

