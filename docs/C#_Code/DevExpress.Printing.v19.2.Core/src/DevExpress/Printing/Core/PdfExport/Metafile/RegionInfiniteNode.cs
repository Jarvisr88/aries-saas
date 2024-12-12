namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    public class RegionInfiniteNode : RegionNode
    {
        public override T Accept<T>(IRegionNodeVisitor<T> visitor) => 
            visitor.Visit(this);

        public override RegionNodeDataType Type =>
            RegionNodeDataType.RegionNodeDataTypeInfinite;
    }
}

