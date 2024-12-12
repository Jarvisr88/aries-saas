namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class SearchControlContainer : Control
    {
        public static readonly DependencyProperty SearchParameterProperty;
        public static readonly DependencyProperty WholeWordProperty;
        public static readonly DependencyProperty IsCaseSensitiveProperty;
        public static readonly DependencyProperty SearchTextProperty;
        public static readonly DependencyProperty IsSearchControlVisibleProperty;
        public static readonly DependencyProperty ActualSearchContainerProperty;

        static SearchControlContainer()
        {
            Type ownerType = typeof(SearchControlContainer);
            IsSearchControlVisibleProperty = DependencyPropertyManager.Register("IsSearchControlVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((SearchControlContainer) d).IsSearchControlVisibleChanged((bool) e.NewValue)));
            SearchParameterProperty = DependencyPropertyManager.Register("SearchParameter", typeof(TextSearchParameter), ownerType, new FrameworkPropertyMetadata(null));
            WholeWordProperty = DependencyPropertyManager.Register("WholeWord", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (o, args) => ((SearchControlContainer) o).SearchPropertyChanged()));
            IsCaseSensitiveProperty = DependencyPropertyManager.Register("IsCaseSensitive", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (o, args) => ((SearchControlContainer) o).SearchPropertyChanged()));
            SearchTextProperty = DependencyPropertyManager.Register("SearchText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (o, args) => ((SearchControlContainer) o).SearchPropertyChanged()));
            ActualSearchContainerProperty = DependencyPropertyManager.RegisterAttached("ActualSearchContainer", typeof(SearchControlContainer), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        }

        public SearchControlContainer()
        {
            SetActualSearchContainer(this, this);
            this.SetDefaultStyleKey(typeof(SearchControlContainer));
            this.FindNextTextCommand = DelegateCommandFactory.Create(new Action(this.FindNextText), new Func<bool>(this.CanFindText));
            this.FindPreviousTextCommand = DelegateCommandFactory.Create(new Action(this.FindPreviousText), new Func<bool>(this.CanFindText));
            this.CloseCommand = DelegateCommandFactory.Create(new Action(this.Close));
        }

        private bool CanFindText()
        {
            Func<TextSearchParameter, bool> evaluator = <>c.<>9__42_0;
            if (<>c.<>9__42_0 == null)
            {
                Func<TextSearchParameter, bool> local1 = <>c.<>9__42_0;
                evaluator = <>c.<>9__42_0 = x => !string.IsNullOrWhiteSpace(x.Text);
            }
            return this.SearchParameter.Return<TextSearchParameter, bool>(evaluator, (<>c.<>9__42_1 ??= () => false));
        }

        private void Close()
        {
            this.CommandProvider.ShowFindTextCommand.TryExecute(false);
        }

        private void FindNextText()
        {
            if (this.SearchParameter != null)
            {
                this.SearchParameter.SearchDirection = TextSearchDirection.Forward;
                this.CommandProvider.Do<DevExpress.Xpf.DocumentViewer.CommandProvider>(x => x.FindNextTextCommand.TryExecute(this.SearchParameter));
            }
        }

        private void FindPreviousText()
        {
            if (this.SearchParameter != null)
            {
                this.SearchParameter.SearchDirection = TextSearchDirection.Backward;
                this.CommandProvider.Do<DevExpress.Xpf.DocumentViewer.CommandProvider>(x => x.FindNextTextCommand.TryExecute(this.SearchParameter));
            }
        }

        public static SearchControlContainer GetActualSearchContainer(DependencyObject d) => 
            (SearchControlContainer) d.GetValue(ActualSearchContainerProperty);

        protected virtual void IsSearchControlVisibleChanged(bool newValue)
        {
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.SearchPropertyChanged();
        }

        protected virtual void SearchPropertyChanged()
        {
            if ((this.SearchParameter != null) && base.IsLoaded)
            {
                this.SearchParameter.WholeWord = this.WholeWord;
                this.SearchParameter.IsCaseSensitive = this.IsCaseSensitive;
                this.SearchParameter.Text = this.SearchText;
            }
        }

        public static void SetActualSearchContainer(DependencyObject d, SearchControlContainer value)
        {
            d.SetValue(ActualSearchContainerProperty, value);
        }

        public ICommand FindNextTextCommand { get; private set; }

        public ICommand FindPreviousTextCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        public bool IsSearchControlVisible
        {
            get => 
                (bool) base.GetValue(IsSearchControlVisibleProperty);
            set => 
                base.SetValue(IsSearchControlVisibleProperty, value);
        }

        public string SearchText
        {
            get => 
                (string) base.GetValue(SearchTextProperty);
            set => 
                base.SetValue(SearchTextProperty, value);
        }

        public bool WholeWord
        {
            get => 
                (bool) base.GetValue(WholeWordProperty);
            set => 
                base.SetValue(WholeWordProperty, value);
        }

        public bool IsCaseSensitive
        {
            get => 
                (bool) base.GetValue(IsCaseSensitiveProperty);
            set => 
                base.SetValue(IsCaseSensitiveProperty, value);
        }

        public TextSearchParameter SearchParameter
        {
            get => 
                (TextSearchParameter) base.GetValue(SearchParameterProperty);
            set => 
                base.SetValue(SearchParameterProperty, value);
        }

        private DocumentViewerControl ActualViewer =>
            DocumentViewerControl.GetActualViewer(this) as DocumentViewerControl;

        private DevExpress.Xpf.DocumentViewer.CommandProvider CommandProvider
        {
            get
            {
                Func<DocumentViewerControl, DevExpress.Xpf.DocumentViewer.CommandProvider> evaluator = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Func<DocumentViewerControl, DevExpress.Xpf.DocumentViewer.CommandProvider> local1 = <>c.<>9__39_0;
                    evaluator = <>c.<>9__39_0 = x => x.ActualCommandProvider;
                }
                return this.ActualViewer.With<DocumentViewerControl, DevExpress.Xpf.DocumentViewer.CommandProvider>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchControlContainer.<>c <>9 = new SearchControlContainer.<>c();
            public static Func<DocumentViewerControl, CommandProvider> <>9__39_0;
            public static Func<TextSearchParameter, bool> <>9__42_0;
            public static Func<bool> <>9__42_1;

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SearchControlContainer) d).IsSearchControlVisibleChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__6_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SearchControlContainer) o).SearchPropertyChanged();
            }

            internal void <.cctor>b__6_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SearchControlContainer) o).SearchPropertyChanged();
            }

            internal void <.cctor>b__6_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SearchControlContainer) o).SearchPropertyChanged();
            }

            internal bool <CanFindText>b__42_0(TextSearchParameter x) => 
                !string.IsNullOrWhiteSpace(x.Text);

            internal bool <CanFindText>b__42_1() => 
                false;

            internal CommandProvider <get_CommandProvider>b__39_0(DocumentViewerControl x) => 
                x.ActualCommandProvider;
        }
    }
}

