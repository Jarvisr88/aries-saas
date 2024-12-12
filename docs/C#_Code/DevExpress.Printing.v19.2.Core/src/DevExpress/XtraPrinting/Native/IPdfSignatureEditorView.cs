namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IPdfSignatureEditorView
    {
        event EventHandler SelectedCertificateItemChanged;

        event EventHandler Submit;

        void EnableTextEditors(bool enable);
        void FillCertificateItems(IEnumerable<ICertificateItem> items);

        string Reason { get; set; }

        string Location { get; set; }

        string ContactInfo { get; set; }

        ICertificateItem SelectedCertificateItem { get; set; }
    }
}

