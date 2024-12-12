namespace DevExpress.Printing.DataAwareExport.Export.TableExport
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;

    internal class TableExportInfo<TCol, TRow> : ExportInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private bool _useTableTotalFooter;

        public TableExportInfo(GridViewExcelExporter<TCol, TRow> helper) : base(helper)
        {
            this._useTableTotalFooter = true;
        }

        internal override void CreateColumnExportProvidersCore(List<IColumnExportProvider<TRow>> providers)
        {
            foreach (TCol local in base.GridColumns)
            {
                if (local != null)
                {
                    if (!base.ColumnHasValidUnboundInfo(local))
                    {
                        providers.Add(new TableColumnExportProvider<TCol, TRow>(local, this.ColumnExportInfo, local.VisibleIndex));
                        continue;
                    }
                    TableExportContext<TCol, TRow> context = base.Helper.Context as TableExportContext<TCol, TRow>;
                    if (context != null)
                    {
                        providers.Add(new UnboundColumnTableExportProvider<TCol, TRow>(context.Table, local, this.ColumnExportInfo, local.VisibleIndex));
                    }
                }
            }
        }

        internal override ExportHelpersProvider<TCol, TRow> CreateHelpersProvider() => 
            new TableExportHelpersProvider<TCol, TRow>(this);

        internal void RaiseBeforeExportTable(IXlTable table)
        {
            if (base.Options.CanRaiseBeforeExportTable)
            {
                BeforeExportTableEventArgs ea = new BeforeExportTableEventArgs {
                    Table = table,
                    UseTableTotalFooter = this.UseTableTotalFooter
                };
                base.Options.RaiseBeforeExportTable(ea);
                this.UseTableTotalFooter = ea.UseTableTotalFooter;
            }
        }

        internal bool UseTableTotalFooter
        {
            get => 
                this._useTableTotalFooter;
            set => 
                this._useTableTotalFooter = value;
        }

        public override bool AllowHorzLines =>
            false;

        public override bool AllowVertLines =>
            false;

        public override bool ApplyFormattingToEntireColumn =>
            false;
    }
}

