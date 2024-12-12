namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    public interface IFormatConditionRuleIconSet : IFormatConditionRuleBase
    {
        bool Percent { get; }

        bool Reverse { get; }

        bool ShowValues { get; }

        XlCondFmtIconSetType IconSetType { get; }

        IList<XlCondFmtValueObject> Values { get; }

        IList<XlCondFmtCustomIcon> Icons { get; }
    }
}

