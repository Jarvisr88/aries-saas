namespace DevExpress.Emf
{
    using System;

    public interface IEmfPlusRegionNodeVisitor
    {
        void Visit(EmfPlusRegionComplexNode node);
        void Visit(EmfPlusRegionEmptyNode node);
        void Visit(EmfPlusRegionInfiniteNode node);
        void Visit(EmfPlusRegionPathNode node);
        void Visit(EmfPlusRegionRectangleNode node);
    }
}

