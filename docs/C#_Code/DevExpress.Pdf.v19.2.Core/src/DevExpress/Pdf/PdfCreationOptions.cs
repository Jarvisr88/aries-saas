namespace DevExpress.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PdfCreationOptions
    {
        private PdfCompatibility compatibility;
        private IList<string> notEmbeddedFontFamilies;
        private bool disableEmbeddingAllFonts;
        private bool rightToLeftLayout;

        internal bool EmbedFont(string fontFamily, bool isBold, bool isItalic)
        {
            if (this.disableEmbeddingAllFonts)
            {
                return false;
            }
            if ((this.notEmbeddedFontFamilies == null) || (this.notEmbeddedFontFamilies.Count == 0))
            {
                return true;
            }
            fontFamily = RemoveSpaces(fontFamily);
            if (!(isBold | isItalic))
            {
                return !this.notEmbeddedFontFamilies.Contains(fontFamily);
            }
            string item = $"{fontFamily},{isBold ? "Bold" : ""}{isItalic ? "Italic" : ""}";
            return (!this.notEmbeddedFontFamilies.Contains(fontFamily) && !this.notEmbeddedFontFamilies.Contains(item));
        }

        private static string RemoveSpaces(string value)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in value)
            {
                if ((ch != ' ') && ((ch != '\r') && (ch != '\n')))
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        public PdfCompatibility Compatibility
        {
            get => 
                this.compatibility;
            set => 
                this.compatibility = value;
        }

        public IList<string> NotEmbeddedFontFamilies
        {
            get => 
                this.notEmbeddedFontFamilies;
            set
            {
                int count = value.Count;
                this.notEmbeddedFontFamilies = new List<string>(count);
                for (int i = 0; i < count; i++)
                {
                    this.notEmbeddedFontFamilies.Add(RemoveSpaces(value[i]));
                }
            }
        }

        public bool DisableEmbeddingAllFonts
        {
            get => 
                this.disableEmbeddingAllFonts;
            set => 
                this.disableEmbeddingAllFonts = value;
        }

        public bool MergePdfADocuments { get; set; }

        internal bool RightToLeftLayout
        {
            get => 
                this.rightToLeftLayout;
            set => 
                this.rightToLeftLayout = value;
        }
    }
}

