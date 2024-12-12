namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class EmfPlusRegionNode
    {
        public EmfPlusRegionNode(MetaReader reader)
        {
            this.Type = (RegionNodeDataType) reader.ReadUInt32();
            switch (this.Type)
            {
                case RegionNodeDataType.RegionNodeDataTypeRect:
                    this.Region = new System.Drawing.Region(reader.ReadRectF());
                    return;

                case RegionNodeDataType.RegionNodeDataTypePath:
                    this.Region = new System.Drawing.Region(new EmfPlusRegionNodePath(reader).Path);
                    return;

                case RegionNodeDataType.RegionNodeDataTypeEmpty:
                    this.Region = new System.Drawing.Region();
                    this.Region.MakeEmpty();
                    return;

                case RegionNodeDataType.RegionNodeDataTypeInfinite:
                    this.Region = new System.Drawing.Region();
                    this.Region.MakeInfinite();
                    return;
            }
            this.Region = new EmfPlusRegionNodeChildNodes(reader, this.Type).Region;
        }

        private RegionNodeDataType Type { get; set; }

        public System.Drawing.Region Region { get; set; }
    }
}

