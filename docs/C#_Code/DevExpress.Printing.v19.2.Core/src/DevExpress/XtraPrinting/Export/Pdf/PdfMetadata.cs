namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PdfMetadata : PdfDocumentStreamObject
    {
        public PdfMetadata(bool compressed) : base(compressed)
        {
        }

        private void AddValue(string value, params string[] tags)
        {
            if (!string.IsNullOrEmpty(value))
            {
                base.Stream.SetString(GetStartTags(tags));
                base.Stream.SetBytes(Encoding.UTF8.GetBytes(value));
                Array.Reverse(tags);
                base.Stream.SetStringLine(GetEndTags(tags));
            }
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Attributes.Add("Type", "Metadata");
            base.Attributes.Add("Subtype", "XML");
            base.Stream.SetString("<?xpacket begin=\"");
            byte[] bytes = new byte[] { 0xef, 0xbb, 0xbf };
            base.Stream.SetBytes(bytes);
            base.Stream.SetStringLine("\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?><x:xmpmeta xmlns:x=\"adobe:ns:meta/\">");
            base.Stream.SetStringLine("<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">");
            base.Stream.SetStringLine("<rdf:Description rdf:about=\"\" xmlns:pdfaid=\"http://www.aiim.org/pdfa/ns/id/\" xmlns:pdf=\"http://ns.adobe.com/pdf/1.3/\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:xmp=\"http://ns.adobe.com/xap/1.0/\">");
            base.Stream.SetStringLine("<pdfaid:part>" + (this.HasEmbeddedFiles ? "3" : "2") + "</pdfaid:part><pdfaid:conformance>B</pdfaid:conformance>");
            string[] tags = new string[] { "pdf:Producer" };
            this.AddValue(this.Producer, tags);
            string[] textArray2 = new string[] { "pdf:Keywords" };
            this.AddValue(this.Keywords, textArray2);
            string[] textArray3 = new string[] { "dc:creator", "rdf:Seq", "rdf:li" };
            this.AddValue(this.Author, textArray3);
            string[] textArray4 = new string[] { "xmp:CreatorTool" };
            this.AddValue(this.Application, textArray4);
            string[] textArray5 = new string[] { "dc:title", "rdf:Alt", "rdf:li xml:lang=\"x-default\"" };
            this.AddValue(this.Title, textArray5);
            string[] textArray6 = new string[] { "dc:subject", "rdf:Bag" };
            this.AddValue(KeywordsAsList(this.Keywords), textArray6);
            string[] textArray7 = new string[] { "dc:description", "rdf:Alt", "rdf:li xml:lang=\"x-default\"" };
            this.AddValue(this.Subject, textArray7);
            string[] textArray8 = new string[] { "xmp:CreateDate" };
            this.AddValue(this.CreationDate.ToUniversalTime().ToString("yyyy-MM-dd'T'hh:mm:ss'Z'"), textArray8);
            base.Stream.SetStringLine("</rdf:Description>");
            if (!string.IsNullOrEmpty(this.AdditionalMetadata))
            {
                base.Stream.SetStringLine(this.AdditionalMetadata);
            }
            string str = new string(' ', 100);
            for (int i = 0; i < 20; i++)
            {
                base.Stream.SetStringLine(str);
            }
            base.Stream.SetStringLine("</rdf:RDF></x:xmpmeta><?xpacket end=\"w\"?>");
        }

        private static string GetEndTags(string[] tags)
        {
            Converter<string, string> converter = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                Converter<string, string> local1 = <>c.<>9__49_0;
                converter = <>c.<>9__49_0 = delegate (string tag) {
                    int index = tag.IndexOf(' ');
                    return "</" + ((index > 0) ? tag.Substring(0, index) : tag) + ">";
                };
            }
            return string.Concat(Array.ConvertAll<string, string>(tags, converter));
        }

        private static string GetStartTags(string[] tags)
        {
            Converter<string, string> converter = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Converter<string, string> local1 = <>c.<>9__48_0;
                converter = <>c.<>9__48_0 = tag => "<" + tag + ">";
            }
            return string.Concat(Array.ConvertAll<string, string>(tags, converter));
        }

        private static string KeywordsAsList(string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                return "";
            }
            char[] separator = new char[] { ';' };
            Converter<string, string> converter = <>c.<>9__46_0;
            if (<>c.<>9__46_0 == null)
            {
                Converter<string, string> local1 = <>c.<>9__46_0;
                converter = <>c.<>9__46_0 = delegate (string x) {
                    string str = x.Trim();
                    return (str.Length == 0) ? "" : ("<rdf:li>" + str + "</rdf:li>");
                };
            }
            return string.Join("", Array.ConvertAll<string, string>(keywords.Split(separator), converter));
        }

        public bool Active =>
            this.PdfACompatible;

        public bool PdfACompatible { get; set; }

        public string Producer { get; set; }

        public string Author { get; set; }

        public string Application { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Keywords { get; set; }

        public DateTime CreationDate { get; set; }

        public string AdditionalMetadata { get; set; }

        public bool HasEmbeddedFiles { get; set; }

        protected override bool UseFlateEncoding =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfMetadata.<>c <>9 = new PdfMetadata.<>c();
            public static Converter<string, string> <>9__46_0;
            public static Converter<string, string> <>9__48_0;
            public static Converter<string, string> <>9__49_0;

            internal string <GetEndTags>b__49_0(string tag)
            {
                int index = tag.IndexOf(' ');
                return ("</" + ((index > 0) ? tag.Substring(0, index) : tag) + ">");
            }

            internal string <GetStartTags>b__48_0(string tag) => 
                "<" + tag + ">";

            internal string <KeywordsAsList>b__46_0(string x)
            {
                string str = x.Trim();
                return ((str.Length == 0) ? "" : ("<rdf:li>" + str + "</rdf:li>"));
            }
        }
    }
}

