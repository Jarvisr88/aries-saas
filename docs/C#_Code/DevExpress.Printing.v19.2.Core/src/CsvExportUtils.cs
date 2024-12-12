using System;
using System.Globalization;

internal static class CsvExportUtils
{
    private static char[] possibleInjection = new char[] { '=', '@' };
    private static char[] possibleInjectionSigns = new char[] { '+', '-' };

    public static string Encode(string text, char textQualifier, CultureInfo culture)
    {
        bool flag = false;
        if (HasCsvInjection(text, culture))
        {
            text = textQualifier.ToString() + text + textQualifier.ToString();
            flag = true;
        }
        else if (HasCsvQuotedInjection(text, textQualifier, culture))
        {
            flag = true;
        }
        if (!flag)
        {
            return text;
        }
        string oldValue = new string(textQualifier, 1);
        return (textQualifier.ToString() + text.Replace(oldValue, new string(textQualifier, 2)) + textQualifier.ToString());
    }

    private static bool HasCsvInjection(string text, CultureInfo culture) => 
        !string.IsNullOrEmpty(text) && ((text.Length >= 2) && ((text.IndexOfAny(possibleInjection, 0, 1) < 0) ? HasExpression(text, culture) : true));

    private static bool HasCsvQuotedInjection(string text, char textQualifier, CultureInfo culture) => 
        !string.IsNullOrEmpty(text) && ((text.Length >= 4) && ((text[0] == textQualifier) && ((text.IndexOfAny(possibleInjection, 1, 1) < 0) ? HasExpression(text.Substring(1, text.Length - 2), culture) : true)));

    private static bool HasExpression(string text, CultureInfo culture)
    {
        double num;
        return (!string.IsNullOrEmpty(text) ? ((text.IndexOfAny(possibleInjectionSigns, 0, 1) >= 0) ? !double.TryParse(text, NumberStyles.Any, culture, out num) : false) : false);
    }
}

