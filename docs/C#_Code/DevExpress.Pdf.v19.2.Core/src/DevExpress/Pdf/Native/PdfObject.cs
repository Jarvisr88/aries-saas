namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfObject : PdfDocumentItem
    {
        internal const int DirectObjectNumber = -1;

        protected PdfObject() : this(-1)
        {
        }

        protected PdfObject(int number) : base(number, 0)
        {
        }

        protected internal virtual PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isCloning) => 
            null;

        protected internal virtual bool IsDeferredObject(bool isCloning) => 
            false;

        protected internal virtual void NotifyMergeCompleted(PdfObjectCollection objects)
        {
        }

        protected internal abstract object ToWritableObject(PdfObjectCollection objects);
        protected internal virtual void UpdateObject(PdfObject value)
        {
        }

        protected internal virtual bool SupportMergeCompletedNotifications =>
            false;
    }
}

