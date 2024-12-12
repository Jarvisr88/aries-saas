namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Navigation;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public class SearchPanelViewModel : ISearchPanelViewModel, INotifyPropertyChanged
    {
        private readonly DelegateCommand<object> findNextCommand;
        private readonly DelegateCommand<object> findPrevCommand;
        private readonly DelegateCommand<object> closeCommand;
        private int brickIndex = -1;
        private string searchString = string.Empty;
        private BrickPagePairCollection brickPagePairs;
        private IDocumentPreviewModel model;
        private bool caseSensitive;
        private bool findWholeWord;

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchPanelViewModel()
        {
            this.findNextCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.FindNext), new Func<object, bool>(this.CanFind), false);
            this.findPrevCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.FindPrev), new Func<object, bool>(this.CanFind), false);
            Func<object, bool> canExecuteMethod = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__9_0;
                canExecuteMethod = <>c.<>9__9_0 = p => true;
            }
            this.closeCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.Close), canExecuteMethod, false);
            this.brickPagePairs = new BrickPagePairCollection();
        }

        private void CalcBrickIndex(SearchDirection searchDirection)
        {
            if (searchDirection == SearchDirection.Up)
            {
                this.brickIndex--;
            }
            else
            {
                if (searchDirection != SearchDirection.Down)
                {
                    throw new NotSupportedException("searchDirection");
                }
                this.brickIndex++;
            }
            if (this.brickIndex < this.MinBrickIndex)
            {
                this.brickIndex = this.MaxBrickIndex;
            }
            if (this.brickIndex > this.MaxBrickIndex)
            {
                this.brickIndex = this.MinBrickIndex;
            }
        }

        private bool CanFind(object parameter) => 
            !string.IsNullOrEmpty(this.SearchString) && ((this.Model != null) && (this.Document != null));

        private void Clear()
        {
            this.brickIndex = -1;
            this.brickPagePairs.Clear();
            if (this.Model != null)
            {
                this.Model.FoundBrickInfo = null;
            }
        }

        private void Close(object parameter)
        {
            if ((this.Model != null) && (this.Model is DocumentPreviewModelBase))
            {
                ICommand toggleSearchPanelCommand = ((DocumentPreviewModelBase) this.Model).ToggleSearchPanelCommand;
                if ((toggleSearchPanelCommand != null) && toggleSearchPanelCommand.CanExecute(null))
                {
                    toggleSearchPanelCommand.Execute(null);
                }
            }
        }

        private void FindCore(SearchDirection searchDirection)
        {
            if (this.brickPagePairs.Count == 0)
            {
                PrintingSystemBase printingSystem = ((PrintingSystemPreviewModel) this.Model).PrintingSystem;
                TextBrickSelector selector = new TextBrickSelector(this.SearchString, this.FindWholeWord, this.CaseSensitive, printingSystem);
                this.brickPagePairs = NavigateHelper.SelectBrickPagePairs(this.Document, selector, new BrickPagePairComparer(printingSystem.Pages));
            }
            if (this.brickPagePairs.Count == 0)
            {
                this.Model.FoundBrickInfo = BrickInfo.Empty;
                ((DocumentPreviewModelBase) this.Model).DialogService.ShowInformation(PrintingLocalizer.GetString(PrintingStringId.Search_EmptyResult), PrintingLocalizer.GetString(PrintingStringId.Information));
            }
            else
            {
                this.CalcBrickIndex(searchDirection);
                BrickPagePair pair = this.brickPagePairs[this.brickIndex];
                string tagByIndices = DocumentMapTreeViewNodeHelper.GetTagByIndices(pair.BrickIndices, pair.PageIndex);
                this.Model.FoundBrickInfo = new BrickInfo(tagByIndices, pair.PageIndex);
            }
        }

        private void FindNext(object parameter)
        {
            this.FindCore(SearchDirection.Down);
        }

        private void FindPrev(object parameter)
        {
            this.FindCore(SearchDirection.Up);
        }

        private void PrintingSystem_DocumentChanged(object sender, EventArgs e)
        {
            this.RaiseFindCommandsCanExecute();
            this.Clear();
        }

        private void RaiseFindCommandsCanExecute()
        {
            if (this.findNextCommand != null)
            {
                this.findNextCommand.RaiseCanExecuteChanged();
            }
            if (this.findPrevCommand != null)
            {
                this.findPrevCommand.RaiseCanExecuteChanged();
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            this.RaisePropertyChanged<T>(this.PropertyChanged, property);
        }

        public IDocumentPreviewModel Model
        {
            get => 
                this.model;
            set
            {
                if (this.IsModelCorrect)
                {
                    ((PrintingSystemPreviewModel) this.Model).PrintingSystem.DocumentChanged -= new EventHandler(this.PrintingSystem_DocumentChanged);
                }
                this.model = value;
                if (this.IsModelCorrect)
                {
                    ((PrintingSystemPreviewModel) this.Model).PrintingSystem.DocumentChanged += new EventHandler(this.PrintingSystem_DocumentChanged);
                }
                this.RaiseFindCommandsCanExecute();
                this.Clear();
            }
        }

        private bool IsModelCorrect =>
            (this.Model != null) && ((this.Model is PrintingSystemPreviewModel) && (((PrintingSystemPreviewModel) this.Model).PrintingSystem != null));

        public DevExpress.XtraPrinting.Document Document =>
            !this.IsModelCorrect ? null : ((PrintingSystemPreviewModel) this.Model).PrintingSystem.Document;

        private int MinBrickIndex =>
            0;

        private int MaxBrickIndex =>
            (this.brickPagePairs.Count == 0) ? 0 : (this.brickPagePairs.Count - 1);

        public bool ReplaceMode =>
            false;

        public bool ShowCaseSensitiveOption =>
            true;

        public bool ShowFindWholeWordOption =>
            true;

        public bool ShowUseRegularExpressionOption =>
            false;

        public bool CaseSensitive
        {
            get => 
                this.caseSensitive;
            set
            {
                if (this.caseSensitive != value)
                {
                    this.caseSensitive = value;
                    this.Clear();
                }
            }
        }

        public bool FindWholeWord
        {
            get => 
                this.findWholeWord;
            set
            {
                if (this.findWholeWord != value)
                {
                    this.findWholeWord = value;
                    this.Clear();
                }
            }
        }

        public bool UseRegularExpression { get; set; }

        public string SearchString
        {
            get => 
                this.searchString;
            set
            {
                this.searchString = value;
                this.Clear();
                this.RaiseFindCommandsCanExecute();
            }
        }

        public ICommand CloseCommand =>
            this.closeCommand;

        public ICommand FindBackwardCommand =>
            this.findPrevCommand;

        public ICommand FindForwardCommand =>
            this.findNextCommand;

        public ICommand ReplaceAllForwardCommand =>
            null;

        public ICommand ReplaceForwardCommand =>
            null;

        public string ReplaceString { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchPanelViewModel.<>c <>9 = new SearchPanelViewModel.<>c();
            public static Func<object, bool> <>9__9_0;

            internal bool <.ctor>b__9_0(object p) => 
                true;
        }
    }
}

