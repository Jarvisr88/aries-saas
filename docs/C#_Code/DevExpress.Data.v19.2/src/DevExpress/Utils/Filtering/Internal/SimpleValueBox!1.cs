namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public abstract class SimpleValueBox<T> : ValueViewModel, ISimpleValueViewModel<T>, IValueViewModel where T: struct
    {
        protected static readonly object valueKey;

        static SimpleValueBox();
        protected SimpleValueBox();
        protected override bool CanResetCore();
        protected void OnValueChanged();
        protected override void ResetCore();
        protected bool TryParseIsNull(string path, CriteriaOperator criteria);

        public virtual T? Value { get; set; }

        bool ISimpleValueViewModel<T>.AllowNull { get; }
    }
}

