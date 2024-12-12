namespace DevExpress.XtraExport
{
    using System;
    using System.Drawing;
    using System.IO;

    [Obsolete("")]
    public class ExportHtmlProvider : ExportDefaultInternalProvider
    {
        public ExportHtmlProvider(Stream stream) : base(stream)
        {
        }

        public ExportHtmlProvider(string fileName) : base(fileName)
        {
        }

        public override IExportProvider Clone(string fileName, Stream stream) => 
            base.IsStreamMode ? new ExportHtmlProvider(base.GetCloneStream(stream)) : new ExportHtmlProvider(base.GetCloneFileName(fileName));

        public override void Commit()
        {
            StreamWriter writer = base.IsStreamMode ? new StreamWriter(base.Stream) : base.CreateStreamWriter(base.FileName);
            try
            {
                this.CommitHtml(writer);
            }
            finally
            {
                if (base.IsStreamMode)
                {
                    writer.Flush();
                }
                else
                {
                    writer.Dispose();
                }
            }
        }

        public override void CommitCache(StreamWriter writer)
        {
            base.OnProviderProgress(0);
            writer.WriteLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\">");
            int row = 0;
            while (row < base.CacheHeight())
            {
                writer.WriteLine("<tr>");
                int col = 0;
                while (true)
                {
                    if (col >= base.CacheWidth())
                    {
                        writer.WriteLine("</tr>");
                        if (base.CacheHeight() > 1)
                        {
                            base.OnProviderProgress((row * 100) / (base.CacheHeight() - 1));
                        }
                        row++;
                        break;
                    }
                    if (!base.cache[col, row].IsHidden)
                    {
                        if (!base.cache[col, row].IsUnion)
                        {
                            writer.Write("<td");
                        }
                        else
                        {
                            writer.Write("<td");
                            if (base.cache[col, row].UnionWidth > 1)
                            {
                                writer.Write(" colspan=\"" + Convert.ToString(base.cache[col, row].UnionWidth) + "\"");
                            }
                            if (base.cache[col, row].UnionHeight > 1)
                            {
                                writer.Write(" rowspan=\"" + Convert.ToString(base.cache[col, row].UnionHeight) + "\"");
                            }
                        }
                        int cellWidth = base.GetCellWidth(col, row);
                        if (cellWidth > 0)
                        {
                            writer.Write(" width=\"" + Convert.ToString(cellWidth) + "\"");
                        }
                        ExportCacheCellStyle cellStyle = base.GetCellStyle(col, row);
                        writer.Write(" align=\"");
                        switch (cellStyle.TextAlignment)
                        {
                            case StringAlignment.Near:
                                writer.Write("left");
                                break;

                            case StringAlignment.Center:
                                writer.Write("center");
                                break;

                            case StringAlignment.Far:
                                writer.Write("right");
                                break;

                            default:
                                break;
                        }
                        writer.Write("\"");
                        writer.Write(" nowrap");
                        writer.Write(" class=\"style" + Convert.ToString(base.cache[col, row].StyleIndex) + "\">");
                        string text = "";
                        if (base.cache[col, row].InternalCache != null)
                        {
                            base.cache[col, row].InternalCache.CommitCache(writer);
                        }
                        else
                        {
                            if (base.cache[col, row].Data == null)
                            {
                                text = "";
                            }
                            else
                            {
                                switch (base.cache[col, row].DataType)
                                {
                                    case ExportCacheDataType.Boolean:
                                        text = Convert.ToString((bool) base.cache[col, row].Data);
                                        break;

                                    case ExportCacheDataType.Integer:
                                        text = Convert.ToString((int) base.cache[col, row].Data);
                                        break;

                                    case ExportCacheDataType.Double:
                                        text = Convert.ToString((double) base.cache[col, row].Data);
                                        break;

                                    case ExportCacheDataType.Decimal:
                                        text = Convert.ToString((decimal) base.cache[col, row].Data);
                                        break;

                                    case ExportCacheDataType.String:
                                        text = this.ConvertSpecialSymbols((string) base.cache[col, row].Data);
                                        break;

                                    case ExportCacheDataType.Single:
                                        text = Convert.ToString((float) base.cache[col, row].Data);
                                        break;

                                    default:
                                        text = Convert.ToString(base.cache[col, row].Data);
                                        break;
                                }
                                text = this.ConvertCRLFSymbols(text);
                            }
                            if (this.IsEmptyString(text))
                            {
                                text = text + "&nbsp";
                            }
                        }
                        writer.WriteLine(text + "</td>");
                    }
                    col++;
                }
            }
            writer.WriteLine("</table>");
            base.OnProviderProgress(100);
        }

        private void CommitHtml(StreamWriter writer)
        {
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("<title>" + this.GetTitle() + "</title>");
            writer.WriteLine("<META http-equiv=Content-type content=\"text/html; charset=UTF-8\">");
            this.CommitStyles(writer);
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            this.CommitCache(writer);
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        private void CommitStyles(StreamWriter writer)
        {
            writer.WriteLine("<style type=\"text/css\"><!--");
            for (int i = 0; i < base.styleManager.Count; i++)
            {
                writer.Write(".style" + Convert.ToString(i) + " {");
                writer.Write(this.GetStyle(base.styleManager[i]));
                writer.WriteLine("}");
            }
            writer.WriteLine("--></style>");
        }

        private string ConvertCRLFSymbols(string text)
        {
            string str = "";
            for (int i = 0; i < text.Length; i++)
            {
                str = (text[i] != '\n') ? (str + text[i].ToString()) : (str + "<br>");
            }
            return str;
        }

        private string ConvertSpecialSymbols(string text)
        {
            string str = "";
            for (int i = 0; i < text.Length; i++)
            {
                str = (text[i] != '<') ? ((text[i] != '>') ? ((text[i] != '&') ? ((text[i] != '"') ? (str + text[i].ToString()) : (str + "&quot;")) : (str + "&amp;")) : (str + "&gt;")) : (str + "&lt;");
            }
            return str;
        }

        private string GetHtmlColor(Color color)
        {
            string str = "rgb(";
            string[] textArray1 = new string[] { str, Convert.ToString(color.R), ",", Convert.ToString(color.G), ",", Convert.ToString(color.B), ")" };
            return string.Concat(textArray1);
        }

        private string GetStyle(ExportCacheCellStyle style)
        {
            string str = "" + " border-style: solid;" + " padding: 3;";
            str = ((style.LeftBorder.Width != style.TopBorder.Width) || ((style.LeftBorder.Width != style.RightBorder.Width) || (style.LeftBorder.Width != style.BottomBorder.Width))) ? ((((str + " border-left-width: " + Convert.ToString(style.LeftBorder.Width) + ";") + " border-top-width: " + Convert.ToString(style.TopBorder.Width) + ";") + " border-right-width: " + Convert.ToString(style.RightBorder.Width) + ";") + " border-bottom-width: " + Convert.ToString(style.BottomBorder.Width) + ";") : (str + " border-width: " + Convert.ToString(style.LeftBorder.Width) + ";");
            str = (!(style.LeftBorder.Color_ == style.TopBorder.Color_) || (!(style.LeftBorder.Color_ == style.RightBorder.Color_) || !(style.LeftBorder.Color_ == style.BottomBorder.Color_))) ? ((((str + " border-left-color: " + this.GetHtmlColor(style.LeftBorder.Color_) + ";") + " border-top-color: " + this.GetHtmlColor(style.TopBorder.Color_) + ";") + " border-right-color: " + this.GetHtmlColor(style.RightBorder.Color_) + ";") + " border-bottom-color: " + this.GetHtmlColor(style.BottomBorder.Color_) + ";") : (str + " border-color: " + this.GetHtmlColor(style.LeftBorder.Color_) + ";");
            str = str + " font-family: \"" + style.TextFont.Name + "\";";
            if (style.TextFont.Bold)
            {
                str = str + " font-weight: bold;";
            }
            if (style.TextFont.Italic)
            {
                str = str + " font-style: italic;";
            }
            if (style.TextFont.Underline)
            {
                str = str + " text-decoration: underline;";
            }
            if (style.TextFont.Strikeout)
            {
                str = str + " text-decoration: line-through;";
            }
            return (((str + " font-size: " + Convert.ToString(style.TextFont.Size) + "pt;") + " color: " + this.GetHtmlColor(style.TextColor) + ";") + " background-color: " + this.GetHtmlColor(style.BkColor));
        }

        private string GetTitle() => 
            !base.IsStreamMode ? Path.GetFileNameWithoutExtension(base.FileName) : ((base.Stream is FileStream) ? Path.GetFileNameWithoutExtension(((FileStream) base.Stream).Name) : "Stream");

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
    }
}

