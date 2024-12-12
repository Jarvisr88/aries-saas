namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class DefaultClipboardConverter
    {
        private static readonly DateTime minExcelDateTime = XlVariantValue.BaseDate.AddDays(1.0);
        private static ColorConverter colorConverter = new ColorConverter();

        public static void ConvertIClipboardRowValue(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            Type[] cellTypes = clipboardRow.GetCellTypes();
            if (((i < cellTypes.Length) && (iColumn.ColumnType != cellTypes[i])) && (cellTypes[i] != null))
            {
                if (cellTypes[i] == typeof(string))
                {
                    if (TryConvertToNullable(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                    if (TryConvertToTimeSpan(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                    if (TryConvertToImage(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                    if (TryConvertToByteArray(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                    if (TryConvertToEnum(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                    if (TryConvertToDateTime(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                    if (TryConvertToColor(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                    if (TryConvertToGuid(iColumn, clipboardRow, i))
                    {
                        return;
                    }
                }
                if ((cellTypes[i] == typeof(DateTime)) && iColumn.ColumnType.TypeOrUnderlyingTypeEquals(typeof(TimeSpan)))
                {
                    clipboardRow.Cells[i] = (TimeSpan) (((DateTime) clipboardRow.Cells[i]) - minExcelDateTime);
                }
            }
        }

        public static bool IsBandCaptionRow(ClipboardBandLayoutInfo layoutInfo, IClipboardRow row, ref int bandLevel, ref bool bandsSkipped)
        {
            if (bandsSkipped)
            {
                return false;
            }
            if (bandLevel >= layoutInfo.BandPanelInfo.Length)
            {
                bandsSkipped = true;
                return false;
            }
            Func<object, bool> predicate = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__3_0;
                predicate = <>c.<>9__3_0 = e => (e != null) && !string.IsNullOrEmpty(e.ToString());
            }
            IEnumerable<object> source = row.Cells.Where<object>(predicate);
            for (int i = 0; i < source.Count<object>(); i++)
            {
                if (i >= layoutInfo.BandPanelInfo[bandLevel].Count<ClipboardBandCellInfo>())
                {
                    bandsSkipped = true;
                    return false;
                }
                if (!Equals(layoutInfo.BandPanelInfo[bandLevel][i].DisplayValue, source.ElementAt<object>(i)))
                {
                    bandsSkipped = true;
                    return false;
                }
            }
            bandLevel++;
            return true;
        }

        private static bool TryConvertToByteArray(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            if (!iColumn.ColumnType.TypeOrUnderlyingTypeEquals(typeof(byte[])))
            {
                return false;
            }
            clipboardRow.Cells[i] = null;
            return true;
        }

        private static bool TryConvertToColor(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            if (!iColumn.ColumnType.TypeOrUnderlyingTypeEquals(typeof(Color)))
            {
                return false;
            }
            try
            {
                clipboardRow.Cells[i] = colorConverter.ConvertFromString((string) clipboardRow.Cells[i]);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryConvertToDateTime(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            DateTime time;
            if (!iColumn.ColumnType.TypeOrUnderlyingTypeEquals(typeof(DateTime)))
            {
                return false;
            }
            if (!DateTime.TryParse((string) clipboardRow.Cells[i], out time))
            {
                return false;
            }
            clipboardRow.Cells[i] = time;
            return true;
        }

        private static bool TryConvertToEnum(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            if (!iColumn.ColumnType.IsEnum)
            {
                return false;
            }
            try
            {
                object obj2 = Enum.Parse(iColumn.ColumnType, (string) clipboardRow.Cells[i]);
                clipboardRow.Cells[i] = obj2;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryConvertToGuid(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            Guid guid;
            if (!iColumn.ColumnType.TypeOrUnderlyingTypeEquals(typeof(Guid)))
            {
                return false;
            }
            if (!Guid.TryParse((string) clipboardRow.Cells[i], out guid))
            {
                return false;
            }
            clipboardRow.Cells[i] = guid;
            return true;
        }

        private static bool TryConvertToImage(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            if (!iColumn.ColumnType.TypeOrUnderlyingTypeEquals(typeof(Image)))
            {
                return false;
            }
            clipboardRow.Cells[i] = null;
            return true;
        }

        private static bool TryConvertToNullable(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            if (Nullable.GetUnderlyingType(iColumn.ColumnType) == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty((string) clipboardRow.Cells[i]))
            {
                return false;
            }
            clipboardRow.Cells[i] = null;
            return true;
        }

        private static bool TryConvertToTimeSpan(IColumn iColumn, IClipboardRow clipboardRow, int i)
        {
            TimeSpan span;
            if (!iColumn.ColumnType.TypeOrUnderlyingTypeEquals(typeof(TimeSpan)))
            {
                return false;
            }
            if (!TimeSpan.TryParse((string) clipboardRow.Cells[i], out span))
            {
                return false;
            }
            clipboardRow.Cells[i] = span;
            return true;
        }

        private static bool TypeOrUnderlyingTypeEquals(this Type checkType, Type matchType) => 
            (checkType == matchType) || (Nullable.GetUnderlyingType(checkType) == matchType);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultClipboardConverter.<>c <>9 = new DefaultClipboardConverter.<>c();
            public static Func<object, bool> <>9__3_0;

            internal bool <IsBandCaptionRow>b__3_0(object e) => 
                (e != null) && !string.IsNullOrEmpty(e.ToString());
        }
    }
}

