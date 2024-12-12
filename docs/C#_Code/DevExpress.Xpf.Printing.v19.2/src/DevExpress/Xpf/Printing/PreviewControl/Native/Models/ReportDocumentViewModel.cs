namespace DevExpress.Xpf.Printing.PreviewControl.Native.Models
{
    using DevExpress.Data.Browsing;
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.Mvvm.Native;
    using DevExpress.ReportServer.Printing;
    using DevExpress.Xpf.Printing.Exports;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters.Models.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.DrillDown;
    using DevExpress.XtraPrinting.Native.Interaction;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.Parameters.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Input;

    public class ReportDocumentViewModel : DocumentViewModel, IReportDocument, IDocumentViewModel
    {
        private bool initializedBeforeDocumentCreation;
        private IReportPrintTool printTool;

        internal event ReportParametersReceivedEventHandler ReportParametersReceived;

        internal ReportDocumentViewModel()
        {
        }

        public override void CreateDocument()
        {
            base.CreateDocument();
            this.initializedBeforeDocumentCreation = true;
        }

        public override void Export(ExportOptionsViewModel options)
        {
            base.IsExporting = true;
            if (this.Report is RemoteDocumentSource)
            {
                base.publishEngine.RemoteExport(options, delegate (string fileName) {
                    this.IsExporting = false;
                    if (options.OpenFileAfterExport && !string.IsNullOrEmpty(fileName))
                    {
                        ProcessLaunchHelper.StartProcess(fileName, false);
                    }
                });
            }
            else
            {
                base.Export(options);
            }
        }

        internal void HandleBrickEvent(string eventName, Page page, Brick brick)
        {
            object[] parameters = new object[,] { { "Brick", brick }, { "Page", page } };
            ChangeEventArgs e = PrintingSystemBase.CreateEventArgs(eventName, parameters);
            base.PrintingSystem.OnAfterChange(e);
        }

        private void HandleDrillDown(Brick brick)
        {
            bool flag;
            DrillDownKey drillDownKey = (DrillDownKey) brick.DrillDownKey;
            IDrillDownServiceBase service = this.Report.GetService(typeof(IDrillDownServiceBase)) as IDrillDownServiceBase;
            if ((service != null) && service.Keys.TryGetValue(drillDownKey, out flag))
            {
                service.Keys[drillDownKey] = !flag;
                this.RefreshDocument(service);
            }
        }

        private void HandleSorting(string sortData)
        {
            IInteractionService service = this.Report.GetService<IInteractionService>();
            if ((service != null) && !string.IsNullOrEmpty(sortData))
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    service.AppendSorting(sortData);
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    service.RemoveSorting(sortData);
                }
                else
                {
                    service.ApplySorting(sortData);
                }
                this.RefreshDocument(service);
            }
        }

        public override void Load(object source)
        {
            base.Load(source);
            base.RaiseDocumentCreatingChanged();
            if (!(this.Report is RemoteDocumentSource))
            {
                this.RequestParameters();
            }
        }

        private void OnGetRemoteParametersCompleted(object sender, GetRemoteParametersCompletedEventArgs e)
        {
            (base.PrintingSystem as RemotePrintingSystem).Do<RemotePrintingSystem>(delegate (RemotePrintingSystem x) {
                x.GetRemoteParametersCompleted -= new GetRemoteParametersCompletedEventHandler(this.OnGetRemoteParametersCompleted);
            });
            Func<ClientParameter, ParameterInfo> selector = <>c.<>9__26_1;
            if (<>c.<>9__26_1 == null)
            {
                Func<ClientParameter, ParameterInfo> local1 = <>c.<>9__26_1;
                selector = <>c.<>9__26_1 = x => ParameterInfoFactory.CreateWithoutEditor(x.OriginalParameter);
            }
            List<ParameterInfo> parametersInfo = e.Parameters.Select<ClientParameter, ParameterInfo>(selector).ToList<ParameterInfo>();
            this.Report.RaiseParametersRequestBeforeShow(parametersInfo);
            List<ParameterModel> models = ModelsCreator.CreateParameterModels((IEnumerable<IClientParameter>) e.Parameters);
            this.ReportParametersReceived.Do<ReportParametersReceivedEventHandler>(x => x(this, new ReportParametersReceivedEventArgs(models, e.LookUpValuesProvider)));
        }

        private void OnPrintingSystemChanged(object sender, ChangeEventArgs e)
        {
            if (e.EventName == "BrickClick")
            {
                VisualBrick brick = e.ValueOf("Brick") as VisualBrick;
                if ((brick != null) && !DrillDownKey.IsNullOrEmpty(brick.DrillDownKey))
                {
                    this.HandleDrillDown(brick);
                }
                else if (brick != null)
                {
                    this.HandleSorting(brick.SortData);
                }
            }
        }

        private void OnRefreshDocumentCompleted(object sender, EventArgs e)
        {
            base.ProgressReflector.PositionChanged -= new EventHandler(this.OnProgressChanged);
            this.Report.PrintingSystemBase.AfterBuildPages -= new EventHandler(this.OnRefreshDocumentCompleted);
            base.bookmarks = null;
            PrintingSystemBase printingSystem = base.PrintingSystem;
            (this.Report.PrintingSystemBase.GetService<IUpdateDrillDownReportStrategy>() ?? new UpdateDrillDownReportStrategy()).Update(this.Report, printingSystem);
            this.ZeroInteracting();
            this.Report.PrintTool = this.printTool;
            base.PrintingSystem = this.Report.PrintingSystemBase;
            printingSystem.Dispose();
            base.ForceProgressVisibility(false);
            base.Initialize();
            base.RaiseDocumentChanged();
            base.RaiseDocumentCreated();
        }

        protected override void PreparePrintingSystem(PrintingSystemBase ps)
        {
            base.PreparePrintingSystem(ps);
            if (!(this.Report is RemoteDocumentSource))
            {
                this.Report.PrintTool = new ReportPrintToolInternal(this.Report);
            }
            if (!(ps is RemotePrintingSystem))
            {
                ps.ReplaceService<BackgroundPageBuildEngineStrategy>(new DispatcherPageBuildStrategy());
            }
        }

        public override void Print(PrintOptionsViewModel model)
        {
            if (this.Report is RemoteDocumentSource)
            {
                base.publishEngine.Print(model);
            }
            else
            {
                base.Print(model);
            }
        }

        public override void PrintDirect(string printerName = null)
        {
            if (this.Report is RemoteDocumentSource)
            {
                base.publishEngine.PrintDirect(printerName);
            }
            else
            {
                base.PrintDirect(printerName);
            }
        }

        private void RefreshDocument(IInteractionServiceBase service)
        {
            this.Unsubscribe();
            this.Report.ReleasePrintingSystem();
            base.PaintService = null;
            try
            {
                ((ISupportInitialize) this.Report.PrintingSystemBase).BeginInit();
                this.Report.PrintingSystemBase.PageSettings.Assign(base.PrintingSystem.PageSettings.Data);
            }
            finally
            {
                ((ISupportInitialize) this.Report.PrintingSystemBase).EndInit();
            }
            this.Report.PrintingSystemBase.PageSettings.IsPreset = true;
            this.PreparePrintingSystem(this.Report.PrintingSystemBase);
            this.Report.PrintingSystemBase.Extender = new DocumentPreviewPrintingSystemExtender(this.Report.PrintingSystemBase, base.ProgressReflector);
            base.ProgressReflector.PositionChanged += new EventHandler(this.OnProgressChanged);
            this.Report.PrintingSystemBase.AfterBuildPages += new EventHandler(this.OnRefreshDocumentCompleted);
            service.IsInteracting = true;
            this.printTool = this.Report.PrintTool;
            this.Report.PrintTool = null;
            base.ForceProgressVisibility(true);
            this.Report.CreateDocument(true);
        }

        private void RequestParameters()
        {
            try
            {
                if (!base.IsCreating && (!base.IsCreated && !this.initializedBeforeDocumentCreation))
                {
                    this.Report.InitializeDocumentCreation();
                }
                this.initializedBeforeDocumentCreation = true;
                List<Parameter> list = new List<Parameter>();
                Predicate<Parameter> condition = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Predicate<Parameter> local1 = <>c.<>9__9_0;
                    condition = <>c.<>9__9_0 = parameter => true;
                }
                this.Report.CollectParameters(list, condition);
                Converter<Parameter, ParameterInfo> converter = <>c.<>9__9_1;
                if (<>c.<>9__9_1 == null)
                {
                    Converter<Parameter, ParameterInfo> local2 = <>c.<>9__9_1;
                    converter = <>c.<>9__9_1 = x => ParameterInfoFactory.CreateWithoutEditor(x);
                }
                this.Report.RaiseParametersRequestBeforeShow(list.ConvertAll<ParameterInfo>(converter));
                DataContext service = this.Report.GetService<DataContext>();
                LookUpValuesProvider lookUpsProvider = new LookUpValuesProvider(list, service);
                List<ParameterModel> models = ModelsCreator.CreateParameterModels(list, service);
                this.ReportParametersReceived.Do<ReportParametersReceivedEventHandler>(x => x(this, new ReportParametersReceivedEventArgs(models, lookUpsProvider)));
                DocumentStatus status = this.Report.RequestParameters ? DocumentStatus.WaitingForParameters : DocumentStatus.DocumentCreation;
                this.DocumentStatus = list.Any<Parameter>() ? status : base.DocumentStatus;
            }
            catch (Exception exception)
            {
                base.RaiseDocumentException(exception);
            }
        }

        public override void Save(string filePath)
        {
            if (!(this.Report is RemoteDocumentSource))
            {
                base.Save(filePath);
            }
            else
            {
                Action<string> afterExport = <>c.<>9__21_0;
                if (<>c.<>9__21_0 == null)
                {
                    Action<string> local1 = <>c.<>9__21_0;
                    afterExport = <>c.<>9__21_0 = delegate (string fileName) {
                    };
                }
                base.publishEngine.RemoteExport(base.PrintingSystem.ExportOptions.NativeFormat, filePath, afterExport);
            }
        }

        public override void Send(SendOptionsViewModel options)
        {
            base.IsExporting = true;
            if (this.Report is RemoteDocumentSource)
            {
                base.publishEngine.RemoteExport(options, delegate (string fileName) {
                    this.IsExporting = false;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string[] files = new string[] { fileName };
                        new EmailSender().Send(files, options.EmailOptions);
                    }
                });
            }
            else
            {
                base.Send(options);
            }
        }

        public override void SetWatermark(Watermark watermark)
        {
            try
            {
                base.SetWatermark(watermark);
            }
            catch (Exception exception)
            {
                base.RaiseDocumentException(exception);
            }
        }

        public void Submit(IList<Parameter> parameters)
        {
            try
            {
                this.Report.RaiseParametersRequestSubmit(parameters.Select<Parameter, ParameterInfo>(new Func<Parameter, ParameterInfo>(ParameterInfoFactory.CreateWithoutEditor)).ToList<ParameterInfo>(), true);
            }
            catch (Exception exception)
            {
                base.RaiseDocumentException(exception);
            }
        }

        protected internal override void Subscribe()
        {
            base.Subscribe();
            base.PrintingSystem.Do<PrintingSystemBase>(delegate (PrintingSystemBase ps) {
                (ps as RemotePrintingSystem).Do<RemotePrintingSystem>(delegate (RemotePrintingSystem x) {
                    x.GetRemoteParametersCompleted += new GetRemoteParametersCompletedEventHandler(this.OnGetRemoteParametersCompleted);
                });
                ps.AfterChange += new ChangeEventHandler(this.OnPrintingSystemChanged);
            });
        }

        protected internal override void Unsubscribe()
        {
            base.Unsubscribe();
            base.PrintingSystem.Do<PrintingSystemBase>(delegate (PrintingSystemBase ps) {
                (ps as RemotePrintingSystem).Do<RemotePrintingSystem>(delegate (RemotePrintingSystem x) {
                    x.GetRemoteParametersCompleted -= new GetRemoteParametersCompletedEventHandler(this.OnGetRemoteParametersCompleted);
                });
                ps.AfterChange -= new ChangeEventHandler(this.OnPrintingSystemChanged);
            });
        }

        private void ZeroInteracting()
        {
            IInteractionService service = this.Report.GetService<IInteractionService>();
            if (service != null)
            {
                service.IsInteracting = false;
            }
            IDrillDownServiceBase base2 = this.Report.GetService<IDrillDownServiceBase>();
            if (base2 != null)
            {
                base2.IsInteracting = false;
            }
        }

        internal IReport Report =>
            (IReport) base.Link;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReportDocumentViewModel.<>c <>9 = new ReportDocumentViewModel.<>c();
            public static Predicate<Parameter> <>9__9_0;
            public static Converter<Parameter, ParameterInfo> <>9__9_1;
            public static Action<string> <>9__21_0;
            public static Func<ClientParameter, ParameterInfo> <>9__26_1;

            internal ParameterInfo <OnGetRemoteParametersCompleted>b__26_1(ClientParameter x) => 
                ParameterInfoFactory.CreateWithoutEditor(x.OriginalParameter);

            internal bool <RequestParameters>b__9_0(Parameter parameter) => 
                true;

            internal ParameterInfo <RequestParameters>b__9_1(Parameter x) => 
                ParameterInfoFactory.CreateWithoutEditor(x);

            internal void <Save>b__21_0(string fileName)
            {
            }
        }
    }
}

