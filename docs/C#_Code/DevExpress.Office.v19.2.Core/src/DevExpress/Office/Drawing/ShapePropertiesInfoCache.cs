namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class ShapePropertiesInfoCache : UniqueItemsCache<ShapePropertiesInfo>
    {
        public ShapePropertiesInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override ShapePropertiesInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new ShapePropertiesInfo { ShapeType = ShapePreset.Rect };
    }
}

