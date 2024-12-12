namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingGradientFillInfoCache : UniqueItemsCache<DrawingGradientFillInfo>
    {
        public DrawingGradientFillInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingGradientFillInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingGradientFillInfo();
    }
}

