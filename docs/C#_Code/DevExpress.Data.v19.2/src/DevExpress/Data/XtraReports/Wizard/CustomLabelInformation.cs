namespace DevExpress.Data.XtraReports.Wizard
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.CustomLabelInformation class from the DevExpress.XtraReports assembly instead.")]
    public class CustomLabelInformation : ICloneable
    {
        public object Clone();
        public override bool Equals(object obj);
        public override int GetHashCode();

        public float Width { get; set; }

        public float Height { get; set; }

        public float VerticalPitch { get; set; }

        public float HorizontalPitch { get; set; }

        public float TopMargin { get; set; }

        public float LeftMargin { get; set; }

        public float RightMargin { get; set; }

        public float BottomMargin { get; set; }

        public int PaperKindDataId { get; set; }

        public GraphicsUnit Unit { get; set; }
    }
}

