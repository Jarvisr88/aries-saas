namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCustomCompositeFontEncoding : PdfCompositeFontEncoding
    {
        private const string dictionaryType = "CMap";
        private const string nameDictionaryKey = "CMapName";
        private const string cidSystemInfoDictionaryKey = "CIDSystemInfo";
        private const string wModeDictionaryKey = "WMode";
        private const string useCMapDictionaryKey = "UseCMap";
        private readonly string name;
        private readonly PdfCIDSystemInfo cidSysteminfo;
        private readonly PdfCompositeFontEncoding baseEncoding;
        private readonly PdfCharacterMapping characterMapping;
        private readonly bool isVertical;
        private readonly Lazy<PdfCIDCMap> cmap;

        internal PdfCustomCompositeFontEncoding(PdfReaderStream stream)
        {
            object obj2;
            PdfReaderDictionary dictionary = stream.Dictionary;
            this.name = dictionary.GetName("CMapName");
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("CIDSystemInfo");
            if (dictionary2 != null)
            {
                this.cidSysteminfo = new PdfCIDSystemInfo(dictionary2);
            }
            int? integer = dictionary.GetInteger("WMode");
            if (integer != null)
            {
                int num = integer.Value;
                if (num == 0)
                {
                    this.isVertical = false;
                }
                else if (num != 1)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else
                {
                    this.isVertical = true;
                }
            }
            if (dictionary.TryGetValue("UseCMap", out obj2))
            {
                this.baseEncoding = Create(dictionary.Objects, obj2);
            }
            this.characterMapping = new PdfCharacterMapping(stream.UncompressedData);
            this.cmap = new Lazy<PdfCIDCMap>(() => PdfCIDCMap.Parse(this.characterMapping.Data));
        }

        internal override short GetCID(byte[] code) => 
            this.cmap.Value.GetCID(code);

        protected internal override PdfStringCommandData GetStringData(byte[] bytes, double[] glyphOffsets) => 
            this.cmap.Value.GetStringData(bytes, glyphOffsets);

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "CMap");
            dictionary.AddName("CMapName", this.name);
            dictionary.Add("CIDSystemInfo", this.cidSysteminfo);
            if (this.isVertical)
            {
                dictionary.Add("WMode", 1);
            }
            dictionary.Add("UseCMap", (PdfObject) this.baseEncoding, null);
            return PdfWriterStream.CreateCompressedStream(dictionary, this.characterMapping.Data);
        }

        protected internal override object Write(PdfObjectCollection objects) => 
            objects.AddObject((PdfObject) this);

        public string Name =>
            this.name;

        public PdfCIDSystemInfo CIDSystemInfo =>
            this.cidSysteminfo;

        public PdfCompositeFontEncoding BaseEncoding =>
            this.baseEncoding;

        public PdfCharacterMapping CharacterMapping =>
            this.characterMapping;

        public override bool IsVertical =>
            this.isVertical;
    }
}

