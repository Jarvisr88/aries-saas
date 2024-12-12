namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDocumentOwner
    {
        void Close(IDocumentContent documentContent, bool force = true);
    }
}

