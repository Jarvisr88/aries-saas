namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;

    [TypeConverter(typeof(MySqlParameterConverter))]
    public class MySqlParameter : DbParameterBase, IDbDataParameter, ICloneable
    {
        private Devart.Data.MySql.MySqlType a;
        private byte b;

        public MySqlParameter();
        public MySqlParameter(string parameterName, Devart.Data.MySql.MySqlType dbType);
        public MySqlParameter(string parameterName, object value);
        public MySqlParameter(string parameterName, Devart.Data.MySql.MySqlType dbType, int size);
        public MySqlParameter(string parameterName, Devart.Data.MySql.MySqlType dbType, int size, string sourceColumn);
        public MySqlParameter(string parameterName, Devart.Data.MySql.MySqlType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value);
        public MySqlParameter(string parameterName, Devart.Data.MySql.MySqlType dbType, int size, ParameterDirection direction, bool isNullable, bool sourceColumnNullMapping, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value);
        internal object a();
        public MySqlParameter Clone();
        public override void ResetDbType();
        internal void ResetMySqlType();
        object ICloneable.Clone();
        public override string ToString();

        [DefaultValue(0x10), aa("DbDataParameter_DbType"), Browsable(false)]
        public override System.Data.DbType DbType { get; set; }

        [DbProviderSpecificTypeProperty(true), aa("MySqlParameter_MySqlType"), RefreshProperties(RefreshProperties.Repaint), Category("Data"), DefaultValue(0x12)]
        public Devart.Data.MySql.MySqlType MySqlType { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), aa("DbDataParameter_Precision"), Browsable(false)]
        public byte Precision { get; set; }

        [Category("Data"), aa("DbDataParameter_Scale"), DefaultValue(0)]
        public byte Scale { get; set; }

        [Browsable(false)]
        public object MySqlValue { get; set; }
    }
}

