namespace DevExpress.Data.Utils.ServiceModel
{
    using System;
    using System.ComponentModel;

    public class ScalarOperationCompletedEventArgs<T> : AsyncCompletedEventArgs
    {
        private object result;

        public ScalarOperationCompletedEventArgs(object result, Exception error, bool cancelled, object userState);

        public T Result { get; }
    }
}

