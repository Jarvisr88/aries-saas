namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    [ContentProperty("Conditions"), Browsable(true)]
    public class RenderConditionGroup : RenderPropertyBase
    {
        private readonly RenderConditionCollection conditions;

        public RenderConditionGroup();
        public override RenderPropertyContextBase CreateContext();
        protected bool Equals(RenderConditionGroup other);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public RenderConditionCollection Conditions { get; }

        public RenderConditionGroupOperator Operator { get; set; }
    }
}

