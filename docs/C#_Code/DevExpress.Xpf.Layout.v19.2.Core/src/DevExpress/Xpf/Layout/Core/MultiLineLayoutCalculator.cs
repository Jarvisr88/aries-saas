namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class MultiLineLayoutCalculator : BaseLayoutCalculator
    {
        private double allElements;

        protected override ITabHeaderLayoutResult CalcCore(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
        {
            List<Row> rows;
            bool isHorizontal = options.IsHorizontal;
            if (this.allElements <= TabHeaderHelper.GetLength(isHorizontal, options.Size))
            {
                rows = this.GetRows(headers, options);
            }
            else
            {
                List<ITabHeaderInfo> list3 = new List<ITabHeaderInfo>();
                List<ITabHeaderInfo> list4 = new List<ITabHeaderInfo>();
                ITabHeaderInfo[] infoArray = headers;
                int index = 0;
                while (true)
                {
                    if (index >= infoArray.Length)
                    {
                        rows = !options.SelectedRowFirst ? this.GetRows(list3.ToArray(), options).Concat<Row>(this.GetRows(list4.ToArray(), options)).ToList<Row>() : this.GetRows(list4.ToArray(), options).Concat<Row>(this.GetRows(list3.ToArray(), options)).ToList<Row>();
                        break;
                    }
                    ITabHeaderInfo item = infoArray[index];
                    if (item.IsPinned)
                    {
                        list3.Add(item);
                    }
                    else
                    {
                        list4.Add(item);
                    }
                    index++;
                }
            }
            double dim = 0.0;
            double num2 = 0.0;
            List<Rect> list2 = new List<Rect>();
            int rowCount = rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                ITabHeaderLayoutResult result;
                Row row = rows[i];
                Rect header = new Rect(isHorizontal ? 0.0 : dim, isHorizontal ? dim : 0.0, row.Size.Width, row.Size.Height);
                if (!options.IsAutoFill && (rowCount <= 1))
                {
                    result = this.CalcRow(new Rect(options.Size), isHorizontal, row.Headers);
                }
                else
                {
                    double scaleFactor = base.CalcScaleFactor(row.Headers, options);
                    result = base.CalcScaledCaptions(header, isHorizontal, row.Headers, scaleFactor);
                    if (rowCount > 1)
                    {
                        int rowIndex = options.SelectedRowFirst ? ((rowCount - 1) - i) : i;
                        Array.ForEach<ITabHeaderInfo>(row.Headers, delegate (ITabHeaderInfo x) {
                            MultiLineLayoutResult result1 = new MultiLineLayoutResult();
                            result1.RowCount = rowCount;
                            result1.RowIndex = rowIndex;
                            x.MultiLineResult = result1;
                        });
                    }
                }
                dim += result.Size.IsEmpty ? 0.0 : TabHeaderHelper.GetLength(!isHorizontal, result.Size);
                num2 = Math.Max(num2, TabHeaderHelper.GetLength(isHorizontal, result.Size));
                list2.AddRange(result.Headers);
            }
            return new BaseLayoutCalculator.Result(TabHeaderHelper.GetSize(isHorizontal, num2, dim), list2.ToArray());
        }

        private void CheckBalance(Row r1, Row r2, ITabHeaderLayoutOptions options)
        {
            bool isHorizontal = options.IsHorizontal;
            double num = r1.GetLength(isHorizontal) + r2.GetLength(isHorizontal);
            r1.Balance(r2, num * 0.5, isHorizontal);
        }

        private List<Row> GetRows(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
        {
            bool isHorizontal = options.IsHorizontal;
            Size size = options.Size;
            int num = 0;
            double length = 0.0;
            double dim = 0.0;
            List<Row> rows = new List<Row>();
            List<ITabHeaderInfo> list2 = new List<ITabHeaderInfo>();
            while (num < headers.Length)
            {
                ITabHeaderInfo info = headers[num++];
                Size size2 = TabHeaderHelper.GetSize(info, isHorizontal);
                double num4 = TabHeaderHelper.GetLength(isHorizontal, size2);
                double num5 = TabHeaderHelper.GetLength(!isHorizontal, size2);
                if ((length + num4) > TabHeaderHelper.GetLength(isHorizontal, size))
                {
                    rows.Add(new Row(list2.ToArray(), TabHeaderHelper.GetSize(isHorizontal, length, dim)));
                    list2.Clear();
                    length = 0.0;
                    dim = 0.0;
                }
                list2.Add(info);
                length += num4;
                dim = Math.Max(dim, num5);
            }
            if (list2.Count > 0)
            {
                rows.Add(new Row(list2.ToArray(), TabHeaderHelper.GetSize(isHorizontal, length, dim)));
                if (rows.Count > 1)
                {
                    this.CheckBalance(rows[rows.Count - 2], rows[rows.Count - 1], options);
                }
            }
            if (rows.Count > 0)
            {
                if (!options.FixedRows)
                {
                    Row selected = this.GetSelected(rows);
                    rows.Remove(selected);
                    if (options.SelectedRowFirst)
                    {
                        rows.Insert(0, selected);
                    }
                    else
                    {
                        rows.Add(selected);
                    }
                }
                this.UpdateZIndex(rows, options.SelectedRowFirst);
            }
            return rows;
        }

        private Row GetSelected(List<Row> rows)
        {
            Row row2;
            using (List<Row>.Enumerator enumerator = rows.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Row current = enumerator.Current;
                        int index = 0;
                        while (true)
                        {
                            if (index >= current.Headers.Length)
                            {
                                break;
                            }
                            if (!current.Headers[index].IsSelected)
                            {
                                index++;
                                continue;
                            }
                            return current;
                        }
                        continue;
                    }
                    else
                    {
                        return rows[0];
                    }
                    break;
                }
            }
            return row2;
        }

        protected override void OnPrepareHeader(ITabHeaderInfo info, ITabHeaderLayoutOptions options)
        {
            Size size = options.Size;
            double length = TabHeaderHelper.GetLength(options.IsHorizontal, info.DesiredSize);
            this.allElements += length;
        }

        private void UpdateZIndex(List<Row> rows, bool first)
        {
            foreach (Row row in rows)
            {
                for (int i = 0; i < row.Headers.Length; i++)
                {
                    row.Headers[i].ZIndex = first ? (rows.Count - rows.IndexOf(row)) : rows.IndexOf(row);
                }
            }
        }

        public class Row
        {
            public Row(ITabHeaderInfo[] headers, System.Windows.Size size)
            {
                this.Headers = headers;
                this.Size = size;
            }

            public void Balance(MultiLineLayoutCalculator.Row target, double threshold, bool horz)
            {
                double num = 0.0;
                List<ITabHeaderInfo> list = new List<ITabHeaderInfo>(this.Headers);
                List<ITabHeaderInfo> list2 = new List<ITabHeaderInfo>(target.Headers);
                List<ITabHeaderInfo> list3 = new List<ITabHeaderInfo>();
                for (int i = 0; i < this.Headers.Length; i++)
                {
                    ITabHeaderInfo info = this.Headers[i];
                    double length = TabHeaderHelper.GetLength(info, horz);
                    if ((num + (0.5 * length)) > threshold)
                    {
                        list.Remove(info);
                        list3.Insert(0, info);
                    }
                    num += length;
                }
                for (int j = 0; j < list3.Count; j++)
                {
                    list2.Insert(0, list3[j]);
                }
                this.Headers = list.ToArray();
                target.Headers = list2.ToArray();
            }

            public double GetLength(bool horz)
            {
                double num = 0.0;
                for (int i = 0; i < this.Headers.Length; i++)
                {
                    num += TabHeaderHelper.GetLength(this.Headers[i], horz);
                }
                return num;
            }

            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public double GetLengtn(bool horz) => 
                this.GetLength(horz);

            public ITabHeaderInfo[] Headers { get; private set; }

            public System.Windows.Size Size { get; private set; }
        }
    }
}

