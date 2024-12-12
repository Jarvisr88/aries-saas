namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfAnnotationBorderStyle : PdfObject
    {
        internal const string DictionaryKey = "BS";
        private const string widthDictionaryKey = "W";
        private const string styleDictionaryKey = "S";
        private const string lineStyleDictionaryKey = "D";
        private const string solidStyleName = "S";
        private const string dashedStyleName = "D";
        private const string beveledStyleName = "B";
        private const string insetStyleName = "I";
        private const string underlineStyleName = "U";
        private const double defaultWidth = 1.0;
        private readonly double width;
        private readonly string styleName;
        private readonly PdfLineStyle lineStyle;

        internal PdfAnnotationBorderStyle(IPdfAnnotationBorderStyleBuilder builder)
        {
            PdfLineStyle style1;
            this.width = builder.Width;
            this.styleName = builder.StyleName;
            if (this.styleName != "D")
            {
                style1 = PdfLineStyle.CreateSolid();
            }
            else
            {
                double[] dashPattern = new double[] { 3.0 };
                style1 = PdfLineStyle.CreateDashed(dashPattern, 0.0);
            }
            this.lineStyle = style1;
        }

        private PdfAnnotationBorderStyle(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            double? number = dictionary.GetNumber("W");
            this.width = (number != null) ? number.GetValueOrDefault() : 1.0;
            string name = dictionary.GetName("S");
            PdfAnnotationBorderStyle style1 = this;
            if (name == null)
            {
                style1 = "S";
            }
            name.styleName = (string) style1;
            this.lineStyle = ParseLineStyle(dictionary.GetArray("D"));
        }

        internal static PdfAnnotationBorderStyle Parse(PdfReaderDictionary dictionary)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("BS");
            return ((dictionary2 == null) ? null : new PdfAnnotationBorderStyle(dictionary2));
        }

        internal static PdfLineStyle ParseLineStyle(IList<object> array)
        {
            if (array != null)
            {
                int count = array.Count;
                if ((count > 1) || ((count == 1) && (PdfDocumentReader.ConvertToDouble(array[0]) != 0.0)))
                {
                    return PdfLineStyle.CreateDashed(PdfLineStyle.ParseDashPattern(array), 0.0);
                }
            }
            return PdfLineStyle.CreateSolid();
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("W", this.width, 1.0);
            dictionary.AddName("S", this.styleName, "S");
            dictionary.Add("D", this.lineStyle.Data[0]);
            return dictionary;
        }

        public double Width =>
            this.width;

        public string StyleName =>
            this.styleName;

        public PdfLineStyle LineStyle =>
            this.lineStyle;

        internal double BorderWidth =>
            ((this.styleName == "B") || (this.styleName == "I")) ? (this.width * 2.0) : this.width;
    }
}

