namespace DMEWorks.Forms
{
    using System;

    internal class NullAdapter : IAdapter
    {
        public IEntity CloneEntity(IEntity entity) => 
            entity;

        public IEntity CreateEntity()
        {
            EntityField[] fields = new EntityField[] { new EntityField("ID", typeof(int)) };
            return new EntityBase(fields);
        }

        public bool DeleteEntity(IEntity entity) => 
            false;

        public IEntity LoadEntity(object[] args)
        {
            EntityField[] fields = new EntityField[] { new EntityField("ID", typeof(int)) };
            return new EntityBase(fields);
        }

        public IEntity SaveEntity(IEntity entity) => 
            entity;

        public bool ValidateEntity(IEntity entity) => 
            true;
    }
}

