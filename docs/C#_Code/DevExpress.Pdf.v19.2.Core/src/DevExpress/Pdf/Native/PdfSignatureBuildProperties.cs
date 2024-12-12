namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSignatureBuildProperties
    {
        private readonly PdfSignatureBuildPropertiesData filter;
        private readonly PdfSignatureBuildPropertiesData pubSec;
        private readonly PdfSignatureBuildPropertiesAppData app;
        private readonly PdfSignatureBuildPropertiesSigQData sigQ;

        public PdfSignatureBuildProperties(PdfReaderDictionary dictionary)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Filter");
            if (dictionary2 != null)
            {
                this.filter = new PdfSignatureBuildPropertiesData(dictionary2);
            }
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("PubSec");
            if (dictionary3 != null)
            {
                this.pubSec = new PdfSignatureBuildPropertiesData(dictionary3);
            }
            PdfReaderDictionary dictionary4 = dictionary.GetDictionary("App");
            if (dictionary4 != null)
            {
                this.app = new PdfSignatureBuildPropertiesAppData(dictionary4);
            }
            PdfReaderDictionary dictionary5 = dictionary.GetDictionary("SigQ");
            if (dictionary5 != null)
            {
                this.sigQ = new PdfSignatureBuildPropertiesSigQData(dictionary5);
            }
        }

        public PdfSignatureBuildPropertiesData Filter =>
            this.filter;

        public PdfSignatureBuildPropertiesData PubSec =>
            this.pubSec;

        public PdfSignatureBuildPropertiesAppData App =>
            this.app;

        public PdfSignatureBuildPropertiesSigQData SigQ =>
            this.sigQ;
    }
}

