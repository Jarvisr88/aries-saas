namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    internal class BrickStyleKey
    {
        public override bool Equals(object obj)
        {
            BrickStyleKey key = obj as BrickStyleKey;
            return ((key == null) ? base.Equals(obj) : this.EqualsCore(key));
        }

        private bool EqualsCore(BrickStyleKey key) => 
            Equals(this.BackColor, key.BackColor) && (Equals(this.ForeColor, key.ForeColor) && (Equals(this.BorderColor, key.BorderColor) && (Equals(this.BorderThickness, key.BorderThickness) && (Equals(this.FontFamily, key.FontFamily) && (Equals(this.FontStyle, key.FontStyle) && (Equals(this.FontWeight, key.FontWeight) && (Equals(this.FontSize, key.FontSize) && (Equals(this.Padding, key.Padding) && (Equals(this.HorizontalAlignment, key.HorizontalAlignment) && (Equals(this.VerticalAlignment, key.VerticalAlignment) && (Equals(this.TextWrapping, key.TextWrapping) && (Equals(this.TextDecorations, key.TextDecorations) && (Equals(this.BorderDashStyle, key.BorderDashStyle) && (Equals(this.TextTrimming, key.TextTrimming) && Equals(this.FlowDirection, key.FlowDirection)))))))))))))));

        public override int GetHashCode() => 
            0;

        public Color BackColor { get; set; }

        public Color ForeColor { get; set; }

        public Color BorderColor { get; set; }

        public Thickness BorderThickness { get; set; }

        public DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle { get; set; }

        public System.Windows.Media.FontFamily FontFamily { get; set; }

        public System.Windows.FontStyle FontStyle { get; set; }

        public System.Windows.FontWeight FontWeight { get; set; }

        public double FontSize { get; set; }

        public Thickness Padding { get; set; }

        public System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }

        public System.Windows.VerticalAlignment VerticalAlignment { get; set; }

        public System.Windows.TextWrapping TextWrapping { get; set; }

        public BrickTextDecorations TextDecorations { get; set; }

        public System.Windows.TextTrimming TextTrimming { get; set; }

        public System.Windows.FlowDirection FlowDirection { get; set; }
    }
}

