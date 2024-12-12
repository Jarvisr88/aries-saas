namespace DevExpress.SpreadsheetSource
{
    using System;
    using System.Runtime.CompilerServices;

    public class InvalidFileException : Exception
    {
        public InvalidFileException(InvalidFileError error, string message) : base(message)
        {
            this.Error = error;
        }

        public InvalidFileError Error { get; private set; }
    }
}

