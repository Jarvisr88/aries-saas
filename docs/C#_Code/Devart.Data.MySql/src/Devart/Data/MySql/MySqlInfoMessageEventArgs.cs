namespace Devart.Data.MySql
{
    using System;

    public class MySqlInfoMessageEventArgs : EventArgs
    {
        private string message;
        private int code;

        public string Message { get; set; }

        public int Code { get; set; }
    }
}

