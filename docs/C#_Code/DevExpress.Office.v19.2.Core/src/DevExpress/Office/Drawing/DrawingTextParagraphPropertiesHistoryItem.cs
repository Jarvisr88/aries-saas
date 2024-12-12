namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public abstract class DrawingTextParagraphPropertiesHistoryItem : IndexChangedHistoryItemCore<DocumentModelChangeActions>
    {
        private readonly DrawingTextParagraphProperties obj;

        protected DrawingTextParagraphPropertiesHistoryItem(DrawingTextParagraphProperties obj) : base(GetModelPart(obj))
        {
            this.obj = obj;
        }

        private static IDocumentModelPart GetModelPart(DrawingTextParagraphProperties obj)
        {
            Guard.ArgumentNotNull(obj, "obj");
            return obj.DocumentModel.MainPart;
        }

        public override IIndexBasedObject<DocumentModelChangeActions> GetObject() => 
            null;

        public override object GetTargetObject() => 
            this.obj;

        protected DrawingTextParagraphProperties Object =>
            this.obj;
    }
}

