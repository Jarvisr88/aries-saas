namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextParagraphInfoCache : UniqueItemsCache<DrawingTextParagraphInfo>
    {
        public const int DefaultItemIndex = 0;

        public DrawingTextParagraphInfoCache(IDocumentModelUnitConverter converter) : base(converter)
        {
        }

        protected override DrawingTextParagraphInfo CreateDefaultItem(IDocumentModelUnitConverter unitConverter) => 
            new DrawingTextParagraphInfo();
    }
}

