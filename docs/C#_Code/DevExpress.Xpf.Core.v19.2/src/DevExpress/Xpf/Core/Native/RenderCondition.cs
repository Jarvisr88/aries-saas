namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Browsable(true)]
    public class RenderCondition : RenderPropertyChangedListener
    {
        public RenderCondition();
        public override RenderPropertyContextBase CreateContext();
        protected bool Equals(RenderCondition other);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public object Value { get; set; }

        public bool FallbackIsValid { get; set; }

        public RenderConditionOperator Operator { get; set; }
    }
}

