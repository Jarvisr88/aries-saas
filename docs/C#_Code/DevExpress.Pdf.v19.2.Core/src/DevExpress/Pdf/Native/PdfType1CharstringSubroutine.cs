namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfType1CharstringSubroutine
    {
        private readonly byte[] data;
        private readonly int lenIV;
        public PdfType1CharstringSubroutine(byte[] data, int lenIV)
        {
            this.data = data;
            this.lenIV = lenIV;
        }

        public IList<IPdfType1CharstringToken> GetTokens() => 
            PdfType1CharstringParser.Parse(PdfType1FontEexecCipher.DecryptCharstring(this.data, this.lenIV));
    }
}

