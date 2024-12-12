namespace DevExpress.Office.Model
{
    using DevExpress.Office;
    using System;

    public class ColorModelInfoCache : UniqueItemsCache<ColorModelInfo>
    {
        public const int DefaultItemIndex = 0;
        public const int EmptyColorIndex = 0;

        public ColorModelInfoCache(DocumentModelUnitConverter unitConverter) : base(unitConverter, DXCollectionUniquenessProviderType.MaximizePerformance)
        {
        }

        protected override ColorModelInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new ColorModelInfo();
    }
}

