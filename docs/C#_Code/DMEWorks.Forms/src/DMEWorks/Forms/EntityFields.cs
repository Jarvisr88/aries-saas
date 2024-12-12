namespace DMEWorks.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class EntityFields : IEntityFields, IEnumerable
    {
        private readonly EntityField[] fields;
        private readonly Dictionary<string, EntityField> hash;

        internal EntityFields(params EntityField[] fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }
            if (fields.Length == 0)
            {
                throw new ArgumentException("empty array is not allowed", "fields");
            }
            this.fields = new EntityField[fields.Length];
            this.hash = new Dictionary<string, EntityField>(StringComparer.InvariantCultureIgnoreCase);
            for (int i = 0; i < fields.Length; i++)
            {
                EntityField field = fields[i];
                this.fields[i] = field;
                this.hash.Add(field.Name, field);
            }
        }

        internal void AcceptChanges()
        {
            for (int i = 0; i < this.fields.Length; i++)
            {
                this.fields[i].AcceptChanges();
            }
        }

        internal void ClearErrors()
        {
            for (int i = 0; i < this.fields.Length; i++)
            {
                this.fields[i].Error = null;
                this.fields[i].Warning = null;
            }
        }

        public IEnumerator GetEnumerator() => 
            this.fields.GetEnumerator();

        internal void RejectChanges()
        {
            for (int i = 0; i < this.fields.Length; i++)
            {
                this.fields[i].RejectChanges();
            }
        }

        public int Count =>
            this.fields.Length;

        public IEntityField this[string name]
        {
            get
            {
                EntityField field;
                if (!this.hash.TryGetValue(name, out field))
                {
                    throw new IndexOutOfRangeException();
                }
                return field;
            }
        }

        public IEntityField this[int index]
        {
            get
            {
                if ((index < 0) || (this.fields.Length < index))
                {
                    throw new IndexOutOfRangeException();
                }
                return this.fields[index];
            }
        }
    }
}

