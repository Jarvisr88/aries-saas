namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Windows;

    public class PdfPasswordSecurityOptionsEditor : IDXTypeEditor
    {
        public void Edit(object value, Window ownerWindow)
        {
            PdfPasswordSecurityOptions options = (PdfPasswordSecurityOptions) value;
            PdfPasswordSecurityOptionsView view = new PdfPasswordSecurityOptionsView();
            if (ownerWindow != null)
            {
                view.Owner = ownerWindow;
                view.FlowDirection = ownerWindow.FlowDirection;
            }
            new PdfPasswordSecurityOptionsPresenter(options, view).Initialize();
            view.ShowDialog();
        }
    }
}

