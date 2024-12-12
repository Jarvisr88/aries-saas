namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class SubSelectSqlGenerator : BaseSqlGenerator
    {
        private readonly BaseSqlGenerator parentGenerator;
        private readonly IEnumerable<SubSelectAggregateInfo> aggregateProperties;
        private readonly bool forceOuterApply;
        private static readonly string[] agg = new string[6];

        static SubSelectSqlGenerator()
        {
            agg[2] = "max({0})";
            agg[3] = "min({0})";
            agg[4] = "avg({0})";
            agg[1] = "count({0})";
            agg[5] = "sum({0})";
            agg[0] = "{0}";
        }

        public SubSelectSqlGenerator(BaseSqlGenerator parentGenerator, ISqlGeneratorFormatter formatter, CriteriaOperator aggregateProperty, Aggregate aggregate) : this(parentGenerator, formatter, aggregateProperty, aggregate, false)
        {
        }

        public SubSelectSqlGenerator(BaseSqlGenerator parentGenerator, ISqlGeneratorFormatter formatter, IEnumerable<CriteriaOperator> aggregateOperands, string customAggregateName) : this(parentGenerator, formatter, aggregateOperands, customAggregateName, false)
        {
        }

        public SubSelectSqlGenerator(BaseSqlGenerator parentGenerator, ISqlGeneratorFormatter formatter, IEnumerable<SubSelectAggregateInfo> aggregateProperties, bool forceOuterApply) : base(formatter)
        {
            this.parentGenerator = parentGenerator;
            this.aggregateProperties = aggregateProperties;
            this.forceOuterApply = forceOuterApply;
        }

        public SubSelectSqlGenerator(BaseSqlGenerator parentGenerator, ISqlGeneratorFormatter formatter, CriteriaOperator aggregateProperty, Aggregate aggregate, bool forceOuterApply) : this(parentGenerator, formatter, infoArray1, forceOuterApply)
        {
            SubSelectAggregateInfo[] infoArray1 = new SubSelectAggregateInfo[] { new SubSelectAggregateInfo(aggregateProperty, aggregate, "Res0") };
        }

        public SubSelectSqlGenerator(BaseSqlGenerator parentGenerator, ISqlGeneratorFormatter formatter, IEnumerable<CriteriaOperator> aggregateOperands, string customAggregateName, bool forceOuterApply) : this(parentGenerator, formatter, infoArray1, forceOuterApply)
        {
            SubSelectAggregateInfo[] infoArray1 = new SubSelectAggregateInfo[] { new SubSelectAggregateInfo(aggregateOperands, customAggregateName, "Res0") };
        }

        public string GenerateSelect(BaseStatement node, bool subSelectUseOnly)
        {
            base.TakeIntoAccountOuterApplyDependencies();
            base.SetUpRootQueryStatement(node);
            StringBuilder builder = base.BuildJoins();
            string str = base.BuildCriteria();
            string selectValue = this.GetSelectValue(subSelectUseOnly);
            string str3 = base.BuildOuterApply();
            if (!string.IsNullOrEmpty(str3))
            {
                builder.Append(str3);
            }
            if (str != null)
            {
                object[] objArray1 = new object[] { str };
                builder.AppendFormat(CultureInfo.InvariantCulture, " where {0}", objArray1);
            }
            object[] args = new object[] { selectValue, base.ReplaceOuterApplyForJoinNodePlaceholders(builder.ToString()) };
            return string.Format(CultureInfo.InvariantCulture, "select {0} from {1}", args);
        }

        public override string GetNextParameterName(OperandValue parameter) => 
            this.parentGenerator.GetNextParameterName(parameter);

        private string GetSelectValue(bool subSelectUseOnly)
        {
            StringBuilder builder = new StringBuilder();
            foreach (SubSelectAggregateInfo info in this.aggregateProperties)
            {
                if (builder.Length > 0)
                {
                    builder.Append(", ");
                }
                string str = (info.AggregateType == Aggregate.Custom) ? GetSelectValue(info.CustomAggregateOperands, info.CustomAggregateName, this, subSelectUseOnly ? null : info.Alias) : GetSelectValue(info.Property, info.AggregateType, this, subSelectUseOnly ? null : info.Alias);
                builder.Append(str);
            }
            return builder.ToString();
        }

        public static string GetSelectValue(CriteriaOperator aggregateProperty, Aggregate aggregate, BaseSqlGenerator generator, string alias)
        {
            if (aggregate == Aggregate.Custom)
            {
                throw new ArgumentException("aggregate");
            }
            string str = aggregateProperty.ReferenceEqualsNull() ? "*" : aggregateProperty.Accept<string>(generator);
            string str2 = string.Format(agg[(int) aggregate], str);
            return (((aggregate == Aggregate.Exists) || string.IsNullOrEmpty(alias)) ? str2 : (str2 + " as " + alias));
        }

        public static string GetSelectValue(IEnumerable<CriteriaOperator> aggregateOperands, string customAggregateName, BaseSqlGenerator generator, string alias)
        {
            ICustomAggregateFormattable customAggregateFormattable;
            if (string.IsNullOrEmpty(customAggregateName))
            {
                throw new ArgumentNullException("customAggregateName");
            }
            ConnectionProviderSql formatter = generator.Formatter as ConnectionProviderSql;
            if (formatter == null)
            {
                customAggregateFormattable = CustomAggregatesHelper.GetCustomAggregateFormattable(customAggregateName);
            }
            else
            {
                customAggregateFormattable = formatter.GetCustomAggregate(customAggregateName);
                if (customAggregateFormattable == null)
                {
                    CustomAggregatesHelper.ThrowAggregateNotFound(customAggregateName);
                }
                if (customAggregateFormattable == null)
                {
                    CustomAggregatesHelper.ThrowCustomAggregateNotFormattable(customAggregateName);
                }
            }
            List<string> list = new List<string>();
            if (aggregateOperands != null)
            {
                foreach (CriteriaOperator @operator in aggregateOperands)
                {
                    list.Add(@operator.Accept<string>(generator));
                }
            }
            string str = customAggregateFormattable.Format(generator.Formatter.GetType(), list.ToArray());
            return (!string.IsNullOrEmpty(alias) ? (str + " as " + alias) : str);
        }

        protected override bool TryUseOuterApply =>
            true;

        protected override bool IsSubQuery =>
            true;

        protected override bool ForceOuterApply =>
            this.forceOuterApply || base.ForceOuterApply;
    }
}

