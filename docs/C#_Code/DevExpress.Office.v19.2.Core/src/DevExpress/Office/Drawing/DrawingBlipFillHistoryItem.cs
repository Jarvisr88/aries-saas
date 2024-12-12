namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public abstract class DrawingBlipFillHistoryItem : IndexChangedHistoryItemCore<PropertyKey>
    {
        private readonly DrawingBlipFill obj;

        protected DrawingBlipFillHistoryItem(DrawingBlipFill obj) : base(GetModelPart(obj))
        {
            this.obj = obj;
        }

        private static IDocumentModelPart GetModelPart(DrawingBlipFill obj)
        {
            Guard.ArgumentNotNull(obj, "obj");
            return obj.DocumentModel.MainPart;
        }

        public override IIndexBasedObject<PropertyKey> GetObject() => 
            null;

        public override object GetTargetObject() => 
            this.obj;

        protected DrawingBlipFill Object =>
            this.obj;
    }
}

