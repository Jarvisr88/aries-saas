namespace DMEWorks.Forms
{
    using System;

    public class EntityField : IEntityField, IDataStorage
    {
        private IDataStorage storage;
        private string warning;
        private string error;
        private string name;

        public EntityField(string name, Type dataType)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name must be not null and non empty", "name");
            }
            this.name = name;
            this.storage = CreateStorage(dataType);
        }

        public void AcceptChanges()
        {
            this.storage.AcceptChanges();
        }

        private static IDataStorage CreateStorage(Type type)
        {
            if (type != typeof(bool))
            {
                if (type == typeof(byte))
                {
                    return new DataStorage<byte>();
                }
                if (type == typeof(char))
                {
                    return new DataStorage<char>();
                }
                if (type == typeof(DateTime))
                {
                    return new DataStorage<DateTime>();
                }
                if (type == typeof(decimal))
                {
                    return new DataStorage<decimal>();
                }
                if (type == typeof(double))
                {
                    return new DataStorage<double>();
                }
                if (type == typeof(short))
                {
                    return new DataStorage<short>();
                }
                if (type == typeof(int))
                {
                    return new DataStorage<int>();
                }
                if (type == typeof(long))
                {
                    return new DataStorage<long>();
                }
                if (type == typeof(sbyte))
                {
                    return new DataStorage<sbyte>();
                }
                if (type == typeof(float))
                {
                    return new DataStorage<float>();
                }
                if (type == typeof(string))
                {
                    return new DataStorage<string>();
                }
                if (type == typeof(ushort))
                {
                    return new DataStorage<ushort>();
                }
                if (type == typeof(uint))
                {
                    return new DataStorage<uint>();
                }
                if (!(type == typeof(ulong)))
                {
                    throw new ArgumentException("", "type");
                }
            }
            return new DataStorage<ulong>();
        }

        public void RejectChanges()
        {
            this.storage.RejectChanges();
        }

        public string Name =>
            this.name;

        public string Error
        {
            get => 
                this.error;
            set => 
                this.error = value;
        }

        public string Warning
        {
            get => 
                this.warning;
            set => 
                this.warning = value;
        }

        public Type DataType =>
            this.storage.DataType;

        public object OriginalValue =>
            this.storage.OriginalValue;

        public object Value
        {
            get => 
                this.storage.Value;
            set => 
                this.storage.Value = value;
        }
    }
}

