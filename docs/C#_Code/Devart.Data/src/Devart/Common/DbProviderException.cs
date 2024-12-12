namespace Devart.Common
{
    using System;

    public class DbProviderException : Exception
    {
        protected DbProviderException()
        {
        }

        protected DbProviderException(string message) : base(message)
        {
        }

        protected DbProviderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override string ToString() => 
            base.Message;
    }
}

