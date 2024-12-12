namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ILazyMetricAttributesFactory
    {
        IMetricAttributes CreateBooleanChoice(Type type);
        IMetricAttributes CreateEnumChoice(Type type, Type enumDataType);
        IMetricAttributes CreateGroup(Type type);
        IMetricAttributes CreateLookup(Type type);
        IMetricAttributes CreateRange(Type type);
    }
}

