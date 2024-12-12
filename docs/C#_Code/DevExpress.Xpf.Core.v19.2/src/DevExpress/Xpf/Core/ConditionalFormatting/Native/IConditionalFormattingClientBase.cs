namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System.Collections.Generic;

    public interface IConditionalFormattingClientBase
    {
        IList<FormatConditionBaseInfo> GetRelatedConditions();
    }
}

