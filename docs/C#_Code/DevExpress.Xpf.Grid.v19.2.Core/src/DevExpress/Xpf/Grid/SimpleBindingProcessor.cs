namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Dynamic;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    internal class SimpleBindingProcessor : ISimpleBindingProcessor
    {
        private IValueConverter converter;
        private object converterParameter;
        private System.Globalization.CultureInfo userCultureInfo;
        private System.Globalization.CultureInfo cultureInfoCore;
        private static Func<Binding, int> GetDelay = CreateGetDelayFunction();
        private string pathCore;
        private ColumnBase column;
        private DataColumnInfo valueColumnInfo;

        public SimpleBindingProcessor(ColumnBase column)
        {
            this.column = column;
        }

        private void ChangeState(SimpleBindingState state, bool validValue)
        {
            if (validValue)
            {
                this.ValidStates |= state;
            }
            else
            {
                this.ValidStates &= ~state;
            }
        }

        private bool CheckDataAccess(object row)
        {
            if (this.valueColumnInfo.PropertyDescriptor.ComponentType.IsAssignableFrom(row.GetType()))
            {
                return true;
            }
            this.Disable();
            return false;
        }

        private object CoerceValue(object value, Type targetType) => 
            (value != null) ? ((!targetType.IsAssignableFrom(value.GetType()) || (Convert.IsDBNull(value) || (value == Binding.DoNothing))) ? DependencyProperty.UnsetValue : value) : NullValueForType(targetType);

        private static Func<Binding, int> CreateGetDelayFunction()
        {
            PropertyInfo property = typeof(Binding).GetProperty("Delay");
            return ((property == null) ? (<>c.<>9__7_0 ??= b => 0) : ((Func<Binding, int>) Delegate.CreateDelegate(typeof(Func<Binding, int>), property.GetGetMethod())));
        }

        private void Disable()
        {
            this.ChangeState(SimpleBindingState.All, false);
        }

        private DataColumnInfo GetInfoByFieldName(string fieldName) => 
            this.OwnerControl?.DataProviderBase.GetActualColumnInfo(fieldName);

        private static string GetPath(Binding binding)
        {
            BindingParseResult result = BindingParser.Parse(binding);
            return ((result.Properties.Length == 1) ? result.Properties[0] : null);
        }

        public object GetValue(object row)
        {
            if ((row == null) || (AsyncServerModeDataController.IsNoValue(row) || !this.CheckDataAccess(row)))
            {
                return null;
            }
            object obj2 = this.valueColumnInfo.PropertyDescriptor.GetValue(row);
            if (this.converter != null)
            {
                obj2 = this.converter.Convert(obj2, typeof(object), this.converterParameter, this.CultureInfo);
            }
            obj2 = this.CoerceValue(obj2, typeof(object));
            if (obj2 == DependencyProperty.UnsetValue)
            {
                this.Disable();
            }
            return obj2;
        }

        private DataColumnInfo GetValueColumnInfo()
        {
            DataColumnInfo infoByFieldName = this.GetInfoByFieldName(this.pathCore);
            if (infoByFieldName == null)
            {
                return null;
            }
            Type componentType = infoByFieldName.PropertyDescriptor.ComponentType;
            if (typeof(DataRowView).IsAssignableFrom(componentType))
            {
                return infoByFieldName;
            }
            Type[] typeArray1 = new Type[] { typeof(DependencyObject), typeof(IDynamicMetaObjectProvider), typeof(ICustomTypeDescriptor) };
            foreach (Type type2 in typeArray1)
            {
                if (type2.IsAssignableFrom(componentType))
                {
                    return null;
                }
            }
            return ((componentType.GetInterface("INotifyDataErrorInfo") == null) ? infoByFieldName : null);
        }

        protected internal static bool IsFieldValid(string field)
        {
            char[] chArray = field.ToCharArray();
            if (chArray.Length == 0)
            {
                return false;
            }
            if (!char.IsLetter(chArray[0]))
            {
                return false;
            }
            if (chArray.Length > 1)
            {
                for (int i = 1; i < chArray.Length; i++)
                {
                    if (!char.IsLetterOrDigit(chArray[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsUnboundColumnInfo(DataColumnInfo info) => 
            (this.OwnerControl != null) && this.OwnerControl.DataProviderBase.IsUnboundColumnInfo(info);

        private static object NullValueForType(Type type) => 
            type?.IsValueType ? ((!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(Nullable<>))) ? DependencyProperty.UnsetValue : null) : null;

        public void ResetLanguage()
        {
            this.cultureInfoCore = null;
        }

        public void SetValue(object row, object newValue)
        {
            if ((row != null) && this.CheckDataAccess(row))
            {
                PropertyDescriptor propertyDescriptor = this.valueColumnInfo.PropertyDescriptor;
                object obj2 = newValue;
                if (this.converter != null)
                {
                    obj2 = this.converter.ConvertBack(obj2, propertyDescriptor.PropertyType, this.converterParameter, this.CultureInfo);
                }
                obj2 = this.CoerceValue(obj2, propertyDescriptor.PropertyType);
                if (obj2 == DependencyProperty.UnsetValue)
                {
                    this.Disable();
                }
                else
                {
                    propertyDescriptor.SetValue(row, obj2);
                }
            }
        }

        public void Validate(SimpleBindingState changedState)
        {
            ValidateIfNeeded(changedState, SimpleBindingState.Binding, new Action(this.ValidateBinding));
            ValidateIfNeeded(changedState, SimpleBindingState.Data, new Action(this.ValidateData));
            ValidateIfNeeded(changedState, SimpleBindingState.Field, new Action(this.ValidateField));
        }

        private void ValidateBinding()
        {
            Binding binding = this.column.Binding as Binding;
            this.pathCore = ((binding == null) || (!binding.GetType().Equals(typeof(Binding)) || !this.ValidateBindingSettings(binding))) ? null : GetPath(binding);
            this.ChangeState(SimpleBindingState.Binding, this.pathCore != null);
            this.ValidateData();
        }

        protected internal static bool ValidateBindingProperties(Binding binding)
        {
            Func<Binding, bool>[] funcArray1 = new Func<Binding, bool>[11];
            Func<Binding, bool> func1 = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<Binding, bool> local1 = <>c.<>9__41_0;
                func1 = <>c.<>9__41_0 = b => b.FallbackValue == DependencyProperty.UnsetValue;
            }
            funcArray1[0] = func1;
            Func<Binding, bool> func2 = <>c.<>9__41_1;
            if (<>c.<>9__41_1 == null)
            {
                Func<Binding, bool> local2 = <>c.<>9__41_1;
                func2 = <>c.<>9__41_1 = b => string.IsNullOrEmpty(b.BindingGroupName);
            }
            funcArray1[1] = func2;
            Func<Binding, bool> func3 = <>c.<>9__41_2;
            if (<>c.<>9__41_2 == null)
            {
                Func<Binding, bool> local3 = <>c.<>9__41_2;
                func3 = <>c.<>9__41_2 = b => b.TargetNullValue == DependencyProperty.UnsetValue;
            }
            funcArray1[2] = func3;
            Func<Binding, bool> func4 = <>c.<>9__41_3;
            if (<>c.<>9__41_3 == null)
            {
                Func<Binding, bool> local4 = <>c.<>9__41_3;
                func4 = <>c.<>9__41_3 = b => ReferenceEquals(b.StringFormat, null);
            }
            funcArray1[3] = func4;
            Func<Binding, bool> func5 = <>c.<>9__41_4;
            if (<>c.<>9__41_4 == null)
            {
                Func<Binding, bool> local5 = <>c.<>9__41_4;
                func5 = <>c.<>9__41_4 = b => !b.IsAsync;
            }
            funcArray1[4] = func5;
            Func<Binding, bool> func6 = <>c.<>9__41_5;
            if (<>c.<>9__41_5 == null)
            {
                Func<Binding, bool> local6 = <>c.<>9__41_5;
                func6 = <>c.<>9__41_5 = b => b.AsyncState == null;
            }
            funcArray1[5] = func6;
            Func<Binding, bool> func7 = <>c.<>9__41_6;
            if (<>c.<>9__41_6 == null)
            {
                Func<Binding, bool> local7 = <>c.<>9__41_6;
                func7 = <>c.<>9__41_6 = b => ReferenceEquals(b.XPath, null);
            }
            funcArray1[6] = func7;
            Func<Binding, bool> func8 = <>c.<>9__41_7;
            if (<>c.<>9__41_7 == null)
            {
                Func<Binding, bool> local8 = <>c.<>9__41_7;
                func8 = <>c.<>9__41_7 = b => !b.NotifyOnSourceUpdated;
            }
            funcArray1[7] = func8;
            Func<Binding, bool> func9 = <>c.<>9__41_8;
            if (<>c.<>9__41_8 == null)
            {
                Func<Binding, bool> local9 = <>c.<>9__41_8;
                func9 = <>c.<>9__41_8 = b => !b.NotifyOnTargetUpdated;
            }
            funcArray1[8] = func9;
            Func<Binding, bool> func10 = <>c.<>9__41_9;
            if (<>c.<>9__41_9 == null)
            {
                Func<Binding, bool> local10 = <>c.<>9__41_9;
                func10 = <>c.<>9__41_9 = b => !b.ValidatesOnDataErrors;
            }
            funcArray1[9] = func10;
            Func<Binding, bool> func11 = <>c.<>9__41_10;
            if (<>c.<>9__41_10 == null)
            {
                Func<Binding, bool> local11 = <>c.<>9__41_10;
                func11 = <>c.<>9__41_10 = b => GetDelay(b) == 0;
            }
            funcArray1[10] = func11;
            foreach (Func<Binding, bool> func in funcArray1)
            {
                if (!func(binding))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateBindingSettings(Binding binding)
        {
            this.converter = ReferenceEquals(binding.Converter, DisplayMemberBindingConverter.Instance) ? null : binding.Converter;
            this.converterParameter = binding.ConverterParameter;
            this.userCultureInfo = binding.ConverterCulture;
            this.ResetLanguage();
            return ValidateBindingProperties(binding);
        }

        private void ValidateData()
        {
            this.valueColumnInfo = this.GetValueColumnInfo();
            this.DescriptorToListen = null;
            if (this.valueColumnInfo == null)
            {
                this.ChangeState(SimpleBindingState.Data, false);
            }
            else
            {
                PropertyDescriptor propertyDescriptor = this.valueColumnInfo.PropertyDescriptor;
                Type componentType = propertyDescriptor.ComponentType;
                if (!typeof(INotifyPropertyChanged).IsAssignableFrom(componentType))
                {
                    this.DescriptorToListen = TypeDescriptor.GetProperties(componentType)[propertyDescriptor.Name];
                }
                this.ChangeState(SimpleBindingState.Data, true);
            }
            this.ValidateField();
        }

        private void ValidateField()
        {
            DataColumnInfo infoByFieldName = this.GetInfoByFieldName(this.column.FieldName);
            this.ChangeState(SimpleBindingState.Field, (infoByFieldName == null) || this.IsUnboundColumnInfo(infoByFieldName));
        }

        private static void ValidateIfNeeded(SimpleBindingState change, SimpleBindingState flag, Action validateAction)
        {
            if (change.HasFlag(flag))
            {
                validateAction();
            }
        }

        private System.Globalization.CultureInfo CultureInfo
        {
            get
            {
                if (this.cultureInfoCore == null)
                {
                    System.Globalization.CultureInfo userCultureInfo = this.userCultureInfo;
                    if (this.userCultureInfo == null)
                    {
                        System.Globalization.CultureInfo local1 = this.userCultureInfo;
                        userCultureInfo = ((XmlLanguage) this.OwnerControl.GetValue(FrameworkElement.LanguageProperty)).GetSpecificCulture();
                    }
                    this.cultureInfoCore = userCultureInfo;
                }
                return this.cultureInfoCore;
            }
        }

        private DataControlBase OwnerControl =>
            this.column.OwnerControl;

        public PropertyDescriptor DescriptorToListen { get; private set; }

        public PropertyDescriptor DataControllerDescriptor =>
            this.valueColumnInfo?.PropertyDescriptor;

        private SimpleBindingState ValidStates { get; set; }

        public bool IsEnabled =>
            this.ValidStates.HasFlag(SimpleBindingState.All);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SimpleBindingProcessor.<>c <>9 = new SimpleBindingProcessor.<>c();
            public static Func<Binding, int> <>9__7_0;
            public static Func<Binding, bool> <>9__41_0;
            public static Func<Binding, bool> <>9__41_1;
            public static Func<Binding, bool> <>9__41_2;
            public static Func<Binding, bool> <>9__41_3;
            public static Func<Binding, bool> <>9__41_4;
            public static Func<Binding, bool> <>9__41_5;
            public static Func<Binding, bool> <>9__41_6;
            public static Func<Binding, bool> <>9__41_7;
            public static Func<Binding, bool> <>9__41_8;
            public static Func<Binding, bool> <>9__41_9;
            public static Func<Binding, bool> <>9__41_10;

            internal int <CreateGetDelayFunction>b__7_0(Binding b) => 
                0;

            internal bool <ValidateBindingProperties>b__41_0(Binding b) => 
                b.FallbackValue == DependencyProperty.UnsetValue;

            internal bool <ValidateBindingProperties>b__41_1(Binding b) => 
                string.IsNullOrEmpty(b.BindingGroupName);

            internal bool <ValidateBindingProperties>b__41_10(Binding b) => 
                SimpleBindingProcessor.GetDelay(b) == 0;

            internal bool <ValidateBindingProperties>b__41_2(Binding b) => 
                b.TargetNullValue == DependencyProperty.UnsetValue;

            internal bool <ValidateBindingProperties>b__41_3(Binding b) => 
                ReferenceEquals(b.StringFormat, null);

            internal bool <ValidateBindingProperties>b__41_4(Binding b) => 
                !b.IsAsync;

            internal bool <ValidateBindingProperties>b__41_5(Binding b) => 
                b.AsyncState == null;

            internal bool <ValidateBindingProperties>b__41_6(Binding b) => 
                ReferenceEquals(b.XPath, null);

            internal bool <ValidateBindingProperties>b__41_7(Binding b) => 
                !b.NotifyOnSourceUpdated;

            internal bool <ValidateBindingProperties>b__41_8(Binding b) => 
                !b.NotifyOnTargetUpdated;

            internal bool <ValidateBindingProperties>b__41_9(Binding b) => 
                !b.ValidatesOnDataErrors;
        }
    }
}

