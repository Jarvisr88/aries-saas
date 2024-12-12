namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class ShapeStyleInfoCache : UniqueItemsCache<ShapeStyleInfo>
    {
        public ShapeStyleInfoCache(IDocumentModelUnitConverter unitConverter) : base(unitConverter)
        {
        }

        protected override ShapeStyleInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new ShapeStyleInfo();
    }
}

