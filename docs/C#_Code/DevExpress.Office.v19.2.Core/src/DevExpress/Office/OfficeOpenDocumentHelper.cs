namespace DevExpress.Office
{
    using System;
    using System.Xml;

    public class OfficeOpenDocumentHelper
    {
        private const string OpenDocumentVersion = "1.0";
        public const string DrawNsPrefix = "draw";
        public const string FoNsPrefix = "fo";
        public const string ManifestNsPrefix = "manifest";
        public const string OfficeNsPrefix = "office";
        public const string StyleNsPrefix = "style";
        public const string SvgNsPrefix = "svg";
        public const string TextNsPrefix = "text";
        public const string XlinkNsPrefix = "xlink";
        public const string TableNsPrefix = "table";
        public const string DcNsPrefix = "dc";
        public const string OOWNsPrefix = "oow";
        public const string ElementNs = "xmlns";
        private const string OpenOfficeRootNS = "http://openoffice.org/";
        private const string W3OrgRootNs = "http://www.w3.org/";
        public const string OpenDocumentRootNs = "urn:oasis:names:tc:opendocument:xmlns:";
        public const string OfficeNamespace = "urn:oasis:names:tc:opendocument:xmlns:office:1.0";
        public const string TextNamespace = "urn:oasis:names:tc:opendocument:xmlns:text:1.0";
        public const string StyleNamespace = "urn:oasis:names:tc:opendocument:xmlns:style:1.0";
        public const string ManifestNamespace = "urn:oasis:names:tc:opendocument:xmlns:manifest:1.0";
        public const string FoNamespace = "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0";
        public const string SvgNamespace = "urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0";
        public const string OOWNamespace = "http://openoffice.org/2004/writer";
        public const string DrawNamespace = "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0";
        public const string DcNamespace = "http://purl.org/dc/elements/1.1/";
        public const string XlinkNamespace = "http://www.w3.org/1999/xlink";
        public const string TableNamespace = "urn:oasis:names:tc:opendocument:xmlns:table:1.0";
        public const string ElementDocument = "document";
        public const string ElementDocumentContent = "document-content";
        public const string ElementDocumentStyles = "document-styles";
        public const string ElementDocumentMeta = "document-meta";
        public const string ManifestMediaType = "application/vnd.oasis.opendocument.text";
        private static readonly TranslationTable<OfficeParagraphAlignment> paragraphAlignmentTable = CreateOfficeParagraphAlignmentTable();
        private static readonly TranslationTable<bool> fontStyleTable = CreateFontStyleTable();
        private static readonly TranslationTable<bool> fontBoldTable = CreateFontBoldTable();
        private static readonly TranslationTable<bool> fontCaseTable = CreateFontCaseTable();
        private static readonly TranslationTable<bool> fontVariantTable = CreateFontVariantTable();
        private static readonly TranslationTable<char> listNumberSeparatorTable = CreateListNumberSeparatorTable();
        private static readonly TranslationTable<bool> columnSeparatorStyleTable = CreateColumnSeparatorStyleTable();
        private static readonly TranslationTable<bool> fontUnderLineWidthTable = CreateUnderLineBoldTable();
        private static readonly TranslationTable<CharacterFormattingScript> characterScriptTable = CreateCharacterScriptTable();
        private static readonly TranslationTable<OpenDocumentTableAlignment> tableRowAlignmentTable = CreateTableRowAlignmentTable();
        private static readonly TranslationTable<OpenDocumentVerticalAlignment> verticalAlignmentTable = CreateVerticalAlignmentTable();
        private static readonly TranslationTable<TextDirection> textDirectionTable = CreateTextDirectionTable();

        public OfficeOpenDocumentHelper(bool val)
        {
        }

        private static TranslationTable<CharacterFormattingScript> CreateCharacterScriptTable()
        {
            TranslationTable<CharacterFormattingScript> table = new TranslationTable<CharacterFormattingScript>();
            table.Add(CharacterFormattingScript.Normal, "0% 100%");
            table.Add(CharacterFormattingScript.Subscript, "sub");
            table.Add(CharacterFormattingScript.Superscript, "super");
            return table;
        }

        private static TranslationTable<bool> CreateColumnSeparatorStyleTable()
        {
            TranslationTable<bool> table = new TranslationTable<bool>();
            table.Add(false, "none");
            table.Add(true, "solid");
            table.Add(true, "dotted");
            table.Add(true, "dashed");
            table.Add(true, "dot-dashed");
            return table;
        }

        private static TranslationTable<bool> CreateFontBoldTable()
        {
            TranslationTable<bool> table = new TranslationTable<bool>();
            table.Add(false, "normal");
            table.Add(true, "bold");
            table.Add(true, "100");
            table.Add(true, "200");
            table.Add(true, "300");
            table.Add(true, "400");
            table.Add(true, "500");
            table.Add(true, "600");
            table.Add(true, "700");
            table.Add(true, "800");
            table.Add(true, "900");
            return table;
        }

        private static TranslationTable<bool> CreateFontCaseTable()
        {
            TranslationTable<bool> table = new TranslationTable<bool>();
            table.Add(false, "none");
            table.Add(true, "uppercase");
            return table;
        }

        private static TranslationTable<bool> CreateFontStyleTable()
        {
            TranslationTable<bool> table = new TranslationTable<bool>();
            table.Add(false, "normal");
            table.Add(true, "italic");
            table.Add(true, "oblique");
            return table;
        }

        private static TranslationTable<bool> CreateFontVariantTable()
        {
            TranslationTable<bool> table = new TranslationTable<bool>();
            table.Add(false, "normal");
            table.Add(true, "small-caps");
            return table;
        }

        private static TranslationTable<char> CreateListNumberSeparatorTable()
        {
            TranslationTable<char> table = new TranslationTable<char>();
            table.Add('\t', "listtab");
            table.Add(' ', "space");
            table.Add('\0', "nothing");
            return table;
        }

        private static TranslationTable<OfficeParagraphAlignment> CreateOfficeParagraphAlignmentTable()
        {
            TranslationTable<OfficeParagraphAlignment> table = new TranslationTable<OfficeParagraphAlignment>();
            table.Add(OfficeParagraphAlignment.Left, "left");
            table.Add(OfficeParagraphAlignment.Right, "right");
            table.Add(OfficeParagraphAlignment.Center, "center");
            table.Add(OfficeParagraphAlignment.Justify, "justify");
            table.Add(OfficeParagraphAlignment.Left, "start");
            table.Add(OfficeParagraphAlignment.Right, "end");
            return table;
        }

        private static TranslationTable<OpenDocumentTableAlignment> CreateTableRowAlignmentTable()
        {
            TranslationTable<OpenDocumentTableAlignment> table = new TranslationTable<OpenDocumentTableAlignment>();
            table.Add(OpenDocumentTableAlignment.Left, "left");
            table.Add(OpenDocumentTableAlignment.Right, "right");
            table.Add(OpenDocumentTableAlignment.Center, "center");
            table.Add(OpenDocumentTableAlignment.Both, "margins");
            return table;
        }

        private static TranslationTable<TextDirection> CreateTextDirectionTable()
        {
            TranslationTable<TextDirection> table = new TranslationTable<TextDirection>();
            table.Add(TextDirection.TopToBottomRightToLeft, "tb-rl");
            table.Add(TextDirection.LeftToRightTopToBottom, "lr");
            return table;
        }

        private static TranslationTable<bool> CreateUnderLineBoldTable()
        {
            TranslationTable<bool> table = new TranslationTable<bool>();
            table.Add(false, "auto");
            table.Add(true, "bold");
            table.Add(true, "thick");
            table.Add(true, "medium");
            return table;
        }

        private static TranslationTable<OpenDocumentVerticalAlignment> CreateVerticalAlignmentTable()
        {
            TranslationTable<OpenDocumentVerticalAlignment> table = new TranslationTable<OpenDocumentVerticalAlignment>();
            table.Add(OpenDocumentVerticalAlignment.Bottom, "bottom");
            table.Add(OpenDocumentVerticalAlignment.Center, "middle");
            table.Add(OpenDocumentVerticalAlignment.Top, "top");
            return table;
        }

        public static T GetEnumValue<T>(XmlReader reader, string attributeName, string ns, TranslationTable<T> table, T defaultValue) where T: struct
        {
            string attribute = reader.GetAttribute(attributeName, ns);
            return (!string.IsNullOrEmpty(attribute) ? table.GetEnumValue(attribute, defaultValue) : defaultValue);
        }

        public static T GetFoEnumValue<T>(XmlReader reader, string attributeName, TranslationTable<T> table, T defaultValue) where T: struct => 
            GetEnumValue<T>(reader, attributeName, "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0", table, defaultValue);

        public static T GetStyleEnumValue<T>(XmlReader reader, string attributeName, TranslationTable<T> table, T defaultValue) where T: struct => 
            GetEnumValue<T>(reader, attributeName, "urn:oasis:names:tc:opendocument:xmlns:style:1.0", table, defaultValue);

        public static TranslationTable<OfficeParagraphAlignment> ParagraphAlignmentTable =>
            paragraphAlignmentTable;

        public static TranslationTable<bool> FontStyleTable =>
            fontStyleTable;

        public static TranslationTable<bool> FontBoldTable =>
            fontBoldTable;

        public static TranslationTable<bool> FontCaseTable =>
            fontCaseTable;

        public static TranslationTable<bool> FontVariantTable =>
            fontVariantTable;

        public static TranslationTable<bool> ColumnSeparatorStyleTable =>
            columnSeparatorStyleTable;

        public static TranslationTable<char> ListNumberSeparatorTable =>
            listNumberSeparatorTable;

        public static TranslationTable<bool> FontUnderLineWidthTable =>
            fontUnderLineWidthTable;

        public static TranslationTable<CharacterFormattingScript> CharacterScriptTable =>
            characterScriptTable;

        public static TranslationTable<TextDirection> TextDirectionTable =>
            textDirectionTable;

        public static TranslationTable<OpenDocumentVerticalAlignment> VerticalAlignmentTable =>
            verticalAlignmentTable;

        public static TranslationTable<OpenDocumentTableAlignment> TableRowAlignmentTable =>
            tableRowAlignmentTable;
    }
}

