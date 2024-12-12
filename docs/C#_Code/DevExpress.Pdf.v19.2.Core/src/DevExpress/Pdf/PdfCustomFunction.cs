namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class PdfCustomFunction : PdfFunction
    {
        private const string domainDictionaryKey = "Domain";
        private const string rangeDictionaryKey = "Range";
        private readonly IList<PdfRange> domain;
        private readonly IList<PdfRange> range;
        private double[] argumentsBuffer;

        protected PdfCustomFunction(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            if (dictionary != null)
            {
                this.domain = PdfDocumentReader.CreateDomain(dictionary.GetArray("Domain"));
                IList<object> array = dictionary.GetArray("Range");
                if (array != null)
                {
                    this.range = PdfDocumentReader.CreateDomain(array);
                }
                else if (this.ShouldCheckEmptyRange)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected PdfCustomFunction(IList<PdfRange> domain, IList<PdfRange> range)
        {
            if (domain == null)
            {
                throw new ArgumentNullException("domain");
            }
            if (domain.Count == 0)
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectListSize), "domain");
            }
            if ((range == null) && this.ShouldCheckEmptyRange)
            {
                throw new ArgumentNullException("range");
            }
            if (range.Count == 0)
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectListSize), "range");
            }
            this.domain = domain;
            this.range = range;
        }

        protected static bool CompareRanges(IList<PdfRange> range1, IList<PdfRange> range2)
        {
            if (range1 == null)
            {
                return ReferenceEquals(range2, null);
            }
            int count = range1.Count;
            if ((range2 == null) || (range2.Count != count))
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                PdfRange range = range1[i];
                PdfRange range3 = range2[i];
                if ((range.Min != range3.Min) || (range.Max != range3.Max))
                {
                    return false;
                }
            }
            return true;
        }

        protected virtual PdfWriterDictionary FillDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("FunctionType", this.FunctionType);
            dictionary.Add("Domain", ToObjectArray(this.domain));
            if (this.range != null)
            {
                dictionary.Add("Range", ToObjectArray(this.range));
            }
            return dictionary;
        }

        protected static double Interpolate(double x, double xmin, double xmax, PdfRange yRange)
        {
            double min = yRange.Min;
            double num2 = (x - xmin) * (yRange.Max - min);
            double num3 = xmax - xmin;
            return (((num2 + num3) == num2) ? min : (min + (num2 / num3)));
        }

        protected internal override bool IsSame(PdfFunction function)
        {
            PdfCustomFunction function2 = function as PdfCustomFunction;
            return ((function2 != null) && ((base.GetType() == function2.GetType()) && (CompareRanges(this.domain, function2.domain) && CompareRanges(this.range, function2.range))));
        }

        internal static PdfCustomFunction Parse(PdfObjectCollection objects, object value) => 
            PerformParse(objects.TryResolve(value, null));

        internal static PdfCustomFunction PerformParse(object value)
        {
            byte[] data = null;
            PdfReaderDictionary dictionary = value as PdfReaderDictionary;
            if (dictionary == null)
            {
                PdfReaderStream stream = value as PdfReaderStream;
                if (stream == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                dictionary = stream.Dictionary;
                data = stream.UncompressedData;
            }
            int? integer = dictionary.GetInteger("FunctionType");
            if (integer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            switch (integer.Value)
            {
                case 0:
                    if (data == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfSampledFunction(dictionary, data);

                case 2:
                    if (data != null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfExponentialInterpolationFunction(dictionary);

                case 3:
                    return new PdfStitchingFunction(dictionary);

                case 4:
                {
                    if (data == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    string str = Encoding.UTF8.GetString(data);
                    return (((str == PdfPostScriptGrayToCMYKColorFunction.StringData) || (str == PdfPostScriptGrayToCMYKColorFunction.AlternateStringData)) ? new PdfPostScriptGrayToCMYKColorFunction(dictionary, data) : new PdfPostScriptCalculatorFunction(dictionary, data));
                }
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected abstract double[] PerformTransformation(double[] arguments);
        private static void Restrict(double[] values, IList<PdfRange> ranges)
        {
            int count = ranges.Count;
            if (values.Length == count)
            {
                for (int i = 0; i < count; i++)
                {
                    double num3 = values[i];
                    PdfRange range = ranges[i];
                    double min = range.Min;
                    if (num3 < min)
                    {
                        values[i] = min;
                    }
                    else
                    {
                        double max = range.Max;
                        if (num3 > max)
                        {
                            values[i] = max;
                        }
                    }
                }
            }
        }

        protected static IList<object> ToObjectArray(IList<PdfRange> ranges)
        {
            List<object> list = new List<object>(ranges.Count * 2);
            foreach (PdfRange range in ranges)
            {
                list.Add(range.Min);
                list.Add(range.Max);
            }
            return list;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.FillDictionary(objects);

        protected internal override double[] Transform(double[] arguments)
        {
            if ((this.argumentsBuffer != null) && (this.argumentsBuffer.Length == arguments.Length))
            {
                Array.Copy(arguments, this.argumentsBuffer, arguments.Length);
            }
            else
            {
                this.argumentsBuffer = (double[]) arguments.Clone();
            }
            Restrict(this.argumentsBuffer, this.Domain);
            double[] values = this.PerformTransformation(this.argumentsBuffer);
            IList<PdfRange> range = this.Range;
            if (range != null)
            {
                Restrict(values, range);
            }
            return values;
        }

        protected internal override object Write(PdfObjectCollection objects) => 
            objects.AddObject((PdfObject) this);

        public IList<PdfRange> Domain =>
            this.domain;

        public IList<PdfRange> Range =>
            this.range;

        protected internal virtual int RangeSize =>
            this.range.Count;

        protected virtual bool ShouldCheckEmptyRange =>
            true;

        protected abstract int FunctionType { get; }
    }
}

