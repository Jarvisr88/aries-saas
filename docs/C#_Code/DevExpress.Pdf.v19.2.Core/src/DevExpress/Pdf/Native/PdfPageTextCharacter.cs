namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfPageTextCharacter
    {
        public PdfCharacter Character { get; }
        public PdfPoint Location { get; }
        public PdfTextBlock Block { get; }
        public PdfPageTextCharacter(PdfCharacter character, PdfPoint location, PdfTextBlock block)
        {
            this.<Character>k__BackingField = character;
            this.<Location>k__BackingField = location;
            this.<Block>k__BackingField = block;
        }
    }
}

