namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing.Drawing2D;

    public class UnionAll : IRegionNodeVisitor<GraphicsPath>
    {
        private static void AddPath(GraphicsPath path, GraphicsPath subPath)
        {
            if ((subPath != null) && (subPath.PointCount > 0))
            {
                path.AddPath(subPath, false);
            }
        }

        public GraphicsPath Process(RegionNode node) => 
            node.Accept<GraphicsPath>(this);

        public GraphicsPath Visit(RegionChildNodesNode node)
        {
            GraphicsPath path = new GraphicsPath();
            AddPath(path, this.Process(node.Left));
            AddPath(path, this.Process(node.Right));
            return path;
        }

        public GraphicsPath Visit(RegionEmptyNode node) => 
            null;

        public GraphicsPath Visit(RegionInfiniteNode node) => 
            null;

        public GraphicsPath Visit(RegionPathNode node) => 
            node.Path;

        public GraphicsPath Visit(RegionRectNode node)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(node.Rect);
            return path;
        }
    }
}

