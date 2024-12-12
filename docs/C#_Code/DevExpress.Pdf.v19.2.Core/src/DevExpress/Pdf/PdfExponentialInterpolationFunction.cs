namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfExponentialInterpolationFunction : PdfCustomFunction
    {
        internal const int Number = 2;
        private const string c0DictionaryKey = "C0";
        private const string c1DictionaryKey = "C1";
        private const string exponentDictionaryKey = "N";
        private readonly IList<double> c0;
        private readonly IList<double> c1;
        private readonly double exponent;

        internal PdfExponentialInterpolationFunction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            IList<double> list8;
            IList<double> list9;
            IList<object> array = dictionary.GetArray("C0");
            IList<object> cArray = dictionary.GetArray("C1");
            IList<PdfRange> range = base.Range;
            int n = (range != null) ? range.Count : ((array == null) ? 1 : array.Count);
            if (array != null)
            {
                list8 = CreateArray(array, n);
            }
            else
            {
                List<double> list1 = new List<double>();
                list1.Add(0.0);
                list8 = list1;
            }
            this.c0 = list8;
            if (cArray != null)
            {
                list9 = CreateArray(cArray, n);
            }
            else
            {
                List<double> list6 = new List<double>();
                list6.Add(1.0);
                list9 = list6;
            }
            this.c1 = list9;
            if (this.c0.Count != this.c1.Count)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            double? number = dictionary.GetNumber("N");
            if (number == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.exponent = number.Value;
        }

        internal PdfExponentialInterpolationFunction(IList<double> c0, IList<double> c1, double exponent, IList<PdfRange> domain, IList<PdfRange> range) : base(domain, range)
        {
            this.c0 = c0;
            this.c1 = c1;
            this.exponent = exponent;
        }

        private static bool CompareArrays(IList<double> array1, IList<double> array2)
        {
            int count = array1.Count;
            if (array2.Count != count)
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static IList<double> CreateArray(IList<object> cArray, int n)
        {
            if (cArray.Count != n)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            IList<double> list = new List<double>(n);
            foreach (object obj2 in cArray)
            {
                list.Add(PdfDocumentReader.ConvertToDouble(obj2));
            }
            return list;
        }

        protected override PdfWriterDictionary FillDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.FillDictionary(objects);
            if ((this.c0.Count != 1) || (this.c0[0] != 0.0))
            {
                dictionary.Add("C0", ToObjectArray(this.c0));
            }
            if ((this.c1.Count != 1) || (this.c1[0] != 1.0))
            {
                dictionary.Add("C1", ToObjectArray(this.c1));
            }
            dictionary.Add("N", this.exponent);
            return dictionary;
        }

        protected internal override bool IsSame(PdfFunction function)
        {
            if (!base.IsSame(function))
            {
                return false;
            }
            PdfExponentialInterpolationFunction function2 = (PdfExponentialInterpolationFunction) function;
            return ((this.exponent == function2.exponent) && (CompareArrays(this.c0, function2.c0) && CompareArrays(this.c1, function2.c1)));
        }

        protected override double[] PerformTransformation(double[] arguments)
        {
            int rangeSize = this.RangeSize;
            if (arguments.Length != 1)
            {
                return arguments;
            }
            double num2 = Math.Pow(arguments[0], this.exponent);
            double[] numArray = new double[rangeSize];
            for (int i = 0; i < rangeSize; i++)
            {
                double num4 = this.c0[i];
                numArray[i] = num4 + (num2 * (this.c1[i] - num4));
            }
            return numArray;
        }

        private static IList<object> ToObjectArray(IList<double> array)
        {
            List<object> list = new List<object>(array.Count);
            foreach (double num in array)
            {
                list.Add(num);
            }
            return list;
        }

        public IList<double> C0 =>
            this.c0;

        public IList<double> C1 =>
            this.c1;

        public double Exponent =>
            this.exponent;

        protected internal override int RangeSize =>
            this.c0.Count;

        protected override bool ShouldCheckEmptyRange =>
            false;

        protected override int FunctionType =>
            2;
    }
}

