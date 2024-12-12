namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct AsyncOperationIdentifier
    {
        private static int counter;
        private int operationId;
        private bool isFromThread;
        public static readonly AsyncOperationIdentifier Empty;
        public static AsyncOperationIdentifier New();
        public static AsyncOperationIdentifier FromCurrentThread();
        public override bool Equals(object obj);
        public override int GetHashCode();
        public static bool operator ==(AsyncOperationIdentifier obj1, AsyncOperationIdentifier obj2);
        public static bool operator !=(AsyncOperationIdentifier obj1, AsyncOperationIdentifier obj2);
        public bool IsEmpty { get; }
        static AsyncOperationIdentifier();
        public class EqualityComparer : IEqualityComparer<AsyncOperationIdentifier>
        {
            public bool Equals(AsyncOperationIdentifier x, AsyncOperationIdentifier y);
            public int GetHashCode(AsyncOperationIdentifier obj);
        }
    }
}

