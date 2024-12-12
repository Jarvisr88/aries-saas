namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class CommonDrawingLocksInfoCache : UniqueItemsCache<CommonDrawingLocksInfo>
    {
        public CommonDrawingLocksInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override CommonDrawingLocksInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new CommonDrawingLocksInfo();
    }
}

