namespace DevExpress.Text.Fonts.DirectWrite.CCW
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class DWriteFontFileEnumerator : IDWriteFontFileEnumerator
    {
        private readonly IEnumerator<DWriteFontFile> enumerator;

        public DWriteFontFileEnumerator(IEnumerator<DWriteFontFile> enumerator)
        {
            this.enumerator = enumerator;
        }

        public int GetCurrentFontFile(out IDWriteFontFile fontFile)
        {
            fontFile = this.enumerator.Current.WrappedObject;
            return 0;
        }

        public int MoveNext(out bool hasCurrentFile)
        {
            hasCurrentFile = this.enumerator.MoveNext();
            return 0;
        }
    }
}

