namespace DevExpress.XtraReports.UI
{
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IReportPrintTool : IDisposable
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool ApproveParameters(Parameter[] parameters, bool requestParameters);
        void ClosePreview();
        void CloseRibbonPreview();
        void Print();
        void Print(string printerName);
        bool? PrintDialog();
        bool? ShowPageSetupDialog(object themeOwner);
        void ShowPreview();
        void ShowPreview(object themeOwner);
        void ShowPreviewDialog();
        void ShowPreviewDialog(object themeOwner);
        void ShowRibbonPreview();
        void ShowRibbonPreview(object themeOwner);
        void ShowRibbonPreviewDialog();
        void ShowRibbonPreviewDialog(object themeOwner);

        List<ParameterInfo> ParametersInfo { get; }
    }
}

