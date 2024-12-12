namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public sealed class FilterModelValueItem : BindableBase
    {
        private readonly Type columnType;
        private readonly FilterModelValueItemInfo info;
        private readonly Action onFilterablePropertyChanged;
        private IReadOnlyCollection<PropertySelectorModelBase> properties;
        private IReadOnlyCollection<string> parameters;

        internal FilterModelValueItem(Type columnType, int index, EditSettingsInfo editSettings, FilterModelValueItemInfo info, Action onFilterablePropertyChanged, ICommand removeCommand = null)
        {
            bool local3;
            this.columnType = columnType;
            this.<EditSettings>k__BackingField = editSettings;
            FilterModelValueItemInfo info1 = info;
            if (info == null)
            {
                FilterModelValueItemInfo local1 = info;
                info1 = FilterModelValueItemInfo.CreateDefault();
            }
            this.info = info1;
            Func<bool> getShowSearchPanel = this.info.GetShowSearchPanel;
            if (getShowSearchPanel != null)
            {
                local3 = getShowSearchPanel();
            }
            else
            {
                Func<bool> local2 = getShowSearchPanel;
                local3 = false;
            }
            this.<ShowSearchPanel>k__BackingField = local3;
            this.onFilterablePropertyChanged = onFilterablePropertyChanged;
            this.Index = index;
            this.<RemoveCommand>k__BackingField = removeCommand;
            this.<SelectParameterCommand>k__BackingField = new DelegateCommand<string>(parameter => this.SelectedParameter = parameter);
            this.Update(null);
        }

        private OperandMenuItem CreateOperandMenuItem(ValueDataKind valueDataKind) => 
            new OperandMenuItem(valueDataKind, new DelegateCommand(delegate {
                this.UpdateMenuAndSelectMenuItem(valueDataKind);
            }));

        private ValueDataKind GetActualValueDataKindAndUpdateState(ValueData valueData)
        {
            Func<ValueDataKind> fallback = <>c.<>9__11_4;
            if (<>c.<>9__11_4 == null)
            {
                Func<ValueDataKind> local1 = <>c.<>9__11_4;
                fallback = <>c.<>9__11_4 = delegate {
                    throw new NotSupportedException();
                };
            }
            return valueData.Match<ValueDataKind>(delegate (object value) {
                this.Value = value;
                return ValueDataKind.Value;
            }, delegate (string propertyName) {
                this.SelectedProperty = LeafNodeModel.SelectProperty(this.Properties, propertyName);
                return ValueDataKind.PropertyName;
            }, delegate (string parameter) {
                this.SelectedParameter = parameter;
                return ValueDataKind.Parameter;
            }, delegate (DevExpress.Xpf.Core.Native.LocalDateTimeFunction localDateTimeFunction) {
                this.LocalDateTimeFunction = new DevExpress.Xpf.Core.Native.LocalDateTimeFunction?(localDateTimeFunction);
                return ValueDataKind.LocalDateTimeFunction;
            }, fallback);
        }

        private List<ValueDataKind> GetAllowedValueDataKinds()
        {
            AllowedOperandTypes types = this.info.GetAllowedOperandTypes();
            List<ValueDataKind> list = new List<ValueDataKind>();
            if (types.HasFlag(AllowedOperandTypes.Value))
            {
                list.Add(ValueDataKind.Value);
            }
            if (types.HasFlag(AllowedOperandTypes.Property))
            {
                list.Add(ValueDataKind.PropertyName);
            }
            if (types.HasFlag(AllowedOperandTypes.Parameter))
            {
                list.Add(ValueDataKind.Parameter);
            }
            if (types.HasFlag(AllowedOperandTypes.LocalDateTimeFunction))
            {
                list.Add(ValueDataKind.LocalDateTimeFunction);
            }
            return list;
        }

        private ValueDataKind GetDefaultValueDataKind()
        {
            List<ValueDataKind> allowedValueDataKinds = this.GetAllowedValueDataKinds();
            return (!allowedValueDataKinds.Contains(ValueDataKind.Value) ? allowedValueDataKinds.FirstOrDefault<ValueDataKind>() : ValueDataKind.Value);
        }

        internal ValueData ToValueData()
        {
            switch (this.SelectedOperandMenuItem.ValueDataKind)
            {
                case ValueDataKind.Value:
                    return ValueData.FromValue(((this.EditSettings == null) || !this.EditSettings.IsUserDefined) ? FilterModelHelper.GetEffectiveFilterValue(this.Value, this.columnType) : this.Value);

                case ValueDataKind.PropertyName:
                {
                    string name;
                    PropertySelectorColumnModel selectedProperty = this.SelectedProperty;
                    if (selectedProperty != null)
                    {
                        name = selectedProperty.Name;
                    }
                    else
                    {
                        PropertySelectorColumnModel local1 = selectedProperty;
                        name = null;
                    }
                    return ValueData.FromProperty(name);
                }
                case ValueDataKind.LocalDateTimeFunction:
                    return ((this.LocalDateTimeFunction != null) ? ValueData.FromLocalDateTimeFunction(this.LocalDateTimeFunction.Value) : null);

                case ValueDataKind.Parameter:
                    return ValueData.FromParameter(this.SelectedParameter);
            }
            throw new ArgumentOutOfRangeException();
        }

        internal void Update(ValueData valueData)
        {
            this.UpdateMenuAndSelectMenuItem(((valueData == null) || ReferenceEquals(valueData, ValueData.NullValue)) ? this.GetDefaultValueDataKind() : this.GetActualValueDataKindAndUpdateState(valueData));
        }

        private void UpdateMenuAndSelectMenuItem(ValueDataKind valueDataKind)
        {
            List<ValueDataKind> allowedValueDataKinds = this.GetAllowedValueDataKinds();
            if ((allowedValueDataKinds.Count == 1) && (((ValueDataKind) allowedValueDataKinds.Single<ValueDataKind>()) == valueDataKind))
            {
                allowedValueDataKinds.Clear();
            }
            this.OperandMenuItems = allowedValueDataKinds.Select<ValueDataKind, OperandMenuItem>(new Func<ValueDataKind, OperandMenuItem>(this.CreateOperandMenuItem)).ToReadOnlyCollection<OperandMenuItem>();
            OperandMenuItem local1 = this.OperandMenuItems.SingleOrDefault<OperandMenuItem>(x => x.ValueDataKind == valueDataKind);
            OperandMenuItem local3 = local1;
            if (local1 == null)
            {
                OperandMenuItem local2 = local1;
                local3 = this.CreateOperandMenuItem(valueDataKind);
            }
            this.SelectedOperandMenuItem = local3;
        }

        public OperandMenuItem SelectedOperandMenuItem
        {
            get => 
                base.GetValue<OperandMenuItem>("SelectedOperandMenuItem");
            set => 
                base.SetValue<OperandMenuItem>(value, "SelectedOperandMenuItem");
        }

        public IReadOnlyCollection<OperandMenuItem> OperandMenuItems
        {
            get => 
                base.GetProperty<IReadOnlyCollection<OperandMenuItem>>(Expression.Lambda<Func<IReadOnlyCollection<OperandMenuItem>>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_OperandMenuItems)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<IReadOnlyCollection<OperandMenuItem>>(Expression.Lambda<Func<IReadOnlyCollection<OperandMenuItem>>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_OperandMenuItems)), new ParameterExpression[0]), value);
        }

        public object Value
        {
            get => 
                base.GetProperty<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_Value)), new ParameterExpression[0]));
            set => 
                base.SetProperty<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_Value)), new ParameterExpression[0]), value, this.onFilterablePropertyChanged);
        }

        public IReadOnlyCollection<PropertySelectorModelBase> Properties
        {
            get
            {
                IReadOnlyCollection<PropertySelectorModelBase> properties = this.properties;
                if (this.properties == null)
                {
                    IReadOnlyCollection<PropertySelectorModelBase> local1 = this.properties;
                    properties = this.properties = this.info.GetProperties();
                }
                return properties;
            }
        }

        public IReadOnlyCollection<string> Parameters
        {
            get
            {
                IReadOnlyCollection<string> parameters = this.parameters;
                if (this.parameters == null)
                {
                    IReadOnlyCollection<string> local1 = this.parameters;
                    parameters = this.parameters = this.info.GetParameters();
                }
                return parameters;
            }
        }

        public PropertySelectorColumnModel SelectedProperty
        {
            get => 
                base.GetProperty<PropertySelectorColumnModel>(Expression.Lambda<Func<PropertySelectorColumnModel>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_SelectedProperty)), new ParameterExpression[0]));
            set => 
                base.SetProperty<PropertySelectorColumnModel>(Expression.Lambda<Func<PropertySelectorColumnModel>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_SelectedProperty)), new ParameterExpression[0]), value, this.onFilterablePropertyChanged);
        }

        public string SelectedParameter
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_SelectedParameter)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_SelectedParameter)), new ParameterExpression[0]), value, this.onFilterablePropertyChanged);
        }

        internal DevExpress.Xpf.Core.Native.LocalDateTimeFunction? LocalDateTimeFunction
        {
            get => 
                base.GetProperty<DevExpress.Xpf.Core.Native.LocalDateTimeFunction?>(Expression.Lambda<Func<DevExpress.Xpf.Core.Native.LocalDateTimeFunction?>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_LocalDateTimeFunction)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<DevExpress.Xpf.Core.Native.LocalDateTimeFunction?>(Expression.Lambda<Func<DevExpress.Xpf.Core.Native.LocalDateTimeFunction?>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_LocalDateTimeFunction)), new ParameterExpression[0]), value, this.onFilterablePropertyChanged);
        }

        public bool AllowRemoving =>
            this.RemoveCommand != null;

        public int Index
        {
            get => 
                base.GetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_Index)), new ParameterExpression[0]));
            internal set => 
                base.SetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(FilterModelValueItem)), (MethodInfo) methodof(FilterModelValueItem.get_Index)), new ParameterExpression[0]), value);
        }

        public EditSettingsInfo EditSettings { get; }

        public ICommand RemoveCommand { get; }

        public ICommand<string> SelectParameterCommand { get; }

        internal bool IsEmpty =>
            this.Value == null;

        public bool ShowSearchPanel { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterModelValueItem.<>c <>9 = new FilterModelValueItem.<>c();
            public static Func<ValueDataKind> <>9__11_4;

            internal ValueDataKind <GetActualValueDataKindAndUpdateState>b__11_4()
            {
                throw new NotSupportedException();
            }
        }
    }
}

