namespace DevExpress.XtraPrinting.Export.Text
{
    using System;

    public class TextException : Exception
    {
        public TextException()
        {
        }

        public TextException(string message) : base(message)
        {
        }

        public TextException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}

