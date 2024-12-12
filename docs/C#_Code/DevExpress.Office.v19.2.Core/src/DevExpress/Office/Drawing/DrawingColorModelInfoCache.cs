namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingColorModelInfoCache : UniqueItemsCache<DrawingColorModelInfo>
    {
        public const int DefaultItemIndex = 0;

        public DrawingColorModelInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingColorModelInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingColorModelInfo();
    }
}

