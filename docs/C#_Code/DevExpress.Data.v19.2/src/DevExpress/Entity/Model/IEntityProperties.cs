namespace DevExpress.Entity.Model
{
    using System.Collections.Generic;

    public interface IEntityProperties
    {
        IEnumerable<IEdmPropertyInfo> AllProperties { get; }
    }
}

