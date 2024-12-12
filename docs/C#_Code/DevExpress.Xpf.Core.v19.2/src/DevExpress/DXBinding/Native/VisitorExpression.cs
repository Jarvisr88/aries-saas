namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class VisitorExpression : VisitorExpressionBase
    {
        private readonly IErrorHandler errorHandler;
        private readonly Dictionary<Operand, OperandInfo> operandInfos;
        private readonly Func<NType, Type> typeResolver;
        private List<ParameterExpression> currentParameters;
        private ParameterExpression backParam;
        private ParameterExpression parameterParam;
        private ParameterExpression senderParam;
        private ParameterExpression argsParam;

        public VisitorExpression(Dictionary<Operand, OperandInfo> operandInfos, Func<NType, Type> typeResolver, IErrorHandler errorHandler)
        {
            this.operandInfos = operandInfos;
            this.errorHandler = errorHandler;
            this.typeResolver = typeResolver;
        }

        protected override bool CanContinue(NBase n, Operand operand)
        {
            if (!this.errorHandler.HasError)
            {
                if (((operand == null) || (!(n is NIdent) && !(n is NMethod))) || (this.operandInfos[operand].OperandValue != null))
                {
                    return true;
                }
                this.errorHandler.Report(null, true);
            }
            return false;
        }

        private void Clean()
        {
            this.currentParameters = null;
            this.backParam = null;
            this.parameterParam = null;
        }

        private Func<object[], object> Compile(Expression expr) => 
            (expr != null) ? new Func<object[], object>(Expression.Lambda(expr, this.currentParameters).Compile().DynamicInvoke) : null;

        protected override ParameterExpression GetOperandParameter(Operand operand, NRelative.NKind? relativeSource)
        {
            if (relativeSource == null)
            {
                if (operand == null)
                {
                    return null;
                }
                if (this.operandInfos[operand].Parameter == null)
                {
                    this.operandInfos[operand].CreateParameter();
                }
                ParameterExpression parameter = this.operandInfos[operand].Parameter;
                if (!this.currentParameters.Contains(parameter))
                {
                    this.currentParameters.Add(parameter);
                }
                return parameter;
            }
            switch (relativeSource.Value)
            {
                case NRelative.NKind.Value:
                    return this.backParam;

                case NRelative.NKind.Parameter:
                    return this.parameterParam;

                case NRelative.NKind.Sender:
                    return this.senderParam;

                case NRelative.NKind.Args:
                    return this.argsParam;
            }
            throw new NotImplementedException();
        }

        public Func<object[], object> Resolve(NRoot expr)
        {
            this.currentParameters = new List<ParameterExpression>();
            Expression expression = base.Resolve(expr, this.typeResolver, this.errorHandler).FirstOrDefault<Expression>();
            Func<object[], object> func = this.Compile(expression);
            this.Clean();
            return func;
        }

        public void ResolveBack(NRoot backExpr, Type backExprType)
        {
            if (backExprType == null)
            {
                this.errorHandler.Throw(ErrorHelper.Err104(), null);
            }
            this.currentParameters = new List<ParameterExpression>();
            this.backParam = Expression.Parameter(backExprType, "$value");
            IEnumerable<Tuple<Operand, Expression>> enumerable = base.ResolveBackCore(backExpr, this.typeResolver, this.errorHandler);
            this.currentParameters.Add(this.backParam);
            foreach (Tuple<Operand, Expression> tuple in enumerable)
            {
                Func<object[], object> backConverter = this.Compile(tuple.Item2);
                if (tuple.Item1 == null)
                {
                    this.operandInfos.Keys.First<Operand>().SetBackConverter(backConverter);
                    continue;
                }
                this.operandInfos.Keys.First<Operand>(x => x.Equals(tuple.Item1)).SetBackConverter(backConverter);
            }
            this.Clean();
        }

        public Func<object[], object> ResolveEvent(NRoot expr, Type senderType, Type argsType)
        {
            this.currentParameters = new List<ParameterExpression>();
            Type type = senderType;
            if (senderType == null)
            {
                Type local1 = senderType;
                type = typeof(object);
            }
            this.senderParam = Expression.Parameter(type);
            Type type2 = argsType;
            if (argsType == null)
            {
                Type local2 = argsType;
                type2 = typeof(object);
            }
            this.argsParam = Expression.Parameter(type2);
            IEnumerable<Expression> expressions = base.Resolve(expr, this.typeResolver, this.errorHandler);
            this.currentParameters.Add(this.senderParam);
            this.currentParameters.Add(this.argsParam);
            Func<object[], object> func = this.errorHandler.HasError ? null : this.Compile(Expression.Block(expressions));
            this.Clean();
            return func;
        }

        public void ResolveExecute(NRoot executeExpr, NRoot canExecuteExpr, Type parameterType, out Func<object[], object> execute, out Func<object[], object> canExecute)
        {
            execute = null;
            canExecute = null;
            this.currentParameters = new List<ParameterExpression>();
            Type type = parameterType;
            if (parameterType == null)
            {
                Type local1 = parameterType;
                type = typeof(object);
            }
            this.parameterParam = Expression.Parameter(type);
            IEnumerable<Expression> expressions = base.Resolve(executeExpr, this.typeResolver, this.errorHandler);
            IEnumerable<Expression> source = base.Resolve(canExecuteExpr, this.typeResolver, this.errorHandler);
            this.currentParameters.Add(this.parameterParam);
            if (!this.errorHandler.HasError)
            {
                execute = this.Compile(Expression.Block(expressions));
                canExecute = this.Compile(Expression.Convert(source.First<Expression>(), typeof(bool)));
            }
            this.Clean();
        }

        public class OperandInfo
        {
            public void Clear()
            {
                this.OperandValue = null;
            }

            public void ClearParameter()
            {
                this.Parameter = null;
            }

            public void CreateParameter()
            {
                Type operandType = this.OperandType;
                Type type = operandType;
                if (operandType == null)
                {
                    Type local1 = operandType;
                    type = typeof(object);
                }
                this.Parameter = Expression.Parameter(type);
            }

            public void Init(object opValue)
            {
                this.OperandValue = opValue;
                this.OperandType = opValue?.GetType();
            }

            public Type OperandType { get; private set; }

            public object OperandValue { get; private set; }

            public ParameterExpression Parameter { get; private set; }
        }
    }
}

