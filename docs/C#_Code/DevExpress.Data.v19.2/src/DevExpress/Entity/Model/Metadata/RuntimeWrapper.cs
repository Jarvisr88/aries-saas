namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class RuntimeWrapper : RuntimeBase
    {
        private string expectedTypeName;
        private Dictionary<string, PropertyAccessor> properties;
        private Dictionary<string, MethodAccessor> methodAccessors;

        protected RuntimeWrapper(string expectedTypeName, object value) : base(value)
        {
            this.properties = new Dictionary<string, PropertyAccessor>();
            this.methodAccessors = new Dictionary<string, MethodAccessor>();
            this.expectedTypeName = expectedTypeName;
        }

        private void CheckTypeName()
        {
            if (this.CheckOnlyTypeName)
            {
                IsTypeNamesMatch(this.Type, this.expectedTypeName, true);
            }
            else
            {
                IsTypeMatch(this.Type, this.expectedTypeName, true);
            }
        }

        public static TTargetType ConvertEnum<TTargetType>(object source) => 
            (TTargetType) Enum.Parse(typeof(TTargetType), source.ToString());

        protected MethodAccessor GetMethodAccessor(string name)
        {
            MethodAccessor accessor;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name");
            }
            if (!this.methodAccessors.TryGetValue(name, out accessor))
            {
                accessor = new MethodAccessor(base.Value, name);
                this.methodAccessors[name] = accessor;
            }
            return accessor;
        }

        protected PropertyAccessor GetPropertyAccessor(string name)
        {
            PropertyAccessor accessor;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name");
            }
            if (!this.properties.TryGetValue(name, out accessor))
            {
                accessor = !PropertyAccessor.IsComplexPropertyName(name) ? new PropertyAccessor(base.Value, name) : new NestedPropertyAccessor(base.Value, name);
                this.properties[name] = accessor;
            }
            return accessor;
        }

        public static bool IsTypeMatch(System.Type targetType, string expectedTypeName, bool throwOnError = false)
        {
            if (string.IsNullOrEmpty(expectedTypeName) || (string.Compare(targetType.FullName, expectedTypeName, true) == 0))
            {
                return true;
            }
            for (System.Type type = targetType.GetBaseType(); type != null; type = type.GetBaseType())
            {
                if (string.Compare(type.FullName, expectedTypeName, true) == 0)
                {
                    return true;
                }
            }
            if (throwOnError)
            {
                throw new ArgumentException($"Expected Type of the Value is "{expectedTypeName}", but was "{targetType.FullName}"");
            }
            return false;
        }

        public static bool IsTypeNamesMatch(System.Type targetType, string expectedTypeName, bool throwOnError = false)
        {
            if (string.IsNullOrEmpty(expectedTypeName) || (string.Compare(targetType.Name, expectedTypeName, true) == 0))
            {
                return true;
            }
            for (System.Type type = targetType.GetBaseType(); type != null; type = type.GetBaseType())
            {
                if (string.Compare(type.Name, expectedTypeName, true) == 0)
                {
                    return true;
                }
            }
            if (throwOnError)
            {
                throw new ArgumentException($"Expected Type of the Value is "{expectedTypeName}", but was "{targetType.Name}"");
            }
            return false;
        }

        protected System.Type Type =>
            base.Value.GetType();

        protected virtual bool CheckOnlyTypeName =>
            false;
    }
}

