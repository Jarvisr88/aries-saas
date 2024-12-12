namespace DevExpress.Utils.Text
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class StringCalculateArgsBase
    {
        private string text;
        private Rectangle bounds;
        private object context;
        private bool allowSimpleString;
        private bool allowBaselineAlignment;

        public StringCalculateArgsBase(string text, Rectangle bounds, object context)
        {
            this.text = text;
            this.bounds = bounds;
            this.context = context;
            this.allowSimpleString = true;
            this.allowBaselineAlignment = true;
            this.HyperlinkSettings = new DevExpress.Utils.Text.HyperlinkSettings();
            this.DpiScaleFactor = 1f;
            this.AllowPartiallyVisibleRows = true;
        }

        public float DpiScaleFactor { get; set; }

        public DevExpress.Utils.Text.HyperlinkSettings HyperlinkSettings { get; set; }

        public Color HyperlinkColor
        {
            get => 
                this.HyperlinkSettings.Color;
            set => 
                this.HyperlinkSettings.Color = value;
        }

        public bool RoundTextHeight { get; set; }

        public bool AllowSimpleString
        {
            get => 
                this.allowSimpleString;
            set => 
                this.allowSimpleString = value;
        }

        public bool AllowBaselineAlignment
        {
            get => 
                this.allowBaselineAlignment;
            set => 
                this.allowBaselineAlignment = value;
        }

        public bool AllowPartiallyVisibleRows { get; set; }

        public bool DisableWrapNonFitText { get; set; }

        public string Text =>
            this.text;

        public Rectangle Bounds =>
            this.bounds;

        public object Context =>
            this.context;
    }
}

