namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextCharacterInfoCache : UniqueItemsCache<DrawingTextCharacterInfo>
    {
        public const int DefaultItemIndex = 0;

        public DrawingTextCharacterInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingTextCharacterInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingTextCharacterInfo();
    }
}

