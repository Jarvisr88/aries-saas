namespace DMEWorks.Forms
{
    using System;

    public interface IEntity
    {
        bool IsNew { get; }

        IEntityFields Fields { get; }
    }
}

