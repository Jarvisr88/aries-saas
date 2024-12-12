namespace DevExpress.Office.Services.Implementation
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;

    public class WebUriStreamCreator
    {
        private WebRequest request;
        private MemoryStream stream;
        private ManualResetEvent complete;
        private byte[] buffer;
        private Stream responseStream;
        private long responseContentLength;

        public WebUriStreamCreator(WebRequest request)
        {
            this.request = request;
        }

        public Stream CreateStream()
        {
            try
            {
                this.complete = new ManualResetEvent(false);
                IAsyncResult result = this.request.BeginGetResponse(new AsyncCallback(this.ResponseCallback), null);
                RegisteredWaitHandle handle = ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(this.OnWaitTimeout), null, 0x7530, true);
                this.complete.WaitOne();
                handle.Unregister(result.AsyncWaitHandle);
                this.complete.Close();
                return this.stream;
            }
            catch
            {
                return null;
            }
        }

        protected internal virtual void OnChunkRead(IAsyncResult result)
        {
            try
            {
                int count = this.responseStream.EndRead(result);
                if (count <= 0)
                {
                    this.responseStream.Close();
                    this.responseStream = null;
                    this.stream.Seek(0L, SeekOrigin.Begin);
                    this.complete.Set();
                }
                else
                {
                    this.stream.Write(this.buffer, 0, count);
                    if ((this.responseContentLength <= 0L) || (this.stream.Length < this.responseContentLength))
                    {
                        this.responseStream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(this.OnChunkRead), null);
                    }
                    else
                    {
                        this.responseStream.Close();
                        this.responseStream = null;
                        this.stream.Seek(0L, SeekOrigin.Begin);
                        this.complete.Set();
                    }
                }
            }
            catch
            {
                try
                {
                    this.complete.Set();
                }
                catch
                {
                }
            }
        }

        protected internal virtual void OnWaitTimeout(object state, bool timedOut)
        {
            try
            {
                if (timedOut)
                {
                    this.request.Abort();
                    this.complete.Set();
                }
            }
            catch
            {
            }
        }

        protected internal virtual void ResponseCallback(IAsyncResult result)
        {
            try
            {
                WebResponse response = this.request.EndGetResponse(result);
                this.responseContentLength = response.ContentLength;
                this.responseStream = response.GetResponseStream();
                this.buffer = new byte[0x1000];
                this.stream = new MemoryStream();
                this.responseStream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(this.OnChunkRead), null);
            }
            catch
            {
                try
                {
                    this.complete.Set();
                }
                catch
                {
                }
            }
        }
    }
}

