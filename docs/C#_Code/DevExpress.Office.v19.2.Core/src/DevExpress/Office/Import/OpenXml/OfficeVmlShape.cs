namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Runtime.CompilerServices;

    public class OfficeVmlShape : VmlShapeType
    {
        private const VmlBlackAndWhiteMode DefaultBlackAndWhiteMode = VmlBlackAndWhiteMode.Auto;

        public OfficeVmlShape(IDocumentModel documentModel) : base(documentModel)
        {
            this.ShapeStyleProperties = new VmlShapeStyleProperties();
        }

        public string SpId { get; set; }

        public VmlShapeStyleProperties ShapeStyleProperties { get; private set; }

        public VmlInsetMode InsetMode { get; set; }

        public string AltText { get; set; }

        public VmlShapeImageData ImageData { get; set; }
    }
}

