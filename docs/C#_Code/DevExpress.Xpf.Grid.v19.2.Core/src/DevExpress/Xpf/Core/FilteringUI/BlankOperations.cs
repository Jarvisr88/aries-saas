namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    public class BlankOperations
    {
        public BlankOperations(Func<object, bool> isBlankValue, Func<string, AllowedUnaryFilters, CriteriaOperator> createIsNullOperator, Func<object, bool> isEmptyValue)
        {
            this.<IsBlankValue>k__BackingField = isBlankValue;
            this.<CreateIsNullOperator>k__BackingField = createIsNullOperator;
            this.<IsEmptyValue>k__BackingField = isEmptyValue;
        }

        public Func<object, bool> IsBlankValue { get; }

        public Func<string, AllowedUnaryFilters, CriteriaOperator> CreateIsNullOperator { get; }

        public Func<object, bool> IsEmptyValue { get; }
    }
}

