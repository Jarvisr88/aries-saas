namespace DevExpress.Office.Drawing
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class TextRendererDrawingTextCharacterProperties : IDrawingTextCharacterProperties
    {
        private IDrawingFill innerFill;

        public TextRendererDrawingTextCharacterProperties(DrawingTextCharacterProperties innerProperties)
        {
            this.<InnerProperties>k__BackingField = innerProperties;
        }

        public DrawingTextCharacterProperties InnerProperties { get; }

        public ContainerEffect Effects =>
            this.InnerProperties.Effects;

        public int Baseline =>
            this.InnerProperties.Baseline;

        public DrawingTextUnderlineType Underline =>
            this.InnerProperties.Underline;

        public IDrawingFill Fill
        {
            get => 
                this.innerFill ?? this.InnerProperties.Fill;
            set => 
                this.innerFill = value;
        }

        public IUnderlineFill UnderlineFill =>
            this.InnerProperties.UnderlineFill;

        public DevExpress.Office.Drawing.Outline Outline =>
            this.InnerProperties.Outline;

        public DrawingTextStrikeType Strikethrough =>
            this.InnerProperties.Strikethrough;

        public int Spacing =>
            this.InnerProperties.Spacing;

        public bool NormalizeHeight =>
            this.InnerProperties.NormalizeHeight;

        public bool Bold =>
            this.InnerProperties.Bold;

        public CultureInfo Language =>
            this.InnerProperties.Language;

        public DrawingTextFont Latin =>
            this.InnerProperties.Latin;

        public ITextCharacterOptions Options =>
            this.InnerProperties.Options;

        public DrawingTextCapsType Caps =>
            this.InnerProperties.Caps;

        public bool Italic =>
            this.InnerProperties.Italic;

        public int FontSize =>
            this.InnerProperties.FontSize;
    }
}

