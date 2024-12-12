namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public abstract class BaseSqlGenerator : IQueryCriteriaVisitor<string>, ICriteriaVisitor<string>
    {
        private const string XpoOuterApplyForJoinNodePlaceholder = "--XPOOUTERAPPLYFORJOIN_{0}_~NODE~#";
        private BaseStatement root;
        private bool hasSubQuery;
        private bool inTopLevelAggregate;
        private int outerApplyAliasCounter;
        private int outerApplyResultCounter;
        private bool isOuterApplyDependenciesTakenIntoAccount;
        private static Regex xpoOuterApplyForJoinNodePlaceholderRegEx;
        private Dictionary<string, List<string>> nodesDependentOnOuterApply;
        private Dictionary<OuterApplyCacheItem, OuterApplyInfo> outerApplyCache;
        protected readonly ISqlGeneratorFormatter formatter;
        protected readonly ISqlGeneratorFormatterEx formatterEx;
        private static string[] groupOps = new string[] { "and", "or" };

        protected BaseSqlGenerator(ISqlGeneratorFormatter formatter)
        {
            this.formatter = formatter;
            this.formatterEx = formatter as ISqlGeneratorFormatterEx;
        }

        private void AppendJoinNode(JoinNode node, StringBuilder joins)
        {
            if (this.formatter.BraceJoin)
            {
                joins.Insert(0, "(");
            }
            joins.Append("\n ");
            bool shouldProccessOuterApplyDependencies = this.GetShouldProccessOuterApplyDependencies();
            if (shouldProccessOuterApplyDependencies)
            {
                joins.AppendFormat("--XPOOUTERAPPLYFORJOIN_{0}_~NODE~#", node.Alias);
                joins.Append("\n ");
            }
            joins.Append((node.Type == JoinType.Inner) ? "inner" : "left");
            joins.Append(" join ");
            DBProjection table = node.Table as DBProjection;
            if (table == null)
            {
                joins.Append(this.formatter.FormatTable(this.formatter.ComposeSafeSchemaName(node.Table.Name), this.formatter.ComposeSafeTableName(node.Table.Name), node.Alias));
            }
            else
            {
                Func<DBColumn, string> selector = <>c.<>9__44_0;
                if (<>c.<>9__44_0 == null)
                {
                    Func<DBColumn, string> local1 = <>c.<>9__44_0;
                    selector = <>c.<>9__44_0 = c => (c == null) ? string.Empty : c.Name;
                }
                Query query = new SelectSqlGenerator(this.formatter, this, table.Columns.Select<DBColumn, string>(selector).ToList<string>()).GenerateSql(table.Projection);
                joins.Append(FormatSubQuery(query.Sql, node.Alias));
            }
            joins.Append(" on ");
            string text1 = this.Process(node.Condition, true);
            string text2 = text1;
            if (text1 == null)
            {
                string local2 = text1;
                text2 = this.Process((CriteriaOperator) (new ConstantValue(1) == new ConstantValue(1)));
            }
            joins.Append(text2);
            if (shouldProccessOuterApplyDependencies)
            {
                this.UpdateAllJoinNodeDependencies(node.Alias);
            }
            if (this.formatter.BraceJoin)
            {
                joins.Append(')');
            }
            foreach (JoinNode node2 in node.SubNodes)
            {
                this.AppendJoinNode(node2, joins);
            }
        }

        protected string BuildCriteria() => 
            this.Process(this.Root.Condition, true);

        protected StringBuilder BuildJoins()
        {
            StringBuilder joins = new StringBuilder();
            DBProjection table = this.Root.Table as DBProjection;
            if (table == null)
            {
                joins.Append(this.formatter.FormatTable(this.formatter.ComposeSafeSchemaName(this.Root.Table.Name), this.formatter.ComposeSafeTableName(this.Root.Table.Name), this.Root.Alias));
            }
            else
            {
                Func<DBColumn, string> selector = <>c.<>9__47_0;
                if (<>c.<>9__47_0 == null)
                {
                    Func<DBColumn, string> local1 = <>c.<>9__47_0;
                    selector = <>c.<>9__47_0 = c => (c == null) ? string.Empty : c.Name;
                }
                joins.Append(FormatSubQuery(new SelectSqlGenerator(this.formatter, this, table.Columns.Select<DBColumn, string>(selector).ToList<string>()).GenerateSql(table.Projection).Sql, this.Root.Alias));
            }
            if (this.GetShouldProccessOuterApplyDependencies())
            {
                this.UpdateAllJoinNodeDependencies(null);
            }
            foreach (JoinNode node in this.Root.SubNodes)
            {
                this.AppendJoinNode(node, joins);
            }
            return joins;
        }

        protected string BuildOuterApply()
        {
            if (this.TryUseOuterApply)
            {
                ISqlGeneratorFormatterSupportOuterApply outerApplyImpl = GetOuterApplyImpl(this.formatter);
                if ((outerApplyImpl != null) && (this.OuterApplyCache.Count > 0))
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (OuterApplyInfo info in this.OuterApplyCache.Values)
                    {
                        string item = outerApplyImpl.FormatOuterApply(info.GenerateOuterApplySqlBody(this, this.formatter), info.Alias);
                        if (this.IsOuterApplyDependenciesTakenIntoAccount)
                        {
                            bool flag = false;
                            foreach (string str2 in info.GetJoinNodeDependencies())
                            {
                                if (str2 != null)
                                {
                                    List<string> list;
                                    flag = true;
                                    if (!this.NodesDependentOnOuterApply.TryGetValue(str2, out list))
                                    {
                                        list = new List<string>();
                                        this.NodesDependentOnOuterApply.Add(str2, list);
                                    }
                                    list.Add(item);
                                }
                            }
                            if (flag)
                            {
                                continue;
                            }
                        }
                        builder.Append("\n");
                        builder.Append(item);
                        builder.Append(" ");
                    }
                    if (builder.Length > 0)
                    {
                        builder.Append("\n");
                    }
                    return builder.ToString();
                }
            }
            return string.Empty;
        }

        string ICriteriaVisitor<string>.Visit(BetweenOperator theOperator) => 
            this.Process(CriteriaOperator.And(new BinaryOperator(theOperator.BeginExpression, theOperator.TestExpression, BinaryOperatorType.LessOrEqual), new BinaryOperator(theOperator.TestExpression, theOperator.EndExpression, BinaryOperatorType.LessOrEqual)));

        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator)
        {
            if (theOperator.OperatorType == BinaryOperatorType.Like)
            {
                throw new InvalidOperationException("Custom function 'Like' expected instead of BinaryOperatorType.Like");
            }
            return this.formatter.FormatBinary(theOperator.OperatorType, this.Process(theOperator.LeftOperand), this.Process(theOperator.RightOperand));
        }

        string ICriteriaVisitor<string>.Visit(FunctionOperator theOperator)
        {
            int num;
            if (EvalHelpers.IsLocalDateTime(theOperator.OperatorType))
            {
                if (theOperator.Operands.Count != 0)
                {
                    throw new ArgumentException("theOperator.Operands.Count != 0");
                }
                return this.Process((CriteriaOperator) new ConstantValue(EvalHelpers.EvaluateLocalDateTime(theOperator.OperatorType)));
            }
            if (EvalHelpers.IsOutlookInterval(theOperator.OperatorType))
            {
                return this.Process(EvalHelpers.ExpandIsOutlookInterval(theOperator));
            }
            if (this.formatterEx != null)
            {
                object[] destinationArray = new object[theOperator.Operands.Count];
                Array.Copy(theOperator.Operands.ToArray(), destinationArray, destinationArray.Length);
                if ((theOperator.OperatorType == FunctionOperatorType.Custom) || (theOperator.OperatorType == FunctionOperatorType.CustomNonDeterministic))
                {
                    if ((destinationArray.Length < 1) || (!(theOperator.Operands[0] is OperandValue) || !(((OperandValue) theOperator.Operands[0]).Value is string)))
                    {
                        throw new Exception();
                    }
                    destinationArray[0] = ((OperandValue) theOperator.Operands[0]).Value;
                }
                return this.formatterEx.FormatFunction(new ProcessParameter(this.Process), theOperator.OperatorType, destinationArray);
            }
            string[] operands = new string[theOperator.Operands.Count];
            if ((theOperator.OperatorType != FunctionOperatorType.Custom) && (theOperator.OperatorType != FunctionOperatorType.CustomNonDeterministic))
            {
                num = 0;
            }
            else
            {
                num = 1;
                if ((operands.Length < 1) || (!(theOperator.Operands[0] is OperandValue) || !(((OperandValue) theOperator.Operands[0]).Value is string)))
                {
                    throw new Exception();
                }
                operands[0] = (string) ((OperandValue) theOperator.Operands[0]).Value;
            }
            while (num < theOperator.Operands.Count)
            {
                operands[num] = this.Process(theOperator.Operands[num]);
                num++;
            }
            return this.formatter.FormatFunction(theOperator.OperatorType, operands);
        }

        string ICriteriaVisitor<string>.Visit(GroupOperator theOperator)
        {
            StringCollection collection = new StringCollection();
            foreach (CriteriaOperator @operator in theOperator.Operands)
            {
                string str = this.Process(@operator);
                if (str != null)
                {
                    collection.Add(str);
                }
            }
            return ("(" + StringListHelper.DelimitedText(collection, " " + GetGroupOpName(theOperator.OperatorType) + " ") + ")");
        }

        string ICriteriaVisitor<string>.Visit(InOperator theOperator)
        {
            string str = this.Process(theOperator.LeftOperand);
            StringBuilder builder = new StringBuilder();
            foreach (CriteriaOperator @operator in theOperator.Operands)
            {
                if (builder.Length > 0)
                {
                    builder.Append(',');
                }
                builder.Append(this.Process(@operator));
            }
            object[] args = new object[] { str, builder.ToString() };
            return string.Format(CultureInfo.InvariantCulture, "{0} in ({1})", args);
        }

        string ICriteriaVisitor<string>.Visit(OperandValue theOperand) => 
            this.GetNextParameterName(theOperand);

        string ICriteriaVisitor<string>.Visit(UnaryOperator theOperator) => 
            this.formatter.FormatUnary(theOperator.OperatorType, this.Process(theOperator.Operand));

        string IQueryCriteriaVisitor<string>.Visit(QueryOperand operand) => 
            (operand.NodeAlias == null) ? this.formatter.FormatColumn(this.formatter.ComposeSafeColumnName(operand.ColumnName)) : this.formatter.FormatColumn(this.formatter.ComposeSafeColumnName(operand.ColumnName), operand.NodeAlias);

        string IQueryCriteriaVisitor<string>.Visit(QuerySubQueryContainer container)
        {
            OuterApplyInfo info;
            bool flag = container.AggregateType == Aggregate.Custom;
            if (container.Node == null)
            {
                string str;
                this.inTopLevelAggregate = true;
                int outerApplyResultCounter = this.outerApplyResultCounter;
                try
                {
                    str = flag ? SubSelectSqlGenerator.GetSelectValue(container.CustomAggregateOperands, container.CustomAggregateName, this, null) : SubSelectSqlGenerator.GetSelectValue(container.AggregateProperty, container.AggregateType, this, null);
                }
                finally
                {
                    this.outerApplyResultCounter = outerApplyResultCounter;
                    this.inTopLevelAggregate = false;
                }
                return str;
            }
            bool flag2 = container.AggregateType == Aggregate.Exists;
            this.hasSubQuery = !flag2;
            CriteriaOperatorCollection aggregateOperands = (container.Node.Operands != null) ? container.Node.Operands : new CriteriaOperatorCollection();
            CriteriaOperator nodeSingleProperty = GetNodeSingleProperty(container.Node);
            if (!this.TryUseOuterApply || (GetOuterApplyImpl(this.formatter) == null))
            {
                SubSelectSqlGenerator generator = flag ? new SubSelectSqlGenerator(this, this.formatter, aggregateOperands, container.CustomAggregateName, true) : new SubSelectSqlGenerator(this, this.formatter, nodeSingleProperty, container.AggregateType, !flag2);
                return FormatSubQuery(container.AggregateType, generator.GenerateSelect(container.Node, true));
            }
            OuterApplyCacheItem key = new OuterApplyCacheItem(container.Node, true);
            if (!this.OuterApplyCache.TryGetValue(key, out info))
            {
                SubSelectSqlGenerator generator2 = flag ? new SubSelectSqlGenerator(this, this.formatter, aggregateOperands, container.CustomAggregateName, true) : new SubSelectSqlGenerator(this, this.formatter, nodeSingleProperty, container.AggregateType, !flag2);
                string subQuery = generator2.GenerateSelect(container.Node, false);
                if ((!generator2.HasSubQuery && (!this.IsSubQuery || flag2)) && !this.ForceOuterApply)
                {
                    return FormatSubQuery(container.AggregateType, subQuery);
                }
                this.hasSubQuery = true;
                if (flag2)
                {
                    subQuery = null;
                }
                info = new OuterApplyInfo(this.GetNextOuterApplyAlias(), container.Node, subQuery);
                this.OuterApplyCache.Add(key, info);
            }
            this.outerApplyResultCounter++;
            string str2 = flag ? info.GetAggregateAlias(container.Node, container.CustomAggregateName) : info.GetAggregateAlias(container.Node, flag2 ? Aggregate.Count : container.AggregateType);
            if (!flag2)
            {
                return (info.Alias + "." + str2);
            }
            string[] textArray1 = new string[] { "(", info.Alias, ".", str2, " > 0)" };
            return string.Concat(textArray1);
        }

        private static string FormatSubQuery(Aggregate aggregateType, string subQuery)
        {
            object[] args = new object[] { subQuery, (aggregateType == Aggregate.Exists) ? "exists" : string.Empty };
            return string.Format(CultureInfo.InvariantCulture, "{1}({0})", args);
        }

        private static string FormatSubQuery(string subQuery, string alias) => 
            "(" + subQuery + ") " + alias;

        private static string GetGroupOpName(GroupOperatorType type) => 
            groupOps[(int) type];

        private string GetNextOuterApplyAlias()
        {
            int outerApplyAliasCounter = this.outerApplyAliasCounter;
            this.outerApplyAliasCounter = outerApplyAliasCounter + 1;
            return ("OA" + outerApplyAliasCounter.ToString());
        }

        public abstract string GetNextParameterName(OperandValue parameter);
        private static CriteriaOperator GetNodeSingleProperty(BaseStatement node) => 
            (node.Operands.Count > 0) ? node.Operands[0] : null;

        protected List<string> GetOuterApplyAliasList()
        {
            if (!this.TryUseOuterApply || ((GetOuterApplyImpl(this.formatter) == null) || (this.OuterApplyCache.Count <= 0)))
            {
                return null;
            }
            List<string> list = new List<string>();
            foreach (OuterApplyInfo info in this.OuterApplyCache.Values)
            {
                list.Add(info.Alias);
            }
            return list;
        }

        public static ISqlGeneratorFormatterSupportOuterApply GetOuterApplyImpl(ISqlGeneratorFormatter formatter)
        {
            ISqlGeneratorFormatterSupportOuterApply apply = formatter as ISqlGeneratorFormatterSupportOuterApply;
            return (((apply == null) || !apply.NativeOuterApplySupported) ? null : apply);
        }

        private bool GetShouldProccessOuterApplyDependencies() => 
            this.TryUseOuterApply && (this.IsOuterApplyDependenciesTakenIntoAccount && (GetOuterApplyImpl(this.formatter) != null));

        protected string Process(CriteriaOperator operand) => 
            this.Process(operand, false);

        private string Process(object operand) => 
            this.Process((CriteriaOperator) operand);

        protected string Process(CriteriaOperator operand, bool nullOnNull)
        {
            if (!operand.ReferenceEqualsNull())
            {
                return operand.Accept<string>(this);
            }
            if (!nullOnNull)
            {
                throw new InvalidOperationException("empty subcriteria");
            }
            return null;
        }

        protected string ReplaceOuterApplyForJoinNodePlaceholders(string sql)
        {
            if (!this.GetShouldProccessOuterApplyDependencies())
            {
                return sql;
            }
            if (this.NodesDependentOnOuterApply.Count == 0)
            {
                return XpoOuterApplyForJoinNodePlaceholderRegEx.Replace(sql, "");
            }
            MatchCollection matchs = XpoOuterApplyForJoinNodePlaceholderRegEx.Matches(sql);
            if (matchs.Count == 0)
            {
                return sql;
            }
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            HashSet<string> set = new HashSet<string>();
            foreach (Match match in matchs)
            {
                List<string> list;
                builder.Append(sql, startIndex, match.Index - startIndex);
                string key = match.Groups[1].Value;
                if (this.NodesDependentOnOuterApply.TryGetValue(key, out list))
                {
                    foreach (string str2 in list)
                    {
                        if (!set.Contains(str2))
                        {
                            set.Add(str2);
                            builder.Append("   ");
                            builder.Append(str2);
                            builder.Append("\n");
                        }
                    }
                    builder.Append(" ");
                }
                startIndex = match.Index + match.Length;
            }
            if (startIndex < sql.Length)
            {
                builder.Append(sql, startIndex, sql.Length - startIndex);
            }
            return builder.ToString();
        }

        protected void SetUpRootQueryStatement(BaseStatement root)
        {
            this.root = root;
        }

        protected void TakeIntoAccountOuterApplyDependencies()
        {
            this.isOuterApplyDependenciesTakenIntoAccount = true;
        }

        private void UpdateAllJoinNodeDependencies(string alias = null)
        {
            foreach (OuterApplyInfo info in this.OuterApplyCache.Values)
            {
                info.UpdateJoinNodeDependencies(alias);
            }
        }

        private static Regex XpoOuterApplyForJoinNodePlaceholderRegEx
        {
            get
            {
                xpoOuterApplyForJoinNodePlaceholderRegEx ??= new Regex(@"--XPOOUTERAPPLYFORJOIN_(.+?)_~NODE~#\n ", RegexOptions.CultureInvariant | RegexOptions.Compiled);
                return xpoOuterApplyForJoinNodePlaceholderRegEx;
            }
        }

        private Dictionary<string, List<string>> NodesDependentOnOuterApply
        {
            get
            {
                this.nodesDependentOnOuterApply ??= new Dictionary<string, List<string>>();
                return this.nodesDependentOnOuterApply;
            }
        }

        protected int OuterApplyAliasCounter =>
            this.outerApplyAliasCounter;

        protected int OuterApplyResultCounter =>
            this.outerApplyResultCounter;

        private Dictionary<OuterApplyCacheItem, OuterApplyInfo> OuterApplyCache
        {
            get
            {
                this.outerApplyCache ??= new Dictionary<OuterApplyCacheItem, OuterApplyInfo>();
                return this.outerApplyCache;
            }
        }

        protected bool IsOuterApplyDependenciesTakenIntoAccount =>
            this.isOuterApplyDependenciesTakenIntoAccount;

        protected virtual bool TryUseOuterApply =>
            false;

        protected virtual bool IsSubQuery =>
            false;

        protected virtual bool ForceOuterApply =>
            this.inTopLevelAggregate;

        protected BaseStatement Root =>
            this.root;

        internal ISqlGeneratorFormatter Formatter =>
            this.formatter;

        public bool HasSubQuery =>
            this.hasSubQuery;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseSqlGenerator.<>c <>9 = new BaseSqlGenerator.<>c();
            public static Func<DBColumn, string> <>9__44_0;
            public static Func<DBColumn, string> <>9__47_0;

            internal string <AppendJoinNode>b__44_0(DBColumn c) => 
                (c == null) ? string.Empty : c.Name;

            internal string <BuildJoins>b__47_0(DBColumn c) => 
                (c == null) ? string.Empty : c.Name;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OuterApplyAggregateInfo
        {
            public readonly BaseStatement Node;
            public readonly Aggregate Type;
            public readonly string CustomAggregateName;
            public OuterApplyAggregateInfo(BaseStatement node, Aggregate type, string customAggregateName)
            {
                if ((type == Aggregate.Custom) && string.IsNullOrEmpty(customAggregateName))
                {
                    throw new ArgumentNullException("customAggregateName");
                }
                this.Node = node;
                this.Type = type;
                this.CustomAggregateName = customAggregateName;
            }
        }

        private class OuterApplyAggregateInfoComparer : IEqualityComparer<BaseSqlGenerator.OuterApplyAggregateInfo>
        {
            public static readonly BaseSqlGenerator.OuterApplyAggregateInfoComparer Instance = new BaseSqlGenerator.OuterApplyAggregateInfoComparer();

            public bool Equals(BaseSqlGenerator.OuterApplyAggregateInfo x, BaseSqlGenerator.OuterApplyAggregateInfo y) => 
                BaseSqlGenerator.OuterApplyCacheCompareHelper.AreEquals(new BaseSqlGenerator.OuterApplyCompareCache(), x.Node, y.Node, false) && ((x.Type == y.Type) && (x.CustomAggregateName == y.CustomAggregateName));

            public int GetHashCode(BaseSqlGenerator.OuterApplyAggregateInfo obj) => 
                (BaseSqlGenerator.OuterApplyCacheCompareHelper.GetHash(obj.Node, false) ^ obj.Type.GetHashCode()) ^ ((obj.CustomAggregateName == null) ? 0 : obj.CustomAggregateName.GetHashCode());
        }

        private class OuterApplyCacheCompareHelper
        {
            private static bool AreEquals(BaseSqlGenerator.OuterApplyCompareCache compareCache, DBTable table1, DBTable table2)
            {
                if (ReferenceEquals(table1, table2))
                {
                    return true;
                }
                if ((table1 == null) || (table2 == null))
                {
                    return false;
                }
                DBProjection projection = table1 as DBProjection;
                DBProjection projection2 = table2 as DBProjection;
                if ((projection == null) && (projection2 == null))
                {
                    return table1.Equals(table2);
                }
                if ((projection == null) || ((projection2 == null) || (projection.Columns.Count != projection2.Columns.Count)))
                {
                    return false;
                }
                for (int i = 0; i < projection.Columns.Count; i++)
                {
                    DBColumn objA = projection.Columns[i];
                    DBColumn objB = projection2.Columns[i];
                    if (!ReferenceEquals(objA, objB))
                    {
                        if ((objA == null) || (objB == null))
                        {
                            return false;
                        }
                        if (objA.Name != objB.Name)
                        {
                            return false;
                        }
                    }
                }
                return AreEquals(compareCache, projection.Projection, projection2.Projection, false);
            }

            public static bool AreEquals(BaseSqlGenerator.OuterApplyCompareCache compareCache, JoinNodeCollection x, JoinNodeCollection y)
            {
                if (!ReferenceEquals(x, y))
                {
                    if ((x == null) || (y == null))
                    {
                        return false;
                    }
                    if (x.Count != y.Count)
                    {
                        return false;
                    }
                    for (int i = 0; i < x.Count; i++)
                    {
                        if (!AreEquals(compareCache, x[i], y[i], false))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            public static bool AreEquals(BaseSqlGenerator.OuterApplyCompareCache compareCache, JoinNode x, JoinNode y, bool operateAsJoinNode = false)
            {
                if (!ReferenceEquals(x, y))
                {
                    if ((x == null) || (y == null))
                    {
                        return false;
                    }
                    BaseStatement statement = x as BaseStatement;
                    BaseStatement statement2 = y as BaseStatement;
                    if (((statement == null) && (statement2 != null)) || ((statement != null) && (statement2 == null)))
                    {
                        return false;
                    }
                    SelectStatement statement3 = x as SelectStatement;
                    SelectStatement statement4 = y as SelectStatement;
                    if (((statement3 == null) && (statement4 != null)) || ((statement3 != null) && (statement4 == null)))
                    {
                        return false;
                    }
                    if (!Equals(x.Type, y.Type) || !AreEquals(compareCache, x.Table, y.Table))
                    {
                        return false;
                    }
                    string nextCacheAlias = compareCache.GetNextCacheAlias();
                    compareCache.AddNode(x.Alias, nextCacheAlias);
                    compareCache.AddNode(y.Alias, nextCacheAlias);
                    if (!AreEquals(compareCache, x.SubNodes, y.SubNodes))
                    {
                        return false;
                    }
                    JoinNodeCollection nodes = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, x.Condition);
                    if (!AreEquals(compareCache, nodes, BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, y.Condition)) || !Equals(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, x.Condition), BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, y.Condition)))
                    {
                        return false;
                    }
                    if ((statement != null) && ((statement2 != null) && !operateAsJoinNode))
                    {
                        if (statement.Operands.Count != statement2.Operands.Count)
                        {
                            return false;
                        }
                        int count = statement.Operands.Count;
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= count)
                            {
                                if ((statement3 != null) && (statement4 != null))
                                {
                                    if ((statement3.TopSelectedRecords != statement4.TopSelectedRecords) || (statement3.SkipSelectedRecords != statement4.SkipSelectedRecords))
                                    {
                                        return false;
                                    }
                                    JoinNodeCollection nodes5 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement3.GroupCondition);
                                    if (!AreEquals(compareCache, nodes5, BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement4.GroupCondition)) || !Equals(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement3.GroupCondition), BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement4.GroupCondition)))
                                    {
                                        return false;
                                    }
                                    if (statement3.GroupProperties.Count != statement4.GroupProperties.Count)
                                    {
                                        return false;
                                    }
                                    int num3 = statement3.GroupProperties.Count;
                                    int num5 = 0;
                                    while (true)
                                    {
                                        if (num5 >= num3)
                                        {
                                            if (statement3.SortProperties.Count != statement4.SortProperties.Count)
                                            {
                                                return false;
                                            }
                                            int num4 = statement3.SortProperties.Count;
                                            for (int i = 0; i < num4; i++)
                                            {
                                                JoinNodeCollection nodes9 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement3.SortProperties[i].Property);
                                                JoinNodeCollection nodes10 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement4.SortProperties[i].Property);
                                                if (!AreEquals(compareCache, nodes9, nodes10) || ((statement3.SortProperties[i].Direction != statement4.SortProperties[i].Direction) || !Equals(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement3.SortProperties[i].Property), BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement4.SortProperties[i].Property))))
                                                {
                                                    return false;
                                                }
                                            }
                                            break;
                                        }
                                        JoinNodeCollection nodes7 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement3.GroupProperties[num5]);
                                        JoinNodeCollection nodes8 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement4.GroupProperties[num5]);
                                        if (!AreEquals(compareCache, nodes7, nodes8) || !Equals(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement3.GroupProperties[num5]), BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement4.GroupProperties[num5])))
                                        {
                                            return false;
                                        }
                                        num5++;
                                    }
                                }
                                break;
                            }
                            JoinNodeCollection nodes3 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement.Operands[num2]);
                            JoinNodeCollection nodes4 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement2.Operands[num2]);
                            if (!AreEquals(compareCache, nodes3, nodes4) || !Equals(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement.Operands[num2]), BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(compareCache, statement2.Operands[num2])))
                            {
                                return false;
                            }
                            num2++;
                        }
                    }
                }
                return true;
            }

            private static int GetHash(DBProjection dbProjection)
            {
                if (dbProjection == null)
                {
                    return 0x78422058;
                }
                int num = (GetHashCode(dbProjection.Name) ^ GetHash(dbProjection.Projection, false)) ^ dbProjection.Columns.Count.GetHashCode();
                foreach (DBColumn column in dbProjection.Columns)
                {
                    if (column != null)
                    {
                        num ^= GetHashCode(column.Name);
                    }
                }
                return num;
            }

            public static int GetHash(JoinNodeCollection obj)
            {
                int num = 0x1259321;
                if (obj != null)
                {
                    foreach (JoinNode node in obj)
                    {
                        num ^= GetHash(node, false);
                    }
                }
                return num;
            }

            public static int GetHash(JoinNode obj, bool operateAsJoinNode = false)
            {
                if (obj == null)
                {
                    return HashCodeHelper.GetNullHash();
                }
                int hashState = HashCodeHelper.Calculate(obj.Type.GetHashCode(), (obj.Table is DBProjection) ? GetHash((DBProjection) obj.Table) : GetHashCode(obj.Table), GetHash(obj.SubNodes), GetHash(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(null, obj.Condition)), GetHashCode(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(null, obj.Condition)), obj.Type.GetHashCode());
                if (operateAsJoinNode)
                {
                    return hashState;
                }
                BaseStatement statement = obj as BaseStatement;
                if (statement == null)
                {
                    return hashState;
                }
                for (int i = 0; i < statement.Operands.Count; i++)
                {
                    hashState = HashCodeHelper.Combine(hashState, GetHash(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(null, statement.Operands[i])), GetHashCode(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(null, statement.Operands[i])));
                }
                SelectStatement statement2 = statement as SelectStatement;
                if (statement2 == null)
                {
                    return hashState;
                }
                if (statement2.GroupProperties != null)
                {
                    foreach (CriteriaOperator @operator in statement2.GroupProperties)
                    {
                        hashState = HashCodeHelper.Combine(hashState, GetHashCode(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(null, @operator)));
                    }
                }
                if (statement2.SortProperties != null)
                {
                    foreach (SortingColumn column in statement2.SortProperties)
                    {
                        hashState = HashCodeHelper.Combine(HashCodeHelper.Combine(hashState, GetHashCode(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(null, column.Property))), column.Direction.GetHashCode());
                    }
                }
                return HashCodeHelper.Finish(HashCodeHelper.CombineGeneric<int, int>(HashCodeHelper.Combine(hashState, GetHashCode(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Preprocess(null, statement2.GroupCondition))), statement2.SkipSelectedRecords, statement2.TopSelectedRecords));
            }

            protected static int GetHashCode(object obj) => 
                (obj == null) ? 0 : obj.GetHashCode();

            public static bool PrepareCacheForConvertion(BaseSqlGenerator.OuterApplyCompareCache compareCache, JoinNode from, JoinNode to)
            {
                if (!ReferenceEquals(from, to))
                {
                    if ((from == null) || (to == null))
                    {
                        return false;
                    }
                    BaseStatement statement = from as BaseStatement;
                    BaseStatement statement2 = to as BaseStatement;
                    if (((statement == null) && (statement2 != null)) || ((statement != null) && (statement2 == null)))
                    {
                        return false;
                    }
                    SelectStatement statement3 = from as SelectStatement;
                    SelectStatement statement4 = to as SelectStatement;
                    if (((statement3 == null) && (statement4 != null)) || ((statement3 != null) && (statement4 == null)))
                    {
                        return false;
                    }
                    if (!Equals(from.Type, to.Type) || !AreEquals(compareCache, from.Table, to.Table))
                    {
                        return false;
                    }
                    compareCache.AddNode(from.Alias, to.Alias);
                    if (!PrepareCacheForConvertion(compareCache, from.SubNodes, to.SubNodes))
                    {
                        return false;
                    }
                    JoinNodeCollection fromSubNodes = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, from.Condition);
                    if (!PrepareCacheForConvertion(compareCache, fromSubNodes, BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, to.Condition)))
                    {
                        return false;
                    }
                    if ((statement != null) && (statement2 != null))
                    {
                        if (statement.Operands.Count != statement2.Operands.Count)
                        {
                            return false;
                        }
                        int count = statement.Operands.Count;
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= count)
                            {
                                if ((statement3 != null) && (statement4 != null))
                                {
                                    if ((statement3.TopSelectedRecords != statement4.TopSelectedRecords) || (statement3.SkipSelectedRecords != statement4.SkipSelectedRecords))
                                    {
                                        return false;
                                    }
                                    JoinNodeCollection nodes5 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement3.GroupCondition);
                                    if (!PrepareCacheForConvertion(compareCache, nodes5, BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement4.GroupCondition)))
                                    {
                                        return false;
                                    }
                                    if (statement3.GroupProperties.Count != statement4.GroupProperties.Count)
                                    {
                                        return false;
                                    }
                                    int num3 = statement3.GroupProperties.Count;
                                    int num5 = 0;
                                    while (true)
                                    {
                                        if (num5 >= num3)
                                        {
                                            if (statement3.SortProperties.Count != statement4.SortProperties.Count)
                                            {
                                                return false;
                                            }
                                            int num4 = statement3.SortProperties.Count;
                                            for (int i = 0; i < num4; i++)
                                            {
                                                JoinNodeCollection nodes9 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement3.SortProperties[i].Property);
                                                JoinNodeCollection nodes10 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement4.SortProperties[i].Property);
                                                if (!PrepareCacheForConvertion(compareCache, nodes9, nodes10))
                                                {
                                                    return false;
                                                }
                                            }
                                            break;
                                        }
                                        JoinNodeCollection nodes7 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement3.GroupProperties[num5]);
                                        JoinNodeCollection nodes8 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement4.GroupProperties[num5]);
                                        if (!PrepareCacheForConvertion(compareCache, nodes7, nodes8))
                                        {
                                            return false;
                                        }
                                        num5++;
                                    }
                                }
                                break;
                            }
                            JoinNodeCollection nodes3 = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement.Operands[num2]);
                            JoinNodeCollection toSubNodes = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.CollectNodes(compareCache, statement2.Operands[num2]);
                            if (!PrepareCacheForConvertion(compareCache, nodes3, toSubNodes))
                            {
                                return false;
                            }
                            num2++;
                        }
                    }
                }
                return true;
            }

            public static bool PrepareCacheForConvertion(BaseSqlGenerator.OuterApplyCompareCache compareCache, JoinNodeCollection fromSubNodes, JoinNodeCollection toSubNodes)
            {
                if (!ReferenceEquals(fromSubNodes, toSubNodes))
                {
                    if ((fromSubNodes == null) || ((toSubNodes == null) || (fromSubNodes.Count != toSubNodes.Count)))
                    {
                        return false;
                    }
                    for (int i = 0; i < fromSubNodes.Count; i++)
                    {
                        if (!PrepareCacheForConvertion(compareCache, fromSubNodes[i], toSubNodes[i]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        private class OuterApplyCacheCriteriaPreprocessor : ClientCriteriaVisitorBase, IQueryCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
        {
            private bool collectNodes;
            private bool convertNodes;
            private BaseSqlGenerator.OuterApplyCompareCache cache;
            private JoinNodeCollection subNodes;

            private OuterApplyCacheCriteriaPreprocessor(BaseSqlGenerator.OuterApplyCompareCache compareCache, bool collectNodes, bool convertNodes)
            {
                this.cache = compareCache;
                this.collectNodes = collectNodes;
                this.convertNodes = convertNodes;
            }

            public static JoinNodeCollection CollectNodes(BaseSqlGenerator.OuterApplyCompareCache compareCache, CriteriaOperator criteria)
            {
                BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor preprocessor = new BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor(compareCache, true, false);
                preprocessor.Process(criteria);
                return preprocessor.SubNodes;
            }

            public static CriteriaOperator Convert(BaseSqlGenerator.OuterApplyCompareCache compareCache, CriteriaOperator criteria) => 
                new BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor(compareCache, false, true).Process(criteria);

            public static CriteriaOperator Preprocess(BaseSqlGenerator.OuterApplyCompareCache compareCache, CriteriaOperator criteria) => 
                new BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor(compareCache, false, false).Process(criteria);

            private BaseStatement Process(BaseStatement node)
            {
                if (node == null)
                {
                    return null;
                }
                SelectStatement srcNode = node as SelectStatement;
                if (srcNode == null)
                {
                    throw new InvalidOperationException();
                }
                SelectStatement destNode = new SelectStatement();
                bool flag = this.Process(srcNode, destNode);
                foreach (CriteriaOperator @operator in srcNode.Operands)
                {
                    CriteriaOperator objA = base.Process(@operator);
                    if (ReferenceEquals(objA, @operator))
                    {
                        destNode.Operands.Add(@operator);
                        continue;
                    }
                    destNode.Operands.Add(objA);
                    flag = true;
                }
                destNode.GroupCondition = base.Process(srcNode.GroupCondition);
                if (!ReferenceEquals(destNode.GroupCondition, srcNode.GroupCondition))
                {
                    flag = true;
                }
                foreach (CriteriaOperator operator3 in srcNode.GroupProperties)
                {
                    CriteriaOperator objA = base.Process(operator3);
                    if (ReferenceEquals(objA, operator3))
                    {
                        destNode.GroupProperties.Add(operator3);
                        continue;
                    }
                    destNode.GroupProperties.Add(objA);
                    flag = true;
                }
                destNode.SkipSelectedRecords = srcNode.SkipSelectedRecords;
                destNode.TopSelectedRecords = srcNode.TopSelectedRecords;
                return (flag ? destNode : srcNode);
            }

            private JoinNode Process(JoinNode srcNode)
            {
                if (srcNode == null)
                {
                    return null;
                }
                JoinNode destNode = new JoinNode();
                return (!this.Process(srcNode, destNode) ? srcNode : destNode);
            }

            private bool Process(JoinNode srcNode, JoinNode destNode)
            {
                destNode.Table = srcNode.Table;
                destNode.Alias = this.cache.GetCacheAliasName(srcNode.Alias);
                destNode.Condition = base.Process(srcNode.Condition);
                destNode.Type = srcNode.Type;
                bool flag = false;
                foreach (JoinNode node in srcNode.SubNodes)
                {
                    JoinNode objA = this.Process(node);
                    if (ReferenceEquals(objA, node))
                    {
                        destNode.SubNodes.Add(node);
                        continue;
                    }
                    destNode.SubNodes.Add(objA);
                    flag = true;
                }
                return (flag || ((destNode.Alias != srcNode.Alias) || (!ReferenceEquals(destNode.Condition, srcNode.Condition) || (destNode.Type != srcNode.Type))));
            }

            public CriteriaOperator Visit(QueryOperand theOperand)
            {
                if (this.collectNodes)
                {
                    return null;
                }
                if (this.cache == null)
                {
                    return new QueryOperand(theOperand.ColumnName, string.Empty, theOperand.ColumnType);
                }
                string cacheAliasName = this.cache.GetCacheAliasName(theOperand.NodeAlias);
                return ((cacheAliasName != theOperand.NodeAlias) ? new QueryOperand(theOperand.ColumnName, cacheAliasName, theOperand.ColumnType) : theOperand);
            }

            public CriteriaOperator Visit(QuerySubQueryContainer theOperand)
            {
                bool flag;
                if (this.collectNodes)
                {
                    this.subNodes ??= new JoinNodeCollection();
                    this.subNodes.Add(theOperand.Node);
                }
                if (this.collectNodes)
                {
                    return null;
                }
                if (theOperand.AggregateType != Aggregate.Custom)
                {
                    CriteriaOperator aggregateProperty = base.Process(theOperand.AggregateProperty);
                    if (!this.convertNodes)
                    {
                        return new QuerySubQueryContainer(null, aggregateProperty, theOperand.AggregateType);
                    }
                    BaseStatement statement = this.Process(theOperand.Node);
                    return ((!ReferenceEquals(statement, theOperand.Node) || !ReferenceEquals(aggregateProperty, theOperand.AggregateProperty)) ? new QuerySubQueryContainer(statement, aggregateProperty, theOperand.AggregateType) : theOperand);
                }
                CriteriaOperatorCollection aggregateOperands = base.ProcessCollection(theOperand.CustomAggregateOperands, out flag);
                if (!this.convertNodes)
                {
                    return new QuerySubQueryContainer(null, aggregateOperands, theOperand.CustomAggregateName);
                }
                BaseStatement objA = this.Process(theOperand.Node);
                return ((flag || !ReferenceEquals(objA, theOperand.Node)) ? new QuerySubQueryContainer(objA, aggregateOperands, theOperand.CustomAggregateName) : theOperand);
            }

            public JoinNodeCollection SubNodes =>
                this.subNodes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OuterApplyCacheItem
        {
            public readonly bool OperateAsJoinNode;
            public readonly JoinNode Node;
            private int hash;
            public OuterApplyCacheItem(JoinNode node, bool operateAsJoinNode)
            {
                this.Node = node;
                this.OperateAsJoinNode = operateAsJoinNode;
                this.hash = BaseSqlGenerator.OuterApplyCacheCompareHelper.GetHash(node, operateAsJoinNode);
            }

            public override int GetHashCode() => 
                this.hash;

            public override bool Equals(object obj) => 
                (obj != null) && ((obj is BaseSqlGenerator.OuterApplyCacheItem) && BaseSqlGenerator.OuterApplyCacheCompareHelper.AreEquals(new BaseSqlGenerator.OuterApplyCompareCache(), this.Node, ((BaseSqlGenerator.OuterApplyCacheItem) obj).Node, this.OperateAsJoinNode));
        }

        private class OuterApplyCompareCache
        {
            private int nodeAliasCounter;
            private Dictionary<string, string> nodeAliasCache = new Dictionary<string, string>();

            public void AddNode(string nodeAlias, string nodeCacheAlias)
            {
                this.nodeAliasCache[nodeAlias] = nodeCacheAlias;
            }

            public string GetCacheAliasName(string alias)
            {
                string str;
                return (this.nodeAliasCache.TryGetValue(alias, out str) ? str : alias);
            }

            public string GetNextCacheAlias()
            {
                int nodeAliasCounter = this.nodeAliasCounter;
                this.nodeAliasCounter = nodeAliasCounter + 1;
                return $"NCA{nodeAliasCounter}";
            }
        }

        private class OuterApplyInfo
        {
            private readonly Dictionary<BaseSqlGenerator.OuterApplyAggregateInfo, string> aggregates = new Dictionary<BaseSqlGenerator.OuterApplyAggregateInfo, string>(BaseSqlGenerator.OuterApplyAggregateInfoComparer.Instance);
            private readonly Dictionary<string, string> joinNodeDependencies = new Dictionary<string, string>();
            public readonly string FirstSqlBody;
            public readonly string Alias;
            public readonly BaseStatement TargetNode;

            public OuterApplyInfo(string alias, BaseStatement targetNode, string firstSqlBody)
            {
                this.Alias = alias;
                this.TargetNode = targetNode;
                this.FirstSqlBody = firstSqlBody;
            }

            public string GenerateOuterApplySqlBody(BaseSqlGenerator parentGenerator, ISqlGeneratorFormatter formatter) => 
                ((this.aggregates.Count >= 2) || string.IsNullOrEmpty(this.FirstSqlBody)) ? new SubSelectSqlGenerator(parentGenerator, formatter, this.aggregates.Select<KeyValuePair<BaseSqlGenerator.OuterApplyAggregateInfo, string>, SubSelectAggregateInfo>(delegate (KeyValuePair<BaseSqlGenerator.OuterApplyAggregateInfo, string> p) {
                    BaseSqlGenerator.OuterApplyAggregateInfo key = p.Key;
                    if (key.Type != Aggregate.Custom)
                    {
                        CriteriaOperator nodeSingleProperty = BaseSqlGenerator.GetNodeSingleProperty(key.Node);
                        if (!nodeSingleProperty.ReferenceEqualsNull())
                        {
                            BaseSqlGenerator.OuterApplyCompareCache compareCache = new BaseSqlGenerator.OuterApplyCompareCache();
                            BaseSqlGenerator.OuterApplyCacheCompareHelper.PrepareCacheForConvertion(compareCache, key.Node, this.TargetNode);
                            nodeSingleProperty = BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Convert(compareCache, nodeSingleProperty);
                        }
                        return new SubSelectAggregateInfo(nodeSingleProperty, key.Type, p.Value);
                    }
                    List<CriteriaOperator> operands = new List<CriteriaOperator>();
                    if (key.Node != null)
                    {
                        foreach (CriteriaOperator operator2 in key.Node.Operands)
                        {
                            BaseSqlGenerator.OuterApplyCompareCache compareCache = new BaseSqlGenerator.OuterApplyCompareCache();
                            BaseSqlGenerator.OuterApplyCacheCompareHelper.PrepareCacheForConvertion(compareCache, key.Node, this.TargetNode);
                            operands.Add(BaseSqlGenerator.OuterApplyCacheCriteriaPreprocessor.Convert(compareCache, operator2));
                        }
                    }
                    return new SubSelectAggregateInfo(operands, key.CustomAggregateName, p.Value);
                }), true).GenerateSelect(this.TargetNode, false) : this.FirstSqlBody;

            public string GetAggregateAlias(BaseStatement aggregateNode, Aggregate aggregateType) => 
                this.GetAggregateAlias(aggregateNode, aggregateType, null);

            public string GetAggregateAlias(BaseStatement aggregateNode, string customAggregateName) => 
                this.GetAggregateAlias(aggregateNode, Aggregate.Custom, customAggregateName);

            private string GetAggregateAlias(BaseStatement aggregateNode, Aggregate aggregateType, string customAggregateName)
            {
                string str;
                if ((aggregateType == Aggregate.Custom) && string.IsNullOrEmpty(customAggregateName))
                {
                    throw new ArgumentNullException("customAggregateName");
                }
                BaseSqlGenerator.OuterApplyAggregateInfo key = new BaseSqlGenerator.OuterApplyAggregateInfo(aggregateNode, aggregateType, customAggregateName);
                if (!this.aggregates.TryGetValue(key, out str))
                {
                    str = "Res" + this.aggregates.Count;
                    this.aggregates.Add(key, str);
                }
                return str;
            }

            public IEnumerable<string> GetJoinNodeDependencies() => 
                this.joinNodeDependencies.Values;

            public void UpdateJoinNodeDependencies(string joinNodeAlias)
            {
                foreach (BaseSqlGenerator.OuterApplyAggregateInfo info in this.aggregates.Keys)
                {
                    string alias = info.Node.Alias;
                    if (!this.joinNodeDependencies.ContainsKey(alias))
                    {
                        this.joinNodeDependencies.Add(alias, joinNodeAlias);
                    }
                }
            }
        }
    }
}

