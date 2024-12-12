namespace DevExpress.Data
{
    using System;
    using System.Collections;

    public interface IRelationList
    {
        IList GetDetailList(int index, int relationIndex);
        string GetRelationName(int index, int relationIndex);
        bool IsMasterRowEmpty(int index, int relationIndex);

        int RelationCount { get; }
    }
}

