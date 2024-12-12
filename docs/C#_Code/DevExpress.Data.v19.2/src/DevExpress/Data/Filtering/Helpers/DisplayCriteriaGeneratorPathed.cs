namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class DisplayCriteriaGeneratorPathed
    {
        public static CriteriaOperator Process(IDisplayCriteriaGeneratorNamesSourcePathed namesSource, CriteriaOperator op);

        private class Impl : DisplayCriteriaGenerator
        {
            public readonly IDisplayCriteriaGeneratorNamesSourcePathed PathedNamesSource;
            private readonly Stack<string> stackTrace;

            public Impl(IDisplayCriteriaGeneratorNamesSourcePathed namesSource);
            private OperandProperty AwkwardConvert(OperandProperty theOperand);
            public CriteriaOperator Do(CriteriaOperator arg);
            public override CriteriaOperator Visit(AggregateOperand theOperand);
            public override CriteriaOperator Visit(FunctionOperator theOperator);
            public override CriteriaOperator Visit(OperandProperty theOperand);

            private string PropertyFullPath { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DisplayCriteriaGeneratorPathed.Impl.<>c <>9;
                public static Func<CriteriaOperator, CriteriaOperator> <>9__8_0;

                static <>c();
                internal CriteriaOperator <Visit>b__8_0(CriteriaOperator t);
            }

            private class NamesSourceWrapper : IDisplayCriteriaGeneratorNamesSource
            {
                private readonly IDisplayCriteriaGeneratorNamesSourcePathed Pathed;

                public NamesSourceWrapper(IDisplayCriteriaGeneratorNamesSourcePathed pathed);
                public string GetDisplayPropertyName(OperandProperty property);
                public string GetValueScreenText(OperandProperty property, object value);
            }
        }
    }
}

