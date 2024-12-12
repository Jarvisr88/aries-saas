namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    public sealed class DXWebFontInfo
    {
        private DXWebStyle owner;

        internal DXWebFontInfo(DXWebStyle owner)
        {
            this.owner = owner;
        }

        public void ClearDefaults()
        {
            if (this.Names.Length == 0)
            {
                this.owner.ViewState.Remove("Font_Names");
                this.owner.ClearBit(0x200);
            }
            if (this.Size == DXWebFontUnit.Empty)
            {
                this.owner.ViewState.Remove("Font_Size");
                this.owner.ClearBit(0x400);
            }
            if (!this.Bold)
            {
                this.ResetBold();
            }
            if (!this.Italic)
            {
                this.ResetItalic();
            }
            if (!this.Underline)
            {
                this.ResetUnderline();
            }
            if (!this.Overline)
            {
                this.ResetOverline();
            }
            if (!this.Strikeout)
            {
                this.ResetStrikeout();
            }
        }

        public void CopyFrom(DXWebFontInfo f)
        {
            if (f != null)
            {
                DXWebStyle owner = f.Owner;
                if (owner.RegisteredCssClass.Length == 0)
                {
                    if (owner.IsSet(0x200))
                    {
                        this.Names = f.Names;
                    }
                    if (owner.IsSet(0x400) && (f.Size != DXWebFontUnit.Empty))
                    {
                        this.Size = f.Size;
                    }
                    if (owner.IsSet(0x800))
                    {
                        this.Bold = f.Bold;
                    }
                    if (owner.IsSet(0x1000))
                    {
                        this.Italic = f.Italic;
                    }
                    if (owner.IsSet(0x4000))
                    {
                        this.Overline = f.Overline;
                    }
                    if (owner.IsSet(0x8000))
                    {
                        this.Strikeout = f.Strikeout;
                    }
                    if (owner.IsSet(0x2000))
                    {
                        this.Underline = f.Underline;
                    }
                }
                else
                {
                    if (owner.IsSet(0x200))
                    {
                        this.ResetNames();
                    }
                    if (owner.IsSet(0x400) && (f.Size != DXWebFontUnit.Empty))
                    {
                        this.ResetFontSize();
                    }
                    if (owner.IsSet(0x800))
                    {
                        this.ResetBold();
                    }
                    if (owner.IsSet(0x1000))
                    {
                        this.ResetItalic();
                    }
                    if (owner.IsSet(0x4000))
                    {
                        this.ResetOverline();
                    }
                    if (owner.IsSet(0x8000))
                    {
                        this.ResetStrikeout();
                    }
                    if (owner.IsSet(0x2000))
                    {
                        this.ResetUnderline();
                    }
                }
            }
        }

        public void MergeWith(DXWebFontInfo f)
        {
            if (f != null)
            {
                DXWebStyle owner = f.Owner;
                if (owner.RegisteredCssClass.Length == 0)
                {
                    if (owner.IsSet(0x200) && !this.owner.IsSet(0x200))
                    {
                        this.Names = f.Names;
                    }
                    if (owner.IsSet(0x400) && (!this.owner.IsSet(0x400) || (this.Size == DXWebFontUnit.Empty)))
                    {
                        this.Size = f.Size;
                    }
                    if (owner.IsSet(0x800) && !this.owner.IsSet(0x800))
                    {
                        this.Bold = f.Bold;
                    }
                    if (owner.IsSet(0x1000) && !this.owner.IsSet(0x1000))
                    {
                        this.Italic = f.Italic;
                    }
                    if (owner.IsSet(0x4000) && !this.owner.IsSet(0x4000))
                    {
                        this.Overline = f.Overline;
                    }
                    if (owner.IsSet(0x8000) && !this.owner.IsSet(0x8000))
                    {
                        this.Strikeout = f.Strikeout;
                    }
                    if (owner.IsSet(0x2000) && !this.owner.IsSet(0x2000))
                    {
                        this.Underline = f.Underline;
                    }
                }
            }
        }

        internal void Reset()
        {
            if (this.owner.IsSet(0x200))
            {
                this.ResetNames();
            }
            if (this.owner.IsSet(0x400))
            {
                this.ResetFontSize();
            }
            if (this.owner.IsSet(0x800))
            {
                this.ResetBold();
            }
            if (this.owner.IsSet(0x1000))
            {
                this.ResetItalic();
            }
            if (this.owner.IsSet(0x2000))
            {
                this.ResetUnderline();
            }
            if (this.owner.IsSet(0x4000))
            {
                this.ResetOverline();
            }
            if (this.owner.IsSet(0x8000))
            {
                this.ResetStrikeout();
            }
        }

        private void ResetBold()
        {
            this.owner.ViewState.Remove("Font_Bold");
            this.owner.ClearBit(0x800);
        }

        private void ResetFontSize()
        {
            this.owner.ViewState.Remove("Font_Size");
            this.owner.ClearBit(0x400);
        }

        private void ResetItalic()
        {
            this.owner.ViewState.Remove("Font_Italic");
            this.owner.ClearBit(0x1000);
        }

        private void ResetNames()
        {
            this.owner.ViewState.Remove("Font_Names");
            this.owner.ClearBit(0x200);
        }

        private void ResetOverline()
        {
            this.owner.ViewState.Remove("Font_Overline");
            this.owner.ClearBit(0x4000);
        }

        private void ResetStrikeout()
        {
            this.owner.ViewState.Remove("Font_Strikeout");
            this.owner.ClearBit(0x8000);
        }

        private void ResetUnderline()
        {
            this.owner.ViewState.Remove("Font_Underline");
            this.owner.ClearBit(0x2000);
        }

        private bool ShouldSerializeBold() => 
            this.owner.IsSet(0x800);

        private bool ShouldSerializeItalic() => 
            this.owner.IsSet(0x1000);

        public bool ShouldSerializeNames() => 
            this.Names.Length != 0;

        private bool ShouldSerializeOverline() => 
            this.owner.IsSet(0x4000);

        private bool ShouldSerializeStrikeout() => 
            this.owner.IsSet(0x8000);

        private bool ShouldSerializeUnderline() => 
            this.owner.IsSet(0x2000);

        public override string ToString()
        {
            string str = this.Size.ToString(CultureInfo.InvariantCulture);
            string name = this.Name;
            return ((str.Length != 0) ? ((name.Length == 0) ? str : $"{name}, {str}") : name);
        }

        public bool Bold
        {
            get => 
                this.owner.IsSet(0x800) && ((bool) this.owner.ViewState["Font_Bold"]);
            set
            {
                this.owner.ViewState["Font_Bold"] = value;
                this.owner.SetBit(0x800);
            }
        }

        public bool Italic
        {
            get => 
                this.owner.IsSet(0x1000) && ((bool) this.owner.ViewState["Font_Italic"]);
            set
            {
                this.owner.ViewState["Font_Italic"] = value;
                this.owner.SetBit(0x1000);
            }
        }

        public string Name
        {
            get
            {
                string[] names = this.Names;
                return ((names.Length == 0) ? string.Empty : names[0]);
            }
            set
            {
                Guard.ArgumentNotNull(value, "value");
                if (value.Length == 0)
                {
                    this.Names = null;
                }
                else
                {
                    this.Names = new string[] { value };
                }
            }
        }

        public string[] Names
        {
            get
            {
                if (this.owner.IsSet(0x200))
                {
                    string[] strArray = this.owner.ViewState["Font_Names"] as string[];
                    if (strArray != null)
                    {
                        return strArray;
                    }
                }
                return new string[0];
            }
            set
            {
                this.owner.ViewState["Font_Names"] = value;
                this.owner.SetBit(0x200);
            }
        }

        public bool Overline
        {
            get => 
                this.owner.IsSet(0x4000) && ((bool) this.owner.ViewState["Font_Overline"]);
            set
            {
                this.owner.ViewState["Font_Overline"] = value;
                this.owner.SetBit(0x4000);
            }
        }

        internal DXWebStyle Owner =>
            this.owner;

        public DXWebFontUnit Size
        {
            get => 
                !this.owner.IsSet(0x400) ? DXWebFontUnit.Empty : ((DXWebFontUnit) this.owner.ViewState["Font_Size"]);
            set
            {
                if ((value.Type == DXWebFontSize.AsUnit) && (value.Unit.Value < 0.0))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.owner.ViewState["Font_Size"] = value;
                this.owner.SetBit(0x400);
            }
        }

        public bool Strikeout
        {
            get => 
                this.owner.IsSet(0x8000) && ((bool) this.owner.ViewState["Font_Strikeout"]);
            set
            {
                this.owner.ViewState["Font_Strikeout"] = value;
                this.owner.SetBit(0x8000);
            }
        }

        public bool Underline
        {
            get => 
                this.owner.IsSet(0x2000) && ((bool) this.owner.ViewState["Font_Underline"]);
            set
            {
                this.owner.ViewState["Font_Underline"] = value;
                this.owner.SetBit(0x2000);
            }
        }
    }
}

