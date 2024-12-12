namespace Dapper
{
    using System;
    using System.Reflection;

    public sealed class CustomPropertyTypeMap : SqlMapper.ITypeMap
    {
        private readonly Type _type;
        private readonly Func<Type, string, PropertyInfo> _propertySelector;

        public CustomPropertyTypeMap(Type type, Func<Type, string, PropertyInfo> propertySelector)
        {
            if (type == null)
            {
                Type local1 = type;
                throw new ArgumentNullException("type");
            }
            this._type = type;
            if (propertySelector == null)
            {
                Func<Type, string, PropertyInfo> local2 = propertySelector;
                throw new ArgumentNullException("propertySelector");
            }
            this._propertySelector = propertySelector;
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types) => 
            this._type.GetConstructor(new Type[0]);

        public ConstructorInfo FindExplicitConstructor() => 
            null;

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            throw new NotSupportedException();
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            PropertyInfo property = this._propertySelector(this._type, columnName);
            return ((property != null) ? new SimpleMemberMap(columnName, property) : null);
        }
    }
}

