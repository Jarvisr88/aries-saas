namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class SparklineItemsProvider : FrameworkElement, IItemsSourceSupport
    {
        private const SparklineSortOrder defaultSortOrder = SparklineSortOrder.Ascending;
        private ItemsProviderChangedEventHandler itemsProviderChanged;
        private SparklinePointCollection points = new SparklinePointCollection();
        private SortedSparklinePointCollection pointsSortedByArgument = new SortedSparklinePointCollection(new SparklinePointArgumentComparer());
        private IList ListSource;
        private ListSourceDataController DataController;
        private object itemsSource;
        private SparklineSortOrder pointArgumentSortOrder = SparklineSortOrder.Ascending;
        private string pointArgumentMember;
        private string pointValueMember;
        private CriteriaOperator filterCriteria;

        public event ItemsProviderChangedEventHandler ItemsProviderChanged
        {
            add
            {
                this.itemsProviderChanged += value;
            }
            remove
            {
                this.itemsProviderChanged -= value;
            }
        }

        private void AddItem(int listSourceIndex)
        {
            int controllerRow = this.DataController.GetControllerRow(listSourceIndex);
            if ((controllerRow != -2147483648) && ((controllerRow >= 0) && (controllerRow <= this.points.Count)))
            {
                object rowByListSourceIndex = this.DataController.GetRowByListSourceIndex(listSourceIndex);
                SparklinePoint item = this.CreateSparklineItem(rowByListSourceIndex, controllerRow);
                if (item != null)
                {
                    if (controllerRow == this.points.Count)
                    {
                        this.points.Add(item);
                    }
                    else
                    {
                        this.points.Insert(controllerRow, item);
                    }
                    this.pointsSortedByArgument.Add(item);
                    this.SetAutoArguments(controllerRow);
                }
            }
        }

        private SparklinePoint CreateSparklineItem(object row, int index)
        {
            SparklinePoint point = null;
            SparklineScaleType type;
            double? pointValue = this.GetPointValue(row, out type);
            if (pointValue != null)
            {
                point = new SparklinePoint((double) index, pointValue.Value) {
                    ValueScaleType = type
                };
            }
            if ((point != null) && !string.IsNullOrEmpty(this.PointArgumentMember))
            {
                SparklineScaleType type2;
                double? pointArgument = this.GetPointArgument(row, out type2);
                if (pointArgument != null)
                {
                    point.Argument = pointArgument.Value;
                    point.ArgumentScaleType = type2;
                }
            }
            return point;
        }

        private void DataController_ListChanged(object sender, ListChangedEventArgs e)
        {
            int newIndex = e.NewIndex;
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                this.UpdateItem(newIndex);
            }
            else if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                this.AddItem(newIndex);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                this.RemoveItem(newIndex);
            }
            else
            {
                this.FillItemCollection();
            }
            this.RaiseDataChanged(e.ListChangedType, newIndex, e.PropertyDescriptor);
        }

        private IList ExtractDataSource()
        {
            object itemsSource = this.ItemsSource;
            return this.ExtractSimpleDataSource(itemsSource);
        }

        private IList ExtractSimpleDataSource(object itemsSource)
        {
            ICollectionView source = itemsSource as ICollectionView;
            return ((source == null) ? DataBindingHelper.ExtractDataSource(itemsSource, true, false, false) : DataBindingHelper.ExtractDataSourceFromCollectionView(source, ItemPropertyNotificationMode.PropertyChanged));
        }

        private void FillItemCollection()
        {
            this.points.Clear();
            int index = 0;
            foreach (object obj2 in this.DataController.GetAllFilteredAndSortedRows())
            {
                SparklinePoint item = this.CreateSparklineItem(obj2, index);
                if (item != null)
                {
                    this.points.Add(item);
                }
                index++;
            }
            this.pointsSortedByArgument.Clear();
            this.pointsSortedByArgument.AddRange(this.points);
        }

        private void FilterDataSource()
        {
            try
            {
                this.DataController.BeginUpdate();
                this.DataController.FilterCriteria = this.FilterCriteria;
            }
            finally
            {
                this.DataController.EndUpdate();
            }
        }

        private double? GetPointArgument(object row, out SparklineScaleType scaleType)
        {
            object obj2 = this.DataControllerData.ArgumentColumnDescriptor.GetValue(row);
            return this.GetValue(obj2, out scaleType);
        }

        private double? GetPointValue(object row, out SparklineScaleType scaleType)
        {
            object obj2 = this.DataControllerData.ValueColumnDescriptor.GetValue(row);
            return this.GetValue(obj2, out scaleType);
        }

        private double? GetValue(object value, out SparklineScaleType scaleType)
        {
            if (SparklinePropertyDescriptorBase.IsUnsetValue(value))
            {
                scaleType = SparklineScaleType.Unknown;
                return null;
            }
            if (value != null)
            {
                return SparklineMathUtils.ConvertToDouble(value, out scaleType);
            }
            scaleType = SparklineScaleType.Unknown;
            return null;
        }

        private void OnItemsSourceChanged()
        {
            this.RecreateDataController();
            this.FillItemCollection();
        }

        private void OnItemsSourceSettingsChanged()
        {
            this.OnItemsSourceChanged();
        }

        public void RaiseDataChanged(ItemsProviderDataChangedEventArgs args)
        {
            if (this.itemsProviderChanged != null)
            {
                this.itemsProviderChanged(this, args);
            }
        }

        public void RaiseDataChanged(ListChangedType changedType = 0, int newIndex = -1, PropertyDescriptor descriptor = null)
        {
            this.RaiseDataChanged(new ItemsProviderDataChangedEventArgs(changedType, newIndex, descriptor));
        }

        private void RecreateDataController()
        {
            if (this.DataController != null)
            {
                this.DataController.ListChanged -= new ListChangedEventHandler(this.DataController_ListChanged);
                this.DataController.Dispose();
            }
            this.DataController = new ListSourceDataController();
            this.DataController.DataClient = new SparklineDataClient(this);
            this.DataController.ListChanged += new ListChangedEventHandler(this.DataController_ListChanged);
            this.ListSource = this.ExtractDataSource();
            this.DataControllerData.ResetDescriptors();
            this.DataController.SetDataSource(this.ListSource);
            this.FilterDataSource();
        }

        private void RemoveItem(int listSourceIndex)
        {
            int controllerRow = this.DataController.GetControllerRow(listSourceIndex);
            if (controllerRow != -2147483648)
            {
                this.pointsSortedByArgument.Remove(this.points[controllerRow]);
                this.points.RemoveAt(controllerRow);
                this.SetAutoArguments(controllerRow);
            }
        }

        private void SetAutoArguments(int controllerIndex)
        {
            if (string.IsNullOrEmpty(this.PointArgumentMember))
            {
                for (int i = controllerIndex; i < this.points.Count; i++)
                {
                    this.points[i].Argument = i;
                }
            }
        }

        private void UpdateItem(int listSourceIndex)
        {
            int controllerRow = this.DataController.GetControllerRow(listSourceIndex);
            if ((controllerRow != -2147483648) && ((controllerRow >= 0) && (controllerRow < this.points.Count)))
            {
                object rowByListSourceIndex = this.DataController.GetRowByListSourceIndex(listSourceIndex);
                SparklinePoint newItem = this.CreateSparklineItem(rowByListSourceIndex, controllerRow);
                SparklinePoint oldItem = this.points[controllerRow];
                if (newItem != null)
                {
                    this.points[controllerRow] = newItem;
                    this.pointsSortedByArgument.Update(oldItem, newItem);
                }
            }
        }

        private SparklineDataClient DataControllerData =>
            this.DataController.DataClient as SparklineDataClient;

        public SparklinePointCollection Points =>
            this.pointsSortedByArgument;

        public object ItemsSource
        {
            get => 
                this.itemsSource;
            set
            {
                if (this.itemsSource != value)
                {
                    this.itemsSource = value;
                    this.OnItemsSourceChanged();
                }
            }
        }

        public SparklineSortOrder PointArgumentSortOrder
        {
            get => 
                this.pointArgumentSortOrder;
            set
            {
                if (this.pointArgumentSortOrder != value)
                {
                    this.pointArgumentSortOrder = value;
                    this.pointsSortedByArgument.Sort(new SparklinePointArgumentComparer(this.pointArgumentSortOrder != SparklineSortOrder.Descending));
                    this.OnItemsSourceSettingsChanged();
                }
            }
        }

        public string PointArgumentMember
        {
            get => 
                this.pointArgumentMember;
            set
            {
                if (this.pointArgumentMember != value)
                {
                    this.pointArgumentMember = value;
                    this.OnItemsSourceSettingsChanged();
                }
            }
        }

        public string PointValueMember
        {
            get => 
                this.pointValueMember;
            set
            {
                if (this.pointValueMember != value)
                {
                    this.pointValueMember = value;
                    this.OnItemsSourceSettingsChanged();
                }
            }
        }

        public CriteriaOperator FilterCriteria
        {
            get => 
                this.filterCriteria;
            set
            {
                if (!ReferenceEquals(this.filterCriteria, value))
                {
                    this.filterCriteria = value;
                    this.OnItemsSourceSettingsChanged();
                }
            }
        }
    }
}

