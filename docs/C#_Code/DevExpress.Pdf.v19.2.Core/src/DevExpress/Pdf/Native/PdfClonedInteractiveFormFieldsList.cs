namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfClonedInteractiveFormFieldsList : PdfInteractiveFormFieldsList
    {
        private readonly List<PdfIndirectObjectId> fieldIds;
        private List<PdfIndirectObjectId> newFieldIds;

        private PdfClonedInteractiveFormFieldsList(List<PdfIndirectObjectId> fieldIds, int objectNumber) : base(objectNumber)
        {
            this.fieldIds = fieldIds;
        }

        public PdfClonedInteractiveFormFieldsList(List<PdfInteractiveFormField> fields, PdfObjectCollection objectCollection) : base(objectCollection.GetNextObjectNumber())
        {
            this.fieldIds = new List<PdfIndirectObjectId>();
            foreach (PdfInteractiveFormField field in fields)
            {
                this.fieldIds.Add(objectCollection.GetObjectId(field.ObjectNumber));
            }
        }

        protected internal override PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isCloning) => 
            new PdfClonedInteractiveFormFieldsList(this.newFieldIds, objects.GetNextObjectNumber());

        public override IEnumerable<PdfInteractiveFormField> GetFormFields(PdfObjectCollection objects)
        {
            List<PdfInteractiveFormField> list = new List<PdfInteractiveFormField>();
            foreach (PdfIndirectObjectId id in this.fieldIds)
            {
                PdfObjectReference savedObjectReference = objects.GetSavedObjectReference(id);
                if (savedObjectReference != null)
                {
                    list.Add(objects.GetResolvedInteractiveFormField(savedObjectReference));
                }
            }
            return ((list.Count != 0) ? list : null);
        }

        protected internal override bool IsDeferredObject(bool isCloning) => 
            isCloning;

        protected internal override void NotifyMergeCompleted(PdfObjectCollection objects)
        {
            base.NotifyMergeCompleted(objects);
            this.newFieldIds = new List<PdfIndirectObjectId>();
            foreach (PdfIndirectObjectId id in this.fieldIds)
            {
                PdfObjectReference savedObjectReference = objects.GetSavedObjectReference(id);
                if (savedObjectReference != null)
                {
                    this.newFieldIds.Add(objects.GetObjectId(savedObjectReference.Number));
                }
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            List<PdfObjectReference> enumerable = new List<PdfObjectReference>();
            foreach (PdfIndirectObjectId id in this.fieldIds)
            {
                PdfObjectReference savedObjectReference = objects.GetSavedObjectReference(id);
                if (savedObjectReference != null)
                {
                    enumerable.Add(savedObjectReference);
                }
            }
            return new PdfWritableArray(enumerable);
        }

        protected internal override bool SupportMergeCompletedNotifications =>
            true;
    }
}

