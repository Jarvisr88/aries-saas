namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;

    internal abstract class OperatorMenuItemIdentityFactoryBase<TID>
    {
        protected OperatorMenuItemIdentityFactoryBase()
        {
        }

        public abstract TID CreateBetween();
        public abstract TID CreateBetweenDates();
        public abstract TID CreateBinary(BinaryOperatorType type);
        public abstract TID CreateCustom(string name);
        public abstract TID CreateDoesNotContain();
        public abstract TID CreateFunction(FunctionOperatorType type);
        public abstract TID CreateIsNotNull();
        public abstract TID CreateIsNotNullOrEmpty();
        public abstract TID CreateIsNotOnDate();
        public abstract TID CreateIsNull();
        public abstract TID CreateIsOnDate();
        public abstract TID CreateLike();
        public abstract TID CreateNotBetween();
        public abstract TID CreateNotLike();
        public abstract TID CreatePredefinedFormatCondition(PredefinedFormatConditionType conditionType);
    }
}

