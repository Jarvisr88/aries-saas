namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class DocumentCommandProvider : CommandProvider
    {
        private string dllName = Assembly.GetExecutingAssembly().GetName().Name;

        private ICommand CreateExportCommand(ExportFormat format, ICommand exportCommand, bool isSendOperation)
        {
            ExportCommandButton button1 = new ExportCommandButton(format);
            button1.Command = exportCommand;
            button1.SmallGlyph = ExportFormatHelper.GetImageUri(format, GlyphSize.Small);
            button1.LargeGlyph = ExportFormatHelper.GetImageUri(format, GlyphSize.Large);
            button1.Caption = PrintingLocalizer.GetString(ExportFormatHelper.GetExportCaption(format, isSendOperation));
            button1.Tag = isSendOperation ? "Send" : "Export";
            return button1;
        }

        protected override ICommand CreateZoomModeAndZoomFactorItem(string dllName)
        {
            double[] numArray = new double[] { 0.1, 0.25, 0.5, 0.75, 1.0, 1.5, 2.0, 5.0 };
            ObservableCollection<CommandToggleButton> zoomItems = new ObservableCollection<CommandToggleButton>();
            CommandCheckItems items1 = new CommandCheckItems();
            items1.Caption = PrintingLocalizer.GetString(PrintingStringId.Zoom);
            items1.Hint = PrintingLocalizer.GetString(PrintingStringId.Zoom_Hint);
            items1.Group = "zoomGroup";
            CommandCheckItems items2 = items1;
            Action executeMethod = <>c.<>9__134_0;
            if (<>c.<>9__134_0 == null)
            {
                Action local1 = <>c.<>9__134_0;
                executeMethod = <>c.<>9__134_0 = delegate {
                };
            }
            items2.Command = DelegateCommandFactory.Create(executeMethod, delegate {
                Func<CommandToggleButton, bool> predicate = <>c.<>9__134_2;
                if (<>c.<>9__134_2 == null)
                {
                    Func<CommandToggleButton, bool> local1 = <>c.<>9__134_2;
                    predicate = <>c.<>9__134_2 = x => x.CanExecute(null);
                }
                return zoomItems.Any<CommandToggleButton>(predicate);
            });
            CommandCheckItems local2 = items2;
            local2.Items = zoomItems;
            local2.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, @"\Images\Zoom_16x16.png");
            local2.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, @"\Images\Zoom_32x32.png");
            CommandCheckItems items = local2;
            DelegateCommand<double> setZoomFactorCommand = DelegateCommandFactory.Create<double>(delegate (double x) {
                this.SetZoomFactorCommandInternal.Execute(x);
                base.UpdateZoomCommand();
            }, x => this.SetZoomFactorCommandInternal.CanExecute(x));
            DelegateCommand<ZoomMode> setZoomModeCommand = DelegateCommandFactory.Create<ZoomMode>(delegate (ZoomMode x) {
                this.SetZoomModeCommandInternal.Execute(x);
                base.UpdateZoomCommand();
            }, x => this.SetZoomModeCommandInternal.CanExecute(x));
            foreach (double num2 in numArray)
            {
                Func<ICommand> <>9__7;
                CommandSetZoomFactorAndModeItem item1 = new CommandSetZoomFactorAndModeItem();
                item1.Caption = $"{num2:P0}";
                CommandSetZoomFactorAndModeItem item5 = item1;
                Func<ICommand> getCommand = <>9__7;
                if (<>9__7 == null)
                {
                    Func<ICommand> local3 = <>9__7;
                    getCommand = <>9__7 = () => setZoomFactorCommand;
                }
                item1.Command = new CommandWrapper(getCommand);
                CommandSetZoomFactorAndModeItem local4 = item1;
                local4.ZoomFactor = num2;
                zoomItems.Add(local4);
            }
            CommandSetZoomFactorAndModeItem item = new CommandSetZoomFactorAndModeItem();
            item.IsSeparator = true;
            zoomItems.Add(item);
            CommandSetZoomFactorAndModeItem item3 = new CommandSetZoomFactorAndModeItem();
            item3.Caption = PrintingLocalizer.GetString(PrintingStringId.ZoomToWholePage);
            item3.Command = new CommandWrapper(() => setZoomModeCommand);
            item3.ZoomMode = ZoomMode.PageLevel;
            item3.KeyGesture = new KeyGesture(Key.D0, ModifierKeys.Control);
            item3.GroupIndex = 2;
            zoomItems.Add(item3);
            CommandSetZoomFactorAndModeItem item4 = new CommandSetZoomFactorAndModeItem();
            item4.Caption = PrintingLocalizer.GetString(PrintingStringId.ZoomToPageWidth);
            item4.Command = new CommandWrapper(() => setZoomModeCommand);
            item4.ZoomMode = ZoomMode.FitToWidth;
            item4.KeyGesture = new KeyGesture(Key.D1, ModifierKeys.Control);
            item4.GroupIndex = 2;
            zoomItems.Add(item4);
            return items;
        }

        protected virtual IEnumerable<ExportFormat> GetExportFormats()
        {
            ExportFormat[] second = new ExportFormat[] { ExportFormat.Prnx, ExportFormat.Xps };
            IEnumerable<ExportFormat> availableFormats = Enum.GetValues(typeof(ExportFormat)).Cast<ExportFormat>().Except<ExportFormat>(second);
            this.DocumentViewer.Do<DocumentPreviewControl>(delegate (DocumentPreviewControl x) {
                IEnumerable<ExportFormat> enumerable;
                ObservableCollection<ExportFormat> hiddenExportFormats = x.HiddenExportFormats;
                if (enumerable == null)
                {
                    ObservableCollection<ExportFormat> local1 = x.HiddenExportFormats;
                    hiddenExportFormats = (ObservableCollection<ExportFormat>) Enumerable.Empty<ExportFormat>();
                }
                availableFormats = availableFormats.Except<ExportFormat>(hiddenExportFormats);
            });
            return availableFormats;
        }

        protected virtual IEnumerable<ExportFormat> GetSendFormats()
        {
            ExportFormat[] second = new ExportFormat[] { ExportFormat.Prnx, ExportFormat.Htm, ExportFormat.Xps };
            IEnumerable<ExportFormat> availableFormats = Enum.GetValues(typeof(ExportFormat)).Cast<ExportFormat>().Except<ExportFormat>(second);
            this.DocumentViewer.Do<DocumentPreviewControl>(delegate (DocumentPreviewControl x) {
                IEnumerable<ExportFormat> enumerable;
                ObservableCollection<ExportFormat> hiddenExportFormats = x.HiddenExportFormats;
                if (enumerable == null)
                {
                    ObservableCollection<ExportFormat> local1 = x.HiddenExportFormats;
                    hiddenExportFormats = (ObservableCollection<ExportFormat>) Enumerable.Empty<ExportFormat>();
                }
                availableFormats = availableFormats.Except<ExportFormat>(hiddenExportFormats);
            });
            return availableFormats;
        }

        protected override void InitializeElements()
        {
            base.InitializeElements();
            ((CommandButton) base.OpenDocumentCommand).Caption = PrintingLocalizer.GetString(PrintingStringId.Open);
            ((CommandButton) base.OpenDocumentCommand).Hint = PrintingLocalizer.GetString(PrintingStringId.Open_Hint);
            ((CommandButton) base.OpenDocumentCommand).LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Open_32x32.png");
            ((CommandButton) base.OpenDocumentCommand).SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Open_16x16.png");
            CommandButton button1 = new CommandButton();
            button1.Caption = PrintingLocalizer.GetString(PrintingStringId.Save);
            button1.Hint = PrintingLocalizer.GetString(PrintingStringId.Save_Hint);
            button1.Command = new CommandWrapper(() => this.SaveInternal);
            button1.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Save_32x32.png");
            button1.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Save_16x16.png");
            button1.Group = "DocumentGroup";
            this.SaveCommand = button1;
            CommandCheckItem item1 = new CommandCheckItem();
            item1.Caption = PrintingLocalizer.GetString(PrintingStringId.Parameters);
            item1.Hint = PrintingLocalizer.GetString(PrintingStringId.Parameters_Hint);
            item1.Command = new CommandWrapper(() => this.ToggleParametersPanelInternal);
            item1.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Parameters_32x32.png");
            item1.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Parameters_16x16.png");
            item1.Group = "DocumentGroup";
            this.ToggleParametersPanelCommand = item1;
            CommandCheckItem item2 = new CommandCheckItem();
            item2.Caption = PrintingLocalizer.GetString(PrintingStringId.DocumentMap);
            item2.Hint = PrintingLocalizer.GetString(PrintingStringId.DocumentMap_Hint);
            item2.Command = new CommandWrapper(() => this.ToggleDocumentMapInternal);
            item2.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\DocumentMap_32x32.png");
            item2.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\DocumentMap_16x16.png");
            item2.Group = "DocumentGroup";
            this.ToggleDocumentMapCommand = item2;
            CommandCheckItem item3 = new CommandCheckItem();
            item3.Caption = PrintingLocalizer.GetString(PrintingStringId.Thumbnails);
            item3.Hint = PrintingLocalizer.GetString(PrintingStringId.Thumbnails_Hint);
            item3.Command = new CommandWrapper(() => this.ToggleThumbnailsInternal);
            item3.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Thumbnails_32x32.png");
            item3.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Thumbnails_16x16.png");
            item3.Group = "DocumentGroup";
            this.ToggleThumbnailsCommand = item3;
            CommandCheckItem item4 = new CommandCheckItem();
            item4.Caption = PrintingLocalizer.GetString(PrintingStringId.EditingFields);
            item4.Hint = PrintingLocalizer.GetString(PrintingStringId.EditingFields_Hint);
            item4.Command = new CommandWrapper(() => this.ToggleEditingFieldsInternal);
            item4.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\HighlightEditingFields_32x32.png");
            item4.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\HighlightEditingFields_16x16.png");
            item4.Group = "DocumentGroup";
            this.ToggleEditingFieldsCommand = item4;
            CommandButton button2 = new CommandButton();
            button2.Caption = PrintingLocalizer.GetString(PrintingStringId.Print);
            button2.Hint = PrintingLocalizer.GetString(PrintingStringId.Print_Hint);
            button2.Command = new CommandWrapper(() => this.PrintInternal);
            button2.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\PrintDialog_32x32.png");
            button2.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\PrintDialog_16x16.png");
            button2.Group = "PrintGroup";
            this.PrintCommand = button2;
            CommandButton button3 = new CommandButton();
            button3.Caption = PrintingLocalizer.GetString(PrintingStringId.PrintDirect);
            button3.Hint = PrintingLocalizer.GetString(PrintingStringId.PrintDirect_Hint);
            button3.Command = new CommandWrapper(() => this.PrintDirectInternal);
            button3.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Print_32x32.png");
            button3.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Print_16x16.png");
            button3.Group = "PrintGroup";
            this.PrintDirectCommand = button3;
            CommandButton button4 = new CommandButton();
            button4.Caption = PrintingLocalizer.GetString(PrintingStringId.PageSetup);
            button4.Hint = PrintingLocalizer.GetString(PrintingStringId.PageSetup_Hint);
            button4.Command = new CommandWrapper(() => this.PageSetupInternal);
            button4.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\PageSetup_32x32.png");
            button4.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\PageSetup_16x16.png");
            button4.Group = "PrintGroup";
            this.PageSetupCommand = button4;
            CommandButton button5 = new CommandButton();
            button5.Caption = PrintingLocalizer.GetString(PrintingStringId.Scaling);
            button5.Hint = PrintingLocalizer.GetString(PrintingStringId.Scaling_Hint);
            button5.Command = new CommandWrapper(() => this.ScaleInternal);
            button5.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Scaling_32x32.png");
            button5.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Scaling_16x16.png");
            button5.Group = "PrintGroup";
            this.ScaleCommand = button5;
            CommandButton button6 = new CommandButton();
            button6.Caption = PrintingLocalizer.GetString(PrintingStringId.FirstPage);
            button6.Hint = PrintingLocalizer.GetString(PrintingStringId.FirstPage_Hint);
            button6.Command = new CommandWrapper(() => this.FirstPageInternal);
            button6.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\First_32x32.png");
            button6.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\First_16x16.png");
            button6.Group = "NavigationGroup";
            this.FirstPageCommand = button6;
            ((CommandButton) base.NextPageCommand).Do<CommandButton>(delegate (CommandButton x) {
                x.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Next_32x32.png");
                x.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Next_16x16.png");
                x.Caption = PrintingLocalizer.GetString(PrintingStringId.NextPage);
                x.Hint = PrintingLocalizer.GetString(PrintingStringId.NextPage_Hint);
            });
            ((CommandButton) base.PreviousPageCommand).Do<CommandButton>(delegate (CommandButton x) {
                x.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Prev_32x32.png");
                x.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Prev_16x16.png");
                x.Caption = PrintingLocalizer.GetString(PrintingStringId.PreviousPage);
                x.Hint = PrintingLocalizer.GetString(PrintingStringId.PreviousPage_Hint);
            });
            CommandButton button7 = new CommandButton();
            button7.Caption = PrintingLocalizer.GetString(PrintingStringId.LastPage);
            button7.Hint = PrintingLocalizer.GetString(PrintingStringId.LastPage_Hint);
            button7.Command = new CommandWrapper(() => this.LastPageInternal);
            button7.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Last_32x32.png");
            button7.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Last_16x16.png");
            button7.Group = "NavigationGroup";
            this.LastPageCommand = button7;
            CommandCheckItem item5 = new CommandCheckItem();
            item5.Caption = PrintingLocalizer.GetString(PrintingStringId.NavigationPane_ButtonCaption);
            item5.Hint = PrintingLocalizer.GetString(PrintingStringId.NavigationPane_ButtonHint);
            item5.Command = new CommandWrapper(() => this.ToggleNavigationPaneInternal);
            item5.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\NavigationPane_32x32.png");
            item5.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\NavigationPane_16x16.png");
            item5.Group = "DocumentGroup";
            this.ToggleNavigationPaneCommand = item5;
            ((CommandButton) base.ZoomInCommand).Do<CommandButton>(delegate (CommandButton x) {
                x.Caption = PrintingLocalizer.GetString(PrintingStringId.IncreaseZoom);
                x.Hint = PrintingLocalizer.GetString(PrintingStringId.IncreaseZoom_Hint);
                x.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\ZoomIn_32x32.png");
                x.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\ZoomIn_16x16.png");
            });
            ((CommandButton) base.ZoomOutCommand).Do<CommandButton>(delegate (CommandButton x) {
                x.Caption = PrintingLocalizer.GetString(PrintingStringId.DecreaseZoom);
                x.Hint = PrintingLocalizer.GetString(PrintingStringId.DecreaseZoom_Hint);
                x.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\ZoomOut_32x32.png");
                x.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\ZoomOut_16x16.png");
            });
            CommandWrapper exportCommandWrapper = new CommandWrapper(() => this.ExportInternal);
            CommandSplitItem item6 = new CommandSplitItem();
            item6.Caption = PrintingLocalizer.GetString(PrintingStringId.ExportFile);
            item6.Hint = PrintingLocalizer.GetString(PrintingStringId.ExportFile_Hint);
            item6.Command = exportCommandWrapper;
            item6.Commands = new ObservableCollection<ICommand>(from x in this.GetExportFormats() select this.CreateExportCommand(x, exportCommandWrapper, false));
            item6.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Export_32x32.png");
            item6.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Export_16x16.png");
            item6.Group = "NavigationGroup";
            this.ExportSplitCommand = item6;
            CommandWrapper sendCommandWrapper = new CommandWrapper(() => this.SendInternal);
            CommandSplitItem item7 = new CommandSplitItem();
            item7.Caption = PrintingLocalizer.GetString(PrintingStringId.SendFile);
            item7.Hint = PrintingLocalizer.GetString(PrintingStringId.SendFile_Hint);
            item7.Command = sendCommandWrapper;
            item7.Commands = new ObservableCollection<ICommand>(from x in this.GetSendFormats() select this.CreateExportCommand(x, sendCommandWrapper, false));
            item7.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Mail_32x32.png");
            item7.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Mail_16x16.png");
            item7.Group = "NavigationGroup";
            this.SendSplitCommand = item7;
            SetCursorModeItem item8 = new SetCursorModeItem();
            item8.Caption = PrintingLocalizer.GetString(PrintingStringId.HandTool);
            item8.Command = new CommandWrapper(() => this.SetCursorModeCommandInternal);
            item8.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\HandTool_32x32.png");
            item8.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\HandTool_16x16.png");
            item8.Group = "CursorMode";
            item8.CommandValue = CursorModeType.HandTool;
            this.HandToolCommand = item8;
            SetCursorModeItem item9 = new SetCursorModeItem();
            item9.Caption = PrintingLocalizer.GetString(PrintingStringId.SelectTool);
            item9.Command = new CommandWrapper(() => this.SetCursorModeCommandInternal);
            item9.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\SelectTool_32x32.png");
            item9.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\SelectTool_16x16.png");
            item9.Group = "CursorMode";
            item9.CommandValue = CursorModeType.SelectTool;
            this.SelectToolCommand = item9;
            CommandButton button8 = new CommandButton();
            button8.Caption = PrintingLocalizer.GetString(PrintingStringId.Copy);
            button8.Command = new CommandWrapper(() => this.CopyCommandInternal);
            button8.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Copy_32x32.png");
            button8.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Copy_16x16.png");
            this.CopyCommand = button8;
            List<PageLayoutCommandButton> list = new List<PageLayoutCommandButton>();
            CommandCheckItems items1 = new CommandCheckItems();
            items1.Caption = PrintingLocalizer.GetString(PrintingStringId.PageLayout);
            items1.Hint = PrintingLocalizer.GetString(PrintingStringId.PageLayout_Hint);
            CommandCheckItems items2 = items1;
            Action executeMethod = <>c.<>9__133_23;
            if (<>c.<>9__133_23 == null)
            {
                Action local1 = <>c.<>9__133_23;
                executeMethod = <>c.<>9__133_23 = delegate {
                };
            }
            items2.Command = DelegateCommandFactory.Create(executeMethod, <>c.<>9__133_24 ??= () => true);
            CommandCheckItems local3 = items2;
            local3.Items = (IEnumerable<CommandToggleButton>) list;
            this.SetPageLayoutSplitCommand = local3;
            PageLayoutCommandButton item = new PageLayoutCommandButton();
            item.Caption = PrintingLocalizer.GetString(PrintingStringId.PageLayout_SinglePage);
            item.Hint = PrintingLocalizer.GetString(PrintingStringId.PageLayout_SinglePage_Hint);
            item.Command = new CommandWrapper(() => this.SetPageLayoutCommandInternal);
            PageLayoutSettings settings1 = new PageLayoutSettings();
            settings1.PageDisplayMode = PageDisplayMode.Single;
            item.PageLayoutSettings = settings1;
            item.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\SinglePage_32x32.png");
            item.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\SinglePage_16x16.png");
            list.Add(item);
            PageLayoutCommandButton button10 = new PageLayoutCommandButton();
            button10.Caption = PrintingLocalizer.GetString(PrintingStringId.PageLayout_TwoPages);
            button10.Hint = PrintingLocalizer.GetString(PrintingStringId.PageLayout_TwoPages_Hint);
            button10.Command = new CommandWrapper(() => this.SetPageLayoutCommandInternal);
            PageLayoutSettings settings2 = new PageLayoutSettings();
            settings2.PageDisplayMode = PageDisplayMode.Columns;
            settings2.ColumnCount = 2;
            button10.PageLayoutSettings = settings2;
            button10.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\TwoPages_32x32.png");
            button10.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\TwoPages_16x16.png");
            list.Add(button10);
            PageLayoutCommandButton button11 = new PageLayoutCommandButton();
            button11.Caption = PrintingLocalizer.GetString(PrintingStringId.PageLayout_WrapPages);
            button11.Hint = PrintingLocalizer.GetString(PrintingStringId.PageLayout_WrapPages_Hint);
            button11.Command = new CommandWrapper(() => this.SetPageLayoutCommandInternal);
            PageLayoutSettings settings3 = new PageLayoutSettings();
            settings3.PageDisplayMode = PageDisplayMode.Wrap;
            button11.PageLayoutSettings = settings3;
            button11.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\WrapPages_32x32.png");
            button11.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\WrapPages_16x16.png");
            list.Add(button11);
            CommandCheckItem item10 = new CommandCheckItem();
            item10.Caption = PrintingLocalizer.GetString(PrintingStringId.ShowCoverPage);
            item10.Hint = PrintingLocalizer.GetString(PrintingStringId.ShowCoverPage_Hint);
            item10.Command = new CommandWrapper(() => this.ToggleShowCoverPageCommandInternal);
            item10.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\CoverPage_32x32.png");
            item10.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\CoverPage_16x16.png");
            this.ToggleShowCoverPageCommand = item10;
            EnableScrollingCheckItem item11 = new EnableScrollingCheckItem();
            item11.Caption = PrintingLocalizer.GetString(PrintingStringId.EnableContinuousScrolling);
            item11.Hint = PrintingLocalizer.GetString(PrintingStringId.EnableContinuousScrolling_Hint);
            item11.Command = new CommandWrapper(() => this.ToggleEnableContinuousScrollingCommandInternal);
            item11.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\EnabledScrolling_32x32.png");
            item11.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\EnabledScrolling_16x16.png");
            this.ToggleEnableContinuousScrollingCommand = item11;
            this.UpdatePageLayoutCommands();
            ((CommandButton) base.ShowFindTextCommand).Do<CommandButton>(delegate (CommandButton x) {
                x.Caption = PrintingLocalizer.GetString(PrintingStringId.Search);
                x.Hint = PrintingLocalizer.GetString(PrintingStringId.Search_Hint);
                x.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Find_32x32.png");
                x.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Find_16x16.png");
            });
            CommandButton button12 = new CommandButton();
            button12.Caption = PrintingLocalizer.GetString(PrintingStringId.Watermark);
            button12.Hint = PrintingLocalizer.GetString(PrintingStringId.Watermark_Hint);
            button12.Command = new CommandWrapper(() => this.SetWatermarkInternal);
            button12.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Watermark_32x32.png");
            button12.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Watermark_16x16.png");
            button12.Group = "Watermark";
            this.SetWatermarkCommand = button12;
            CommandButton button13 = new CommandButton();
            button13.Caption = PrintingLocalizer.GetString(PrintingStringId.StopPageBuilding);
            button13.Command = new CommandWrapper(() => this.StopPageBuildingCommandInternal);
            button13.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(this.dllName, @"\Images\BarItems\Stop_16x16.png");
            this.StopPageBuildingCommand = button13;
        }

        internal void UpdateCommands()
        {
            Func<DevExpress.Xpf.Printing.DocumentPresenterControl, bool> evaluator = <>c.<>9__138_0;
            if (<>c.<>9__138_0 == null)
            {
                Func<DevExpress.Xpf.Printing.DocumentPresenterControl, bool> local1 = <>c.<>9__138_0;
                evaluator = <>c.<>9__138_0 = x => x.IsContentLoaded;
            }
            if (this.DocumentViewer.DocumentPresenter.Return<DevExpress.Xpf.Printing.DocumentPresenterControl, bool>(evaluator, <>c.<>9__138_1 ??= () => true))
            {
                ((CommandCheckItem) this.ToggleDocumentMapCommand).Do<CommandCheckItem>(delegate (CommandCheckItem c) {
                    Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> func1 = <>c.<>9__138_3;
                    if (<>c.<>9__138_3 == null)
                    {
                        Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__138_3;
                        func1 = <>c.<>9__138_3 = x => x.HasBookmarks;
                    }
                    bool canShow = this.DocumentViewer.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(func1, <>c.<>9__138_4 ??= () => false);
                    c.UpdateCheckState(() => canShow && this.DocumentViewer.PropertyProvider.ShowDocumentMap);
                });
                ((CommandCheckItem) this.ToggleParametersPanelCommand).Do<CommandCheckItem>(delegate (CommandCheckItem c) {
                    bool canShow = this.DocumentViewer.ParametersModel.HasVisibleParameters;
                    c.UpdateCheckState(() => canShow && this.DocumentViewer.PropertyProvider.ShowParametersPanel);
                });
                ((CommandCheckItem) this.ToggleThumbnailsCommand).Do<CommandCheckItem>(delegate (CommandCheckItem c) {
                    bool canShow = (this.DocumentViewer.Document != null) && !this.DocumentViewer.Document.IsRemoteReportDocumentSource();
                    c.UpdateCheckState(() => canShow && this.DocumentViewer.ShowThumbnails);
                });
                ((CommandCheckItem) this.ToggleNavigationPaneCommand).Do<CommandCheckItem>(delegate (CommandCheckItem c) {
                    bool canShow = this.DocumentViewer.CanToggleNavigationPane();
                    c.UpdateCheckState(() => canShow && this.DocumentViewer.ShowNavigationPane);
                });
                ((CommandCheckItem) this.ToggleParametersPanelCommand).Do<CommandCheckItem>(delegate (CommandCheckItem c) {
                    bool canShow = (this.DocumentViewer.Document != null) && !this.DocumentViewer.Document.IsRemoteReportDocumentSource();
                    c.UpdateCheckState(() => canShow && this.DocumentViewer.ShowThumbnails);
                });
                ((CommandCheckItem) this.ToggleEditingFieldsCommand).Do<CommandCheckItem>(delegate (CommandCheckItem c) {
                    Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> func1 = <>c.<>9__138_15;
                    if (<>c.<>9__138_15 == null)
                    {
                        Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__138_15;
                        func1 = <>c.<>9__138_15 = x => x.EditingFields.Any<EditingField>();
                    }
                    bool canShow = this.DocumentViewer.Document.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(func1, <>c.<>9__138_16 ??= () => false);
                    c.UpdateCheckState(() => canShow && this.DocumentViewer.HighlightEditingFields);
                });
                Func<DocumentPreviewControl, CursorModeType> func2 = <>c.<>9__138_18;
                if (<>c.<>9__138_18 == null)
                {
                    Func<DocumentPreviewControl, CursorModeType> local3 = <>c.<>9__138_18;
                    func2 = <>c.<>9__138_18 = x => x.CursorMode;
                }
                CursorModeType actualCursorMode = this.DocumentViewer.Return<DocumentPreviewControl, CursorModeType>(func2, <>c.<>9__138_19 ??= () => CursorModeType.HandTool);
                ((CommandCheckItem) this.HandToolCommand).UpdateCheckState(() => actualCursorMode == CursorModeType.HandTool);
                ((CommandCheckItem) this.SelectToolCommand).UpdateCheckState(() => actualCursorMode == CursorModeType.SelectTool);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        internal void UpdateExportCommands()
        {
            ((CommandSplitItem) this.ExportSplitCommand).Do<CommandSplitItem>(c => c.Commands = new ObservableCollection<ICommand>(from x in this.GetExportFormats() select this.CreateExportCommand(x, c.Command, false)));
            ((CommandSplitItem) this.SendSplitCommand).Do<CommandSplitItem>(c => c.Commands = new ObservableCollection<ICommand>(from x in this.GetSendFormats() select this.CreateExportCommand(x, c.Command, false)));
        }

        internal void UpdatePageLayoutCommands()
        {
            // Unresolved stack state at '0000011A'
        }

        private DocumentPreviewControl DocumentViewer =>
            base.DocumentViewer as DocumentPreviewControl;

        public ICommand HandToolCommand { get; protected set; }

        public ICommand SelectToolCommand { get; protected set; }

        public ICommand CopyCommand { get; private set; }

        public ICommand ToggleParametersPanelCommand { get; private set; }

        public ICommand ToggleDocumentMapCommand { get; private set; }

        public ICommand ToggleThumbnailsCommand { get; private set; }

        public ICommand ToggleNavigationPaneCommand { get; private set; }

        public ICommand ToggleEditingFieldsCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand PrintCommand { get; private set; }

        public ICommand PrintDirectCommand { get; private set; }

        public ICommand PageSetupCommand { get; private set; }

        public ICommand ScaleCommand { get; private set; }

        public ICommand FirstPageCommand { get; private set; }

        public ICommand LastPageCommand { get; private set; }

        public ICommand ExportSplitCommand { get; private set; }

        public ICommand SendSplitCommand { get; private set; }

        public ICommand SetWatermarkCommand { get; private set; }

        public ICommand StopPageBuildingCommand { get; private set; }

        public ICommand SetPageLayoutSplitCommand { get; private set; }

        public ICommand ToggleShowCoverPageCommand { get; private set; }

        public ICommand ToggleEnableContinuousScrollingCommand { get; private set; }

        protected internal virtual ICommand SetCursorModeCommandInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__92_0;
                if (<>c.<>9__92_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__92_0;
                    evaluator = <>c.<>9__92_0 = x => x.SetCursorModeCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand CopyCommandInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__94_0;
                if (<>c.<>9__94_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__94_0;
                    evaluator = <>c.<>9__94_0 = x => x.CopyCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SaveInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__96_0;
                if (<>c.<>9__96_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__96_0;
                    evaluator = <>c.<>9__96_0 = x => x.SaveCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ToggleParametersPanelInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__98_0;
                if (<>c.<>9__98_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__98_0;
                    evaluator = <>c.<>9__98_0 = x => x.ToggleParametersPanelCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ExportInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__100_0;
                if (<>c.<>9__100_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__100_0;
                    evaluator = <>c.<>9__100_0 = x => x.ExportCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SendInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__102_0;
                if (<>c.<>9__102_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__102_0;
                    evaluator = <>c.<>9__102_0 = x => x.SendCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand LastPageInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__104_0;
                if (<>c.<>9__104_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__104_0;
                    evaluator = <>c.<>9__104_0 = x => x.LastPageCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand FirstPageInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__106_0;
                if (<>c.<>9__106_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__106_0;
                    evaluator = <>c.<>9__106_0 = x => x.FirstPageCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ScaleInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__108_0;
                if (<>c.<>9__108_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__108_0;
                    evaluator = <>c.<>9__108_0 = x => x.ScaleCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand PageSetupInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__110_0;
                if (<>c.<>9__110_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__110_0;
                    evaluator = <>c.<>9__110_0 = x => x.PageSetupCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand PrintDirectInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__112_0;
                if (<>c.<>9__112_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__112_0;
                    evaluator = <>c.<>9__112_0 = x => x.PrintDirectCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand PrintInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__114_0;
                if (<>c.<>9__114_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__114_0;
                    evaluator = <>c.<>9__114_0 = x => x.PrintCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ToggleDocumentMapInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__116_0;
                if (<>c.<>9__116_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__116_0;
                    evaluator = <>c.<>9__116_0 = x => x.ToggleDocumentMapCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ToggleThumbnailsInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__118_0;
                if (<>c.<>9__118_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__118_0;
                    evaluator = <>c.<>9__118_0 = x => x.ToggleThumbnailsCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ToggleNavigationPaneInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__120_0;
                if (<>c.<>9__120_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__120_0;
                    evaluator = <>c.<>9__120_0 = x => x.ToggleNavigationPaneCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ToggleEditingFieldsInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__122_0;
                if (<>c.<>9__122_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__122_0;
                    evaluator = <>c.<>9__122_0 = x => x.ToggleEditingFieldsCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SetWatermarkInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__124_0;
                if (<>c.<>9__124_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__124_0;
                    evaluator = <>c.<>9__124_0 = x => x.SetWatermarkCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand StopPageBuildingCommandInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__126_0;
                if (<>c.<>9__126_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__126_0;
                    evaluator = <>c.<>9__126_0 = x => x.StopPageBuildingCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand SetPageLayoutCommandInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__128_0;
                if (<>c.<>9__128_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__128_0;
                    evaluator = <>c.<>9__128_0 = x => x.SetPageLayoutCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ToggleShowCoverPageCommandInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__130_0;
                if (<>c.<>9__130_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__130_0;
                    evaluator = <>c.<>9__130_0 = x => x.ToggleShowCoverPageCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand ToggleEnableContinuousScrollingCommandInternal
        {
            get
            {
                Func<DocumentPreviewControl, ICommand> evaluator = <>c.<>9__132_0;
                if (<>c.<>9__132_0 == null)
                {
                    Func<DocumentPreviewControl, ICommand> local1 = <>c.<>9__132_0;
                    evaluator = <>c.<>9__132_0 = x => x.ToggleEnableContinuousScrollingCommand;
                }
                return this.DocumentViewer.With<DocumentPreviewControl, ICommand>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentCommandProvider.<>c <>9 = new DocumentCommandProvider.<>c();
            public static Func<DocumentPreviewControl, ICommand> <>9__92_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__94_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__96_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__98_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__100_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__102_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__104_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__106_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__108_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__110_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__112_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__114_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__116_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__118_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__120_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__122_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__124_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__126_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__128_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__130_0;
            public static Func<DocumentPreviewControl, ICommand> <>9__132_0;
            public static Action <>9__133_23;
            public static Func<bool> <>9__133_24;
            public static Action <>9__134_0;
            public static Func<CommandToggleButton, bool> <>9__134_2;
            public static Func<DevExpress.Xpf.Printing.DocumentPresenterControl, bool> <>9__138_0;
            public static Func<bool> <>9__138_1;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__138_3;
            public static Func<bool> <>9__138_4;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__138_15;
            public static Func<bool> <>9__138_16;
            public static Func<DocumentPreviewControl, CursorModeType> <>9__138_18;
            public static Func<CursorModeType> <>9__138_19;
            public static Func<DocumentPreviewControl, PageLayoutSettings> <>9__140_0;
            public static Func<PageLayoutSettings> <>9__140_1;
            public static Func<CommandToggleButton, bool> <>9__140_3;
            public static Func<CommandToggleButton, bool> <>9__140_4;
            public static Func<CommandToggleButton, Uri> <>9__140_5;
            public static Func<CommandToggleButton, Uri> <>9__140_7;
            public static Func<DocumentPreviewControl, bool> <>9__140_10;
            public static Func<bool> <>9__140_11;
            public static Func<DocumentPreviewControl, bool> <>9__140_13;
            public static Func<bool> <>9__140_14;

            internal void <CreateZoomModeAndZoomFactorItem>b__134_0()
            {
            }

            internal bool <CreateZoomModeAndZoomFactorItem>b__134_2(CommandToggleButton x) => 
                x.CanExecute(null);

            internal ICommand <get_CopyCommandInternal>b__94_0(DocumentPreviewControl x) => 
                x.CopyCommand;

            internal ICommand <get_ExportInternal>b__100_0(DocumentPreviewControl x) => 
                x.ExportCommand;

            internal ICommand <get_FirstPageInternal>b__106_0(DocumentPreviewControl x) => 
                x.FirstPageCommand;

            internal ICommand <get_LastPageInternal>b__104_0(DocumentPreviewControl x) => 
                x.LastPageCommand;

            internal ICommand <get_PageSetupInternal>b__110_0(DocumentPreviewControl x) => 
                x.PageSetupCommand;

            internal ICommand <get_PrintDirectInternal>b__112_0(DocumentPreviewControl x) => 
                x.PrintDirectCommand;

            internal ICommand <get_PrintInternal>b__114_0(DocumentPreviewControl x) => 
                x.PrintCommand;

            internal ICommand <get_SaveInternal>b__96_0(DocumentPreviewControl x) => 
                x.SaveCommand;

            internal ICommand <get_ScaleInternal>b__108_0(DocumentPreviewControl x) => 
                x.ScaleCommand;

            internal ICommand <get_SendInternal>b__102_0(DocumentPreviewControl x) => 
                x.SendCommand;

            internal ICommand <get_SetCursorModeCommandInternal>b__92_0(DocumentPreviewControl x) => 
                x.SetCursorModeCommand;

            internal ICommand <get_SetPageLayoutCommandInternal>b__128_0(DocumentPreviewControl x) => 
                x.SetPageLayoutCommand;

            internal ICommand <get_SetWatermarkInternal>b__124_0(DocumentPreviewControl x) => 
                x.SetWatermarkCommand;

            internal ICommand <get_StopPageBuildingCommandInternal>b__126_0(DocumentPreviewControl x) => 
                x.StopPageBuildingCommand;

            internal ICommand <get_ToggleDocumentMapInternal>b__116_0(DocumentPreviewControl x) => 
                x.ToggleDocumentMapCommand;

            internal ICommand <get_ToggleEditingFieldsInternal>b__122_0(DocumentPreviewControl x) => 
                x.ToggleEditingFieldsCommand;

            internal ICommand <get_ToggleEnableContinuousScrollingCommandInternal>b__132_0(DocumentPreviewControl x) => 
                x.ToggleEnableContinuousScrollingCommand;

            internal ICommand <get_ToggleNavigationPaneInternal>b__120_0(DocumentPreviewControl x) => 
                x.ToggleNavigationPaneCommand;

            internal ICommand <get_ToggleParametersPanelInternal>b__98_0(DocumentPreviewControl x) => 
                x.ToggleParametersPanelCommand;

            internal ICommand <get_ToggleShowCoverPageCommandInternal>b__130_0(DocumentPreviewControl x) => 
                x.ToggleShowCoverPageCommand;

            internal ICommand <get_ToggleThumbnailsInternal>b__118_0(DocumentPreviewControl x) => 
                x.ToggleThumbnailsCommand;

            internal void <InitializeElements>b__133_23()
            {
            }

            internal bool <InitializeElements>b__133_24() => 
                true;

            internal bool <UpdateCommands>b__138_0(DevExpress.Xpf.Printing.DocumentPresenterControl x) => 
                x.IsContentLoaded;

            internal bool <UpdateCommands>b__138_1() => 
                true;

            internal bool <UpdateCommands>b__138_15(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.EditingFields.Any<EditingField>();

            internal bool <UpdateCommands>b__138_16() => 
                false;

            internal CursorModeType <UpdateCommands>b__138_18(DocumentPreviewControl x) => 
                x.CursorMode;

            internal CursorModeType <UpdateCommands>b__138_19() => 
                CursorModeType.HandTool;

            internal bool <UpdateCommands>b__138_3(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.HasBookmarks;

            internal bool <UpdateCommands>b__138_4() => 
                false;

            internal PageLayoutSettings <UpdatePageLayoutCommands>b__140_0(DocumentPreviewControl x) => 
                new PageLayoutSettings(x.PageDisplayMode, x.ColumnsCount);

            internal PageLayoutSettings <UpdatePageLayoutCommands>b__140_1() => 
                new PageLayoutSettings();

            internal bool <UpdatePageLayoutCommands>b__140_10(DocumentPreviewControl x) => 
                x.ShowCoverPage;

            internal bool <UpdatePageLayoutCommands>b__140_11() => 
                false;

            internal bool <UpdatePageLayoutCommands>b__140_13(DocumentPreviewControl x) => 
                x.EnableContinuousScrolling;

            internal bool <UpdatePageLayoutCommands>b__140_14() => 
                true;

            internal bool <UpdatePageLayoutCommands>b__140_3(CommandToggleButton x) => 
                x is PageLayoutCommandButton;

            internal bool <UpdatePageLayoutCommands>b__140_4(CommandToggleButton x) => 
                x.IsChecked;

            internal Uri <UpdatePageLayoutCommands>b__140_5(CommandToggleButton x) => 
                x.LargeGlyph;

            internal Uri <UpdatePageLayoutCommands>b__140_7(CommandToggleButton x) => 
                x.SmallGlyph;
        }
    }
}

