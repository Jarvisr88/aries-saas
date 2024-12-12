namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;

    public class Scene3DPropertiesBatchInitHelper : MultiIndexBatchUpdateHelper
    {
        public Scene3DPropertiesBatchInitHelper(IBatchInitHandler handler) : base(new BatchInitAdapter(handler))
        {
        }

        public IBatchInitHandler BatchInitHandler =>
            ((BatchInitAdapter) base.BatchUpdateHandler).BatchInitHandler;
    }
}

