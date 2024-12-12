namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomFunctionCollection : ICollection<ICustomFunctionOperator>, IEnumerable<ICustomFunctionOperator>, IEnumerable
    {
        private Dictionary<string, ICustomFunctionOperator> customFunctionByName;

        public CustomFunctionCollection();
        public void Add(ICustomFunctionOperator item);
        public void Add(IEnumerable<ICustomFunctionOperator> items);
        public void Clear();
        public bool Contains(ICustomFunctionOperator item);
        public void CopyTo(ICustomFunctionOperator[] array, int arrayIndex);
        public ICustomFunctionOperator GetCustomFunction(string functionName);
        public IEnumerator<ICustomFunctionOperator> GetEnumerator();
        public bool Remove(ICustomFunctionOperator item);
        IEnumerator IEnumerable.GetEnumerator();

        public int Count { get; }

        public bool IsReadOnly { get; }
    }
}

