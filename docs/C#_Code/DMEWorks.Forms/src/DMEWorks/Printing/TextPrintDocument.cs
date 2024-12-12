namespace DMEWorks.Printing
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Text;

    public class TextPrintDocument : PrintDocument
    {
        private int _width;
        private Font _font;
        private Brush _brush;
        private string _text;
        private TextReader _reader;

        public TextPrintDocument(string text) : this(text, 80)
        {
        }

        public TextPrintDocument(string text, int width) : this(text, new Font("Courier New", 9f), Brushes.Black, width)
        {
        }

        public TextPrintDocument(string text, Font font, int width) : this(text, font, Brushes.Black, width)
        {
        }

        public TextPrintDocument(string text, Font font, Brush brush, int width)
        {
            this._width = 80;
            this._text = text;
            this._font = font;
            this._brush = brush;
            this._width = width;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._font != null)
                {
                    this._font.Dispose();
                    this._font = null;
                }
                if (this._brush != null)
                {
                    this._brush.Dispose();
                    this._brush = null;
                }
                if (this._reader != null)
                {
                    this._reader.Dispose();
                    this._reader = null;
                }
            }
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            this._reader = new StringReader(this._text);
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
            if (this._reader != null)
            {
                this._reader.Dispose();
                this._reader = null;
            }
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            StringFormat format = new StringFormat();
            float num = ((float) e.MarginBounds.Height) / this._font.GetHeight(e.Graphics);
            float left = e.MarginBounds.Left;
            float top = e.MarginBounds.Top;
            e.HasMorePages = false;
            for (int i = 0; i < num; i++)
            {
                string s = ReadLine(this._reader, this._width);
                if (s == null)
                {
                    return;
                }
                e.Graphics.DrawString(s, this._font, Brushes.Black, left, top + (i * this._font.GetHeight(e.Graphics)), format);
            }
            e.HasMorePages = true;
        }

        private static string ReadLine(TextReader reader, int maxLen)
        {
            StringBuilder builder = new StringBuilder(maxLen);
            while (true)
            {
                int num = reader.Read();
                if (num == -1)
                {
                    return ((builder.Length > 0) ? builder.ToString() : null);
                }
                if ((num == 13) || (num == 10))
                {
                    if ((num == 13) && (reader.Peek() == 10))
                    {
                        reader.Read();
                    }
                    return builder.ToString();
                }
                builder.Append((char) num);
                if (maxLen <= builder.Length)
                {
                    int num2 = reader.Peek();
                    if (num2 == 10)
                    {
                        reader.Read();
                    }
                    else if (num2 == 13)
                    {
                        reader.Read();
                        if (reader.Peek() == 10)
                        {
                            reader.Read();
                        }
                    }
                    return builder.ToString();
                }
            }
        }
    }
}

