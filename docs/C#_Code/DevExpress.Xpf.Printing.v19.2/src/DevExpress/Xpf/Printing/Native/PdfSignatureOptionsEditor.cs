namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Windows;

    public class PdfSignatureOptionsEditor : IDXTypeEditor
    {
        public void Edit(object value, Window ownerWindow)
        {
            PdfSignatureOptions options = (PdfSignatureOptions) value;
            PdfSignatureOptions options2 = new PdfSignatureOptions();
            options2.Assign(options);
            PdfSignatureEditorWindow window1 = new PdfSignatureEditorWindow();
            window1.Owner = ownerWindow;
            PdfSignatureEditorWindow view = window1;
            PdfSignatureEditorPresenter presenter1 = new PdfSignatureEditorPresenter(options2, view);
            bool? nullable = view.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false)
            {
                options.Assign(options2);
            }
        }
    }
}

