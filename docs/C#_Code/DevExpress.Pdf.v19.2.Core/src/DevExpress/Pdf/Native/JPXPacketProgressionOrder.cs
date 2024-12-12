namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class JPXPacketProgressionOrder : IEnumerable<JPXPacketPosition>, IEnumerable
    {
        private readonly JPXImage image;
        private readonly JPXTile tile;

        protected JPXPacketProgressionOrder(JPXImage image, JPXTile tile)
        {
            this.image = image;
            this.tile = tile;
        }

        public static JPXPacketProgressionOrder Create(JPXProgressionOrder order, JPXImage image, JPXTile tile)
        {
            switch (order)
            {
                case JPXProgressionOrder.LayerResolutionComponentPosition:
                    return new JPXLayerResolutionComponentPrecinctPacketProgressionOrder(image, tile);

                case JPXProgressionOrder.ResolutionLayerComponentPosition:
                    return new JPXResolutionLayerComponentPrecinctPacketProgressionOrder(image, tile);

                case JPXProgressionOrder.ResolutionPositionComponentLayer:
                    return new JPXResolutionPrecinctComponentLayerPacketProgressionOrder(image, tile);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        public abstract IEnumerator<JPXPacketPosition> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        protected JPXImage Image =>
            this.image;

        protected JPXTile Tile =>
            this.tile;

        protected int MaxResolutionLevel
        {
            get
            {
                JPXCodingStyleComponent[] codingStyleComponents = this.Image.CodingStyleComponents;
                int num = 0;
                for (int i = 0; i < codingStyleComponents.Length; i++)
                {
                    num = Math.Max(codingStyleComponents[i].DecompositionLevelCount + 1, num);
                }
                return num;
            }
        }
    }
}

