namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextSpacingInfoCache : UniqueItemsCache<DrawingTextSpacingInfo>
    {
        public const int DefaultItemIndex = 0;

        public DrawingTextSpacingInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingTextSpacingInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingTextSpacingInfo();
    }
}

