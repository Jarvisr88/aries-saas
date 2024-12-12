namespace DevExpress.Data.XtraReports.Labels
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Labels.LabelDetails class from the DevExpress.XtraReports assembly instead.")]
    public class LabelDetails
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PaperKindId { get; set; }

        public string Name { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float HPitch { get; set; }

        public float VPitch { get; set; }

        public float TopMargin { get; set; }

        public float LeftMargin { get; set; }

        public float RightMargin { get; set; }

        public float BottomMargin { get; set; }

        public GraphicsUnit Unit { get; set; }
    }
}

