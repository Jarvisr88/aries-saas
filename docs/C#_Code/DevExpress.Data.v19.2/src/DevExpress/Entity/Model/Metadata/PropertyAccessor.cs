namespace DevExpress.Entity.Model.Metadata
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class PropertyAccessor
    {
        private object source;
        private Type sourceType;

        private PropertyAccessor(string name)
        {
            this.Name = name;
        }

        public PropertyAccessor(object source, string name) : this(name)
        {
            this.source = source;
            if (source != null)
            {
                this.sourceType = source.GetType();
            }
        }

        public PropertyAccessor(string name, Type sourceType) : this(name)
        {
            this.sourceType = sourceType;
        }

        public static object GetValue(object source, string name)
        {
            if (source == null)
            {
                return null;
            }
            PropertyInfo property = source.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property == null)
            {
                return null;
            }
            return property.GetGetMethod()?.Invoke(source, null);
        }

        public static bool IsComplexPropertyName(string fullName) => 
            !string.IsNullOrEmpty(fullName) && (fullName.IndexOf(".") > 0);

        public override string ToString()
        {
            try
            {
                object obj2 = this.Value;
                obj2 ??= "null";
                return $"{this.Name}: {obj2.ToString()}";
            }
            catch
            {
                return base.ToString();
            }
        }

        protected PropertyInfo Property { get; set; }

        public string Name { get; private set; }

        public virtual object Value
        {
            get
            {
                if (this.source == null)
                {
                    return null;
                }
                if (this.Property == null)
                {
                    this.Property = this.sourceType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase).FirstOrDefault<PropertyInfo>(x => x.Name == this.Name);
                }
                if (this.Property == null)
                {
                    return null;
                }
                MethodInfo getMethod = this.Property.GetGetMethod();
                return ((getMethod == null) ? this.Property.GetValue(this.source, new object[0]) : getMethod.Invoke(this.source, null));
            }
        }
    }
}

