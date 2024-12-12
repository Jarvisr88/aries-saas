namespace DevExpress.Printing.ExportHelpers
{
    using System;

    public abstract class ExportHelperBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private ExportInfo<TCol, TRow> _exportInfo;

        protected ExportHelperBase(ExportInfo<TCol, TRow> exportInfo)
        {
            this._exportInfo = exportInfo;
        }

        public abstract void Execute();

        protected ExportInfo<TCol, TRow> ExportInfo =>
            this._exportInfo;
    }
}

