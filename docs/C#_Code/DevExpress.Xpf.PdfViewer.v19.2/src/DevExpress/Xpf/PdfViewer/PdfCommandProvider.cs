namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class PdfCommandProvider : CommandProvider
    {
        protected virtual ICommand CreatePageLayoutCommand(string dllName)
        {
            ObservableCollection<CommandToggleButton> pageLayoutItems = new ObservableCollection<CommandToggleButton>();
            CommandCheckItems items1 = new CommandCheckItems();
            items1.Caption = PdfViewerLocalizer.GetString(PdfViewerStringId.CommandPageLayoutCaption);
            items1.Hint = PdfViewerLocalizer.GetString(PdfViewerStringId.CommandPageLayoutDescription);
            CommandCheckItems items2 = items1;
            Action executeMethod = <>c.<>9__145_0;
            if (<>c.<>9__145_0 == null)
            {
                Action local1 = <>c.<>9__145_0;
                executeMethod = <>c.<>9__145_0 = delegate {
                };
            }
            items2.Command = DelegateCommandFactory.Create(executeMethod, delegate {
                Func<CommandToggleButton, bool> predicate = <>c.<>9__145_2;
                if (<>c.<>9__145_2 == null)
                {
                    Func<CommandToggleButton, bool> local1 = <>c.<>9__145_2;
                    predicate = <>c.<>9__145_2 = x => x.CanExecute(null);
                }
                return pageLayoutItems.Any<CommandToggleButton>(predicate);
            });
            CommandCheckItems local2 = items2;
            local2.Items = pageLayoutItems;
            local2.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/Copy_16x16.png");
            local2.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/Copy_32x32.png");
            CommandCheckItems items = local2;
            CommandSetPageLayout item = new CommandSetPageLayout();
            item.Caption = PdfViewerLocalizer.GetString(PdfViewerStringId.CommandSinglePageView);
            item.Command = new CommandWrapper(() => this.SetPageLayoutCommandInternal);
            item.PageLayout = PdfPageLayout.SinglePage;
            item.GroupIndex = 3;
            item.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/SinglePage_16x16.png");
            item.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/SinglePage_32x32.png");
            pageLayoutItems.Add(item);
            CommandSetPageLayout layout2 = new CommandSetPageLayout();
            layout2.Caption = PdfViewerLocalizer.GetString(PdfViewerStringId.CommandOneColumnView);
            layout2.Command = new CommandWrapper(() => this.SetPageLayoutCommandInternal);
            layout2.PageLayout = PdfPageLayout.OneColumn;
            layout2.GroupIndex = 3;
            layout2.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/SinglePageEnabledScrolling_16x16.png");
            layout2.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/SinglePageEnabledScrolling_32x32.png");
            pageLayoutItems.Add(layout2);
            CommandSetPageLayout layout3 = new CommandSetPageLayout();
            layout3.Caption = PdfViewerLocalizer.GetString(PdfViewerStringId.CommandTwoPageView);
            layout3.Command = new CommandWrapper(() => this.SetPageLayoutCommandInternal);
            layout3.PageLayout = PdfPageLayout.TwoPageLeft;
            layout3.GroupIndex = 3;
            layout3.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/TwoPages_16x16.png");
            layout3.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/TwoPages_32x32.png");
            pageLayoutItems.Add(layout3);
            CommandSetPageLayout layout4 = new CommandSetPageLayout();
            layout4.Caption = PdfViewerLocalizer.GetString(PdfViewerStringId.CommandTwoColumnView);
            layout4.Command = new CommandWrapper(() => this.SetPageLayoutCommandInternal);
            layout4.PageLayout = PdfPageLayout.TwoColumnLeft;
            layout4.GroupIndex = 3;
            layout4.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/TwoPagesEnabledScrolling_16x16.png");
            layout4.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, "Images/TwoPagesEnabledScrolling_32x32.png");
            pageLayoutItems.Add(layout4);
            CommandSetPageLayout layout5 = new CommandSetPageLayout();
            layout5.IsSeparator = true;
            pageLayoutItems.Add(layout5);
            CommandShowCoverPage page1 = new CommandShowCoverPage();
            page1.Caption = PdfViewerLocalizer.GetString(PdfViewerStringId.CommandShowCoverPage);
            page1.Command = new CommandWrapper(() => this.ShowCoverPageCommandInternal);
            page1.GroupIndex = 4;
            pageLayoutItems.Add(page1);
            return items;
        }

        protected override void InitializeElements()
        {
            // Unresolved stack state at '00000756'
        }

        private void OnCursorModeChanged(object sender, RoutedEventArgs e)
        {
            this.UpdateCursorModeCheckState();
        }

        protected override void OnDocumentChanged(object sender, RoutedEventArgs e)
        {
            base.OnDocumentChanged(sender, e);
            if (this.PdfViewer.RecentFiles != null)
            {
                (this.OpenDocumentSplitCommand as PdfOpenDocumentSplitItem).Do<PdfOpenDocumentSplitItem>(x => x.RecentFiles = new ObservableCollection<RecentFileViewModel>(this.PdfViewer.RecentFiles.Reverse<RecentFileViewModel>()));
            }
        }

        protected virtual void OnDocumentLoaded(object sender, RoutedEventArgs e)
        {
            base.UpdatePagination();
        }

        protected virtual void OnPageLayoutChanged(object sender, RoutedEventArgs e)
        {
            this.UpdatePageLayoutCommand();
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            this.PdfViewer.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.CursorModeChanged += new RoutedEventHandler(this.OnCursorModeChanged);
            });
            this.PdfViewer.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.DocumentLoaded += new RoutedEventHandler(this.OnDocumentLoaded);
            });
            this.PdfViewer.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.PageLayoutChanged += new RoutedEventHandler(this.OnPageLayoutChanged);
            });
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            this.PdfViewer.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.CursorModeChanged -= new RoutedEventHandler(this.OnCursorModeChanged);
            });
            this.PdfViewer.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.DocumentLoaded -= new RoutedEventHandler(this.OnDocumentLoaded);
            });
            this.PdfViewer.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.PageLayoutChanged -= new RoutedEventHandler(this.OnPageLayoutChanged);
            });
        }

        internal void UpdateCursorModeCheckState()
        {
            Func<PdfViewerControl, CursorModeType> evaluator = <>c.<>9__148_0;
            if (<>c.<>9__148_0 == null)
            {
                Func<PdfViewerControl, CursorModeType> local1 = <>c.<>9__148_0;
                evaluator = <>c.<>9__148_0 = x => x.CursorMode;
            }
            switch (this.PdfViewer.Return<PdfViewerControl, CursorModeType>(evaluator, (<>c.<>9__148_1 ??= () => CursorModeType.HandTool)))
            {
                case CursorModeType.HandTool:
                    ((PdfSetCursorModeItem) this.HandToolCommand).IsChecked = true;
                    ((PdfSetCursorModeItem) this.SelectToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.MarqueeZoomCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.HighlightTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.StrikethroughTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.UnderlineTextToolCommand).IsChecked = false;
                    return;

                case CursorModeType.SelectTool:
                    ((PdfSetCursorModeItem) this.HandToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.SelectToolCommand).IsChecked = true;
                    ((PdfSetCursorModeItem) this.MarqueeZoomCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.HighlightTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.StrikethroughTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.UnderlineTextToolCommand).IsChecked = false;
                    return;

                case CursorModeType.MarqueeZoom:
                    ((PdfSetCursorModeItem) this.HandToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.SelectToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.MarqueeZoomCommand).IsChecked = true;
                    ((PdfSetCursorModeItem) this.HighlightTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.StrikethroughTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.UnderlineTextToolCommand).IsChecked = false;
                    return;

                case CursorModeType.TextHighlightTool:
                    ((PdfSetCursorModeItem) this.HandToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.SelectToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.MarqueeZoomCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.HighlightTextToolCommand).IsChecked = true;
                    ((PdfSetCursorModeItem) this.StrikethroughTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.UnderlineTextToolCommand).IsChecked = false;
                    return;

                case CursorModeType.TextStrikethroughTool:
                    ((PdfSetCursorModeItem) this.HandToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.SelectToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.MarqueeZoomCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.HighlightTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.StrikethroughTextToolCommand).IsChecked = true;
                    ((PdfSetCursorModeItem) this.UnderlineTextToolCommand).IsChecked = false;
                    return;

                case CursorModeType.TextUnderlineTool:
                    ((PdfSetCursorModeItem) this.HandToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.SelectToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.MarqueeZoomCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.HighlightTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.StrikethroughTextToolCommand).IsChecked = false;
                    ((PdfSetCursorModeItem) this.UnderlineTextToolCommand).IsChecked = true;
                    return;
            }
        }

        protected virtual bool UpdatePageLayoutCheckState(CommandToggleButton button)
        {
            if (button is CommandShowCoverPage)
            {
                Func<PdfViewerControl, bool> func1 = <>c.<>9__154_0;
                if (<>c.<>9__154_0 == null)
                {
                    Func<PdfViewerControl, bool> local1 = <>c.<>9__154_0;
                    func1 = <>c.<>9__154_0 = x => x.IsShowCoverPage;
                }
                return this.PdfViewer.Return<PdfViewerControl, bool>(func1, (<>c.<>9__154_1 ??= () => false));
            }
            CommandSetPageLayout layout = button as CommandSetPageLayout;
            if ((layout == null) || layout.IsSeparator)
            {
                return false;
            }
            PdfPageLayout pageLayout = layout.PageLayout;
            if (pageLayout == PdfPageLayout.TwoColumnLeft)
            {
                Func<PdfViewerControl, bool> func3 = <>c.<>9__154_2;
                if (<>c.<>9__154_2 == null)
                {
                    Func<PdfViewerControl, bool> local3 = <>c.<>9__154_2;
                    func3 = <>c.<>9__154_2 = x => (x.PageLayout == PdfPageLayout.TwoColumnLeft) || (x.PageLayout == PdfPageLayout.TwoColumnRight);
                }
                return this.PdfViewer.Return<PdfViewerControl, bool>(func3, (<>c.<>9__154_3 ??= () => false));
            }
            if (pageLayout != PdfPageLayout.TwoPageLeft)
            {
                PdfPageLayout layout1 = layout.PageLayout;
                if (<>c.<>9__154_6 == null)
                {
                    PdfPageLayout local7 = layout.PageLayout;
                    layout1 = (PdfPageLayout) (<>c.<>9__154_6 = y => y.PageLayout);
                }
                return (<>c.<>9__154_6 == this.PdfViewer.Return<PdfViewerControl, PdfPageLayout>(((Func<PdfViewerControl, PdfPageLayout>) layout1), (<>c.<>9__154_7 ??= () => PdfPageLayout.OneColumn)));
            }
            Func<PdfViewerControl, bool> evaluator = <>c.<>9__154_4;
            if (<>c.<>9__154_4 == null)
            {
                Func<PdfViewerControl, bool> local5 = <>c.<>9__154_4;
                evaluator = <>c.<>9__154_4 = x => (x.PageLayout == PdfPageLayout.TwoPageLeft) || (x.PageLayout == PdfPageLayout.TwoPageRight);
            }
            return this.PdfViewer.Return<PdfViewerControl, bool>(evaluator, (<>c.<>9__154_5 ??= () => false));
        }

        private void UpdatePageLayoutCommand()
        {
            CommandCheckItems pageLayoutCommand = (CommandCheckItems) this.PageLayoutCommand;
            pageLayoutCommand.UpdateCheckState(new Func<CommandToggleButton, bool>(this.UpdatePageLayoutCheckState));
            Func<CommandToggleButton, bool> predicate = <>c.<>9__147_0;
            if (<>c.<>9__147_0 == null)
            {
                Func<CommandToggleButton, bool> local1 = <>c.<>9__147_0;
                predicate = <>c.<>9__147_0 = x => x is CommandSetPageLayout;
            }
            Func<CommandToggleButton, bool> func2 = <>c.<>9__147_1;
            if (<>c.<>9__147_1 == null)
            {
                Func<CommandToggleButton, bool> local2 = <>c.<>9__147_1;
                func2 = <>c.<>9__147_1 = x => x.IsChecked;
            }
            pageLayoutCommand.SmallGlyph = pageLayoutCommand.Items.Where<CommandToggleButton>(predicate).SingleOrDefault<CommandToggleButton>(func2).SmallGlyph;
            Func<CommandToggleButton, bool> func3 = <>c.<>9__147_2;
            if (<>c.<>9__147_2 == null)
            {
                Func<CommandToggleButton, bool> local3 = <>c.<>9__147_2;
                func3 = <>c.<>9__147_2 = x => x is CommandSetPageLayout;
            }
            Func<CommandToggleButton, bool> func4 = <>c.<>9__147_3;
            if (<>c.<>9__147_3 == null)
            {
                Func<CommandToggleButton, bool> local4 = <>c.<>9__147_3;
                func4 = <>c.<>9__147_3 = x => x.IsChecked;
            }
            pageLayoutCommand.LargeGlyph = pageLayoutCommand.Items.Where<CommandToggleButton>(func3).SingleOrDefault<CommandToggleButton>(func4).LargeGlyph;
        }

        public ICommand OpenDocumentSplitCommand { get; protected set; }

        public ICommand OpenDocumentFromWebCommand { get; protected set; }

        public ICommand PrintDocumentCommand { get; protected set; }

        public ICommand ShowPropertiesCommand { get; protected set; }

        public ICommand HandToolCommand { get; protected set; }

        public ICommand SelectToolCommand { get; protected set; }

        public ICommand MarqueeZoomCommand { get; protected set; }

        public ICommand HighlightTextToolCommand { get; protected set; }

        public ICommand StrikethroughTextToolCommand { get; protected set; }

        public ICommand UnderlineTextToolCommand { get; protected set; }

        public ICommand HighlightSelectedTextCommand { get; protected set; }

        public ICommand StrikethroughSelectedTextCommand { get; protected set; }

        public ICommand UnderlineSelectedTextCommand { get; protected set; }

        public ICommand SelectAllCommand { get; protected set; }

        public ICommand SaveAsCommand { get; protected set; }

        public ICommand CopyCommand { get; protected set; }

        public ICommand SelectionCommand { get; protected set; }

        public ICommand UnselectAllCommand { get; protected set; }

        public ICommand ImportFormDataCommand { get; protected set; }

        public ICommand ExportFormDataCommand { get; protected set; }

        public ICommand PageLayoutCommand { get; protected set; }

        public ICommand OpenAttachmentCommand { get; protected set; }

        public ICommand SaveAttachmentCommand { get; protected set; }

        public ICommand DeleteAnnotationCommand { get; protected set; }

        public ICommand ShowAnnotationPropertiesCommand { get; protected set; }

        protected internal virtual ICommand PrintDocumentCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__101_0;
                if (<>c.<>9__101_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__101_0;
                    evaluator = <>c.<>9__101_0 = x => x.PrintDocumentCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ShowPropertiesCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__103_0;
                if (<>c.<>9__103_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__103_0;
                    evaluator = <>c.<>9__103_0 = x => x.ShowPropertiesCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SetCursorModeCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__105_0;
                if (<>c.<>9__105_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__105_0;
                    evaluator = <>c.<>9__105_0 = x => x.SetCursorModeCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SelectAllCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__107_0;
                if (<>c.<>9__107_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__107_0;
                    evaluator = <>c.<>9__107_0 = x => x.SelectAllCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SaveAsCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__109_0;
                if (<>c.<>9__109_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__109_0;
                    evaluator = <>c.<>9__109_0 = x => x.SaveAsCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand CopyCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__111_0;
                if (<>c.<>9__111_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__111_0;
                    evaluator = <>c.<>9__111_0 = x => x.CopyCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SelectionCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__113_0;
                if (<>c.<>9__113_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__113_0;
                    evaluator = <>c.<>9__113_0 = x => x.SelectionCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand OpenDocumentFromWebCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__115_0;
                if (<>c.<>9__115_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__115_0;
                    evaluator = <>c.<>9__115_0 = x => x.OpenDocumentFromWebCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand UnselectAllCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__117_0;
                if (<>c.<>9__117_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__117_0;
                    evaluator = <>c.<>9__117_0 = x => x.UnselectAllCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ImportFormDataCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__119_0;
                if (<>c.<>9__119_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__119_0;
                    evaluator = <>c.<>9__119_0 = x => x.ImportFormDataCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ExportFormDataCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__121_0;
                if (<>c.<>9__121_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__121_0;
                    evaluator = <>c.<>9__121_0 = x => x.ExportFormDataCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SetPageLayoutCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__123_0;
                if (<>c.<>9__123_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__123_0;
                    evaluator = <>c.<>9__123_0 = x => x.SetPageLayoutCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ShowCoverPageCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__125_0;
                if (<>c.<>9__125_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__125_0;
                    evaluator = <>c.<>9__125_0 = x => x.ShowCoverPageCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand OpenAttachmentCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__127_0;
                if (<>c.<>9__127_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__127_0;
                    evaluator = <>c.<>9__127_0 = x => x.OpenAttachmentCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SaveAttachmentCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__129_0;
                if (<>c.<>9__129_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__129_0;
                    evaluator = <>c.<>9__129_0 = x => x.SaveAttachmentCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand HighlightTextCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__131_0;
                if (<>c.<>9__131_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__131_0;
                    evaluator = <>c.<>9__131_0 = x => x.HighlightTextCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand StrikethroughTextCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__133_0;
                if (<>c.<>9__133_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__133_0;
                    evaluator = <>c.<>9__133_0 = x => x.StrikethroughTextCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand UnderlineTextCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__135_0;
                if (<>c.<>9__135_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__135_0;
                    evaluator = <>c.<>9__135_0 = x => x.UnderlineTextCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand DeleteAnnotationCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__137_0;
                if (<>c.<>9__137_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__137_0;
                    evaluator = <>c.<>9__137_0 = x => x.DeleteAnnotationCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ShowAnnotationPropertiesCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__139_0;
                if (<>c.<>9__139_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__139_0;
                    evaluator = <>c.<>9__139_0 = x => x.ShowAnnotationPropertiesCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SetCommentCursorModeCommandInternal
        {
            get
            {
                Func<PdfViewerControl, ICommand> evaluator = <>c.<>9__141_0;
                if (<>c.<>9__141_0 == null)
                {
                    Func<PdfViewerControl, ICommand> local1 = <>c.<>9__141_0;
                    evaluator = <>c.<>9__141_0 = x => x.SetCommentCursorModeCommand;
                }
                return this.PdfViewer.With<PdfViewerControl, ICommand>(evaluator);
            }
        }

        private PdfViewerControl PdfViewer =>
            base.DocumentViewer as PdfViewerControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfCommandProvider.<>c <>9 = new PdfCommandProvider.<>c();
            public static Func<PdfViewerControl, ICommand> <>9__101_0;
            public static Func<PdfViewerControl, ICommand> <>9__103_0;
            public static Func<PdfViewerControl, ICommand> <>9__105_0;
            public static Func<PdfViewerControl, ICommand> <>9__107_0;
            public static Func<PdfViewerControl, ICommand> <>9__109_0;
            public static Func<PdfViewerControl, ICommand> <>9__111_0;
            public static Func<PdfViewerControl, ICommand> <>9__113_0;
            public static Func<PdfViewerControl, ICommand> <>9__115_0;
            public static Func<PdfViewerControl, ICommand> <>9__117_0;
            public static Func<PdfViewerControl, ICommand> <>9__119_0;
            public static Func<PdfViewerControl, ICommand> <>9__121_0;
            public static Func<PdfViewerControl, ICommand> <>9__123_0;
            public static Func<PdfViewerControl, ICommand> <>9__125_0;
            public static Func<PdfViewerControl, ICommand> <>9__127_0;
            public static Func<PdfViewerControl, ICommand> <>9__129_0;
            public static Func<PdfViewerControl, ICommand> <>9__131_0;
            public static Func<PdfViewerControl, ICommand> <>9__133_0;
            public static Func<PdfViewerControl, ICommand> <>9__135_0;
            public static Func<PdfViewerControl, ICommand> <>9__137_0;
            public static Func<PdfViewerControl, ICommand> <>9__139_0;
            public static Func<PdfViewerControl, ICommand> <>9__141_0;
            public static Func<PdfViewerControl, PdfMarkupToolsSettings> <>9__144_6;
            public static Func<PdfMarkupToolsSettings, Color> <>9__144_7;
            public static Func<Color> <>9__144_8;
            public static Func<PdfViewerControl, PdfMarkupToolsSettings> <>9__144_10;
            public static Action<PdfViewerControl> <>9__144_12;
            public static Func<PdfViewerControl, PdfMarkupToolsSettings> <>9__144_14;
            public static Func<PdfMarkupToolsSettings, Color> <>9__144_15;
            public static Func<Color> <>9__144_16;
            public static Func<PdfViewerControl, PdfMarkupToolsSettings> <>9__144_18;
            public static Action<PdfViewerControl> <>9__144_20;
            public static Func<PdfViewerControl, PdfMarkupToolsSettings> <>9__144_22;
            public static Func<PdfMarkupToolsSettings, Color> <>9__144_23;
            public static Func<Color> <>9__144_24;
            public static Func<PdfViewerControl, PdfMarkupToolsSettings> <>9__144_26;
            public static Action<PdfViewerControl> <>9__144_28;
            public static Func<PdfViewerControl, ObservableCollection<RecentFileViewModel>> <>9__144_36;
            public static Action <>9__145_0;
            public static Func<CommandToggleButton, bool> <>9__145_2;
            public static Func<CommandToggleButton, bool> <>9__147_0;
            public static Func<CommandToggleButton, bool> <>9__147_1;
            public static Func<CommandToggleButton, bool> <>9__147_2;
            public static Func<CommandToggleButton, bool> <>9__147_3;
            public static Func<PdfViewerControl, CursorModeType> <>9__148_0;
            public static Func<CursorModeType> <>9__148_1;
            public static Func<PdfViewerControl, bool> <>9__154_0;
            public static Func<bool> <>9__154_1;
            public static Func<PdfViewerControl, bool> <>9__154_2;
            public static Func<bool> <>9__154_3;
            public static Func<PdfViewerControl, bool> <>9__154_4;
            public static Func<bool> <>9__154_5;
            public static Func<PdfViewerControl, PdfPageLayout> <>9__154_6;
            public static Func<PdfPageLayout> <>9__154_7;

            internal void <CreatePageLayoutCommand>b__145_0()
            {
            }

            internal bool <CreatePageLayoutCommand>b__145_2(CommandToggleButton x) => 
                x.CanExecute(null);

            internal ICommand <get_CopyCommandInternal>b__111_0(PdfViewerControl x) => 
                x.CopyCommand;

            internal ICommand <get_DeleteAnnotationCommandInternal>b__137_0(PdfViewerControl x) => 
                x.DeleteAnnotationCommand;

            internal ICommand <get_ExportFormDataCommandInternal>b__121_0(PdfViewerControl x) => 
                x.ExportFormDataCommand;

            internal ICommand <get_HighlightTextCommandInternal>b__131_0(PdfViewerControl x) => 
                x.HighlightTextCommand;

            internal ICommand <get_ImportFormDataCommandInternal>b__119_0(PdfViewerControl x) => 
                x.ImportFormDataCommand;

            internal ICommand <get_OpenAttachmentCommandInternal>b__127_0(PdfViewerControl x) => 
                x.OpenAttachmentCommand;

            internal ICommand <get_OpenDocumentFromWebCommandInternal>b__115_0(PdfViewerControl x) => 
                x.OpenDocumentFromWebCommand;

            internal ICommand <get_PrintDocumentCommandInternal>b__101_0(PdfViewerControl x) => 
                x.PrintDocumentCommand;

            internal ICommand <get_SaveAsCommandInternal>b__109_0(PdfViewerControl x) => 
                x.SaveAsCommand;

            internal ICommand <get_SaveAttachmentCommandInternal>b__129_0(PdfViewerControl x) => 
                x.SaveAttachmentCommand;

            internal ICommand <get_SelectAllCommandInternal>b__107_0(PdfViewerControl x) => 
                x.SelectAllCommand;

            internal ICommand <get_SelectionCommandInternal>b__113_0(PdfViewerControl x) => 
                x.SelectionCommand;

            internal ICommand <get_SetCommentCursorModeCommandInternal>b__141_0(PdfViewerControl x) => 
                x.SetCommentCursorModeCommand;

            internal ICommand <get_SetCursorModeCommandInternal>b__105_0(PdfViewerControl x) => 
                x.SetCursorModeCommand;

            internal ICommand <get_SetPageLayoutCommandInternal>b__123_0(PdfViewerControl x) => 
                x.SetPageLayoutCommand;

            internal ICommand <get_ShowAnnotationPropertiesCommandInternal>b__139_0(PdfViewerControl x) => 
                x.ShowAnnotationPropertiesCommand;

            internal ICommand <get_ShowCoverPageCommandInternal>b__125_0(PdfViewerControl x) => 
                x.ShowCoverPageCommand;

            internal ICommand <get_ShowPropertiesCommandInternal>b__103_0(PdfViewerControl x) => 
                x.ShowPropertiesCommand;

            internal ICommand <get_StrikethroughTextCommandInternal>b__133_0(PdfViewerControl x) => 
                x.StrikethroughTextCommand;

            internal ICommand <get_UnderlineTextCommandInternal>b__135_0(PdfViewerControl x) => 
                x.UnderlineTextCommand;

            internal ICommand <get_UnselectAllCommandInternal>b__117_0(PdfViewerControl x) => 
                x.UnselectAllCommand;

            internal PdfMarkupToolsSettings <InitializeElements>b__144_10(PdfViewerControl y) => 
                y.MarkupToolsSettings;

            internal void <InitializeElements>b__144_12(PdfViewerControl y)
            {
                y.HighlightSelectedText();
            }

            internal PdfMarkupToolsSettings <InitializeElements>b__144_14(PdfViewerControl x) => 
                x.MarkupToolsSettings;

            internal Color <InitializeElements>b__144_15(PdfMarkupToolsSettings x) => 
                x.TextStrikethroughColor;

            internal Color <InitializeElements>b__144_16() => 
                Colors.White;

            internal PdfMarkupToolsSettings <InitializeElements>b__144_18(PdfViewerControl y) => 
                y.MarkupToolsSettings;

            internal void <InitializeElements>b__144_20(PdfViewerControl y)
            {
                y.StrikethroughSelectedText();
            }

            internal PdfMarkupToolsSettings <InitializeElements>b__144_22(PdfViewerControl x) => 
                x.MarkupToolsSettings;

            internal Color <InitializeElements>b__144_23(PdfMarkupToolsSettings x) => 
                x.TextUnderlineColor;

            internal Color <InitializeElements>b__144_24() => 
                Colors.White;

            internal PdfMarkupToolsSettings <InitializeElements>b__144_26(PdfViewerControl y) => 
                y.MarkupToolsSettings;

            internal void <InitializeElements>b__144_28(PdfViewerControl y)
            {
                y.UnderlineSelectedText();
            }

            internal ObservableCollection<RecentFileViewModel> <InitializeElements>b__144_36(PdfViewerControl x) => 
                x.RecentFiles;

            internal PdfMarkupToolsSettings <InitializeElements>b__144_6(PdfViewerControl x) => 
                x.MarkupToolsSettings;

            internal Color <InitializeElements>b__144_7(PdfMarkupToolsSettings x) => 
                x.TextHighlightColor;

            internal Color <InitializeElements>b__144_8() => 
                Colors.White;

            internal CursorModeType <UpdateCursorModeCheckState>b__148_0(PdfViewerControl x) => 
                x.CursorMode;

            internal CursorModeType <UpdateCursorModeCheckState>b__148_1() => 
                CursorModeType.HandTool;

            internal bool <UpdatePageLayoutCheckState>b__154_0(PdfViewerControl x) => 
                x.IsShowCoverPage;

            internal bool <UpdatePageLayoutCheckState>b__154_1() => 
                false;

            internal bool <UpdatePageLayoutCheckState>b__154_2(PdfViewerControl x) => 
                (x.PageLayout == PdfPageLayout.TwoColumnLeft) || (x.PageLayout == PdfPageLayout.TwoColumnRight);

            internal bool <UpdatePageLayoutCheckState>b__154_3() => 
                false;

            internal bool <UpdatePageLayoutCheckState>b__154_4(PdfViewerControl x) => 
                (x.PageLayout == PdfPageLayout.TwoPageLeft) || (x.PageLayout == PdfPageLayout.TwoPageRight);

            internal bool <UpdatePageLayoutCheckState>b__154_5() => 
                false;

            internal PdfPageLayout <UpdatePageLayoutCheckState>b__154_6(PdfViewerControl y) => 
                y.PageLayout;

            internal PdfPageLayout <UpdatePageLayoutCheckState>b__154_7() => 
                PdfPageLayout.OneColumn;

            internal bool <UpdatePageLayoutCommand>b__147_0(CommandToggleButton x) => 
                x is CommandSetPageLayout;

            internal bool <UpdatePageLayoutCommand>b__147_1(CommandToggleButton x) => 
                x.IsChecked;

            internal bool <UpdatePageLayoutCommand>b__147_2(CommandToggleButton x) => 
                x is CommandSetPageLayout;

            internal bool <UpdatePageLayoutCommand>b__147_3(CommandToggleButton x) => 
                x.IsChecked;
        }
    }
}

