namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomAggregateFormattable : ICustomAggregate
    {
        string Format(Type providerType, params string[] operands);
    }
}

