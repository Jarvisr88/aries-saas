namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    internal class CriteriaConverter<T>
    {
        public readonly Func<CriteriaOperator, T> ToValue;
        public readonly Func<T, string, CriteriaOperator> ToCriteria;
        public readonly Func<FilterRestrictions, bool> CanBuildFilter;

        public CriteriaConverter(Func<CriteriaOperator, T> toValue, Func<T, string, CriteriaOperator> toCriteria, Func<FilterRestrictions, bool> canBuildFilter)
        {
            this.ToValue = toValue;
            this.ToCriteria = toCriteria;
            this.CanBuildFilter = canBuildFilter;
        }
    }
}

