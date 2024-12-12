namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface ILocalaizableCriteriaToStringProcessorOpNamesSource
    {
        string GetBetweenString();
        string GetInString();
        string GetIsNotNullString();
        string GetIsNullString();
        string GetNotLikeString();
        string GetString(Aggregate opType);
        string GetString(BinaryOperatorType opType);
        string GetString(FunctionOperatorType opType);
        string GetString(GroupOperatorType opType);
        string GetString(UnaryOperatorType opType);
    }
}

