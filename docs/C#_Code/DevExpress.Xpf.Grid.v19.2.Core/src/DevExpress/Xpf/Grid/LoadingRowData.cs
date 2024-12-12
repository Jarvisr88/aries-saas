namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Windows;

    public class LoadingRowData : RowData
    {
        private string errorTextCore;
        private bool hasErrorCore;
        private bool allowRetryCore;
        private bool allowLoadMoreCore;
        private bool showLoadingIndicatorCore;

        public LoadingRowData(DataTreeBuilder builder) : base(builder, false, false, false)
        {
            this.showLoadingIndicatorCore = true;
        }

        protected override void AssignFromCore(NodeContainer nodeContainer, RowNode rowNode)
        {
            base.AssignFromCore(nodeContainer, rowNode);
            this.UpdateLoadingState();
        }

        protected override FrameworkElement CreateRowElement() => 
            base.View.CreateLoadingRowElement(this);

        public void LoadMoreRows()
        {
            if (base.View.DataProviderBase != null)
            {
                base.View.DataProviderBase.FetchMoreRows();
            }
        }

        public void UpdateLoadingState()
        {
            if (base.View.DataProviderBase != null)
            {
                base.View.DataProviderBase.UpdateLoadingRowState(this);
            }
        }

        public void UpdateLoadingState(bool areRowsFetching, string errorText, bool allowRetry, bool allowLoadMore)
        {
            this.ErrorText = errorText;
            this.HasError = !string.IsNullOrEmpty(this.ErrorText);
            this.AllowRetry = allowRetry;
            this.ShowLoadingIndicator = areRowsFetching;
            this.AllowLoadMore = allowLoadMore;
        }

        public string ErrorText
        {
            get => 
                this.errorTextCore;
            protected set
            {
                if (this.ErrorText != value)
                {
                    this.errorTextCore = value;
                    base.RaisePropertyChanged("ErrorText");
                }
            }
        }

        public bool HasError
        {
            get => 
                this.hasErrorCore;
            protected set
            {
                if (this.HasError != value)
                {
                    this.hasErrorCore = value;
                    base.RaisePropertyChanged("HasError");
                }
            }
        }

        public bool AllowRetry
        {
            get => 
                this.allowRetryCore;
            protected set
            {
                if (this.AllowRetry != value)
                {
                    this.allowRetryCore = value;
                    base.RaisePropertyChanged("AllowRetry");
                }
            }
        }

        public bool AllowLoadMore
        {
            get => 
                this.allowLoadMoreCore;
            protected set
            {
                if (this.AllowLoadMore != value)
                {
                    this.allowLoadMoreCore = value;
                    base.RaisePropertyChanged("AllowLoadMore");
                }
            }
        }

        public bool ShowLoadingIndicator
        {
            get => 
                this.showLoadingIndicatorCore;
            protected set
            {
                if (this.ShowLoadingIndicator != value)
                {
                    this.showLoadingIndicatorCore = value;
                    base.RaisePropertyChanged("ShowLoadingIndicator");
                }
            }
        }
    }
}

