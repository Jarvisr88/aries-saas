namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfSampledFunction : PdfCustomFunction
    {
        internal const int Number = 0;
        private const string sizeDictionaryKey = "Size";
        private const string bitsPerSampleDictionaryKey = "BitsPerSample";
        private const string orderDictionaryKey = "Order";
        private const string encodeDictionaryKey = "Encode";
        private const string decodeDictionaryKey = "Decode";
        private readonly IList<int> size;
        private readonly int bitsPerSample;
        private readonly bool isCubicInterpolation;
        private readonly IList<PdfRange> encode;
        private readonly IList<PdfRange> decode;
        private readonly long[] samples;

        internal PdfSampledFunction(PdfReaderDictionary dictionary, byte[] data) : base(dictionary)
        {
            IList<object> array = dictionary.GetArray("Size");
            if ((data == null) || (array == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int count = array.Count;
            this.size = new List<int>(count);
            foreach (object obj2 in array)
            {
                if (!(obj2 is int))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.size.Add((int) obj2);
            }
            int? integer = dictionary.GetInteger("BitsPerSample");
            if (integer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.bitsPerSample = integer.Value;
            int? nullable2 = dictionary.GetInteger("Order");
            if (nullable2 != null)
            {
                int num2 = nullable2.Value;
                if (num2 == 1)
                {
                    this.isCubicInterpolation = false;
                }
                else if (num2 != 3)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else
                {
                    this.isCubicInterpolation = true;
                }
            }
            IList<object> list2 = dictionary.GetArray("Encode");
            this.encode = (list2 == null) ? this.CreateEncode() : CreateRangeArray(list2, count);
            IList<object> list3 = dictionary.GetArray("Decode");
            this.decode = (list3 == null) ? base.Range : CreateRangeArray(list3, base.Range.Count);
            this.samples = PdfSampledDataConverter.Convert(this.bitsPerSample, this.SamplesCount, data);
        }

        private IList<PdfRange> CreateEncode()
        {
            List<PdfRange> list = new List<PdfRange>(this.size.Count);
            foreach (int num in this.size)
            {
                list.Add(new PdfRange(0.0, (double) (num - 1)));
            }
            return list;
        }

        private static IList<PdfRange> CreateRangeArray(IList<object> array, int size)
        {
            if (array.Count != (size * 2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            IList<PdfRange> list = new List<PdfRange>(size);
            int num = 0;
            int num2 = 0;
            while (num < size)
            {
                double min = PdfDocumentReader.ConvertToDouble(array[num2++]);
                double max = PdfDocumentReader.ConvertToDouble(array[num2++]);
                list.Add(new PdfRange(min, max));
                num++;
            }
            return list;
        }

        protected override PdfWriterDictionary FillDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.FillDictionary(objects);
            List<object> list = new List<object>(this.size.Count);
            foreach (int num in this.size)
            {
                list.Add(num);
            }
            dictionary.Add("Size", list);
            dictionary.Add("BitsPerSample", this.bitsPerSample);
            if (this.isCubicInterpolation)
            {
                dictionary.Add("Order", 3);
            }
            if (!CompareRanges(this.encode, this.CreateEncode()))
            {
                dictionary.Add("Encode", ToObjectArray(this.encode));
            }
            if (!CompareRanges(this.decode, base.Range))
            {
                dictionary.Add("Decode", ToObjectArray(this.decode));
            }
            return dictionary;
        }

        private double Interpolate(double argument, double argumentMin, double argumentMax, PdfRange code, double valueMin, double valueMax)
        {
            argument = this.Normalize(argument, argumentMin, argumentMax);
            argument = Interpolate(argument, argumentMin, argumentMax, code);
            return this.Normalize(argument, valueMin, valueMax);
        }

        private unsafe double[] InterpolateSamples(double[] coordinates)
        {
            int length = coordinates.Length;
            int rangeSize = this.RangeSize;
            double[] numArray = new double[rangeSize];
            int num3 = 0;
            while (num3 < (1 << (length & 0x1f)))
            {
                double num4 = 1.0;
                int num5 = 0;
                int num6 = 1;
                int index = 0;
                while (true)
                {
                    if (index >= length)
                    {
                        if (num4 > 0.0)
                        {
                            num5 *= rangeSize;
                            for (int i = 0; i < rangeSize; i++)
                            {
                                double* numPtr1 = &(numArray[i]);
                                numPtr1[0] += this.samples[num5++] * num4;
                            }
                        }
                        num3++;
                        break;
                    }
                    int num8 = (int) Math.Truncate(coordinates[index]);
                    double num9 = coordinates[index] - num8;
                    if ((num3 & (1 << (index & 0x1f))) == 0)
                    {
                        num4 *= 1.0 - num9;
                        num5 += num8 * num6;
                    }
                    else
                    {
                        num4 *= num9;
                        num5 += (num8 + 1) * num6;
                    }
                    num6 *= this.size[index];
                    index++;
                }
            }
            return numArray;
        }

        protected internal override bool IsSame(PdfFunction function)
        {
            if (!base.IsSame(function))
            {
                return false;
            }
            PdfSampledFunction function2 = (PdfSampledFunction) function;
            if ((this.bitsPerSample != function2.bitsPerSample) || ((this.isCubicInterpolation != function2.isCubicInterpolation) || (!CompareRanges(this.encode, function2.encode) || !CompareRanges(this.decode, function2.decode))))
            {
                return false;
            }
            IList<int> size = function2.size;
            int count = size.Count;
            if (this.size.Count != count)
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                if (this.size[i] != size[i])
                {
                    return false;
                }
            }
            long[] samples = function2.samples;
            count = samples.Length;
            if (this.samples.Length != count)
            {
                return false;
            }
            for (int j = 0; j < count; j++)
            {
                if (this.samples[j] != samples[j])
                {
                    return false;
                }
            }
            return true;
        }

        private double Normalize(double value, double min, double max) => 
            Math.Min(Math.Max(value, min), max);

        protected override double[] PerformTransformation(double[] arguments)
        {
            int length = arguments.Length;
            int rangeSize = this.RangeSize;
            if (length != base.Domain.Count)
            {
                return new double[rangeSize];
            }
            for (int i = 0; i < length; i++)
            {
                PdfRange range = base.Domain[i];
                arguments[i] = this.Interpolate(arguments[i], range.Min, range.Max, this.encode[i], 0.0, (double) (this.size[i] - 1));
            }
            double[] numArray = this.InterpolateSamples(arguments);
            for (int j = 0; j < rangeSize; j++)
            {
                PdfRange range2 = base.Range[j];
                numArray[j] = this.Interpolate(numArray[j], 0.0, (double) this.MaxSampleValue, this.decode[j], range2.Min, range2.Max);
            }
            return numArray;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            PdfWriterStream.CreateCompressedStream(this.FillDictionary(objects), PdfSampledDataConverter.ConvertBack(this.bitsPerSample, this.SamplesCount, this.samples));

        public IList<int> Size =>
            this.size;

        public int BitsPerSample =>
            this.bitsPerSample;

        public bool IsCubicInterpolation =>
            this.isCubicInterpolation;

        public IList<PdfRange> Encode =>
            this.encode;

        public IList<PdfRange> Decode =>
            this.decode;

        public long[] Samples =>
            this.samples;

        private int SamplesCount
        {
            get
            {
                int num = 1;
                int count = this.size.Count;
                for (int i = 0; i < count; i++)
                {
                    num *= this.size[i];
                }
                return (num * base.Range.Count);
            }
        }

        private long MaxSampleValue
        {
            get
            {
                int bitsPerSample = this.bitsPerSample;
                if (bitsPerSample > 12)
                {
                    if (bitsPerSample == 0x10)
                    {
                        return 0xffffL;
                    }
                    if (bitsPerSample == 0x18)
                    {
                        return 0xffffffL;
                    }
                    if (bitsPerSample == 0x20)
                    {
                        return 0xffffffffL;
                    }
                }
                else
                {
                    switch (bitsPerSample)
                    {
                        case 1:
                            return 1L;

                        case 2:
                            return 3L;

                        case 3:
                            break;

                        case 4:
                            return (long) 15;

                        default:
                            if (bitsPerSample != 12)
                            {
                                break;
                            }
                            return 0xfffL;
                    }
                }
                return 0xffL;
            }
        }

        protected override int FunctionType =>
            0;
    }
}

