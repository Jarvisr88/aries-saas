namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public interface IClauseNodeEx : IClauseNode, INode
    {
        object FunctionType { get; set; }
    }
}

