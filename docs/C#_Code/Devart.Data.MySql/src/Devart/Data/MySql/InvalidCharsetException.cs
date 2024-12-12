namespace Devart.Data.MySql
{
    using System;

    public class InvalidCharsetException : MySqlDbProviderException
    {
        private readonly string a;
        private readonly string b;

        internal InvalidCharsetException(string A_0);
        internal InvalidCharsetException(string A_0, Exception A_1);
        internal InvalidCharsetException(string A_0, string A_1, string A_2);
        internal InvalidCharsetException(string A_0, string A_1, string A_2, Exception A_3);

        public string ClientCharset { get; }

        public string ServerCharset { get; }
    }
}

