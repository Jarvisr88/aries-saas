namespace DevExpress.XtraPrinting.Export.Text
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections;
    using System.IO;

    public class TextDocument
    {
        private Document document;
        private string separator;
        private bool insertSpaces;
        private bool followReportLayout;
        private bool skipEmptyRows;
        private bool skipEmptyColumns;
        private static bool isTxt;

        public TextDocument(Document document, string separator, bool insertSpaces)
        {
            this.separator = "\t";
            this.document = document;
            if (separator != null)
            {
                this.separator = separator;
            }
            this.insertSpaces = insertSpaces;
        }

        public TextDocument(Document document, string separator, bool insertSpaces, bool skipEmptyRows, bool skipEmptyColumns, bool followReportLayout) : this(document, separator, insertSpaces)
        {
            this.skipEmptyColumns = skipEmptyColumns;
            this.followReportLayout = followReportLayout;
            this.skipEmptyRows = skipEmptyRows;
        }

        public static void CreateDocument(LayoutControlCollection layoutControls, Document document, StreamWriter writer, TextExportOptionsBase options, bool insertSpaces)
        {
            ProgressReflector progressReflector = document.ProgressReflector;
            progressReflector.RangeValue++;
            isTxt = options is TextExportOptions;
            (isTxt ? new TextDocument(document, options.GetActualSeparator(), insertSpaces) : new TextDocument(document, options.GetActualSeparator(), insertSpaces, ((CsvExportOptions) options).SkipEmptyRows, ((CsvExportOptions) options).SkipEmptyColumns, CsvExportOptions.FollowReportLayout)).WriteTo(writer, layoutControls);
        }

        private string CreateSpaces(int count) => 
            new string(' ', count);

        private int GetLastItemIndex(TextLayoutItem[] row)
        {
            for (int i = row.Length - 1; i >= 0; i--)
            {
                if (row[i] != null)
                {
                    return i;
                }
            }
            return 0;
        }

        private void WriteCsvContent(StreamWriter writer, object[] objects, int[] mapX, int[] mapY)
        {
            int index = 0;
            for (int i = 0; i < mapY.Length; i++)
            {
                int startCol = 0;
                int countSeparators = 0;
                if ((mapY[i] != 0) || !this.skipEmptyRows)
                {
                    int num5 = 0;
                    while (true)
                    {
                        if (num5 >= mapY[i])
                        {
                            countSeparators = (mapX.Length - startCol) - 1;
                            this.WriteSeparators(writer, countSeparators, startCol, mapX.Length, mapX);
                            writer.WriteLine();
                            break;
                        }
                        ObjectInfo info = objects[index] as ObjectInfo;
                        TextLayoutItem layoutItem = (info.Object as TextBrickViewData).GetLayoutItem(0);
                        countSeparators = info.ColIndex - startCol;
                        this.WriteSeparators(writer, countSeparators, startCol, info.ColIndex, mapX);
                        writer.Write(layoutItem.Text);
                        startCol = info.ColIndex;
                        index++;
                        num5++;
                    }
                }
            }
        }

        private void WriteLastTextItem(StreamWriter writer, TextLayoutItem item)
        {
            this.WriteTextItemWithoutSpaces(writer, item);
        }

        private void WriteRow(StreamWriter writer, TextLayoutItem[] row, int[] columnWidthArray)
        {
            if (row.Length != 0)
            {
                int lastItemIndex = this.GetLastItemIndex(row);
                for (int i = 0; i < lastItemIndex; i++)
                {
                    this.WriteTextItem(writer, row[i], columnWidthArray[i]);
                    writer.Write(this.separator);
                }
                this.WriteLastTextItem(writer, row[lastItemIndex]);
                writer.WriteLine();
            }
        }

        private void WriteSeparators(StreamWriter writer, int count)
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(this.separator);
            }
        }

        private void WriteSeparators(StreamWriter writer, int countSeparators, int startCol, int endCol, int[] mapX)
        {
            if (this.skipEmptyColumns)
            {
                for (int i = startCol; i < endCol; i++)
                {
                    if (mapX[i] == 0)
                    {
                        countSeparators--;
                    }
                }
            }
            this.WriteSeparators(writer, countSeparators);
        }

        private void WriteTextItem(StreamWriter writer, TextLayoutItem item, int columnWidth)
        {
            if (this.insertSpaces)
            {
                this.WriteTextItemWidthSpaces(writer, item, columnWidth);
            }
            else
            {
                this.WriteTextItemWithoutSpaces(writer, item);
            }
        }

        private void WriteTextItemWidthSpaces(StreamWriter writer, TextLayoutItem item, int columnWidth)
        {
            if (item == null)
            {
                writer.Write(this.CreateSpaces(columnWidth));
            }
            else
            {
                int length = item.Text.Length;
                writer.Write(item.Text + this.CreateSpaces(columnWidth - length));
            }
        }

        private void WriteTextItemWithoutSpaces(StreamWriter writer, TextLayoutItem item)
        {
            if (item != null)
            {
                writer.Write(item.Text);
            }
        }

        public unsafe void WriteTo(StreamWriter writer, LayoutControlCollection layoutControls)
        {
            if (isTxt || !this.followReportLayout)
            {
                TextLayoutMapX mapX = new TextLayoutMapX(layoutControls);
                if (this.document != null)
                {
                    ProgressReflector progressReflector = this.document.ProgressReflector;
                    progressReflector.RangeValue++;
                }
                TextLayoutMapY mapY = new TextLayoutMapY(layoutControls);
                if (this.document != null)
                {
                    ProgressReflector progressReflector = this.document.ProgressReflector;
                    progressReflector.RangeValue++;
                }
                this.WriteTxtContent(writer, mapX, mapY);
            }
            else
            {
                MegaTable table = new MegaTable(layoutControls, true, false, -1);
                int[] mapX = new int[table.ColumnCount];
                int[] mapY = new int[table.RowCount];
                int index = 0;
                while (true)
                {
                    if (index >= table.Objects.Length)
                    {
                        if (this.document != null)
                        {
                            ProgressReflector progressReflector = this.document.ProgressReflector;
                            progressReflector.RangeValue++;
                        }
                        ObjectInfo[] objects = table.Objects;
                        Array.Sort(objects, new ObjectInfoComparer());
                        this.WriteCsvContent(writer, objects, mapX, mapY);
                        break;
                    }
                    if ((table.Objects[index].RowSpan != 0) && (table.Objects[index].ColSpan != 0))
                    {
                        int* numPtr1 = &(mapX[table.Objects[index].ColIndex]);
                        numPtr1[0]++;
                        int* numPtr2 = &(mapY[table.Objects[index].RowIndex]);
                        numPtr2[0]++;
                    }
                    index++;
                }
            }
            if (this.document != null)
            {
                ProgressReflector progressReflector = this.document.ProgressReflector;
                progressReflector.RangeValue++;
            }
        }

        private void WriteTxtContent(StreamWriter writer, TextLayoutMapX mapX, TextLayoutMapY mapY)
        {
            int count = mapX.Count;
            int num2 = 0;
            while (num2 < mapY.Count)
            {
                TextLayoutItem[] row = new TextLayoutItem[count];
                int num3 = 0;
                while (true)
                {
                    if (num3 >= mapY[num2].Count)
                    {
                        this.WriteRow(writer, row, mapX.ColumnWidthArray);
                        num2++;
                        break;
                    }
                    TextLayoutItem item = mapY[num2][num3];
                    int index = mapX.FindAndRemove(item.Owner);
                    row[index] = item;
                    num3++;
                }
            }
        }

        private class ObjectInfoComparer : IComparer
        {
            private int Compare(int x1, int y1, int x2, int y2) => 
                (y1 <= y2) ? ((y1 >= y2) ? ((x1 <= x2) ? ((x1 >= x2) ? 0 : -1) : 1) : -1) : 1;

            int IComparer.Compare(object x, object y)
            {
                ObjectInfo info = (ObjectInfo) x;
                ObjectInfo info2 = (ObjectInfo) y;
                return this.Compare(info.ColIndex, info.RowIndex, info2.ColIndex, info2.RowIndex);
            }
        }
    }
}

