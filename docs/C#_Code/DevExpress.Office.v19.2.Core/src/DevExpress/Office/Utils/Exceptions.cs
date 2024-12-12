namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Localization;
    using System;

    public static class Exceptions
    {
        public static void ThrowArgumentException(string propName, object val)
        {
            string str = (val != null) ? val.ToString() : "null";
            throw new ArgumentException(string.Format(OfficeLocalizer.GetString(OfficeStringId.Msg_IsNotValid), str, propName));
        }

        public static void ThrowArgumentNullException(string propName)
        {
            throw new ArgumentNullException(propName);
        }

        public static void ThrowArgumentOutOfRangeException(OfficeStringId id, string parameterName)
        {
            ThrowArgumentOutOfRangeException(parameterName, OfficeLocalizer.GetString(id));
        }

        public static void ThrowArgumentOutOfRangeException(string parameterName, string message)
        {
            throw new ArgumentOutOfRangeException(parameterName, message);
        }

        public static void ThrowInternalException()
        {
            throw new Exception(OfficeLocalizer.GetString(OfficeStringId.Msg_InternalError));
        }

        public static void ThrowInvalidOperationException(OfficeStringId id)
        {
            throw new InvalidOperationException(OfficeLocalizer.GetString(id));
        }

        public static void ThrowInvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }

        public static void ThrowUnsupportedFormatOrCorruptedFileException()
        {
            throw new ArgumentException(OfficeLocalizer.GetString(OfficeStringId.Msg_UnsupportedFormatOrCorruptedFile));
        }
    }
}

