namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using DevExpress.Data.Browsing;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.Native.Data;
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class LookUpHelper
    {
        private static void AssignSorting(DataController dataController, DynamicListLookUpSettings dynamicListLookUpSettings)
        {
            string sortingMember = GetSortingMember(dynamicListLookUpSettings);
            if (!string.IsNullOrEmpty(sortingMember) && (dynamicListLookUpSettings.SortOrder != ColumnSortOrder.None))
            {
                DataColumnInfo columnInfo = dataController.Columns[sortingMember];
                if (columnInfo != null)
                {
                    DataColumnSortInfo info2 = new DataColumnSortInfo(columnInfo, dynamicListLookUpSettings.SortOrder);
                    DataColumnSortInfo[] sortInfo = new DataColumnSortInfo[] { info2 };
                    dataController.UpdateSortGroup(sortInfo, 1, new SummarySortInfo[0]);
                }
            }
        }

        private static IListController GetController(DataBrowser dataBrowser)
        {
            ListBrowser browser = dataBrowser as ListBrowser;
            return browser?.ListController;
        }

        private static DataController GetDataController(IListController listController)
        {
            IDataControllerProvider provider = listController as IDataControllerProvider;
            return provider?.DataController;
        }

        private static LookUpValueCollection GetDynamicLookUpValues(DynamicListLookUpSettings dynamicListLookUpSettings, DataContext dataContext, IParameterEditorValueProvider parameterValueProvider)
        {
            if (string.IsNullOrEmpty(dynamicListLookUpSettings.ValueMember))
            {
                return null;
            }
            dataContext.Clear();
            string destinationMember = string.IsNullOrEmpty(dynamicListLookUpSettings.DisplayMember) ? dynamicListLookUpSettings.ValueMember : dynamicListLookUpSettings.DisplayMember;
            string memberPath = GetMemberPath(dynamicListLookUpSettings.DataMember, destinationMember);
            DataBrowser browser = dataContext[dynamicListLookUpSettings.DataSource, memberPath];
            string str3 = GetMemberPath(dynamicListLookUpSettings.DataMember, dynamicListLookUpSettings.ValueMember);
            DataBrowser browser2 = dataContext[dynamicListLookUpSettings.DataSource, str3];
            string dataMember = (dynamicListLookUpSettings.DataMember == null) ? "" : dynamicListLookUpSettings.DataMember;
            DataBrowser dataBrowser = dataContext.GetDataBrowser(dynamicListLookUpSettings.DataSource, dataMember, true);
            if (dataBrowser == null)
            {
                return null;
            }
            IListController listController = GetController(dataBrowser);
            if (listController is ICalculatedFieldsApplicator)
            {
                ((ICalculatedFieldsApplicator) listController).ApplyCalculatedFields();
            }
            if (listController is IFilteredListController)
            {
                ((IFilteredListController) listController).FilterCriteria = GetFilterCriteria(dynamicListLookUpSettings, parameterValueProvider);
            }
            DataController dataController = GetDataController(listController);
            if (dataController != null)
            {
                AssignSorting(dataController, dynamicListLookUpSettings);
            }
            List<LookUpValue> source = new List<LookUpValue>();
            for (int i = 0; i < dataBrowser.Count; i++)
            {
                dataBrowser.Position = i;
                if ((browser2.Current != null) && (browser2.Current != DBNull.Value))
                {
                    object current = browser.Current;
                    LookUpValue item = new LookUpValue();
                    item.Value = browser2.Current;
                    item.Description = current?.ToString();
                    source.Add(item);
                }
            }
            LookUpValueCollection values = new LookUpValueCollection();
            values.AddRange(source.Distinct<LookUpValue>(new LookUpValueValueComparer()));
            return values;
        }

        private static CriteriaOperator GetFilterCriteria(LookUpSettings lookUpSettings, IParameterEditorValueProvider parameterValueProvider)
        {
            CriteriaOperator criteriaOperator = CriteriaOperator.Parse(lookUpSettings.FilterString, new object[0]);
            if (criteriaOperator == null)
            {
                return null;
            }
            CriteriaOperator operator2 = ProcessCriteriaOperator(lookUpSettings, criteriaOperator);
            CascadingParametersValueSetter.Process(operator2, lookUpSettings.Parameter.Owner, parameterValueProvider);
            return operator2;
        }

        public static LookUpValueCollection GetLookUpValues(LookUpSettings lookUpSettings, DataContext dataContext) => 
            GetLookUpValues(lookUpSettings, dataContext, null);

        public static LookUpValueCollection GetLookUpValues(LookUpSettings lookUpSettings, DataContext dataContext, IParameterEditorValueProvider parameterValueProvider)
        {
            StaticListLookUpSettings staticListLookUpSettings = lookUpSettings as StaticListLookUpSettings;
            if (staticListLookUpSettings != null)
            {
                return GetStaticLookUpValues(staticListLookUpSettings, parameterValueProvider);
            }
            DynamicListLookUpSettings dynamicListLookUpSettings = lookUpSettings as DynamicListLookUpSettings;
            return ((dynamicListLookUpSettings == null) ? null : GetDynamicLookUpValues(dynamicListLookUpSettings, dataContext, parameterValueProvider));
        }

        private static string GetMemberPath(string dataMember, string destinationMember) => 
            string.IsNullOrEmpty(dataMember) ? destinationMember : (dataMember + "." + destinationMember);

        private static string GetSortingMember(DynamicListLookUpSettings dynamicListLookUpSettings) => 
            string.IsNullOrEmpty(dynamicListLookUpSettings.SortMember) ? (string.IsNullOrEmpty(dynamicListLookUpSettings.DisplayMember) ? (!string.IsNullOrEmpty(dynamicListLookUpSettings.ValueMember) ? dynamicListLookUpSettings.ValueMember : null) : dynamicListLookUpSettings.DisplayMember) : dynamicListLookUpSettings.SortMember;

        private static LookUpValueCollection GetStaticLookUpValues(StaticListLookUpSettings staticListLookUpSettings, IParameterEditorValueProvider parameterValueProvider)
        {
            if (string.IsNullOrEmpty(staticListLookUpSettings.FilterString))
            {
                return staticListLookUpSettings.LookUpValues;
            }
            CriteriaOperator filterCriteria = GetFilterCriteria(staticListLookUpSettings, parameterValueProvider);
            ListSourceDataController controller1 = new ListSourceDataController();
            controller1.ListSource = staticListLookUpSettings.LookUpValues;
            controller1.FilterCriteria = filterCriteria;
            ListSourceDataController controller = controller1;
            controller.DoRefresh();
            LookUpValueCollection values = new LookUpValueCollection();
            Func<LookUpValue, LookUpValue> selector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<LookUpValue, LookUpValue> local1 = <>c.<>9__2_0;
                selector = <>c.<>9__2_0 = x => x.Clone();
            }
            values.AddRange(controller.GetAllFilteredAndSortedRows().Cast<LookUpValue>().Select<LookUpValue, LookUpValue>(selector));
            return values;
        }

        public static object GetUpdatedMultiValueParameterValue(object oldValueObject, IList<LookUpValue> lookUps)
        {
            Guard.ArgumentNotNull(lookUps, "lookUps");
            IEnumerable enumerable = oldValueObject as IEnumerable;
            if (enumerable != null)
            {
                ArrayList list = new ArrayList();
                foreach (object item in enumerable)
                {
                    if (lookUps.Any<LookUpValue>(x => Equals(x.Value, item)))
                    {
                        list.Add(item);
                    }
                }
                if (list.Count > 0)
                {
                    return list.ToArray(list[0].GetType());
                }
            }
            return ((lookUps.Count == 0) ? null : Array.CreateInstance(lookUps[0].Value.GetType(), 0));
        }

        public static object GetUpdatedSingleValueParameterValue(object oldValue, IList<LookUpValue> lookUps)
        {
            Guard.ArgumentNotNull(lookUps, "lookUps");
            return (!lookUps.Any<LookUpValue>(x => Equals(x.Value, oldValue)) ? ((lookUps.Count == 0) ? null : lookUps[0].Value) : oldValue);
        }

        private static CriteriaOperator ProcessCriteriaOperator(IDataContainerBase dataContainer, CriteriaOperator criteriaOperator)
        {
            using (DataContext context = new XRDataContextBase(Enumerable.Empty<ICalculatedField>(), true))
            {
                DeserializationFilterStringVisitor visitor = new DeserializationFilterStringVisitor(null, context, dataContainer.DataSource, dataContainer.DataMember);
                return criteriaOperator.Accept<CriteriaOperator>(visitor);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LookUpHelper.<>c <>9 = new LookUpHelper.<>c();
            public static Func<LookUpValue, LookUpValue> <>9__2_0;

            internal LookUpValue <GetStaticLookUpValues>b__2_0(LookUpValue x) => 
                x.Clone();
        }
    }
}

