namespace DevExpress.Data
{
    using System;

    public class MailMergeFieldInfo
    {
        public const char FormatStringDelimiter = '!';
        public const char OpeningBracket = '[';
        public const char ClosingBracket = ']';
        private int startPosition;
        private int endPosition;
        private string fieldName;
        private string displayName;
        private string rawFormatString;
        private string dataMember;

        public MailMergeFieldInfo();
        public MailMergeFieldInfo(MailMergeFieldInfo mailMergeFieldInfo);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public static string MakeFormatString(string str);
        public override string ToString();
        public static string WrapColumnInfoInBrackets(string columnName);
        public static string WrapColumnInfoInBrackets(string columnName, string formatString);

        public int StartPosition { get; set; }

        public int EndPosition { get; set; }

        public string FieldName { get; set; }

        public string TrimmedFieldName { get; }

        public string DisplayName { get; set; }

        public string FormatString { get; }

        public string DataMember { get; set; }

        public bool HasFormatStringInfo { get; }
    }
}

