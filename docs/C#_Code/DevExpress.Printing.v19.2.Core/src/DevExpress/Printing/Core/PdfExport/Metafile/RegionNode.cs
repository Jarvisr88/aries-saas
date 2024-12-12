namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;

    public abstract class RegionNode
    {
        protected RegionNode()
        {
        }

        public abstract T Accept<T>(IRegionNodeVisitor<T> visitor);
        public static RegionNode Read(MetaReader reader)
        {
            RegionNodeDataType type = (RegionNodeDataType) reader.ReadUInt32();
            switch (type)
            {
                case RegionNodeDataType.RegionNodeDataTypeRect:
                    return new RegionRectNode(reader);

                case RegionNodeDataType.RegionNodeDataTypePath:
                    return new RegionPathNode(reader);

                case RegionNodeDataType.RegionNodeDataTypeEmpty:
                    return new RegionEmptyNode();

                case RegionNodeDataType.RegionNodeDataTypeInfinite:
                    return new RegionInfiniteNode();
            }
            return new RegionChildNodesNode(reader, type);
        }

        public abstract RegionNodeDataType Type { get; }
    }
}

