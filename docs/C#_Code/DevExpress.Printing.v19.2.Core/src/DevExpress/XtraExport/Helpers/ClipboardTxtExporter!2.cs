namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class ClipboardTxtExporter<TCol, TRow> : IClipboardExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private StringBuilder txtCore;
        private List<List<CellInfo<TCol, TRow>>> rowCollectionCore;
        private int[] columnSizeCollection;

        public void AddBandedHeader(ClipboardBandLayoutInfo info)
        {
            this.columnSizeCollection ??= new int[Math.Max(info.GetColumnPanelRowWidth(0, -1), info.GetBandPanelRowWidth(0, false))];
            int index = 0;
            while (index < info.HeaderPanelInfo.Length)
            {
                List<ClipboardBandCellInfo> list = info.HeaderPanelInfo[index];
                List<CellInfo<TCol, TRow>> item = new List<CellInfo<TCol, TRow>>();
                int num2 = 0;
                int num3 = 0;
                while (true)
                {
                    CellInfo<TCol, TRow> info3;
                    if (num3 >= list.Count)
                    {
                        this.rowCollection.Add(item);
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo info2 = list[num3];
                    num2 += info2.SpaceBefore;
                    string str = (info2.AllowHtmlDraw ? info2.DisplayValueFormatted.Text : info2.DisplayValue).Replace("\r\n", " ");
                    if (this.columnSizeCollection[num2] < str.Length)
                    {
                        this.columnSizeCollection[num2] = str.Length;
                    }
                    info3.firstColumn = num2;
                    num2 += info2.Width;
                    info3.lastColumn = num2 - 1;
                    info3.text = str;
                    item.Add(info3);
                    num3++;
                }
            }
        }

        public void AddGroupHeader(ClipboardCellInfo groupHeaderInfo, int columnCount)
        {
            CellInfo<TCol, TRow> info;
            this.columnSizeCollection ??= new int[columnCount];
            List<CellInfo<TCol, TRow>> item = new List<CellInfo<TCol, TRow>>();
            info.firstColumn = -1;
            info.lastColumn = -1;
            info.text = groupHeaderInfo.DisplayValue;
            item.Add(info);
            this.rowCollection.Add(item);
        }

        public void AddHeaders(IEnumerable<ClipboardCellInfo> headerInfo)
        {
            this.columnSizeCollection ??= new int[headerInfo.Count<ClipboardCellInfo>()];
            List<CellInfo<TCol, TRow>> item = new List<CellInfo<TCol, TRow>>();
            for (int i = 0; i < headerInfo.Count<ClipboardCellInfo>(); i++)
            {
                CellInfo<TCol, TRow> info;
                ClipboardCellInfo info2 = headerInfo.ElementAt<ClipboardCellInfo>(i);
                info.text = info2.AllowHtmlDraw ? info2.DisplayValueFormatted.Text : (!string.IsNullOrEmpty(info2.DisplayValue) ? info2.DisplayValue : info2.Value.ToString());
                info.firstColumn = i;
                info.lastColumn = i;
                this.columnSizeCollection[i] = info.text.Length;
                item.Add(info);
            }
            this.rowCollection.Add(item);
        }

        public void AddRow(IEnumerable<ClipboardCellInfo> rowInfo)
        {
            this.columnSizeCollection ??= new int[rowInfo.Count<ClipboardCellInfo>()];
            List<CellInfo<TCol, TRow>> item = new List<CellInfo<TCol, TRow>>();
            for (int i = 0; i < rowInfo.Count<ClipboardCellInfo>(); i++)
            {
                CellInfo<TCol, TRow> info;
                info.text = new string(' ', 3 * rowInfo.ElementAt<ClipboardCellInfo>(i).Formatting.Alignment.Indent) + rowInfo.ElementAt<ClipboardCellInfo>(i).DisplayValue;
                info.firstColumn = i;
                info.lastColumn = i;
                item.Add(info);
                if (info.text.Length > this.columnSizeCollection[i])
                {
                    this.columnSizeCollection[i] = info.text.Length;
                }
            }
            this.rowCollection.Add(item);
        }

        public void AddRow(IEnumerable<ClipboardBandCellInfo>[] rowInfo)
        {
            if (this.columnSizeCollection == null)
            {
                int num = -1;
                IEnumerable<ClipboardBandCellInfo>[] enumerableArray = rowInfo;
                int num2 = 0;
                while (true)
                {
                    if (num2 >= enumerableArray.Length)
                    {
                        this.columnSizeCollection = new int[num];
                        break;
                    }
                    IEnumerable<ClipboardBandCellInfo> enumerable = enumerableArray[num2];
                    int num3 = 0;
                    foreach (ClipboardBandCellInfo info in enumerable)
                    {
                        num3 += info.SpaceBefore + info.Width;
                    }
                    if (num3 > num)
                    {
                        num = num3;
                    }
                    num2++;
                }
            }
            int index = 0;
            while (index < rowInfo.Length)
            {
                List<ClipboardBandCellInfo> list = rowInfo[index].ToList<ClipboardBandCellInfo>();
                List<CellInfo<TCol, TRow>> item = new List<CellInfo<TCol, TRow>>();
                int num5 = 0;
                int num6 = 0;
                while (true)
                {
                    CellInfo<TCol, TRow> info3;
                    if (num6 >= list.Count)
                    {
                        this.rowCollection.Add(item);
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo info2 = list[num6];
                    num5 += info2.SpaceBefore;
                    string str = (info2.AllowHtmlDraw ? info2.DisplayValueFormatted.Text : info2.DisplayValue).Replace("\r\n", " ");
                    if (this.columnSizeCollection[num5] < str.Length)
                    {
                        this.columnSizeCollection[num5] = str.Length;
                    }
                    info3.firstColumn = num5;
                    num5 += info2.Width;
                    info3.lastColumn = num5 - 1;
                    info3.text = str;
                    item.Add(info3);
                    num6++;
                }
            }
        }

        public void BeginExport()
        {
            this.txtCore = new StringBuilder();
            this.rowCollectionCore = null;
            this.columnSizeCollection = null;
        }

        public void EndExport()
        {
            if (this.rowCollection.Count > 0)
            {
                int num = this.columnSizeCollection.Sum();
                int num2 = 0;
                while (num2 < this.rowCollection.Count)
                {
                    int firstColumn = 0;
                    int num4 = 0;
                    while (true)
                    {
                        if (num4 >= this.rowCollection[num2].Count)
                        {
                            if (num2 < (this.rowCollection.Count - 1))
                            {
                                this.txtCore.AppendLine();
                            }
                            num2++;
                            break;
                        }
                        CellInfo<TCol, TRow> info = this.rowCollection[num2][num4];
                        string text = info.text;
                        if (info.firstColumn != firstColumn)
                        {
                            int index = firstColumn;
                            while (true)
                            {
                                if (index >= info.firstColumn)
                                {
                                    firstColumn = info.firstColumn;
                                    break;
                                }
                                text = new string(' ', this.columnSizeCollection[index]) + "\t" + text;
                                index++;
                            }
                        }
                        if (num4 != (this.rowCollection[num2].Count - 1))
                        {
                            text = text + new string(' ', this.columnSizeCollection[info.firstColumn] - info.text.Length) + "\t";
                            for (int i = firstColumn + 1; i <= info.lastColumn; i++)
                            {
                                text = text + new string(' ', this.columnSizeCollection[i]) + "\t";
                            }
                        }
                        this.txtCore.Append(text);
                        firstColumn = info.lastColumn + 1;
                        num4++;
                    }
                }
            }
        }

        public void SetDataObject(DataObject data)
        {
            data.SetData(typeof(string), this.txtCore.ToString());
        }

        private List<List<CellInfo<TCol, TRow>>> rowCollection
        {
            get
            {
                this.rowCollectionCore ??= new List<List<CellInfo<TCol, TRow>>>();
                return this.rowCollectionCore;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CellInfo
        {
            public string text;
            public int firstColumn;
            public int lastColumn;
        }
    }
}

