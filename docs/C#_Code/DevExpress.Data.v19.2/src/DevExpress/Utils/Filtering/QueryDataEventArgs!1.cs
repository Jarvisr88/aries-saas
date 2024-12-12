namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class QueryDataEventArgs<TData> : EventArgs where TData: MetricAttributesData
    {
        private readonly TData resultCore;

        protected QueryDataEventArgs(string propertyPath, IDictionary<string, object> memberValues)
        {
            this.PropertyPath = propertyPath;
            this.resultCore = this.CreateData(memberValues);
        }

        protected abstract TData CreateData(IDictionary<string, object> memberValues);

        public string PropertyPath { get; private set; }

        public TData Result =>
            this.resultCore;
    }
}

