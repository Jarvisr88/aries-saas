namespace DevExpress.Data
{
    using System;
    using System.Collections;

    public interface IDataControllerRelationSupport
    {
        IList GetDetailList(int controllerRow, int relationIndex);
        int GetRelationCount(int relationCount, int controllerRow);
        string GetRelationDisplayName(string displayName, int controllerRow, int relationIndex);
        string GetRelationName(string name, int controllerRow, int relationIndex);
        bool IsMasterRowEmpty(bool isEmpty, int controllerRow, int relationIndex);
    }
}

