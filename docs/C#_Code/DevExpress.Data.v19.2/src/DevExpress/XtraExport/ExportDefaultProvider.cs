namespace DevExpress.XtraExport
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing.Printing;
    using System.IO;

    public class ExportDefaultProvider : ExportCustomProvider, IExportProvider, IDisposable, IExportStyleManagerCreator
    {
        protected ExportCacheItem[,] cache;
        protected int[] columns;
        protected int[] rows;
        protected ExportStyleManagerBase styleManager;
        protected const string rangeError = "Incorrect cache width or height";
        protected const string indexError = "Incorrect cache col or row";
        protected const string unionError = "Incorrect union width or height";

        public ExportDefaultProvider(Stream stream) : base(stream)
        {
            this.styleManager = ExportStyleManagerBase.GetInstance("", stream, this);
        }

        public ExportDefaultProvider(string fileName) : base(fileName)
        {
            this.styleManager = ExportStyleManagerBase.GetInstance(fileName, null, this);
        }

        protected int CacheHeight() => 
            this.cache.GetLength(1);

        protected int CacheWidth() => 
            this.cache.GetLength(0);

        public virtual IExportProvider Clone(string fileName, Stream stream) => 
            base.IsStreamMode ? new ExportDefaultProvider(this.GetCloneStream(stream)) : new ExportDefaultProvider(this.GetCloneFileName(fileName));

        public virtual void Commit()
        {
        }

        protected StreamWriter CreateStreamWriter(string fileName) => 
            new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read, 0x1000));

        ExportStyleManagerBase IExportStyleManagerCreator.CreateInstance(string fileName, Stream stream) => 
            new ExportStyleManager(fileName, stream);

        public ExportCacheCellStyle GetCellStyle(int col, int row)
        {
            this.TestIndex(col, row);
            return this.styleManager[this.cache[col, row].StyleIndex];
        }

        protected int GetCellWidth(int col, int row)
        {
            int num = 0;
            for (int i = 0; i < this.cache[col, row].UnionWidth; i++)
            {
                num += this.GetColumnWidth(col + i);
            }
            return num;
        }

        protected string GetCloneFileName(string fileName)
        {
            string str = fileName;
            if (string.IsNullOrEmpty(str))
            {
                str = base.FileName;
            }
            return str;
        }

        protected Stream GetCloneStream(Stream stream) => 
            (stream != null) ? stream : base.Stream;

        public int GetColumnWidth(int col)
        {
            this.TestIndex(col, 0);
            return this.columns[col];
        }

        public ExportCacheCellStyle GetDefaultStyle() => 
            this.styleManager.DefaultStyle;

        public int GetRowHeight(int row)
        {
            this.TestIndex(0, row);
            return this.rows[row];
        }

        public ExportCacheCellStyle GetStyle(int styleIndex) => 
            this.styleManager[styleIndex];

        private void InternalSetRange(int width, int height, bool isVisible)
        {
            if ((width <= 0) || (height <= 0))
            {
                throw new ExportCacheException("Incorrect cache width or height");
            }
            this.cache = new ExportCacheItem[width, height];
            this.columns = new int[width];
            this.rows = new int[height];
            int index = 0;
            while (index < width)
            {
                this.columns[index] = 0;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= height)
                    {
                        index++;
                        break;
                    }
                    this.cache[index, num3].Data = null;
                    this.cache[index, num3].InternalCache = null;
                    this.cache[index, num3].IsHidden = false;
                    this.cache[index, num3].IsUnion = false;
                    this.cache[index, num3].UnionHeight = 1;
                    this.cache[index, num3].UnionWidth = 1;
                    this.cache[index, num3].StyleIndex = 0;
                    num3++;
                }
            }
            for (int i = 0; i < height; i++)
            {
                this.rows[i] = 0;
            }
            ExportCacheCellStyle defaultStyle = this.styleManager.DefaultStyle;
            int num = 0;
            if (isVisible)
            {
                num = 1;
            }
            defaultStyle.LeftBorder.Width = num;
            defaultStyle.TopBorder.Width = num;
            defaultStyle.RightBorder.Width = num;
            defaultStyle.BottomBorder.Width = num;
            this.SetDefaultStyle(defaultStyle);
        }

        public int RegisterStyle(ExportCacheCellStyle style) => 
            this.styleManager.RegisterStyle(style);

        public void SetCellData(int col, int row, object data)
        {
            this.TestIndex(col, row);
            this.cache[col, row].Data = data;
            if (data as bool)
            {
                this.cache[col, row].DataType = ExportCacheDataType.Boolean;
            }
            else if (data is int)
            {
                this.cache[col, row].DataType = ExportCacheDataType.Integer;
            }
            else if (data is float)
            {
                this.cache[col, row].DataType = ExportCacheDataType.Single;
            }
            else if (data is double)
            {
                this.cache[col, row].DataType = ExportCacheDataType.Double;
            }
            else if (data is decimal)
            {
                this.cache[col, row].DataType = ExportCacheDataType.Decimal;
            }
            else if (data is string)
            {
                this.cache[col, row].DataType = ExportCacheDataType.String;
            }
            else
            {
                this.cache[col, row].DataType = ExportCacheDataType.Object;
            }
        }

        public void SetCellString(int col, int row, string str)
        {
            this.TestIndex(col, row);
            this.cache[col, row].Data = str;
            this.cache[col, row].DataType = ExportCacheDataType.String;
        }

        public void SetCellStyle(int col, int row, ExportCacheCellStyle style)
        {
            this.TestIndex(col, row);
            this.cache[col, row].StyleIndex = this.styleManager.RegisterStyle(style);
        }

        public void SetCellStyle(int col, int row, int styleIndex)
        {
            this.TestIndex(col, row);
            this.cache[col, row].StyleIndex = styleIndex;
        }

        public void SetCellStyle(int col, int row, int exampleCol, int exampleRow)
        {
            this.TestIndex(col, row);
            this.TestIndex(exampleCol, exampleRow);
            this.cache[col, row].StyleIndex = this.cache[col, row].StyleIndex;
        }

        public void SetCellStyleAndUnion(int col, int row, int width, int height, ExportCacheCellStyle style)
        {
            this.SetCellUnion(col, row, width, height);
            this.SetCellStyle(col, row, style);
        }

        public void SetCellStyleAndUnion(int col, int row, int width, int height, int styleIndex)
        {
            this.SetCellUnion(col, row, width, height);
            this.SetCellStyle(col, row, styleIndex);
        }

        public void SetCellUnion(int col, int row, int width, int height)
        {
            if ((width != 1) || (height != 1))
            {
                this.TestIndex(col, row);
                if ((width <= 0) || ((((col + width) - 1) >= this.CacheWidth()) || ((height <= 0) || (((row + height) - 1) >= this.CacheHeight()))))
                {
                    throw new ExportCacheException("Incorrect union width or height");
                }
                this.cache[col, row].IsUnion = true;
                this.cache[col, row].IsHidden = false;
                this.cache[col, row].UnionWidth = width;
                this.cache[col, row].UnionHeight = height;
                int num = col;
                while (num < (col + width))
                {
                    int num2 = row;
                    while (true)
                    {
                        if (num2 >= (row + height))
                        {
                            num++;
                            break;
                        }
                        if ((num != col) || (num2 != row))
                        {
                            this.cache[num, num2].IsUnion = false;
                            this.cache[num, num2].IsHidden = true;
                            this.cache[num, num2].UnionWidth = 1;
                            this.cache[num, num2].UnionHeight = 1;
                        }
                        num2++;
                    }
                }
            }
        }

        public void SetColumnWidth(int col, int width)
        {
            this.TestIndex(col, 0);
            this.columns[col] = width;
        }

        public void SetDefaultStyle(ExportCacheCellStyle style)
        {
            this.styleManager.DefaultStyle = style;
        }

        public void SetPageSettings(MarginsF margins, PaperKind paperKind, bool landscape)
        {
        }

        public void SetRange(int width, int height, bool isVisible)
        {
            this.InternalSetRange(width, height, isVisible);
        }

        public void SetRowHeight(int row, int height)
        {
            this.TestIndex(0, row);
            this.rows[row] = height;
        }

        public void SetStyle(ExportCacheCellStyle style)
        {
            int styleIndex = this.styleManager.RegisterStyle(style);
            this.SetStyle(styleIndex);
        }

        public void SetStyle(int styleIndex)
        {
            int num = 0;
            while (num < this.CacheWidth())
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 >= this.CacheHeight())
                    {
                        num++;
                        break;
                    }
                    this.cache[num, num2].StyleIndex = styleIndex;
                    num2++;
                }
            }
        }

        protected void TestIndex(int col, int row)
        {
            if ((col < 0) || ((col >= this.CacheWidth()) || ((row < 0) || (row >= this.CacheHeight()))))
            {
                throw new ExportCacheException("Incorrect cache col or row");
            }
        }
    }
}

