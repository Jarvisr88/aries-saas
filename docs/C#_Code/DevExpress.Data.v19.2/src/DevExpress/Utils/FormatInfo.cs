namespace DevExpress.Utils
{
    using DevExpress.Data;
    using DevExpress.Utils.Serializing;
    using DevExpress.WebUtils;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FormatInfo : ViewStatePersisterCore, IXtraSerializable
    {
        private static bool alwaysUseThreadFormat;
        private static readonly FormatInfo fEmpty = new FormatInfo();
        private int fLockParse;
        private IComponentLoading componentLoading;
        private bool shouldModifyFormatString;
        protected DevExpress.Utils.FormatType fFormatType;
        protected string fFormatString;
        private IFormatProvider _format;
        private bool deserializing;
        [ThreadStatic]
        private static NumberFormatInfo numberFormatInfoCached;
        [ThreadStatic]
        private static DateTimeFormatInfo dateTimeFormatInfoCached;

        [Browsable(false)]
        public event EventHandler Changed;

        public FormatInfo() : this(null)
        {
        }

        public FormatInfo(IComponentLoading componentLoading) : this(componentLoading, null, string.Empty)
        {
        }

        public FormatInfo(IViewBagOwner bagOwner, string objectPath) : this(null, bagOwner, objectPath)
        {
        }

        public FormatInfo(IComponentLoading componentLoading, IViewBagOwner bagOwner, string objectPath) : base(bagOwner, objectPath)
        {
            this.componentLoading = componentLoading;
            this.fLockParse = 0;
            this.Reset();
        }

        public virtual void Assign(FormatInfo info)
        {
            this._format = info.Format;
            this.fFormatString = info.FormatString;
            this.fFormatType = info.FormatType;
            this.shouldModifyFormatString = info.shouldModifyFormatString;
            this.OnChanged();
        }

        protected void CheckFormatString()
        {
            this.shouldModifyFormatString = this.FormatString.IndexOf('{') == -1;
        }

        public virtual string GetDisplayText(object val) => 
            this.GetDisplayTextCore(val, this.Format, this.FormatString);

        protected virtual string GetDisplayTextCore(object val, IFormatProvider format, string formatString)
        {
            string stringValue;
            if (val == null)
            {
                return string.Empty;
            }
            try
            {
                if ((format != null) && this.shouldModifyFormatString)
                {
                    string str = this.TryFormat(val, format, formatString);
                    if (str != null)
                    {
                        return str;
                    }
                }
            }
            catch
            {
            }
            if ((format == null) && (this.FormatType != DevExpress.Utils.FormatType.Custom))
            {
                return this.GetStringValue(val);
            }
            formatString = this.GetFormatString(formatString);
            if (format != null)
            {
                try
                {
                    object[] args = new object[] { val };
                    stringValue = string.Format(format, formatString, args);
                }
                catch
                {
                    stringValue = val.ToString();
                }
            }
            else
            {
                if (this.fFormatType != DevExpress.Utils.FormatType.Custom)
                {
                    return this.GetStringValue(val);
                }
                try
                {
                    stringValue = string.Format(formatString, val);
                }
                catch
                {
                    stringValue = this.GetStringValue(val);
                }
            }
            return stringValue;
        }

        public string GetFormatString() => 
            this.GetFormatString(this.FormatString);

        protected string GetFormatString(string formatString)
        {
            if (this.shouldModifyFormatString)
            {
                formatString = !string.IsNullOrEmpty(formatString) ? ("{0:" + this.FormatString + "}") : "{0}";
            }
            return formatString;
        }

        private static string GetSimpleValueString(object val, IFormatProvider format, string formatString)
        {
            switch (val)
            {
                case (int _):
                {
                    IFormatProvider numberFormatInfoCached = format;
                    if (format == null)
                    {
                        IFormatProvider local1 = format;
                        numberFormatInfoCached = NumberFormatInfoCached;
                    }
                    return ((int) val).ToString(formatString, numberFormatInfoCached);
                    break;
                }
            }
            if (val is string)
            {
                return ((string) val).ToString(format);
            }
            if (val is decimal)
            {
                IFormatProvider numberFormatInfoCached = format;
                if (format == null)
                {
                    IFormatProvider local2 = format;
                    numberFormatInfoCached = NumberFormatInfoCached;
                }
                return ((decimal) val).ToString(formatString, numberFormatInfoCached);
            }
            if (val is DateTime)
            {
                IFormatProvider dateTimeFormatInfoCached = format;
                if (format == null)
                {
                    IFormatProvider local3 = format;
                    dateTimeFormatInfoCached = DateTimeFormatInfoCached;
                }
                return ((DateTime) val).ToString(formatString, dateTimeFormatInfoCached);
            }
            if (val is short)
            {
                IFormatProvider numberFormatInfoCached = format;
                if (format == null)
                {
                    IFormatProvider local4 = format;
                    numberFormatInfoCached = NumberFormatInfoCached;
                }
                return ((short) val).ToString(formatString, numberFormatInfoCached);
            }
            if (val is long)
            {
                IFormatProvider numberFormatInfoCached = format;
                if (format == null)
                {
                    IFormatProvider local5 = format;
                    numberFormatInfoCached = NumberFormatInfoCached;
                }
                return ((long) val).ToString(formatString, numberFormatInfoCached);
            }
            if (val is float)
            {
                IFormatProvider numberFormatInfoCached = format;
                if (format == null)
                {
                    IFormatProvider local6 = format;
                    numberFormatInfoCached = NumberFormatInfoCached;
                }
                return ((float) val).ToString(formatString, numberFormatInfoCached);
            }
            if (!(val is double))
            {
                return null;
            }
            IFormatProvider provider = format;
            if (format == null)
            {
                IFormatProvider local7 = format;
                provider = NumberFormatInfoCached;
            }
            return ((double) val).ToString(formatString, provider);
        }

        private string GetStringValue(object val)
        {
            try
            {
                return val.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public virtual bool IsEquals(FormatInfo info) => 
            (info != null) ? (!ReferenceEquals(info, fEmpty) ? (ReferenceEquals(info.Format, this.Format) && ((info.FormatType == this.FormatType) && (info.FormatString == this.FormatString))) : ((this.Format == null) && ((this.FormatType == DevExpress.Utils.FormatType.None) && (this.FormatString == string.Empty)))) : false;

        public virtual void LockParse()
        {
            this.fLockParse++;
        }

        protected virtual void OnChanged()
        {
            this.SetViewBagProperty<DevExpress.Utils.FormatType>("FormatType", DevExpress.Utils.FormatType.None, this.fFormatType);
            this.SetViewBagProperty<string>("FormatString", string.Empty, this.fFormatString);
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        public void OnEndDeserializing(string restoredVersion)
        {
            this.deserializing = false;
        }

        public void OnEndSerializing()
        {
        }

        public void OnStartDeserializing(LayoutAllowEventArgs e)
        {
            this.deserializing = true;
        }

        public void OnStartSerializing()
        {
        }

        public virtual void Parse()
        {
            switch (this.FormatType)
            {
                case DevExpress.Utils.FormatType.Numeric:
                    this._format = NumberFormatInfo.CurrentInfo;
                    if (!this.IsLoading)
                    {
                        this.fFormatString = "";
                    }
                    break;

                case DevExpress.Utils.FormatType.DateTime:
                    this._format = DateTimeFormatInfo.CurrentInfo;
                    if (!this.IsLoading)
                    {
                        this.fFormatString = "d";
                    }
                    break;

                case DevExpress.Utils.FormatType.Custom:
                    break;

                default:
                    this._format = null;
                    this.fFormatString = "";
                    break;
            }
            if (!this.IsLoading)
            {
                try
                {
                    this.TestFormatString(this.FormatString);
                }
                catch
                {
                    this.fFormatString = "";
                }
            }
            this.CheckFormatString();
        }

        public virtual void Reset()
        {
            this._format = null;
            this.fFormatType = DevExpress.Utils.FormatType.None;
            this.fFormatString = "";
            this.shouldModifyFormatString = true;
            this.OnChanged();
        }

        public static void ResetCache()
        {
            dateTimeFormatInfoCached = null;
            numberFormatInfoCached = null;
        }

        protected virtual void ResetFormatString()
        {
            this.FormatString = "";
        }

        protected virtual void ResetFormatType()
        {
            this.FormatType = DevExpress.Utils.FormatType.None;
        }

        public virtual bool ShouldSerialize() => 
            !this.IsEmpty;

        protected virtual bool ShouldSerializeFormatString() => 
            this.FormatString != "";

        protected virtual bool ShouldSerializeFormatType() => 
            this.FormatType != DevExpress.Utils.FormatType.None;

        protected virtual void TestFormatString(string format)
        {
            if ((this.Format != null) && (this.FormatType != DevExpress.Utils.FormatType.Custom))
            {
                object now = 1;
                if (this.Format is DateTimeFormatInfo)
                {
                    if (format == "d")
                    {
                        return;
                    }
                    now = DateTime.Now;
                }
                IFormattable formattable = now as IFormattable;
                if (formattable != null)
                {
                    try
                    {
                        formattable.ToString(format, this.Format);
                    }
                    catch (Exception exception)
                    {
                        throw new WarningException("Format (" + format + "): " + exception.Message, exception);
                    }
                }
            }
        }

        public override string ToString()
        {
            string str = string.Empty;
            if (this.FormatType != DevExpress.Utils.FormatType.None)
            {
                str = this.FormatType.ToString();
            }
            if (!string.IsNullOrEmpty(this.FormatString))
            {
                string[] textArray1 = new string[5];
                textArray1[0] = str;
                textArray1[1] = string.IsNullOrEmpty(str) ? string.Empty : " ";
                string[] local1 = textArray1;
                local1[2] = "\"";
                local1[3] = this.FormatString;
                local1[4] = "\"";
                str = string.Concat(local1);
            }
            return str;
        }

        private string TryFormat(object val, IFormatProvider format, string formatString)
        {
            string str = null;
            ICustomFormatter formatter = (ICustomFormatter) format.GetFormat(typeof(ICustomFormatter));
            if (formatter != null)
            {
                str = formatter.Format(formatString, val, format);
            }
            if (str == null)
            {
                IFormattable formattable = val as IFormattable;
                str = (formattable == null) ? val?.ToString() : formattable.ToString(formatString, format);
                str ??= string.Empty;
            }
            return str;
        }

        public virtual void UnlockParse()
        {
            this.fLockParse--;
        }

        [Description("Gets or sets whether a value for the FormatInfo.Format property should be determined each time the property is accessed.")]
        public static bool AlwaysUseThreadFormat
        {
            get => 
                alwaysUseThreadFormat;
            set => 
                alwaysUseThreadFormat = value;
        }

        [Browsable(false)]
        public virtual bool IsEmpty =>
            this.IsEquals(Empty);

        [Description("Gets a FormatInfo object with default settings.")]
        public static FormatInfo Empty =>
            fEmpty;

        protected bool IsDeserializing =>
            this.deserializing;

        protected virtual bool IsLoading =>
            (this.fLockParse != 0) || (this.IsComponentLoading || this.IsDeserializing);

        protected IComponentLoading ComponentLoading =>
            this.componentLoading;

        protected bool IsComponentLoading =>
            (this.ComponentLoading != null) ? this.ComponentLoading.IsLoading : false;

        [Browsable(false), DefaultValue((string) null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), NotifyParentProperty(true)]
        public virtual IFormatProvider Format
        {
            get
            {
                if (AlwaysUseThreadFormat)
                {
                    DevExpress.Utils.FormatType formatType = this.FormatType;
                    if (formatType == DevExpress.Utils.FormatType.Numeric)
                    {
                        return NumberFormatInfo.CurrentInfo;
                    }
                    if (formatType == DevExpress.Utils.FormatType.DateTime)
                    {
                        return DateTimeFormatInfo.CurrentInfo;
                    }
                }
                return this._format;
            }
            set
            {
                if (!ReferenceEquals(this.Format, value))
                {
                    this._format = value;
                    this.OnChanged();
                }
            }
        }

        [Description("Gets the pattern for formatting values."), DXDisplayName(typeof(ResFinder), "DevExpress.Utils.FormatInfo.FormatString"), NotifyParentProperty(true), XtraSerializableProperty]
        public virtual string FormatString
        {
            get => 
                this.GetViewBagProperty<string>("FormatString", this.fFormatString);
            set
            {
                value ??= "";
                if (this.FormatString != value)
                {
                    if (!this.IsLoading)
                    {
                        this.TestFormatString(value);
                    }
                    this.fFormatString = value;
                    this.SetViewBagProperty<string>("FormatString", "", this.fFormatString);
                    this.CheckFormatString();
                    this.OnChanged();
                }
            }
        }

        [Description("Gets or sets the type of formatting specified by the current FormatInfo object."), DXDisplayName(typeof(ResFinder), "DevExpress.Utils.FormatInfo.FormatType"), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, NotifyParentProperty(true)]
        public virtual DevExpress.Utils.FormatType FormatType
        {
            get => 
                this.GetViewBagProperty<DevExpress.Utils.FormatType>("FormatType", this.fFormatType);
            set
            {
                if (this.FormatType != value)
                {
                    this.fFormatType = value;
                    this.SetViewBagProperty<DevExpress.Utils.FormatType>("FormatType", DevExpress.Utils.FormatType.None, this.fFormatType);
                    this.Parse();
                    this.OnChanged();
                }
            }
        }

        private static NumberFormatInfo NumberFormatInfoCached
        {
            get
            {
                numberFormatInfoCached ??= NumberFormatInfo.CurrentInfo;
                return numberFormatInfoCached;
            }
        }

        private static DateTimeFormatInfo DateTimeFormatInfoCached
        {
            get
            {
                dateTimeFormatInfoCached ??= DateTimeFormatInfo.CurrentInfo;
                return dateTimeFormatInfoCached;
            }
        }
    }
}

