namespace DevExpress.Emf
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class EmfPlusRegion : EmfPlusObject
    {
        private EmfPlusRegionNode head;

        public EmfPlusRegion(EmfPlusReader reader)
        {
            reader.ReadInt32();
            int num = reader.ReadInt32();
            this.head = Create(reader);
        }

        public EmfPlusRegion(EmfPlusRegionNode head)
        {
            this.head = head;
        }

        public EmfPlusRegion Combine(EmfPlusCombineMode mode, EmfPlusRegion newRegion) => 
            (mode != EmfPlusCombineMode.CombineModeReplace) ? new EmfPlusRegion(new EmfPlusRegionComplexNode(this.head, newRegion.head, mode)) : newRegion;

        private static EmfPlusRegionNode Create(EmfPlusReader reader)
        {
            EmfPlusCombineMode combineModeIntersect;
            EmfPlusRegionNodeDataType type = (EmfPlusRegionNodeDataType) reader.ReadInt32();
            switch (type)
            {
                case EmfPlusRegionNodeDataType.RegionNodeDataTypeRect:
                    return new EmfPlusRegionRectangleNode(new RectangleF(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));

                case EmfPlusRegionNodeDataType.RegionNodeDataTypePath:
                {
                    int num = reader.ReadInt32() + ((int) reader.BaseStream.Position);
                    EmfPlusPath path = new EmfPlusPath(reader);
                    reader.BaseStream.Position = num;
                    return new EmfPlusRegionPathNode(path.PathData);
                }
                case EmfPlusRegionNodeDataType.RegionNodeDataTypeEmpty:
                    return new EmfPlusRegionEmptyNode();

                case EmfPlusRegionNodeDataType.RegionNodeDataTypeInfinite:
                    return new EmfPlusRegionInfiniteNode();
            }
            EmfPlusRegionNode left = Create(reader);
            EmfPlusRegionNode right = Create(reader);
            switch (type)
            {
                case EmfPlusRegionNodeDataType.RegionNodeDataTypeAnd:
                    combineModeIntersect = EmfPlusCombineMode.CombineModeIntersect;
                    break;

                case EmfPlusRegionNodeDataType.RegionNodeDataTypeOr:
                    combineModeIntersect = EmfPlusCombineMode.CombineModeUnion;
                    break;

                case EmfPlusRegionNodeDataType.RegionNodeDataTypeXor:
                    combineModeIntersect = EmfPlusCombineMode.CombineModeXOR;
                    break;

                case EmfPlusRegionNodeDataType.RegionNodeDataTypeExclude:
                    combineModeIntersect = EmfPlusCombineMode.CombineModeExclude;
                    break;

                case EmfPlusRegionNodeDataType.RegionNodeDataTypeComplement:
                    combineModeIntersect = EmfPlusCombineMode.CombineModeComplement;
                    break;

                default:
                    return null;
            }
            return new EmfPlusRegionComplexNode(left, right, combineModeIntersect);
        }

        public EmfPlusRegion GetTransformedRegion(Matrix transformMatrix) => 
            new EmfPlusRegion(this.head.Transform(transformMatrix));

        public override void Write(EmfContentWriter writer)
        {
        }

        public EmfPlusRegionNode RegionData =>
            this.head;

        public override EmfPlusObjectType Type =>
            EmfPlusObjectType.ObjectTypeRegion;

        public override int Size =>
            0;
    }
}

