namespace DevExpress.Utils.OAuth
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;

    public class ConsumerOperation : IAsyncResult
    {
        private Exception _errorInfo;
        private HttpWebRequest _request;
        private System.AsyncCallback _asyncCallback;
        private Consumer _owner;
        private bool _unescape;
        private string _version;
        private bool _endCalled;
        private byte[] _response;
        private object _asyncState;
        private bool _synchronously;
        private int _isCompleted;

        public ConsumerOperation(HttpWebRequest request, Consumer owner, string version, bool unescape, System.AsyncCallback callback, object state)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "request is null.");
            }
            this._request = request;
            this._asyncCallback = callback;
            this._asyncState = state;
            this._owner = owner;
            this._version = version;
            this._unescape = unescape;
        }

        private void Complete(bool synchronously)
        {
            try
            {
                this._synchronously = synchronously;
                System.AsyncCallback asyncCallback = this.AsyncCallback;
                if (asyncCallback != null)
                {
                    asyncCallback(this);
                }
            }
            finally
            {
                this.Finish();
            }
        }

        public void Complete(byte[] response, bool synchronously)
        {
            if (Interlocked.Exchange(ref this._isCompleted, 0x7fffffff) == 0)
            {
                this._response = response;
                this.Complete(synchronously);
            }
        }

        public void CompleteWithError(Exception exception, bool synchronously)
        {
            if ((exception is OutOfMemoryException) || ((exception is StackOverflowException) || (exception is ThreadAbortException)))
            {
                throw exception;
            }
            if (Interlocked.Exchange(ref this._isCompleted, 0x7fffffff) == 0)
            {
                this._errorInfo = exception;
                this.Complete(synchronously);
            }
        }

        public byte[] End(object owner)
        {
            bool flag = true;
            if (owner == null)
            {
                if (this._owner != null)
                {
                    flag = false;
                }
            }
            else if (this._owner == null)
            {
                flag = false;
            }
            else if (owner != this._owner)
            {
                flag = false;
            }
            if (!flag)
            {
                throw new InvalidOperationException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.");
            }
            if (!this.IsCompleted)
            {
                throw new InvalidOperationException("Method can only be called when an asynchronous operation is completed.");
            }
            if (this.EndCalled)
            {
                throw new InvalidOperationException("Method can only be called once for each asynchronous operation.");
            }
            this._endCalled = true;
            Exception errorInfo = this.ErrorInfo;
            if (errorInfo != null)
            {
                throw errorInfo;
            }
            return this._response;
        }

        protected virtual void Finish()
        {
        }

        public static byte[] GetResponseBytes(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                byte[] buffer = null;
                GetResponseBytes(stream, ref buffer);
                return buffer;
            }
        }

        public static void GetResponseBytes(Stream source, ref byte[] buffer)
        {
            int num;
            byte[] buffer2 = new byte[0x1000];
            while ((num = source.Read(buffer2, 0, buffer2.Length)) > 0)
            {
                byte[] dst = new byte[num + (((buffer == null) || (buffer.Length == 0)) ? 0 : buffer.Length)];
                if ((buffer != null) && (buffer.Length != 0))
                {
                    Buffer.BlockCopy(buffer, 0, dst, 0, buffer.Length);
                }
                Buffer.BlockCopy(buffer2, 0, dst, dst.Length - num, num);
                buffer = dst;
            }
        }

        public Exception ErrorInfo =>
            this._errorInfo;

        public HttpWebRequest Request =>
            this._request;

        public System.AsyncCallback AsyncCallback =>
            this._asyncCallback;

        public Consumer Owner =>
            this._owner;

        public bool Unescape =>
            this._unescape;

        public string Version =>
            this._version;

        public bool EndCalled =>
            this._endCalled;

        public byte[] Response =>
            this._response;

        public object AsyncState =>
            this._asyncState;

        public virtual WaitHandle AsyncWaitHandle
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public bool CompletedSynchronously =>
            this._synchronously;

        public bool IsCompleted =>
            this._isCompleted != 0;
    }
}

