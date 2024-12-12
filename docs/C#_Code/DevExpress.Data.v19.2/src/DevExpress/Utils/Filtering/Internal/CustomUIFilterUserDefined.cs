namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class CustomUIFilterUserDefined : CustomUIFilter
    {
        private IEnumerable<IUserDefinedFilterItem> filterItems;
        private IEnumerable<ICustomUIFilterValue> children;

        internal CustomUIFilterUserDefined(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            if (!this.HasFilterItems)
            {
                return false;
            }
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__4_0;
                get = <>c.<>9__4_0 = opt => opt.ShowUserDefinedFilters;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowPredefinedFilters.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new UserDefinedCriteriaParser(this);

        protected sealed override ICustomUIFilterValue CreateValue(object[] values)
        {
            ICustomUIFilterValuesFactory valuesFactory = base.GetService<ICustomUIFilterValuesFactory>();
            this.children ??= (from item in this.filterItems select valuesFactory.Create(CustomUIFilterType.User, new object[] { item })).ToArray<ICustomUIFilterValue>();
            object[] objArray1 = new object[] { values, this.children };
            return valuesFactory.Create(base.id, objArray1);
        }

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            if ((filterValue == null) || (filterValue.Value == null))
            {
                return null;
            }
            Func<IUserDefinedFilterItem, CriteriaOperator> selector = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<IUserDefinedFilterItem, CriteriaOperator> local1 = <>c.<>9__5_0;
                selector = <>c.<>9__5_0 = item => item.Criteria;
            }
            return CriteriaOperator.Or((filterValue.Value as IEnumerable<IUserDefinedFilterItem>).Select<IUserDefinedFilterItem, CriteriaOperator>(selector));
        }

        private IUserDefinedFilterItem[] GetCustomFunctions(string path, Type type, IEnumerable<string> customFunctions)
        {
            List<IUserDefinedFilterItem> list = new List<IUserDefinedFilterItem>();
            foreach (string str in customFunctions)
            {
                ICustomFunctionOperator customFunction = CriteriaOperator.GetCustomFunction(str);
                list.Add(new CustomFunctionOperatorFilterItem(customFunction, path));
            }
            return list.ToArray();
        }

        protected sealed override ICustomUIFilterValue GetCustomUIFilterDialogViewModelParameter(ICustomUIFilter activeFilter) => 
            base.Value ?? this.CreateValue(null);

        internal static bool Match(CustomUIFilterType filterType) => 
            filterType == CustomUIFilterType.User;

        protected sealed override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            object[] objArray1 = new object[2];
            object[] objArray2 = new object[2];
            objArray2[0] = viewModel.Parameter?.Value;
            object[] values = objArray2;
            values[1] = this.children;
            viewModel.SetResult(values);
        }

        internal void SetFilterItems(IEnumerable<IUserDefinedFilterItem> filterItems, string path, Type type, IEnumerable<string> customFunctions)
        {
            this.children = null;
            IEnumerable<string> enumerable1 = customFunctions;
            if (customFunctions == null)
            {
                IEnumerable<string> local1 = customFunctions;
                enumerable1 = new string[0];
            }
            IUserDefinedFilterItem[] second = this.GetCustomFunctions(path, type, enumerable1);
            if (filterItems == null)
            {
                this.filterItems = second;
            }
            else
            {
                this.filterItems = filterItems.Concat<IUserDefinedFilterItem>(second);
            }
        }

        private bool HasFilterItems =>
            (this.filterItems != null) && this.filterItems.Any<IUserDefinedFilterItem>();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterUserDefined.<>c <>9 = new CustomUIFilterUserDefined.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__4_0;
            public static Func<IUserDefinedFilterItem, CriteriaOperator> <>9__5_0;

            internal bool <AllowCore>b__4_0(ICustomUIFiltersOptions opt) => 
                opt.ShowUserDefinedFilters;

            internal CriteriaOperator <GetCriteria>b__5_0(IUserDefinedFilterItem item) => 
                item.Criteria;
        }

        private sealed class CustomFunctionOperatorFilterItem : IUserDefinedFilterItem
        {
            private readonly ICustomFunctionOperator @operator;
            private readonly FunctionOperator criteria;

            public CustomFunctionOperatorFilterItem(ICustomFunctionOperator @operator, string path)
            {
                this.@operator = @operator;
                CriteriaOperator[] operands = new CriteriaOperator[] { new ConstantValue(@operator.Name), new OperandProperty(path) };
                this.criteria = new FunctionOperator(FunctionOperatorType.Custom, operands);
            }

            string IUserDefinedFilterItem.Name
            {
                get
                {
                    ICustomFunctionDisplayAttributes @operator = this.@operator as ICustomFunctionDisplayAttributes;
                    if (@operator != null)
                    {
                        return @operator.DisplayName;
                    }
                    ICustomFunctionOperatorBrowsable browsable = this.@operator as ICustomFunctionOperatorBrowsable;
                    return ((browsable == null) ? this.@operator.Name : browsable.Description);
                }
            }

            CriteriaOperator IUserDefinedFilterItem.Criteria =>
                this.criteria;
        }

        private sealed class UserDefinedCriteriaParser : ICustomUIFilterCriteriaParser
        {
            private readonly CustomUIFilterUserDefined owner;

            internal UserDefinedCriteriaParser(CustomUIFilterUserDefined owner)
            {
                this.owner = owner;
            }

            bool ICustomUIFilterCriteriaParser.TryParse(CriteriaOperator criteria, out object[] values)
            {
                values = null;
                if (!this.owner.HasFilterItems)
                {
                    return false;
                }
                values = this.GetMatches(criteria);
                return ((values != null) && (values.Length != 0));
            }

            private IUserDefinedFilterItem[] GetMatches(CriteriaOperator criteria)
            {
                List<IUserDefinedFilterItem> list = new List<IUserDefinedFilterItem>();
                string str = criteria.LegacyToString();
                foreach (IUserDefinedFilterItem item in this.owner.filterItems)
                {
                    string str2 = item.Criteria.LegacyToString();
                    int index = str.IndexOf(str2, StringComparison.Ordinal);
                    if (index != -1)
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            break;
                        }
                        list.Add(item);
                        str = str.Remove(index, str2.Length);
                    }
                }
                return (string.IsNullOrEmpty(str.Replace(" Or ", string.Empty)) ? list.ToArray() : null);
            }
        }
    }
}

