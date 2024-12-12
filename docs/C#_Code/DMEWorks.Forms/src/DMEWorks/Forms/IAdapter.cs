namespace DMEWorks.Forms
{
    using System;

    public interface IAdapter
    {
        IEntity CloneEntity(IEntity entity);
        IEntity CreateEntity();
        bool DeleteEntity(IEntity entity);
        IEntity LoadEntity(object[] args);
        IEntity SaveEntity(IEntity entity);
        bool ValidateEntity(IEntity entity);
    }
}

