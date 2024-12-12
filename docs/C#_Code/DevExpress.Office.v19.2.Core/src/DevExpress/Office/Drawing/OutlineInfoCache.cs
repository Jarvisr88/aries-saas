namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class OutlineInfoCache : UniqueItemsCache<OutlineInfo>
    {
        public const int DefaultItemIndex = 0;

        public OutlineInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override OutlineInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new OutlineInfo();
    }
}

