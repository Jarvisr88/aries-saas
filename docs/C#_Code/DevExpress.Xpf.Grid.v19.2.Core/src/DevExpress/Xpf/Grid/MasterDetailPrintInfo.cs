namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MasterDetailPrintInfo
    {
        public MasterDetailPrintInfo(DefaultBoolean allowPrintDetails, DefaultBoolean allowPrintEmptyDetails, DefaultBoolean printAllDetails, int detailLevel, ISupportMasterDetailPrinting rootPrintingDataTreeBuilder, DevExpress.Xpf.Grid.PrintDetailType printDetailType = 1, int detailGroupLevel = 0)
        {
            this.AllowPrintDetails = allowPrintDetails;
            this.AllowPrintEmptyDetails = allowPrintEmptyDetails;
            this.PrintAllDetails = printAllDetails;
            this.PrintDetailType = printDetailType;
            this.DetailGroupLevel = detailGroupLevel;
            this.RootPrintingDataTreeBuilder = rootPrintingDataTreeBuilder;
            this.DetailLevel = detailLevel;
        }

        public DefaultBoolean AllowPrintDetails { get; private set; }

        public DefaultBoolean AllowPrintEmptyDetails { get; private set; }

        public DefaultBoolean PrintAllDetails { get; private set; }

        public DevExpress.Xpf.Grid.PrintDetailType PrintDetailType { get; private set; }

        public int DetailGroupLevel { get; private set; }

        public ISupportMasterDetailPrinting RootPrintingDataTreeBuilder { get; private set; }

        public int DetailLevel { get; private set; }
    }
}

