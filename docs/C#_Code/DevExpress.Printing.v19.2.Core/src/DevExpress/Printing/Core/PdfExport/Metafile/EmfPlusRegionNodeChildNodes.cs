namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class EmfPlusRegionNodeChildNodes
    {
        public EmfPlusRegionNodeChildNodes(MetaReader reader, RegionNodeDataType type)
        {
            System.Drawing.Region region = new EmfPlusRegionNode(reader).Region;
            System.Drawing.Region region2 = new EmfPlusRegionNode(reader).Region;
            switch (type)
            {
                case RegionNodeDataType.RegionNodeDataTypeAnd:
                    region.Intersect(region2);
                    break;

                case RegionNodeDataType.RegionNodeDataTypeOr:
                    region.Union(region2);
                    break;

                case RegionNodeDataType.RegionNodeDataTypeXor:
                    region.Xor(region2);
                    break;

                case RegionNodeDataType.RegionNodeDataTypeExclude:
                    region.Exclude(region2);
                    break;

                case RegionNodeDataType.RegionNodeDataTypeComplement:
                    region.Complement(region2);
                    break;

                default:
                    break;
            }
            this.Region = region;
        }

        public System.Drawing.Region Region { get; set; }
    }
}

