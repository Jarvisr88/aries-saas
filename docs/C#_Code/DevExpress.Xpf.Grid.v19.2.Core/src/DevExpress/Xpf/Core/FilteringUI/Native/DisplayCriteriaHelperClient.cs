namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using System;

    internal class DisplayCriteriaHelperClient
    {
        public readonly Func<string, DisplayCriteriaColumnTraits> GetColumn;
        public readonly Func<bool> HasCustomColumnDisplayTextSubscription;
        public readonly Func<IEnumerable<FormatConditionFilter>> GetFormatConditionFilters;

        public DisplayCriteriaHelperClient(Func<string, DisplayCriteriaColumnTraits> getColumn, Func<bool> hasCustomColumnDisplayTextSubscription, Func<IEnumerable<FormatConditionFilter>> getFormatConditionFilters)
        {
            this.GetColumn = getColumn;
            this.HasCustomColumnDisplayTextSubscription = hasCustomColumnDisplayTextSubscription;
            this.GetFormatConditionFilters = getFormatConditionFilters;
        }
    }
}

