namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class ServiceDocumentBand : DocumentBand
    {
        public ServiceDocumentBand(DocumentBandKind kind);

        public override bool ShouldAssignParent { get; }
    }
}

