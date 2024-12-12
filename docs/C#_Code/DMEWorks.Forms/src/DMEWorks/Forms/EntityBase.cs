namespace DMEWorks.Forms
{
    using System;

    public class EntityBase : IEntity
    {
        private readonly EntityFields fields;
        private bool isNew = true;

        public EntityBase(params EntityField[] fields)
        {
            this.fields = new EntityFields(fields);
        }

        public void AcceptChanges()
        {
            this.fields.AcceptChanges();
        }

        protected EntityField CheckField(IEntityField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            EntityField field1 = field as EntityField;
            if (field1 == null)
            {
                throw new ArgumentException("field must be of EntityField type", "field");
            }
            return field1;
        }

        public void ClearErrors()
        {
            this.fields.ClearErrors();
        }

        public virtual EntityBase Clone()
        {
            EntityBase base2 = (EntityBase) Activator.CreateInstance(base.GetType());
            base2.IsNew = true;
            for (int i = 0; i < this.fields.Count; i++)
            {
                IEntityField field = this.fields[i];
                base2.fields[field.Name].Value = this.IsNew ? field.Value : field.OriginalValue;
            }
            return base2;
        }

        public static bool HasErrors(IEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            for (int i = 0; i < entity.Fields.Count; i++)
            {
                string error = entity.Fields[i].Error;
                if ((error != null) && (error.Length != 0))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasWarnings(IEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            for (int i = 0; i < entity.Fields.Count; i++)
            {
                string warning = entity.Fields[i].Warning;
                if ((warning != null) && (warning.Length != 0))
                {
                    return true;
                }
            }
            return false;
        }

        public IEntityFields Fields =>
            this.fields;

        public bool IsNew
        {
            get => 
                this.isNew;
            set => 
                this.isNew = value;
        }
    }
}

