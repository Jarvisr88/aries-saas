namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.IO;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Runtime.InteropServices;

    public static class FilterTreeCloner
    {
        private static ICollection<CriteriaOperator> Clone(ICollection<CriteriaOperator> source);
        private static IClauseNode Clone(IClauseNode source, INodesFactory factory);
        public static IGroupNode Clone(IGroupNode source, INodesFactory factory);
        private static INode Clone(INode node, INodesFactory factory);
        private static ICollection<INode> Clone(ICollection<INode> source, INodesFactory factory);
        private static CriteriaOperator CriteriaParse(string criteria, INodesFactory factory);
        public static IGroupNode Deserialize(Stream stream, INodesFactory factory);
        private static IAggregateNode DeserializeAggregateNode(TypedBinaryReader reader, INodesFactory factory);
        private static IClauseNode DeserializeClauseNode(TypedBinaryReader reader, INodesFactory factory);
        private static void DeserializeClauseNode(TypedBinaryReader reader, out ClauseType clauseType, out CriteriaOperator firstOperand, Collection<CriteriaOperator> operands, INodesFactory factory);
        private static IGroupNode DeserializeGroupNode(TypedBinaryReader reader, INodesFactory factory);
        private static INode DeserializeNode(TypedBinaryReader reader, INodesFactory factory);
        public static IGroupNode FromString(string str, INodesFactory factory);
        public static void Serialize(IGroupNode source, Stream stream);
        private static void SerializeAggregateNode(IAggregateNode source, TypedBinaryWriter writer);
        private static void SerializeClauseNode(IClauseNode source, TypedBinaryWriter writer);
        private static void SerializeGroupNode(IGroupNode source, TypedBinaryWriter writer);
        private static void SerializeNode(INode node, TypedBinaryWriter writer);
        public static string ToString(IGroupNode source);
    }
}

