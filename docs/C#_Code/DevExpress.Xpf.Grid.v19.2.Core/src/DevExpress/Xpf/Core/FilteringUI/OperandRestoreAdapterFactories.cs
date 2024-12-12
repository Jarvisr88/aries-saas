namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class OperandRestoreAdapterFactories
    {
        public static OperandRestoreAdapterFactory Binary(CriteriaConverter<ValueData> converter) => 
            Binary(client => new BinaryFilterModel(client, null, converter, null));

        public static OperandRestoreAdapterFactory Binary(Func<FilterModelClient, BinaryFilterModel> createModel) => 
            delegate (FilterModelClient client) {
                BinaryFilterModel model = createModel(client);
                return new FilterModelOperandRestoreAdapter(model, () => !model.ValueItem.IsEmpty ? OperandValuesRecord.CreateBinary(model.CreateConverterValue()) : OperandValuesRecord.CreateEmpty(), delegate (OperandValuesRecord record) {
                    model.Update(model.BuildFilter(record.Binary));
                });
            };

        public static OperandRestoreAdapterFactory DateTimeOperators() => 
            <>c.<>9__9_0 ??= delegate (FilterModelClient client) {
                DatePeriodsFilterModel model = new DatePeriodsFilterModel(client);
                return new FilterModelOperandRestoreAdapter(model, delegate {
                    bool flag1;
                    List<object> selectedPredefinedFilters = model.SelectedPredefinedFilters;
                    if (selectedPredefinedFilters != null)
                    {
                        flag1 = selectedPredefinedFilters.Count != 0;
                    }
                    else
                    {
                        List<object> local1 = selectedPredefinedFilters;
                        flag1 = true;
                    }
                    return flag1 ? OperandValuesRecord.CreateDateTimeOperators(model.BuildFilter()) : OperandValuesRecord.CreateEmpty();
                }, delegate (OperandValuesRecord record) {
                    if (record.DateTimeOperatorsFilter != null)
                    {
                        model.Update(record.DateTimeOperatorsFilter);
                    }
                    else
                    {
                        model.Update(null);
                    }
                });
            };

        public static OperandRestoreAdapterFactory Default(Func<FilterModelClient, FilterModelBase> createModel) => 
            delegate (FilterModelClient client) {
                FilterModelBase model = createModel(client);
                Func<OperandValuesRecord> save = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Func<OperandValuesRecord> local1 = <>c.<>9__4_1;
                    save = <>c.<>9__4_1 = () => OperandValuesRecord.CreateEmpty();
                }
                return new FilterModelOperandRestoreAdapter(model, save, delegate (OperandValuesRecord _) {
                    model.Update(null);
                });
            };

        public static OperandRestoreAdapterFactory FormatConditions() => 
            <>c.<>9__7_0 ??= delegate (FilterModelClient client) {
                FormatConditionFilterModel model = new FormatConditionFilterModel(client);
                return new FilterModelOperandRestoreAdapter(model, () => ((model.SelectedFilters == null) || !model.SelectedFilters.Any<object>()) ? OperandValuesRecord.CreateEmpty() : OperandValuesRecord.CreateFormatCondition(model.BuildFilter()), delegate (OperandValuesRecord record) {
                    if (record.FormatConditionFilter == null)
                    {
                        model.Update(null);
                    }
                    else
                    {
                        model.EnsureFilters();
                        model.Update(record.FormatConditionFilter);
                    }
                });
            };

        public static OperandRestoreAdapterFactory Predefined() => 
            <>c.<>9__8_0 ??= delegate (FilterModelClient client) {
                PredefinedFiltersModel model = new PredefinedFiltersModel(client);
                return new FilterModelOperandRestoreAdapter(model, () => ((model.SelectedFilters == null) || !model.SelectedFilters.Any<object>()) ? OperandValuesRecord.CreateEmpty() : OperandValuesRecord.CreateCustom(model.BuildFilter()), delegate (OperandValuesRecord record) {
                    if (record.CustomFilter == null)
                    {
                        model.Update(null);
                    }
                    else
                    {
                        model.EnsureFilters();
                        model.Update(record.CustomFilter);
                    }
                });
            };

        public static OperandRestoreAdapterFactory PredefinedFormatCondition(PredefinedFormatConditionType type) => 
            delegate (FilterModelClient client) {
                PredefinedFormatConditionFilterModel model = new PredefinedFormatConditionFilterModel(client, new PredefinedFormatConditionType?(type));
                return new FilterModelOperandRestoreAdapter(model, () => PredefinedFormatConditionFilterModel.IsConstant(new PredefinedFormatConditionType?(type)) ? OperandValuesRecord.CreateEmpty() : OperandValuesRecord.CreatePredefinedFormatCondition(model), delegate (OperandValuesRecord record) {
                    if (record.PredefinedFormatConditionFilter != null)
                    {
                        model.Update(model.BuildFilter(record.PredefinedFormatConditionFilter.Item1, record.PredefinedFormatConditionFilter.Item2));
                    }
                    else
                    {
                        model.Update(null);
                    }
                });
            };

        public static OperandRestoreAdapterFactory Ternary(CriteriaConverter<Tuple<ValueData, ValueData>> converter) => 
            Ternary(client => new TernaryFilterModel(client, null, converter, null, null));

        public static OperandRestoreAdapterFactory Ternary(Func<FilterModelClient, TernaryFilterModel> createModel) => 
            delegate (FilterModelClient client) {
                TernaryFilterModel model = createModel(client);
                return new FilterModelOperandRestoreAdapter(model, () => (model.LeftValueItem.IsEmpty || model.RightValueItem.IsEmpty) ? OperandValuesRecord.CreateEmpty() : OperandValuesRecord.CreateTernary(model.CreateConverterValue()), delegate (OperandValuesRecord record) {
                    model.Update(model.BuildFilter(record.Ternary));
                });
            };

        public static OperandRestoreAdapterFactory Variadic(Func<FilterModelClient, VariadicFilterModel> createModel) => 
            delegate (FilterModelClient client) {
                VariadicFilterModel model = createModel(client);
                return new FilterModelOperandRestoreAdapter(model, delegate {
                    Func<FilterModelValueItem, bool> predicate = <>c.<>9__6_2;
                    if (<>c.<>9__6_2 == null)
                    {
                        Func<FilterModelValueItem, bool> local1 = <>c.<>9__6_2;
                        predicate = <>c.<>9__6_2 = x => !x.IsEmpty;
                    }
                    return model.Items.Any<FilterModelValueItem>(predicate) ? OperandValuesRecord.CreateVariable(model.CreateConverterValue()) : OperandValuesRecord.CreateEmpty();
                }, delegate (OperandValuesRecord record) {
                    model.Update(model.BuildFilter(record.Variable));
                });
            };

        public static OperandRestoreAdapterFactory Variadic(CriteriaConverter<ValueData[]> converter, OperandListObserver<FilterModelBase> operandListObserver = null) => 
            Variadic(client => new VariadicFilterModel(client, null, converter, operandListObserver, null, null));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OperandRestoreAdapterFactories.<>c <>9 = new OperandRestoreAdapterFactories.<>c();
            public static Func<OperandValuesRecord> <>9__4_1;
            public static Func<FilterModelValueItem, bool> <>9__6_2;
            public static OperandRestoreAdapterFactory <>9__7_0;
            public static OperandRestoreAdapterFactory <>9__8_0;
            public static OperandRestoreAdapterFactory <>9__9_0;

            internal FilterModelOperandRestoreAdapter <DateTimeOperators>b__9_0(FilterModelClient client)
            {
                DatePeriodsFilterModel model = new DatePeriodsFilterModel(client);
                return new FilterModelOperandRestoreAdapter(model, delegate {
                    bool flag1;
                    List<object> selectedPredefinedFilters = model.SelectedPredefinedFilters;
                    if (selectedPredefinedFilters != null)
                    {
                        flag1 = selectedPredefinedFilters.Count != 0;
                    }
                    else
                    {
                        List<object> local1 = selectedPredefinedFilters;
                        flag1 = true;
                    }
                    return flag1 ? OperandValuesRecord.CreateDateTimeOperators(model.BuildFilter()) : OperandValuesRecord.CreateEmpty();
                }, delegate (OperandValuesRecord record) {
                    if (record.DateTimeOperatorsFilter != null)
                    {
                        model.Update(record.DateTimeOperatorsFilter);
                    }
                    else
                    {
                        model.Update(null);
                    }
                });
            }

            internal OperandValuesRecord <Default>b__4_1() => 
                OperandValuesRecord.CreateEmpty();

            internal FilterModelOperandRestoreAdapter <FormatConditions>b__7_0(FilterModelClient client)
            {
                FormatConditionFilterModel model = new FormatConditionFilterModel(client);
                return new FilterModelOperandRestoreAdapter(model, () => ((model.SelectedFilters == null) || !model.SelectedFilters.Any<object>()) ? OperandValuesRecord.CreateEmpty() : OperandValuesRecord.CreateFormatCondition(model.BuildFilter()), delegate (OperandValuesRecord record) {
                    if (record.FormatConditionFilter == null)
                    {
                        model.Update(null);
                    }
                    else
                    {
                        model.EnsureFilters();
                        model.Update(record.FormatConditionFilter);
                    }
                });
            }

            internal FilterModelOperandRestoreAdapter <Predefined>b__8_0(FilterModelClient client)
            {
                PredefinedFiltersModel model = new PredefinedFiltersModel(client);
                return new FilterModelOperandRestoreAdapter(model, () => ((model.SelectedFilters == null) || !model.SelectedFilters.Any<object>()) ? OperandValuesRecord.CreateEmpty() : OperandValuesRecord.CreateCustom(model.BuildFilter()), delegate (OperandValuesRecord record) {
                    if (record.CustomFilter == null)
                    {
                        model.Update(null);
                    }
                    else
                    {
                        model.EnsureFilters();
                        model.Update(record.CustomFilter);
                    }
                });
            }

            internal bool <Variadic>b__6_2(FilterModelValueItem x) => 
                !x.IsEmpty;
        }
    }
}

