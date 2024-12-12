namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class ConditionalFormattingExporter<TCol, TRow> : ExportHelperBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public ConditionalFormattingExporter(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        protected XlCellRange CalcColumnDataRange(IFormatRuleBase rule)
        {
            XlCellRange range = null;
            IColumn positionColumn = this.GetPositionColumn(rule);
            int index = base.ExportInfo.ColumnsInfoColl.IndexOf((TCol) positionColumn);
            bool applyToRow = rule.ApplyToRow;
            if (this.CheckPositionColumn(positionColumn) | applyToRow)
            {
                int column = applyToRow ? 0 : index;
                int num3 = applyToRow ? (base.ExportInfo.Exporter.CurrentColumnIndex - 1) : index;
                bool flag2 = base.ExportInfo.GroupsList.Count > 0;
                int row = flag2 ? base.ExportInfo.GroupsList.First<Group>().Start : 0;
                range = new XlCellRange(new XlCellPosition(column, row, XlPositionType.Absolute, XlPositionType.Relative), new XlCellPosition(num3, flag2 ? base.ExportInfo.GroupsList.Last<Group>().End : base.ExportInfo.ExportRowIndex, XlPositionType.Absolute, XlPositionType.Relative));
            }
            return range;
        }

        protected virtual IList<XlCellRange> CalcColumnRange(IFormatRuleBase rule)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            IColumn positionColumn = this.GetPositionColumn(rule);
            int index = base.ExportInfo.ColumnsInfoColl.IndexOf((TCol) positionColumn);
            if (positionColumn != null)
            {
                int column = index;
                list.Add(new XlCellRange(new XlCellPosition(index, 0, XlPositionType.Absolute, XlPositionType.Relative), new XlCellPosition(column, base.ExportInfo.ExportRowIndex, XlPositionType.Absolute, XlPositionType.Relative)));
            }
            return list;
        }

        private IList<XlCellRange> CalcLookupColumnRange(IColumn column, string sheetName = "")
        {
            List<XlCellRange> list = new List<XlCellRange>();
            if (column != null)
            {
                int num2 = 1;
                XlCellRange item = new XlCellRange(new XlCellPosition(1, 0, XlPositionType.Absolute, XlPositionType.Relative), new XlCellPosition(num2, base.ExportInfo.ExportRowIndex - 1, XlPositionType.Absolute, XlPositionType.Relative));
                if (!string.Equals(sheetName, string.Empty))
                {
                    item.SheetName = sheetName;
                }
                list.Add(item);
            }
            return list;
        }

        private void CalcPositionForAllCells(XlConditionalFormatting cf, IFormatRuleBase rule, IColumn positionCol, int columnPosition)
        {
            this.CalcPositionForGroups(cf, rule, positionCol, columnPosition);
        }

        private void CalcPositionForGroups(XlConditionalFormatting cf, IFormatRuleBase rule, IColumn positionCol, int columnPosition)
        {
            for (int i = 0; i < base.ExportInfo.GroupsList.Count; i++)
            {
                Group objB = base.ExportInfo.GroupsList[i];
                int row = !Equals(base.ExportInfo.GroupsList.Last<Group>(), objB) ? objB.End : (objB.End - 1);
                if ((row >= 0) && (this.CheckPositionColumn(positionCol) || rule.ApplyToRow))
                {
                    int column = rule.ApplyToRow ? 0 : columnPosition;
                    int num4 = rule.ApplyToRow ? (base.ExportInfo.Exporter.CurrentColumnIndex - 1) : columnPosition;
                    cf.Ranges.Add(new XlCellRange(new XlCellPosition(column, objB.Start), new XlCellPosition(num4, row)));
                }
            }
        }

        protected virtual void CalcRulePosition(XlConditionalFormatting cf, IFormatRuleBase rule, bool iconRule = false)
        {
            IColumn positionColumn = this.GetPositionColumn(rule);
            int index = base.ExportInfo.ColumnsInfoColl.IndexOf((TCol) positionColumn);
            if ((index != 0) & iconRule)
            {
                this.CalcPositionForAllCells(cf, rule, positionColumn, index);
            }
            else
            {
                this.CalcPositionForGroups(cf, rule, positionColumn, index);
            }
        }

        private static bool CanDefaultMaxValue(IFormatConditionRuleMinMaxBase rule) => 
            (rule.MaxType == XlCondFmtValueObjectType.Percent) && (((decimal) rule.MaxValue) == 0M);

        private static bool CanDefaultMidValue(IFormatConditionRule3ColorScale gridRule) => 
            (gridRule.MidpointType == XlCondFmtValueObjectType.Percent) && (((decimal) gridRule.MidpointValue) == 0M);

        private static bool CanDefaultMinValue(IFormatConditionRuleMinMaxBase rule) => 
            (rule.MinType == XlCondFmtValueObjectType.Percent) && (((decimal) rule.MinValue) == 0M);

        private bool CanExportRule(IFormatRuleBase fmtritem)
        {
            if (fmtritem.ApplyToRow)
            {
                return fmtritem.Enabled;
            }
            IColumn positionColumn = this.GetPositionColumn(fmtritem);
            bool flag = (positionColumn != null) && positionColumn.IsVisible;
            IColumn sourceColumn = this.GetSourceColumn(fmtritem);
            bool flag2 = (sourceColumn != null) && sourceColumn.IsVisible;
            return ((fmtritem.Enabled & flag) & flag2);
        }

        private bool CheckConditions(bool lockChange, bool percent) => 
            !lockChange && (!percent && base.ExportInfo.AutoSelectMinimumIconValue);

        private void CheckDataValidationItems(IColumn sourceColumn, string ruleExpression)
        {
            if ((sourceColumn?.DataValidationItems != null) && !string.IsNullOrEmpty(ruleExpression))
            {
                foreach (object obj2 in sourceColumn.DataValidationItems.Values)
                {
                    if ((obj2 != null) && ruleExpression.Contains(obj2.ToString()))
                    {
                        string fieldName = sourceColumn.FieldName;
                        base.ExportInfo.Converter.Context.LookupColumns[fieldName].ResultColumn = 1;
                        break;
                    }
                }
            }
        }

        protected bool CheckPositionColumn(IColumn positionCol) => 
            (positionCol != null) && ((positionCol.GroupIndex == -1) && positionCol.IsVisible);

        private XlCellRange ConstructRange(int itemsCount) => 
            new XlCellRange(new XlCellPosition(0, 0), new XlCellPosition(1, itemsCount)).AsAbsolute();

        private void ConvertAboveAverageToExpression(IFormatRuleBase rule, IFormatConditionRuleAboveBelowAverage condFmtRab, XlCondFmtRuleAboveAverage exrule)
        {
            XlCondFmtExpressionFactory factory = new XlCondFmtExpressionFactory();
            if (!this.RuleSourceAppliedToLookupColumn(rule))
            {
                IList<XlCellRange> ranges = this.CalcColumnRange(rule);
                if (rule.ApplyToRow && (ranges.Count > 0))
                {
                    XlCellOffset cellOffset = new XlCellOffset(ranges[0].LastColumn, ranges[0].FirstRow, XlCellOffsetType.Position, XlCellOffsetType.Offset);
                    this.ExportExpressionCore(condFmtRab.Formatting, rule, factory.CreateAboveAverageExpression(exrule, ranges, cellOffset));
                }
            }
            else
            {
                IColumn sourceColumn = this.GetSourceColumn(rule);
                if (sourceColumn != null)
                {
                    IList<XlCellRange> ranges = this.CalcLookupColumnRange(sourceColumn, sourceColumn.FieldName);
                    XlCellOffset cellOffset = new XlCellOffset(sourceColumn.LogicalPosition, 0, XlCellOffsetType.Position, XlCellOffsetType.Offset);
                    this.ExportExpressionCore(condFmtRab.Formatting, rule, factory.CreateAboveAverageExpression(exrule, ranges, cellOffset, true, this.CreateLookupFuncArea(sourceColumn)));
                }
            }
        }

        private void ConvertContainsRuleToExpression(string expressionFormat, IFormatRuleBase rule, IFormatConditionRuleContains condFmtRc)
        {
            IColumn sourceColumn = this.GetSourceColumn(rule);
            string str = (sourceColumn != null) ? sourceColumn.FieldName : string.Empty;
            for (int i = 0; i < condFmtRc.Values.Count; i++)
            {
                object obj2 = condFmtRc.Values[i];
                this.ExportExpression(string.Format(expressionFormat, str, obj2), condFmtRc.Appearance, rule);
            }
        }

        private bool ConvertToExpression(IFormatRuleBase rule) => 
            (rule.ApplyToRow || (rule.ColumnApplyTo != null)) || this.RuleSourceAppliedToLookupColumn(rule);

        private void ConvertTop10ToExpression(IFormatRuleBase rule, IFormatConditionRuleTopBottom condFmtRtb, XlCondFmtRuleTop10 exrule)
        {
            XlCondFmtExpressionFactory factory = new XlCondFmtExpressionFactory();
            if (!this.RuleSourceAppliedToLookupColumn(rule))
            {
                IList<XlCellRange> ranges = this.CalcColumnRange(rule);
                if (rule.ApplyToRow && (ranges.Count > 0))
                {
                    XlCellOffset cellOffset = new XlCellOffset(ranges[0].LastColumn, ranges[0].FirstRow, XlCellOffsetType.Position, XlCellOffsetType.Offset);
                    this.ExportExpressionCore(condFmtRtb.Appearance, rule, factory.CreateTop10Expression(exrule, ranges, cellOffset));
                }
            }
            else
            {
                IColumn sourceColumn = this.GetSourceColumn(rule);
                if (sourceColumn != null)
                {
                    IList<XlCellRange> ranges = this.CalcLookupColumnRange(sourceColumn, sourceColumn.FieldName);
                    XlCellOffset cellOffset = new XlCellOffset(sourceColumn.LogicalPosition, 0, XlCellOffsetType.Position, XlCellOffsetType.Offset);
                    this.ExportExpressionCore(condFmtRtb.Appearance, rule, factory.CreateTop10Expression(exrule, ranges, cellOffset, true, this.CreateLookupFuncArea(sourceColumn)));
                }
            }
        }

        private XlCondFmtRuleIconSet CreateIconSet(IFormatConditionRuleIconSet exrule)
        {
            XlCondFmtRuleIconSet set1 = new XlCondFmtRuleIconSet();
            set1.IconSetType = ConditionalFormattingExporter<TCol, TRow>.GetIconSetType(exrule);
            set1.Reverse = exrule.Reverse;
            set1.ShowValues = exrule.ShowValues;
            set1.Percent = exrule.Percent;
            return set1;
        }

        private void CreateLookupColumnsInfo(Dictionary<string, XlColumnLookupInfo> contextLookupColumns)
        {
            for (int i = 0; i < base.ExportInfo.ColumnsInfoColl.Count; i++)
            {
                TCol local = base.ExportInfo.ColumnsInfoColl[i];
                IDictionary<object, object> dataValidationItems = local.DataValidationItems;
                if ((dataValidationItems != null) && (dataValidationItems.Count > 0))
                {
                    XlColumnLookupInfo info = new XlColumnLookupInfo {
                        ResultColumn = 2,
                        Range = this.ConstructRange(base.ExportInfo.ExportRowIndex - 1)
                    };
                    info.Range.SheetName = local.FieldName;
                    info.ApproximateMatch = false;
                    if (!contextLookupColumns.ContainsKey(local.FieldName))
                    {
                        contextLookupColumns.Add(local.FieldName, info);
                    }
                }
            }
        }

        private XlPtgArea3d CreateLookupFuncArea(IColumn sourceColumn) => 
            new XlPtgArea3d(this.ConstructRange(base.ExportInfo.ExportRowIndex - 1), sourceColumn.FieldName);

        public override void Execute()
        {
            base.ExportInfo.Converter.Context.CurrentTable = null;
            if (base.ExportInfo.AllowLookupValues)
            {
                this.CreateLookupColumnsInfo(base.ExportInfo.Converter.Context.LookupColumns);
            }
            foreach (IFormatRuleBase base2 in base.ExportInfo.View.FormatRules.Reverse<IFormatRuleBase>())
            {
                if (this.CanExportRule(base2))
                {
                    if (base2.Rule is IFormatConditionRuleDataBar)
                    {
                        this.ExportDataBar(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleIconSet)
                    {
                        this.ExportIconSet(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleValue)
                    {
                        this.ExportFmtCondRuleValue(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleAboveBelowAverage)
                    {
                        this.ExportAboveBelowAverageRule(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleUniqueDuplicate)
                    {
                        this.ExportUniqueDuplicate(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRule3ColorScale)
                    {
                        this.ExportColorScale(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRule2ColorScale)
                    {
                        this.ExportColorScale(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleExpression)
                    {
                        this.ExportExpressionRule(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleTopBottom)
                    {
                        this.ExportTopBottomRule(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleContains)
                    {
                        this.ExportContainsRule(base2);
                        continue;
                    }
                    if (base2.Rule is IFormatConditionRuleDateOccuring)
                    {
                        this.ExportDateOccuringRule(base2);
                    }
                }
            }
        }

        private void ExportAboveBelowAverageRule(IFormatRuleBase rule)
        {
            IFormatConditionRuleAboveBelowAverage condFmtRab = rule.Rule as IFormatConditionRuleAboveBelowAverage;
            if (condFmtRab != null)
            {
                XlCondFmtRuleAboveAverage average1 = new XlCondFmtRuleAboveAverage();
                average1.StopIfTrue = rule.StopIfTrue;
                average1.Condition = condFmtRab.Condition;
                average1.Formatting = condFmtRab.Formatting;
                XlCondFmtRuleAboveAverage exrule = average1;
                if (this.ConvertToExpression(rule))
                {
                    this.ConvertAboveAverageToExpression(rule, condFmtRab, exrule);
                }
                else
                {
                    XlConditionalFormatting cf = new XlConditionalFormatting();
                    this.CalcRulePosition(cf, rule, false);
                    cf.Rules.Add(exrule);
                    base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
                }
            }
        }

        private void ExportColorScale(IFormatRuleBase rule)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            this.CalcRulePosition(cf, rule, false);
            IFormatConditionRuleColorScaleBase base2 = rule.Rule as IFormatConditionRuleColorScaleBase;
            XlCondFmtColorScaleType type = (rule.Rule is IFormatConditionRule3ColorScale) ? XlCondFmtColorScaleType.ColorScale3 : XlCondFmtColorScaleType.ColorScale2;
            if (base2 != null)
            {
                XlCondFmtRuleColorScale scale1 = new XlCondFmtRuleColorScale();
                scale1.ColorScaleType = type;
                scale1.MaxColor = base2.MaxColor;
                scale1.MinColor = base2.MinColor;
                XlCondFmtRuleColorScale colorScale = scale1;
                ConditionalFormattingExporter<TCol, TRow>.SetMinMaxValue(base2, colorScale.MinValue, colorScale.MaxValue);
                if (base2 is IFormatConditionRule3ColorScale)
                {
                    colorScale.MidpointColor = ((IFormatConditionRule3ColorScale) base2).MidpointColor;
                    ConditionalFormattingExporter<TCol, TRow>.SetMidValue(colorScale, (IFormatConditionRule3ColorScale) base2);
                }
                cf.Rules.Add(colorScale);
                base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
            }
        }

        private void ExportContainsRule(IFormatRuleBase rule)
        {
            IFormatConditionRuleContains condFmtRc = rule.Rule as IFormatConditionRuleContains;
            XlConditionalFormatting cf = new XlConditionalFormatting();
            this.CalcRulePosition(cf, rule, false);
            if (condFmtRc != null)
            {
                if (this.RuleSourceAppliedToLookupColumn(rule))
                {
                    this.ConvertContainsRuleToExpression("[{0}]={1}", rule, condFmtRc);
                }
                else if (rule.ApplyToRow)
                {
                    this.ConvertContainsRuleToExpression("Contains([{0}],'{1}')", rule, condFmtRc);
                }
                else
                {
                    for (int i = 0; i < condFmtRc.Values.Count; i++)
                    {
                        object obj2 = condFmtRc.Values[i];
                        XlCondFmtRuleSpecificText text1 = new XlCondFmtRuleSpecificText(XlCondFmtSpecificTextType.Contains, obj2.ToString());
                        text1.Formatting = condFmtRc.Appearance;
                        XlCondFmtRuleSpecificText item = text1;
                        cf.Rules.Add(item);
                    }
                }
            }
            base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
        }

        private void ExportDataBar(IFormatRuleBase rule)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            this.CalcRulePosition(cf, rule, false);
            IFormatConditionRuleDataBar condFmtRDataBar = rule.Rule as IFormatConditionRuleDataBar;
            if (condFmtRDataBar != null)
            {
                XlCondFmtRuleDataBar bar1 = new XlCondFmtRuleDataBar();
                bar1.AxisColor = condFmtRDataBar.AxisColor;
                bar1.FillColor = condFmtRDataBar.FillColor;
                bar1.BorderColor = condFmtRDataBar.BorderColor;
                bar1.GradientFill = condFmtRDataBar.GradientFill;
                bar1.NegativeFillColor = condFmtRDataBar.NegativeFillColor;
                bar1.NegativeBorderColor = condFmtRDataBar.NegativeBorderColor;
                bar1.Direction = ConditionalFormattingExporter<TCol, TRow>.GetDataBarDirection(condFmtRDataBar);
                bar1.AxisPosition = ConditionalFormattingExporter<TCol, TRow>.GetDataBarAxisPosition(condFmtRDataBar);
                XlCondFmtRuleDataBar item = bar1;
                ConditionalFormattingExporter<TCol, TRow>.SetMinMaxValue(condFmtRDataBar, item.MinValue, item.MaxValue);
                cf.Rules.Add(item);
                base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
            }
        }

        private void ExportDateOccuringRule(IFormatRuleBase rule)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            this.CalcRulePosition(cf, rule, false);
            IFormatConditionRuleDateOccuring occuring = rule.Rule as IFormatConditionRuleDateOccuring;
            if (occuring != null)
            {
                foreach (XlCondFmtTimePeriod period in occuring.DateTypes)
                {
                    if (period != ~XlCondFmtTimePeriod.Last7Days)
                    {
                        XlCondFmtRuleTimePeriod period1 = new XlCondFmtRuleTimePeriod();
                        period1.Formatting = occuring.Formatting;
                        period1.TimePeriod = period;
                        XlCondFmtRuleTimePeriod item = period1;
                        if (!rule.ApplyToRow)
                        {
                            cf.Rules.Add(item);
                            continue;
                        }
                        XlCondFmtExpressionFactory factory = new XlCondFmtExpressionFactory();
                        IList<XlCellRange> list = this.CalcColumnRange(rule);
                        if (list.Count > 0)
                        {
                            XlCellOffset cellOffset = new XlCellOffset(list[0].LastColumn, list[0].FirstRow, XlCellOffsetType.Position, XlCellOffsetType.Offset);
                            XlCondFmtRuleExpression expression1 = new XlCondFmtRuleExpression(factory.CreateTimePeriodExpression(item, cellOffset));
                            expression1.Formatting = XlFormatting.CopyObject<XlDifferentialFormatting>(occuring.Formatting);
                            XlCondFmtRuleExpression expression2 = expression1;
                            cf.Rules.Add(expression2);
                        }
                    }
                }
                base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
            }
        }

        protected virtual void ExportExpression(string ruleExpression, XlDifferentialFormatting formatting, IFormatRuleBase rule)
        {
            CriteriaOperator criteriaOperator = CriteriaOperator.TryParse(ruleExpression, new object[0]);
            if (criteriaOperator != null)
            {
                try
                {
                    if (this.RuleSourceAppliedToLookupColumn(rule))
                    {
                        this.CheckDataValidationItems(this.GetSourceColumn(rule), ruleExpression);
                    }
                    else
                    {
                        BinaryOperator operator2 = criteriaOperator as BinaryOperator;
                        if (operator2 != null)
                        {
                            string propertyName;
                            OperandProperty leftOperand = operator2.LeftOperand as OperandProperty;
                            if (leftOperand != null)
                            {
                                propertyName = leftOperand.PropertyName;
                            }
                            else
                            {
                                OperandProperty local1 = leftOperand;
                                propertyName = null;
                            }
                            string str = propertyName;
                            if (!string.IsNullOrEmpty(str))
                            {
                                IColumn sourceColumn = base.ExportInfo.ColumnsInfoColl[str];
                                if ((sourceColumn != null) && base.ExportInfo.AllowLookupValues)
                                {
                                    this.CheckDataValidationItems(sourceColumn, ruleExpression);
                                }
                            }
                        }
                    }
                    XlExpression expression = base.ExportInfo.Converter.Execute(criteriaOperator);
                    this.ExportExpressionCore(formatting, rule, expression);
                }
                catch (ExpressionConversionException)
                {
                }
            }
        }

        protected virtual void ExportExpressionCore(XlDifferentialFormatting formatting, IFormatRuleBase rule, XlExpression expression)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            XlCondFmtRuleExpression expression1 = new XlCondFmtRuleExpression(expression);
            expression1.Formatting = XlFormatting.CopyObject<XlDifferentialFormatting>(formatting);
            XlCondFmtRuleExpression item = expression1;
            this.CalcRulePosition(cf, rule, false);
            cf.Rules.Add(item);
            base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
        }

        private void ExportExpressionRule(IFormatRuleBase rule)
        {
            IFormatConditionRuleExpression expression = rule.Rule as IFormatConditionRuleExpression;
            if ((expression != null) && !string.IsNullOrEmpty(expression.Expression))
            {
                this.ProcessExpression(expression.Expression, expression.Appearance, rule);
            }
        }

        private void ExportFmtCondRuleValue(IFormatRuleBase rule)
        {
            IFormatConditionRuleValue value2 = rule.Rule as IFormatConditionRuleValue;
            if (!string.IsNullOrEmpty(value2.Expression) && (value2.Condition == FormatConditions.Expression))
            {
                this.ProcessExpression(value2.Expression, value2.Appearance, rule);
            }
            else if (this.ConvertToExpression(rule))
            {
                if (rule.Column != null)
                {
                    this.ExportExpression(ExpressionHelper.ConvertToExpression(value2, rule.Column.FieldName), value2.Appearance, rule);
                }
            }
            else
            {
                XlConditionalFormatting cf = new XlConditionalFormatting();
                XlCondFmtRuleCellIs is1 = new XlCondFmtRuleCellIs();
                is1.Operator = this.TransformFormatConditionToXCondFmtOperator(value2.Condition);
                is1.Formatting = XlFormatting.CopyObject<XlDifferentialFormatting>(value2.Appearance);
                XlCondFmtRuleCellIs item = is1;
                item.Value = XlValueObject.FromObject(value2.Value1);
                item.SecondValue = XlValueObject.FromObject(value2.Value2);
                this.CalcRulePosition(cf, rule, false);
                cf.Rules.Add(item);
                base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
            }
        }

        private void ExportIconSet(IFormatRuleBase rule)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            this.CalcRulePosition(cf, rule, true);
            IFormatConditionRuleIconSet exrule = rule.Rule as IFormatConditionRuleIconSet;
            if (exrule != null)
            {
                XlCondFmtRuleIconSet cfmtris = this.CreateIconSet(exrule);
                this.GetCustomIcons(cfmtris, exrule.Icons);
                if (exrule.Values.Count != 0)
                {
                    cfmtris.Values.Clear();
                    bool lockChange = false;
                    for (int i = 0; i < exrule.Values.Count; i++)
                    {
                        XlCondFmtValueObject item = exrule.Values[i];
                        if (this.CheckConditions(lockChange, exrule.Percent))
                        {
                            item.ObjectType = XlCondFmtValueObjectType.Percent;
                            item.Value = 0.0;
                            item.GreaterThanOrEqual = true;
                            lockChange = true;
                        }
                        cfmtris.Values.Add(item);
                    }
                }
                cf.Rules.Add(cfmtris);
                base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
            }
        }

        private void ExportTopBottomRule(IFormatRuleBase rule)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            this.CalcRulePosition(cf, rule, false);
            IFormatConditionRuleTopBottom condFmtRtb = rule.Rule as IFormatConditionRuleTopBottom;
            if (condFmtRtb != null)
            {
                XlCondFmtRuleTop10 top1 = new XlCondFmtRuleTop10();
                top1.StopIfTrue = rule.StopIfTrue;
                top1.Percent = condFmtRtb.Percent;
                top1.Rank = condFmtRtb.Rank;
                top1.Formatting = condFmtRtb.Appearance;
                top1.Bottom = condFmtRtb.Bottom;
                XlCondFmtRuleTop10 exrule = top1;
                if (rule.ApplyToRow || this.RuleSourceAppliedToLookupColumn(rule))
                {
                    this.ConvertTop10ToExpression(rule, condFmtRtb, exrule);
                }
                else
                {
                    cf.Rules.Add(exrule);
                    base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
                }
            }
        }

        private void ExportUniqueDuplicate(IFormatRuleBase rule)
        {
            XlConditionalFormatting cf = new XlConditionalFormatting();
            this.CalcRulePosition(cf, rule, false);
            IFormatConditionRuleUniqueDuplicate duplicate = rule.Rule as IFormatConditionRuleUniqueDuplicate;
            if (duplicate != null)
            {
                if (rule.ApplyToRow)
                {
                    XlCondFmtExpressionFactory factory = new XlCondFmtExpressionFactory();
                    IList<XlCellRange> list = this.CalcColumnRange(rule);
                    if (list.Count > 0)
                    {
                        XlCellOffset cellOffset = new XlCellOffset(list[0].LastColumn, list[0].FirstRow, XlCellOffsetType.Position, XlCellOffsetType.Offset);
                        this.ExportExpressionCore(duplicate.Formatting, rule, factory.CreateUniqueDuplicatesExpression(duplicate.Unique, this.CalcColumnRange(rule), cellOffset));
                    }
                }
                else
                {
                    if (duplicate.Duplicate)
                    {
                        XlCondFmtRuleDuplicates item = new XlCondFmtRuleDuplicates {
                            Formatting = duplicate.Formatting,
                            StopIfTrue = rule.StopIfTrue
                        };
                        cf.Rules.Add(item);
                    }
                    else
                    {
                        XlCondFmtRuleUnique item = new XlCondFmtRuleUnique {
                            Formatting = duplicate.Formatting,
                            StopIfTrue = rule.StopIfTrue
                        };
                        cf.Rules.Add(item);
                    }
                    base.ExportInfo.Sheet.ConditionalFormattings.Add(cf);
                }
            }
        }

        private void GetCustomIcons(XlCondFmtRuleIconSet cfmtris, IList<XlCondFmtCustomIcon> icons)
        {
            if ((icons != null) && (icons.Count > 0))
            {
                for (int i = 0; i < icons.Count; i++)
                {
                    XlCondFmtCustomIcon item = icons[i];
                    cfmtris.CustomIcons.Add(item);
                }
            }
        }

        private static XlCondFmtAxisPosition GetDataBarAxisPosition(IFormatConditionRuleDataBar condFmtRDataBar)
        {
            XlCondFmtAxisPosition automatic = XlCondFmtAxisPosition.Automatic;
            if (!condFmtRDataBar.DrawAxis)
            {
                automatic = XlCondFmtAxisPosition.None;
            }
            else if (condFmtRDataBar.DrawAxisAtMiddle)
            {
                automatic = XlCondFmtAxisPosition.Midpoint;
            }
            return automatic;
        }

        private static XlDataBarDirection GetDataBarDirection(IFormatConditionRuleDataBar condFmtRDataBar)
        {
            XlDataBarDirection context = XlDataBarDirection.Context;
            switch (condFmtRDataBar.Direction)
            {
                case 0:
                    context = XlDataBarDirection.RightToLeft;
                    break;

                case 1:
                    context = XlDataBarDirection.LeftToRight;
                    break;

                case 2:
                    context = XlDataBarDirection.Context;
                    break;

                default:
                    break;
            }
            return context;
        }

        private static XlCondFmtIconSetType GetIconSetType(IFormatConditionRuleIconSet exrule) => 
            (exrule.IconSetType != ~XlCondFmtIconSetType.Arrows3) ? exrule.IconSetType : ((exrule.Icons.Count != 3) ? ((exrule.Icons.Count != 4) ? ((exrule.Icons.Count != 5) ? XlCondFmtIconSetType.Arrows3 : XlCondFmtIconSetType.Arrows5) : XlCondFmtIconSetType.Arrows4) : XlCondFmtIconSetType.Arrows3);

        protected IColumn GetPositionColumn(IFormatRuleBase rule) => 
            rule.ColumnApplyTo ?? rule.Column;

        protected IColumn GetSourceColumn(IFormatRuleBase rule) => 
            rule.Column ?? rule.ColumnApplyTo;

        private void ProcessExpression(string ruleExpression, XlDifferentialFormatting formatting, IFormatRuleBase rule)
        {
            CriteriaOperator @operator = CriteriaOperator.TryParse(ruleExpression, new object[0]);
            if (!(@operator is InOperator))
            {
                this.ExportExpression(ruleExpression, formatting, rule);
            }
            else
            {
                InOperator operator2 = (InOperator) @operator;
                for (int i = 0; i < operator2.Operands.Count; i++)
                {
                    CriteriaOperator operator3 = operator2.Operands[i];
                    string format = "{0}={1}";
                    this.ExportExpression(string.Format(format, operator2.LeftOperand, operator3), formatting, rule);
                }
            }
        }

        private bool RuleSourceAppliedToLookupColumn(IFormatRuleBase rule)
        {
            IColumn sourceColumn = this.GetSourceColumn(rule);
            return ((sourceColumn != null) && base.ExportInfo.ColumnsInfoColl.ColumnHasLookupItems(sourceColumn.FieldName));
        }

        private static void SetMidValue(XlCondFmtRuleColorScale colorScale, IFormatConditionRule3ColorScale gridRule)
        {
            colorScale.MidpointValue.ObjectType = XlCondFmtValueObjectType.Percent;
            if ((gridRule.MidpointValue != null) && !ConditionalFormattingExporter<TCol, TRow>.CanDefaultMidValue(gridRule))
            {
                colorScale.MidpointValue.Value = XlValueObject.FromObject(gridRule.MidpointValue);
                colorScale.MidpointValue.ObjectType = gridRule.MidpointType;
            }
        }

        private static void SetMinMaxValue(IFormatConditionRuleMinMaxBase rule, XlCondFmtValueObject min, XlCondFmtValueObject max)
        {
            if ((rule.MinValue != null) || (rule.MaxValue != null))
            {
                if (!ConditionalFormattingExporter<TCol, TRow>.CanDefaultMinValue(rule))
                {
                    min.Value = XlValueObject.FromObject(rule.MinValue);
                    min.ObjectType = rule.MinType;
                }
                if (!ConditionalFormattingExporter<TCol, TRow>.CanDefaultMaxValue(rule))
                {
                    max.Value = XlValueObject.FromObject(rule.MaxValue);
                    max.ObjectType = rule.MaxType;
                }
            }
        }

        private XlCondFmtOperator TransformFormatConditionToXCondFmtOperator(FormatConditions fc)
        {
            switch (fc)
            {
                case FormatConditions.Equal:
                    return XlCondFmtOperator.Equal;

                case FormatConditions.NotEqual:
                    return XlCondFmtOperator.NotEqual;

                case FormatConditions.Between:
                    return XlCondFmtOperator.Between;

                case FormatConditions.Less:
                    return XlCondFmtOperator.LessThan;

                case FormatConditions.Greater:
                    return XlCondFmtOperator.GreaterThan;

                case FormatConditions.GreaterOrEqual:
                    return XlCondFmtOperator.GreaterThanOrEqual;

                case FormatConditions.LessOrEqual:
                    return XlCondFmtOperator.LessThanOrEqual;
            }
            return XlCondFmtOperator.NotBetween;
        }
    }
}

