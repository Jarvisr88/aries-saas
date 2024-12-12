namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;

    public class Scene3DPropertiesInfoCache : UniqueItemsCache<Scene3DPropertiesInfo>
    {
        public const int DefaultItemIndex = 0;

        public Scene3DPropertiesInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override Scene3DPropertiesInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new Scene3DPropertiesInfo();
    }
}

