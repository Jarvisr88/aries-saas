namespace DevExpress.Utils.OAuth.Provider
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Response
    {
        private int _statusCode;
        private string _contentType;
        private string _content;
        public Response(int statusCode, string content, string contentType)
        {
            this._content = content;
            this._contentType = contentType;
            this._statusCode = statusCode;
        }

        public int StatusCode =>
            (this._statusCode > 0) ? this._statusCode : 500;
        public string ContentType =>
            !string.IsNullOrEmpty(this._contentType) ? this._contentType : string.Empty;
        public string Content =>
            !string.IsNullOrEmpty(this._content) ? this._content : string.Empty;
    }
}

