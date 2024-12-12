﻿namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections.Generic;

    internal class PivotDataAwareExportInfo<TCol, TRow> : ExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public PivotDataAwareExportInfo(GridViewExcelExporter<TCol, TRow> helper) : base(helper)
        {
        }

        internal override void CreateColumnExportProvidersCore(List<IColumnExportProvider<TRow>> providers)
        {
            foreach (TCol local in base.GridColumns)
            {
                if (local != null)
                {
                    providers.Add(new PivotColumnExportProvider<TCol, TRow>(local, this.ColumnExportInfo, local.VisibleIndex));
                }
            }
        }

        internal override void RaiseSkipRowEvent(FooterAreaType areaType, int areaRowHandle, XlGroup group)
        {
        }
    }
}

