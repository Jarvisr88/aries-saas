namespace DevExpress.Data.XtraReports.Labels
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Labels.PaperKindData class from the DevExpress.XtraReports assembly instead.")]
    public class PaperKindData
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Validate();

        public int Id { get; set; }

        public int EnumId { get; set; }

        public string Name { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public GraphicsUnit Unit { get; set; }

        public bool IsRollPaper { get; set; }
    }
}

