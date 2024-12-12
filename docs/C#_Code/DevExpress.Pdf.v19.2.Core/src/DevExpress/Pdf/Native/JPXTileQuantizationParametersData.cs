namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXTileQuantizationParametersData : IJPXTileDataAction
    {
        private readonly JPXQuantizationComponentParameters quantizationComponentParameters;
        private int componentIndex;

        public JPXTileQuantizationParametersData(int componentIndex, JPXQuantizationComponentParameters quantizationComponentParameters)
        {
            this.quantizationComponentParameters = quantizationComponentParameters;
            this.componentIndex = componentIndex;
        }

        public void Process(JPXTile tile)
        {
            tile.Components[this.componentIndex].QuantizationParameters = this.quantizationComponentParameters;
        }
    }
}

