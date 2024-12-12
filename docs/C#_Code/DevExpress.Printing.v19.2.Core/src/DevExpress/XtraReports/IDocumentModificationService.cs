namespace DevExpress.XtraReports
{
    using System;

    internal interface IDocumentModificationService
    {
        void ModifyDocument(Action<IDocumentModifier> callback);
    }
}

