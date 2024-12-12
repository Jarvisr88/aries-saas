namespace DMEWorks.Data.Common
{
    using System;

    public interface IAdapter
    {
        object Clone(object entity);
        object Create();
        void Delete(object entity);
        object GetKey(object entity);
        bool IsNew(object entity);
        object Load(object key);
        object Save(object entity);
        IValidationResult Validate(object entity);
    }
}

