namespace DevExpress.DataAccess.Native.Sql
{
    using DevExpress.Data.Browsing;
    using DevExpress.Data.Controls.ExpressionEditor;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Exceptions;
    using DevExpress.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class ParametersEvaluator
    {
        public static object EvaluateExpression(this Expression expression, XREvaluatorContextDescriptor context)
        {
            CriteriaOperator @operator;
            try
            {
                @operator = CriteriaOperator.Parse(expression.ExpressionString, (object[]) null);
            }
            catch (CriteriaParserException)
            {
                @operator = CriteriaOperator.Parse(string.Empty, (object[]) null);
            }
            ICustomFunctionOperator[] customFunctions = new ICustomFunctionOperator[] { JoinFunction.Instance, CreateTableFunction.Instance };
            object o = new CalculatedExpressionEvaluator(context, @operator, true, customFunctions, new ICustomAggregate[0]).Evaluate(null);
            if ((o != null) && ((expression.ResultType != null) && !expression.ResultType.IsInstanceOfType(o)))
            {
                try
                {
                    return Convert.ChangeType(o, expression.ResultType);
                }
                catch (InvalidCastException)
                {
                }
            }
            return o;
        }

        public static object EvaluateExpression(this Expression expression, IEnumerable<IParameter> sourceParameters)
        {
            XREvaluatorContextDescriptor context = new XREvaluatorContextDescriptor(sourceParameters, new DataContext(), null, string.Empty);
            return expression.EvaluateExpression(context);
        }

        public static IEnumerable<IParameter> EvaluateParameters(IEnumerable<IParameter> sourceParameters, IEnumerable<DataSourceParameterBase> parametersToEvaluate)
        {
            List<IParameter> list = new List<IParameter>();
            XREvaluatorContextDescriptor context = new XREvaluatorContextDescriptor(sourceParameters, new DataContext(), null, string.Empty);
            foreach (DataSourceParameterBase base2 in parametersToEvaluate)
            {
                if ((base2.Type != typeof(Expression)) || (base2.Value == null))
                {
                    list.Add(new DataSourceParameterBase(base2.Name, base2.Type, base2.Value));
                    continue;
                }
                Expression expression = base2.Value as Expression;
                if (expression == null)
                {
                    throw new InvalidOperationException("If the Type property is set to Expression, the Value property must return an Expression instance.");
                }
                object obj2 = expression.EvaluateExpression(context);
                list.Add(new DataSourceParameterBase(base2.Name, (obj2 != null) ? obj2.GetType() : typeof(object), obj2));
            }
            return list;
        }
    }
}

