namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing.PreviewControl;
    using System;
    using System.Collections.Generic;

    public interface IReportDocument : IDocumentViewModel
    {
        void Submit(IList<Parameter> parameters);
    }
}

