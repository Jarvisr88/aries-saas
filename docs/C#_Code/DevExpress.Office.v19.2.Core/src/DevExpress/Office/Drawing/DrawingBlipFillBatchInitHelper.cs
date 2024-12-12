namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingBlipFillBatchInitHelper : DrawingBlipFillBatchUpdateHelper
    {
        public DrawingBlipFillBatchInitHelper(IBatchInitHandler handler) : base(new BatchInitAdapter(handler))
        {
        }

        public IBatchInitHandler BatchInitHandler =>
            ((BatchInitAdapter) base.BatchUpdateHandler).BatchInitHandler;
    }
}

