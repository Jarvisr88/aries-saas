namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Linq;

    internal class AdvBandedLookUpValuesExporter<TCol, TRow> : LookUpValuesExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private AdvBandedExportInfo<TCol, TRow> advBandedExportInfo;

        public AdvBandedLookUpValuesExporter(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.advBandedExportInfo = exportInfo as AdvBandedExportInfo<TCol, TRow>;
        }

        protected override void SetRanges(XlCellRange sourcerangeitem, IColumn gridColumn)
        {
            BandNodeDescriptor descriptor;
            this.advBandedExportInfo.BandsLayoutInfo.TryGetValue((TCol) gridColumn, out descriptor);
            if (descriptor.Column != null)
            {
                int num = this.advBandedExportInfo.BandedRowPattern.FindColumnRowIndexInTemplate(descriptor.Column.FieldName);
                int bandedRowPatternCount = this.advBandedExportInfo.BandedRowPatternCount;
                if (bandedRowPatternCount == 1)
                {
                    base.SetRanges(sourcerangeitem, gridColumn);
                }
                else
                {
                    for (int i = 0; i < base.ExportInfo.GroupsList.Count; i++)
                    {
                        Group objA = base.ExportInfo.GroupsList[i];
                        if ((sourcerangeitem != null) && (objA.End != 0))
                        {
                            int end = objA.End;
                            if (Equals(objA, base.ExportInfo.GroupsList.Last<Group>()))
                            {
                                end--;
                            }
                            for (int j = objA.Start; j < end; j += bandedRowPatternCount)
                            {
                                int startrow = j + num;
                                base.SetDataValidationOnCellsGroup(sourcerangeitem, descriptor.ColIndex, startrow, startrow);
                            }
                        }
                    }
                }
            }
        }
    }
}

