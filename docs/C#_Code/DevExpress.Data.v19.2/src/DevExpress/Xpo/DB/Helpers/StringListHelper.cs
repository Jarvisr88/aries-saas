namespace DevExpress.Xpo.DB.Helpers
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;

    public sealed class StringListHelper
    {
        public static void AddFormat(StringCollection collection, string s, params object[] parameters)
        {
            collection.Add(string.Format(CultureInfo.InvariantCulture, s, parameters));
        }

        public static StringCollection CreateStringCollection(params string[] strings)
        {
            StringCollection strings2 = new StringCollection();
            strings2.AddRange(strings);
            return strings2;
        }

        public static string DelimitedText(StringCollection collection, string delimiter)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < collection.Count; i++)
            {
                builder.Append(collection[i]);
                if (i != (collection.Count - 1))
                {
                    builder.Append(delimiter);
                }
            }
            return builder.ToString();
        }
    }
}

