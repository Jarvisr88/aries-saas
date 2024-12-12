namespace DevExpress.Data.Filtering
{
    using System;

    public class BaseNodeEventArgs : EventArgs
    {
        private readonly string propertyName;
        private readonly Type propertyType;

        public BaseNodeEventArgs(string propertyName, Type propertyType);

        public string PropertyName { get; }

        public Type PropertyType { get; }
    }
}

