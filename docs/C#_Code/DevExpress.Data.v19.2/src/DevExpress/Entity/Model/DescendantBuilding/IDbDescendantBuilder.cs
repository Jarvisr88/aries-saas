namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using System;

    public interface IDbDescendantBuilder : IDisposable
    {
        object Build();

        DevExpress.Entity.Model.TypesCollector TypesCollector { get; }

        object DescendantInstance { get; }

        bool SuppressExceptions { get; }
    }
}

