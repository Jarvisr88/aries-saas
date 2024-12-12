namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class XlExpressionStringBuilder : IXlPtgVisitor
    {
        private static readonly Dictionary<XlCellErrorType, string> errCodeToTextConversionTable = CreateErrorCodeToFunctionInfoConversionTable();
        private const string trueConstant = "TRUE";
        private const string falseConstant = "FALSE";
        private const string FUTURE_FUNCTION_PREFIX = "_xlfn.";
        private readonly Stack<int> stack = new Stack<int>();
        private readonly StringBuilder builder = new StringBuilder();
        private IXlExpressionContext context = null;

        private void BuildFunctionString(int parametersCount, string functionName)
        {
            if (parametersCount <= 0)
            {
                this.stack.Push(this.builder.Length);
                this.builder.Append(functionName);
                this.builder.Append("()");
            }
            else
            {
                string str = ",";
                for (int i = 0; i < (parametersCount - 1); i++)
                {
                    this.builder.Insert(this.stack.Pop(), str);
                }
                int index = this.stack.Peek();
                this.builder.Insert(index, functionName + "(");
                this.builder.Append(")");
            }
        }

        public string BuildString(XlExpression expression)
        {
            foreach (XlPtgBase base2 in expression)
            {
                base2.Visit(this);
            }
            return this.builder.ToString();
        }

        public void ConvertArrayToString(XlPtgArray ptg, StringBuilder builder)
        {
            char ch = '.';
            char ch2 = (ch == ',') ? '\\' : ',';
            char ch3 = (ch == ';') ? '\\' : ';';
            builder.Append('{');
            int num = 0;
            int width = ptg.Width;
            int height = ptg.Height;
            int num4 = 0;
            while (num4 < height)
            {
                int num5 = 0;
                while (true)
                {
                    if (num5 >= width)
                    {
                        if (num4 != (height - 1))
                        {
                            builder.Append(ch3);
                        }
                        num4++;
                        break;
                    }
                    XlVariantValue value2 = ptg.Values[num];
                    XlVariantValue value3 = value2.ToText();
                    string textValue = value3.TextValue;
                    if (value2.IsText)
                    {
                        textValue = "\"" + textValue.Replace("\"", "\"\"") + "\"";
                    }
                    builder.Append(textValue);
                    if (num5 != (width - 1))
                    {
                        builder.Append(ch2);
                    }
                    num++;
                    num5++;
                }
            }
            builder.Append('}');
        }

        private string ConvertNumberToText(double value, CultureInfo culture)
        {
            string s = value.ToString(culture);
            if ((value > 1E+15) && (value < 1E+16))
            {
                try
                {
                    double num = double.Parse(s, culture);
                    long num2 = (long) num;
                    if (num == num2)
                    {
                        string str2 = num2.ToString(culture);
                        if (str2.Length < s.Length)
                        {
                            return str2;
                        }
                    }
                }
                catch
                {
                }
            }
            return s;
        }

        private static Dictionary<XlCellErrorType, string> CreateErrorCodeToFunctionInfoConversionTable() => 
            new Dictionary<XlCellErrorType, string> { 
                { 
                    XlCellErrorType.DivisionByZero,
                    "#DIV/0!"
                },
                { 
                    XlCellErrorType.NotAvailable,
                    "#N/A"
                },
                { 
                    XlCellErrorType.Name,
                    "#NAME?"
                },
                { 
                    XlCellErrorType.Null,
                    "#NULL!"
                },
                { 
                    XlCellErrorType.Number,
                    "#NUM!"
                },
                { 
                    XlCellErrorType.Reference,
                    "#REF!"
                },
                { 
                    XlCellErrorType.Value,
                    "#VALUE!"
                }
            };

        private string PrepareFunctionName(XlFunctionInfo function) => 
            (function.Properties != XlFunctionProperty.Excel2010Future) ? function.Name : ("_xlfn." + function.Name);

        public void Visit(XlPtgArea ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(ptg.CellRange.ToString());
        }

        public void Visit(XlPtgArea3d ptg)
        {
            this.stack.Push(this.builder.Length);
            XlCellRange cellRange = ptg.CellRange;
            cellRange.SheetName = ptg.SheetName;
            this.builder.Append(cellRange.ToString());
        }

        public void Visit(XlPtgAreaErr ptg)
        {
        }

        public void Visit(XlPtgAreaErr3d ptg)
        {
        }

        public void Visit(XlPtgAreaN ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(new XlCellRange(ptg.TopLeft.ToCellPosition(this.Context), ptg.BottomRight.ToCellPosition(this.Context)).ToString());
        }

        public void Visit(XlPtgAreaN3d ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(new XlCellRange(ptg.TopLeft.ToCellPosition(this.Context), ptg.BottomRight.ToCellPosition(this.Context)) { SheetName = ptg.SheetName }.ToString());
        }

        public void Visit(XlPtgArray ptg)
        {
            this.stack.Push(this.builder.Length);
            this.ConvertArrayToString(ptg, this.builder);
        }

        public void Visit(XlPtgAttrChoose ptg)
        {
        }

        public void Visit(XlPtgAttrGoto ptg)
        {
        }

        public void Visit(XlPtgAttrIf ptg)
        {
        }

        public void Visit(XlPtgAttrIfError ptg)
        {
        }

        public void Visit(XlPtgAttrSemi ptg)
        {
        }

        public void Visit(XlPtgAttrSpace ptg)
        {
        }

        public void Visit(XlPtgBinaryOperator ptg)
        {
            this.builder.Insert(this.stack.Pop(), ptg.OperatorText);
        }

        public void Visit(XlPtgBool ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(ptg.Value ? "TRUE" : "FALSE");
        }

        public void Visit(XlPtgErr ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(errCodeToTextConversionTable[ptg.Value]);
        }

        public void Visit(XlPtgExp ptg)
        {
        }

        public void Visit(XlPtgFunc ptg)
        {
            XlFunctionInfo functionInfo = XlFunctionRepository.GetFunctionInfo(ptg.FuncCode);
            string functionName = this.PrepareFunctionName(functionInfo);
            this.BuildFunctionString(functionInfo.MinArgumentsCount, functionName);
        }

        public void Visit(XlPtgFuncVar ptg)
        {
            XlFunctionInfo functionInfo = XlFunctionRepository.GetFunctionInfo(ptg.FuncCode);
            string functionName = this.PrepareFunctionName(functionInfo);
            this.BuildFunctionString(ptg.ParamCount, functionName);
        }

        public void Visit(XlPtgInt ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(ptg.Value.ToString(CultureInfo.InvariantCulture));
        }

        public void Visit(XlPtgMemArea ptg)
        {
        }

        public void Visit(XlPtgMemErr ptg)
        {
        }

        public void Visit(XlPtgMemFunc ptg)
        {
        }

        public void Visit(XlPtgMissArg ptg)
        {
            this.stack.Push(this.builder.Length);
        }

        public void Visit(XlPtgName ptg)
        {
        }

        public void Visit(XlPtgNameX ptg)
        {
        }

        public void Visit(XlPtgNum ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(this.ConvertNumberToText(ptg.Value, CultureInfo.InvariantCulture));
        }

        public void Visit(XlPtgParen ptg)
        {
            int index = this.stack.Peek();
            this.builder.Insert(index, "(");
            this.builder.Append(")");
        }

        public void Visit(XlPtgRef ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(ptg.CellPosition.ToString());
        }

        public void Visit(XlPtgRef3d ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(new XlCellRange(ptg.CellPosition) { SheetName = ptg.SheetName }.ToString());
        }

        public void Visit(XlPtgRefErr ptg)
        {
        }

        public void Visit(XlPtgRefErr3d ptg)
        {
        }

        public void Visit(XlPtgRefN ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(ptg.CellOffset.ToCellPosition(this.Context).ToString());
        }

        public void Visit(XlPtgRefN3d ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(new XlCellRange(ptg.CellOffset.ToCellPosition(this.Context)) { SheetName = ptg.SheetName }.ToString());
        }

        public void Visit(XlPtgStr ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append("\"");
            this.builder.Append(ptg.Value.Replace("\"", "\"\""));
            this.builder.Append("\"");
        }

        public void Visit(XlPtgTableRef ptg)
        {
            this.stack.Push(this.builder.Length);
            this.builder.Append(ptg.TableReference.ToString());
        }

        public void Visit(XlPtgUnaryOperator ptg)
        {
            if (ptg.TypeCode != 20)
            {
                this.builder.Insert(this.stack.Peek(), ptg.OperatorText);
            }
            else
            {
                this.builder.Append(ptg.OperatorText);
            }
        }

        public IXlExpressionContext Context
        {
            get => 
                this.context;
            set => 
                this.context = value;
        }
    }
}

