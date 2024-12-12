namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfIconFit : PdfObject
    {
        private const string scalingCircumstancesDictionaryKey = "SW";
        private const string scalingTypeDictionaryKey = "S";
        private const string positionDictionaryKey = "A";
        private const string fitToAnnotationBoundsDictionaryKey = "FB";
        private const double defaultPosition = 0.5;
        private readonly PdfIconScalingCircumstances scalingCircumstances;
        private readonly PdfIconScalingType scalingType;
        private readonly double horizontalPosition;
        private readonly double verticalPosition;
        private readonly bool fitToAnnotationBounds;

        internal PdfIconFit() : base(-1)
        {
            this.scalingCircumstances = PdfIconScalingCircumstances.Always;
            this.scalingType = PdfIconScalingType.Proportional;
            this.horizontalPosition = 0.5;
            this.verticalPosition = 0.5;
            this.fitToAnnotationBounds = false;
        }

        internal PdfIconFit(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.scalingCircumstances = PdfEnumToStringConverter.Parse<PdfIconScalingCircumstances>(dictionary.GetName("SW"), true);
            this.scalingType = PdfEnumToStringConverter.Parse<PdfIconScalingType>(dictionary.GetName("S"), true);
            IList<object> array = dictionary.GetArray("A");
            if (array == null)
            {
                this.horizontalPosition = 0.5;
                this.verticalPosition = 0.5;
            }
            else
            {
                if (array.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.horizontalPosition = ConvertToPosition(array[0]);
                this.verticalPosition = ConvertToPosition(array[1]);
            }
            bool? boolean = dictionary.GetBoolean("FB");
            this.fitToAnnotationBounds = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        private static double ConvertToPosition(object value)
        {
            double num = PdfDocumentReader.ConvertToDouble(value);
            if ((num < 0.0) || (num > 1.0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return num;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddEnumName<PdfIconScalingCircumstances>("SW", this.scalingCircumstances);
            dictionary.AddEnumName<PdfIconScalingType>("S", this.scalingType);
            if ((this.horizontalPosition != 0.5) || (this.verticalPosition != 0.5))
            {
                object[] objArray1 = new object[] { this.horizontalPosition, this.verticalPosition };
                dictionary.Add("A", objArray1);
            }
            dictionary.Add("FB", this.fitToAnnotationBounds, false);
            return dictionary;
        }

        public PdfIconScalingCircumstances ScalingCircumstances =>
            this.scalingCircumstances;

        public PdfIconScalingType ScalingType =>
            this.scalingType;

        public double HorizontalPosition =>
            this.horizontalPosition;

        public double VerticalPosition =>
            this.verticalPosition;

        public bool FitToAnnotationBounds =>
            this.fitToAnnotationBounds;
    }
}

