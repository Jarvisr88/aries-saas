namespace DevExpress.XtraPrinting.Export.Rtf
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.IO;

    public abstract class RtfExportProviderBase
    {
        private Stream stream;
        protected StreamWriter writer;
        protected RtfExportHelper rtfExportHelper;

        protected RtfExportProviderBase(Stream stream, RtfExportHelper rtfExportHelper)
        {
            this.stream = stream;
            this.rtfExportHelper = rtfExportHelper;
        }

        public void Commit()
        {
            this.writer = new StreamWriter(this.stream, DXEncoding.Default);
            this.writer.WriteLine(string.Format(RtfTags.StartWithCodePage, DXEncoding.Default.CodePage));
            this.WriteHeader();
            this.WriteContent();
            this.writer.WriteLine("}");
            this.writer.Flush();
        }

        protected void WriteColorTable()
        {
            this.writer.WriteLine("{");
            this.writer.WriteLine(RtfTags.ColorTable);
            ColorCollection colorCollection = this.rtfExportHelper.ColorCollection;
            int count = colorCollection.Count;
            for (int i = 0; i < count; i++)
            {
                if (colorCollection[i] != Color.Empty)
                {
                    this.writer.WriteLine(string.Format(RtfTags.RGB, colorCollection[i].R, colorCollection[i].G, colorCollection[i].B));
                }
                else
                {
                    this.writer.WriteLine(";");
                }
            }
            this.writer.WriteLine("}");
        }

        protected abstract void WriteContent();
        protected void WriteFontTable()
        {
            this.writer.WriteLine("{");
            this.writer.WriteLine(RtfTags.FontTable);
            StringCollection fontNamesCollection = this.rtfExportHelper.FontNamesCollection;
            int count = fontNamesCollection.Count;
            for (int i = 0; i < count; i++)
            {
                this.writer.WriteLine(string.Format(RtfTags.DefineFont, i, fontNamesCollection[i]));
            }
            this.writer.WriteLine("}");
        }

        protected virtual void WriteHeader()
        {
            this.WriteFontTable();
            this.WriteColorTable();
            this.WriteListTable();
            this.WriteListOverrideTable();
        }

        protected internal virtual void WriteListOverrideTable()
        {
            List<string> listOverrideCollection = this.rtfExportHelper.ListOverrideCollection;
            if (listOverrideCollection.Count != 0)
            {
                this.writer.WriteLine("{");
                this.writer.WriteLine(RtfTags.ListOverrideTable);
                int count = listOverrideCollection.Count;
                for (int i = 0; i < count; i++)
                {
                    this.writer.WriteLine(listOverrideCollection[i]);
                }
                this.writer.WriteLine("}");
            }
        }

        protected internal virtual void WriteListTable()
        {
            Dictionary<int, string> listCollection = this.rtfExportHelper.ListCollection;
            if (listCollection.Count > 0)
            {
                this.writer.WriteLine("{");
                this.writer.WriteLine(RtfTags.NumberingListTable);
                foreach (string str in listCollection.Values)
                {
                    this.writer.WriteLine(str);
                }
                this.writer.WriteLine("}");
            }
        }
    }
}

