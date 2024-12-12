namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Import.OpenXml;
    using DevExpress.Office.Model;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Xml;

    public abstract class ThemesBaseExporter
    {
        private static readonly Dictionary<ThemeColorIndex, string> themeColorIndexTranslationTable = CreateThemeColorIndexTranslationTable();
        public const string DrawingMLNamespace = "http://schemas.openxmlformats.org/drawingml/2006/main";
        private IDocumentModel documentModel;
        private XmlWriter documentContentWriter;
        private IThemeOpenXmlExporter openXmlExporter;

        public ThemesBaseExporter(IDocumentModel documentModel, XmlWriter documentContentWriter, IThemeOpenXmlExporter openXmlExporter)
        {
            this.documentModel = documentModel;
            this.documentContentWriter = documentContentWriter;
            this.openXmlExporter = openXmlExporter;
        }

        private static Dictionary<ThemeColorIndex, string> CreateThemeColorIndexTranslationTable() => 
            new Dictionary<ThemeColorIndex, string> { 
                { 
                    ThemeColorIndex.Dark1,
                    "dk1"
                },
                { 
                    ThemeColorIndex.Dark2,
                    "dk2"
                },
                { 
                    ThemeColorIndex.Light1,
                    "lt1"
                },
                { 
                    ThemeColorIndex.Light2,
                    "lt2"
                },
                { 
                    ThemeColorIndex.Accent1,
                    "accent1"
                },
                { 
                    ThemeColorIndex.Accent2,
                    "accent2"
                },
                { 
                    ThemeColorIndex.Accent3,
                    "accent3"
                },
                { 
                    ThemeColorIndex.Accent4,
                    "accent4"
                },
                { 
                    ThemeColorIndex.Accent5,
                    "accent5"
                },
                { 
                    ThemeColorIndex.Accent6,
                    "accent6"
                },
                { 
                    ThemeColorIndex.Hyperlink,
                    "hlink"
                },
                { 
                    ThemeColorIndex.FollowedHyperlink,
                    "folHlink"
                }
            };

        private void ExportColorAttributes(string tag, Color color)
        {
            byte r = color.R;
            byte g = color.G;
            byte b = color.B;
            this.WriteStringValue(tag, $"{r:X2}" + $"{g:X2}" + $"{b:X2}");
        }

        public void ExportLastComputedColorAttribute(Color color)
        {
            this.ExportColorAttributes("lastClr", color);
        }

        public void ExportRgbColorAttributes(Color color)
        {
            this.ExportColorAttributes("val", color);
        }

        public void ExportSystemColorAttributes(SystemColorValues color)
        {
            string str;
            if (SystemColorDestination.systemColorTable.TryGetValue(color, out str))
            {
                this.WriteStringValue("val", str);
            }
        }

        public abstract void GenerateDrawingColorContent(DrawingColor color);
        private void GenerateDrawingListContent<T>(string tagName, List<T> list, Action<T> action)
        {
            this.WriteStartElement(tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    action(list[i]);
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        public void GenerateDrawingNamespaceAttributes()
        {
            this.WriteStringAttr("xmlns", "a", null, "http://schemas.openxmlformats.org/drawingml/2006/main");
        }

        protected internal void GenerateDrawingTextFontContent(DrawingTextFont textFont, string tagName)
        {
            this.WriteStartElement(tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                string typeface = textFont.Typeface;
                this.WriteStringValue("typeface", string.IsNullOrEmpty(typeface) ? string.Empty : typeface);
                string panose = textFont.Panose;
                this.WriteStringValue("panose", panose, !string.IsNullOrEmpty(panose) && (panose.Length == 20));
                byte pitchFamily = textFont.PitchFamily;
                this.WriteIntValue("pitchFamily", pitchFamily, 0);
                byte charset = textFont.Charset;
                this.WriteIntValue("charset", (sbyte) charset, 1);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void GenerateExtraClrScheme()
        {
        }

        protected internal virtual void GenerateObjectDefaults()
        {
        }

        private void GenerateSupplementalFontsContent(ThemeFontSchemePart scheme)
        {
            foreach (KeyValuePair<string, string> pair in scheme.SupplementalFonts)
            {
                this.GenerateThemeSupplementalFontContent(pair.Key, pair.Value);
            }
        }

        private void GenerateThemeColorSchemeContent(ThemeColorIndex index, DrawingColor color)
        {
            this.WriteStartElement(themeColorIndexTranslationTable[index], "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.GenerateDrawingColorContent(color);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void GenerateThemeColorSchemesContent()
        {
            this.WriteStartElement("clrScheme", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.WriteStringValue("name", this.DocumentModel.OfficeTheme.Colors.Name);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Dark1, this.DocumentModel.OfficeTheme.Colors.Dark1);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Light1, this.DocumentModel.OfficeTheme.Colors.Light1);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Dark2, this.DocumentModel.OfficeTheme.Colors.Dark2);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Light2, this.DocumentModel.OfficeTheme.Colors.Light2);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Accent1, this.DocumentModel.OfficeTheme.Colors.Accent1);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Accent2, this.DocumentModel.OfficeTheme.Colors.Accent2);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Accent3, this.DocumentModel.OfficeTheme.Colors.Accent3);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Accent4, this.DocumentModel.OfficeTheme.Colors.Accent4);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Accent5, this.DocumentModel.OfficeTheme.Colors.Accent5);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Accent6, this.DocumentModel.OfficeTheme.Colors.Accent6);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.Hyperlink, this.DocumentModel.OfficeTheme.Colors.Hyperlink);
                this.GenerateThemeColorSchemeContent(ThemeColorIndex.FollowedHyperlink, this.DocumentModel.OfficeTheme.Colors.FollowedHyperlink);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected void GenerateThemeElementsContent()
        {
            this.WriteStartElement("themeElements", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.GenerateThemeColorSchemesContent();
                this.GenerateThemeFontSchemesContent();
                this.GenerateThemeFormatSchemesContent();
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void GenerateThemeFontSchemePartContent(ThemeFontSchemePart scheme, string tagName)
        {
            this.WriteStartElement(tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.GenerateDrawingTextFontContent(scheme.Latin, "latin");
                this.GenerateDrawingTextFontContent(scheme.EastAsian, "ea");
                this.GenerateDrawingTextFontContent(scheme.ComplexScript, "cs");
                this.GenerateSupplementalFontsContent(scheme);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void GenerateThemeFontSchemesContent()
        {
            this.WriteStartElement("fontScheme", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.WriteStringValue("name", this.DocumentModel.OfficeTheme.FontScheme.Name);
                this.GenerateThemeFontSchemePartContent(this.DocumentModel.OfficeTheme.FontScheme.MajorFont, "majorFont");
                this.GenerateThemeFontSchemePartContent(this.DocumentModel.OfficeTheme.FontScheme.MinorFont, "minorFont");
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal abstract void GenerateThemeFormatSchemesContent();
        public virtual void GenerateThemesContent()
        {
            this.WriteStartElement("a", "theme", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.GenerateDrawingNamespaceAttributes();
                this.WriteStringValue("name", this.DocumentModel.OfficeTheme.Name);
                this.GenerateThemeElementsContent();
                this.GenerateObjectDefaults();
                this.GenerateExtraClrScheme();
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void GenerateThemeSupplementalFontContent(string script, string typeface)
        {
            this.WriteStartElement("font", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.WriteStringValue("script", script);
                this.WriteStringValue("typeface", typeface);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        public virtual void WriteEndElement()
        {
            this.documentContentWriter.WriteEndElement();
        }

        protected internal virtual void WriteIntValue(string tag, int value)
        {
            this.WriteStringAttr(null, tag, null, value.ToString(CultureInfo.InvariantCulture));
        }

        protected internal virtual void WriteIntValue(string attr, int value, bool shouldExport)
        {
            if (shouldExport)
            {
                this.WriteIntValue(attr, value);
            }
        }

        protected internal virtual void WriteIntValue(string attr, int value, int defaultValue)
        {
            this.WriteIntValue(attr, value, value != defaultValue);
        }

        protected internal virtual void WriteRaw(string data)
        {
            this.documentContentWriter.WriteRaw(data);
        }

        public virtual void WriteStartElement(string tag, string ns)
        {
            this.documentContentWriter.WriteStartElement(tag, ns);
        }

        protected internal virtual void WriteStartElement(string prefix, string tag, string ns)
        {
            this.documentContentWriter.WriteStartElement(prefix, tag, ns);
        }

        protected internal virtual void WriteString(string text)
        {
            this.documentContentWriter.WriteString(text);
        }

        protected internal virtual void WriteStringAttr(string prefix, string attr, string ns, string value)
        {
            this.documentContentWriter.WriteAttributeString(prefix, attr, ns, value);
        }

        protected internal virtual void WriteStringValue(string tag, string value)
        {
            this.WriteStringAttr(null, tag, null, value);
        }

        protected internal virtual void WriteStringValue(string attr, string value, bool shouldExport)
        {
            if (shouldExport)
            {
                this.WriteStringValue(attr, value);
            }
        }

        public IDocumentModel DocumentModel
        {
            get => 
                this.documentModel;
            set => 
                this.documentModel = value;
        }

        public IThemeOpenXmlExporter OpenXmlExporter
        {
            get => 
                this.openXmlExporter;
            set => 
                this.openXmlExporter = value;
        }
    }
}

