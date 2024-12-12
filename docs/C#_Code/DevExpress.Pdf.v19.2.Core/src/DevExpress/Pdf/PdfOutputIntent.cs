namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOutputIntent
    {
        private const string subtypeKey = "S";
        private const string outputConditionKey = "OutputCondition";
        private const string outputConditionIdentifierKey = "OutputConditionIdentifier";
        private const string registryNameKey = "RegistryName";
        private const string infoKey = "Info";
        private const string destOutputProfileKey = "DestOutputProfile";
        private const string defaultOutputConditionIdentifier = "sRGB IEC61966-2.1";
        private const string defaultSubtype = "GTS_PDFA1";
        private readonly string subtype;
        private readonly string outputCondition;
        private readonly string outputConditionIdentifier;
        private readonly string registryName;
        private readonly string info;
        private readonly PdfICCBasedColorSpace destOutputProfile;

        internal PdfOutputIntent()
        {
            this.subtype = "GTS_PDFA1";
            this.outputConditionIdentifier = "sRGB IEC61966-2.1";
            this.destOutputProfile = new PdfICCBasedColorSpace();
        }

        internal PdfOutputIntent(PdfReaderDictionary dictionary)
        {
            object obj2;
            this.subtype = dictionary.GetName("S");
            this.outputCondition = dictionary.GetString("OutputCondition");
            this.outputConditionIdentifier = dictionary.GetString("OutputConditionIdentifier");
            this.registryName = dictionary.GetString("RegistryName");
            this.info = dictionary.GetString("Info");
            PdfObjectCollection objects = dictionary.Objects;
            if (dictionary.TryGetValue("DestOutputProfile", out obj2))
            {
                obj2 = objects.TryResolve(obj2, null);
                IList<object> array = obj2 as IList<object>;
                if (array != null)
                {
                    this.destOutputProfile = new PdfICCBasedColorSpace(dictionary.Objects, array);
                }
                else
                {
                    PdfStream stream = obj2 as PdfStream;
                    if (stream != null)
                    {
                        this.destOutputProfile = new PdfICCBasedColorSpace(dictionary.Objects, stream);
                    }
                }
            }
        }

        internal object Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("OutputIntent"));
            dictionary.AddName("S", this.subtype);
            dictionary.AddIfPresent("OutputCondition", this.outputCondition);
            dictionary.AddIfPresent("OutputConditionIdentifier", this.outputConditionIdentifier);
            dictionary.AddIfPresent("RegistryName", this.registryName);
            dictionary.AddIfPresent("Info", this.info);
            if (this.destOutputProfile != null)
            {
                dictionary.Add("DestOutputProfile", this.destOutputProfile.CreateStream(objects));
            }
            return dictionary;
        }

        public string Subtype =>
            this.subtype;

        public string OutputCondition =>
            this.outputCondition;

        public string OutputConditionIdentifier =>
            this.outputConditionIdentifier;

        public string RegistryName =>
            this.registryName;

        public string Info =>
            this.info;

        public PdfICCBasedColorSpace DestOutputProfile =>
            this.destOutputProfile;
    }
}

