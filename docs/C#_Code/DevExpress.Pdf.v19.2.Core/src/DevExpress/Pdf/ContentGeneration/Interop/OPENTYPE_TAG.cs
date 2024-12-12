namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct OPENTYPE_TAG
    {
        public static readonly OPENTYPE_TAG Kerning;
        public static readonly OPENTYPE_TAG RequiredLigatures;
        public static readonly OPENTYPE_TAG StandardLigatures;
        public static readonly OPENTYPE_TAG ContextualLigatures;
        public static readonly OPENTYPE_TAG HistoricalLigatures;
        public static readonly OPENTYPE_TAG DiscretionaryLigatures;
        private uint value;
        private static OPENTYPE_TAG MakeOpenTypeTag(char a, char b, char c, char d) => 
            new OPENTYPE_TAG((((d << 0x18) | (c << 0x10)) | (b << 8)) | a);

        public OPENTYPE_TAG(int v)
        {
            this.value = (uint) v;
        }

        static OPENTYPE_TAG()
        {
            Kerning = MakeOpenTypeTag('k', 'e', 'r', 'n');
            RequiredLigatures = MakeOpenTypeTag('r', 'l', 'i', 'g');
            StandardLigatures = MakeOpenTypeTag('l', 'i', 'g', 'a');
            ContextualLigatures = MakeOpenTypeTag('c', 'l', 'i', 'g');
            HistoricalLigatures = MakeOpenTypeTag('h', 'l', 'i', 'g');
            DiscretionaryLigatures = MakeOpenTypeTag('d', 'l', 'i', 'g');
        }
    }
}

