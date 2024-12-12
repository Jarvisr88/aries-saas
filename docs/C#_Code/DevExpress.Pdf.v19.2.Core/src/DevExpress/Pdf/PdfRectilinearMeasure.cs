namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRectilinearMeasure : PdfObject
    {
        private const string scaleRatioDictionaryKey = "R";
        private const string xAxisNumberFormatDictionaryKey = "X";
        private const string yAxisNumberFormatDictionaryKey = "Y";
        private const string distanceNumberFormatsDictionaryKey = "D";
        private const string areaNumberFormatsDictionaryKey = "A";
        private const string angleNumberFormatsDictionaryKey = "T";
        private const string lineSlopeNumberFormatsDictionaryKey = "S";
        private const string originDictionaryKey = "O";
        private const string yToXFactorDictionaryKey = "CYX";
        private readonly string scaleRatio;
        private readonly PdfNumberFormat[] xAxisNumberFormats;
        private readonly PdfNumberFormat[] yAxisNumberFormats;
        private readonly PdfNumberFormat[] distanceNumberFormats;
        private readonly PdfNumberFormat[] areaNumberFormats;
        private readonly PdfNumberFormat[] angleNumberFormats;
        private readonly PdfNumberFormat[] lineSlopeNumberFormats;
        private readonly PdfPoint? origin;
        private readonly double? yToXFactor;

        internal PdfRectilinearMeasure(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            string name = dictionary.GetName("Subtype");
            this.scaleRatio = dictionary.GetString("R");
            this.xAxisNumberFormats = ParseNumberFormats(dictionary, "X");
            this.distanceNumberFormats = ParseNumberFormats(dictionary, "D");
            this.areaNumberFormats = ParseNumberFormats(dictionary, "A");
            if ((((name != null) && (name != "RL")) || (this.scaleRatio == null)) || ((this.xAxisNumberFormats == null) || ((this.distanceNumberFormats == null) || (this.areaNumberFormats == null))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfNumberFormat[] formatArray1 = ParseNumberFormats(dictionary, "Y");
            PdfNumberFormat[] xAxisNumberFormats = formatArray1;
            if (formatArray1 == null)
            {
                PdfNumberFormat[] local1 = formatArray1;
                xAxisNumberFormats = this.xAxisNumberFormats;
            }
            this.yAxisNumberFormats = xAxisNumberFormats;
            this.angleNumberFormats = ParseNumberFormats(dictionary, "T");
            this.lineSlopeNumberFormats = ParseNumberFormats(dictionary, "S");
            IList<object> array = dictionary.GetArray("O");
            if (array != null)
            {
                if (array.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.origin = new PdfPoint?(PdfDocumentReader.CreatePoint(array, 0));
            }
            this.yToXFactor = dictionary.GetNumber("CYX");
        }

        private static PdfNumberFormat[] ParseNumberFormats(PdfReaderDictionary dictionary, string key)
        {
            IList<object> array = dictionary.GetArray(key);
            if (array == null)
            {
                return null;
            }
            int count = array.Count;
            if (count == 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfObjectCollection objects = dictionary.Objects;
            PdfNumberFormat[] formatArray = new PdfNumberFormat[count];
            for (int i = 0; i < count; i++)
            {
                PdfReaderDictionary dictionary2 = objects.TryResolve(array[i], null) as PdfReaderDictionary;
                if (dictionary2 == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                formatArray[i] = new PdfNumberFormat(dictionary2);
            }
            return formatArray;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("R", this.scaleRatio);
            dictionary.AddList<PdfNumberFormat>("X", this.xAxisNumberFormats);
            if (this.xAxisNumberFormats != this.yAxisNumberFormats)
            {
                dictionary.AddList<PdfNumberFormat>("Y", this.yAxisNumberFormats);
            }
            dictionary.AddList<PdfNumberFormat>("D", this.distanceNumberFormats);
            dictionary.AddList<PdfNumberFormat>("A", this.areaNumberFormats);
            dictionary.AddList<PdfNumberFormat>("T", this.angleNumberFormats);
            dictionary.AddList<PdfNumberFormat>("S", this.lineSlopeNumberFormats);
            if (this.origin != null)
            {
                PdfPoint point = this.origin.Value;
                double[] numArray1 = new double[] { point.X, point.Y };
                dictionary.Add("O", numArray1);
            }
            if (this.yToXFactor != null)
            {
                dictionary.Add("CYX", this.yToXFactor.Value);
            }
            return dictionary;
        }

        public string ScaleRatio =>
            this.scaleRatio;

        public PdfNumberFormat[] XAxisNumberFormats =>
            this.xAxisNumberFormats;

        public PdfNumberFormat[] YAxisNumberFormats =>
            this.yAxisNumberFormats;

        public PdfNumberFormat[] DistanceNumberFormats =>
            this.distanceNumberFormats;

        public PdfNumberFormat[] AreaNumberFormats =>
            this.areaNumberFormats;

        public PdfNumberFormat[] AngleNumberFormats =>
            this.angleNumberFormats;

        public PdfNumberFormat[] LineSlopeNumberFormats =>
            this.lineSlopeNumberFormats;

        public PdfPoint? Origin =>
            this.origin;

        public double? YToXFactor =>
            this.yToXFactor;
    }
}

