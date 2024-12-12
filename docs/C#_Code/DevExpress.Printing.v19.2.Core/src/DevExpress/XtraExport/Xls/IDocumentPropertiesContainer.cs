namespace DevExpress.XtraExport.Xls
{
    using System;

    public interface IDocumentPropertiesContainer
    {
        void SetApplication(string value);
        void SetAuthor(string value);
        void SetBoolean(string propName, bool propValue);
        void SetCategory(string value);
        void SetCompany(string value);
        void SetContentStatus(string value);
        void SetCreated(DateTime value);
        void SetDateTime(string propName, DateTime propValue);
        void SetDescription(string value);
        void SetDocumentRevision(string value);
        void SetDocumentVersion(string value);
        void SetKeywords(string value);
        void SetLastModifiedBy(string value);
        void SetLastPrinted(DateTime value);
        void SetLinkToContent(string propName, string linkTarget);
        void SetManager(string value);
        void SetModified(DateTime value);
        void SetNumeric(string propName, double propValue);
        void SetSecurity(int value);
        void SetSubject(string value);
        void SetText(string propName, string propValue);
        void SetTitle(string value);
        void SetVersion(string value);
    }
}

