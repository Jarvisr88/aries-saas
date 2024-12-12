namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;

    public class FormatConditionRuleDateOccuringExportWrapper : FormatConditionRuleBase, IFormatConditionRuleDateOccuring, IFormatConditionRuleBase
    {
        private readonly XlCondFmtTimePeriod dateTypeCore;
        private readonly XlDifferentialFormatting formattingCore;

        public FormatConditionRuleDateOccuringExportWrapper(Format format, DateOccurringConditionRule rule)
        {
            this.formattingCore = new FormatConverter(format).GetXlDifferentialFormatting();
            this.dateTypeCore = GetTimePeriodByDateOccurringRule(rule).Value;
        }

        public static XlCondFmtTimePeriod? GetTimePeriodByDateOccurringRule(DateOccurringConditionRule rule)
        {
            switch (rule)
            {
                case DateOccurringConditionRule.Yesterday:
                    return 9;

                case DateOccurringConditionRule.Today:
                    return 7;

                case DateOccurringConditionRule.Tomorrow:
                    return 8;

                case DateOccurringConditionRule.InTheLast7Days:
                    return 0;

                case DateOccurringConditionRule.LastWeek:
                    return 2;

                case DateOccurringConditionRule.ThisWeek:
                    return 6;

                case DateOccurringConditionRule.NextWeek:
                    return 4;

                case DateOccurringConditionRule.LastMonth:
                    return 1;

                case DateOccurringConditionRule.ThisMonth:
                    return 5;

                case DateOccurringConditionRule.NextMonth:
                    return 3;
            }
            return null;
        }

        public IEnumerable<XlCondFmtTimePeriod> DateTypes =>
            new List<XlCondFmtTimePeriod> { this.dateTypeCore };

        public XlDifferentialFormatting Formatting =>
            this.formattingCore;
    }
}

