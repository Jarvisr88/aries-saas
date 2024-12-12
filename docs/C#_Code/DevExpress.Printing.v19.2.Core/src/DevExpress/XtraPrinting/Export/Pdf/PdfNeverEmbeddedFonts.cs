namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class PdfNeverEmbeddedFonts
    {
        private ArrayList list = new ArrayList();

        private void AddFontInfo(string fontName)
        {
            if (!this.Find(fontName))
            {
                this.list.Add(fontName);
            }
        }

        private bool Find(string fontName)
        {
            bool flag;
            using (IEnumerator enumerator = this.list.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string current = (string) enumerator.Current;
                        if (current != fontName)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public bool FindFont(Font font) => 
            this.Find(PdfFontUtils.GetFontName(font));

        public void RegisterFont(Font font)
        {
            this.AddFontInfo(PdfFontUtils.GetFontName(font));
        }

        public void RegisterFontFamily(string familyName)
        {
            this.AddFontInfo(PdfFontUtils.GetFontName(familyName, false, false));
            this.AddFontInfo(PdfFontUtils.GetFontName(familyName, true, false));
            this.AddFontInfo(PdfFontUtils.GetFontName(familyName, false, true));
            this.AddFontInfo(PdfFontUtils.GetFontName(familyName, true, true));
        }
    }
}

