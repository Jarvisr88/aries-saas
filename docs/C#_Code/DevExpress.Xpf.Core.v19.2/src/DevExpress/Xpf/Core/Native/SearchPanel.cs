namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class SearchPanel : Control, ILogicalOwner, IInputElement
    {
        public static readonly DependencyProperty ReplaceButtonTextProperty;
        public static readonly DependencyProperty ReplaceAllButtonTextProperty;
        public static readonly DependencyProperty FindLabelTextProperty;
        public static readonly DependencyProperty ReplaceLabelTextProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty FindNextButtonTooltipProperty;
        public static readonly DependencyProperty FindPrevButtonTooltipProperty;
        public static readonly DependencyProperty CloseButtonTooltipProperty;
        public static readonly DependencyProperty SearchOptionsButtonTooltipProperty;
        public static readonly RoutedEvent ClosedEvent;

        public event RoutedEventHandler Closed;

        event RoutedEventHandler ILogicalOwner.Loaded;

        static SearchPanel();
        public SearchPanel();
        public SearchPanel(ISearchPanelViewModel viewModel);
        private void CloseSearchPanel();
        void ILogicalOwner.AddChild(object child);
        void ILogicalOwner.RemoveChild(object child);
        protected virtual void ExecuteFindForwardCommand();
        protected virtual void ExecuteReplaceForwardCommand();
        public override void OnApplyTemplate();
        protected internal void OnCaseSensitiveCheckedChanged(object sender, ItemClickEventArgs e);
        protected internal virtual void OnClose(object sender, RoutedEventArgs e);
        protected virtual void OnRegularExpressionCheckedChanged(bool? IsChecked);
        protected internal void OnRegularExpressionCheckedChanged(object sender, ItemClickEventArgs e);
        protected internal virtual void OnReplaceStringKeyDown(object sender, KeyEventArgs e);
        protected virtual void OnSearchOptionsButtonClick(UIElement control);
        protected internal virtual void OnSearchStringKeyDown(object sender, KeyEventArgs e);
        protected internal void OnWholeWordCheckedChanged(object sender, ItemClickEventArgs e);
        protected internal virtual void RaiseCloseEvent();
        private void SearchOptionsButton_Click(object sender, RoutedEventArgs e);
        protected static void ShowPopupMenu(UIElement control, PopupMenu menu);

        public ISearchPanelViewModel ViewModel { get; set; }

        public string ReplaceButtonText { get; set; }

        public string ReplaceAllButtonText { get; set; }

        public string FindLabelText { get; set; }

        public string ReplaceLabelText { get; set; }

        public object CommandParameter { get; set; }

        public string FindNextButtonTooltip { get; set; }

        public string FindPrevButtonTooltip { get; set; }

        public string CloseButtonTooltip { get; set; }

        public string SearchOptionsButtonTooltip { get; set; }

        protected internal ButtonEdit SearchStringEdit { get; }

        protected internal ButtonEdit ReplaceStringEdit { get; }

        protected internal Button FindNextButton { get; }

        protected internal Button FindPrevButton { get; }

        protected internal Button ReplaceButton { get; }

        protected internal Button ReplaceAllButton { get; }

        protected internal FrameworkElement ReplaceBox { get; }

        protected internal FrameworkElement ReplaceButtons { get; }

        protected internal Button SearchClose { get; }

        protected internal Button SearchOptionsButton { get; }

        bool ILogicalOwner.IsLoaded { get; }

        double ILogicalOwner.ActualWidth { get; }

        double ILogicalOwner.ActualHeight { get; }
    }
}

