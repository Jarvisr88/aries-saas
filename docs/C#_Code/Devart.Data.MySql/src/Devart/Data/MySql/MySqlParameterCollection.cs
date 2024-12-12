namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    [Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner", "System.Drawing.Design.UITypeEditor, System.Drawing"), ListBindable(false)]
    public class MySqlParameterCollection : DbParameterBaseCollection, IList
    {
        private MySqlCommand a;

        internal MySqlParameterCollection(MySqlCommand A_0);
        public MySqlParameter Add(MySqlParameter value);
        public MySqlParameter Add(string parameterName, MySqlType type);
        public MySqlParameter Add(string parameterName, object value);
        public MySqlParameter Add(string parameterName, MySqlType dbType, int size);
        public MySqlParameter Add(string parameterName, MySqlType dbType, int size, string sourceColumn);
        public MySqlParameter AddWithValue(string parameterName, object value);
        protected override void OnChange();
        protected override void ValidateType(object value);

        public MySqlParameter this[int index] { get; set; }

        public MySqlParameter this[string parameterName] { get; set; }

        protected override Type ItemType { get; }

        protected override DbCommandBase Parent { get; }
    }
}

