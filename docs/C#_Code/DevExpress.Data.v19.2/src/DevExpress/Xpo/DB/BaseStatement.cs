namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class BaseStatement : JoinNode
    {
        [XmlArrayItem(typeof(ContainsOperator)), XmlArrayItem(typeof(BetweenOperator)), XmlArrayItem(typeof(BinaryOperator)), XmlArrayItem(typeof(UnaryOperator)), XmlArrayItem(typeof(InOperator)), XmlArrayItem(typeof(GroupOperator)), XmlArrayItem(typeof(OperandValue)), XmlArrayItem(typeof(ConstantValue)), XmlArrayItem(typeof(OperandProperty)), XmlArrayItem(typeof(AggregateOperand)), XmlArrayItem(typeof(JoinOperand)), XmlArrayItem(typeof(FunctionOperator)), XmlArrayItem(typeof(NotOperator)), XmlArrayItem(typeof(NullOperator)), XmlArrayItem(typeof(QueryOperand)), XmlArrayItem(typeof(QuerySubQueryContainer))]
        public CriteriaOperatorCollection Operands;

        protected BaseStatement()
        {
            this.Operands = new CriteriaOperatorCollection();
        }

        protected BaseStatement(DBTable table, string alias) : base(table, alias, JoinType.Inner)
        {
            this.Operands = new CriteriaOperatorCollection();
        }

        internal override void CollectJoinNodesAndCriteriaInternal(List<JoinNode> nodes, List<CriteriaOperator> criteria)
        {
            base.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
            foreach (CriteriaOperator @operator in this.Operands)
            {
                criteria.Add(@operator);
                foreach (JoinNode node in SubQueriesFinder.FindSubQueries(@operator))
                {
                    node.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
                }
            }
        }

        public override bool Equals(object obj)
        {
            BaseStatement statement = obj as BaseStatement;
            return ((statement != null) ? (base.Equals(statement) && Equals(this.Operands, statement.Operands)) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        [IteratorStateMachine(typeof(<GetTablesColumns>d__8))]
        public static IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetTablesColumns(params BaseStatement[] statements)
        {
            <GetTablesColumns>d__8 d__1 = new <GetTablesColumns>d__8(-2);
            d__1.<>3__statements = statements;
            return d__1;
        }

        public string[] GetTablesNames()
        {
            BaseStatement[] statements = new BaseStatement[] { this };
            return GetTablesNames(statements);
        }

        public static string[] GetTablesNames(params BaseStatement[] statements)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (BaseStatement statement in statements)
            {
                List<JoinNode> nodes = new List<JoinNode>();
                List<CriteriaOperator> criteria = new List<CriteriaOperator>();
                statement.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
                foreach (JoinNode node in nodes)
                {
                    DBTable table = node.Table;
                    if (table != null)
                    {
                        DBProjection projection = node.Table as DBProjection;
                        if (projection == null)
                        {
                            set.Add(table.Name);
                            continue;
                        }
                        BaseStatement[] statementArray1 = new BaseStatement[] { projection.Projection };
                        foreach (string str in GetTablesNames(statementArray1))
                        {
                            set.Add(str);
                        }
                    }
                }
            }
            string[] array = new string[set.Count];
            set.CopyTo(array);
            return array;
        }

        [CompilerGenerated]
        private sealed class <GetTablesColumns>d__8 : IEnumerable<KeyValuePair<string, IEnumerable<string>>>, IEnumerable, IEnumerator<KeyValuePair<string, IEnumerable<string>>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<string, IEnumerable<string>> <>2__current;
            private int <>l__initialThreadId;
            private BaseStatement[] statements;
            public BaseStatement[] <>3__statements;
            private Dictionary<string, Dictionary<string, object>>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetTablesColumns>d__8(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                this.<>7__wrap1.Dispose();
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        Dictionary<string, Dictionary<string, object>> dictionary = new Dictionary<string, Dictionary<string, object>>();
                        List<BaseStatement> list = new List<BaseStatement>(this.statements);
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= list.Count)
                            {
                                this.<>7__wrap1 = dictionary.GetEnumerator();
                                this.<>1__state = -3;
                                break;
                            }
                            BaseStatement statement = list[num2];
                            List<JoinNode> nodes = new List<JoinNode>();
                            List<CriteriaOperator> criteria = new List<CriteriaOperator>();
                            statement.CollectJoinNodesAndCriteriaInternal(nodes, criteria);
                            Dictionary<string, DBTable> dictionary2 = new Dictionary<string, DBTable>();
                            foreach (JoinNode node in nodes)
                            {
                                DBTable table;
                                string alias = node.Alias;
                                alias ??= string.Empty;
                                if (dictionary2.TryGetValue(alias, out table))
                                {
                                    if (!Equals(table, node.Table))
                                    {
                                        throw new InvalidOperationException("different tables with the same alias -- possible internal error");
                                    }
                                    continue;
                                }
                                dictionary2.Add(alias, node.Table);
                                DBProjection projection = node.Table as DBProjection;
                                if (projection != null)
                                {
                                    list.Add(projection.Projection);
                                }
                            }
                            foreach (DBTable table2 in dictionary2.Values)
                            {
                                if (!(table2 is DBProjection) && !dictionary.ContainsKey(table2.Name))
                                {
                                    dictionary.Add(table2.Name, new Dictionary<string, object>());
                                }
                            }
                            foreach (QueryOperand operand in TopLevelQueryOperandsFinder.Find(criteria))
                            {
                                Dictionary<string, object> dictionary3;
                                string nodeAlias = operand.NodeAlias;
                                nodeAlias ??= string.Empty;
                                string name = dictionary2[nodeAlias].Name;
                                string columnName = operand.ColumnName;
                                if (!dictionary.TryGetValue(name, out dictionary3))
                                {
                                    dictionary3 = new Dictionary<string, object>();
                                    dictionary.Add(name, dictionary3);
                                }
                                dictionary3[columnName] = columnName;
                            }
                            num2++;
                        }
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = new Dictionary<string, Dictionary<string, object>>.Enumerator();
                        flag = false;
                    }
                    else
                    {
                        KeyValuePair<string, Dictionary<string, object>> current = this.<>7__wrap1.Current;
                        this.<>2__current = new KeyValuePair<string, IEnumerable<string>>(current.Key, current.Value.Keys);
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<KeyValuePair<string, IEnumerable<string>>> IEnumerable<KeyValuePair<string, IEnumerable<string>>>.GetEnumerator()
            {
                BaseStatement.<GetTablesColumns>d__8 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new BaseStatement.<GetTablesColumns>d__8(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.statements = this.<>3__statements;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Collections.Generic.IEnumerable<System.String>>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            KeyValuePair<string, IEnumerable<string>> IEnumerator<KeyValuePair<string, IEnumerable<string>>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

