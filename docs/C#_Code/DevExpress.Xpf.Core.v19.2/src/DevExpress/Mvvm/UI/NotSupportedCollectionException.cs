namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class NotSupportedCollectionException : Exception
    {
        public NotSupportedCollectionException(Type collectionType, string message = null, Exception innerException = null) : base(message, innerException)
        {
            this.CollectionType = collectionType;
        }

        public Type CollectionType { get; private set; }
    }
}

