namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class JPXTile : JPXArea, IDisposable
    {
        private readonly JPXTileComponent[] components;
        private readonly JPXImage image;
        private IEnumerator<JPXPacketPosition> packetEnumerator;
        private JPXColorTransformation colorTransformation;

        public JPXTile(JPXImage image, int tileIndex)
        {
            this.image = image;
            int num = tileIndex % image.NumXTiles;
            int num2 = (int) Math.Floor((double) (((float) tileIndex) / ((float) image.NumXTiles)));
            JPXSize size = image.Size;
            base.X0 = Math.Max(size.TileHorizontalOffset + (num * size.TileWidth), size.GridHorizontalOffset);
            base.Y0 = Math.Max(size.TileVerticalOffset + (num2 * size.TileHeight), size.GridVerticalOffset);
            base.X1 = Math.Min(size.TileHorizontalOffset + ((num + 1) * size.TileWidth), size.GridWidth);
            base.Y1 = Math.Min(size.TileVerticalOffset + ((num2 + 1) * size.TileHeight), size.GridHeight);
            int length = size.Components.Length;
            this.components = new JPXTileComponent[length];
            for (int i = 0; i < length; i++)
            {
                this.components[i] = new JPXTileComponent(this, size.Components[i], image.CodingStyleComponents[i], image.QuantizationParameters[i]);
            }
            this.packetEnumerator = JPXPacketProgressionOrder.Create(image.CodingStyleDefault.ProgressionOrder, image, this).GetEnumerator();
        }

        public void AppendPacket(Stream stream)
        {
            if (!this.packetEnumerator.MoveNext())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            JPXPacketPosition current = this.packetEnumerator.Current;
            JPXBitReader bitReader = new JPXBitReader(stream);
            if (bitReader.GetBit() != 0)
            {
                JPXPacketPosition position2 = this.packetEnumerator.Current;
                int layer = position2.Layer;
                JPXResolutionLevel resolutionLevel = this.components[position2.Component].GetResolutionLevel(position2.ResolutionLevel);
                int codeBlockWidth = resolutionLevel.CodeBlockWidth;
                int codeBlockHeight = resolutionLevel.CodeBlockHeight;
                Queue<JPXCodeBlockHeaderData> queue = new Queue<JPXCodeBlockHeaderData>();
                foreach (JPXPrecinct precinct in resolutionLevel.Precincts)
                {
                    if (precinct.Number == position2.Precinct)
                    {
                        foreach (JPXCodeBlock block in precinct.CodeBlocks)
                        {
                            int x = (block.X0 - precinct.X0) / codeBlockWidth;
                            int y = (block.Y0 - precinct.Y0) / codeBlockHeight;
                            if (!block.IsFirstTimeInclusion)
                            {
                                if (bitReader.GetBit() == 0)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (layer < precinct.InclusionTree.ReadInclusion(x, y, bitReader, layer))
                                {
                                    continue;
                                }
                                block.IsFirstTimeInclusion = false;
                                block.ZeroBitPlanes = precinct.ZeroBitPlaneTree.Read(x, y, bitReader);
                            }
                            byte codingPasses = (byte) ReadCodingPassCount(bitReader);
                            int lBlock = block.LBlock;
                            while (true)
                            {
                                if (bitReader.GetBit() != 1)
                                {
                                    block.LBlock = lBlock;
                                    queue.Enqueue(new JPXCodeBlockHeaderData(block, codingPasses, bitReader.GetInteger(lBlock + ((int) Math.Floor(Math.Log((double) codingPasses, 2.0))))));
                                    break;
                                }
                                lBlock++;
                            }
                        }
                    }
                }
                bitReader.AlignToByte();
                while (queue.Count > 0)
                {
                    JPXCodeBlockHeaderData data = queue.Dequeue();
                    int chunkLength = data.ChunkLength;
                    byte codingPasses = data.CodingPasses;
                    byte[] buffer = new byte[chunkLength];
                    stream.Read(buffer, 0, chunkLength);
                    data.CodeBlock.AddChunk(new JPXCodeBlockChunk(codingPasses, buffer));
                }
            }
        }

        public void Dispose()
        {
            this.packetEnumerator.Dispose();
        }

        private static int ReadCodingPassCount(JPXBitReader reader)
        {
            if (reader.GetBit() == 0)
            {
                return 1;
            }
            if (reader.GetBit() == 0)
            {
                return 2;
            }
            int integer = reader.GetInteger(2);
            if (integer < 3)
            {
                return (integer + 3);
            }
            integer = reader.GetInteger(5);
            return ((integer >= 0x1f) ? (reader.GetInteger(7) + 0x25) : (integer + 6));
        }

        public JPXTileComponent[] Components =>
            this.components;

        public bool IsMultipleComponentTransformationSpecified =>
            this.image.CodingStyleDefault.IsMultipleComponentTransformationSpecified;

        public bool UseWaveletTransformation =>
            this.image.CodingStyleComponents[0].UseWaveletTransformation;

        public JPXColorTransformation ColorTransformation
        {
            get
            {
                this.colorTransformation ??= JPXColorTransformation.Create(this);
                return this.colorTransformation;
            }
        }
    }
}

