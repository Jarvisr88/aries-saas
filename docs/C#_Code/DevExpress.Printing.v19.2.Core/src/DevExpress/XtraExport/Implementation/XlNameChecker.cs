namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Utils;
    using System;

    internal static class XlNameChecker
    {
        public static bool IsValidIdentifier(string value)
        {
            Guard.ArgumentIsNotNullOrEmpty(value, "value");
            for (int i = 0; i < value.Length; i++)
            {
                if (!IsValidIdentifierChar(value[i], i, value))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsValidIdentifierChar(char curChar, int index, string value)
        {
            if (index == 0)
            {
                if (curChar != '\\')
                {
                    if (!char.IsLetter(curChar) && (curChar != '_'))
                    {
                        return false;
                    }
                }
                else
                {
                    if (value.Length == 1)
                    {
                        return true;
                    }
                    if ((value[1] != '\\') && ((value[1] != '.') && ((value[1] != '?') && (value[1] != '_'))))
                    {
                        return false;
                    }
                }
            }
            return (char.IsLetterOrDigit(curChar) || ((curChar == '_') || ((curChar == '.') || ((curChar == '\\') || (curChar == '?')))));
        }
    }
}

