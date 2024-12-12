namespace DMEWorks.Data.Common
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class AdapterBase<TEntity, TKey, TNew, TExisting> : IAdapter where TEntity: class where TNew: TEntity where TExisting: TEntity, IKeyed<TKey>
    {
        protected AdapterBase()
        {
        }

        private static void Check(object entity, out bool isNew)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if ((entity is TExisting) && (entity is TNew))
            {
                throw new ArgumentException("", "entity");
            }
            if (entity is TExisting)
            {
                isNew = false;
            }
            else
            {
                if (!(entity is TNew))
                {
                    throw new ArgumentException("", "entity");
                }
                isNew = true;
            }
        }

        protected abstract TNew Clone(TExisting entity);
        protected abstract TNew Create();
        protected abstract void Delete(TKey key);
        object IAdapter.Clone(object entity)
        {
            bool flag;
            AdapterBase<TEntity, TKey, TNew, TExisting>.Check(entity, out flag);
            return (!flag ? this.Clone((TExisting) entity) : ((TNew) entity));
        }

        object IAdapter.Create() => 
            this.Create();

        void IAdapter.Delete(object entity)
        {
            bool flag;
            AdapterBase<TEntity, TKey, TNew, TExisting>.Check(entity, out flag);
            if (flag)
            {
                throw new InvalidOperationException();
            }
            this.Delete(((TExisting) entity).Key);
        }

        object IAdapter.GetKey(object entity)
        {
            bool flag;
            AdapterBase<TEntity, TKey, TNew, TExisting>.Check(entity, out flag);
            if (flag)
            {
                throw new InvalidOperationException();
            }
            return ((TExisting) entity).Key;
        }

        bool IAdapter.IsNew(object entity)
        {
            bool flag;
            AdapterBase<TEntity, TKey, TNew, TExisting>.Check(entity, out flag);
            return flag;
        }

        object IAdapter.Load(object key)
        {
            if (!(key is TKey))
            {
                throw new ArgumentException("", "key");
            }
            return this.Select((TKey) key);
        }

        object IAdapter.Save(object entity)
        {
            bool flag;
            AdapterBase<TEntity, TKey, TNew, TExisting>.Check(entity, out flag);
            return (!flag ? this.Update((TExisting) entity) : this.Insert((TNew) entity));
        }

        IValidationResult IAdapter.Validate(object entity)
        {
            bool flag;
            AdapterBase<TEntity, TKey, TNew, TExisting>.Check(entity, out flag);
            return this.Validate((TEntity) entity);
        }

        protected abstract TExisting Insert(TNew entity);
        protected abstract TExisting Select(TKey key);
        protected abstract TExisting Update(TExisting entity);
        protected abstract IValidationResult Validate(TEntity entity);
    }
}

