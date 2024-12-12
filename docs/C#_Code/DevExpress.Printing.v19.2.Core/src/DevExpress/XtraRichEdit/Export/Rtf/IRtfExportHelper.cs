namespace DevExpress.XtraRichEdit.Export.Rtf
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public interface IRtfExportHelper
    {
        int GetColorIndex(Color backColor);
        int GetColorIndex(int backColorIndex, Func<int, Color> getColor);
        int GetFontNameIndex(string name, RtfThemeFontType themeFontType);

        int DefaultFontIndex { get; }

        Dictionary<int, string> ListCollection { get; }

        Dictionary<int, int> ListOverrideCollectionIndex { get; }

        Dictionary<string, int> ParagraphStylesCollectionIndex { get; }

        Dictionary<string, int> CharacterStylesCollectionIndex { get; }

        Dictionary<string, int> TableStylesCollectionIndex { get; }

        Dictionary<int, int> FontCharsetTable { get; }

        List<string> ListOverrideCollection { get; }

        List<string> StylesCollection { get; }

        string DefaultCharacterProperties { get; set; }

        string DefaultCharacterPropertiesStSh { get; set; }

        string DefaultParagraphProperties { get; set; }

        bool SupportStyle { get; }
    }
}

