namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class IndicatorAnimationFactory : ConditionalAnimationFactoryBase
    {
        protected IndicatorAnimationFactory()
        {
        }

        protected string GetActualFieldName()
        {
            Func<FormatConditionBaseInfo, string> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<FormatConditionBaseInfo, string> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.ActualFieldName;
            }
            return this.GetConditionInfo().With<FormatConditionBaseInfo, string>(evaluator);
        }

        public override void UpdateContext(DataUpdate update)
        {
            if (update != null)
            {
                this.Provider = update.GetNewValue(this.GetActualFieldName());
            }
        }

        public FormatValueProvider Provider { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IndicatorAnimationFactory.<>c <>9 = new IndicatorAnimationFactory.<>c();
            public static Func<FormatConditionBaseInfo, string> <>9__4_0;

            internal string <GetActualFieldName>b__4_0(FormatConditionBaseInfo x) => 
                x.ActualFieldName;
        }
    }
}

