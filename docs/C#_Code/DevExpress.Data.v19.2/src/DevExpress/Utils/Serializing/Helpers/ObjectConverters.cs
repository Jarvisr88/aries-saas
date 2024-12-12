namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections.Generic;

    public class ObjectConverters
    {
        private Dictionary<Type, IOneTypeObjectConverter> converters = new Dictionary<Type, IOneTypeObjectConverter>();

        public object ConvertFromString(Type type, string str) => 
            this.GetConverter(type)?.FromString(str);

        public string ConvertToString(object obj) => 
            this.GetConverter(obj.GetType())?.ToString(obj);

        public void CopyTo(ObjectConverters toConverters)
        {
            foreach (Type type in this.Converters.Keys)
            {
                toConverters.RegisterConverter(this.Converters[type]);
            }
        }

        public virtual IOneTypeObjectConverter GetConverter(Type type) => 
            !this.IsConverterExists(type) ? null : this.Converters[type];

        public virtual bool IsConverterExists(Type type) => 
            this.Converters.ContainsKey(type);

        public void RegisterConverter(IOneTypeObjectConverter converter)
        {
            if (!this.Converters.ContainsKey(converter.Type))
            {
                this.Converters.Add(converter.Type, converter);
            }
        }

        public Type ResolveType(string typeName)
        {
            Type type2;
            using (Dictionary<Type, IOneTypeObjectConverter>.KeyCollection.Enumerator enumerator = this.Converters.Keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type current = enumerator.Current;
                        if (current.FullName != typeName)
                        {
                            continue;
                        }
                        type2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return type2;
        }

        public void UnregisterConverter(Type type)
        {
            this.Converters.Remove(type);
        }

        protected Dictionary<Type, IOneTypeObjectConverter> Converters =>
            this.converters;
    }
}

