namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    public class RegionEmptyNode : RegionNode
    {
        public override T Accept<T>(IRegionNodeVisitor<T> visitor) => 
            visitor.Visit(this);

        public override RegionNodeDataType Type =>
            RegionNodeDataType.RegionNodeDataTypeEmpty;
    }
}

