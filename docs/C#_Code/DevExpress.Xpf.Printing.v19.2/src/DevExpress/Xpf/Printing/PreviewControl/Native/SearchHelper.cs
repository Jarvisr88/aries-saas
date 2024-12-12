namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native.Navigation;
    using System;

    public class SearchHelper
    {
        private readonly SearchHelperBase searchHelper = new SearchHelperBase();
        private string actualText;
        private bool actualCaseSensitive;
        private bool actualWholeWord;
        private SearchDirection actualDirection;

        private void AssignParameters(TextSearchParameter searchParameter)
        {
            this.ActualText = searchParameter.Text;
            this.ActualCaseSensitive = searchParameter.IsCaseSensitive;
            this.ActualWholeWord = searchParameter.WholeWord;
            this.ActualDirection = (searchParameter.SearchDirection == TextSearchDirection.Forward) ? SearchDirection.Down : SearchDirection.Up;
        }

        public BrickPagePair FindNext(PrintingSystemBase ps, TextSearchParameter searchParameter)
        {
            this.AssignParameters(searchParameter);
            return this.searchHelper.CircleFindNext(ps, this.ActualText, this.ActualDirection, this.ActualWholeWord, this.ActualCaseSensitive);
        }

        private string ActualText
        {
            get => 
                this.actualText;
            set
            {
                if (this.actualText != value)
                {
                    this.actualText = value;
                    this.searchHelper.ResetSearchResults();
                }
            }
        }

        private bool ActualCaseSensitive
        {
            get => 
                this.actualCaseSensitive;
            set
            {
                if (this.actualCaseSensitive != value)
                {
                    this.actualCaseSensitive = value;
                    this.searchHelper.ResetSearchResults();
                }
            }
        }

        private bool ActualWholeWord
        {
            get => 
                this.actualWholeWord;
            set
            {
                if (this.actualWholeWord != value)
                {
                    this.actualWholeWord = value;
                    this.searchHelper.ResetSearchResults();
                }
            }
        }

        private SearchDirection ActualDirection
        {
            get => 
                this.actualDirection;
            set
            {
                if (this.actualDirection != value)
                {
                    this.actualDirection = value;
                }
            }
        }
    }
}

