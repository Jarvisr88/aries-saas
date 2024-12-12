namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CriteriaToExpressionConverterInternal : IClientCriteriaVisitor<Expression>, ICriteriaVisitor<Expression>
    {
        private static readonly Dictionary<Type, bool> CompareToTypes;
        private static readonly Dictionary<Type, HashSet<Type>> AppliableTypes;
        protected readonly ICriteriaToExpressionConverter Owner;
        protected readonly Stack<Expression> ExpressionStack;
        protected ParameterExpression ThisExpression;
        protected Expression BaseExpression;

        public event EventHandler<CriteriaToExpressionConverterOnCriteriaArgs> OnFunctionOperator;

        static CriteriaToExpressionConverterInternal();
        public CriteriaToExpressionConverterInternal(ICriteriaToExpressionConverter owner, ParameterExpression thisExpression);
        private Expression AggregateBasic(AggregateOperand theOperand, Type rowType, Expression collectionProperty, ParameterExpression elementParameter);
        private Expression AggregateCustom(AggregateOperand theOperand, Type rowType, Expression collectionProperty, ParameterExpression elementParameter);
        private Expression BitwiseOperator(Expression left, Expression right, BinaryOperatorType binaryOperatorType);
        protected static bool CanBeApplied(Type targetType, Type sourceType);
        public Expression ConvertFromNullable(Expression exp);
        public Expression ConvertIfArgumentsAreNull(IList<Expression> expression, Expression result);
        public Expression ConvertToNullable(Expression exp);
        protected virtual Expression ConvertToType(Expression instanceExpr, Type type);
        private MethodCallExpression FnCharIndex(FunctionOperator theOperator);
        private Expression FnConcat(FunctionOperator theOperator);
        private static Expression FnConcat(Expression[] args, bool allStrings);
        private Expression FnCustom(FunctionOperator theOperator);
        private Expression FnCustomCore(string name, IEnumerable<CriteriaOperator> operands);
        private Expression FnIif(FunctionOperator theOperator);
        private Expression FnIif(Expression[] args, int index);
        private Expression FnIsNull(FunctionOperator theOperator);
        private Expression FnIsNullOrEmpty(FunctionOperator theOperator);
        public Expression FnToType(FunctionOperator theOperator, Type type);
        private static Type GetBinaryNumericPromotionType(Type leftType, Type rightType, BinaryOperatorType exceptionType);
        private static MethodInfo GetStringConcatMethod(Type[] parameters);
        protected static bool IsCompareToExpressions(Type leftType, Type rightType);
        protected static bool IsNotNullType(Type type);
        public static bool IsNullable(Expression exp);
        private Expression MakeComparerCallExpression(Expression left, Expression right);
        public Expression MakeInstanceCall(string methodName, Type type, FunctionOperator theOperator, params Type[] argTypes);
        public Expression MakeInstanceCall(string methodName, Type type, FunctionOperator theOperator, bool forceIifForInstance, params Type[] argTypes);
        public Expression MakeInstanceMemberAccess(string memberName, Type type, FunctionOperator theOperator);
        public Expression MakeInstanceMemberAccess(string memberName, Type type, FunctionOperator theOperator, bool forceIifForInstance);
        public MemberExpression MakeInstanceMemberAccessCore(string memberName, Type type, Expression arg);
        protected Expression MakePropertyAccess(Expression expression, string propertyName);
        public Expression MakeStaticCall(string methodName, Type type, FunctionOperator theOperator, params Type[] argTypes);
        public Expression MakeStaticCall(string methodName, Type type, FunctionOperator theOperator, bool useArgTypesOnlyIfDifferenct, params Type[] argTypes);
        public Expression MakeStaticCallCore(string methodName, Type type, FunctionOperator theOperator, bool useArgTypesOnlyIfDifferenct, object thisArgument, Type[] argTypes);
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Expression MakeStaticCallSqlCore(string methodName, FunctionOperator theOperator, bool useArgTypesOnlyIfDifferenct, object thisArgument, Type[] argTypes);
        public Expression MakeStaticExtensionCall(string methodName, Type type, FunctionOperator theOperator, object thisArgument, params Type[] argTypes);
        public MemberExpression MakeStaticMemberAccess(string memberName, Type type);
        public Expression MakeStaticSqlCall(string methodName, FunctionOperator theOperator, params Type[] argTypes);
        private static MethodCallExpression MakeStringCompareCallExpression(Expression left, Expression right);
        private void Nullabler(ref Expression left, ref Expression right);
        private static void PrepareParametersForBinaryOperator(ref Expression left, ref Expression right, BinaryOperatorType binaryOperatorType);
        public Expression Process(CriteriaOperator op);
        protected virtual Expression ProcessInContext(OperandProperty contextProperty, ParameterExpression thisExpression, CriteriaOperator op);
        public static void RemoveCompareType(Type t);
        private Expression[] RolloutPropertyExpressions(OperandProperty contextProperty, out int upLevels);
        public static void SetCompareType(Type t, bool v);
        private void TryAssignThisExpression();
        public Expression Visit(AggregateOperand theOperand);
        public Expression Visit(BetweenOperator theOperator);
        public Expression Visit(BinaryOperator theOperator);
        public Expression Visit(FunctionOperator theOperator);
        public Expression Visit(GroupOperator theOperator);
        public Expression Visit(InOperator theOperator);
        public Expression Visit(JoinOperand theOperand);
        public Expression Visit(OperandProperty theOperand);
        public Expression Visit(OperandValue theOperand);
        public Expression Visit(UnaryOperator theOperator);
        protected virtual Expression VisitInternal(AggregateOperand theOperand);
        protected virtual Expression VisitInternal(BetweenOperator theOperator);
        protected virtual Expression VisitInternal(BinaryOperator theOperator);
        protected virtual Expression VisitInternal(FunctionOperator theOperator);
        protected virtual Expression VisitInternal(GroupOperator theOperator);
        protected virtual Expression VisitInternal(InOperator theOperator);
        protected virtual Expression VisitInternal(JoinOperand theOperand);
        protected virtual Expression VisitInternal(OperandProperty theOperand);
        protected virtual Expression VisitInternal(OperandValue theOperand);
        protected virtual Expression VisitInternal(UnaryOperator theOperator);

        public virtual bool ForceIifForInstance { get; }
    }
}

