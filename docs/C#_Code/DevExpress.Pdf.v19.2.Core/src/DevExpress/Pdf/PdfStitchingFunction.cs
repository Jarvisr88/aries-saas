namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfStitchingFunction : PdfCustomFunction
    {
        internal const int Number = 3;
        private const string functionsDictionaryKey = "Functions";
        private const string boundsDictionaryKey = "Bounds";
        private const string encodeDictionaryKey = "Encode";
        private readonly PdfObjectList<PdfCustomFunction> functions;
        private readonly IList<double> bounds;
        private readonly IList<PdfRange> encode;

        internal PdfStitchingFunction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<PdfRange> domain = base.Domain;
            this.functions = dictionary.GetFunctions("Functions", true);
            int count = this.functions.Count;
            this.encode = dictionary.GetRanges("Encode");
            if ((domain.Count > 1) || ((count < 1) || (this.encode.Count != count)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int rangeSize = this.RangeSize;
            foreach (PdfCustomFunction function in this.functions)
            {
                if (function.RangeSize != rangeSize)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            this.bounds = dictionary.GetDoubleArray("Bounds");
            int num3 = this.bounds.Count;
            if (num3 != (count - 1))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfRange range = domain[0];
            if (num3 == 0)
            {
                if (range.Min >= range.Max)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else
            {
                double num4 = this.bounds[0];
                if (range.Min > num4)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int num5 = 1;
                while (true)
                {
                    if (num5 >= num3)
                    {
                        if (range.Max < num4)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        break;
                    }
                    double num6 = this.bounds[num5];
                    if (num6 < num4)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    num4 = num6;
                    num5++;
                }
            }
        }

        internal PdfStitchingFunction(IList<double> bounds, IList<PdfRange> encode, PdfObjectList<PdfCustomFunction> functions, IList<PdfRange> domain, IList<PdfRange> range) : base(domain, range)
        {
            this.bounds = bounds;
            this.encode = encode;
            this.functions = functions;
        }

        protected override PdfWriterDictionary FillDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.FillDictionary(objects);
            dictionary.Add("Functions", this.functions);
            dictionary.Add("Bounds", this.bounds);
            dictionary.Add("Encode", PdfRange.ToArray(this.encode));
            return dictionary;
        }

        protected override double[] PerformTransformation(double[] arguments)
        {
            if (arguments.Length != 1)
            {
                return arguments;
            }
            PdfRange range = base.Domain[0];
            double x = arguments[0];
            PdfFunction function = null;
            double min = range.Min;
            double max = range.Max;
            PdfRange yRange = null;
            bool flag = false;
            int count = this.bounds.Count;
            int num5 = 0;
            while (true)
            {
                if (num5 < count)
                {
                    max = this.bounds[num5];
                    if (x >= max)
                    {
                        min = max;
                        num5++;
                        continue;
                    }
                    function = this.functions[num5];
                    yRange = this.encode[num5];
                    flag = true;
                }
                if (!flag)
                {
                    function = this.functions[count];
                    yRange = this.encode[count];
                    max = range.Max;
                }
                double[] numArray1 = new double[] { Interpolate(x, min, max, yRange) };
                return function.Transform(numArray1);
            }
        }

        protected override int FunctionType =>
            3;

        protected override bool ShouldCheckEmptyRange =>
            false;

        public IList<PdfCustomFunction> Functions =>
            this.functions;

        public IList<double> Bounds =>
            this.bounds;

        public IList<PdfRange> Encode =>
            this.encode;

        protected internal override int RangeSize
        {
            get
            {
                IList<PdfRange> range = base.Range;
                return ((range == null) ? this.functions[0].RangeSize : range.Count);
            }
        }
    }
}

