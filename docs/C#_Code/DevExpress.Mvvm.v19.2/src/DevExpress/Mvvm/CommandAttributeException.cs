namespace DevExpress.Mvvm
{
    using System;

    [Serializable]
    public class CommandAttributeException : Exception
    {
        public CommandAttributeException()
        {
        }

        public CommandAttributeException(string message) : base(message)
        {
        }
    }
}

