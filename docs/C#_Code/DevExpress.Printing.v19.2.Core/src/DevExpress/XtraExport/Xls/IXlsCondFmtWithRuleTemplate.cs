namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;

    public interface IXlsCondFmtWithRuleTemplate
    {
        XlsCFRuleTemplate RuleTemplate { get; set; }

        bool FilterTop { get; set; }

        bool FilterPercent { get; set; }

        int FilterValue { get; set; }

        XlCondFmtSpecificTextType TextRule { get; set; }

        int StdDev { get; set; }
    }
}

