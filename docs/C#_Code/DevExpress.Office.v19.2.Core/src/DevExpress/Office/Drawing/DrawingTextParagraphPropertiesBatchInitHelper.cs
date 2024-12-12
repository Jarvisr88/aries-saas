namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextParagraphPropertiesBatchInitHelper : DrawingTextParagraphPropertiesBatchUpdateHelper
    {
        public DrawingTextParagraphPropertiesBatchInitHelper(IBatchInitHandler handler) : base(new BatchInitAdapter(handler))
        {
        }

        public IBatchInitHandler BatchInitHandler =>
            ((BatchInitAdapter) base.BatchUpdateHandler).BatchInitHandler;
    }
}

