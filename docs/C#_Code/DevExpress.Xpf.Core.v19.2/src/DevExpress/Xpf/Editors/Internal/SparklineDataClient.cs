namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class SparklineDataClient : IDataControllerData2, IDataControllerData
    {
        private const string ArgumentColumn = "ArgumentColumn";
        private const string ValueColumn = "ValueColumn";
        private SparklinePropertyDescriptorBase valueColumnDescriptor;
        private SparklinePropertyDescriptorBase argumentColumnDescriptor;

        public SparklineDataClient(IItemsSourceSupport itemsSourceSettings)
        {
            this.ItemsSourceSettings = itemsSourceSettings;
        }

        private void AddDescriptor(List<PropertyDescriptor> descriptors, PropertyDescriptor newDescriptor)
        {
            int num = descriptors.FindIndex(propertyDescriptor => propertyDescriptor.Name == newDescriptor.Name);
            if (num > -1)
            {
                descriptors[num] = newDescriptor;
            }
            else
            {
                descriptors.Add(newDescriptor);
            }
        }

        private SparklinePropertyDescriptor CreateSparklinePropertyDescriptor(string path, string internalPath) => 
            new SparklinePropertyDescriptor(path, internalPath);

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
            List<PropertyDescriptor> descriptors = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor descriptor in collection)
            {
                descriptors.Add(descriptor);
            }
            this.EnsureArgumentDescriptor();
            this.EnsureValueDescriptor();
            this.AddDescriptor(descriptors, this.argumentColumnDescriptor);
            this.AddDescriptor(descriptors, this.valueColumnDescriptor);
            return new PropertyDescriptorCollection(descriptors.ToArray());
        }

        void IDataControllerData2.SubstituteFilter(SubstituteFilterEventArgs args)
        {
        }

        private void EnsureArgumentDescriptor()
        {
            string internalPath = string.IsNullOrEmpty(this.ItemsSourceSettings.PointArgumentMember) ? "ArgumentColumn" : this.ItemsSourceSettings.PointArgumentMember;
            if ((this.argumentColumnDescriptor == null) || !this.argumentColumnDescriptor.IsRelevant(internalPath))
            {
                this.argumentColumnDescriptor = this.CreateSparklinePropertyDescriptor(internalPath, this.ItemsSourceSettings.PointArgumentMember);
            }
        }

        private void EnsureValueDescriptor()
        {
            string internalPath = string.IsNullOrEmpty(this.ItemsSourceSettings.PointValueMember) ? "ValueColumn" : this.ItemsSourceSettings.PointValueMember;
            if ((this.valueColumnDescriptor == null) || !this.valueColumnDescriptor.IsRelevant(internalPath))
            {
                this.valueColumnDescriptor = this.CreateSparklinePropertyDescriptor(internalPath, this.ItemsSourceSettings.PointValueMember);
            }
        }

        public void ResetDescriptors()
        {
            if (this.argumentColumnDescriptor != null)
            {
                this.argumentColumnDescriptor.Reset();
            }
            if (this.valueColumnDescriptor != null)
            {
                this.valueColumnDescriptor.Reset();
            }
        }

        private IItemsSourceSupport ItemsSourceSettings { get; set; }

        public SparklinePropertyDescriptorBase ArgumentColumnDescriptor
        {
            get
            {
                if (this.argumentColumnDescriptor == null)
                {
                    string path = string.IsNullOrEmpty(this.ItemsSourceSettings.PointValueMember) ? "ArgumentColumn" : this.ItemsSourceSettings.PointArgumentMember;
                    this.argumentColumnDescriptor = this.CreateSparklinePropertyDescriptor(path, this.ItemsSourceSettings.PointArgumentMember);
                }
                return this.argumentColumnDescriptor;
            }
        }

        public SparklinePropertyDescriptorBase ValueColumnDescriptor
        {
            get
            {
                if (this.valueColumnDescriptor == null)
                {
                    string path = string.IsNullOrEmpty(this.ItemsSourceSettings.PointValueMember) ? "ValueColumn" : this.ItemsSourceSettings.PointValueMember;
                    this.valueColumnDescriptor = this.CreateSparklinePropertyDescriptor(path, this.ItemsSourceSettings.PointValueMember);
                }
                return this.valueColumnDescriptor;
            }
        }

        bool IDataControllerData2.CanUseFastProperties =>
            false;

        bool IDataControllerData2.HasUserFilter =>
            false;
    }
}

