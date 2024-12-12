namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class XlCondFmtExpressionFactory
    {
        private const int funcCount = 0;
        private const int funcIf = 1;
        private const int funcIsError = 3;
        private const int funcAverage = 5;
        private const int funcMin = 6;
        private const int funcMax = 7;
        private const int funcInt = 0x19;
        private const int funcLen = 0x20;
        private const int funcAnd = 0x24;
        private const int funcNot = 0x26;
        private const int funcSearch = 0x52;
        private const int funcLeft = 0x73;
        private const int funcRight = 0x74;
        private const int funcTrim = 0x76;
        private const int funcIsBlank = 0x81;
        private const int funcLarge = 0x145;
        private const int funcSmall = 0x146;
        private const int funcCountIf = 0x15a;
        private const int funcToday = 0xdd;
        private const int funcMonth = 0x44;
        private const int funcYear = 0x45;
        private const int funcWeekday = 70;
        private const int funcFloor = 0x11d;
        private const int funcRoundDown = 0xd5;
        private const int funcStDevP = 0xc1;
        private readonly XlPtgInt ptgZero = new XlPtgInt(0);
        private readonly XlPtgInt ptgOne = new XlPtgInt(1);
        private readonly XlPtgAttrSemi ptgAttrSemi = new XlPtgAttrSemi();
        private readonly XlPtgAttrSpace ptgAttrSpace = new XlPtgAttrSpace(XlPtgAttrSpaceType.SpaceBeforeBaseExpression, 1);
        private readonly XlPtgBinaryOperator ptgAdd = new XlPtgBinaryOperator(3);
        private readonly XlPtgBinaryOperator ptgSub = new XlPtgBinaryOperator(4);
        private readonly XlPtgBinaryOperator ptgMul = new XlPtgBinaryOperator(5);
        private readonly XlPtgBinaryOperator ptgEq = new XlPtgBinaryOperator(11);
        private readonly XlPtgBinaryOperator ptgGt = new XlPtgBinaryOperator(13);
        private readonly XlPtgBinaryOperator ptgGe = new XlPtgBinaryOperator(12);
        private readonly XlPtgBinaryOperator ptgLt = new XlPtgBinaryOperator(9);
        private readonly XlPtgBinaryOperator ptgLe = new XlPtgBinaryOperator(10);
        private readonly XlPtgParen ptgParen = new XlPtgParen();
        private readonly XlPtgUnaryOperator ptgPercent = new XlPtgUnaryOperator(20);
        private readonly XlCellOffset defaultCellOffset = new XlCellOffset(0, 0, XlCellOffsetType.Offset, XlCellOffsetType.Offset);
        private CultureInfo culture;

        private void AddVLookupFunc(XlExpression expression, XlPtgArea lookupFuncArea)
        {
            expression.Add(lookupFuncArea);
            expression.Add(new XlPtgInt(2));
            expression.Add(new XlPtgInt(0));
            expression.Add(new XlPtgFuncVar(0x66, 4, XlPtgDataType.Value));
        }

        public XlExpression CreateAboveAverageExpression(XlCondFmtRuleAboveAverage rule, IList<XlCellRange> ranges) => 
            this.CreateAboveAverageExpression(rule, ranges, this.defaultCellOffset);

        public XlExpression CreateAboveAverageExpression(XlCondFmtRuleAboveAverage rule, IList<XlCellRange> ranges, XlCellOffset cellOffset) => 
            this.CreateAboveAverageExpression(rule, ranges, cellOffset, false, null);

        public XlExpression CreateAboveAverageExpression(XlCondFmtRuleAboveAverage rule, IList<XlCellRange> ranges, XlCellOffset cellOffset, bool addLookupFunc, XlPtgArea lookupFuncArea)
        {
            Guard.ArgumentNotNull(rule, "rule");
            Guard.ArgumentNotNull(ranges, "ranges");
            Guard.ArgumentPositive(ranges.Count, "ranges.Count");
            Guard.ArgumentNotNull(cellOffset, "cellOffset");
            XlExpression expression = new XlExpression();
            XlPtgRefN item = new XlPtgRefN(cellOffset);
            item.DataType = XlPtgDataType.Value;
            expression.Add(item);
            if (addLookupFunc)
            {
                this.AddVLookupFunc(expression, lookupFuncArea);
            }
            if (rule.StdDev == 0)
            {
                int count = ranges.Count;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= count)
                    {
                        int num2;
                        expression.Add(new XlPtgFuncVar(5, count, XlPtgDataType.Value));
                        num2 = !rule.AboveAverage ? (rule.EqualAverage ? 10 : (num2 = 9)) : (rule.EqualAverage ? 12 : 13);
                        expression.Add(new XlPtgBinaryOperator(num2));
                        break;
                    }
                    this.CreateAverageExpression(expression, addLookupFunc ? ranges[num3].AsAbsolute() : ranges[num3]);
                    num3++;
                }
            }
            else
            {
                if (ranges.Count > 1)
                {
                    return expression;
                }
                expression.Add(this.CreatePtg(ranges[0]));
                expression.Add(new XlPtgFuncVar(5, 1, XlPtgDataType.Value));
                expression.Add(this.ptgSub);
                expression.Add(this.ptgParen);
                expression.Add(this.CreatePtg(ranges[0]));
                expression.Add(new XlPtgFuncVar(0xc1, 1, XlPtgDataType.Value));
                if (!rule.AboveAverage)
                {
                    expression.Add(new XlPtgNum((double) -rule.StdDev));
                }
                else
                {
                    expression.Add(new XlPtgInt(rule.StdDev));
                }
                expression.Add(this.ptgParen);
                expression.Add(this.ptgMul);
                expression.Add(new XlPtgBinaryOperator(rule.AboveAverage ? 12 : 10));
            }
            return expression;
        }

        private void CreateAverageExpression(XlExpression result, XlCellRange cellRange)
        {
            XlPtgArea3d aread2;
            if (string.IsNullOrEmpty(cellRange.SheetName))
            {
                XlPtgArea area1 = new XlPtgArea(cellRange);
                area1.DataType = XlPtgDataType.Array;
                aread2 = (XlPtgArea3d) area1;
            }
            else
            {
                XlPtgArea3d aread1 = new XlPtgArea3d(cellRange, cellRange.SheetName);
                aread1.DataType = XlPtgDataType.Array;
                aread2 = aread1;
            }
            XlPtgArea item = aread2;
            XlPtgStr str = new XlPtgStr(string.Empty);
            result.Add(item);
            result.Add(new XlPtgFunc(3, XlPtgDataType.Array));
            result.Add(this.ptgAttrSpace);
            result.Add(str);
            result.Add(item);
            result.Add(new XlPtgFunc(0x81, XlPtgDataType.Array));
            result.Add(this.ptgAttrSpace);
            result.Add(str);
            result.Add(this.ptgAttrSpace);
            result.Add(item);
            result.Add(this.ptgAttrSpace);
            result.Add(new XlPtgFuncVar(1, 3, XlPtgDataType.Reference));
            result.Add(this.ptgAttrSpace);
            result.Add(new XlPtgFuncVar(1, 3, XlPtgDataType.Reference));
        }

        public XlExpression CreateBlanksExpression() => 
            this.CreateBlanksExpression(this.defaultCellOffset);

        public XlExpression CreateBlanksExpression(XlCellOffset cellOffset)
        {
            Guard.ArgumentNotNull(cellOffset, "cellOffset");
            XlExpression expression = new XlExpression();
            XlPtgRefN item = new XlPtgRefN(cellOffset);
            item.DataType = XlPtgDataType.Value;
            expression.Add(item);
            expression.Add(new XlPtgFunc(0x76, XlPtgDataType.Value));
            expression.Add(new XlPtgFunc(0x20, XlPtgDataType.Value));
            expression.Add(this.ptgZero);
            expression.Add(this.ptgEq);
            return expression;
        }

        private void CreateDayExpression(XlExpression result, XlCondFmtTimePeriod period, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(ptgRefN);
            result.Add(this.ptgOne);
            result.Add(new XlPtgFunc(0x11d, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            if (period != XlCondFmtTimePeriod.Today)
            {
                result.Add(this.ptgOne);
                if (period == XlCondFmtTimePeriod.Tomorrow)
                {
                    result.Add(this.ptgAdd);
                }
                else
                {
                    result.Add(this.ptgSub);
                }
            }
            result.Add(this.ptgEq);
        }

        private void CreateLast7DaysExpression(XlExpression result, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(ptgRefN);
            result.Add(this.ptgOne);
            result.Add(new XlPtgFunc(0x11d, XlPtgDataType.Value));
            result.Add(this.ptgSub);
            result.Add(new XlPtgInt(6));
            result.Add(this.ptgLe);
            result.Add(ptgRefN);
            result.Add(this.ptgOne);
            result.Add(new XlPtgFunc(0x11d, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(this.ptgLe);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
        }

        private void CreateLastMonthExpression(XlExpression result, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(this.ptgOne);
            result.Add(this.ptgEq);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(new XlPtgInt(12));
            result.Add(this.ptgEq);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(this.ptgOne);
            result.Add(this.ptgSub);
            result.Add(this.ptgParen);
            result.Add(this.ptgEq);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(this.ptgOne);
            result.Add(this.ptgSub);
            result.Add(this.ptgParen);
            result.Add(this.ptgEq);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(this.ptgEq);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
            result.Add(new XlPtgFuncVar(1, 3, XlPtgDataType.Value));
        }

        private void CreateLastWeekExpression(XlExpression result, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(ptgRefN);
            result.Add(this.ptgZero);
            result.Add(new XlPtgFunc(0xd5, XlPtgDataType.Value));
            result.Add(this.ptgSub);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            this.CreateWeekdayFunction(result);
            result.Add(this.ptgParen);
            result.Add(this.ptgGe);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(ptgRefN);
            result.Add(this.ptgZero);
            result.Add(new XlPtgFunc(0xd5, XlPtgDataType.Value));
            result.Add(this.ptgSub);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            this.CreateWeekdayFunction(result);
            result.Add(new XlPtgInt(7));
            result.Add(this.ptgAdd);
            result.Add(this.ptgParen);
            result.Add(this.ptgLt);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
        }

        private void CreateNextMonthExpression(XlExpression result, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(new XlPtgInt(12));
            result.Add(this.ptgEq);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(this.ptgOne);
            result.Add(this.ptgEq);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(this.ptgOne);
            result.Add(this.ptgAdd);
            result.Add(this.ptgParen);
            result.Add(this.ptgEq);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(this.ptgOne);
            result.Add(this.ptgAdd);
            result.Add(this.ptgParen);
            result.Add(this.ptgEq);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(this.ptgEq);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
            result.Add(new XlPtgFuncVar(1, 3, XlPtgDataType.Value));
        }

        private void CreateNextWeekExpression(XlExpression result, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(ptgRefN);
            result.Add(this.ptgZero);
            result.Add(new XlPtgFunc(0xd5, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(this.ptgSub);
            result.Add(new XlPtgInt(7));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            this.CreateWeekdayFunction(result);
            result.Add(this.ptgSub);
            result.Add(this.ptgParen);
            result.Add(this.ptgGt);
            result.Add(ptgRefN);
            result.Add(this.ptgZero);
            result.Add(new XlPtgFunc(0xd5, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(this.ptgSub);
            result.Add(new XlPtgInt(15));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            this.CreateWeekdayFunction(result);
            result.Add(this.ptgSub);
            result.Add(this.ptgParen);
            result.Add(this.ptgLt);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
        }

        public XlExpression CreateNoBlanksExpression() => 
            this.CreateNoBlanksExpression(this.defaultCellOffset);

        public XlExpression CreateNoBlanksExpression(XlCellOffset cellOffset)
        {
            Guard.ArgumentNotNull(cellOffset, "cellOffset");
            XlExpression expression = new XlExpression();
            XlPtgRefN item = new XlPtgRefN(cellOffset);
            item.DataType = XlPtgDataType.Value;
            expression.Add(item);
            expression.Add(new XlPtgFunc(0x76, XlPtgDataType.Value));
            expression.Add(new XlPtgFunc(0x20, XlPtgDataType.Value));
            expression.Add(this.ptgZero);
            expression.Add(this.ptgGt);
            return expression;
        }

        private XlPtgBase CreatePtg(XlCellRange range) => 
            this.CreatePtg(range, XlPtgDataType.Reference);

        private XlPtgBase CreatePtg(XlCellRange range, XlPtgDataType dataType)
        {
            if (string.IsNullOrEmpty(range.SheetName))
            {
                if (range.TopLeft.Equals(range.BottomRight))
                {
                    XlPtgRef ref1 = new XlPtgRef(range.TopLeft);
                    ref1.DataType = dataType;
                    return ref1;
                }
                XlPtgArea area1 = new XlPtgArea(range);
                area1.DataType = dataType;
                return area1;
            }
            if (range.TopLeft.Equals(range.BottomRight))
            {
                XlPtgRef3d refd1 = new XlPtgRef3d(range.TopLeft, range.SheetName);
                refd1.DataType = dataType;
                return refd1;
            }
            XlPtgArea3d aread1 = new XlPtgArea3d(range, range.SheetName);
            aread1.DataType = dataType;
            return aread1;
        }

        public XlExpression CreateSpecificTextExpression(XlCondFmtRuleSpecificText rule) => 
            this.CreateSpecificTextExpression(rule, this.defaultCellOffset);

        public XlExpression CreateSpecificTextExpression(XlCondFmtRuleSpecificText rule, XlCellOffset cellOffset)
        {
            Guard.ArgumentNotNull(rule, "rule");
            Guard.ArgumentNotNull(cellOffset, "cellOffset");
            XlExpression expression = new XlExpression();
            XlPtgRefN fn1 = new XlPtgRefN(cellOffset);
            fn1.DataType = XlPtgDataType.Value;
            XlPtgRefN item = fn1;
            XlPtgStr str = new XlPtgStr(rule.Text);
            XlCondFmtType ruleType = rule.RuleType;
            if (ruleType > XlCondFmtType.ContainsText)
            {
                if (ruleType == XlCondFmtType.EndsWith)
                {
                    expression.Add(item);
                    expression.Add(str);
                    expression.Add(new XlPtgFunc(0x20, XlPtgDataType.Value));
                    expression.Add(new XlPtgFuncVar(0x74, 2, XlPtgDataType.Value));
                    expression.Add(str);
                    expression.Add(this.ptgEq);
                }
                else if (ruleType == XlCondFmtType.NotContainsText)
                {
                    expression.Add(str);
                    expression.Add(item);
                    expression.Add(new XlPtgFuncVar(0x52, 2, XlPtgDataType.Value));
                    expression.Add(new XlPtgFunc(3, XlPtgDataType.Value));
                }
            }
            else if (ruleType == XlCondFmtType.BeginsWith)
            {
                expression.Add(item);
                expression.Add(str);
                expression.Add(new XlPtgFunc(0x20, XlPtgDataType.Value));
                expression.Add(new XlPtgFuncVar(0x73, 2, XlPtgDataType.Value));
                expression.Add(str);
                expression.Add(this.ptgEq);
            }
            else if (ruleType == XlCondFmtType.ContainsText)
            {
                expression.Add(str);
                expression.Add(item);
                expression.Add(new XlPtgFuncVar(0x52, 2, XlPtgDataType.Value));
                expression.Add(new XlPtgFunc(3, XlPtgDataType.Value));
                expression.Add(new XlPtgFunc(0x26, XlPtgDataType.Value));
            }
            return expression;
        }

        private void CreateThisMonthExpression(XlExpression result, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x44, XlPtgDataType.Value));
            result.Add(this.ptgEq);
            result.Add(ptgRefN);
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0x45, XlPtgDataType.Value));
            result.Add(this.ptgEq);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
        }

        private void CreateThisWeekExpression(XlExpression result, XlPtgRefN ptgRefN)
        {
            result.Add(this.ptgAttrSemi);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(ptgRefN);
            result.Add(this.ptgZero);
            result.Add(new XlPtgFunc(0xd5, XlPtgDataType.Value));
            result.Add(this.ptgSub);
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            this.CreateWeekdayFunction(result);
            result.Add(this.ptgOne);
            result.Add(this.ptgSub);
            result.Add(this.ptgLe);
            result.Add(ptgRefN);
            result.Add(this.ptgZero);
            result.Add(new XlPtgFunc(0xd5, XlPtgDataType.Value));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            result.Add(this.ptgSub);
            result.Add(new XlPtgInt(7));
            result.Add(new XlPtgFunc(0xdd, XlPtgDataType.Value));
            this.CreateWeekdayFunction(result);
            result.Add(this.ptgSub);
            result.Add(this.ptgLe);
            result.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
        }

        public XlExpression CreateTimePeriodExpression(XlCondFmtRuleTimePeriod rule) => 
            this.CreateTimePeriodExpression(rule, this.defaultCellOffset);

        public XlExpression CreateTimePeriodExpression(XlCondFmtRuleTimePeriod rule, XlCellOffset cellOffset)
        {
            Guard.ArgumentNotNull(rule, "rule");
            Guard.ArgumentNotNull(cellOffset, "cellOffset");
            XlExpression result = new XlExpression();
            XlPtgRefN fn1 = new XlPtgRefN(cellOffset);
            fn1.DataType = XlPtgDataType.Value;
            XlPtgRefN ptgRefN = fn1;
            switch (rule.TimePeriod)
            {
                case XlCondFmtTimePeriod.Last7Days:
                    this.CreateLast7DaysExpression(result, ptgRefN);
                    break;

                case XlCondFmtTimePeriod.LastMonth:
                    this.CreateLastMonthExpression(result, ptgRefN);
                    break;

                case XlCondFmtTimePeriod.LastWeek:
                    this.CreateLastWeekExpression(result, ptgRefN);
                    break;

                case XlCondFmtTimePeriod.NextMonth:
                    this.CreateNextMonthExpression(result, ptgRefN);
                    break;

                case XlCondFmtTimePeriod.NextWeek:
                    this.CreateNextWeekExpression(result, ptgRefN);
                    break;

                case XlCondFmtTimePeriod.ThisMonth:
                    this.CreateThisMonthExpression(result, ptgRefN);
                    break;

                case XlCondFmtTimePeriod.ThisWeek:
                    this.CreateThisWeekExpression(result, ptgRefN);
                    break;

                case XlCondFmtTimePeriod.Today:
                case XlCondFmtTimePeriod.Tomorrow:
                case XlCondFmtTimePeriod.Yesterday:
                    this.CreateDayExpression(result, rule.TimePeriod, ptgRefN);
                    break;

                default:
                    break;
            }
            return result;
        }

        public XlExpression CreateTop10Expression(XlCondFmtRuleTop10 rule, IList<XlCellRange> ranges) => 
            this.CreateTop10Expression(rule, ranges, this.defaultCellOffset);

        public XlExpression CreateTop10Expression(XlCondFmtRuleTop10 rule, IList<XlCellRange> ranges, XlCellOffset cellOffset) => 
            this.CreateTop10Expression(rule, ranges, cellOffset, false, null);

        public XlExpression CreateTop10Expression(XlCondFmtRuleTop10 rule, IList<XlCellRange> ranges, XlCellOffset cellOffset, bool addLookupFunc, XlPtgArea lookupFuncArea)
        {
            Guard.ArgumentNotNull(rule, "rule");
            Guard.ArgumentNotNull(ranges, "ranges");
            Guard.ArgumentPositive(ranges.Count, "ranges.Count");
            Guard.ArgumentNotNull(cellOffset, "cellOffset");
            XlExpression expression = new XlExpression();
            if (ranges.Count <= 1)
            {
                XlPtgRefN fn1 = new XlPtgRefN(cellOffset);
                fn1.DataType = XlPtgDataType.Value;
                XlPtgRefN item = fn1;
                XlPtgArea area = !string.IsNullOrEmpty(ranges[0].SheetName) ? new XlPtgArea3d(ranges[0].AsAbsolute(), ranges[0].SheetName) : new XlPtgArea(ranges[0].AsAbsolute());
                if (!rule.Percent && !rule.Bottom)
                {
                    expression.Add(area);
                    expression.Add(this.ptgParen);
                    expression.Add(this.ptgAttrSpace);
                    expression.Add(new XlPtgInt(rule.Rank));
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(0, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgFuncVar(6, 2, XlPtgDataType.Value));
                    expression.Add(new XlPtgFunc(0x145, XlPtgDataType.Value));
                    expression.Add(item);
                    if (addLookupFunc)
                    {
                        this.AddVLookupFunc(expression, lookupFuncArea);
                    }
                    expression.Add(this.ptgLe);
                }
                else if (!rule.Percent)
                {
                    expression.Add(area);
                    expression.Add(this.ptgParen);
                    expression.Add(this.ptgAttrSpace);
                    expression.Add(new XlPtgInt(rule.Rank));
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(0, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgFuncVar(6, 2, XlPtgDataType.Value));
                    expression.Add(new XlPtgFunc(0x146, XlPtgDataType.Value));
                    expression.Add(item);
                    if (addLookupFunc)
                    {
                        this.AddVLookupFunc(expression, lookupFuncArea);
                    }
                    expression.Add(this.ptgGe);
                }
                else if (!rule.Bottom)
                {
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(0, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgInt(rule.Rank));
                    expression.Add(this.ptgPercent);
                    expression.Add(this.ptgMul);
                    expression.Add(new XlPtgFunc(0x19, XlPtgDataType.Value));
                    expression.Add(this.ptgZero);
                    expression.Add(this.ptgGt);
                    expression.Add(area);
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(0, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgInt(rule.Rank));
                    expression.Add(this.ptgPercent);
                    expression.Add(this.ptgMul);
                    expression.Add(new XlPtgFunc(0x19, XlPtgDataType.Value));
                    expression.Add(new XlPtgFunc(0x145, XlPtgDataType.Value));
                    expression.Add(this.ptgAttrSpace);
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(7, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgFuncVar(1, 3, XlPtgDataType.Value));
                    expression.Add(item);
                    if (addLookupFunc)
                    {
                        this.AddVLookupFunc(expression, lookupFuncArea);
                    }
                    expression.Add(this.ptgLe);
                }
                else
                {
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(0, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgInt(rule.Rank));
                    expression.Add(this.ptgPercent);
                    expression.Add(this.ptgMul);
                    expression.Add(new XlPtgFunc(0x19, XlPtgDataType.Value));
                    expression.Add(this.ptgZero);
                    expression.Add(this.ptgGe);
                    expression.Add(area);
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(0, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgInt(rule.Rank));
                    expression.Add(this.ptgPercent);
                    expression.Add(this.ptgMul);
                    expression.Add(new XlPtgFunc(0x19, XlPtgDataType.Value));
                    expression.Add(new XlPtgFunc(0x146, XlPtgDataType.Value));
                    expression.Add(this.ptgAttrSpace);
                    expression.Add(area);
                    expression.Add(new XlPtgFuncVar(6, 1, XlPtgDataType.Value));
                    expression.Add(new XlPtgFuncVar(1, 3, XlPtgDataType.Value));
                    expression.Add(item);
                    if (addLookupFunc)
                    {
                        this.AddVLookupFunc(expression, lookupFuncArea);
                    }
                    expression.Add(this.ptgGe);
                }
            }
            return expression;
        }

        public XlExpression CreateUniqueDuplicatesExpression(bool isUnique, IList<XlCellRange> ranges) => 
            this.CreateUniqueDuplicatesExpression(isUnique, ranges, this.defaultCellOffset);

        public XlExpression CreateUniqueDuplicatesExpression(bool isUnique, IList<XlCellRange> ranges, XlCellOffset cellOffset)
        {
            Guard.ArgumentNotNull(ranges, "ranges");
            Guard.ArgumentPositive(ranges.Count, "ranges.Count");
            Guard.ArgumentNotNull(cellOffset, "cellOffset");
            XlExpression expression = new XlExpression();
            XlPtgRefN fn1 = new XlPtgRefN(cellOffset);
            fn1.DataType = XlPtgDataType.Value;
            XlPtgRefN item = fn1;
            int count = ranges.Count;
            for (int i = 0; i < count; i++)
            {
                expression.Add(new XlPtgArea(ranges[i].AsAbsolute()));
                expression.Add(this.ptgAttrSpace);
                expression.Add(item);
                expression.Add(new XlPtgFunc(0x15a, XlPtgDataType.Value));
            }
            for (int j = 0; j < (count - 1); j++)
            {
                expression.Add(this.ptgAdd);
            }
            expression.Add(this.ptgOne);
            if (isUnique)
            {
                expression.Add(this.ptgEq);
            }
            else
            {
                expression.Add(this.ptgGt);
            }
            expression.Add(item);
            expression.Add(new XlPtgFunc(0x81, XlPtgDataType.Value));
            expression.Add(new XlPtgFunc(0x26, XlPtgDataType.Value));
            expression.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
            return expression;
        }

        private void CreateWeekdayFunction(XlExpression result)
        {
            int weekdayReturnType = this.GetWeekdayReturnType();
            if (weekdayReturnType == 1)
            {
                result.Add(new XlPtgFuncVar(70, 1, XlPtgDataType.Reference));
            }
            else
            {
                result.Add(new XlPtgInt(weekdayReturnType));
                result.Add(new XlPtgFuncVar(70, 2, XlPtgDataType.Reference));
            }
        }

        private int GetWeekdayReturnType()
        {
            switch (this.Culture.DateTimeFormat.FirstDayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 2;

                case DayOfWeek.Tuesday:
                    return 12;

                case DayOfWeek.Wednesday:
                    return 13;

                case DayOfWeek.Thursday:
                    return 14;

                case DayOfWeek.Friday:
                    return 15;

                case DayOfWeek.Saturday:
                    return 0x10;
            }
            return 1;
        }

        public CultureInfo Culture
        {
            get => 
                (this.culture != null) ? this.culture : CultureInfo.InvariantCulture;
            set => 
                this.culture = value;
        }
    }
}

