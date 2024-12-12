namespace DevExpress.Utils.Text.Internal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class StringFontSettings
    {
        private bool styleSet;
        private bool styleModified;
        private FontStyle style = FontStyle.Regular;
        private System.Drawing.Color color;
        private System.Drawing.Color backColor;
        private float size;
        private List<StringBlockTextModifierInfo> modifiers;

        public StringFontSettings()
        {
            this.backColor = this.color = System.Drawing.Color.Empty;
            this.size = 0f;
        }

        public void AddModifier(StringBlockTextModifier modifier, float height)
        {
            this.modifiers ??= new List<StringBlockTextModifierInfo>();
            StringBlockTextModifierInfo item = new StringBlockTextModifierInfo();
            item.Modifier = modifier;
            item.Size = height;
            this.Modifiers.Add(item);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddStyle(FontStyle style)
        {
            this.SetStyle(this.Style | style);
            int styleLevel = this.StyleLevel;
            this.StyleLevel = styleLevel + 1;
        }

        internal void Assign(StringFontSettings source)
        {
            this.styleSet = source.styleSet;
            this.styleModified = source.styleModified;
            this.style = source.style;
            this.color = source.color;
            this.size = source.size;
            this.backColor = source.backColor;
            this.StyleLevel = source.StyleLevel;
            this.PrevStyle = source.PrevStyle;
            this.FontFamily = source.FontFamily;
            if (source.modifiers != null)
            {
                this.modifiers = new List<StringBlockTextModifierInfo>(source.Modifiers.Count);
                this.modifiers.AddRange(source.Modifiers);
            }
        }

        internal StringFontSettings Clone()
        {
            StringFontSettings settings = new StringFontSettings();
            settings.Assign(this);
            return settings;
        }

        public bool IsEquals(StringFontSettings settings) => 
            (settings.Style == this.Style) && ((settings.Color == this.Color) && ((settings.Size == this.Size) && ((settings.BackColor == this.BackColor) && ((settings.FontFamily == this.FontFamily) && (settings.StyleLevel == this.StyleLevel)))));

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public void ModifyStyle(FontStyle style)
        {
            this.style |= style;
            this.styleModified = true;
        }

        public void PopStyle()
        {
            this.style = this.PrevStyle;
            int styleLevel = this.StyleLevel;
            this.StyleLevel = styleLevel - 1;
            if (this.StyleLevel == 0)
            {
                this.styleModified = false;
                this.styleSet = false;
            }
        }

        public void PushStyle(FontStyle fontStyle)
        {
            this.PrevStyle = this.Style;
            this.SetStyle(fontStyle);
            int styleLevel = this.StyleLevel;
            this.StyleLevel = styleLevel + 1;
        }

        public void RemoveLastModifier(StringBlockTextModifier modifierToRemove)
        {
            if (this.modifiers != null)
            {
                for (int i = this.Modifiers.Count - 1; i >= 0; i--)
                {
                    if (this.Modifiers[i].Modifier == modifierToRemove)
                    {
                        this.Modifiers.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveStyle(FontStyle style)
        {
            this.SetStyle(this.Style & ~style);
            int styleLevel = this.StyleLevel;
            this.StyleLevel = styleLevel - 1;
            if (this.StyleLevel == 0)
            {
                this.styleModified = false;
                this.styleSet = false;
            }
        }

        internal void ResetStyle(FontStyle style)
        {
            this.style = style;
            this.styleModified = false;
            this.styleSet = false;
        }

        internal void SetBackColor(System.Drawing.Color backColor)
        {
            this.backColor = backColor;
        }

        internal void SetColor(System.Drawing.Color color)
        {
            this.color = color;
        }

        internal void SetSize(float size)
        {
            this.size = size;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetStyle(FontStyle style)
        {
            this.style = style;
            this.styleSet = true;
        }

        public FontStyle Style =>
            this.style;

        public System.Drawing.Color Color =>
            this.color;

        public System.Drawing.Color BackColor =>
            this.backColor;

        public float Size =>
            this.size;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStyleSet
        {
            get => 
                this.styleSet;
            internal set => 
                this.styleSet = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStyleModified
        {
            get => 
                this.styleModified;
            internal set => 
                this.styleModified = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int StyleLevel { get; set; }

        public List<StringBlockTextModifierInfo> Modifiers =>
            this.modifiers;

        public bool HasModifiers =>
            (this.modifiers != null) && (this.modifiers.Count > 0);

        protected FontStyle PrevStyle { get; set; }

        public string FontFamily { get; internal set; }
    }
}

