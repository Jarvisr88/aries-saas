namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class Pdf3dMeasurementDataContainer
    {
        private readonly IList<object> a1;
        private readonly IList<object> tp;
        private readonly double ts;
        private readonly IList<object> c;
        private readonly string ut;

        public Pdf3dMeasurementDataContainer(PdfReaderDictionary dictionary)
        {
            this.a1 = dictionary.GetArray("A1");
            this.tp = dictionary.GetArray("TP");
            double? number = dictionary.GetNumber("TS");
            this.ts = (number != null) ? number.GetValueOrDefault() : 12.0;
            this.c = dictionary.GetArray("C");
            this.ut = dictionary.GetString("UT");
        }

        public void FillDictionary(PdfWriterDictionary dictionary)
        {
            dictionary.AddIfPresent("A1", this.a1);
            dictionary.AddIfPresent("TP", this.tp);
            dictionary.Add("TS", this.ts, 12.0);
            dictionary.AddIfPresent("C", this.c);
            dictionary.AddIfPresent("UT", this.ut);
        }

        public IList<object> A1 =>
            this.a1;

        public IList<object> TP =>
            this.tp;

        public double TS =>
            this.ts;

        public IList<object> C =>
            this.c;

        public string UT =>
            this.ut;
    }
}

