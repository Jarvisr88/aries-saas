namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpo.DB;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class SelectSqlGenerator : BaseSqlGeneratorWithParameters
    {
        private readonly BaseSqlGenerator parentGenerator;
        private readonly IList<string> propertyAliases;
        private readonly Dictionary<int, OperandValue> constantValues;
        private readonly Dictionary<int, int> operandIndexes;
        private HashSet<string> oaProperties;
        private HashSet<string> groupProperties;
        private bool isBuildGrouping;

        public SelectSqlGenerator(ISqlGeneratorFormatter formatter) : this(formatter, null, null)
        {
        }

        public SelectSqlGenerator(ISqlGeneratorFormatter formatter, BaseSqlGenerator parentGenerator, IList<string> propertyAliases) : base(formatter, new TaggedParametersHolder(), new Dictionary<OperandValue, string>())
        {
            this.constantValues = new Dictionary<int, OperandValue>();
            this.operandIndexes = new Dictionary<int, int>();
            this.parentGenerator = parentGenerator;
            this.propertyAliases = propertyAliases;
        }

        private string BuildAdditionalGroupingOuterApply()
        {
            if ((this.oaProperties == null) || ((this.groupProperties == null) || !this.TryUseOuterApply))
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.oaProperties)
            {
                if (!this.groupProperties.Contains(str))
                {
                    object[] args = new object[] { str };
                    builder.AppendFormat(CultureInfo.InvariantCulture, ",{0}", args);
                }
            }
            return builder.ToString();
        }

        private string BuildGroupCriteria() => 
            base.Process(this.Root.GroupCondition, true);

        private string BuildGrouping()
        {
            string str;
            this.isBuildGrouping = true;
            try
            {
                if (this.Root.GroupProperties.Count == 0)
                {
                    str = null;
                }
                else
                {
                    this.groupProperties = new HashSet<string>();
                    StringBuilder builder = new StringBuilder();
                    foreach (CriteriaOperator @operator in this.Root.GroupProperties)
                    {
                        string item = base.Process(@operator);
                        this.groupProperties.Add(item);
                        object[] args = new object[] { item };
                        builder.AppendFormat(CultureInfo.InvariantCulture, "{0},", args);
                    }
                    str = builder.ToString(0, builder.Length - 1);
                }
            }
            finally
            {
                this.isBuildGrouping = false;
            }
            return str;
        }

        private string BuildProperties()
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.Root.Operands.Count; i++)
            {
                CriteriaOperator @operator = this.Root.Operands[i];
                if ((@operator is OperandValue) && (this.parentGenerator == null))
                {
                    this.constantValues.Add(i, (OperandValue) @operator);
                }
                else
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(',');
                    }
                    int outerApplyResultCounter = base.OuterApplyResultCounter;
                    string item = base.Process(@operator);
                    if (base.OuterApplyResultCounter > outerApplyResultCounter)
                    {
                        this.oaProperties ??= new HashSet<string>();
                        this.oaProperties.Add(item);
                    }
                    builder.Append(this.PatchProperty(@operator, item));
                    if (this.propertyAliases != null)
                    {
                        string str2 = (num < this.propertyAliases.Count) ? this.propertyAliases[num] : null;
                        QueryOperand criterion = @operator as QueryOperand;
                        builder.Append(" as ");
                        string columnName = "";
                        columnName = !criterion.ReferenceEqualsNull() ? ((!string.IsNullOrEmpty(str2) || string.Equals(str2, criterion.ColumnName, StringComparison.InvariantCultureIgnoreCase)) ? str2 : criterion.ColumnName) : (str2 ?? ("PrP" + num.ToString(CultureInfo.InvariantCulture)));
                        builder.Append(base.formatter.FormatColumn(base.formatter.ComposeSafeColumnName(columnName)));
                    }
                    this.operandIndexes.Add(i, num++);
                }
            }
            return ((num != 0) ? builder.ToString() : "1");
        }

        private string BuildSorting()
        {
            if (this.Root.SortProperties.Count == 0)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            int num = 0;
            while (num < this.Root.SortProperties.Count)
            {
                SortingColumn column = this.Root.SortProperties[num];
                int num2 = 0;
                while (true)
                {
                    if ((num2 >= num) || this.Root.SortProperties[num2].Property.Equals(column.Property))
                    {
                        if (num2 >= num)
                        {
                            builder.Append(base.formatter.FormatOrder(base.Process(column.Property), column.Direction));
                            builder.Append(',');
                        }
                        num++;
                        break;
                    }
                    num2++;
                }
            }
            return builder.ToString(0, builder.Length - 1);
        }

        protected override Query CreateQuery(string sql, QueryParameterCollection parameters, IList parametersNames) => 
            new Query(sql, parameters, parametersNames, this.Root.SkipSelectedRecords, this.Root.TopSelectedRecords, this.constantValues, this.operandIndexes);

        public override string GetNextParameterName(OperandValue parameter) => 
            (this.parentGenerator == null) ? base.GetNextParameterName(parameter) : this.parentGenerator.GetNextParameterName(parameter);

        public static ISqlGeneratorFormatterSupportSkipTake GetSkipTakeImpl(ISqlGeneratorFormatter formatter)
        {
            ISqlGeneratorFormatterSupportSkipTake take = formatter as ISqlGeneratorFormatterSupportSkipTake;
            return (((take == null) || !take.NativeSkipTakeSupported) ? null : take);
        }

        protected override string InternalGenerateSql()
        {
            base.TakeIntoAccountOuterApplyDependencies();
            string groupBySql = this.BuildGrouping();
            string selectedPropertiesSql = this.BuildProperties();
            string sql = base.BuildJoins().ToString();
            string whereSql = base.BuildCriteria();
            string havingSql = this.BuildGroupCriteria();
            string orderBySql = this.BuildSorting();
            int skipSelectedRecords = this.Root.SkipSelectedRecords;
            int topSelectedRecords = this.Root.TopSelectedRecords;
            string str7 = base.BuildOuterApply();
            sql = base.ReplaceOuterApplyForJoinNodePlaceholders(sql) + str7;
            if (groupBySql != null)
            {
                groupBySql = groupBySql + this.BuildAdditionalGroupingOuterApply();
            }
            if (skipSelectedRecords != 0)
            {
                ISqlGeneratorFormatterSupportSkipTake skipTakeImpl = GetSkipTakeImpl(base.formatter);
                if (skipTakeImpl != null)
                {
                    return skipTakeImpl.FormatSelect(selectedPropertiesSql, sql, whereSql, orderBySql, groupBySql, havingSql, skipSelectedRecords, topSelectedRecords);
                }
                if (topSelectedRecords != 0)
                {
                    return base.formatter.FormatSelect(selectedPropertiesSql, sql, whereSql, orderBySql, groupBySql, havingSql, skipSelectedRecords + topSelectedRecords);
                }
            }
            return base.formatter.FormatSelect(selectedPropertiesSql, sql, whereSql, orderBySql, groupBySql, havingSql, topSelectedRecords);
        }

        protected virtual string PatchProperty(CriteriaOperator propertyOperator, string propertyString) => 
            propertyString;

        protected SelectStatement Root =>
            (SelectStatement) base.Root;

        protected override bool TryUseOuterApply =>
            true;

        protected override bool ForceOuterApply =>
            this.isBuildGrouping || base.ForceOuterApply;
    }
}

