namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CustomFunctionEventArgs : BaseNodeEventArgs
    {
        protected internal readonly HashSet<string> customFunctions;
        private readonly Lazy<IEnumerable<string>> availableFunctions;
        protected internal readonly bool onlyUnary;

        public CustomFunctionEventArgs(string propertyName, Type propertyType, bool onlyUnary = false);
        public void Add(params string[] functionNames);
        private void ExcludeUnsupportedFunctions();
        public IReadOnlyCollection<string> GetFunctions();
    }
}

