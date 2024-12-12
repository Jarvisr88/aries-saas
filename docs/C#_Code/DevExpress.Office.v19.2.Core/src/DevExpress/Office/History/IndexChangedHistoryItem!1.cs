namespace DevExpress.Office.History
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class IndexChangedHistoryItem<TActions> : IndexChangedHistoryItemCore<TActions> where TActions: struct
    {
        public const int TypeCode = 2;
        private readonly IIndexBasedObject<TActions> obj;

        public IndexChangedHistoryItem(IDocumentModelPart documentModelPart, IIndexBasedObject<TActions> obj) : base(documentModelPart)
        {
            Guard.ArgumentNotNull(obj, "obj");
            this.obj = obj;
        }

        public override IIndexBasedObject<TActions> GetObject() => 
            this.obj;

        public override object GetTargetObject() => 
            this.obj;

        public IIndexBasedObject<TActions> Object =>
            this.obj;
    }
}

