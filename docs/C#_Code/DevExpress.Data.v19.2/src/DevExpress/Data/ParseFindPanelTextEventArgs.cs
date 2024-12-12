namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ParseFindPanelTextEventArgs : HandledEventArgs
    {
        internal CriteriaOperator _FindCriteria;
        internal Func<string, string, DisplayTextHighlightRange[]> _Highlighter;

        public ParseFindPanelTextEventArgs(string findPanelText);
        public void SetFindCriteria(CriteriaOperator findCriteria);
        public virtual void SetFindCriteriaAndHighlightedRangesGetterFromDisplayText(CriteriaOperator findCriteria, Func<string, DisplayTextHighlightRange?> highlightedRangeGetterFromDisplayText);
        public virtual void SetFindCriteriaAndHighlightedRangesGetterFromDisplayText(CriteriaOperator findCriteria, Func<string, DisplayTextHighlightRange[]> highlightedRangesGetterFromDisplayText);
        public virtual void SetFindCriteriaAndHighlightedRangesGetterFromDisplayTextAndFieldName(CriteriaOperator findCriteria, Func<string, string, DisplayTextHighlightRange?> highlightedRangeGetterFromDisplayTextAndFieldName);
        public virtual void SetFindCriteriaAndHighlightedRangesGetterFromDisplayTextAndFieldName(CriteriaOperator findCriteria, Func<string, string, DisplayTextHighlightRange[]> highlightedRangesGetterFromDisplayTextAndFieldName);
        private void SetFindCriteriaWithNoHighlight(CriteriaOperator findCriteria);

        public virtual string FindPanelText { get; }
    }
}

