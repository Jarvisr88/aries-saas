namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingBlipFillInfoCache : UniqueItemsCache<DrawingBlipFillInfo>
    {
        public DrawingBlipFillInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingBlipFillInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingBlipFillInfo();
    }
}

