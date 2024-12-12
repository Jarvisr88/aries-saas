namespace DevExpress.Printing.Native.StreamingPagination
{
    using System;

    internal class BucketQueueException : Exception
    {
        public BucketQueueException()
        {
        }

        public BucketQueueException(string message) : base(message)
        {
        }
    }
}

