namespace DevExpress.Utils.IoC
{
    using System;

    public class RegistrationFailedException : Exception
    {
        public RegistrationFailedException()
        {
        }

        public RegistrationFailedException(string message) : base(message)
        {
        }

        public RegistrationFailedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

