namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    [Editor("DevExpress.XtraReports.Design.DesignBindingEditor,DevExpress.Utils.v19.2.UI, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), TypeConverter(typeof(DesignBindingConverterBase))]
    public class DesignBinding
    {
        public static DesignBinding Null;
        private object dataSource;
        private string dataMember;

        static DesignBinding();
        public DesignBinding();
        public DesignBinding(object dataSource, string dataMember);
        protected void Assign(object dataSource, string dataMember);
        public override bool Equals(object value);
        private bool Equals(object dataSource, string dataMember);
        public string GetDataSourceDisplayName(IServiceProvider serviceProvider);
        public string GetDisplayName(IServiceProvider serviceProvider);
        public override int GetHashCode();

        [Browsable(false)]
        public object DataSource { get; }

        [Browsable(false)]
        public string DataMember { get; }

        [Browsable(false)]
        public bool IsNull { get; }
    }
}

