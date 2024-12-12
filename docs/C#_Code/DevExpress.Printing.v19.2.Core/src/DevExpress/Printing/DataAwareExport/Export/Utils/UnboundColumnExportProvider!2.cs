namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    internal class UnboundColumnExportProvider<TCol, TRow> : ColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private bool unboundExpressionValid;

        public UnboundColumnExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
            this.unboundExpressionValid = (base.targetLocalColumn.UnboundInfo != null) && this.ValidateUnboundExpression(base.targetLocalColumn.UnboundInfo.UnboundExpression);
        }

        protected override bool IgnoreOverride(SheetAreaType area) => 
            (area == SheetAreaType.GroupHeader) || (area == SheetAreaType.Header);

        protected virtual void SetExpressionCore(IXlCell cell, object value, CriteriaOperator co, UnboundColumnType unboundType, int rowIndex)
        {
            XlExpression expression = base.columnInfo.ExpressionConverter.Execute(co);
            switch (unboundType)
            {
                case UnboundColumnType.Bound:
                    base.SetValueCore(cell, value, rowIndex, false);
                    return;

                case UnboundColumnType.DateTime:
                    base.SetValueCore(cell, value, rowIndex, false);
                    return;

                case UnboundColumnType.String:
                    if (value == null)
                    {
                        break;
                    }
                    base.SetValueCore(cell, value.ToString(), rowIndex, false);
                    return;

                case UnboundColumnType.Boolean:
                    base.SetValueCore(cell, value, rowIndex, false);
                    return;

                default:
                    this.SetFormulaCore(cell, expression);
                    break;
            }
        }

        protected virtual void SetFormulaCore(IXlCell cell, XlExpression expression)
        {
            cell.SetFormula(expression);
        }

        protected override void SetValueCore(IXlCell cell, object value, int rowIndex, bool ignoreOverride = false)
        {
            if ((base.columnInfo.Options.UnboundExpressionExportMode == UnboundExpressionExportMode.AsValue) | ignoreOverride)
            {
                base.SetValueCore(cell, value, rowIndex, false);
            }
            else if ((base.targetLocalColumn.UnboundInfo != null) && !string.IsNullOrEmpty(base.targetLocalColumn.UnboundInfo.UnboundExpression))
            {
                if (!this.unboundExpressionValid)
                {
                    base.SetValueCore(cell, value, rowIndex, false);
                }
                else
                {
                    CriteriaOperator co = CriteriaOperator.TryParse(base.targetLocalColumn.UnboundInfo.UnboundExpression, new object[0]);
                    if (co == null)
                    {
                        base.SetValueCore(cell, value, rowIndex, false);
                    }
                    else
                    {
                        try
                        {
                            XlCellReferenceMode referenceMode = base.columnInfo.ExpressionConverter.Context.ReferenceMode;
                            XlCellPosition currentCell = base.columnInfo.ExpressionConverter.Context.CurrentCell;
                            base.columnInfo.ExpressionConverter.Context.ReferenceMode = XlCellReferenceMode.Reference;
                            base.columnInfo.ExpressionConverter.Context.CurrentCell = cell.Position;
                            try
                            {
                                this.SetExpressionCore(cell, value, co, base.targetLocalColumn.UnboundInfo.UnboundType, rowIndex);
                            }
                            finally
                            {
                                base.columnInfo.ExpressionConverter.Context.ReferenceMode = referenceMode;
                                base.columnInfo.ExpressionConverter.Context.CurrentCell = currentCell;
                            }
                        }
                        catch (ExpressionConversionException)
                        {
                            base.SetValueCore(cell, value, rowIndex, false);
                        }
                    }
                }
            }
        }

        internal bool ValidateUnboundExpression(string expression)
        {
            Func<TCol, bool> predicate = <>c<TCol, TRow>.<>9__2_0;
            if (<>c<TCol, TRow>.<>9__2_0 == null)
            {
                Func<TCol, bool> local1 = <>c<TCol, TRow>.<>9__2_0;
                predicate = <>c<TCol, TRow>.<>9__2_0 = x => (x.GroupIndex == -1) && x.IsVisible;
            }
            List<TCol> source = base.columnInfo.ColumnsInfoCollection.Where<TCol>(predicate).ToList<TCol>();
            MatchCollection matchs = Regex.Matches(expression, @"\[([^]]*)\]");
            for (int i = 0; i < matchs.Count; i++)
            {
                Match part = matchs[i];
                TCol local = source.FirstOrDefault<TCol>(x => string.Equals(x.FieldName, part.Groups[1].Value));
                if (local == null)
                {
                    return false;
                }
            }
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UnboundColumnExportProvider<TCol, TRow>.<>c <>9;
            public static Func<TCol, bool> <>9__2_0;

            static <>c()
            {
                UnboundColumnExportProvider<TCol, TRow>.<>c.<>9 = new UnboundColumnExportProvider<TCol, TRow>.<>c();
            }

            internal bool <ValidateUnboundExpression>b__2_0(TCol x) => 
                (x.GroupIndex == -1) && x.IsVisible;
        }
    }
}

