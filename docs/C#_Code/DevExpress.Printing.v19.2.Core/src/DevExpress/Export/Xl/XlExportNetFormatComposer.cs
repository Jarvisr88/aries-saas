namespace DevExpress.Export.Xl
{
    using System;
    using System.Text;

    public static class XlExportNetFormatComposer
    {
        private const string leadingBrace = "{";
        private const string trailingBrace = "}";
        private const string escapedLeadingBrace = "{{";
        private const string escapedTrailingBrace = "}}";

        public static string CreateFormat(string formatString) => 
            CreateFormat(string.Empty, formatString, string.Empty);

        public static string CreateFormat(string prefix, string formatString, string postfix)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix))
            {
                builder.Append(prefix.Replace("{", "{{").Replace("}", "}}"));
            }
            builder.Append("{");
            builder.Append("0");
            if (!string.IsNullOrEmpty(formatString))
            {
                builder.Append(":");
                builder.Append(formatString);
            }
            builder.Append("}");
            if (!string.IsNullOrEmpty(postfix))
            {
                builder.Append(postfix.Replace("{", "{{").Replace("}", "}}"));
            }
            return builder.ToString();
        }
    }
}

