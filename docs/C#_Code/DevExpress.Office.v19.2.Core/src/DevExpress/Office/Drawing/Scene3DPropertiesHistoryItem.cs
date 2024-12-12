namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public abstract class Scene3DPropertiesHistoryItem : IndexChangedHistoryItemCore<PropertyKey>
    {
        private readonly Scene3DProperties obj;

        protected Scene3DPropertiesHistoryItem(Scene3DProperties obj) : base(GetModelPart(obj))
        {
            this.obj = obj;
        }

        private static IDocumentModelPart GetModelPart(Scene3DProperties obj)
        {
            Guard.ArgumentNotNull(obj, "obj");
            return obj.DocumentModel.MainPart;
        }

        public override IIndexBasedObject<PropertyKey> GetObject() => 
            null;

        public override object GetTargetObject() => 
            this.obj;

        protected Scene3DProperties Object =>
            this.obj;
    }
}

