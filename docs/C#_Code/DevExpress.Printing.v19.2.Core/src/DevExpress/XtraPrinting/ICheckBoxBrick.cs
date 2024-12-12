namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public interface ICheckBoxBrick
    {
        System.Windows.Forms.CheckState CheckState { get; set; }

        DevExpress.XtraPrinting.GlyphStyle GlyphStyle { get; set; }

        SizeF GlyphSize { get; set; }

        CheckBoxGlyphCollection CustomGlyphs { get; }
    }
}

