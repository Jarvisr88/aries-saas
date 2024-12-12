namespace DevExpress.Xpf.Printing
{
    using DevExpress.Data.Browsing;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.Xpf.Printing.Parameters.Models.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.XamlExport;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.Parameters.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Forms;

    [Obsolete("The DocumentPreview and corresponding xxxPreviewModel classes and interfaces are now obsolete. Use the DocumentPreviewControl class to display a print preview. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444")]
    public class XtraReportPreviewModel : LegacyLinkPreviewModel
    {
        private IReport report;
        private bool autoShowParametersPanel;
        private readonly DevExpress.Xpf.Printing.Parameters.Models.ParametersModel parametersModel;

        public XtraReportPreviewModel() : this(null)
        {
        }

        public XtraReportPreviewModel(IReport report) : base(report)
        {
            this.autoShowParametersPanel = true;
            this.report = report;
            this.parametersModel = DevExpress.Xpf.Printing.Parameters.Models.ParametersModel.CreateParametersModel();
            this.parametersModel.Submit += new EventHandler(this.OnSubmit);
            this.PrepareParametersModel();
            this.WatermarkService = new DevExpress.Xpf.Printing.Native.WatermarkService();
            base.OnSourceChanged();
        }

        internal XtraReportPreviewModel(IReport report, IPageSettingsConfiguratorService pageSettingsConfiguratorService, IPrintService printService, IExportSendService exportSendService, IHighlightingService highlightService, IScaleService scaleService, IWatermarkService watermarkService) : base(report, pageSettingsConfiguratorService, printService, exportSendService, highlightService, scaleService)
        {
            this.autoShowParametersPanel = true;
            this.report = report;
            this.WatermarkService = watermarkService;
            base.OnSourceChanged();
        }

        protected override bool CanSetWatermark(object parameter) => 
            base.BuildPagesComplete && (!base.IsExporting && !base.IsSaving);

        protected override bool CanShowParametersPanel(object parameter)
        {
            bool flag;
            if ((this.Report == null) || (CollectReportParameters(this.Report).Count == 0))
            {
                return false;
            }
            using (List<Parameter>.Enumerator enumerator = CollectReportParameters(this.Report).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Parameter current = enumerator.Current;
                        if (!current.Visible)
                        {
                            continue;
                        }
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

        private static List<Parameter> CollectReportParameters(IReport report)
        {
            List<Parameter> list = new List<Parameter>();
            Predicate<Parameter> condition = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Predicate<Parameter> local1 = <>c.<>9__32_0;
                condition = <>c.<>9__32_0 = parameter => true;
            }
            report.CollectParameters(list, condition);
            return list;
        }

        protected override void CreateDocument(bool buildPagesInBackground)
        {
            this.Report.CreateDocument(buildPagesInBackground);
        }

        protected override DataContext GetDataContext() => 
            this.Report.GetService(typeof(DataContext)) as DataContext;

        protected override void HookPrintingSystem()
        {
            base.HookPrintingSystem();
            this.Report.PrintTool = new ReportPrintToolWpf(this.Report);
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            this.SubmitParameters(CollectReportParameters(this.Report));
        }

        private void PrepareParametersModel()
        {
            List<ParameterModel> parameters = new List<ParameterModel>();
            if (this.Report == null)
            {
                this.parametersModel.AssignParameters(parameters);
            }
            else
            {
                List<Parameter> list2 = CollectReportParameters(this.Report);
                if (list2.Count == 0)
                {
                    this.parametersModel.AssignParameters(parameters);
                }
                else
                {
                    string str;
                    if (!CascadingParametersService.ValidateFilterStrings(list2, out str))
                    {
                        this.parametersModel.AssignParameters(parameters);
                        base.ShowError(str);
                    }
                    else
                    {
                        Converter<Parameter, DevExpress.XtraReports.Parameters.ParameterInfo> converter = <>c.<>9__24_0;
                        if (<>c.<>9__24_0 == null)
                        {
                            Converter<Parameter, DevExpress.XtraReports.Parameters.ParameterInfo> local1 = <>c.<>9__24_0;
                            converter = <>c.<>9__24_0 = x => ParameterInfoFactory.CreateWithoutEditor(x);
                        }
                        this.Report.RaiseParametersRequestBeforeShow(list2.ConvertAll<DevExpress.XtraReports.Parameters.ParameterInfo>(converter));
                        DataContext service = this.Report.GetService<DataContext>();
                        parameters.AddRange(ModelsCreator.CreateParameterModels(list2, service));
                        LookUpValuesProvider provider = new LookUpValuesProvider(list2, service);
                        this.parametersModel.AssignParameters(parameters);
                        this.parametersModel.LookUpValuesProvider = provider;
                        if (this.AutoShowParametersPanel && this.parametersModel.HasVisibleParameters)
                        {
                            this.IsParametersPanelVisible = true;
                            base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(XtraReportPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_IsParametersPanelVisible)), new ParameterExpression[0]));
                        }
                    }
                }
            }
        }

        protected override void SetWatermark(object parameter)
        {
            this.WatermarkService.EditCompleted += new EventHandler<WatermarkServiceEventArgs>(this.watermarkService_EditCompleted);
            this.WatermarkService.Edit(this.DialogService.GetParentWindow(), this.PrintingSystem.Pages[base.CurrentPageIndex], this.PrintingSystem.PageCount, this.PrintingSystem.Watermark);
        }

        private void SubmitParameters(List<Parameter> parameters)
        {
            try
            {
                Converter<Parameter, DevExpress.XtraReports.Parameters.ParameterInfo> converter = <>c.<>9__33_0;
                if (<>c.<>9__33_0 == null)
                {
                    Converter<Parameter, DevExpress.XtraReports.Parameters.ParameterInfo> local1 = <>c.<>9__33_0;
                    converter = <>c.<>9__33_0 = delegate (Parameter parameter) {
                        Function<Control, Parameter> createEditor = <>c.<>9__33_1;
                        if (<>c.<>9__33_1 == null)
                        {
                            Function<Control, Parameter> local1 = <>c.<>9__33_1;
                            createEditor = <>c.<>9__33_1 = (Function<Control, Parameter>) (type => null);
                        }
                        return new DevExpress.XtraReports.Parameters.ParameterInfo(parameter, createEditor);
                    };
                }
                this.Report.RaiseParametersRequestSubmit(parameters.ConvertAll<DevExpress.XtraReports.Parameters.ParameterInfo>(converter), true);
            }
            catch (Exception exception)
            {
                if ((this.Report != null) && this.Report.PrintingSystemBase.RaiseCreateDocumentException(exception))
                {
                    throw;
                }
            }
        }

        protected override void UnhookPrintingSystem()
        {
            base.UnhookPrintingSystem();
            this.PrintingSystem.RemoveService(typeof(XpsExportServiceBase));
        }

        protected override FrameworkElement VisualizePage(int pageIndex)
        {
            BrickPageVisualizer visualizer = new BrickPageVisualizer(TextMeasurementSystem.GdiPlus);
            PSPage drawingPage = (PSPage) this.PrintingSystem.Pages[pageIndex];
            drawingPage.PerformLayout(new PrintingSystemContextWrapper(this.PrintingSystem, drawingPage));
            return visualizer.Visualize(drawingPage, pageIndex, this.PrintingSystem.Pages.Count);
        }

        private void watermarkService_EditCompleted(object sender, WatermarkServiceEventArgs e)
        {
            this.WatermarkService.EditCompleted -= new EventHandler<WatermarkServiceEventArgs>(this.watermarkService_EditCompleted);
            bool? isWatermarkAssigned = e.IsWatermarkAssigned;
            bool flag = true;
            if ((isWatermarkAssigned.GetValueOrDefault() == flag) ? (isWatermarkAssigned != null) : false)
            {
                this.PrintingSystem.Watermark.CopyFrom(e.Watermark);
                this.report.Watermark.CopyFrom(e.Watermark);
                base.ClearCache();
                base.UpdateCurrentPageContent();
            }
        }

        [Description("Specifies the report assigned to the model.")]
        public IReport Report
        {
            get => 
                this.report;
            set
            {
                if (!ReferenceEquals(this.report, value))
                {
                    base.OnSourceChanging();
                    this.report = value;
                    this.PrepareParametersModel();
                    base.OnSourceChanged();
                    base.UpdateCurrentPageContent();
                }
            }
        }

        [Description("Specifies whether the Parameters panel is visible in the Print Preview.")]
        public bool AutoShowParametersPanel
        {
            get => 
                this.autoShowParametersPanel;
            set => 
                this.autoShowParametersPanel = value;
        }

        public override bool IsEmptyDocument =>
            !this.IsCreating && ((this.PageCount == 0) && !this.parametersModel.IsSubmitted);

        protected internal override PrintingSystemBase PrintingSystem =>
            this.report?.PrintingSystemBase;

        internal IWatermarkService WatermarkService { get; set; }

        public override bool IsSetWatermarkVisible =>
            true;

        public override DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel =>
            this.parametersModel;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly XtraReportPreviewModel.<>c <>9 = new XtraReportPreviewModel.<>c();
            public static Converter<Parameter, DevExpress.XtraReports.Parameters.ParameterInfo> <>9__24_0;
            public static Predicate<Parameter> <>9__32_0;
            public static Function<Control, Parameter> <>9__33_1;
            public static Converter<Parameter, DevExpress.XtraReports.Parameters.ParameterInfo> <>9__33_0;

            internal bool <CollectReportParameters>b__32_0(Parameter parameter) => 
                true;

            internal DevExpress.XtraReports.Parameters.ParameterInfo <PrepareParametersModel>b__24_0(Parameter x) => 
                ParameterInfoFactory.CreateWithoutEditor(x);

            internal DevExpress.XtraReports.Parameters.ParameterInfo <SubmitParameters>b__33_0(Parameter parameter)
            {
                Function<Control, Parameter> createEditor = <>9__33_1;
                if (<>9__33_1 == null)
                {
                    Function<Control, Parameter> local1 = <>9__33_1;
                    createEditor = <>9__33_1 = (Function<Control, Parameter>) (type => null);
                }
                return new DevExpress.XtraReports.Parameters.ParameterInfo(parameter, createEditor);
            }

            internal Control <SubmitParameters>b__33_1(Parameter type) => 
                null;
        }
    }
}

