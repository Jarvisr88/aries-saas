namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class RegionRectNode : RegionNode
    {
        public RegionRectNode(MetaReader reader)
        {
            this.Rect = reader.ReadRectF();
        }

        public override T Accept<T>(IRegionNodeVisitor<T> visitor) => 
            visitor.Visit(this);

        public RectangleF Rect { get; private set; }

        public override RegionNodeDataType Type =>
            RegionNodeDataType.RegionNodeDataTypeRect;
    }
}

