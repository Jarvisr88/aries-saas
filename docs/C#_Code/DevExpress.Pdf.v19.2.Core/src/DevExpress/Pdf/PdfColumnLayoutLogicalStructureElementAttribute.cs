namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfColumnLayoutLogicalStructureElementAttribute : PdfLayoutLogicalStructureElementAttribute
    {
        private const string columnCountKey = "ColumnCount";
        private const string columnGapKey = "ColumnGap";
        private const string columnWidthsKey = "ColumnWidths";
        internal static string[] Keys = new string[] { "ColumnCount", "ColumnGap", "ColumnWidths" };
        private readonly int columnCount;
        private readonly IList<double> columnGap;
        private readonly IList<double> columnWidths;

        internal PdfColumnLayoutLogicalStructureElementAttribute(PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            this.columnCount = 1;
            PdfObjectCollection collection = dictionary.Objects;
            int? integer = dictionary.GetInteger("ColumnCount");
            this.columnCount = (integer != null) ? integer.GetValueOrDefault() : 1;
            if (dictionary.TryGetValue("ColumnGap", out obj2))
            {
                this.columnGap = this.GetValues(obj2, this.columnCount - 1, collection);
            }
            if (dictionary.TryGetValue("ColumnWidths", out obj2))
            {
                this.columnWidths = this.GetValues(obj2, this.columnCount, collection);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("ColumnCount", this.columnCount, 1);
            dictionary.AddIfPresent("ColumnGap", this.WriteValues(this.columnGap));
            dictionary.AddIfPresent("ColumnWidths", this.WriteValues(this.columnWidths));
            return dictionary;
        }

        private IList<double> GetValues(object value, int listLength, PdfObjectCollection collection)
        {
            value = collection.TryResolve(value, null);
            if ((value is double) || (value is int))
            {
                double item = Convert.ToDouble(value);
                List<double> list2 = new List<double>(listLength);
                for (int j = 0; j < listLength; j++)
                {
                    list2.Add(item);
                }
                return list2;
            }
            IList<object> list = value as IList<object>;
            if (list == null)
            {
                return null;
            }
            int count = list.Count;
            int capacity = (list.Count > listLength) ? list.Count : listLength;
            List<double> list3 = new List<double>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                list3.Add((i < count) ? PdfDocumentReader.ConvertToDouble(list[i]) : list3[i - 1]);
            }
            return list3;
        }

        private object WriteValues(IList<double> value)
        {
            if (value == null)
            {
                return null;
            }
            for (int i = value.Count - 1; i >= 1; i--)
            {
                if (value[i] != value[i - 1])
                {
                    List<double> list = new List<double>();
                    for (int j = 0; j <= i; j++)
                    {
                        list.Add(value[j]);
                    }
                    return new PdfWritableDoubleArray(list);
                }
            }
            return value[0];
        }

        public int ColumnCount =>
            this.columnCount;

        public IList<double> ColumnGap =>
            this.columnGap;

        public IList<double> ColumnWidths =>
            this.columnWidths;
    }
}

