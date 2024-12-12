namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DataControllerData : IDataControllerData2, IDataControllerData
    {
        public const string ToStringColumn = "LookUpEditBaseToStringColumn";
        public const string ValueColumn = "ValueColumn";
        public const string DisplayColumn = "DisplayColumn";

        public DataControllerData(DevExpress.Data.DataController dataController, IItemsProviderOwner owner)
        {
            this.Owner = owner;
            this.DataController = dataController;
        }

        UnboundColumnInfoCollection IDataControllerData.GetUnboundColumns() => 
            null;

        object IDataControllerData.GetUnboundData(int listSourceRow, DataColumnInfo column, object value) => 
            null;

        void IDataControllerData.SetUnboundData(int listSourceRow, DataColumnInfo column, object value)
        {
        }

        ComplexColumnInfoCollection IDataControllerData2.GetComplexColumns() => 
            null;

        bool? IDataControllerData2.IsRowFit(int listSourceRow, bool fit) => 
            null;

        PropertyDescriptorCollection IDataControllerData2.PatchPropertyDescriptorCollection(PropertyDescriptorCollection collection)
        {
            List<PropertyDescriptor> source = new List<PropertyDescriptor>(collection.Cast<PropertyDescriptor>());
            this.ValueColumnDescriptor = this.GetValueDescriptor();
            int num = source.FindIndex(propertyDescriptor => propertyDescriptor.Name == this.ValueColumnDescriptor.Name);
            if (num > -1)
            {
                source[num] = this.ValueColumnDescriptor;
            }
            else
            {
                source.Add(this.ValueColumnDescriptor);
            }
            this.DisplayColumnDescriptor = this.GetDisplayDescriptor();
            int num2 = source.FindIndex(propertyDescriptor => propertyDescriptor.Name == this.DisplayColumnDescriptor.Name);
            if (num2 > -1)
            {
                source[num2] = this.DisplayColumnDescriptor;
            }
            else
            {
                source.Add(this.DisplayColumnDescriptor);
            }
            return new PropertyDescriptorCollection(source.ToArray<PropertyDescriptor>());
        }

        void IDataControllerData2.SubstituteFilter(SubstituteFilterEventArgs args)
        {
        }

        private LookUpPropertyDescriptorBase GetDisplayDescriptor()
        {
            string internalPath = string.IsNullOrEmpty(this.Owner.DisplayMember) ? "DisplayColumn" : this.Owner.DisplayMember;
            LookUpPropertyDescriptorBase displayColumnDescriptor = this.DisplayColumnDescriptor;
            return (((displayColumnDescriptor == null) || !displayColumnDescriptor.IsRelevant(internalPath)) ? new LookUpPropertyDescriptor(LookUpPropertyDescriptorType.Display, internalPath, this.Owner.DisplayMember) : displayColumnDescriptor);
        }

        private LookUpPropertyDescriptorBase GetValueDescriptor()
        {
            string internalPath = string.IsNullOrEmpty(this.Owner.ValueMember) ? "ValueColumn" : this.Owner.ValueMember;
            LookUpPropertyDescriptorBase valueColumnDescriptor = this.ValueColumnDescriptor;
            return (((valueColumnDescriptor == null) || !valueColumnDescriptor.IsRelevant(internalPath)) ? new LookUpPropertyDescriptor(LookUpPropertyDescriptorType.Value, internalPath, this.Owner.ValueMember) : valueColumnDescriptor);
        }

        public void ResetDescriptors()
        {
            if (this.ValueColumnDescriptor != null)
            {
                this.ValueColumnDescriptor.Reset();
            }
            if (this.DisplayColumnDescriptor != null)
            {
                this.DisplayColumnDescriptor.Reset();
            }
        }

        public LookUpPropertyDescriptorBase ValueColumnDescriptor { get; private set; }

        public LookUpPropertyDescriptorBase DisplayColumnDescriptor { get; private set; }

        public string ValueColumnName =>
            this.ValueColumnDescriptor.Name;

        public string DisplayColumnName =>
            this.DisplayColumnDescriptor.Name;

        private IItemsProviderOwner Owner { get; set; }

        private DevExpress.Data.DataController DataController { get; set; }

        bool IDataControllerData2.CanUseFastProperties =>
            false;

        bool IDataControllerData2.HasUserFilter =>
            false;

        private class FilterEvaluatorContext : EvaluatorContextDescriptor
        {
            private readonly object value;

            public override IEnumerable GetCollectionContexts(object source, string collectionName) => 
                null;

            public override EvaluatorContext GetNestedContext(object source, string propertyPath) => 
                null;

            public override object GetPropertyValue(object source, EvaluatorProperty propertyPath) => 
                this.value;

            public override IEnumerable GetQueryContexts(object source, string queryTypeName, CriteriaOperator condition, int top) => 
                null;
        }
    }
}

