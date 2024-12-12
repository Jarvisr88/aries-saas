namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.XtraExport.Helpers;
    using System;

    internal static class CustomizationEventsUtils<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public static ExportSummaryItem GetExportedItem(ISummaryItemEx item)
        {
            ExportSummaryItem item2 = new ExportSummaryItem();
            if (item != null)
            {
                item2.DisplayFormat = item.DisplayFormat;
                item2.FieldName = item.FieldName;
                item2.SummaryType = SummaryExportUtils.ConvertSummaryItemTypeToExcel(item.SummaryType);
                item2.AlignByColumnInFooter = item.AlignByColumnInFooter;
            }
            return item2;
        }

        public static void OnCustomizeTableFooterCell(CustomizeTableSummaryCellInfo<TCol, TRow> info, ExportInfo<TCol, TRow> exportInfo)
        {
            CustomizeCellEventArgsExtended ea = info.CreateEventArgs();
            exportInfo.Options.RaiseCustomizeCellEvent(ea);
            if (ea.Handled && (ea.AreaType == info.AreaType))
            {
                info.TableColumn.TotalRowFormatting = ea.Formatting.ConvertWith(exportInfo.AllowHorzLines, exportInfo.AllowVertLines);
            }
        }

        public static bool RaiseCustomizeCellEvent(CustomizeCellInfo<TCol, TRow> info, ExportInfo<TCol, TRow> exportInfo)
        {
            CustomizeCellEventArgsExtended ea = info.CreateEventArgs();
            exportInfo.Options.RaiseCustomizeCellEvent(ea);
            if (!ea.Handled || (ea.AreaType != info.AreaType))
            {
                return false;
            }
            info.Hyperlink = ea.Hyperlink;
            info.CellValue = ea.Value;
            info.Cell.Formatting = ea.Formatting.ConvertWith(exportInfo.AllowHorzLines, exportInfo.AllowVertLines);
            if ((info.Cell.Formatting != null) && ((info.Cell.Formatting.NumberFormat == null) && string.IsNullOrWhiteSpace(info.Cell.Formatting.NetFormatString)))
            {
                FormattingUtils.PrimaryFormatColumn(info.Column, info.Cell.Formatting);
            }
            return true;
        }

        public static void RaiseCustomizeSummaryCellEvent(CustomizeSummaryCellInfo<TCol, TRow> info, ExportInfo<TCol, TRow> exportInfo)
        {
            ExportSummaryItem exportedItem = CustomizationEventsUtils<TCol, TRow>.GetExportedItem(info.Item);
            info.ItemEx = exportedItem;
            CustomizeCellEventArgsExtended ea = info.CreateEventArgs();
            exportInfo.Options.RaiseCustomizeCellEvent(ea);
            bool eventHandled = ea.Handled && (ea.AreaType == info.AreaType);
            if (eventHandled)
            {
                if (ea.Value != info.SummaryValue)
                {
                    info.Cell.Value = XlVariantValue.FromObject(ea.Value);
                }
                info.Cell.Formatting = ea.Formatting.ConvertWith(exportInfo.AllowHorzLines, exportInfo.AllowVertLines);
                info.Cell.Formatting.NetFormatString = !string.IsNullOrEmpty(ea.SummaryItem.DisplayFormat) ? ea.SummaryItem.DisplayFormat : ea.Formatting.FormatString;
            }
            if (info.Cell.Value.IsEmpty && (exportedItem != null))
            {
                exportInfo.HelpersProvider.SummaryExporter.ExportItem(info.AreaType, info.GroupRanges, info.SummaryValue, exportedItem.FieldName, exportedItem.DisplayFormat, exportedItem.SummaryType, info.Column, info.Cell, eventHandled, true, exportedItem.AlignByColumnInFooter);
            }
        }
    }
}

