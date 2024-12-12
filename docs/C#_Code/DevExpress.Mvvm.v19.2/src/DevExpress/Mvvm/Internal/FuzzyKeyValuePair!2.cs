namespace DevExpress.Mvvm.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public class FuzzyKeyValuePair<TKey, TValue>
    {
        public FuzzyKeyValuePair(TKey key, TValue value, bool useIncludeCondition)
        {
            this.Key = key;
            this.Value = value;
            this.UseIncludeCondition = useIncludeCondition;
        }

        public TKey Key { get; private set; }

        public TValue Value { get; private set; }

        public bool UseIncludeCondition { get; private set; }
    }
}

