namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingBlipTileInfoCache : UniqueItemsCache<DrawingBlipTileInfo>
    {
        public DrawingBlipTileInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingBlipTileInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingBlipTileInfo();
    }
}

