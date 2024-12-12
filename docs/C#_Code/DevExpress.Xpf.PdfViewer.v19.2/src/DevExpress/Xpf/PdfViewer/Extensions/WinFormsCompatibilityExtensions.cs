namespace DevExpress.Xpf.PdfViewer.Extensions
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public static class WinFormsCompatibilityExtensions
    {
        public static Size Rotate(this Size size, double angle) => 
            ((((int) angle) % 180) == 0) ? size : new Size(size.Height, size.Width);

        public static PdfModifierKeys ToPdfModifierKeys(this ModifierKeys modifierKeys) => 
            (PdfModifierKeys) modifierKeys;
    }
}

