namespace DevExpress.Data.Filtering
{
    using System;

    public interface ICustomAggregate
    {
        object CreateEvaluationContext();
        object GetResult(object context);
        bool Process(object context, object[] operands);
        Type ResultType(params Type[] operands);

        string Name { get; }
    }
}

