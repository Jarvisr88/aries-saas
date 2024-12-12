namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class EmfPlusRegionComplexityAnalyzer : IEmfPlusRegionNodeVisitor
    {
        private bool isSimple = true;

        private EmfPlusRegionComplexityAnalyzer()
        {
        }

        public static bool IsSimple(EmfPlusRegionNode node)
        {
            EmfPlusRegionComplexityAnalyzer visitor = new EmfPlusRegionComplexityAnalyzer();
            node.Accept(visitor);
            return visitor.isSimple;
        }

        public void Visit(EmfPlusRegionComplexNode node)
        {
            this.isSimple &= ((node.CombineMode == EmfPlusCombineMode.CombineModeIntersect) && IsSimple(node.Left)) && IsSimple(node.Right);
        }

        public void Visit(EmfPlusRegionEmptyNode node)
        {
        }

        public void Visit(EmfPlusRegionInfiniteNode node)
        {
        }

        public void Visit(EmfPlusRegionPathNode node)
        {
        }

        public void Visit(EmfPlusRegionRectangleNode node)
        {
        }
    }
}

