namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [ComVisible(false)]
    public interface IPrintable : IBasePrintable
    {
        void AcceptChanges();
        bool HasPropertyEditor();
        void RejectChanges();
        void ShowHelp();
        bool SupportsHelp();

        UserControl PropertyEditorControl { get; }

        bool CreatesIntersectedBricks { get; }
    }
}

