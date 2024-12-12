namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    [ListBindable(false)]
    public sealed class SelectColumnCollection : CollectionBase, IList
    {
        private SelectStatement a;
        private SelectStatementCollection b;
        private readonly bool c;

        internal SelectColumnCollection(SelectStatement A_0, string A_1, bool A_2)
        {
            this.b = new SelectStatementCollection(this, A_1);
            this.c = A_2;
            this.a = A_0;
        }

        internal void a(ICollection A_0)
        {
            base.InnerList.AddRange(A_0);
        }

        private string a(string A_0) => 
            this.a.QuoteStart + A_0 + this.a.QuoteEnd;

        internal static void a(IList A_0, IList A_1, int A_2)
        {
            SelectStatementNode node = new SelectStatementNode();
            int count = A_0.Count;
            int num2 = A_0.Count - 1;
            while (true)
            {
                if (num2 > -1)
                {
                    if (!((SelectStatementNode) A_0[num2]).Binded)
                    {
                        num2--;
                        continue;
                    }
                    count = num2;
                }
                if ((A_2 + 1) < count)
                {
                    node.a = ((SelectStatementNode) A_0[A_2]).a;
                    node.b = ((SelectStatementNode) A_0[A_2 + 1]).a;
                }
                else if (A_2 > 0)
                {
                    node.a = ((SelectStatementNode) A_0[A_2 - 1]).b;
                    node.b = ((SelectStatementNode) A_0[A_2]).b;
                }
                else
                {
                    node.a = ((SelectStatementNode) A_0[A_2]).a;
                    node.b = ((SelectStatementNode) A_0[A_2]).b;
                }
                A_1.Add(node);
                return;
            }
        }

        public int Add(SelectColumn value) => 
            base.List.Add(value);

        public SelectColumn Add(string name)
        {
            SelectColumn column = new SelectColumn(name);
            this.Add(column);
            return column;
        }

        public SelectColumn Add(string name, bool quote)
        {
            SelectColumn column = new SelectColumn(quote ? this.a(name) : name);
            this.Add(column);
            return column;
        }

        public SelectColumn Add(string schema, string table, string name, string alias)
        {
            SelectColumn column = new SelectColumn(schema, table, name, alias);
            this.Add(column);
            return column;
        }

        public SelectColumn Add(string schema, string table, string name, string alias, bool quote)
        {
            SelectColumn column = new SelectColumn(quote ? this.a(schema) : schema, quote ? this.a(table) : table, quote ? this.a(name) : name, quote ? this.a(alias) : alias);
            this.Add(column);
            return column;
        }

        public SelectColumn Add(string database, string schema, string table, string name, string alias)
        {
            SelectColumn column = new SelectColumn(database, schema, table, name, alias);
            this.Add(column);
            return column;
        }

        public SelectColumn Add(string database, string schema, string table, string name, string alias, bool quote)
        {
            SelectColumn column = new SelectColumn(quote ? this.a(database) : database, quote ? this.a(schema) : schema, quote ? this.a(table) : table, quote ? this.a(name) : name, quote ? this.a(alias) : alias);
            this.Add(column);
            return column;
        }

        public bool Contains(SelectColumn value) => 
            base.List.Contains(value);

        public void CopyTo(SelectColumn[] array, int index)
        {
            base.List.CopyTo(array, index);
        }

        public int IndexOf(SelectColumn value) => 
            base.List.IndexOf(value);

        public void Insert(int index, SelectColumn value)
        {
            base.List.Insert(index, value);
        }

        protected override void OnClear()
        {
            while (base.Count > 0)
            {
                base.RemoveAt(0);
            }
        }

        protected override void OnRemove(int index, object value)
        {
            if (base.Count == 1)
            {
                this.CollectionNode.Node.g();
            }
            this.CollectionNode.AddDeleted(index);
        }

        public void Remove(SelectColumn value)
        {
            base.RemoveAt(this.IndexOf(value));
        }

        public SelectColumn this[int index]
        {
            get => 
                (SelectColumn) base.List[index];
            set
            {
                SelectColumn column = (SelectColumn) base.List[index];
                if (column.a != -1)
                {
                    value.a = column.a;
                    value.b = column.b;
                    value.g();
                }
                base.List[index] = value;
            }
        }

        internal SelectStatementCollection CollectionNode =>
            this.b;
    }
}

