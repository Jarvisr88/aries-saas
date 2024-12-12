namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class ObservablePropertySchemeNode
    {
        private readonly string propertyName;
        private readonly ObservablePropertySchemeNode[] children;

        public ObservablePropertySchemeNode(string propertyName);
        public ObservablePropertySchemeNode(string propertyName, ObservablePropertySchemeNode[] children);

        public string PropertyName { get; }

        public ObservablePropertySchemeNode[] Children { get; }
    }
}

