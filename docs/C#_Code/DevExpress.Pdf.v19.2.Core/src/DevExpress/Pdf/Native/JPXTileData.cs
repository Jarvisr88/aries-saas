namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class JPXTileData
    {
        private readonly List<IJPXTileDataAction> packets = new List<IJPXTileDataAction>();

        public void AppendPacket(Stream stream)
        {
            this.packets.Add(new JPXTilePacketData(stream));
        }

        public void AppendQuantizationParameters(int componentIndex, JPXQuantizationComponentParameters parameters)
        {
            this.packets.Add(new JPXTileQuantizationParametersData(componentIndex, parameters));
        }

        public void Process(JPXTile tile)
        {
            foreach (IJPXTileDataAction action in this.packets)
            {
                action.Process(tile);
            }
            this.packets.Clear();
        }
    }
}

