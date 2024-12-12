namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Runtime.CompilerServices;

    public class RegionChildNodesNode : RegionNode
    {
        private RegionNodeDataType type;

        public RegionChildNodesNode(MetaReader reader, RegionNodeDataType type)
        {
            this.type = type;
            this.Left = Read(reader);
            this.Right = Read(reader);
        }

        public override T Accept<T>(IRegionNodeVisitor<T> visitor) => 
            visitor.Visit(this);

        public RegionNode Left { get; private set; }

        public RegionNode Right { get; private set; }

        public override RegionNodeDataType Type =>
            this.type;
    }
}

