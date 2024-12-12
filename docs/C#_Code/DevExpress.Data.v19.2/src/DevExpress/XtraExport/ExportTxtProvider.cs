namespace DevExpress.XtraExport
{
    using System;
    using System.IO;

    [Obsolete("")]
    public class ExportTxtProvider : ExportDefaultProvider
    {
        private string beginString;
        private string endString;
        private string separator;
        private bool alignColumnWidth;
        private bool quoteData;
        private int[] columnMaxWidth;

        public ExportTxtProvider(Stream stream) : base(stream)
        {
            this.beginString = "";
            this.endString = "";
            this.separator = " ";
            this.alignColumnWidth = true;
        }

        public ExportTxtProvider(string fileName) : base(fileName)
        {
            this.beginString = "";
            this.endString = "";
            this.separator = " ";
            this.alignColumnWidth = true;
        }

        private string AppendData(string data, int col)
        {
            if (this.columnMaxWidth[col] > data.Length)
            {
                int num = this.columnMaxWidth[col] - data.Length;
                for (int i = 0; i < num; i++)
                {
                    data = data + " ";
                }
            }
            return data;
        }

        private void CalculateColumnMaxWidth()
        {
            this.columnMaxWidth = new int[base.CacheWidth()];
            int index = 0;
            while (index < base.CacheWidth())
            {
                this.columnMaxWidth[index] = 0;
                int row = 0;
                while (true)
                {
                    if (row >= base.CacheHeight())
                    {
                        index++;
                        break;
                    }
                    if (!base.cache[index, row].IsHidden)
                    {
                        string data = this.GetData(index, row);
                        if (data.Length > this.columnMaxWidth[index])
                        {
                            this.columnMaxWidth[index] = data.Length;
                        }
                    }
                    row++;
                }
            }
        }

        public override IExportProvider Clone(string fileName, Stream stream)
        {
            ExportTxtProvider provider = base.IsStreamMode ? new ExportTxtProvider(base.GetCloneStream(stream)) : new ExportTxtProvider(base.GetCloneFileName(fileName));
            provider.BeginString = this.beginString;
            provider.EndString = this.beginString;
            provider.Separator = this.separator;
            provider.AlignColumnWidth = this.alignColumnWidth;
            provider.QuoteData = this.quoteData;
            return provider;
        }

        public override void Commit()
        {
            StreamWriter writer = base.IsStreamMode ? new StreamWriter(base.Stream) : base.CreateStreamWriter(base.FileName);
            try
            {
                this.CommitTxt(writer);
            }
            finally
            {
                if (base.IsStreamMode)
                {
                    writer.Flush();
                }
                else
                {
                    writer.Dispose();
                }
            }
        }

        private void CommitTxt(StreamWriter writer)
        {
            base.OnProviderProgress(0);
            if (this.alignColumnWidth)
            {
                this.CalculateColumnMaxWidth();
            }
            int i = 0;
            while (i < base.CacheHeight())
            {
                writer.Write(this.beginString);
                int num2 = base.CacheWidth();
                int j = 0;
                while (true)
                {
                    if (j >= (num2 - 1))
                    {
                        writer.Write(this.GetTextData(i, num2 - 1));
                        writer.WriteLine(this.endString);
                        if (base.CacheHeight() > 1)
                        {
                            base.OnProviderProgress((i * 100) / (base.CacheHeight() - 1));
                        }
                        i++;
                        break;
                    }
                    writer.Write(this.GetTextData(i, j) + this.separator);
                    j++;
                }
            }
            base.OnProviderProgress(100);
        }

        private string DoQuoteData(string data)
        {
            int startIndex = 0;
            while (startIndex < data.Length)
            {
                if (data[startIndex] != '"')
                {
                    startIndex++;
                    continue;
                }
                data = data.Insert(startIndex, "\"");
                startIndex += 2;
            }
            return ("\"" + data + "\"");
        }

        private string GetData(int col, int row)
        {
            string data = "";
            if (base.cache[col, row].Data != null)
            {
                switch (base.cache[col, row].DataType)
                {
                    case ExportCacheDataType.Boolean:
                        data = Convert.ToString((bool) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.Integer:
                        data = Convert.ToString((int) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.Double:
                        data = Convert.ToString((double) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.Decimal:
                        data = Convert.ToString((decimal) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.String:
                        data = (string) base.cache[col, row].Data;
                        break;

                    case ExportCacheDataType.Single:
                        data = Convert.ToString((float) base.cache[col, row].Data);
                        break;

                    default:
                        data = Convert.ToString(base.cache[col, row].Data);
                        break;
                }
            }
            return data;
        }

        private string GetTextData(int i, int j)
        {
            string data = this.GetData(j, i);
            if (this.quoteData)
            {
                data = this.DoQuoteData(data);
            }
            if (this.alignColumnWidth)
            {
                data = this.AppendData(data, j);
            }
            return data;
        }

        public string BeginString
        {
            get => 
                this.beginString;
            set => 
                this.beginString = value;
        }

        public string EndString
        {
            get => 
                this.endString;
            set => 
                this.endString = value;
        }

        public string Separator
        {
            get => 
                this.separator;
            set => 
                this.separator = value;
        }

        public bool AlignColumnWidth
        {
            get => 
                this.alignColumnWidth;
            set => 
                this.alignColumnWidth = value;
        }

        public bool QuoteData
        {
            get => 
                this.quoteData;
            set => 
                this.quoteData = value;
        }
    }
}

