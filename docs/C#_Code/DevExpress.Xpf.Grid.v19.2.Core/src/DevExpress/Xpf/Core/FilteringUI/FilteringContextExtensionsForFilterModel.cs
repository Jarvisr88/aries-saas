namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class FilteringContextExtensionsForFilterModel
    {
        internal static RegisteredModelInfo<TModel> CreateAndRegisterModel<TModel>(this FilteringUIContext context, string propertyName, Func<FilterModelClient, TModel> createModel, Func<FilterSetInfo> getFilterSetInfo) where TModel: FilterModelBase
        {
            FilterModelClient client = new FilterModelClient(propertyName, new Func<string, FilteringColumn>(context.GetColumn), countsIncludeMode => context.GetUniqueValues(propertyName, countsIncludeMode == CountsIncludeMode.Include), (valuesPropertyName, filter, countsIncludeMode, filterPropertyName) => context.GetGroupUniqueValues(valuesPropertyName, countsIncludeMode == CountsIncludeMode.Include, filter, filterPropertyName), x => context.GetCounts(propertyName, x), delegate (Lazy<CriteriaOperator> x) {
                FilterSetInfo local2;
                if (getFilterSetInfo != null)
                {
                    local2 = getFilterSetInfo();
                }
                else
                {
                    Func<FilterSetInfo> local1 = getFilterSetInfo;
                    local2 = null;
                }
                FilterSetInfo info = local2;
                if (info != null)
                {
                    info.ApplyFilter(propertyName, x);
                }
                else
                {
                    context.ApplyFilter(propertyName, x.Value);
                }
            }, new Func<CriteriaOperator, CriteriaOperator>(context.SubstituteWholeFilter), () => context.GetMinMaxRange(propertyName, true), () => context.GetFormatConditionFilters(propertyName));
            if (client.GetColumn() == null)
            {
                return null;
            }
            TModel model = createModel(client);
            if (getFilterSetInfo == null)
            {
                Func<FilterSetInfo> local1 = getFilterSetInfo;
            }
            else
            {
                FilterSetInfo local2 = getFilterSetInfo();
                if (local2 == null)
                {
                    FilterSetInfo local3 = local2;
                }
                else
                {
                    local2.RegisterModel(model);
                }
            }
            Action update = delegate {
                model.UpdateEditSettings();
                model.Update();
            };
            Action<string> columnAddedRemoved = delegate (string fieldName) {
                if (fieldName == propertyName)
                {
                    update();
                }
            };
            Action dataSourceChanged = <>c__1<TModel>.<>9__1_9;
            if (<>c__1<TModel>.<>9__1_9 == null)
            {
                Action local4 = <>c__1<TModel>.<>9__1_9;
                dataSourceChanged = <>c__1<TModel>.<>9__1_9 = delegate {
                };
            }
            FilteringContextListener listener = new FilteringContextListener(delegate {
                FilteringColumn column = client.GetColumn();
                if (column != null)
                {
                    CriteriaOperator @operator = context.GetFilter(column, null);
                    model.Update(@operator);
                    if (getFilterSetInfo == null)
                    {
                        Func<FilterSetInfo> local1 = getFilterSetInfo;
                    }
                    else
                    {
                        FilterSetInfo local2 = getFilterSetInfo();
                        if (local2 == null)
                        {
                            FilterSetInfo local3 = local2;
                        }
                        else
                        {
                            local2.FilterUpdated();
                        }
                    }
                }
            }, dataSourceChanged, <>c__1<TModel>.<>9__1_10 ??= delegate {
            }, delegate {
                if (model.AllowLiveDataShaping)
                {
                    model.Update();
                }
            }, delegate {
                model.Update();
            }, columnAddedRemoved, update, columnAddedRemoved, columnAddedRemoved, columnAddedRemoved, delegate {
                model.UpdateFormatConditionFilters();
            });
            return new RegisteredModelInfo<TModel>(model, context.RegisterListener(listener));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<TModel> where TModel: FilterModelBase
        {
            public static readonly FilteringContextExtensionsForFilterModel.<>c__1<TModel> <>9;
            public static Action <>9__1_9;
            public static Action <>9__1_10;

            static <>c__1()
            {
                FilteringContextExtensionsForFilterModel.<>c__1<TModel>.<>9 = new FilteringContextExtensionsForFilterModel.<>c__1<TModel>();
            }

            internal void <CreateAndRegisterModel>b__1_10()
            {
            }

            internal void <CreateAndRegisterModel>b__1_9()
            {
            }
        }

        internal class RegisteredModelInfo<TModel> where TModel: FilterModelBase
        {
            internal readonly TModel Model;
            internal readonly UnsubscribeAction Unsubscribe;

            public RegisteredModelInfo(TModel model, UnsubscribeAction unsubscribe)
            {
                this.Model = model;
                this.Unsubscribe = unsubscribe;
            }
        }
    }
}

