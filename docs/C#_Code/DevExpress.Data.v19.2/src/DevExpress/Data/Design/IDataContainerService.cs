namespace DevExpress.Data.Design
{
    using DevExpress.Data;
    using System;

    public interface IDataContainerService
    {
        bool AreAppropriate(IDataContainerBase dataContainer, object dataSource);
    }
}

