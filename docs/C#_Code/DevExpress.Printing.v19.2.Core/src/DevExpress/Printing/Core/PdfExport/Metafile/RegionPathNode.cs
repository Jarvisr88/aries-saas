namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class RegionPathNode : RegionNode
    {
        public RegionPathNode(MetaReader reader)
        {
            int count = reader.ReadInt32();
            MetaReader reader2 = new MetaReader(new MemoryStream(reader.ReadBytes(count)));
            this.Path = new EmfPlusPath(reader2).Path;
        }

        public override T Accept<T>(IRegionNodeVisitor<T> visitor) => 
            visitor.Visit(this);

        public GraphicsPath Path { get; private set; }

        public override RegionNodeDataType Type =>
            RegionNodeDataType.RegionNodeDataTypePath;
    }
}

