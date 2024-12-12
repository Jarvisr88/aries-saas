namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class JBIG2UserHuffmanTablesEnumerator
    {
        private readonly IEnumerator<JBIG2HuffmanTableSegment> enumerator;

        public JBIG2UserHuffmanTablesEnumerator(IList<JBIG2HuffmanTableSegment> userDefinedTables)
        {
            this.enumerator = userDefinedTables.GetEnumerator();
        }

        public IHuffmanTreeNode GetNext()
        {
            if (!this.enumerator.MoveNext())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return this.enumerator.Current.TreeRoot;
        }
    }
}

