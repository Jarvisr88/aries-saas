namespace DevExpress.Utils.About
{
    using System;
    using System.IO;
    using System.Threading;

    internal class UAlgoPost
    {
        private Thread postThread;
        private string path;
        private Guid id;
        private int version;
        private static string url = "http://uxlog.devexpress.com?id={0}&version={1}&proto=2";
        internal bool disabled;

        public UAlgoPost(string path, Guid id, int version)
        {
            this.path = path;
            this.id = id;
            this.version = version;
        }

        protected void OnStart()
        {
            try
            {
                this.ProcessData();
            }
            catch
            {
            }
            this.postThread = null;
        }

        public bool Post()
        {
            if (this.postThread != null)
            {
                return false;
            }
            this.postThread = new Thread(new ThreadStart(this.OnStart));
            this.postThread.Start();
            return true;
        }

        private void ProcessData()
        {
            if (File.Exists(this.path))
            {
                byte[] buffer = null;
                using (FileStream stream = new FileStream(this.path, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[stream.Length];
                    if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                    {
                        return;
                    }
                }
                XmlHttpRequest.XmlHttpResponse response = XmlHttpRequest.Submit(string.Format(url, this.id.ToString().Replace("-", ""), this.version), "POST", null, buffer);
                if (response.Content == null)
                {
                    File.Delete(this.path);
                }
                if (response.ContentText == "DISABLEDX")
                {
                    this.disabled = true;
                }
            }
        }

        public bool IsWorking =>
            this.postThread != null;
    }
}

