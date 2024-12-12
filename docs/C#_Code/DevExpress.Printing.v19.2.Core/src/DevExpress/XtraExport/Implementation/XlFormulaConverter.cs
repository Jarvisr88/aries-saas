namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using DevExpress.XtraExport;
    using System;
    using System.Globalization;

    internal class XlFormulaConverter
    {
        private readonly XlExportNumberFormatConverter numberFormatConverter = new XlExportNumberFormatConverter();
        private XlPtgDataType paramType = XlPtgDataType.Value;
        private IXlDocumentOptions options;

        public XlFormulaConverter(IXlDocumentOptions options)
        {
            Guard.ArgumentNotNull(options, "options");
            this.options = options;
        }

        public XlExpression Convert(IXlFormulaParameter formula)
        {
            Guard.ArgumentNotNull(formula, "formula");
            XlExpression expression = new XlExpression();
            this.PrepareExpression(expression, formula);
            return expression;
        }

        private XlPtgBase CreatePtg(XlCellRange range) => 
            this.CreatePtg(range, XlPtgDataType.Reference);

        private XlPtgBase CreatePtg(XlVariantValue value) => 
            !value.IsBoolean ? (!value.IsNumeric ? (!value.IsText ? (!value.IsError ? ((XlPtgBase) new XlPtgMissArg()) : ((value.ErrorValue.Type != XlCellErrorType.Reference) ? ((XlPtgBase) new XlPtgErr(value.ErrorValue.Type)) : ((XlPtgBase) new XlPtgRefErr(this.paramType)))) : ((XlPtgBase) new XlPtgStr(value.TextValue))) : ((XlPtgBase) new XlPtgNum(value.NumericValue))) : ((XlPtgBase) new XlPtgBool(value.BooleanValue));

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

        private void PrepareExpression(XlExpression expression, IXlFormulaParameter parameter)
        {
            if (((((!this.PrepareExpression(expression, parameter as XlFormulaParameter) && !this.PrepareExpression(expression, parameter as XlSubtotalFunction)) && !this.PrepareExpression(expression, parameter as XlVLookupFunction)) && !this.PrepareExpression(expression, parameter as XlTextFunction)) && !this.PrepareExpression(expression, parameter as XlFunctionBase)) && !this.PrepareExpression(expression, parameter as XlBinaryOperator))
            {
                XlCellRange range = parameter as XlCellRange;
                if (range != null)
                {
                    expression.Add(this.CreatePtg(range, this.paramType));
                }
                else
                {
                    XlTableReference tableReference = parameter as XlTableReference;
                    if (tableReference != null)
                    {
                        expression.Add(new XlPtgTableRef(tableReference, this.paramType));
                    }
                    else
                    {
                        expression.Add(new XlPtgErr(XlCellErrorType.Value));
                    }
                }
            }
        }

        private bool PrepareExpression(XlExpression expression, XlBinaryOperator oper)
        {
            if (oper == null)
            {
                return false;
            }
            this.PrepareExpression(expression, oper.Left);
            this.PrepareExpression(expression, oper.Right);
            expression.Add(new XlPtgBinaryOperator(oper.TypeCode));
            return true;
        }

        private bool PrepareExpression(XlExpression expression, XlFormulaParameter parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            expression.Add(this.CreatePtg(parameter.Value));
            return true;
        }

        private bool PrepareExpression(XlExpression expression, XlFunctionBase function)
        {
            if (function == null)
            {
                return false;
            }
            XlPtgDataType paramType = this.paramType;
            this.paramType = function.ParamType;
            try
            {
                if (!function.IsValidParametersCount)
                {
                    expression.Add(new XlPtgErr(XlCellErrorType.Value));
                }
                else
                {
                    foreach (IXlFormulaParameter parameter in function.Parameters)
                    {
                        this.PrepareExpression(expression, parameter);
                    }
                    expression.Add(new XlPtgFuncVar(function.FunctionCode, function.Parameters.Count, XlPtgDataType.Value));
                }
            }
            finally
            {
                this.paramType = paramType;
            }
            return true;
        }

        private bool PrepareExpression(XlExpression expression, XlSubtotalFunction function)
        {
            if (function == null)
            {
                return false;
            }
            int summary = (int) function.Summary;
            if (function.IgnoreHidden)
            {
                summary += 100;
            }
            int paramCount = 0;
            int num3 = 0;
            foreach (XlCellRange range in function.Ranges)
            {
                if (num3 == 0)
                {
                    expression.Add(new XlPtgInt(summary));
                }
                expression.Add(this.CreatePtg(range));
                num3++;
                if (num3 >= 0x1d)
                {
                    expression.Add(new XlPtgFuncVar(0x158, num3 + 1, XlPtgDataType.Value));
                    paramCount++;
                    num3 = 0;
                }
            }
            if (num3 > 0)
            {
                expression.Add(new XlPtgFuncVar(0x158, num3 + 1, XlPtgDataType.Value));
                paramCount++;
            }
            if (paramCount > 1)
            {
                if (function.Summary == XlSummary.Average)
                {
                    expression.Add(new XlPtgFuncVar(5, paramCount, XlPtgDataType.Value));
                }
                else if (function.Summary == XlSummary.Min)
                {
                    expression.Add(new XlPtgFuncVar(6, paramCount, XlPtgDataType.Value));
                }
                else if (function.Summary == XlSummary.Max)
                {
                    expression.Add(new XlPtgFuncVar(7, paramCount, XlPtgDataType.Value));
                }
                else
                {
                    expression.Add(new XlPtgFuncVar(4, paramCount, XlPtgDataType.Value));
                }
            }
            return true;
        }

        private bool PrepareExpression(XlExpression formula, XlTextFunction function)
        {
            if (function == null)
            {
                return false;
            }
            XlPtgDataType paramType = this.paramType;
            this.paramType = XlPtgDataType.Value;
            try
            {
                if (function.Value != null)
                {
                    this.PrepareExpression(formula, function.Value);
                }
                else
                {
                    formula.Add(new XlPtgErr(XlCellErrorType.Value));
                }
                if (function.NumberFormat != null)
                {
                    formula.Add(new XlPtgStr(function.NumberFormat.GetLocalizedFormatCode(this.CurrentCulture)));
                }
                else
                {
                    ExcelNumberFormat format = this.numberFormatConverter.Convert(function.NetFormatString, function.IsDateTimeFormatString, this.CurrentCulture);
                    string str = function.IsDateTimeFormatString ? this.numberFormatConverter.GetLocalDateFormatString((format != null) ? format.FormatString : string.Empty, this.CurrentCulture) : this.numberFormatConverter.GetLocalFormatString((format != null) ? format.FormatString : string.Empty, this.CurrentCulture);
                    formula.Add(new XlPtgStr(str));
                }
                formula.Add(new XlPtgFunc(0x30, XlPtgDataType.Value));
            }
            finally
            {
                this.paramType = paramType;
            }
            return true;
        }

        private bool PrepareExpression(XlExpression expression, XlVLookupFunction function)
        {
            if (function == null)
            {
                return false;
            }
            XlPtgDataType paramType = this.paramType;
            this.paramType = XlPtgDataType.Value;
            try
            {
                if (function.LookupValue != null)
                {
                    this.PrepareExpression(expression, function.LookupValue);
                }
                else
                {
                    expression.Add(new XlPtgErr(XlCellErrorType.Value));
                }
                expression.Add(this.CreatePtg(function.Table));
                expression.Add(new XlPtgInt(function.ColumnIndex));
                expression.Add(new XlPtgBool(function.RangeLookup));
                expression.Add(new XlPtgFuncVar(0x66, 4, XlPtgDataType.Value));
            }
            finally
            {
                this.paramType = paramType;
            }
            return true;
        }

        private CultureInfo CurrentCulture =>
            (this.options.Culture == null) ? CultureInfo.InvariantCulture : this.options.Culture;
    }
}

