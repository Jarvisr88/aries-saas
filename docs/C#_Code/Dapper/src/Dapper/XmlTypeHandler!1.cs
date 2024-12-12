namespace Dapper
{
    using System;
    using System.Data;

    internal abstract class XmlTypeHandler<T> : SqlMapper.StringTypeHandler<T>
    {
        protected XmlTypeHandler()
        {
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            base.SetValue(parameter, value);
            parameter.DbType = DbType.Xml;
        }
    }
}

