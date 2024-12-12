namespace DevExpress.XtraExport.Helpers
{
    using System.Drawing;

    public interface IFormatConditionRuleColorScaleBase : IFormatConditionRuleBase, IFormatConditionRuleMinMaxBase
    {
        Color MinColor { get; }

        Color MaxColor { get; }
    }
}

