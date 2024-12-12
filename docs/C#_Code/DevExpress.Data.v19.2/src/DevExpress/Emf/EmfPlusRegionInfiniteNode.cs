namespace DevExpress.Emf
{
    using System;
    using System.Drawing.Drawing2D;

    public class EmfPlusRegionInfiniteNode : EmfPlusRegionNode
    {
        public override void Accept(IEmfPlusRegionNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override EmfPlusRegionNode Transform(Matrix transformMatrix) => 
            this;
    }
}

