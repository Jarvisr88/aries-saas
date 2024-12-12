namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraReports.UI;
    using System;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    [StructLayout(LayoutKind.Sequential), DataContract]
    public struct LineSpecifics
    {
        [DataMember]
        public DevExpress.XtraReports.UI.LineDirection LineDirection { get; set; }
        [DataMember]
        public DashStyle LineStyle { get; set; }
        [DataMember]
        public int LineWidth { get; set; }
        [DataMember]
        public int ColorARGB { get; set; }
    }
}

