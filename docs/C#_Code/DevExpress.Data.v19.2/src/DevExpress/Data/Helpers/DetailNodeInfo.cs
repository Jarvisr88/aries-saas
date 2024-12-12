namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class DetailNodeInfo
    {
        private string caption;
        private object list;
        private DataColumnInfo[] columns;
        private DetailNodeInfo[] nodes;

        public DetailNodeInfo(string caption);
        public DetailNodeInfo Find(string name);
        public static DetailNodeInfo Find(DetailNodeInfo[] nodes, string name);

        public string Caption { get; set; }

        public DataColumnInfo[] Columns { get; set; }

        public DetailNodeInfo[] Nodes { get; set; }

        public bool HasChildren { get; }

        public object List { get; set; }
    }
}

