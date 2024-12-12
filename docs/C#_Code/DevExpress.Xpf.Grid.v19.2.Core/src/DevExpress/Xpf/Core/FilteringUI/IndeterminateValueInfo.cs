namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    internal class IndeterminateValueInfo
    {
        public readonly object Value;
        public readonly CheckedValuesInfo Children;
        public readonly string PropertyName;

        public IndeterminateValueInfo(object value, CheckedValuesInfo children, string propertyName)
        {
            this.Value = value;
            this.Children = children;
            this.PropertyName = propertyName;
        }
    }
}

