namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;

    public interface IFormatConditionRuleDataBar : IFormatConditionRuleBase, IFormatConditionRuleMinMaxBase
    {
        Color AxisColor { get; }

        Color FillColor { get; }

        Color BorderColor { get; }

        Color NegativeFillColor { get; }

        Color NegativeBorderColor { get; }

        bool AllowNegativeAxis { get; }

        bool ShowBarOnly { get; }

        bool GradientFill { get; }

        bool DrawAxis { get; }

        bool DrawAxisAtMiddle { get; }

        string PredefinedName { get; }

        int Direction { get; }
    }
}

