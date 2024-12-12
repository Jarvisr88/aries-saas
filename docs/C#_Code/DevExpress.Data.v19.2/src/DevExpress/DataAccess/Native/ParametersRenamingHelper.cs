namespace DevExpress.DataAccess.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class ParametersRenamingHelper : IClientCriteriaVisitor, ICriteriaVisitor
    {
        private readonly IDictionary<string, string> renamingMap;

        public ParametersRenamingHelper(IDictionary<string, string> renamingMap)
        {
            this.renamingMap = renamingMap;
        }

        public void Process(CriteriaOperator op)
        {
            if (op != null)
            {
                op.Accept(this);
            }
        }

        public void Process(IEnumerable<IParameter> parameters)
        {
            foreach (IParameter parameter in parameters)
            {
                if (parameter.Type == typeof(Expression))
                {
                    Expression expression = parameter.Value as Expression;
                    if (expression != null)
                    {
                        CriteriaOperator @operator;
                        try
                        {
                            @operator = CriteriaOperator.Parse(expression.ExpressionString, new object[0]);
                        }
                        catch
                        {
                            continue;
                        }
                        if (@operator != null)
                        {
                            this.Process(@operator);
                            expression.ExpressionString = @operator.ToString();
                        }
                    }
                }
            }
        }

        public void Visit(AggregateOperand theOperand)
        {
        }

        public void Visit(BetweenOperator theOperator)
        {
            this.Process(theOperator.TestExpression);
            this.Process(theOperator.BeginExpression);
            this.Process(theOperator.EndExpression);
        }

        public void Visit(BinaryOperator theOperator)
        {
            this.Process(theOperator.LeftOperand);
            this.Process(theOperator.RightOperand);
        }

        public void Visit(FunctionOperator theOperator)
        {
            foreach (CriteriaOperator @operator in theOperator.Operands)
            {
                this.Process(@operator);
            }
        }

        public void Visit(GroupOperator theOperator)
        {
            foreach (CriteriaOperator @operator in theOperator.Operands)
            {
                this.Process(@operator);
            }
        }

        public void Visit(InOperator theOperator)
        {
            this.Process(theOperator.LeftOperand);
            foreach (CriteriaOperator @operator in theOperator.Operands)
            {
                this.Process(@operator);
            }
        }

        public void Visit(JoinOperand theOperand)
        {
        }

        public void Visit(OperandProperty theOperand)
        {
            Match match = new Regex(@"\AParameters\.(?<name>.+)\z", RegexOptions.None).Match(theOperand.PropertyName);
            if (match.Success)
            {
                string str3;
                string key = match.Groups["name"].Value;
                if (this.renamingMap.TryGetValue(key, out str3))
                {
                    theOperand.PropertyName = $"Parameters.{str3}";
                }
            }
        }

        public void Visit(OperandValue theOperand)
        {
            OperandParameter parameter = theOperand as OperandParameter;
            if ((parameter != null) && this.renamingMap.ContainsKey(parameter.ParameterName))
            {
                parameter.ParameterName = this.renamingMap[parameter.ParameterName];
            }
        }

        public void Visit(UnaryOperator theOperator)
        {
            this.Process(theOperator.Operand);
        }
    }
}

