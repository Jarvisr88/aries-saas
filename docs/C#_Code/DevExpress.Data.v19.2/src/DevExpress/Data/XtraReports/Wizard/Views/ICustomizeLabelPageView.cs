namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.ICustomizeLabelPageView class from the DevExpress.XtraReports assembly instead.")]
    public interface ICustomizeLabelPageView
    {
        event EventHandler LabelInformationChanged;

        event EventHandler SelectedPaperKindChanged;

        event EventHandler UnitChanged;

        void FillPageSizeList(IEnumerable<PaperKindViewInfo> paperKinds);

        float Width { get; set; }

        float Height { get; set; }

        float VerticalPitch { get; set; }

        float HorizontalPitch { get; set; }

        float TopMargin { get; set; }

        float LeftMargin { get; set; }

        float RightMargin { get; set; }

        float BottomMargin { get; set; }

        int LabelsCountHorz { set; }

        int LabelsCountVert { set; }

        GraphicsUnit Unit { get; set; }

        string PaperKindFormattedText { set; }

        int SelectedPaperKindId { get; set; }
    }
}

