namespace DevExpress.Pdf.Native
{
    using System;

    public enum JPXProgressionOrder
    {
        [PdfFieldValue(0)]
        LayerResolutionComponentPosition = 0,
        [PdfFieldValue(1)]
        ResolutionLayerComponentPosition = 1,
        [PdfFieldValue(2)]
        ResolutionPositionComponentLayer = 2,
        [PdfFieldValue(3)]
        PositionComponentResolutionLayer = 3,
        [PdfFieldValue(4)]
        ComponentPositionResolutionLayer = 4
    }
}

