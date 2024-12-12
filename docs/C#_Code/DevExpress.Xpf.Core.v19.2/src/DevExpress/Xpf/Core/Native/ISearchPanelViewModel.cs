namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Input;

    public interface ISearchPanelViewModel
    {
        ICommand FindForwardCommand { get; }

        ICommand FindBackwardCommand { get; }

        ICommand ReplaceForwardCommand { get; }

        ICommand ReplaceAllForwardCommand { get; }

        ICommand CloseCommand { get; }

        string SearchString { get; set; }

        string ReplaceString { get; set; }

        bool ReplaceMode { get; }

        bool CaseSensitive { get; set; }

        bool FindWholeWord { get; set; }

        bool UseRegularExpression { get; set; }

        bool ShowCaseSensitiveOption { get; }

        bool ShowFindWholeWordOption { get; }

        bool ShowUseRegularExpressionOption { get; }
    }
}

