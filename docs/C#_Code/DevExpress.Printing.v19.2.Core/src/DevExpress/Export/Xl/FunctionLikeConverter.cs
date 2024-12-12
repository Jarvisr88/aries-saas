namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using DevExpress.XtraExport.Utils;
    using System;
    using System.Text;

    internal class FunctionLikeConverter : FunctionConverter
    {
        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 3;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            base.ProcessOperand(operands, visitor, 2);
            XlPtgStr ptg = expression[expression.Count - 1] as XlPtgStr;
            if (!this.ConvertWildCard(ptg))
            {
                return false;
            }
            LikePatternKind patternKind = LikePatternHelper.GetPatternKind(ptg.Value);
            if (patternKind == LikePatternKind.StartWith)
            {
                expression.RemoveAt(expression.Count - 1);
                base.ProcessOperand(operands, visitor, 1);
                ptg.Value = ptg.Value.Substring(0, ptg.Value.Length - 1);
                expression.Add(ptg);
                expression.Add(base.CreateFuncThing(0x20));
                expression.Add(base.CreateFuncVarThing(0x73, 2));
                expression.Add(ptg);
                expression.Add(new XlPtgBinaryOperator(11));
            }
            else if (patternKind != LikePatternKind.EndWith)
            {
                base.ProcessOperand(operands, visitor, 1);
                expression.Add(base.CreateFuncVarThing(0x52, 2));
                expression.Add(base.CreateFuncThing(3));
                expression.Add(base.CreateFuncThing(0x26));
            }
            else
            {
                expression.RemoveAt(expression.Count - 1);
                base.ProcessOperand(operands, visitor, 1);
                ptg.Value = ptg.Value.Substring(0, ptg.Value.Length - 1);
                expression.Add(ptg);
                expression.Add(base.CreateFuncThing(0x20));
                expression.Add(base.CreateFuncVarThing(0x74, 2));
                expression.Add(ptg);
                expression.Add(new XlPtgBinaryOperator(11));
            }
            return true;
        }

        private bool ConvertWildCard(XlPtgStr ptg)
        {
            if (ptg == null)
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            int num = 0;
            foreach (char ch in ptg.Value)
            {
                if (flag && (((num + 1) > 1) && (ch != ']')))
                {
                    return false;
                }
                if (ch == '~')
                {
                    builder.Append("~~");
                }
                else if (ch == '*')
                {
                    builder.Append("~*");
                }
                else if (ch == '?')
                {
                    builder.Append("~?");
                }
                else if (ch == '%')
                {
                    builder.Append(flag ? ch : '*');
                }
                else if (ch == '_')
                {
                    builder.Append(flag ? ch : '?');
                }
                else if (ch == '[')
                {
                    if (flag)
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        flag = true;
                        num = 0;
                    }
                }
                else if (ch != ']')
                {
                    builder.Append(ch);
                }
                else if (!flag)
                {
                    builder.Append(ch);
                }
                else
                {
                    flag = false;
                }
            }
            ptg.Value = builder.ToString();
            return true;
        }
    }
}

