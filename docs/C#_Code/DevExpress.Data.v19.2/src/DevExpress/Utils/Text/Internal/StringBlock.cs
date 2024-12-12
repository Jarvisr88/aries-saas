namespace DevExpress.Utils.Text.Internal
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class StringBlock
    {
        private string text = null;
        private string imageName = null;
        private System.Drawing.Font font;
        private int fontHeight = -1;
        private int fontAscentHeight = -1;
        private int internalLeading = -1;
        private int lineNumber = -1;
        private System.Drawing.Size? size = null;
        private StringBlockType type = StringBlockType.Text;
        private StringFontSettings fontSettings = new StringFontSettings();
        private StringBlockAlignment? alignment = null;
        private int[] matchIndex;
        private int[] matchLength;

        public StringBlock()
        {
            this.DpiScaleFactor = 1f;
        }

        public string GetMatchText(int index)
        {
            if ((this.matchIndex == null) || string.IsNullOrEmpty(this.Text))
            {
                return null;
            }
            if (index >= this.matchIndex.Length)
            {
                return null;
            }
            int startIndex = this.MatchIndexes[index];
            int num2 = this.MatchLength[index];
            return ((startIndex < this.Text.Length) ? this.Text.Substring(startIndex, Math.Min(this.Text.Length - startIndex, num2)) : null);
        }

        public static void ParseParameter(string parameter, out string name, out string value)
        {
            name = parameter;
            value = null;
            int index = parameter.IndexOf('=');
            if (index >= 0)
            {
                name = parameter.Substring(0, index).ToLowerInvariant();
                value = parameter.Substring(index + 1).ToLowerInvariant();
            }
        }

        private void ParseSettings()
        {
            if ((this.Type != StringBlockType.Text) && !string.IsNullOrEmpty(this.Text))
            {
                if (this.text.IndexOf(';') < 0)
                {
                    this.imageName = this.Text;
                    this.alignment = 1;
                    this.size = new System.Drawing.Size?(System.Drawing.Size.Empty);
                }
                else
                {
                    char[] separator = new char[] { ';' };
                    string[] strArray = this.text.Split(separator);
                    this.imageName = strArray[0];
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        string str;
                        string str2;
                        ParseParameter(strArray[i], out str, out str2);
                        this.ParseSettingsParameter(str, str2);
                    }
                }
            }
        }

        private StringBlockAlignment ParseSettingsAlignment(string parValue) => 
            (parValue != "top") ? ((parValue != "bottom") ? StringBlockAlignment.Center : StringBlockAlignment.Bottom) : StringBlockAlignment.Top;

        private void ParseSettingsParameter(string parName, string parValue)
        {
            if ((parName == "align") || (parName == "alignment"))
            {
                this.alignment = new StringBlockAlignment?(this.ParseSettingsAlignment(parValue));
            }
            else if (parName == "size")
            {
                this.size = new System.Drawing.Size?(this.ParseSettingsSize(parValue));
            }
        }

        private System.Drawing.Size ParseSettingsSize(string parValue)
        {
            System.Drawing.Size empty = System.Drawing.Size.Empty;
            try
            {
                int index = parValue.IndexOf(',');
                if (index >= 0)
                {
                    empty.Width = (int) (this.DpiScaleFactor * int.Parse(parValue.Substring(0, index)));
                    empty.Height = (int) (this.DpiScaleFactor * int.Parse(parValue.Substring(index + 1)));
                }
                else
                {
                    return empty;
                }
            }
            catch
            {
            }
            return empty;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetMatchInfo()
        {
            this.SetMatchInfo((int[]) null, (int[]) null);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetAscentHeight(int fontAscentHeight)
        {
            this.fontAscentHeight = fontAscentHeight;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetBlock(StringBlock source)
        {
            this.size = source.size;
            this.alignment = source.alignment;
            this.font = source.font;
            this.fontHeight = source.fontHeight;
            this.fontAscentHeight = source.fontAscentHeight;
            this.internalLeading = source.internalLeading;
            this.FontSettings.Assign(source.FontSettings);
            this.type = source.Type;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetFontInfo(System.Drawing.Font font, int fontHeight, int fontAscentHeight)
        {
            this.SetFontInfo(font, fontHeight, fontAscentHeight, 0);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetFontInfo(System.Drawing.Font font, int fontHeight, int fontAscentHeight, int internalLeading)
        {
            this.font = font;
            this.fontHeight = fontHeight;
            this.fontAscentHeight = fontAscentHeight;
            this.internalLeading = internalLeading;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetMatchInfo(int matchIndex, int matchLength)
        {
            if (this.matchIndex != null)
            {
                Array.Resize<int>(ref this.matchIndex, this.matchIndex.Length + 1);
                Array.Resize<int>(ref this.matchLength, this.matchLength.Length + 1);
                this.matchIndex[this.matchIndex.Length - 1] = matchIndex;
                this.matchLength[this.matchLength.Length - 1] = matchLength;
            }
            else
            {
                this.matchIndex = new int[] { matchIndex };
                this.matchLength = new int[] { matchLength };
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetMatchInfo(int[] matchIndex, int[] matchLength)
        {
            this.matchIndex = matchIndex;
            this.matchLength = matchLength;
        }

        public override string ToString() => 
            ((this.Type == StringBlockType.Image) || (this.Type == StringBlockType.Object)) ? this.Type.ToString() : (string.IsNullOrEmpty(this.Text) ? "" : this.Text);

        internal float DpiScaleFactor { get; set; }

        public StringBlockAlignment Alignment
        {
            get
            {
                if (this.alignment == null)
                {
                    this.ParseSettings();
                }
                return ((this.alignment != null) ? this.alignment.Value : StringBlockAlignment.Center);
            }
        }

        public int Height { get; set; }

        public string ImageName
        {
            get
            {
                if (this.imageName == null)
                {
                    this.ParseSettings();
                }
                return ((this.imageName == null) ? string.Empty : this.imageName);
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                if (this.size == null)
                {
                    this.ParseSettings();
                }
                return ((this.size != null) ? this.size.Value : System.Drawing.Size.Empty);
            }
        }

        public string Link { get; set; }

        public StringBlockType Type
        {
            get => 
                this.type;
            set => 
                this.type = value;
        }

        public System.Drawing.Font Font =>
            this.font;

        public bool IsEmpty =>
            string.IsNullOrEmpty(this.text);

        public object Data { get; set; }

        public string Text
        {
            get => 
                this.text;
            set => 
                this.text = value;
        }

        public int LineNumber
        {
            get => 
                this.lineNumber;
            set => 
                this.lineNumber = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int FontHeight =>
            this.fontHeight;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int FontAscentHeight =>
            this.fontAscentHeight;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int FontInnerHeight =>
            this.FontAscentHeight - this.FontInternalLeading;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int FontBaseLine =>
            this.FontAscentHeight;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int FontInternalLeading =>
            this.internalLeading;

        public StringFontSettings FontSettings =>
            this.fontSettings;

        public int[] MatchIndexes =>
            this.matchIndex;

        public int[] MatchLength =>
            this.matchLength;
    }
}

