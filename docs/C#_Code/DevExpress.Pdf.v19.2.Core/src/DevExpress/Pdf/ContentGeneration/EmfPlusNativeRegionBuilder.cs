namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class EmfPlusNativeRegionBuilder : IEmfPlusRegionNodeVisitor
    {
        private Region region;

        private EmfPlusNativeRegionBuilder()
        {
        }

        public static Region CreateRegion(EmfPlusRegionNode node)
        {
            EmfPlusNativeRegionBuilder visitor = new EmfPlusNativeRegionBuilder();
            node.Accept(visitor);
            return visitor.region;
        }

        public void Visit(EmfPlusRegionComplexNode node)
        {
            if (node.CombineMode == EmfPlusCombineMode.CombineModeReplace)
            {
                this.region = CreateRegion(node.Right);
            }
            else
            {
                this.region = CreateRegion(node.Left);
                using (Region region = CreateRegion(node.Right))
                {
                    switch (node.CombineMode)
                    {
                        case EmfPlusCombineMode.CombineModeReplace:
                            this.region.Dispose();
                            this.region = CreateRegion(node.Right);
                            break;

                        case EmfPlusCombineMode.CombineModeIntersect:
                            this.region.Intersect(region);
                            break;

                        case EmfPlusCombineMode.CombineModeUnion:
                            this.region.Union(region);
                            break;

                        case EmfPlusCombineMode.CombineModeXOR:
                            this.region.Xor(region);
                            break;

                        case EmfPlusCombineMode.CombineModeExclude:
                            this.region.Exclude(region);
                            break;

                        case EmfPlusCombineMode.CombineModeComplement:
                            this.region.Complement(region);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void Visit(EmfPlusRegionEmptyNode node)
        {
            this.region = new Region();
            this.region.MakeEmpty();
        }

        public void Visit(EmfPlusRegionInfiniteNode node)
        {
            this.region = new Region();
            this.region.MakeInfinite();
        }

        public void Visit(EmfPlusRegionPathNode node)
        {
            this.region = new Region(new GraphicsPath(node.Points, node.Types, node.IsWindingFillMode ? FillMode.Winding : FillMode.Alternate));
        }

        public void Visit(EmfPlusRegionRectangleNode node)
        {
            this.region = new Region(node.Rectangle);
        }
    }
}

