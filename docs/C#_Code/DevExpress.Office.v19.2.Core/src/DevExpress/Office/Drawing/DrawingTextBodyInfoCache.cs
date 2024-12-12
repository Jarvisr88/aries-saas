namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextBodyInfoCache : UniqueItemsCache<DrawingTextBodyInfo>
    {
        public const int DefaultItemIndex = 0;

        public DrawingTextBodyInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingTextBodyInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingTextBodyInfo();
    }
}

