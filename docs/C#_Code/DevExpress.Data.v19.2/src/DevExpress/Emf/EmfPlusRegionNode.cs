namespace DevExpress.Emf
{
    using System;
    using System.Drawing.Drawing2D;

    public abstract class EmfPlusRegionNode
    {
        protected EmfPlusRegionNode()
        {
        }

        public abstract void Accept(IEmfPlusRegionNodeVisitor visitor);
        public abstract EmfPlusRegionNode Transform(Matrix transformMatrix);
    }
}

