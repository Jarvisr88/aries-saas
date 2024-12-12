namespace DevExpress.Utils.Internal
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class FontSurrogate
    {
        private void ClearStyle(FontStyle flag)
        {
            this.Style &= ~flag;
        }

        public override bool Equals(object obj)
        {
            FontSurrogate surrogate = obj as FontSurrogate;
            return ((surrogate == null) ? base.Equals(obj) : ((this.Name == surrogate.Name) && ((this.Size == surrogate.Size) && (this.Style == surrogate.Style))));
        }

        private bool ExistsStyle(FontStyle flag) => 
            (this.Style & flag) > FontStyle.Regular;

        public static FontSurrogate FromFont(Font font)
        {
            if (font == null)
            {
                FontSurrogate surrogate1 = new FontSurrogate();
                surrogate1.Name = string.Empty;
                surrogate1.Size = 0f;
                surrogate1.Style = FontStyle.Regular;
                return surrogate1;
            }
            FontSurrogate surrogate2 = new FontSurrogate();
            surrogate2.Name = font.Name;
            surrogate2.Size = font.Size;
            surrogate2.Style = font.Style;
            return surrogate2;
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        private void SetStyle(FontStyle flag)
        {
            this.Style |= flag;
        }

        public static Font ToFont(FontSurrogate surrogate) => 
            new Font(surrogate.Name, surrogate.Size, surrogate.Style);

        public float Size { get; set; }

        public string Name { get; set; }

        public FontStyle Style { get; set; }

        public bool Regular
        {
            get => 
                this.Style == FontStyle.Regular;
            set => 
                this.Style = FontStyle.Regular;
        }

        public bool Bold
        {
            get => 
                this.ExistsStyle(FontStyle.Bold);
            set
            {
                if (value)
                {
                    this.SetStyle(FontStyle.Bold);
                }
                else
                {
                    this.ClearStyle(FontStyle.Bold);
                }
            }
        }

        public bool Italic
        {
            get => 
                this.ExistsStyle(FontStyle.Italic);
            set
            {
                if (value)
                {
                    this.SetStyle(FontStyle.Italic);
                }
                else
                {
                    this.ClearStyle(FontStyle.Italic);
                }
            }
        }

        public bool Underline
        {
            get => 
                this.ExistsStyle(FontStyle.Underline);
            set
            {
                if (value)
                {
                    this.SetStyle(FontStyle.Underline);
                }
                else
                {
                    this.ClearStyle(FontStyle.Underline);
                }
            }
        }

        public bool Strikeout
        {
            get => 
                this.ExistsStyle(FontStyle.Strikeout);
            set
            {
                if (value)
                {
                    this.SetStyle(FontStyle.Strikeout);
                }
                else
                {
                    this.ClearStyle(FontStyle.Strikeout);
                }
            }
        }

        public bool IsEmpty =>
            (this.Name == string.Empty) && ((this.Size == 0f) && (this.Style == FontStyle.Regular));
    }
}

