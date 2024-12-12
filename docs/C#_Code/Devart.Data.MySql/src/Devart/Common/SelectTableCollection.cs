namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    [ListBindable(false)]
    public sealed class SelectTableCollection : CollectionBase, IList
    {
        private SelectStatementCollection a;
        private SelectStatement b;

        internal SelectTableCollection(SelectStatement A_0, string A_1);
        internal void a(ICollection A_0);
        private string a(string A_0);
        public int Add(SelectTable value);
        public SelectTable Add(string name);
        public SelectTable Add(string name, bool quoted);
        public SelectTable Add(string schema, string name, string alias);
        public SelectTable Add(string schema, string name, string alias, bool quote);
        public SelectTable Add(string database, string schema, string name, string alias);
        public SelectTable Add(string database, string schema, string name, string alias, bool quote);
        public bool Contains(SelectTable value);
        public void CopyTo(SelectTable[] array, int index);
        public int IndexOf(SelectTable value);
        public void Insert(int index, SelectTable value);
        protected override void OnClear();
        protected override void OnRemove(int index, object value);
        public void Remove(SelectTable value);

        public SelectTable this[int index] { get; set; }

        internal SelectStatementCollection CollectionNode { get; }
    }
}

