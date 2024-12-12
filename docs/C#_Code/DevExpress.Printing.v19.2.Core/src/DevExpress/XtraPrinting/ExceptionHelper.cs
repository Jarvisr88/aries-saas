namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.IO;

    public static class ExceptionHelper
    {
        public static Exception CreateFriendlyException(FileNotFoundException ex)
        {
            object[] args = new object[] { ex.FileName };
            return new Exception(PreviewStringId.Msg_CannotFindFile.GetString(args), ex);
        }

        public static Exception CreateFriendlyException(OutOfMemoryException ex) => 
            new Exception(PreviewStringId.Msg_BigFileToCreate.GetString(), ex);

        public static Exception CreateFriendlyException(IOException ex, string fileName)
        {
            object[] args = new object[] { fileName };
            return new Exception(PreviewStringId.Msg_CannotAccessFile.GetString(args), ex);
        }

        public static void ThrowInvalidOperationException()
        {
            ThrowInvalidOperationException<object>(string.Empty);
        }

        public static T ThrowInvalidOperationException<T>() => 
            ThrowInvalidOperationException<T>(string.Empty);

        public static void ThrowInvalidOperationException(string message)
        {
            ThrowInvalidOperationException<object>(message);
        }

        public static T ThrowInvalidOperationException<T>(string message)
        {
            throw new InvalidOperationException(message);
        }
    }
}

