namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;

    public class FormatConditionFilter : ImmutableObject
    {
        public readonly Func<CriteriaOperator> GetSubstitutionFilter;
        public readonly FormatConditionFilterInfo Info;
        public readonly bool ApplyToRow;
        public readonly object Source;
        private readonly Lazy<string> displayExpression;

        public FormatConditionFilter(object source, Func<CriteriaOperator> getSubstitutionFilter, Func<string> getDisplayExpression, DevExpress.Xpf.Core.ConditionalFormatting.Format format, FormatConditionFilterInfo info, bool applyToRow);

        public string DisplayExpression { get; }

        public DevExpress.Xpf.Core.ConditionalFormatting.Format Format { get; }
    }
}

