namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class PdfCompositeFontEncoding : PdfEncoding
    {
        protected PdfCompositeFontEncoding()
        {
        }

        internal static PdfCompositeFontEncoding Create(PdfObjectCollection collection, object value)
        {
            if (value == null)
            {
                return PdfIdentityEncoding.HorizontalIdentity;
            }
            PdfName name = value as PdfName;
            if (name != null)
            {
                string str = name.Name;
                return ((str == "Identity-H") ? PdfIdentityEncoding.HorizontalIdentity : ((str == "Identity-V") ? ((PdfCompositeFontEncoding) PdfIdentityEncoding.VerticalIdentity) : ((PdfCompositeFontEncoding) new PdfPredefinedCompositeFontEncoding(str))));
            }
            Func<object, PdfCustomCompositeFontEncoding> create = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<object, PdfCustomCompositeFontEncoding> local1 = <>c.<>9__0_0;
                create = <>c.<>9__0_0 = delegate (object o) {
                    PdfReaderStream stream = o as PdfReaderStream;
                    if (stream == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfCustomCompositeFontEncoding(stream);
                };
            }
            return collection.GetObject<PdfCustomCompositeFontEncoding>(value, create);
        }

        internal abstract short GetCID(byte[] code);
        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.Write(objects);

        protected internal override bool ShouldUseEmbeddedFontEncoding =>
            false;

        public abstract bool IsVertical { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfCompositeFontEncoding.<>c <>9 = new PdfCompositeFontEncoding.<>c();
            public static Func<object, PdfCustomCompositeFontEncoding> <>9__0_0;

            internal PdfCustomCompositeFontEncoding <Create>b__0_0(object o)
            {
                PdfReaderStream stream = o as PdfReaderStream;
                if (stream == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfCustomCompositeFontEncoding(stream);
            }
        }
    }
}

