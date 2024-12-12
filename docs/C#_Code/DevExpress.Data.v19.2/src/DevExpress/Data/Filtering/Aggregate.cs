namespace DevExpress.Data.Filtering
{
    using System;

    [Serializable]
    public enum Aggregate
    {
        public const Aggregate Exists = Aggregate.Exists;,
        public const Aggregate Count = Aggregate.Count;,
        public const Aggregate Max = Aggregate.Max;,
        public const Aggregate Min = Aggregate.Min;,
        public const Aggregate Avg = Aggregate.Avg;,
        public const Aggregate Sum = Aggregate.Sum;,
        public const Aggregate Single = Aggregate.Single;,
        public const Aggregate Custom = Aggregate.Custom;
    }
}

