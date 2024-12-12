namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class RenderFontSettings : FreezableRenderObject
    {
        private static readonly double DefaultFontSize;
        private static readonly System.Windows.Media.FontFamily DefaultFontFamily;
        private static readonly System.Windows.FontStretch DefaultFontStretch;
        private static readonly System.Windows.FontStyle DefaultFontStyle;
        private static readonly System.Windows.FontWeight DefaultFontWeight;
        private double? fontSize;
        private System.Windows.Media.FontFamily fontFamily;
        private System.Windows.FontStretch? fontStretch;
        private System.Windows.FontStyle? fontStyle;
        private System.Windows.FontWeight? fontWeight;

        static RenderFontSettings();
        public RenderFontSettings();
        public RenderFontSettings(double? fontSize = new double?(), System.Windows.Media.FontFamily fontFamily = null, System.Windows.FontStretch? fontStretch = new System.Windows.FontStretch?(), System.Windows.FontStyle? fontStyle = new System.Windows.FontStyle?(), System.Windows.FontWeight? fontWeight = new System.Windows.FontWeight?());
        protected bool Equals(RenderFontSettings other);
        public override bool Equals(object obj);
        protected override void FreezeOverride();
        public override int GetHashCode();
        public RenderFontSettings Update(double? fontSize = new double?(), System.Windows.Media.FontFamily fontFamily = null, System.Windows.FontStretch? fontStretch = new System.Windows.FontStretch?(), System.Windows.FontStyle? fontStyle = new System.Windows.FontStyle?(), System.Windows.FontWeight? fontWeight = new System.Windows.FontWeight?());

        public double? FontSize { get; set; }

        public System.Windows.Media.FontFamily FontFamily { get; set; }

        public System.Windows.FontStretch? FontStretch { get; set; }

        public System.Windows.FontStyle? FontStyle { get; set; }

        public System.Windows.FontWeight? FontWeight { get; set; }

        public bool IsGlyphsCompatible { get; }

        public bool IsEmpty { get; }
    }
}

