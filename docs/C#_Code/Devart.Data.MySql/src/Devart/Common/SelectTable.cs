namespace Devart.Common
{
    using System;

    public sealed class SelectTable : SelectStatementNode
    {
        private string a;
        private string b;
        private string c;
        private string d;
        private string e;
        internal string f;
        internal string g;

        public SelectTable(string name);
        public SelectTable(string schema, string name, string alias);
        public SelectTable(string database, string schema, string name, string alias);
        internal SelectTable(string A_0, string A_1, string A_2, string A_3, string A_4, int A_5, int A_6);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public override string ToString();

        public string Database { get; set; }

        public string Schema { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string SubQuery { get; set; }

        public string JoinClause { get; set; }

        public string JoinCondition { get; set; }
    }
}

