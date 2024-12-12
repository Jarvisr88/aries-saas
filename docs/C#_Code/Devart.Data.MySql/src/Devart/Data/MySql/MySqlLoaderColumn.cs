namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(MySqlLoaderColumnConverter))]
    public class MySqlLoaderColumn : DbLoaderColumn
    {
        private Devart.Data.MySql.MySqlType a;

        public MySqlLoaderColumn();
        public MySqlLoaderColumn(string name, Devart.Data.MySql.MySqlType dbType);
        public MySqlLoaderColumn(string name, Devart.Data.MySql.MySqlType dbType, int size);
        public MySqlLoaderColumn(string name, Devart.Data.MySql.MySqlType dbType, int size, int precision, int scale);

        [DefaultValue(0x12)]
        public Devart.Data.MySql.MySqlType MySqlType { get; set; }

        [DefaultValue(0)]
        public override int Size { get; set; }
    }
}

