namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.Serialization;

    [Serializable, ToolboxItem(typeof(MySqlDataSetToolboxItem)), ToolboxItemFilter("Devart.Data.MySql.MySqlDataSet", ToolboxItemFilterType.Allow)]
    internal class MySqlDataSetToolboxItem : DataSetToolboxItem
    {
        public MySqlDataSetToolboxItem();
        private MySqlDataSetToolboxItem(SerializationInfo A_0, StreamingContext A_1);
        public MySqlDataSetToolboxItem(Type A_0, Bitmap A_1);
        protected override DataSet d();

        protected override string ProviderPrefix { get; }

        protected override string ProviderName { get; }

        protected override string ProviderRegKey { get; }

        protected override string Devart.Common.DataSetToolboxItem.ProviderPrefix { get; }

        protected override string Devart.Common.DataSetToolboxItem.ProviderName { get; }

        protected override string Devart.Common.DataSetToolboxItem.ProviderRegKey { get; }
    }
}

