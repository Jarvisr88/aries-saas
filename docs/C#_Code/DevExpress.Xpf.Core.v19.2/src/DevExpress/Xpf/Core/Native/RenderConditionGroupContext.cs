namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderConditionGroupContext : RenderPropertyContextBase, IRenderConditionContext, IRenderPropertyContext
    {
        private readonly IRenderConditionContext[] conditionContexts;

        public RenderConditionGroupContext(RenderConditionGroup factory);
        protected override void AttachOverride(Namescope scope, RenderTriggerContextBase context);
        protected virtual bool CalculateIsValid();
        protected virtual bool CalculateIsValidAnd();
        protected virtual bool CalculateIsValidOr();
        protected override void DetachOverride();
        public IRenderConditionContext GetChild(int index);
        public int GetChildrenCount();
        protected override void ResetOverride(RenderValueSource valueSource);

        public RenderConditionGroup Factory { get; }

        public bool IsValid { get; }
    }
}

