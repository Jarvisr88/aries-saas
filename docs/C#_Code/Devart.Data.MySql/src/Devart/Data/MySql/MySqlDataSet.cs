namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.Runtime.Serialization;

    [DesignTimeVisible(true), ToolboxItem(typeof(MySqlDataSetToolboxItem)), aa("DbDataSet_Description"), Designer("Devart.Data.MySql.Design.MySqlDataSetDesigner, Devart.Data.MySql.Design"), DesignerSerializer("Devart.Data.MySql.Design.MySqlDataSetSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
    public class MySqlDataSet : DbDataSet
    {
        private bool shouldSerializeConnection;

        public MySqlDataSet();
        public MySqlDataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema);
        public override DbDataTable CreateDataTable();
        private bool ShouldSerializeConnection();

        [MergableProperty(false), Editor("Devart.Data.MySql.Design.MySqlDataSetTablesEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public DataTableCollection Tables { get; }

        [Editor("Devart.Common.Design.DbDataSetTablesEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), MergableProperty(false)]
        public DataRelationCollection Relations { get; }

        [TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), Category("Data"), RefreshProperties(RefreshProperties.Repaint), MergableProperty(false), Editor("Devart.Data.MySql.Design.MySqlConnectionEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), aa("DbDataSet_Connection")]
        public MySqlConnection Connection { get; set; }
    }
}

