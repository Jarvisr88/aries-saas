namespace DMEWorks.Expressions
{
    using System;
    using System.Linq.Expressions;

    public abstract class DmeworksExpressionVisitor : ExpressionVisitor
    {
        protected DmeworksExpressionVisitor()
        {
        }

        protected sealed override Expression VisitBlock(BlockExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitConditional(ConditionalExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitConstant(ConstantExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitDynamic(DynamicExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override ElementInit VisitElementInit(ElementInit initializer)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitExtension(Expression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitGoto(GotoExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitIndex(IndexExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitInvocation(InvocationExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitLabel(LabelExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override LabelTarget VisitLabelTarget(LabelTarget node) => 
            base.VisitLabelTarget(node);

        protected sealed override Expression VisitListInit(ListInitExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitLoop(LoopExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitMember(MemberExpression assignment)
        {
            throw new NotSupportedException();
        }

        protected sealed override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            throw new NotSupportedException();
        }

        protected sealed override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override MemberListBinding VisitMemberListBinding(MemberListBinding binding)
        {
            throw new NotSupportedException();
        }

        protected sealed override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitMethodCall(MethodCallExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitNew(NewExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitNewArray(NewArrayExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitParameter(ParameterExpression node)
        {
            throw new NotSupportedException();
        }

        protected internal abstract Expression VisitPrimitive(PrimitiveExpression node);
        protected internal abstract Expression VisitPrimitiveComparison(PrimitiveComparisonExpression node);
        protected sealed override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitTry(TryExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotSupportedException();
        }

        protected sealed override Expression VisitUnary(UnaryExpression node)
        {
            throw new NotSupportedException();
        }
    }
}

