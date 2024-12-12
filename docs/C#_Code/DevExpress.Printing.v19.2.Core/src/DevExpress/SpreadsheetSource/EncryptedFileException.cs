namespace DevExpress.SpreadsheetSource
{
    using System;
    using System.Runtime.CompilerServices;

    public class EncryptedFileException : Exception
    {
        public EncryptedFileException(EncryptedFileError error, string message) : base(message)
        {
            this.Error = error;
        }

        public EncryptedFileError Error { get; private set; }
    }
}

