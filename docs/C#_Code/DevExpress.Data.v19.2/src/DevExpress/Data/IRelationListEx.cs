namespace DevExpress.Data
{
    using System;

    public interface IRelationListEx : IRelationList
    {
        int GetRelationCount(int index);
        string GetRelationDisplayName(int index, int relationIndex);
    }
}

