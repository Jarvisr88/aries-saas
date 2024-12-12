namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class AdvBandedConditionalFormattingExporter<TCol, TRow> : ConditionalFormattingExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private AdvBandedExportInfo<TCol, TRow> advBandedExportInfo;
        private int offset;

        public AdvBandedConditionalFormattingExporter(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.advBandedExportInfo = exportInfo as AdvBandedExportInfo<TCol, TRow>;
        }

        protected override IList<XlCellRange> CalcColumnRange(IFormatRuleBase rule)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            IColumn positionColumn = base.GetPositionColumn(rule);
            BandNodeDescriptor validDescriptor = this.GetValidDescriptor(positionColumn);
            if (validDescriptor.Column != null)
            {
                int num = this.advBandedExportInfo.BandedRowPattern.FindColumnRowIndexInTemplate(validDescriptor.Column.FieldName);
                int bandedRowPatternCount = this.advBandedExportInfo.BandedRowPatternCount;
                if (bandedRowPatternCount == 1)
                {
                    return base.CalcColumnRange(rule);
                }
                if (base.CheckPositionColumn(positionColumn))
                {
                    int column = rule.ApplyToRow ? 0 : validDescriptor.ColIndex;
                    int num4 = rule.ApplyToRow ? (base.ExportInfo.Exporter.CurrentColumnIndex - 1) : validDescriptor.ColIndex;
                    for (int i = 0; i < base.ExportInfo.ExportRowIndex; i += bandedRowPatternCount)
                    {
                        int row = i + num;
                        XlCellRange item = new XlCellRange(new XlCellPosition(column, row), new XlCellPosition(num4, row));
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        protected override void CalcRulePosition(XlConditionalFormatting cf, IFormatRuleBase rule, bool iconRule = false)
        {
            IColumn positionColumn = base.GetPositionColumn(rule);
            BandNodeDescriptor validDescriptor = this.GetValidDescriptor(positionColumn);
            if (validDescriptor.Column != null)
            {
                int num = this.advBandedExportInfo.BandedRowPattern.FindColumnRowIndexInTemplate(validDescriptor.Column.FieldName);
                int bandedRowPatternCount = this.advBandedExportInfo.BandedRowPatternCount;
                if (bandedRowPatternCount == 1)
                {
                    base.CalcRulePosition(cf, rule, iconRule);
                }
                else
                {
                    for (int i = 0; i < base.ExportInfo.GroupsList.Count; i++)
                    {
                        Group objB = base.ExportInfo.GroupsList[i];
                        int num4 = !Equals(base.ExportInfo.GroupsList.Last<Group>(), objB) ? objB.End : (objB.End - 1);
                        if ((num4 >= 0) && base.CheckPositionColumn(positionColumn))
                        {
                            int column = rule.ApplyToRow ? 0 : validDescriptor.ColIndex;
                            int num6 = rule.ApplyToRow ? (base.ExportInfo.Exporter.CurrentColumnIndex - 1) : validDescriptor.ColIndex;
                            for (int j = objB.Start; j < num4; j += bandedRowPatternCount)
                            {
                                int row = j + num;
                                cf.Ranges.Add(new XlCellRange(new XlCellPosition(column, row), new XlCellPosition(num6, row)));
                            }
                        }
                    }
                }
            }
        }

        protected override void ExportExpression(string ruleExpression, XlDifferentialFormatting formatting, IFormatRuleBase rule)
        {
            if (!rule.ApplyToRow)
            {
                base.ExportInfo.Converter.Context.RowOffset = 0;
                base.ExportExpression(ruleExpression, formatting, rule);
            }
            else
            {
                base.ExportInfo.Converter.Context.RowOffset = 0;
                this.offset = 0;
                int count = this.advBandedExportInfo.BandedRowPattern.Count;
                if (count == 1)
                {
                    base.ExportExpression(ruleExpression, formatting, rule);
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        base.ExportExpression(ruleExpression, formatting, rule);
                        IXlExpressionContextEx context = base.ExportInfo.Converter.Context;
                        int rowOffset = context.RowOffset;
                        context.RowOffset = rowOffset - 1;
                        this.offset++;
                    }
                }
            }
        }

        protected override void ExportExpressionCore(XlDifferentialFormatting formatting, IFormatRuleBase rule, XlExpression expression)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            XlCondFmtRuleExpression expression1 = new XlCondFmtRuleExpression(expression);
            expression1.Formatting = XlFormatting.CopyObject<XlDifferentialFormatting>(formatting);
            XlCondFmtRuleExpression item = expression1;
            this.CalcRulePosition(cf, rule, false);
            if (rule.ApplyToRow)
            {
                for (int i = 0; i < cf.Ranges.Count; i++)
                {
                    cf.Ranges[i].Offset(0, this.offset);
                }
            }
            cf.Rules.Add(item);
            base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
        }

        private BandNodeDescriptor GetValidDescriptor(IColumn positionCol)
        {
            Func<BandNodeDescriptor, bool> predicate = (positionCol != null) ? this.SearchDescPredicate(positionCol) : (<>c<TCol, TRow>.<>9__7_0 ??= x => true);
            return this.advBandedExportInfo.BandsLayoutInfo.Values.FirstOrDefault<BandNodeDescriptor>(predicate);
        }

        private Func<BandNodeDescriptor, bool> SearchDescPredicate(IColumn positionCol) => 
            x => (positionCol != null) && (x.Column.FieldName == positionCol.FieldName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AdvBandedConditionalFormattingExporter<TCol, TRow>.<>c <>9;
            public static Func<BandNodeDescriptor, bool> <>9__7_0;

            static <>c()
            {
                AdvBandedConditionalFormattingExporter<TCol, TRow>.<>c.<>9 = new AdvBandedConditionalFormattingExporter<TCol, TRow>.<>c();
            }

            internal bool <GetValidDescriptor>b__7_0(BandNodeDescriptor x) => 
                true;
        }
    }
}

