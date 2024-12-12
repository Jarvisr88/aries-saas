namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class StringsTolowerCloningHelper : ClientCriteriaClonerBase.DeepValuesCloneBase
    {
        private static readonly StringsTolowerCloningHelper Instance;

        static StringsTolowerCloningHelper();
        private StringsTolowerCloningHelper();
        private CriteriaOperator LiftToToLowerIfNeeded(CriteriaOperator processed);
        public static CriteriaOperator Process(CriteriaOperator op);
        public override CriteriaOperator Visit(BinaryOperator theOperator);
        public override CriteriaOperator Visit(FunctionOperator theOperator);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StringsTolowerCloningHelper.<>c <>9;
            public static Predicate<FunctionOperator> <>9__2_0;
            public static Func<FunctionOperator, FunctionOperator> <>9__2_1;
            public static Func<CriteriaOperator, FunctionOperator> <>9__2_2;

            static <>c();
            internal bool <LiftToToLowerIfNeeded>b__2_0(FunctionOperator fop);
            internal FunctionOperator <LiftToToLowerIfNeeded>b__2_1(FunctionOperator alreadyLower);
            internal FunctionOperator <LiftToToLowerIfNeeded>b__2_2(CriteriaOperator other);
        }
    }
}

