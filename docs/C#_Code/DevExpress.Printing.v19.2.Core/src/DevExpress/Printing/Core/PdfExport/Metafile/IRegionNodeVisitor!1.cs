namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    public interface IRegionNodeVisitor<T>
    {
        T Visit(RegionChildNodesNode node);
        T Visit(RegionEmptyNode node);
        T Visit(RegionInfiniteNode node);
        T Visit(RegionPathNode node);
        T Visit(RegionRectNode node);
    }
}

