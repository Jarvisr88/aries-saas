namespace Devart.Common
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ProxyException : Exception
    {
        public ProxyException(Exception inner) : base(inner.Message)
        {
        }

        public ProxyException(string message) : base(message)
        {
        }

        protected ProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

