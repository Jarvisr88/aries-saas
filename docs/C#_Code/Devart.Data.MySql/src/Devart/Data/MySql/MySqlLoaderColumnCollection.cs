namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.Reflection;

    [ListBindable(false)]
    public class MySqlLoaderColumnCollection : DbLoaderColumnCollection
    {
        public int Add(string name, MySqlType dbType, int size, int precision, int scale);

        public MySqlLoaderColumn this[int index] { get; set; }

        public MySqlLoaderColumn this[string name] { get; set; }
    }
}

