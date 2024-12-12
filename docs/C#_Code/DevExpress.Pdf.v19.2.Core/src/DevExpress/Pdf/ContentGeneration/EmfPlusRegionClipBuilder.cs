namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;
    using System.Drawing;

    public class EmfPlusRegionClipBuilder : IEmfPlusRegionNodeVisitor
    {
        private readonly PdfGraphicsCommandConstructor commandConstructor;

        public EmfPlusRegionClipBuilder(PdfGraphicsCommandConstructor commandConstructor)
        {
            this.commandConstructor = commandConstructor;
        }

        public void Visit(EmfPlusRegionComplexNode node)
        {
            node.Left.Accept(this);
            node.Right.Accept(this);
        }

        public void Visit(EmfPlusRegionEmptyNode node)
        {
            this.commandConstructor.IntersectClipWithoutWorldTransform(RectangleF.Empty);
        }

        public void Visit(EmfPlusRegionInfiniteNode node)
        {
        }

        public void Visit(EmfPlusRegionPathNode node)
        {
            this.commandConstructor.IntersectClipWithoutWorldTransform(node.Points, node.Types, node.IsWindingFillMode);
        }

        public void Visit(EmfPlusRegionRectangleNode node)
        {
            this.commandConstructor.IntersectClipWithoutWorldTransform(node.Rectangle);
        }
    }
}

