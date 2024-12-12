namespace DevExpress.Emf
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusRegionComplexNode : EmfPlusRegionNode
    {
        public EmfPlusRegionComplexNode(EmfPlusRegionNode left, EmfPlusRegionNode right, EmfPlusCombineMode combineMode)
        {
            this.<Left>k__BackingField = left;
            this.<Right>k__BackingField = right;
            this.<CombineMode>k__BackingField = combineMode;
        }

        public override void Accept(IEmfPlusRegionNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override EmfPlusRegionNode Transform(Matrix transformMatrix) => 
            new EmfPlusRegionComplexNode(this.Left.Transform(transformMatrix), this.Right.Transform(transformMatrix), this.CombineMode);

        public EmfPlusRegionNode Left { get; }

        public EmfPlusRegionNode Right { get; }

        public EmfPlusCombineMode CombineMode { get; }
    }
}

