namespace DevExpress.XtraPrinting.Export.Rtf
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraRichEdit.Export.Rtf;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class RtfExportHelper : IRtfExportHelper
    {
        private const string defaultFontName = "Times New Roman";
        private readonly DevExpress.XtraPrinting.Export.ColorCollection colorCollection = new DevExpress.XtraPrinting.Export.ColorCollection();
        private readonly StringCollection fontNamesCollection = new StringCollection();
        private readonly Dictionary<int, string> listCollection;
        private readonly Dictionary<int, int> listOverrideCollectionIndex;
        private readonly Dictionary<object, int> listCollectionIndex;
        private readonly List<string> listOverrideCollection;
        private readonly Dictionary<string, int> paragraphStylesCollectionIndex;
        private readonly Dictionary<string, int> characterStylesCollectionIndex;
        private readonly Dictionary<string, int> tableStylesCollectionIndex;
        private readonly Dictionary<int, int> fontCharsetTable;
        private readonly List<string> stylesCollection;
        private string defaultCharacterProperties;
        private string defaultParagraphProperties;
        private int defaultFontIndex;

        public RtfExportHelper()
        {
            this.defaultFontIndex = this.GetFontNameIndex("Times New Roman", RtfThemeFontType.None);
            this.colorCollection.Add(Color.Empty);
            this.listCollection = new Dictionary<int, string>();
            this.listOverrideCollectionIndex = new Dictionary<int, int>();
            this.listCollectionIndex = new Dictionary<object, int>();
            this.listOverrideCollection = new List<string>();
            this.paragraphStylesCollectionIndex = new Dictionary<string, int>();
            this.characterStylesCollectionIndex = new Dictionary<string, int>();
            this.tableStylesCollectionIndex = new Dictionary<string, int>();
            this.fontCharsetTable = new Dictionary<int, int>();
            this.stylesCollection = new List<string>();
        }

        public int GetColorIndex(Color color) => 
            !DXColor.IsTransparentColor(color) ? ((this.colorCollection.IndexOf(color) >= 0) ? this.colorCollection.IndexOf(color) : this.colorCollection.Add(color)) : 0;

        public int GetColorIndex(int backColorIndex, Func<int, Color> getColor) => 
            this.GetColorIndex(getColor(backColorIndex));

        public int GetFontNameIndex(string fontName, RtfThemeFontType themeFontType)
        {
            int index = this.fontNamesCollection.IndexOf(fontName);
            if (index >= 0)
            {
                return index;
            }
            this.fontNamesCollection.Add(fontName);
            return (this.FontNamesCollection.Count - 1);
        }

        public static int GetFontSize(Font font)
        {
            float sizeInPoints = FontSizeHelper.GetSizeInPoints(font);
            if (font.Unit != GraphicsUnit.Point)
            {
                sizeInPoints += GraphicsDpi.UnitToDpi(GraphicsUnit.Point) / GraphicsDpi.UnitToDpi(font.Unit);
            }
            return (int) Math.Floor((double) (sizeInPoints * 2f));
        }

        public DevExpress.XtraPrinting.Export.ColorCollection ColorCollection =>
            this.colorCollection;

        public StringCollection FontNamesCollection =>
            this.fontNamesCollection;

        public Dictionary<int, string> ListCollection =>
            this.listCollection;

        public Dictionary<int, int> ListOverrideCollectionIndex =>
            this.listOverrideCollectionIndex;

        public Dictionary<object, int> ListCollectionIndex =>
            this.listCollectionIndex;

        public List<string> ListOverrideCollection =>
            this.listOverrideCollection;

        public int DefaultFontIndex =>
            this.defaultFontIndex;

        public Dictionary<string, int> ParagraphStylesCollectionIndex =>
            this.paragraphStylesCollectionIndex;

        public Dictionary<string, int> CharacterStylesCollectionIndex =>
            this.characterStylesCollectionIndex;

        public Dictionary<string, int> TableStylesCollectionIndex =>
            this.tableStylesCollectionIndex;

        public Dictionary<int, int> FontCharsetTable =>
            this.fontCharsetTable;

        public List<string> StylesCollection =>
            this.stylesCollection;

        public bool SupportStyle =>
            false;

        public string DefaultCharacterProperties
        {
            get => 
                this.defaultCharacterProperties;
            set => 
                this.defaultCharacterProperties = value;
        }

        public string DefaultCharacterPropertiesStSh { get; set; }

        public string DefaultParagraphProperties
        {
            get => 
                this.defaultParagraphProperties;
            set => 
                this.defaultParagraphProperties = value;
        }
    }
}

