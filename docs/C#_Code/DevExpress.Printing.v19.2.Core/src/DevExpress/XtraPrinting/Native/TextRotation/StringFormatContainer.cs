namespace DevExpress.XtraPrinting.Native.TextRotation
{
    using System;
    using System.Drawing;

    public class StringFormatContainer : IDisposable
    {
        private StringFormat stringFormat;
        private bool dispose;

        public StringFormatContainer(StringFormat stringFormat, bool dispose);
        public void Dispose();

        public StringFormat Value { get; }
    }
}

