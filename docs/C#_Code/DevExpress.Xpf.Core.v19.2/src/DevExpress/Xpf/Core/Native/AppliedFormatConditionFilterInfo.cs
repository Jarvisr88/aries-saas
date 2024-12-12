namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class AppliedFormatConditionFilterInfo
    {
        public AppliedFormatConditionFilterInfo(FormatConditionFilterInfo info, string propertyName, bool applyToRow);

        public FormatConditionFilterInfo Info { get; }

        public string PropertyName { get; }

        public bool ApplyToRow { get; }
    }
}

