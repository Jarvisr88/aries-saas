namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    internal class IndicatorFormatConditionExportWrapperDelegate
    {
        private readonly IndicatorFormatConditionBase formatCondition;

        public IndicatorFormatConditionExportWrapperDelegate(IndicatorFormatConditionBase formatCondition)
        {
            this.formatCondition = formatCondition;
        }

        private string WrapFieldNameAsExpression() => 
            (this.FieldName == null) ? null : ("[" + this.FieldName + "]");

        private string Expression
        {
            get
            {
                Func<IndicatorFormatConditionBase, string> evaluator = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<IndicatorFormatConditionBase, string> local1 = <>c.<>9__3_0;
                    evaluator = <>c.<>9__3_0 = x => x.Expression;
                }
                return this.formatCondition.With<IndicatorFormatConditionBase, string>(evaluator);
            }
        }

        private string FieldName
        {
            get
            {
                Func<IndicatorFormatConditionBase, string> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<IndicatorFormatConditionBase, string> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x.FieldName;
                }
                return this.formatCondition.With<IndicatorFormatConditionBase, string>(evaluator);
            }
        }

        public bool IsValid =>
            string.IsNullOrEmpty(this.Expression) || (this.Expression == this.WrapFieldNameAsExpression());

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IndicatorFormatConditionExportWrapperDelegate.<>c <>9 = new IndicatorFormatConditionExportWrapperDelegate.<>c();
            public static Func<IndicatorFormatConditionBase, string> <>9__3_0;
            public static Func<IndicatorFormatConditionBase, string> <>9__5_0;

            internal string <get_Expression>b__3_0(IndicatorFormatConditionBase x) => 
                x.Expression;

            internal string <get_FieldName>b__5_0(IndicatorFormatConditionBase x) => 
                x.FieldName;
        }
    }
}

