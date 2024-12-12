namespace Dapper
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class SimpleMemberMap : SqlMapper.IMemberMap
    {
        public SimpleMemberMap(string columnName, FieldInfo field)
        {
            if (columnName == null)
            {
                string local1 = columnName;
                throw new ArgumentNullException("columnName");
            }
            this.<ColumnName>k__BackingField = columnName;
            if (field == null)
            {
                FieldInfo local2 = field;
                throw new ArgumentNullException("field");
            }
            this.<Field>k__BackingField = field;
        }

        public SimpleMemberMap(string columnName, ParameterInfo parameter)
        {
            if (columnName == null)
            {
                string local1 = columnName;
                throw new ArgumentNullException("columnName");
            }
            this.<ColumnName>k__BackingField = columnName;
            if (parameter == null)
            {
                ParameterInfo local2 = parameter;
                throw new ArgumentNullException("parameter");
            }
            this.<Parameter>k__BackingField = parameter;
        }

        public SimpleMemberMap(string columnName, PropertyInfo property)
        {
            if (columnName == null)
            {
                string local1 = columnName;
                throw new ArgumentNullException("columnName");
            }
            this.<ColumnName>k__BackingField = columnName;
            if (property == null)
            {
                PropertyInfo local2 = property;
                throw new ArgumentNullException("property");
            }
            this.<Property>k__BackingField = property;
        }

        public string ColumnName { get; }

        public Type MemberType
        {
            get
            {
                Type fieldType;
                FieldInfo field = this.Field;
                if (field != null)
                {
                    fieldType = field.FieldType;
                }
                else
                {
                    FieldInfo local1 = field;
                    fieldType = null;
                }
                Type local2 = fieldType;
                Type parameterType = local2;
                if (local2 == null)
                {
                    Type propertyType;
                    Type local3 = local2;
                    PropertyInfo property = this.Property;
                    if (property != null)
                    {
                        propertyType = property.PropertyType;
                    }
                    else
                    {
                        PropertyInfo local4 = property;
                        propertyType = null;
                    }
                    Type local5 = propertyType;
                    parameterType = local5;
                    if (local5 == null)
                    {
                        Type local6 = local5;
                        ParameterInfo parameter = this.Parameter;
                        if (parameter == null)
                        {
                            ParameterInfo local7 = parameter;
                            return null;
                        }
                        parameterType = parameter.ParameterType;
                    }
                }
                return parameterType;
            }
        }

        public PropertyInfo Property { get; }

        public FieldInfo Field { get; }

        public ParameterInfo Parameter { get; }
    }
}

