namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;

    public class Scene3DRotationInfoCache : UniqueItemsCache<Scene3DRotationInfo>
    {
        public const int DefaultItemIndex = 0;

        public Scene3DRotationInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override Scene3DRotationInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new Scene3DRotationInfo();
    }
}

