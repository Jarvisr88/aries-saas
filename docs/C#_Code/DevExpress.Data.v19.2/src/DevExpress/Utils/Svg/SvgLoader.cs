namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class SvgLoader
    {
        private static void AddStyle(SvgElement element, SvgStyle style)
        {
            if (!element.Styles.Contains(style))
            {
                element.Styles.Push(style);
            }
        }

        private static void ApplyElementStyle(SvgElement element)
        {
            if ((element.Style != null) && (element.Style.Attributes.Count > 0))
            {
                AddStyle(element, element.Style);
            }
            foreach (SvgElement element2 in element.Elements)
            {
                ApplyParentStyle(element2, element.Style);
                ApplyElementStyle(element2);
            }
        }

        private static void ApplyParentProperties(SvgElement element, SvgElement parent)
        {
        }

        private static void ApplyParentStyle(SvgElement element, SvgStyle parentStyle)
        {
            if ((parentStyle != null) && (parentStyle.Attributes.Count > 0))
            {
                AddStyle(element, parentStyle);
                foreach (SvgElement element2 in element.Elements)
                {
                    ApplyParentStyle(element2, parentStyle);
                }
            }
        }

        private static void ApplyStyle(SvgElement element, SvgStyle style)
        {
            foreach (SvgElement element2 in element.Elements)
            {
                ApplyStyle(element2, style);
            }
            element.Styles.Push(style);
        }

        private static void ApplyStyles(List<SvgElement> elements, List<SvgStyle> styles)
        {
            if ((styles.Count != 0) && (elements.Count != 0))
            {
                foreach (SvgElement element in elements)
                {
                    foreach (SvgStyle style in styles)
                    {
                        if (element.StyleName.Equals(style.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            ApplyStyle(element, style);
                        }
                    }
                }
            }
        }

        private static SvgElement GetTopElement(Stack<SvgElement> elementStack)
        {
            SvgElement element = elementStack.Peek();
            if (element == null)
            {
                foreach (SvgElement element2 in elementStack)
                {
                    if (element2 != null)
                    {
                        element = element2;
                        break;
                    }
                }
            }
            return element;
        }

        public static SvgImage LoadFromFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return LoadFromStream(stream);
            }
        }

        public static SvgImage LoadFromStream(Stream stream) => 
            LoadFromStream(stream, null);

        public static SvgImage LoadFromStream(Stream stream, SvgImage image)
        {
            Action<XmlReaderSettings> settings = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Action<XmlReaderSettings> local1 = <>c.<>9__4_0;
                settings = <>c.<>9__4_0 = delegate (XmlReaderSettings settings) {
                    settings.DtdProcessing = DtdProcessing.Ignore;
                    settings.IgnoreWhitespace = true;
                };
            }
            using (XmlReader reader = SafeXml.CreateReader(stream, settings))
            {
                return ParseDocument(reader, image);
            }
        }

        public static SvgBitmap LoadSvgBitmapFromFile(string path) => 
            new SvgBitmap(LoadFromFile(path));

        public static SvgBitmap LoadSvgBitmapFromStream(Stream stream) => 
            new SvgBitmap(LoadFromStream(stream));

        public static SvgImage ParseDocument(XmlReader reader, SvgImage image = null)
        {
            SvgImage image2 = image ?? new SvgImage();
            List<SvgStyle> styles = image2.Styles;
            Stack<SvgElement> elementStack = new Stack<SvgElement>();
            List<SvgElement> elements = image2.Elements;
            List<SvgElement> list3 = new List<SvgElement>();
            List<string> unknownTags = image2.UnknownTags as List<string>;
            SvgElement styleElement = null;
            bool isEmptyElement = false;
            while (reader.Read())
            {
                try
                {
                    isEmptyElement = reader.IsEmptyElement;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        SvgElement parent = null;
                        if (elementStack.Count > 0)
                        {
                            parent = GetTopElement(elementStack);
                        }
                        styleElement = SvgElementCreator.CreateElement(reader, unknownTags);
                        if (styleElement is SvgStyleItem)
                        {
                            SvgStyleParser.ReadStyles(reader, styles, styleElement);
                        }
                        if (styleElement != null)
                        {
                            if ((styleElement is SvgRoot) && (image2.Root == null))
                            {
                                image2.SetRoot(styleElement as SvgRoot);
                            }
                            if (parent == null)
                            {
                                elements.Add(styleElement);
                            }
                            else
                            {
                                parent.AddElement(styleElement);
                                ApplyParentProperties(styleElement, parent);
                            }
                            list3.Add(styleElement);
                        }
                        if (!isEmptyElement)
                        {
                            elementStack.Push(styleElement);
                        }
                    }
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        SvgElement element3 = null;
                        if (elementStack.Count > 0)
                        {
                            element3 = elementStack.Peek();
                        }
                        if (element3 != null)
                        {
                            SvgContent element = new SvgContent();
                            element.Content = reader.Value;
                            element3.AddElement(element);
                        }
                    }
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        elementStack.Pop();
                    }
                }
                catch
                {
                }
            }
            ApplyStyles(list3, styles);
            ApplyElementStyle(image2.Root);
            return image2;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgLoader.<>c <>9 = new SvgLoader.<>c();
            public static Action<XmlReaderSettings> <>9__4_0;

            internal void <LoadFromStream>b__4_0(XmlReaderSettings settings)
            {
                settings.DtdProcessing = DtdProcessing.Ignore;
                settings.IgnoreWhitespace = true;
            }
        }
    }
}

