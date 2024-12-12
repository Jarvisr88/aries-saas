namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXCodingStyleDefault
    {
        private readonly int numberOfLayers;
        private readonly JPXProgressionOrder progressionOrder;
        private readonly bool isMultipleComponentTransformationSpecified;
        public int NumberOfLayers =>
            this.numberOfLayers;
        public JPXProgressionOrder ProgressionOrder =>
            this.progressionOrder;
        public bool IsMultipleComponentTransformationSpecified =>
            this.isMultipleComponentTransformationSpecified;
        public JPXCodingStyleDefault(PdfBigEndianStreamReader reader)
        {
            JPXProgressionOrder? defaultValue = null;
            this.progressionOrder = PdfEnumToValueConverter.Parse<JPXProgressionOrder>(new int?(reader.ReadByte()), defaultValue);
            this.numberOfLayers = reader.ReadInt16();
            this.isMultipleComponentTransformationSpecified = reader.ReadByte() > 0;
        }
    }
}

