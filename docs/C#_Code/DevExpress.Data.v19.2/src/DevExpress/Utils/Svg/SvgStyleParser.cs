namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public static class SvgStyleParser
    {
        private static bool CheckStyleNodeType(XmlReader reader, XmlNodeType type) => 
            (reader.NodeType == type) && reader.Name.ToLower().Equals("style");

        private static bool EndOfStyle(XmlReader reader) => 
            CheckStyleNodeType(reader, XmlNodeType.EndElement);

        private static void Parse(string text, List<SvgStyle> result)
        {
            char[] separator = new char[] { '}' };
            foreach (string str in text.Split(separator))
            {
                if (str.IndexOf('{') > -1)
                {
                    ParseStyleClass(str, result);
                }
            }
        }

        private static void ParseStyleAttributes(string text, SvgStyle style)
        {
            char[] separator = new char[] { ';' };
            foreach (string str in text.Split(separator))
            {
                if (str.Contains<char>(':'))
                {
                    char[] chArray2 = new char[] { ':' };
                    string[] strArray3 = str.Split(chArray2);
                    string key = strArray3[0].Trim();
                    style.SetValue(key, strArray3[1].Trim());
                }
            }
        }

        private static void ParseStyleClass(string text, List<SvgStyle> result)
        {
            char[] separator = new char[] { '{' };
            string[] strArray = text.Split(separator);
            if (strArray.Length >= 2)
            {
                SvgStyle style = new SvgStyle();
                string str = strArray[0].Trim().ToLower();
                style.Name = str.StartsWith(".") ? str.Substring(1) : str;
                ParseStyleAttributes(strArray[1], style);
                result.Add(style);
            }
        }

        public static SvgStyle ReadStyleAttribute(string text)
        {
            SvgStyle style = new SvgStyle();
            ParseStyleAttributes(text, style);
            return style;
        }

        public static bool ReadStyles(XmlReader reader, List<SvgStyle> result, SvgElement styleElement)
        {
            SvgStyleItem item = styleElement as SvgStyleItem;
            reader.Read();
            while (!EndOfStyle(reader))
            {
                if ((reader.NodeType == XmlNodeType.Text) || (reader.NodeType == XmlNodeType.CDATA))
                {
                    if ((item != null) && !string.IsNullOrEmpty(reader.Value))
                    {
                        SvgContent element = new SvgContent {
                            Content = reader.Value
                        };
                        styleElement.AddElement(element);
                    }
                    Parse(reader.Value, result);
                }
                reader.Read();
            }
            return true;
        }

        private static bool StartOfStyle(XmlReader reader) => 
            CheckStyleNodeType(reader, XmlNodeType.Element);
    }
}

