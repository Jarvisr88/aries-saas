namespace DevExpress.XtraReports
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface IReport : IDocumentSource, ILink, IComponent, IDisposable, IServiceProvider, IExtensionsProvider
    {
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        event EventHandler<ParametersRequestEventArgs> ParametersRequestSubmit;

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void ApplyExportOptions(ExportOptions destinationOptions);
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void ApplyPageSettings(XtraPageSettingsBase destinationSettings);
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void CollectParameters(IList<Parameter> list, Predicate<Parameter> condition);
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void InitializeDocumentCreation();
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void RaiseParametersRequestBeforeShow(IList<ParameterInfo> parametersInfo);
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void RaiseParametersRequestSubmit(IList<ParameterInfo> parametersInfo, bool createDocument);
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void RaiseParametersRequestValueChanged(IList<ParameterInfo> parametersInfo, ParameterInfo changedParameterInfo);
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void ReleasePrintingSystem();
        void StopPageBuilding();
        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        void UpdatePageSettings(XtraPageSettingsBase sourceSettings, string paperName);

        DevExpress.XtraPrinting.Drawing.Watermark Watermark { get; }

        Func<PrintingSystemBase, PrintingDocument> InstantiateDocument { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        EventHandlerList Events { get; }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        bool IsDisposed { get; }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        bool ShowPreviewMarginLines { get; }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        IReportPrintTool PrintTool { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        bool RequestParameters { get; }
    }
}

