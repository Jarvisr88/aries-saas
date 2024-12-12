namespace DevExpress.Export.Xl
{
    using System;

    internal class XlRangeReferenceParser
    {
        private static XlRangeReferenceParser instance = new XlRangeReferenceParser();

        internal static XlCellRange Parse(string reference) => 
            Parse(reference, true);

        internal static XlCellRange Parse(string reference, bool throwInvalidException) => 
            instance.ParseCore(reference, throwInvalidException);

        private XlCellRange ParseCore(string reference, bool throwInvalidException)
        {
            int length = reference.LastIndexOf('!');
            string sheetName = (length == -1) ? string.Empty : reference.Substring(0, length);
            if (sheetName.StartsWith("'"))
            {
                sheetName = sheetName.Remove(0, 1);
                sheetName = sheetName.Remove(sheetName.Length - 1, 1).Replace("''", "'");
            }
            char[] separator = new char[] { ':' };
            string[] strArray = reference.Substring(length + 1).Split(separator);
            XlCellPosition topLeft = XlCellReferenceParser.Parse(strArray[0], throwInvalidException);
            if (!topLeft.IsValid)
            {
                return null;
            }
            XlCellPosition bottomRight = (strArray.Length == 1) ? topLeft : XlCellReferenceParser.Parse(strArray[1], throwInvalidException);
            return (bottomRight.IsValid ? new XlCellRange(sheetName, topLeft, bottomRight) : null);
        }
    }
}

