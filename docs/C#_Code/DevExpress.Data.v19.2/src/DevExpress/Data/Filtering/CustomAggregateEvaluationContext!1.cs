namespace DevExpress.Data.Filtering
{
    using System;

    public class CustomAggregateEvaluationContext<T>
    {
        private T valueHolder;

        public T ProcessValue(Func<T, T> processFunction);

        public T Value { get; set; }
    }
}

