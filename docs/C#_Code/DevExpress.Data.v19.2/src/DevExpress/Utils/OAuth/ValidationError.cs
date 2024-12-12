namespace DevExpress.Utils.OAuth
{
    using System;

    public class ValidationError
    {
        private int _statusCode;
        private string _message;

        public ValidationError(int statusCode, string message)
        {
            this._message = message;
            this._statusCode = statusCode;
        }

        public int StatusCode =>
            this._statusCode;

        public string Message =>
            this._message;
    }
}

