namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class CriteriaOperatorToXlExpressionConverter : IClientCriteriaVisitor<CriteriaPriorityClass>, ICriteriaVisitor<CriteriaPriorityClass>
    {
        private const int functionNotCode = 0x26;
        private const int functionIsBlankCode = 0x81;
        private const int functionBitwiseOrCode = 0x402c;
        private const int functionBitwiseAndCode = 0x402a;
        private const int functionBitwiseXorCode = 0x402e;
        private const int functionModCode = 0x27;
        private const int functionAndCode = 0x24;
        private const int functionOrCode = 0x25;
        private static Dictionary<FunctionOperatorType, IFunctionConverter> functionConvertersList;
        private readonly IXlColumnPositionConverter columnNameConverter;
        private XlExpression expression;
        private XlExpressionContext context;
        private CultureInfo culture;

        public CriteriaOperatorToXlExpressionConverter(IXlColumnPositionConverter columnNameConverter)
        {
            this.columnNameConverter = columnNameConverter;
            this.context = new XlExpressionContext();
        }

        private XlPtgBase ConvertOperand(object value)
        {
            if ((value == null) || Convert.IsDBNull(value))
            {
                return null;
            }
            Type type = value.GetType();
            if (type == typeof(string))
            {
                return new XlPtgStr((string) value);
            }
            if (type == typeof(bool))
            {
                return new XlPtgBool((bool) value);
            }
            if ((type == typeof(double)) || ((type == typeof(int)) || ((type == typeof(long)) || ((type == typeof(decimal)) || ((type == typeof(float)) || ((type == typeof(short)) || ((type == typeof(byte)) || ((type == typeof(ushort)) || (type == typeof(uint))))))))))
            {
                return this.CreateParsedThingForDouble(Convert.ToDouble(value));
            }
            if (!(type == typeof(DateTime)))
            {
                return new XlPtgStr(value.ToString());
            }
            XlVariantValue value2 = (DateTime) value;
            return this.CreateParsedThingForDouble(value2.NumericValue);
        }

        private static Dictionary<FunctionOperatorType, IFunctionConverter> CreateFunctionConvertersList() => 
            new Dictionary<FunctionOperatorType, IFunctionConverter> { 
                { 
                    FunctionOperatorType.Abs,
                    new FunctionDefaultConverter(0x18, 1)
                },
                { 
                    FunctionOperatorType.Acos,
                    new FunctionDefaultConverter(0x63, 1)
                },
                { 
                    FunctionOperatorType.AddDays,
                    new FunctionAddDaysConverter()
                },
                { 
                    FunctionOperatorType.AddHours,
                    new FunctionAddTimeConverter(0x18)
                },
                { 
                    FunctionOperatorType.AddMinutes,
                    new FunctionAddTimeConverter(0x5a0)
                },
                { 
                    FunctionOperatorType.AddMonths,
                    new FunctionAddMonthsConverter(1)
                },
                { 
                    FunctionOperatorType.AddSeconds,
                    new FunctionAddTimeConverter(0x15180)
                },
                { 
                    FunctionOperatorType.AddYears,
                    new FunctionAddMonthsConverter(12)
                },
                { 
                    FunctionOperatorType.Asin,
                    new FunctionDefaultConverter(0x62, 1)
                },
                { 
                    FunctionOperatorType.Atn,
                    new FunctionDefaultConverter(0x12, 1)
                },
                { 
                    FunctionOperatorType.Atn2,
                    new FunctionDefaultConverter(0x61, 2)
                },
                { 
                    FunctionOperatorType.Ceiling,
                    new FunctionCeilingConverter()
                },
                { 
                    FunctionOperatorType.Char,
                    new FunctionDefaultConverter(0x6f, 1)
                },
                { 
                    FunctionOperatorType.Concat,
                    new FunctionVarDefaultConverter(0x150, 1, 0x100)
                },
                { 
                    FunctionOperatorType.Contains,
                    new FunctionContainsTextConverter()
                },
                { 
                    FunctionOperatorType.Cos,
                    new FunctionDefaultConverter(0x10, 1)
                },
                { 
                    FunctionOperatorType.Cosh,
                    new FunctionDefaultConverter(230, 1)
                },
                { 
                    FunctionOperatorType.DateDiffDay,
                    new FunctionDateDiffDayConverter()
                },
                { 
                    FunctionOperatorType.DateDiffHour,
                    new FunctionDateDiffTimeConverter(0x18)
                },
                { 
                    FunctionOperatorType.DateDiffMinute,
                    new FunctionDateDiffTimeConverter(0x5a0)
                },
                { 
                    FunctionOperatorType.DateDiffMonth,
                    new FunctionDateDiffMonthConverter()
                },
                { 
                    FunctionOperatorType.DateDiffSecond,
                    new FunctionDateDiffTimeConverter(0x15180)
                },
                { 
                    FunctionOperatorType.DateDiffYear,
                    new FunctionDateDiffYearConverter()
                },
                { 
                    FunctionOperatorType.EndsWith,
                    new FunctionEndsWithConverter()
                },
                { 
                    FunctionOperatorType.Exp,
                    new FunctionDefaultConverter(0x15, 1)
                },
                { 
                    FunctionOperatorType.Floor,
                    new FunctionFloorConverter()
                },
                { 
                    FunctionOperatorType.GetDate,
                    new FunctionFloorConverter()
                },
                { 
                    FunctionOperatorType.GetDay,
                    new FunctionDefaultConverter(0x43, 1)
                },
                { 
                    FunctionOperatorType.GetDayOfWeek,
                    new FunctionGetDayOfWeekConverter()
                },
                { 
                    FunctionOperatorType.GetDayOfYear,
                    new FunctionGetDayOfYearConverter()
                },
                { 
                    FunctionOperatorType.GetHour,
                    new FunctionDefaultConverter(0x47, 1)
                },
                { 
                    FunctionOperatorType.GetMinute,
                    new FunctionDefaultConverter(0x48, 1)
                },
                { 
                    FunctionOperatorType.GetMonth,
                    new FunctionDefaultConverter(0x44, 1)
                },
                { 
                    FunctionOperatorType.GetSecond,
                    new FunctionDefaultConverter(0x49, 1)
                },
                { 
                    FunctionOperatorType.GetYear,
                    new FunctionDefaultConverter(0x45, 1)
                },
                { 
                    FunctionOperatorType.Iif,
                    new FunctionVarDefaultConverter(1, 3, 3)
                },
                { 
                    FunctionOperatorType.IsNull,
                    new FunctionDefaultConverter(0x81, 1)
                },
                { 
                    FunctionOperatorType.IsNullOrEmpty,
                    new FunctionIsNullOrEmptyConverter()
                },
                { 
                    FunctionOperatorType.IsThisMonth,
                    new FunctionIsThisMonthConverter()
                },
                { 
                    FunctionOperatorType.IsThisWeek,
                    new FunctionIsThisWeekConverter()
                },
                { 
                    FunctionOperatorType.IsThisYear,
                    new FunctionIsThisYearConverter()
                },
                { 
                    FunctionOperatorType.Len,
                    new FunctionDefaultConverter(0x20, 1)
                },
                { 
                    FunctionOperatorType.LocalDateTimeDayAfterTomorrow,
                    new FunctionDateTimeTodayConverter(2)
                },
                { 
                    FunctionOperatorType.LocalDateTimeLastWeek,
                    new FunctionLocalDateTimeWeekConverter(-7)
                },
                { 
                    FunctionOperatorType.LocalDateTimeNextMonth,
                    new FunctionLocalDateTimeNextMonthConverter()
                },
                { 
                    FunctionOperatorType.LocalDateTimeNextWeek,
                    new FunctionLocalDateTimeWeekConverter(7)
                },
                { 
                    FunctionOperatorType.LocalDateTimeNextYear,
                    new FunctionLocalDateTimeNextYearConverter()
                },
                { 
                    FunctionOperatorType.LocalDateTimeNow,
                    new FunctionDefaultConverter(0x4a, 0)
                },
                { 
                    FunctionOperatorType.LocalDateTimeThisMonth,
                    new FunctionLocalDateTimeThisMonthConverter()
                },
                { 
                    FunctionOperatorType.LocalDateTimeThisWeek,
                    new FunctionLocalDateTimeWeekConverter(0)
                },
                { 
                    FunctionOperatorType.LocalDateTimeThisYear,
                    new FunctionLocalDateTimeThisYearConverter()
                },
                { 
                    FunctionOperatorType.LocalDateTimeToday,
                    new FunctionDefaultConverter(0xdd, 0)
                },
                { 
                    FunctionOperatorType.LocalDateTimeTomorrow,
                    new FunctionDateTimeTodayConverter(1)
                },
                { 
                    FunctionOperatorType.LocalDateTimeTwoWeeksAway,
                    new FunctionLocalDateTimeWeekConverter(14)
                },
                { 
                    FunctionOperatorType.LocalDateTimeYesterday,
                    new FunctionDateTimeTodayConverter(-1)
                },
                { 
                    FunctionOperatorType.Log,
                    new FunctionVarDefaultConverter(0x6d, 1, 2)
                },
                { 
                    FunctionOperatorType.Log10,
                    new FunctionDefaultConverter(0x17, 1)
                },
                { 
                    FunctionOperatorType.Lower,
                    new FunctionDefaultConverter(0x70, 1)
                },
                { 
                    FunctionOperatorType.Max,
                    new FunctionVarDefaultConverter(7, 1, 0x100)
                },
                { 
                    FunctionOperatorType.Min,
                    new FunctionVarDefaultConverter(6, 1, 0x100)
                },
                { 
                    FunctionOperatorType.Now,
                    new FunctionDefaultConverter(0x4a, 0)
                },
                { 
                    FunctionOperatorType.Power,
                    new FunctionDefaultConverter(0x63, 2)
                },
                { 
                    FunctionOperatorType.Rnd,
                    new FunctionDefaultConverter(0x3f, 1)
                },
                { 
                    FunctionOperatorType.Round,
                    new FunctionRoundConverter()
                },
                { 
                    FunctionOperatorType.Sign,
                    new FunctionDefaultConverter(0x1a, 1)
                },
                { 
                    FunctionOperatorType.Sin,
                    new FunctionDefaultConverter(15, 1)
                },
                { 
                    FunctionOperatorType.Sinh,
                    new FunctionDefaultConverter(0xe5, 1)
                },
                { 
                    FunctionOperatorType.StartsWith,
                    new FunctionStartsWithConverter()
                },
                { 
                    FunctionOperatorType.Tan,
                    new FunctionDefaultConverter(0x11, 1)
                },
                { 
                    FunctionOperatorType.Tanh,
                    new FunctionDefaultConverter(0xe7, 1)
                },
                { 
                    FunctionOperatorType.Today,
                    new FunctionDefaultConverter(0xdd, 0)
                },
                { 
                    FunctionOperatorType.Trim,
                    new FunctionDefaultConverter(0x76, 1)
                },
                { 
                    FunctionOperatorType.Upper,
                    new FunctionDefaultConverter(0x71, 1)
                }
            };

        public XlPtgBase CreateParsedThingForDouble(double numericValue)
        {
            double num = Math.Truncate(numericValue);
            return (((num != numericValue) || ((num < 0.0) || (num > 65535.0))) ? ((XlPtgBase) new XlPtgNum(numericValue)) : ((XlPtgBase) new XlPtgInt((int) num)));
        }

        public XlExpression Execute(CriteriaOperator criteriaOperator)
        {
            this.expression = new XlExpression();
            criteriaOperator.Accept<CriteriaPriorityClass>(this);
            if (this.expression.Volatile)
            {
                this.expression.Insert(0, new XlPtgAttrSemi());
            }
            return this.ParsedExpression;
        }

        private IFunctionConverter GetConverter(FunctionOperatorType functionType)
        {
            IFunctionConverter converter;
            return (!FunctionConvertersList.TryGetValue(functionType, out converter) ? null : converter);
        }

        private CriteriaPriorityClass Process(CriteriaOperator criteriaOperator) => 
            criteriaOperator.Accept<CriteriaPriorityClass>(this);

        private void ProcessOperandList(CriteriaOperatorCollection collection)
        {
            foreach (CriteriaOperator @operator in collection)
            {
                @operator.Accept<CriteriaPriorityClass>(this);
            }
        }

        protected internal XlExpression TestConvert(string expression)
        {
            OperandValue[] criteriaParametersList = new OperandValue[0];
            CriteriaOperator criteriaOperator = CriteriaOperator.Parse(expression, out criteriaParametersList);
            return this.Execute(criteriaOperator);
        }

        private void ThrowConversionError(string message)
        {
            throw new ExpressionConversionException(message);
        }

        public CriteriaPriorityClass Visit(AggregateOperand theOperand)
        {
            this.ThrowConversionError("Aggregate operand can not be converted to expression.");
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(BetweenOperator theOperator)
        {
            CriteriaPriorityClass cmpGt = CriteriaPriorityClass.CmpGt;
            if (this.Process(theOperator.BeginExpression) > cmpGt)
            {
                this.expression.Add(new XlPtgParen());
            }
            if (this.Process(theOperator.TestExpression) >= cmpGt)
            {
                this.expression.Add(new XlPtgParen());
            }
            this.expression.Add(new XlPtgBinaryOperator(10));
            if (this.Process(theOperator.TestExpression) > cmpGt)
            {
                this.expression.Add(new XlPtgParen());
            }
            if (this.Process(theOperator.EndExpression) >= cmpGt)
            {
                this.expression.Add(new XlPtgParen());
            }
            this.expression.Add(new XlPtgBinaryOperator(10));
            this.expression.Add(new XlPtgFuncVar(0x24, 2, XlPtgDataType.Value));
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(BinaryOperator theOperator)
        {
            XlPtgBase base2;
            CriteriaPriorityClass cmpEq;
            bool flag = false;
            switch (theOperator.OperatorType)
            {
                case BinaryOperatorType.Equal:
                    base2 = new XlPtgBinaryOperator(11);
                    cmpEq = CriteriaPriorityClass.CmpEq;
                    break;

                case BinaryOperatorType.NotEqual:
                    base2 = new XlPtgBinaryOperator(14);
                    cmpEq = CriteriaPriorityClass.CmpEq;
                    break;

                case BinaryOperatorType.Greater:
                    base2 = new XlPtgBinaryOperator(13);
                    cmpEq = CriteriaPriorityClass.CmpGt;
                    break;

                case BinaryOperatorType.Less:
                    base2 = new XlPtgBinaryOperator(9);
                    cmpEq = CriteriaPriorityClass.CmpGt;
                    break;

                case BinaryOperatorType.LessOrEqual:
                    base2 = new XlPtgBinaryOperator(10);
                    cmpEq = CriteriaPriorityClass.CmpGt;
                    break;

                case BinaryOperatorType.GreaterOrEqual:
                    base2 = new XlPtgBinaryOperator(12);
                    cmpEq = CriteriaPriorityClass.CmpGt;
                    break;

                case BinaryOperatorType.BitwiseAnd:
                    base2 = new XlPtgFunc(0x402a, XlPtgDataType.Value);
                    cmpEq = CriteriaPriorityClass.Atom;
                    flag = true;
                    break;

                case BinaryOperatorType.BitwiseOr:
                    base2 = new XlPtgFunc(0x402c, XlPtgDataType.Value);
                    cmpEq = CriteriaPriorityClass.Atom;
                    flag = true;
                    break;

                case BinaryOperatorType.BitwiseXor:
                    base2 = new XlPtgFunc(0x402e, XlPtgDataType.Value);
                    cmpEq = CriteriaPriorityClass.Atom;
                    flag = true;
                    break;

                case BinaryOperatorType.Divide:
                    base2 = new XlPtgBinaryOperator(6);
                    cmpEq = CriteriaPriorityClass.Mul;
                    break;

                case BinaryOperatorType.Modulo:
                    base2 = new XlPtgFunc(0x27, XlPtgDataType.Value);
                    cmpEq = CriteriaPriorityClass.Atom;
                    flag = true;
                    break;

                case BinaryOperatorType.Multiply:
                    base2 = new XlPtgBinaryOperator(5);
                    cmpEq = CriteriaPriorityClass.Mul;
                    break;

                case BinaryOperatorType.Plus:
                    base2 = new XlPtgBinaryOperator(3);
                    cmpEq = CriteriaPriorityClass.Add;
                    break;

                case BinaryOperatorType.Minus:
                    base2 = new XlPtgBinaryOperator(4);
                    cmpEq = CriteriaPriorityClass.Add;
                    break;

                default:
                    this.ThrowConversionError(string.Empty);
                    return CriteriaPriorityClass.Atom;
            }
            CriteriaPriorityClass class3 = this.Process(theOperator.LeftOperand);
            if (!flag && (class3 > cmpEq))
            {
                this.expression.Add(new XlPtgParen());
            }
            CriteriaPriorityClass class4 = this.Process(theOperator.RightOperand);
            if (!flag && (class4 >= cmpEq))
            {
                this.expression.Add(new XlPtgParen());
            }
            this.expression.Add(base2);
            return cmpEq;
        }

        public CriteriaPriorityClass Visit(FunctionOperator theOperator)
        {
            IFunctionConverter converter = this.GetConverter(theOperator.OperatorType);
            if (converter == null)
            {
                if (LikeCustomFunction.IsBinaryCompatibleLikeFunction(theOperator))
                {
                    converter = new FunctionLikeConverter();
                }
                else
                {
                    this.ThrowConversionError("Function " + theOperator.OperatorType.ToString() + " can not be converted.");
                }
            }
            converter.Culture = this.Culture;
            if (!converter.ConvertFunction(theOperator.Operands, this, this.expression))
            {
                this.ThrowConversionError("An error occurred while converting function " + theOperator.OperatorType.ToString() + ".");
            }
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(GroupOperator theOperator)
        {
            int num2;
            int count = theOperator.Operands.Count;
            if (count == 0)
            {
                this.ThrowConversionError("GroupOperator without operands can not be converted");
            }
            else if (count == 1)
            {
                return this.Process(theOperator.Operands[0]);
            }
            GroupOperatorType operatorType = theOperator.OperatorType;
            if (operatorType == GroupOperatorType.And)
            {
                num2 = 0x24;
            }
            else
            {
                if (operatorType != GroupOperatorType.Or)
                {
                    this.ThrowConversionError("Invalid GroupOperatorType" + theOperator.OperatorType.ToString());
                    return CriteriaPriorityClass.Atom;
                }
                num2 = 0x25;
            }
            this.ProcessOperandList(theOperator.Operands);
            this.expression.Add(new XlPtgFuncVar(num2, count, XlPtgDataType.Value));
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(InOperator theOperator)
        {
            this.ThrowConversionError("The \"in\" operator can be converted to Excel-compatible expression only using array constants which are illegal for conditional formatting rules.");
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(JoinOperand theOperand)
        {
            this.ThrowConversionError("Join operand can not be converted to expression.");
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(OperandProperty theOperand)
        {
            XlColumnLookupInfo info;
            string propertyName = theOperand.PropertyName;
            int columnIndex = this.columnNameConverter.GetColumnIndex(propertyName);
            if (columnIndex < 0)
            {
                this.ThrowConversionError("Property \"" + propertyName + "\" can not be converted to column.");
            }
            int rowOffset = this.columnNameConverter.GetRowOffset(propertyName);
            int count = this.expression.Count;
            if (this.Context.ReferenceMode == XlCellReferenceMode.Offset)
            {
                XlCellOffset cellOffset = new XlCellOffset(columnIndex, this.Context.RowOffset + rowOffset, XlCellOffsetType.Position, XlCellOffsetType.Offset);
                XlPtgRefN fn1 = new XlPtgRefN(cellOffset);
                fn1.DataType = XlPtgDataType.Value;
                XlPtgRefN item = fn1;
                this.expression.Add(item);
            }
            else
            {
                IXlTable currentTable = this.Context.CurrentTable;
                if (((rowOffset == 0) && (currentTable != null)) && ((columnIndex >= currentTable.Range.TopLeft.Column) && (columnIndex <= currentTable.Range.BottomRight.Column)))
                {
                    XlPtgTableRef item = new XlPtgTableRef(currentTable.GetRowReference(currentTable.Columns[columnIndex - currentTable.Range.TopLeft.Column].Name), XlPtgDataType.Value);
                    this.expression.Add(item);
                }
                else
                {
                    XlCellPosition cellPosition = new XlCellPosition(columnIndex, this.Context.CurrentCell.Row + rowOffset, XlPositionType.Relative, XlPositionType.Relative);
                    XlPtgRef item = new XlPtgRef(cellPosition, XlPtgDataType.Value);
                    this.expression.Add(item);
                }
            }
            if (this.Context.LookupColumns.TryGetValue(propertyName, out info))
            {
                if (!string.IsNullOrEmpty(info.Range.SheetName))
                {
                    this.expression.Add(new XlPtgArea3d(info.Range, info.Range.SheetName));
                }
                else
                {
                    this.expression.Add(new XlPtgArea(info.Range));
                }
                this.expression.Add(new XlPtgInt(info.ResultColumn));
                this.expression.Add(new XlPtgInt(info.ApproximateMatch ? 1 : 0));
                this.expression.Add(new XlPtgFuncVar(0x66, 4, XlPtgDataType.Value));
                int num4 = this.expression.Count;
                this.expression.Add(new XlPtgFunc(2, XlPtgDataType.Value));
                this.expression.Add(new XlPtgStr(""));
                int num5 = count;
                while (true)
                {
                    if (num5 >= num4)
                    {
                        this.expression.Add(new XlPtgFuncVar(1, 3, XlPtgDataType.Value));
                        break;
                    }
                    this.expression.Add(this.expression[num5]);
                    num5++;
                }
            }
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(OperandValue theOperand)
        {
            XlPtgBase item = this.ConvertOperand(theOperand.Value);
            if (item == null)
            {
                this.ThrowConversionError("Operand can not be converted.");
            }
            this.expression.Add(item);
            return CriteriaPriorityClass.Atom;
        }

        public CriteriaPriorityClass Visit(UnaryOperator theOperator)
        {
            XlPtgBase base2;
            CriteriaPriorityClass binaryNot;
            bool flag = false;
            switch (theOperator.OperatorType)
            {
                case UnaryOperatorType.BitwiseNot:
                    base2 = null;
                    binaryNot = CriteriaPriorityClass.BinaryNot;
                    break;

                case UnaryOperatorType.Plus:
                    base2 = new XlPtgUnaryOperator(0x12);
                    binaryNot = CriteriaPriorityClass.Neg;
                    break;

                case UnaryOperatorType.Minus:
                    base2 = new XlPtgUnaryOperator(0x13);
                    binaryNot = CriteriaPriorityClass.Neg;
                    break;

                case UnaryOperatorType.Not:
                    base2 = new XlPtgFunc(0x26, XlPtgDataType.Value);
                    binaryNot = CriteriaPriorityClass.Atom;
                    flag = true;
                    break;

                case UnaryOperatorType.IsNull:
                    base2 = new XlPtgFunc(0x81, XlPtgDataType.Value);
                    binaryNot = CriteriaPriorityClass.IsNull;
                    binaryNot = CriteriaPriorityClass.Atom;
                    flag = true;
                    break;

                default:
                    this.ThrowConversionError(string.Empty);
                    return CriteriaPriorityClass.Atom;
            }
            CriteriaPriorityClass class3 = this.Process(theOperator.Operand);
            if (!flag && (class3 > binaryNot))
            {
                this.expression.Add(new XlPtgParen());
            }
            this.expression.Add(base2);
            return binaryNot;
        }

        public IXlExpressionContextEx Context =>
            this.context;

        public CultureInfo Culture
        {
            get => 
                (this.culture != null) ? this.culture : CultureInfo.InvariantCulture;
            set => 
                this.culture = value;
        }

        public XlExpression ParsedExpression =>
            this.expression;

        private static Dictionary<FunctionOperatorType, IFunctionConverter> FunctionConvertersList
        {
            get
            {
                functionConvertersList ??= CreateFunctionConvertersList();
                return functionConvertersList;
            }
        }
    }
}

