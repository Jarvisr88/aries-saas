namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraEditors.Filtering;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.Expressions;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing.Design;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [ToolboxItem(false), DesignTimeVisible(false), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter"), TypeConverter("DevExpress.XtraReports.Design.ParameterValueEditorChangingConverter,DevExpress.Utils.v19.2.UI, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), Designer("DevExpress.XtraReports.Design.ParameterDesigner,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public class Parameter : Component, IXtraSupportShouldSerialize, IXtraSupportDeserializeCollectionItem, IParameter, IFilterParameter, IMultiValueParameter, INullableParameter, IRangeRootParameter
    {
        private object value;
        private System.Type type;
        private string name = string.Empty;
        private DevExpress.XtraReports.Parameters.ValueSourceSettings valueSourceSettings;
        private bool multiValue;
        private string valueInfo = string.Empty;
        private bool shouldDeserializeValue;
        internal static string RangeSeparator = " - ";

        public Parameter()
        {
            this.Description = string.Empty;
            this.Visible = true;
        }

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "ExpressionBindings") ? null : new BasicExpressionBinding();

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "ExpressionBindings")
            {
                this.ExpressionBindings.Add(e.Item.Value as BasicExpressionBinding);
            }
        }

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName == "ObjectType") ? (base.GetType() != typeof(Parameter)) : true;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.IsRangeParameter)
                {
                    DevExpress.XtraReports.Parameters.RangeParametersSettings valueSourceSettings = this.ValueSourceSettings as DevExpress.XtraReports.Parameters.RangeParametersSettings;
                    if ((valueSourceSettings != null) && (valueSourceSettings.StartParameter != null))
                    {
                        valueSourceSettings.StartParameter.Dispose();
                    }
                    if ((valueSourceSettings != null) && (valueSourceSettings.EndParameter != null))
                    {
                        valueSourceSettings.EndParameter.Dispose();
                    }
                }
                if (this.Owner != null)
                {
                    this.Owner.Remove(this);
                }
            }
            base.Dispose(disposing);
        }

        private object GetValueCore()
        {
            object obj3;
            if (this.shouldDeserializeValue)
            {
                object obj2;
                this.Logic.TryDeserializeValue(this.valueInfo, out obj2);
                this.SetValueCore(obj2);
            }
            if (!this.IsRangeParameter)
            {
                return (!this.AllowNull ? (this.value ?? this.Logic.DefaultValue) : this.value);
            }
            DevExpress.XtraReports.Parameters.RangeParametersSettings valueSourceSettings = (DevExpress.XtraReports.Parameters.RangeParametersSettings) this.ValueSourceSettings;
            System.Type[] typeArguments = new System.Type[] { this.Type };
            System.Type type = typeof(Range<>).MakeGenericType(typeArguments);
            try
            {
                object[] args = new object[] { valueSourceSettings.StartParameter.Value, valueSourceSettings.EndParameter.Value };
                obj3 = Activator.CreateInstance(type, args);
            }
            catch (Exception exception)
            {
                Tracer.TraceError("DXperience.Reporting", exception);
                throw new Exception($"An error occurred while evaluating the '{this.Name}' parameter.", exception);
            }
            return obj3;
        }

        private void OnChangeValueSourceSettings(DevExpress.XtraReports.Parameters.ValueSourceSettings oldSettings, DevExpress.XtraReports.Parameters.ValueSourceSettings newSettings)
        {
            if (oldSettings != null)
            {
                this.SetOwnerParameter(oldSettings, null);
            }
            if (newSettings != null)
            {
                this.SetOwnerParameter(newSettings, this);
                newSettings.SyncParameterType(this.Type);
            }
            if (!this.IsLoading)
            {
                this.SetValueCore(this.Logic.ConvertValue(this.Value));
            }
        }

        private void ResetType()
        {
            this.Type = typeof(string);
        }

        internal void ResetValue()
        {
            this.SetValueCore(this.Logic.DefaultValue);
        }

        private void ResetValueInfo()
        {
            this.SetValueCore(this.Logic.DefaultValue);
        }

        private void SetOwnerParameter(DevExpress.XtraReports.Parameters.ValueSourceSettings valueSourceSettings, Parameter owner)
        {
            if (valueSourceSettings != null)
            {
                valueSourceSettings.Parameter = owner;
            }
        }

        private void SetValueCore(object value)
        {
            this.value = value;
            this.valueInfo = string.Empty;
            this.shouldDeserializeValue = false;
            this.Logic.OnValueChanged(value);
        }

        private bool ShouldSerializeType() => 
            this.Type != typeof(string);

        private bool ShouldSerializeValue()
        {
            DevExpress.XtraReports.Parameters.RangeParametersSettings settings = this.IsRangeParameter ? (this.ValueSourceSettings as DevExpress.XtraReports.Parameters.RangeParametersSettings) : null;
            return ((settings == null) ? this.ShouldSerializeValueInfo() : (settings.StartParameter.ShouldSerializeValue() || settings.EndParameter.ShouldSerializeValue()));
        }

        private bool ShouldSerializeValueInfo() => 
            !string.IsNullOrEmpty(this.ValueInfo) && !this.IsRangeParameter;

        private static System.Type ToType(DevExpress.XtraReports.Parameters.ParameterType value) => 
            (value != DevExpress.XtraReports.Parameters.ParameterType.Boolean) ? ((value != DevExpress.XtraReports.Parameters.ParameterType.DateTime) ? ((value != DevExpress.XtraReports.Parameters.ParameterType.Decimal) ? ((value != DevExpress.XtraReports.Parameters.ParameterType.Double) ? ((value != DevExpress.XtraReports.Parameters.ParameterType.Float) ? ((value != DevExpress.XtraReports.Parameters.ParameterType.Int32) ? ((value != DevExpress.XtraReports.Parameters.ParameterType.Int64) ? ((value != DevExpress.XtraReports.Parameters.ParameterType.String) ? typeof(object) : typeof(string)) : typeof(long)) : typeof(int)) : typeof(float)) : typeof(double)) : typeof(decimal)) : typeof(DateTime)) : typeof(bool);

        private object ValidateValue(object value)
        {
            object obj2;
            return ((!(value is string) || (!(this.Type != typeof(string)) || !this.Logic.TryConvertValue(value, out obj2))) ? value : obj2);
        }

        internal bool IsRangeParameter =>
            this.RangeParametersSettings != null;

        private DevExpress.XtraReports.Parameters.RangeParametersSettings RangeParametersSettings =>
            this.ValueSourceSettings as DevExpress.XtraReports.Parameters.RangeParametersSettings;

        bool IRangeRootParameter.IsRange =>
            this.RangeParametersSettings != null;

        IRangeBoundaryParameter IRangeRootParameter.StartParameter
        {
            get
            {
                DevExpress.XtraReports.Parameters.RangeParametersSettings rangeParametersSettings = this.RangeParametersSettings;
                if (rangeParametersSettings != null)
                {
                    return rangeParametersSettings.StartParameter;
                }
                DevExpress.XtraReports.Parameters.RangeParametersSettings local1 = rangeParametersSettings;
                return null;
            }
        }

        IRangeBoundaryParameter IRangeRootParameter.EndParameter
        {
            get
            {
                DevExpress.XtraReports.Parameters.RangeParametersSettings rangeParametersSettings = this.RangeParametersSettings;
                if (rangeParametersSettings != null)
                {
                    return rangeParametersSettings.EndParameter;
                }
                DevExpress.XtraReports.Parameters.RangeParametersSettings local1 = rangeParametersSettings;
                return null;
            }
        }

        private ValueLogic Logic =>
            this.IsRangeParameter ? new RangeValueLogic(this) : (this.MultiValue ? new MultiValueLogic(this) : new ValueLogic(this));

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object RawValue =>
            this.value;

        [Obsolete("The ParameterType property is now obsolete. Use the Type property instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public DevExpress.XtraReports.Parameters.ParameterType ParameterType
        {
            get => 
                DevExpress.XtraReports.Parameters.ParameterType.String;
            set => 
                this.Type = ToType(value);
        }

        [Description("Specifies whether a parameter's editor should be displayed in the Parameters UI, which is invoked for an end-user if the XtraReport.RequestParameters property is enabled."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.Visible"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), Category("Behavior"), XtraSerializableProperty]
        public bool Visible { get; set; }

        [Description("Specifies a description displayed to an end-user, along with the parameter's editor in the Parameters UI, that is generated if the XtraReport.RequestParameters property is enabled."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.Description"), DefaultValue(""), Localizable(true), Category("Data"), XtraSerializableProperty, Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Description { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public DevExpress.XtraReports.Parameters.LookUpSettings LookUpSettings
        {
            get => 
                this.ValueSourceSettings as DevExpress.XtraReports.Parameters.LookUpSettings;
            set => 
                this.ValueSourceSettings = value;
        }

        [Description("Provides access to the settings that are used to generate the parameter's predefined values."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.ValueSourceSettings"), XtraSerializableProperty(XtraSerializationVisibility.Reference), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.All), TypeConverter("DevExpress.XtraReports.Design.ParameterValueSourceSettingsConverter,DevExpress.Utils.v19.2.UI, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), Editor("DevExpress.XtraReports.Design.ParameterValueSourceSettingsEditor,DevExpress.Utils.v19.2.UI, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
        public virtual DevExpress.XtraReports.Parameters.ValueSourceSettings ValueSourceSettings
        {
            get
            {
                this.SetOwnerParameter(this.valueSourceSettings, this);
                return this.valueSourceSettings;
            }
            set
            {
                if (!ReferenceEquals(this.valueSourceSettings, value))
                {
                    DevExpress.XtraReports.Parameters.ValueSourceSettings valueSourceSettings = this.valueSourceSettings;
                    this.valueSourceSettings = value;
                    this.OnChangeValueSourceSettings(valueSourceSettings, this.valueSourceSettings);
                }
            }
        }

        [Description("Specifies whether or not a parameter can have multiple values."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.MultiValue"), XtraSerializableProperty, DefaultValue(false), Category("Data"), RefreshProperties(RefreshProperties.All), TypeConverter("DevExpress.XtraReports.Design.RangeIncompatiblePropertyConverter,DevExpress.Utils.v19.2.UI, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
        public virtual bool MultiValue
        {
            get => 
                !this.IsRangeParameter && this.multiValue;
            set
            {
                if (this.multiValue != value)
                {
                    this.multiValue = value;
                    if (!this.IsLoading)
                    {
                        this.SetValueCore(this.Logic.ConvertMultiValue(this.Value, this.type));
                    }
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty]
        public string ValueInfo
        {
            get
            {
                if (string.IsNullOrEmpty(this.valueInfo) && (this.value != null))
                {
                    this.valueInfo = this.Logic.SerializeValue(this.value);
                }
                return this.valueInfo;
            }
            set
            {
                this.valueInfo = value;
                this.shouldDeserializeValue = !string.IsNullOrEmpty(value);
            }
        }

        private System.TypeCode TypeCode =>
            DXTypeExtensions.GetTypeCode(this.Type);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(-1)]
        public string ObjectType =>
            base.GetType().AssemblyQualifiedName;

        [Description("Indicates whether the parameter's value can be unspecified."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.AllowNull"), XtraSerializableProperty, DefaultValue(false), Category("Data"), RefreshProperties(RefreshProperties.All), TypeConverter("DevExpress.XtraReports.Design.RangeIncompatiblePropertyConverter,DevExpress.Utils.v19.2.UI, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
        public bool AllowNull { get; set; }

        [Description("Specifies the object that contains data about the report parameter."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.Tag"), XtraSerializableProperty, DefaultValue(""), Category("Data"), TypeConverter(typeof(StringConverter))]
        public object Tag { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.Cached)]
        public BasicExpressionBindingCollection ExpressionBindings { get; } = new BasicExpressionBindingCollection()

        internal virtual bool IsLoading =>
            (this.Owner == null) || this.Owner.IsLoading;

        protected internal ParameterCollection Owner { get; set; }

        internal virtual object DefaultValue =>
            ParameterHelper.GetDefaultValue(this.Type);

        [DefaultValue(""), Browsable(false), XtraSerializableProperty]
        public string Name
        {
            get => 
                (this.Site != null) ? this.Site.Name : this.name;
            set => 
                this.name = value;
        }

        [Description("Determines which values a report parameter can accept."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.Type"), RefreshProperties(RefreshProperties.All), Category("Data"), XtraSerializableProperty(XtraSerializationVisibility.Reference), TypeConverter("DevExpress.XtraReports.Design.ParameterTypeConverter,DevExpress.Utils.v19.2.UI, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
        public virtual System.Type Type
        {
            get
            {
                System.Type type = this.type;
                if (this.type == null)
                {
                    System.Type local1 = this.type;
                    System.Type type1 = this.Logic.GetType(this.value);
                    type = type1;
                    if (type1 == null)
                    {
                        System.Type local2 = type1;
                        type = typeof(string);
                    }
                }
                return type;
            }
            set
            {
                if (!this.IsLoading)
                {
                    Guard.ArgumentNotNull(value, "Type");
                }
                if (this.type != value)
                {
                    this.type = value;
                    if (!this.IsLoading)
                    {
                        this.SetValueCore(this.Logic.ConvertValue(this.Value));
                    }
                    if (this.ValueSourceSettings != null)
                    {
                        this.ValueSourceSettings.SyncParameterType(this.type);
                    }
                }
            }
        }

        [Description("Specifies the report parameter's value."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.Parameter.Value"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get => 
                this.GetValueCore();
            set
            {
                if (!this.IsLoading)
                {
                    this.SetValueCore(this.ValidateValue(value));
                }
                else
                {
                    this.value = value;
                    this.Logic.OnValueChanged(value);
                }
            }
        }

        private class MultiValueLogic : Parameter.ValueLogic
        {
            public MultiValueLogic(Parameter parameter) : base(parameter)
            {
            }

            [CompilerGenerated, DebuggerHidden]
            private string <>n__0(object value) => 
                base.SerializeValue(value);

            public override object ConvertMultiValue(object value, Type type)
            {
                if ((value != null) && (value.GetType() == type))
                {
                    object[] objArray1 = new object[] { value };
                    return new ArrayList(objArray1).ToArray(type);
                }
                if (!(value is string) || (type != null))
                {
                    return Array.CreateInstance(type ?? typeof(string), 0);
                }
                object[] c = new object[] { value };
                return new ArrayList(c).ToArray(typeof(string));
            }

            public override Type GetType(object value) => 
                null;

            [IteratorStateMachine(typeof(<SerializeParts>d__7))]
            private IEnumerable<string> SerializeParts(IEnumerable value)
            {
                <SerializeParts>d__7 d__1 = new <SerializeParts>d__7(-2);
                d__1.<>4__this = this;
                d__1.<>3__value = value;
                return d__1;
            }

            public override string SerializeValue(object value) => 
                !(value is IEnumerable) ? base.SerializeValue(value) : StringCombiner.Combine(this.SerializeParts((IEnumerable) value));

            public override bool TryConvertValue(object value, out object result) => 
                ParameterHelper.TryConvertEnumerable(value as IEnumerable, out result, base.Type, CultureInfo.InvariantCulture);

            public override bool TryDeserializeValue(string value, out object result)
            {
                if (string.IsNullOrEmpty(value))
                {
                    result = null;
                    return false;
                }
                ArrayList list = new ArrayList();
                foreach (string str in StringCombiner.Split(value))
                {
                    object obj2 = null;
                    if (base.TryDeserializeValue(str, out obj2))
                    {
                        list.Add(obj2);
                    }
                }
                result = (list.Count > 0) ? list.ToArray(base.Type) : null;
                return (result != null);
            }

            public override object DefaultValue =>
                Array.CreateInstance(base.Type, 0);

            [CompilerGenerated]
            private sealed class <SerializeParts>d__7 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private string <>2__current;
                private int <>l__initialThreadId;
                private IEnumerable value;
                public IEnumerable <>3__value;
                public Parameter.MultiValueLogic <>4__this;
                private IEnumerator <>7__wrap1;

                [DebuggerHidden]
                public <SerializeParts>d__7(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    IDisposable disposable = this.<>7__wrap1 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            this.<>7__wrap1 = this.value.GetEnumerator();
                            this.<>1__state = -3;
                        }
                        else if (num == 1)
                        {
                            this.<>1__state = -3;
                        }
                        else
                        {
                            return false;
                        }
                        while (true)
                        {
                            if (!this.<>7__wrap1.MoveNext())
                            {
                                this.<>m__Finally1();
                                this.<>7__wrap1 = null;
                                flag = false;
                            }
                            else
                            {
                                object current = this.<>7__wrap1.Current;
                                if (!this.<>4__this.Type.IsAssignableFrom(current.GetType()))
                                {
                                    continue;
                                }
                                this.<>2__current = this.<>4__this.<>n__0(current);
                                this.<>1__state = 1;
                                flag = true;
                            }
                            break;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                IEnumerator<string> IEnumerable<string>.GetEnumerator()
                {
                    Parameter.MultiValueLogic.<SerializeParts>d__7 d__;
                    if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                    {
                        this.<>1__state = 0;
                        d__ = this;
                    }
                    else
                    {
                        d__ = new Parameter.MultiValueLogic.<SerializeParts>d__7(0) {
                            <>4__this = this.<>4__this
                        };
                    }
                    d__.value = this.<>3__value;
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if ((num == -3) || (num == 1))
                    {
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                string IEnumerator<string>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        private class RangeValueLogic : Parameter.MultiValueLogic
        {
            public RangeValueLogic(Parameter parameter) : base(parameter)
            {
            }

            public override object ConvertMultiValue(object value, Type type) => 
                null;

            public override void OnValueChanged(object value)
            {
                IRange range = value as IRange;
                if (range != null)
                {
                    RangeParametersSettings valueSourceSettings = (RangeParametersSettings) base.Parameter.ValueSourceSettings;
                    valueSourceSettings.StartParameter.Value = range.Start;
                    valueSourceSettings.EndParameter.Value = range.End;
                }
            }

            public override bool TryConvertValue(object value, out object result)
            {
                if (value is IRange)
                {
                    result = value;
                    return true;
                }
                result = null;
                return false;
            }

            public override bool TryDeserializeValue(string value, out object result)
            {
                result = null;
                return false;
            }

            public override object DefaultValue
            {
                get
                {
                    Type[] typeArguments = new Type[] { base.Parameter.Type };
                    object[] args = new object[] { base.Parameter.DefaultValue, base.Parameter.DefaultValue };
                    return Activator.CreateInstance(typeof(Range<>).MakeGenericType(typeArguments), args);
                }
            }
        }

        private class ValueLogic
        {
            public ValueLogic(DevExpress.XtraReports.Parameters.Parameter parameter)
            {
                this.Parameter = parameter;
            }

            public virtual object ConvertMultiValue(object value, System.Type type)
            {
                object obj2;
                return ((!(value is IEnumerable) || ((value is string) || (!TryGetValuableItem((IEnumerable) value, out obj2) || ((obj2.GetType() != type) && (!(obj2 is string) || (type != null)))))) ? ParameterHelper.GetDefaultValue(type ?? typeof(string)) : obj2);
            }

            public object ConvertValue(object value)
            {
                object obj2;
                return (this.TryConvertValue(value, out obj2) ? obj2 : (this.Parameter.AllowNull ? null : this.DefaultValue));
            }

            public virtual System.Type GetType(object value) => 
                ((value == null) || (value == DBNull.Value)) ? null : value.GetType();

            public virtual void OnValueChanged(object value)
            {
            }

            public virtual string SerializeValue(object value) => 
                ((this.TypeCode != System.TypeCode.Object) || !(this.Type != typeof(Guid))) ? ParameterHelper.ConvertValueToString(value) : ((this.Owner != null) ? this.Owner.Serialize(value) : string.Empty);

            public virtual bool TryConvertValue(object value, out object result) => 
                ParameterHelper.TryConvertValue(value, out result, this.Type, CultureInfo.InvariantCulture);

            public virtual bool TryDeserializeValue(string value, out object result)
            {
                if ((this.Owner != null) && this.Owner.Deserialize(value, this.Type.FullName, out result))
                {
                    return true;
                }
                result = ParameterHelper.ConvertFrom(value, this.Type, null);
                return (result != null);
            }

            private static bool TryGetValuableItem(IEnumerable enumerable, out object value)
            {
                bool flag;
                using (IEnumerator enumerator = enumerable.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            object current = enumerator.Current;
                            if (current == null)
                            {
                                continue;
                            }
                            value = current;
                            flag = true;
                        }
                        else
                        {
                            value = null;
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }

            protected DevExpress.XtraReports.Parameters.Parameter Parameter { get; private set; }

            protected System.Type Type =>
                this.Parameter.Type;

            protected System.TypeCode TypeCode =>
                this.Parameter.TypeCode;

            protected ParameterCollection Owner =>
                this.Parameter.Owner;

            public virtual object DefaultValue =>
                this.Parameter.DefaultValue;
        }
    }
}

