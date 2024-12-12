namespace DevExpress.Data
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    [TypeConverter(typeof(CalculatedColumnTypeConverter))]
    public class CalculatedColumn : ICalculatedField
    {
        internal ComparativeSource owner;
        private string nameCore;
        private string displayName;
        private string expression;
        private DevExpress.XtraReports.UI.FieldType fieldTypeCore;

        public CalculatedColumn();
        public CalculatedColumn(string name, string displayName, string expression, DevExpress.XtraReports.UI.FieldType fieldType);

        [XtraSerializableProperty]
        public string Name { get; set; }

        [XtraSerializableProperty]
        public string DisplayName { get; set; }

        [XtraSerializableProperty, Editor("DevExpress.Utils.Design.CalculatedColumnExpressionEditor, DevExpress.Design.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public string Expression { get; set; }

        [XtraSerializableProperty]
        public DevExpress.XtraReports.UI.FieldType FieldType { get; set; }

        [Browsable(false)]
        object ICalculatedField.DataSource { get; }

        [Browsable(false)]
        string ICalculatedField.DataMember { get; }
    }
}

