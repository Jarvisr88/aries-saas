namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface IVisualBrick : IBaseBrick, IBrick
    {
        Color BackColor { get; set; }

        Color BorderColor { get; set; }

        BrickBorderStyle BorderStyle { get; set; }

        float BorderWidth { get; set; }

        DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle { get; set; }

        PaddingInfo Padding { get; set; }

        BorderSide Sides { get; set; }

        BrickStyle Style { get; set; }

        bool SeparableHorz { get; set; }

        bool SeparableVert { get; set; }

        bool Separable { get; set; }

        bool RepeatForVerticallySplitContent { get; set; }

        string TextValueFormatString { get; set; }

        object TextValue { get; set; }

        string Text { get; set; }

        bool UseTextAsDefaultHint { get; set; }
    }
}

