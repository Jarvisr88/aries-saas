namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Navigation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    public class NavigationPaneSearchModel
    {
        private CancellationTokenSource tokenSource;
        private readonly INavigationPaneUI navigationPaneUI;
        private readonly ObservableRangeCollection<SearchData> searchResults = new ObservableRangeCollection<SearchData>();
        private readonly ObservableRangeCollection<int> resultPageIndices = new ObservableRangeCollection<int>();
        private readonly SearchHelperBase searchHelper = new SearchHelperBase();
        private readonly ImmediateSingleActionManager singleActionManager;

        protected NavigationPaneSearchModel(INavigationPaneUI navigationPaneUI)
        {
            this.navigationPaneUI = navigationPaneUI;
            this.singleActionManager = new ImmediateSingleActionManager(() => this.Search(), 800.0);
            this.BookmarkNodes = navigationPaneUI.BookmarkNodes;
        }

        protected bool CanNavigateToNextResult(TextSearchDirection direction) => 
            this.searchResults.Any<SearchData>();

        protected internal bool CanStopSearch() => 
            this.SearchState == DevExpress.Xpf.Printing.SearchState.InProgress;

        public static NavigationPaneSearchModel Create(INavigationPaneUI navigationPaneUI)
        {
            <>c__DisplayClass42_0 class_;
            Expression[] expressionArray1 = new Expression[] { Expression.Field(Expression.Constant(class_, typeof(<>c__DisplayClass42_0)), fieldof(<>c__DisplayClass42_0.navigationPaneUI)) };
            return ViewModelSource.Create<NavigationPaneSearchModel>(Expression.Lambda<Func<NavigationPaneSearchModel>>(Expression.New((ConstructorInfo) methodof(NavigationPaneSearchModel..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]));
        }

        private SearchData GetNextSelection(TextSearchDirection searchDirection) => 
            (searchDirection != TextSearchDirection.Forward) ? ((this.SelectedSearchResult == this.SearchResults.First<SearchData>()) ? this.SearchResults.Last<SearchData>() : this.searchResults[this.searchResults.IndexOf(this.SelectedSearchResult) - 1]) : ((this.SelectedSearchResult == this.SearchResults.Last<SearchData>()) ? this.SearchResults.First<SearchData>() : this.searchResults[this.searchResults.IndexOf(this.SelectedSearchResult) + 1]);

        internal void InvalidateSearch()
        {
            if ((this.SearchState == DevExpress.Xpf.Printing.SearchState.InProgress) && (this.tokenSource != null))
            {
                this.tokenSource.Cancel();
            }
            this.SearchState = DevExpress.Xpf.Printing.SearchState.None;
            this.searchResults.Clear();
            this.SelectedSearchResult = null;
            foreach (BookmarkNodeItem item in this.BookmarkNodes)
            {
                item.IsHighlighted = false;
            }
            this.resultPageIndices.Clear();
        }

        private void NavigateToBrick(TextSearchDirection searchDirection)
        {
            int index = this.searchResults.IndexOf(this.SelectedSearchResult);
            this.SelectedSearchResult = this.searchResults[(searchDirection == TextSearchDirection.Forward) ? (index + 1) : (index - 1)];
            NavigationPaneSearchModel model1 = this;
            if (<>c.<>9__52_0 == null)
            {
                model1 = (NavigationPaneSearchModel) (<>c.<>9__52_0 = x => x.BrickPagePair);
            }
            <>c.<>9__52_0.SelectBrick(this.SelectedSearchResult.With<SearchData, BrickPagePair>((Func<SearchData, BrickPagePair>) model1));
        }

        public void NavigateToNextResult(TextSearchDirection searchDirection)
        {
            if ((this.navigationPaneUI.ActiveTab != NavigationPaneTabType.DocumentMap) || !this.BookmarkNodes.Any<object>())
            {
                if (this.navigationPaneUI.ActiveTab != NavigationPaneTabType.Pages)
                {
                    this.SelectedSearchResult = (this.SelectedSearchResult != null) ? this.GetNextSelection(searchDirection) : ((searchDirection == TextSearchDirection.Forward) ? this.SearchResults.FirstOrDefault<SearchData>() : this.SearchResults.LastOrDefault<SearchData>());
                    NavigationPaneSearchModel model2 = this;
                    if (<>c.<>9__49_6 == null)
                    {
                        model2 = (NavigationPaneSearchModel) (<>c.<>9__49_6 = x => x.BrickPagePair);
                    }
                    <>c.<>9__49_6.SelectBrick(this.SelectedSearchResult.With<SearchData, BrickPagePair>((Func<SearchData, BrickPagePair>) model2));
                }
                else if ((this.SelectedSearchResult != null) && ((this.SelectedSearchResult.PageIndex + 1) == this.navigationPaneUI.CurrentPageNumber))
                {
                    this.SelectedSearchResult = this.GetNextSelection(searchDirection);
                    Func<SearchData, BrickPagePair> evaluator = <>c.<>9__49_3;
                    if (<>c.<>9__49_3 == null)
                    {
                        Func<SearchData, BrickPagePair> local3 = <>c.<>9__49_3;
                        evaluator = <>c.<>9__49_3 = x => x.BrickPagePair;
                    }
                    this.SelectBrick(this.SelectedSearchResult.With<SearchData, BrickPagePair>(evaluator));
                }
                else
                {
                    SearchData data = (from x in this.SearchResults
                        where (x.BrickPagePair.PageIndex + 1) == this.navigationPaneUI.CurrentPageNumber
                        select x).FirstOrDefault<SearchData>() ?? this.SearchResults.FirstOrDefault<SearchData>();
                    this.SelectedSearchResult = data;
                    NavigationPaneSearchModel model1 = this;
                    if (<>c.<>9__49_5 == null)
                    {
                        model1 = (NavigationPaneSearchModel) (<>c.<>9__49_5 = x => x.BrickPagePair);
                    }
                    <>c.<>9__49_5.SelectBrick(this.SelectedSearchResult.With<SearchData, BrickPagePair>((Func<SearchData, BrickPagePair>) model1));
                }
            }
            else
            {
                BookmarkNodeItem selectedBookmarkItem = (BookmarkNodeItem) this.navigationPaneUI.SelectedBookmarkNode;
                IEnumerable<SearchData> source = from x in this.SearchResults
                    where ReferenceEquals(x.BookmarkNode, selectedBookmarkItem.BookmarkNode)
                    select x;
                if (((this.SelectedSearchResult != null) && (ReferenceEquals(this.SelectedSearchResult.BookmarkNode, selectedBookmarkItem.BookmarkNode) && source.Skip<SearchData>(1).Any<SearchData>())) && (((searchDirection == TextSearchDirection.Forward) && (source.Last<SearchData>() != this.SelectedSearchResult)) || ((searchDirection == TextSearchDirection.Backward) && (source.First<SearchData>() != this.SelectedSearchResult))))
                {
                    this.NavigateToBrick(searchDirection);
                }
                else if (searchDirection == TextSearchDirection.Forward)
                {
                    for (int i = this.BookmarkNodes.IndexOf(selectedBookmarkItem) + 1; i < this.BookmarkNodes.Count<object>(); i++)
                    {
                        if (this.SetSelectedItem((BookmarkNodeItem) this.BookmarkNodes[i]))
                        {
                            return;
                        }
                    }
                    Func<SearchData, bool> predicate = <>c.<>9__49_1;
                    if (<>c.<>9__49_1 == null)
                    {
                        Func<SearchData, bool> local1 = <>c.<>9__49_1;
                        predicate = <>c.<>9__49_1 = x => x.BookmarkNode != null;
                    }
                    this.SelectedSearchResult = this.SearchResults.First<SearchData>(predicate);
                    this.SelectDocumentMapNode();
                }
                else
                {
                    for (int i = this.BookmarkNodes.IndexOf(selectedBookmarkItem) - 1; i >= 0; i--)
                    {
                        if (this.SetSelectedItem((BookmarkNodeItem) this.BookmarkNodes[i]))
                        {
                            return;
                        }
                    }
                    Func<SearchData, bool> predicate = <>c.<>9__49_2;
                    if (<>c.<>9__49_2 == null)
                    {
                        Func<SearchData, bool> local2 = <>c.<>9__49_2;
                        predicate = <>c.<>9__49_2 = x => x.BookmarkNode != null;
                    }
                    this.SelectedSearchResult = this.SearchResults.Last<SearchData>(predicate);
                    this.SelectDocumentMapNode();
                }
            }
        }

        protected void OnSearchTextChanged()
        {
            this.InvalidateSearch();
            this.singleActionManager.RaiseAction();
        }

        protected void OnSearchWholeWordsChanged()
        {
            this.navigationPaneUI.SyncSearchParametersWithSettings();
            this.InvalidateSearch();
            this.singleActionManager.RaiseAction();
        }

        protected void OnSelectedSearchResultChanged()
        {
            ParameterExpression expression = Expression.Parameter(typeof(NavigationPaneSearchModel), "m");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            this.RaisePropertyChanged<NavigationPaneSearchModel, int>(Expression.Lambda<Func<NavigationPaneSearchModel, int>>(Expression.Property(expression, (MethodInfo) methodof(NavigationPaneSearchModel.get_SelectedSearchResultIndex)), parameters));
        }

        protected void OnUseCaseSensitiveSearchChanged()
        {
            this.navigationPaneUI.SyncSearchParametersWithSettings();
            this.InvalidateSearch();
            this.singleActionManager.RaiseAction();
        }

        private void ProcessBookmarks(IEnumerable<SearchData> items)
        {
            if (items.Any<SearchData>())
            {
                Func<SearchData, BookmarkNode> keySelector = <>c.<>9__47_0;
                if (<>c.<>9__47_0 == null)
                {
                    Func<SearchData, BookmarkNode> local1 = <>c.<>9__47_0;
                    keySelector = <>c.<>9__47_0 = x => x.BookmarkNode;
                }
                foreach (IGrouping<BookmarkNode, SearchData> grouping in items.GroupBy<SearchData, BookmarkNode>(keySelector))
                {
                    foreach (BookmarkNodeItem item in this.BookmarkNodes)
                    {
                        if (grouping.Key == item.BookmarkNode)
                        {
                            item.IsHighlighted = true;
                            break;
                        }
                    }
                }
            }
        }

        private void ProcessThumbnails(IEnumerable<int> indices)
        {
            if (indices.Any<int>())
            {
                this.resultPageIndices.AddRange(indices);
            }
        }

        public void Search()
        {
            this.InvalidateSearch();
            if (!string.IsNullOrEmpty(this.SearchText))
            {
                BrickPagePairComparer comparer = new BrickPagePairComparer(this.navigationPaneUI.PrintingSystem.Pages);
                Action<List<SearchData>> fillSearchResultAction = x => this.navigationPaneUI.Dispatcher.Invoke(delegate {
                    if (this.SearchState == DevExpress.Xpf.Printing.SearchState.InProgress)
                    {
                        List<SearchData> collection = x;
                        this.searchResults.AddRange(collection);
                        Func<SearchData, bool> predicate = <>c.<>9__41_2;
                        if (<>c.<>9__41_2 == null)
                        {
                            Func<SearchData, bool> local1 = <>c.<>9__41_2;
                            predicate = <>c.<>9__41_2 = y => y.BookmarkNode != null;
                        }
                        this.ProcessBookmarks(collection.Where<SearchData>(predicate));
                        Func<SearchData, int> func2 = <>c.<>9__41_3;
                        if (<>c.<>9__41_3 == null)
                        {
                            Func<SearchData, int> local2 = <>c.<>9__41_3;
                            func2 = <>c.<>9__41_3 = y => y.PageIndex;
                        }
                        this.ProcessThumbnails(collection.Select<SearchData, int>(func2));
                    }
                }, DispatcherPriority.ApplicationIdle);
                this.tokenSource = new CancellationTokenSource();
                Func<object, BookmarkNode> selector = <>c.<>9__41_4;
                if (<>c.<>9__41_4 == null)
                {
                    Func<object, BookmarkNode> local1 = <>c.<>9__41_4;
                    selector = <>c.<>9__41_4 = x => ((BookmarkNodeItem) x).BookmarkNode;
                }
                Task task = this.searchHelper.GetSearchTask(this.navigationPaneUI.PrintingSystem, fillSearchResultAction, new TextBrickSelector(this.SearchText, this.SearchWholeWords, this.UseCaseSensitiveSearch, this.navigationPaneUI.PrintingSystem), this.tokenSource, this.BookmarkNodes.Select<object, BookmarkNode>(selector));
                task.ContinueWith<DevExpress.Xpf.Printing.SearchState>(delegate (Task x) {
                    DevExpress.Xpf.Printing.SearchState state;
                    this.SearchState = state = DevExpress.Xpf.Printing.SearchState.Finished;
                    return state;
                });
                this.SearchState = DevExpress.Xpf.Printing.SearchState.InProgress;
                task.Start();
            }
        }

        private void SelectBrick(BrickPagePair result)
        {
            if (result != null)
            {
                this.navigationPaneUI.DocumentPresenter.Do<DevExpress.Xpf.Printing.DocumentPresenterControl>(delegate (DevExpress.Xpf.Printing.DocumentPresenterControl x) {
                    x.SelectionService.SelectBrick(result.GetPage(this.navigationPaneUI.PrintingSystem.Pages), result.GetBrick(this.navigationPaneUI.PrintingSystem.Pages));
                    ScrollIntoViewMode? scrollIntoView = null;
                    x.NavigationStrategy.ShowBrick(result, scrollIntoView);
                    x.Update();
                });
            }
        }

        private void SelectDocumentMapNode()
        {
            Func<SearchData, BrickPagePair> evaluator = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                Func<SearchData, BrickPagePair> local1 = <>c.<>9__51_0;
                evaluator = <>c.<>9__51_0 = x => x.BrickPagePair;
            }
            this.SelectBrick(this.SelectedSearchResult.With<SearchData, BrickPagePair>(evaluator));
            object obj2 = this.BookmarkNodes.First<object>(y => ReferenceEquals(((BookmarkNodeItem) y).BookmarkNode, this.SelectedSearchResult.BookmarkNode));
            this.navigationPaneUI.SelectedBookmarkNode = obj2;
        }

        private bool SetSelectedItem(BookmarkNodeItem bookmarkNodeItem)
        {
            bool flag;
            using (IEnumerator<SearchData> enumerator = this.SearchResults.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        SearchData current = enumerator.Current;
                        BookmarkNode bookmarkNode = bookmarkNodeItem.BookmarkNode;
                        if (!ReferenceEquals(bookmarkNode, current.BookmarkNode))
                        {
                            continue;
                        }
                        this.SelectedSearchResult = current;
                        Func<SearchData, BrickPagePair> evaluator = <>c.<>9__48_0;
                        if (<>c.<>9__48_0 == null)
                        {
                            Func<SearchData, BrickPagePair> local1 = <>c.<>9__48_0;
                            evaluator = <>c.<>9__48_0 = x => x.BrickPagePair;
                        }
                        this.SelectBrick(this.SelectedSearchResult.With<SearchData, BrickPagePair>(evaluator));
                        this.navigationPaneUI.SelectedBookmarkNode = bookmarkNodeItem;
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public void StopSearch()
        {
            this.tokenSource.Cancel();
        }

        public virtual ObservableRangeCollection<object> BookmarkNodes { get; protected set; }

        public IEnumerable<int> ResultPageIndices =>
            this.resultPageIndices;

        public IEnumerable<SearchData> SearchResults =>
            this.searchResults;

        public virtual SearchData SelectedSearchResult { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int SelectedSearchResultIndex =>
            this.searchResults.Contains(this.SelectedSearchResult) ? this.searchResults.IndexOf(this.SelectedSearchResult) : -1;

        public virtual DevExpress.Xpf.Printing.SearchState SearchState { get; protected set; }

        public virtual string SearchText { get; set; }

        public virtual bool UseCaseSensitiveSearch { get; set; }

        public virtual bool SearchWholeWords { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NavigationPaneSearchModel.<>c <>9 = new NavigationPaneSearchModel.<>c();
            public static Func<SearchData, bool> <>9__41_2;
            public static Func<SearchData, int> <>9__41_3;
            public static Func<object, BookmarkNode> <>9__41_4;
            public static Func<SearchData, BookmarkNode> <>9__47_0;
            public static Func<SearchData, BrickPagePair> <>9__48_0;
            public static Func<SearchData, bool> <>9__49_1;
            public static Func<SearchData, bool> <>9__49_2;
            public static Func<SearchData, BrickPagePair> <>9__49_3;
            public static Func<SearchData, BrickPagePair> <>9__49_5;
            public static Func<SearchData, BrickPagePair> <>9__49_6;
            public static Func<SearchData, BrickPagePair> <>9__51_0;
            public static Func<SearchData, BrickPagePair> <>9__52_0;

            internal BrickPagePair <NavigateToBrick>b__52_0(SearchData x) => 
                x.BrickPagePair;

            internal bool <NavigateToNextResult>b__49_1(SearchData x) => 
                x.BookmarkNode != null;

            internal bool <NavigateToNextResult>b__49_2(SearchData x) => 
                x.BookmarkNode != null;

            internal BrickPagePair <NavigateToNextResult>b__49_3(SearchData x) => 
                x.BrickPagePair;

            internal BrickPagePair <NavigateToNextResult>b__49_5(SearchData x) => 
                x.BrickPagePair;

            internal BrickPagePair <NavigateToNextResult>b__49_6(SearchData x) => 
                x.BrickPagePair;

            internal BookmarkNode <ProcessBookmarks>b__47_0(SearchData x) => 
                x.BookmarkNode;

            internal bool <Search>b__41_2(SearchData y) => 
                y.BookmarkNode != null;

            internal int <Search>b__41_3(SearchData y) => 
                y.PageIndex;

            internal BookmarkNode <Search>b__41_4(object x) => 
                ((BookmarkNodeItem) x).BookmarkNode;

            internal BrickPagePair <SelectDocumentMapNode>b__51_0(SearchData x) => 
                x.BrickPagePair;

            internal BrickPagePair <SetSelectedItem>b__48_0(SearchData x) => 
                x.BrickPagePair;
        }
    }
}

