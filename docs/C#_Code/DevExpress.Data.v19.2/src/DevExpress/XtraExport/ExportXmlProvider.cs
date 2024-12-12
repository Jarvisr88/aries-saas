namespace DevExpress.XtraExport
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;

    public class ExportXmlProvider : ExportDefaultInternalProvider
    {
        private string xslFileName;
        public static char EmptyChar = '.';

        public ExportXmlProvider(Stream stream) : base(stream)
        {
            this.xslFileName = "";
        }

        public ExportXmlProvider(string fileName) : base(fileName)
        {
            this.xslFileName = "";
        }

        public override IExportProvider Clone(string fileName, Stream stream) => 
            base.IsStreamMode ? new ExportXmlProvider(base.GetCloneStream(stream)) : new ExportXmlProvider(base.GetCloneFileName(fileName));

        public override void Commit()
        {
            if (base.IsStreamMode)
            {
                this.CommitInStreamMode();
            }
            else
            {
                this.CommitInFileMode();
            }
        }

        public override void CommitCache(StreamWriter writer)
        {
            base.OnProviderProgress(0);
            string[] textArray1 = new string[] { "<lines colcount=\"", Convert.ToString(base.CacheWidth()), "\" rowcount=\"", Convert.ToString(base.CacheHeight()), "\">" };
            writer.WriteLine(string.Concat(textArray1));
            int row = 0;
            while (row < base.CacheHeight())
            {
                writer.WriteLine("<line>");
                int col = 0;
                while (true)
                {
                    if (col >= base.CacheWidth())
                    {
                        writer.WriteLine("</line>");
                        if (base.CacheHeight() > 1)
                        {
                            base.OnProviderProgress((row * 0x63) / (base.CacheHeight() - 1));
                        }
                        row++;
                        break;
                    }
                    if (!base.cache[col, row].IsHidden)
                    {
                        writer.Write("<cell" + this.GetCellParams(col, row) + ">");
                        if (base.cache[col, row].InternalCache != null)
                        {
                            base.cache[col, row].InternalCache.CommitCache(writer);
                        }
                        else
                        {
                            writer.Write(this.ConvertTextToXML(this.GetData(col, row)));
                        }
                        writer.WriteLine("</cell>");
                    }
                    col++;
                }
            }
            writer.WriteLine("</lines>");
            base.OnProviderProgress(0x63);
        }

        private void CommitInFileMode()
        {
            this.xslFileName = this.GetXslFileName(base.FileName);
            StreamWriter writer = base.CreateStreamWriter(base.FileName);
            StreamWriter writer2 = base.CreateStreamWriter(this.xslFileName);
            try
            {
                this.CommitXml(writer);
                this.CommitXsl(writer2);
            }
            finally
            {
                writer2.Dispose();
                writer.Dispose();
            }
        }

        private void CommitInStreamMode()
        {
            StreamWriter writer = new StreamWriter(base.Stream);
            try
            {
                this.CommitXml(writer);
            }
            finally
            {
                writer.Flush();
            }
        }

        private void CommitStyle(StreamWriter writer)
        {
            writer.WriteLine("<styles>");
            for (int i = 0; i < base.styleManager.Count; i++)
            {
                string[] textArray1 = new string[] { "<style id=\"", Convert.ToString(i), "\" ", this.GetStyle(base.styleManager[i]), ">" };
                writer.WriteLine(string.Concat(textArray1));
                writer.WriteLine(this.GetBorderStyle(base.styleManager[i]));
                writer.WriteLine("</style>");
            }
            writer.WriteLine("</styles>");
        }

        private void CommitXml(StreamWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\"?>");
            writer.WriteLine("<?xml-stylesheet type=\"text/xsl\" href=\"" + Path.GetFileName(this.xslFileName) + "\"?>");
            writer.WriteLine("<cache>");
            writer.WriteLine("<title>" + this.GetTitle() + "</title>");
            this.CommitStyle(writer);
            this.CommitCache(writer);
            writer.WriteLine("</cache>");
        }

        private void CommitXsl(StreamWriter writer)
        {
            writer.WriteLine("<?xml version=\"1.0\"?>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">");
            writer.WriteLine("<xsl:template match=\"/\">");
            writer.WriteLine("<xsl:apply-templates select=\"cache\" />");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"cache\">");
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("<xsl:apply-templates select=\"title\" />");
            writer.WriteLine("<xsl:apply-templates select=\"styles\" />");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<xsl:apply-templates select=\"lines\" />");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"title\">");
            writer.WriteLine("<title>");
            writer.WriteLine("<xsl:value-of select=\".\" />");
            writer.WriteLine("</title>");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"styles\">");
            writer.WriteLine("<style type=\"text/css\">");
            writer.WriteLine("<xsl:apply-templates select=\"style\" />");
            writer.WriteLine("</style>");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"style\">");
            writer.WriteLine(".style<xsl:value-of select=\"@id\" />");
            writer.WriteLine("{ border-style: solid; padding: 3;");
            writer.WriteLine("  font-family: <xsl:value-of select=\"@fontname\" />;");
            writer.WriteLine("  font-size: <xsl:value-of select=\"@fontsize\" />pt;");
            writer.WriteLine("  color: <xsl:value-of select=\"@fontcolor\" />;");
            writer.WriteLine("  background-color: <xsl:value-of select=\"@bkcolor\" />;");
            writer.WriteLine("<xsl:if test=\"@bold='true'\">");
            writer.WriteLine("  font-weight: bold;");
            writer.WriteLine("</xsl:if>");
            writer.WriteLine("<xsl:if test=\"@italic='true'\">");
            writer.WriteLine("  font-style: italic;");
            writer.WriteLine("</xsl:if>");
            writer.WriteLine("<xsl:if test=\"@underline='true'\">");
            writer.WriteLine("  text-decoration: underline;");
            writer.WriteLine("</xsl:if>");
            writer.WriteLine("<xsl:if test=\"@strikeout='true'\">");
            writer.WriteLine("  text-decoration: line-through;");
            writer.WriteLine("</xsl:if>");
            writer.WriteLine("<xsl:apply-templates select=\"border_left\" />");
            writer.WriteLine("<xsl:apply-templates select=\"border_up\" />");
            writer.WriteLine("<xsl:apply-templates select=\"border_right\" />");
            writer.WriteLine("<xsl:apply-templates select=\"border_down\" />");
            writer.WriteLine("}");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"border_left\">");
            writer.WriteLine("border-left-width: <xsl:value-of select=\"@width\" />;");
            writer.WriteLine("border-left-color: <xsl:value-of select=\"@color\" />;");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"border_up\">");
            writer.WriteLine("border-top-width: <xsl:value-of select=\"@width\" />;");
            writer.WriteLine("border-top-color: <xsl:value-of select=\"@color\" />;");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"border_right\">");
            writer.WriteLine("border-right-width: <xsl:value-of select=\"@width\" />;");
            writer.WriteLine("border-right-color: <xsl:value-of select=\"@color\" />;");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"border_down\">");
            writer.WriteLine("border-bottom-width: <xsl:value-of select=\"@width\" />;");
            writer.WriteLine("border-bottom-color: <xsl:value-of select=\"@color\" />;");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"lines\">");
            writer.WriteLine("<table border=\"1\" cellspacing=\"0\">");
            writer.WriteLine("<xsl:apply-templates select=\"line\" />");
            writer.WriteLine("</table>");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"line\">");
            writer.WriteLine("<tr>");
            writer.WriteLine("<xsl:apply-templates select=\"cell\" />");
            writer.WriteLine("</tr>");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.WriteLine("<xsl:template match=\"cell\">");
            writer.WriteLine("<td>");
            writer.WriteLine("<xsl:attribute name=\"nowrap\"></xsl:attribute>");
            writer.WriteLine("<xsl:attribute name=\"width\"><xsl:value-of select=\"@width\" /></xsl:attribute>");
            writer.WriteLine("<xsl:attribute name=\"align\"><xsl:value-of select=\"@align\" /></xsl:attribute>");
            writer.WriteLine("<xsl:attribute name=\"colspan\"><xsl:value-of select=\"@colspan\" /></xsl:attribute>");
            writer.WriteLine("<xsl:attribute name=\"rowspan\"><xsl:value-of select=\"@rowspan\" /></xsl:attribute>");
            writer.WriteLine("<xsl:attribute name=\"class\">style<xsl:value-of select=\"@styleclass\" /></xsl:attribute>");
            writer.WriteLine("<xsl:choose>");
            writer.WriteLine("<xsl:when test=\"lines\">");
            writer.WriteLine("<xsl:apply-templates select=\"lines\" />");
            writer.WriteLine("</xsl:when>");
            writer.WriteLine("<xsl:otherwise>");
            writer.WriteLine("<xsl:value-of select=\".\" />");
            writer.WriteLine("</xsl:otherwise>");
            writer.WriteLine("</xsl:choose>");
            writer.WriteLine("</td>");
            writer.WriteLine("</xsl:template>");
            writer.WriteLine("");
            writer.Write("</xsl:stylesheet>");
            base.OnProviderProgress(100);
        }

        private string ConvertTextToXML(string text)
        {
            string str = "";
            for (int i = 0; i < text.Length; i++)
            {
                str = (text[i] != '<') ? ((text[i] != '>') ? ((text[i] != '&') ? ((text[i] != '"') ? ((text[i] != '\'') ? (str + text[i].ToString()) : (str + "&apos;")) : (str + "&quot;")) : (str + "&amp;")) : (str + "&gt;")) : (str + "&lt;");
            }
            if (this.IsEmptyCharOnly(str))
            {
                str = "&#160;";
            }
            return str;
        }

        private string GetAlignText(StringAlignment alignment)
        {
            switch (alignment)
            {
                case StringAlignment.Near:
                    return "Left";

                case StringAlignment.Center:
                    return "Center";

                case StringAlignment.Far:
                    return "Right";
            }
            return "";
        }

        private string GetBorderStyle(ExportCacheCellStyle style) => 
            ((("" + "<border_left" + this.GetBorderStyleContent(style.LeftBorder) + "/>\n") + "<border_up" + this.GetBorderStyleContent(style.TopBorder) + "/>\n") + "<border_right" + this.GetBorderStyleContent(style.RightBorder) + "/>\n") + "<border_down" + this.GetBorderStyleContent(style.BottomBorder) + "/>";

        private string GetBorderStyleContent(ExportCacheCellBorderStyle borderStyle) => 
            ("" + " width=\"" + Convert.ToString(borderStyle.Width) + "\"") + " color=\"" + this.GetHtmlColor(borderStyle.Color_) + "\"";

        private string GetCellParams(int col, int row)
        {
            string str = "";
            int cellWidth = base.GetCellWidth(col, row);
            if (cellWidth > 0)
            {
                str = str + " width=\"" + Convert.ToString(cellWidth) + "\"";
            }
            str = str + " align=\"";
            switch (base.GetCellStyle(col, row).TextAlignment)
            {
                case StringAlignment.Near:
                    str = str + "left\"";
                    break;

                case StringAlignment.Center:
                    str = str + "center\"";
                    break;

                case StringAlignment.Far:
                    str = str + "right\"";
                    break;

                default:
                    break;
            }
            if (base.cache[col, row].IsUnion)
            {
                if (base.cache[col, row].UnionWidth > 1)
                {
                    str = str + " colspan=\"" + Convert.ToString(base.cache[col, row].UnionWidth) + "\"";
                }
                if (base.cache[col, row].UnionHeight > 1)
                {
                    str = str + " rowspan=\"" + Convert.ToString(base.cache[col, row].UnionHeight) + "\"";
                }
            }
            return (str + " styleclass=\"" + Convert.ToString(base.cache[col, row].StyleIndex) + "\"");
        }

        private string GetData(int col, int row)
        {
            string data = "";
            if (base.cache[col, row].Data != null)
            {
                switch (base.cache[col, row].DataType)
                {
                    case ExportCacheDataType.Boolean:
                        data = Convert.ToString((bool) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.Integer:
                        data = Convert.ToString((int) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.Double:
                        data = Convert.ToString((double) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.Decimal:
                        data = Convert.ToString((decimal) base.cache[col, row].Data);
                        break;

                    case ExportCacheDataType.String:
                        data = (string) base.cache[col, row].Data;
                        break;

                    case ExportCacheDataType.Single:
                        data = Convert.ToString((float) base.cache[col, row].Data);
                        break;

                    default:
                        data = Convert.ToString(base.cache[col, row].Data);
                        break;
                }
            }
            if (this.IsEmptyString(data))
            {
                data = data + EmptyChar.ToString();
            }
            return data;
        }

        private string GetFontStyles(Font font)
        {
            string str = "";
            str = !font.Bold ? (str + " bold=\"false\"") : (str + " bold=\"true\"");
            str = !font.Italic ? (str + " italic=\"false\"") : (str + " italic=\"true\"");
            str = !font.Underline ? (str + " underline=\"false\"") : (str + " underline=\"true\"");
            return (!font.Strikeout ? (str + " strikeout=\"false\"") : (str + " strikeout=\"true\""));
        }

        private string GetHtmlColor(Color color)
        {
            string str = "rgb(";
            string[] textArray1 = new string[] { str, Convert.ToString(color.R), ",", Convert.ToString(color.G), ",", Convert.ToString(color.B), ")" };
            return string.Concat(textArray1);
        }

        private string GetStyle(ExportCacheCellStyle style) => 
            ((((("" + "aligntext=\"" + this.GetAlignText(style.TextAlignment) + "\"") + " fontname=\"" + style.TextFont.Name + "\"") + this.GetFontStyles(style.TextFont)) + " fontcolor=\"" + this.GetHtmlColor(style.TextColor) + "\"") + " fontsize=\"" + Convert.ToString(style.TextFont.Size) + "\"") + " bkcolor=\"" + this.GetHtmlColor(style.BkColor) + "\"";

        private string GetTitle() => 
            !base.IsStreamMode ? Path.GetFileNameWithoutExtension(base.FileName) : ((base.Stream is FileStream) ? Path.GetFileNameWithoutExtension(((FileStream) base.Stream).Name) : "Stream");

        private string GetXslFileName(string xmlFileName)
        {
            StringBuilder builder = new StringBuilder();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(xmlFileName);
            string directoryName = Path.GetDirectoryName(xmlFileName);
            if (!string.IsNullOrEmpty(directoryName) && (directoryName[directoryName.Length - 1] != '\\'))
            {
                directoryName = directoryName + @"\";
            }
            builder.Append(directoryName);
            for (int i = 0; i < fileNameWithoutExtension.Length; i++)
            {
                if (this.IsValidChar(fileNameWithoutExtension[i]))
                {
                    builder.Append(fileNameWithoutExtension[i]);
                }
                else
                {
                    builder.Append('_');
                }
            }
            builder.Append(".xsl");
            return builder.ToString();
        }

        private bool IsEmptyCharOnly(string str)
        {
            bool flag = false;
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] != ' ') && (str[i] != '\t'))
                {
                    if (str[i] != EmptyChar)
                    {
                        return false;
                    }
                    if (flag)
                    {
                        return false;
                    }
                    flag = true;
                }
            }
            return true;
        }

        private bool IsEmptyString(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] != ' ') && (str[i] != '\t'))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidChar(char ch) => 
            ((ch < 'a') || (ch > 'z')) ? (((ch >= 'A') && (ch <= 'Z')) || (((ch >= '0') && (ch <= '9')) || ((ch == '_') || ((ch == '-') || (ch == ' '))))) : true;
    }
}

