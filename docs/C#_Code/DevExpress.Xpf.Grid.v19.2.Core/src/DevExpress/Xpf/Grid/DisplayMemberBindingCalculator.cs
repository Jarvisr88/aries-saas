namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Utils.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class DisplayMemberBindingCalculator
    {
        private ICalculatorStrategy strategyCore;
        internal const string RowPath = "RowData.Row";
        private const string RowDataContextPath = "RowData.DataContext";
        internal const string RowPathWithDot = "RowData.Row.";
        internal const string DataPath = "Data.";

        public DisplayMemberBindingCalculator(DataViewBase gridView, ColumnBase column)
        {
            this.GridView = gridView;
            this.Column = column;
        }

        private static bool ContainsRowData(string path) => 
            path.Contains("RowData.Row");

        public static string GetBindingName(BindingBase binding)
        {
            if (binding == null)
            {
                return null;
            }
            string bindingNameCore = GetBindingNameCore(binding);
            return ((bindingNameCore != null) ? bindingNameCore : binding.GetHashCode().ToString());
        }

        internal static string GetBindingNameCore(BindingBase binding)
        {
            if (binding == null)
            {
                return null;
            }
            Binding binding2 = binding as Binding;
            if ((binding2 != null) && (binding2.Path != null))
            {
                return binding2.Path.Path;
            }
            MultiBinding binding3 = binding as MultiBinding;
            return (((binding3 == null) || (binding3.Bindings.Count <= 0)) ? null : GetBindingNameCore(binding3.Bindings[0]));
        }

        public Type GetColumnType() => 
            this.Strategy.GetColumnType();

        public UnboundColumnType GetUnboundColumnType() => 
            (this.GridView.DataProviderBase.DataRowCount != 0) ? UnboundColumnTypeHelper.TypeToUnboundColumnType(this.GetColumnType()) : UnboundColumnType.Object;

        public object GetValue(int rowHandle) => 
            this.GetValue(rowHandle, -1);

        public object GetValue(int rowHandle, int listSourceRowIndex)
        {
            bool isSimpleMode = this.Strategy.IsSimpleMode;
            object obj2 = this.Strategy.GetValue(rowHandle, listSourceRowIndex);
            if (this.Strategy.IsSimpleMode != isSimpleMode)
            {
                obj2 = this.Strategy.GetValue(rowHandle, listSourceRowIndex);
            }
            return obj2;
        }

        private static void PatchUpdateSourceTrigger(BindingBase displayMemberBinding)
        {
            if (displayMemberBinding is Binding)
            {
                ((Binding) displayMemberBinding).UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            }
            if (displayMemberBinding is MultiBinding)
            {
                ((MultiBinding) displayMemberBinding).UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            }
            else
            {
                foreach (Binding binding in BindingParser.ExtractBindings(displayMemberBinding))
                {
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                }
            }
        }

        public void SetValue(int rowHandle, object value)
        {
            bool isSimpleMode = this.Strategy.IsSimpleMode;
            this.Strategy.SetValue(rowHandle, value);
            if (this.Strategy.IsSimpleMode != isSimpleMode)
            {
                this.Strategy.SetValue(rowHandle, value);
            }
        }

        private static bool ShouldBindToEntireRow(Binding binding) => 
            (binding.Path == null) || (string.IsNullOrEmpty(binding.Path.Path) || (binding.Path.Path == "."));

        private static bool ShouldPatchPath(string path) => 
            !ContainsRowData(path) && (!StartWithRowDataContext(path) && (!StartsWithData(path) && !StartsWithView(path)));

        private static bool StartsWithData(string path) => 
            path.StartsWith("Data.");

        private static bool StartsWithView(string path) => 
            path.StartsWith("View.");

        private static bool StartWithDataContext(string path) => 
            path.StartsWith("DataContext");

        private static bool StartWithRowDataContext(string path) => 
            path.StartsWith("RowData.DataContext");

        public static void ValidateBinding(BindingBase displayMemberBinding)
        {
            ValidateBinding(displayMemberBinding, "RowData.Row", "RowData.Row.", true);
        }

        public static void ValidateBinding(BindingBase displayMemberBinding, string path, bool isSelf)
        {
            ValidateBinding(displayMemberBinding, path, path + ".", isSelf);
        }

        private static void ValidateBinding(BindingBase displayMemberBinding, string path, string pathWithDot, bool self = true)
        {
            if (displayMemberBinding != null)
            {
                PatchUpdateSourceTrigger(displayMemberBinding);
                foreach (Binding binding in BindingParser.ExtractBindings(displayMemberBinding))
                {
                    bool flag = binding.Source != null;
                    if (self && !flag)
                    {
                        binding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
                    }
                    binding.Converter ??= DisplayMemberBindingConverter.Instance;
                    if (ShouldBindToEntireRow(binding))
                    {
                        if (!flag)
                        {
                            binding.Path = new PropertyPath(path, new object[0]);
                        }
                    }
                    else
                    {
                        PropertyPath path2 = binding.Path;
                        string str = path2.Path;
                        if (ShouldPatchPath(str) && !flag)
                        {
                            binding.Path = !StartWithDataContext(str) ? new PropertyPath(pathWithDot + str, path2.PathParameters.ToArray<object>()) : new PropertyPath("RowData." + str, path2.PathParameters.ToArray<object>());
                        }
                    }
                }
            }
        }

        private void ValidateStrategy()
        {
            if ((this.strategyCore == null) || (this.strategyCore.IsSimpleMode != this.Column.IsSimpleBindingEnabled))
            {
                if (this.Column.IsSimpleBindingEnabled)
                {
                    this.strategyCore = new SimpleBindingStrategy(this);
                }
                else
                {
                    this.strategyCore = new DefaultStrategy(this);
                }
            }
        }

        private ICalculatorStrategy Strategy
        {
            get
            {
                this.ValidateStrategy();
                return this.strategyCore;
            }
        }

        public DataViewBase GridView { get; private set; }

        private ColumnBase Column { get; set; }

        private class DefaultStrategy : DisplayMemberBindingCalculator.ICalculatorStrategy
        {
            public DefaultStrategy(DisplayMemberBindingCalculator owner)
            {
                this.Column = owner.Column;
                this.GridView = owner.GridView;
                this.RowData = new StandaloneRowData(owner.GridView.VisualDataTreeBuilder, false, false);
            }

            private void ClearRowData()
            {
                if ((this.RowData.RowHandle != null) && (this.RowData.RowHandle.Value != -2147483648))
                {
                    this.RowData.AssignFrom(-2147483648);
                }
            }

            public Type GetColumnType()
            {
                this.RowData.AssignFrom(0);
                object obj2 = this.RowData.GetCellDataByColumn(this.Column).Value;
                this.ClearRowData();
                return obj2?.GetType();
            }

            public object GetValue(int rowHandle, int listSourceRowIndex)
            {
                this.RowData.conditionalFormattingLocker.DoIfNotLocked(delegate {
                    this.RowData.AssignFromCore(rowHandle, listSourceRowIndex, this.Column);
                });
                object displayMemberBindingValue = ((GridCellData) this.RowData.GetCellDataByColumn(this.Column)).DisplayMemberBindingValue;
                this.ClearRowData();
                return displayMemberBindingValue;
            }

            public void SetValue(int rowHandle, object value)
            {
                if (!this.TrySetValue(rowHandle, value, true))
                {
                    this.TrySetValue(rowHandle, value, false);
                }
                this.ClearRowData();
            }

            private bool TrySetValue(int rowHandle, object value, bool useRealData)
            {
                DevExpress.Xpf.Grid.RowData data = useRealData ? this.GridView.GetRowData(rowHandle) : this.RowData;
                if (data == null)
                {
                    return false;
                }
                data.AssignFrom(rowHandle);
                GridCellData cellDataByColumn = data.GetCellDataByColumn(this.Column) as GridCellData;
                if (cellDataByColumn == null)
                {
                    return false;
                }
                cellDataByColumn.DisplayMemberBindingValue = value;
                return (cellDataByColumn.displayMemberBinding != null);
            }

            internal DevExpress.Xpf.Grid.RowData RowData { get; private set; }

            private DataViewBase GridView { get; set; }

            private ColumnBase Column { get; set; }

            public bool IsSimpleMode =>
                false;
        }

        private interface ICalculatorStrategy
        {
            Type GetColumnType();
            object GetValue(int rowHandle, int listSourceRowIndex);
            void SetValue(int rowHandle, object value);

            bool IsSimpleMode { get; }
        }

        private class SimpleBindingStrategy : DisplayMemberBindingCalculator.ICalculatorStrategy
        {
            public SimpleBindingStrategy(DisplayMemberBindingCalculator owner)
            {
                this.SimpleBindingProcessor = owner.Column.SimpleBindingProcessor;
                this.DataControl = owner.GridView.DataControl;
            }

            public Type GetColumnType() => 
                this.GetValue(0, 0)?.GetType();

            private object GetRowByRowHandle(int rowHandle) => 
                this.DataControl.GetRow(rowHandle);

            public object GetValue(int rowHandle, int listSourceRowIndex)
            {
                object row = null;
                if (rowHandle != -2147483648)
                {
                    row = this.GetRowByRowHandle(rowHandle);
                }
                else if (listSourceRowIndex != -2147483648)
                {
                    row = this.DataControl.DataProviderBase.GetRowByListIndex(listSourceRowIndex);
                }
                return this.SimpleBindingProcessor.GetValue(row);
            }

            public void SetValue(int rowHandle, object value)
            {
                this.SimpleBindingProcessor.SetValue(this.GetRowByRowHandle(rowHandle), value);
            }

            private ISimpleBindingProcessor SimpleBindingProcessor { get; set; }

            private DataControlBase DataControl { get; set; }

            public bool IsSimpleMode =>
                true;
        }
    }
}

