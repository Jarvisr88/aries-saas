namespace DevExpress.Utils.IoC
{
    using System;

    public class ResolutionFailedException : Exception
    {
        public ResolutionFailedException()
        {
        }

        public ResolutionFailedException(string message) : base(message)
        {
        }

        public ResolutionFailedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

