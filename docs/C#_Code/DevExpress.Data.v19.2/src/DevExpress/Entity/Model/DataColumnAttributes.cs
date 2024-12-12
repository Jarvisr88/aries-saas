namespace DevExpress.Entity.Model
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    public class DataColumnAttributes
    {
        private readonly AttributeCollection attributes;
        private readonly Lazy<System.ComponentModel.TypeConverter> typeConverterValue;
        protected readonly Func<System.ComponentModel.TypeConverter> getTypeConverterCallback;
        private readonly Lazy<DisplayAttribute> displayAttributeValue;
        private readonly Lazy<DisplayFormatAttribute> displayFormatAttributeValue;
        private readonly Lazy<DataType> dataTypeValue;
        private readonly Lazy<int> maxLengthValue;
        private readonly Lazy<int> maxLength2Value;
        private readonly Lazy<bool> requiredValue;
        private readonly Lazy<bool?> isReadOnlyValue;
        private readonly Lazy<bool?> allowEditValue;
        private readonly Lazy<bool> allowScaffoldingValue;
        private static readonly Type maxLengthAttributeType = typeof(ValidationAttribute).Assembly.GetType(typeof(ValidationAttribute).Namespace + ".MaxLengthAttribute", false);

        public DataColumnAttributes(AttributeCollection attributes, Func<System.ComponentModel.TypeConverter> getTypeConverterCallback = null)
        {
            this.attributes = attributes;
            this.getTypeConverterCallback = getTypeConverterCallback;
            Func<System.ComponentModel.TypeConverter> valueFactory = getTypeConverterCallback;
            if (getTypeConverterCallback == null)
            {
                Func<System.ComponentModel.TypeConverter> local1 = getTypeConverterCallback;
                valueFactory = <>c.<>9__54_0;
                if (<>c.<>9__54_0 == null)
                {
                    Func<System.ComponentModel.TypeConverter> local2 = <>c.<>9__54_0;
                    valueFactory = <>c.<>9__54_0 = (Func<System.ComponentModel.TypeConverter>) (() => null);
                }
            }
            this.typeConverterValue = new Lazy<System.ComponentModel.TypeConverter>(valueFactory);
            this.allowScaffoldingValue = this.ReadAttributeProperty<ScaffoldColumnAttribute, bool>(<>c.<>9__54_1 ??= x => x.Scaffold, true);
            DataColumnAttributes attributes1 = this;
            if (<>c.<>9__54_2 == null)
            {
                attributes1 = (DataColumnAttributes) (<>c.<>9__54_2 = x => new bool?(x.IsReadOnly));
            }
            bool? defaultValue = null;
            <>c.<>9__54_2.isReadOnlyValue = this.ReadAttributeProperty<ReadOnlyAttribute, bool?>((Func<ReadOnlyAttribute, bool?>) attributes1, defaultValue);
            Func<EditableAttribute, bool?> reader = <>c.<>9__54_3;
            if (<>c.<>9__54_3 == null)
            {
                Func<EditableAttribute, bool?> local5 = <>c.<>9__54_3;
                reader = <>c.<>9__54_3 = x => new bool?(x.AllowEdit);
            }
            defaultValue = null;
            this.allowEditValue = this.ReadAttributeProperty<EditableAttribute, bool?>(reader, defaultValue);
            this.maxLengthValue = this.ReadAttributeProperty<Attribute, int>(MaxLengthAttributeType, <>c.<>9__54_4 ??= x => ((int) TypeDescriptor.GetProperties(x)["Length"].GetValue(x)), 0);
            DataColumnAttributes attributes2 = this;
            if (<>c.<>9__54_5 == null)
            {
                attributes2 = (DataColumnAttributes) (<>c.<>9__54_5 = x => x.MaximumLength);
            }
            <>c.<>9__54_5.maxLength2Value = this.ReadAttributeProperty<StringLengthAttribute, int>((Func<StringLengthAttribute, int>) attributes2, 0);
            Func<DisplayFormatAttribute, DisplayFormatAttribute> func3 = <>c.<>9__54_6;
            if (<>c.<>9__54_6 == null)
            {
                Func<DisplayFormatAttribute, DisplayFormatAttribute> local8 = <>c.<>9__54_6;
                func3 = <>c.<>9__54_6 = x => x;
            }
            this.displayFormatAttributeValue = this.ReadAttributeProperty<DisplayFormatAttribute, DisplayFormatAttribute>(func3, null);
            Func<RequiredAttribute, bool> func4 = <>c.<>9__54_7;
            if (<>c.<>9__54_7 == null)
            {
                Func<RequiredAttribute, bool> local9 = <>c.<>9__54_7;
                func4 = <>c.<>9__54_7 = x => true;
            }
            this.requiredValue = this.ReadAttributeProperty<RequiredAttribute, bool>(func4, false);
            Func<DisplayAttribute, DisplayAttribute> func5 = <>c.<>9__54_8;
            if (<>c.<>9__54_8 == null)
            {
                Func<DisplayAttribute, DisplayAttribute> local10 = <>c.<>9__54_8;
                func5 = <>c.<>9__54_8 = x => x;
            }
            this.displayAttributeValue = this.ReadAttributeProperty<DisplayAttribute, DisplayAttribute>(func5, null);
            Func<DataTypeAttribute, DataType> func6 = <>c.<>9__54_9;
            if (<>c.<>9__54_9 == null)
            {
                Func<DataTypeAttribute, DataType> local11 = <>c.<>9__54_9;
                func6 = <>c.<>9__54_9 = x => x.DataType;
            }
            this.dataTypeValue = this.ReadAttributeProperty<DataTypeAttribute, DataType>(func6, DataType.Custom);
        }

        public DataColumnAttributes AddAttributes(IEnumerable<Attribute> newAttributes) => 
            new DataColumnAttributes(CombineAttributes(this.attributes, newAttributes), this.getTypeConverterCallback);

        private static AttributeCollection CombineAttributes(AttributeCollection collection, IEnumerable<Attribute> newAttributes) => 
            new AttributeCollection(collection.Cast<Attribute>().Concat<Attribute>(newAttributes).ToArray<Attribute>());

        public static TValue GetAttributeValue<TAttribute, TValue>(Type type, Func<TAttribute, TValue> reader, TValue defaultValue = null) where TAttribute: Attribute => 
            AnnotationAttributes.Reader.Read<TAttribute, TValue>(type, reader, defaultValue);

        public TValue[] GetAttributeValues<TAttribute, TValue>(Func<TAttribute, TValue> reader) where TAttribute: Attribute => 
            AnnotationAttributes.Reader.Read<TAttribute, TValue>(this.attributes, reader);

        public Lazy<TValue> ReadAttributeProperty<TAttribute, TValue>(Func<TAttribute, TValue> reader, TValue defaultValue = null) where TAttribute: Attribute => 
            this.ReadAttributeProperty<TAttribute, TValue>(typeof(TAttribute), reader, defaultValue);

        public TValue ReadAttributeProperty<TAttribute, TValue>(Lazy<TAttribute> lazyAttributeValue, Func<TAttribute, TValue> reader, TValue defaultValue = null) where TAttribute: Attribute => 
            (lazyAttributeValue.Value == null) ? defaultValue : reader(lazyAttributeValue.Value);

        public Lazy<TValue> ReadAttributeProperty<TAttribute, TValue>(Type attributeType, Func<TAttribute, TValue> reader, TValue defaultValue = null) where TAttribute: Attribute => 
            new Lazy<TValue>(() => AnnotationAttributes.Reader.Read<TAttribute, TValue>(attributeType, this.attributes, reader, defaultValue));

        public DataType DataTypeValue =>
            this.dataTypeValue.Value;

        public bool? AllowEdit =>
            this.allowEditValue.Value;

        public bool? IsReadOnly =>
            this.isReadOnlyValue.Value;

        public bool AllowScaffolding =>
            this.allowScaffoldingValue.Value;

        public int MaxLengthValue =>
            this.maxLengthValue.Value;

        public int MaxLength2Value =>
            this.maxLength2Value.Value;

        public bool RequiredValue =>
            this.requiredValue.Value;

        public DisplayAttribute DisplayAttributeValue =>
            this.displayAttributeValue.Value;

        public System.ComponentModel.TypeConverter TypeConverter =>
            this.typeConverterValue.Value;

        public bool? AutoGenerateField
        {
            get
            {
                Func<DisplayAttribute, bool?> reader = <>c.<>9__31_0;
                if (<>c.<>9__31_0 == null)
                {
                    Func<DisplayAttribute, bool?> local1 = <>c.<>9__31_0;
                    reader = <>c.<>9__31_0 = x => x.GetAutoGenerateField();
                }
                bool? defaultValue = null;
                return this.ReadAttributeProperty<DisplayAttribute, bool?>(this.displayAttributeValue, reader, defaultValue);
            }
        }

        public string Description
        {
            get
            {
                Func<DisplayAttribute, string> reader = <>c.<>9__33_0;
                if (<>c.<>9__33_0 == null)
                {
                    Func<DisplayAttribute, string> local1 = <>c.<>9__33_0;
                    reader = <>c.<>9__33_0 = x => x.GetDescription();
                }
                return this.ReadAttributeProperty<DisplayAttribute, string>(this.displayAttributeValue, reader, null);
            }
        }

        public string GroupName
        {
            get
            {
                Func<DisplayAttribute, string> reader = <>c.<>9__35_0;
                if (<>c.<>9__35_0 == null)
                {
                    Func<DisplayAttribute, string> local1 = <>c.<>9__35_0;
                    reader = <>c.<>9__35_0 = x => x.GetGroupName();
                }
                return this.ReadAttributeProperty<DisplayAttribute, string>(this.displayAttributeValue, reader, null);
            }
        }

        public int? Order
        {
            get
            {
                Func<DisplayAttribute, int?> reader = <>c.<>9__37_0;
                if (<>c.<>9__37_0 == null)
                {
                    Func<DisplayAttribute, int?> local1 = <>c.<>9__37_0;
                    reader = <>c.<>9__37_0 = x => x.GetOrder();
                }
                int? defaultValue = null;
                return this.ReadAttributeProperty<DisplayAttribute, int?>(this.displayAttributeValue, reader, defaultValue);
            }
        }

        public string Name
        {
            get
            {
                Func<DisplayAttribute, string> reader = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Func<DisplayAttribute, string> local1 = <>c.<>9__39_0;
                    reader = <>c.<>9__39_0 = x => x.GetName();
                }
                return this.ReadAttributeProperty<DisplayAttribute, string>(this.displayAttributeValue, reader, null);
            }
        }

        public string ShortName
        {
            get
            {
                Func<DisplayAttribute, string> reader = <>c.<>9__41_0;
                if (<>c.<>9__41_0 == null)
                {
                    Func<DisplayAttribute, string> local1 = <>c.<>9__41_0;
                    reader = <>c.<>9__41_0 = x => x.GetShortName();
                }
                return this.ReadAttributeProperty<DisplayAttribute, string>(this.displayAttributeValue, reader, null);
            }
        }

        public bool ApplyFormatInEditMode
        {
            get
            {
                Func<DisplayFormatAttribute, bool> reader = <>c.<>9__43_0;
                if (<>c.<>9__43_0 == null)
                {
                    Func<DisplayFormatAttribute, bool> local1 = <>c.<>9__43_0;
                    reader = <>c.<>9__43_0 = x => x.ApplyFormatInEditMode;
                }
                return this.ReadAttributeProperty<DisplayFormatAttribute, bool>(this.displayFormatAttributeValue, reader, false);
            }
        }

        public bool ConvertEmptyStringToNull
        {
            get
            {
                Func<DisplayFormatAttribute, bool> reader = <>c.<>9__45_0;
                if (<>c.<>9__45_0 == null)
                {
                    Func<DisplayFormatAttribute, bool> local1 = <>c.<>9__45_0;
                    reader = <>c.<>9__45_0 = x => x.ConvertEmptyStringToNull;
                }
                return this.ReadAttributeProperty<DisplayFormatAttribute, bool>(this.displayFormatAttributeValue, reader, true);
            }
        }

        public string DataFormatString
        {
            get
            {
                Func<DisplayFormatAttribute, string> reader = <>c.<>9__47_0;
                if (<>c.<>9__47_0 == null)
                {
                    Func<DisplayFormatAttribute, string> local1 = <>c.<>9__47_0;
                    reader = <>c.<>9__47_0 = x => x.DataFormatString;
                }
                return this.ReadAttributeProperty<DisplayFormatAttribute, string>(this.displayFormatAttributeValue, reader, null);
            }
        }

        public string NullDisplayText
        {
            get
            {
                Func<DisplayFormatAttribute, string> reader = <>c.<>9__49_0;
                if (<>c.<>9__49_0 == null)
                {
                    Func<DisplayFormatAttribute, string> local1 = <>c.<>9__49_0;
                    reader = <>c.<>9__49_0 = x => x.NullDisplayText;
                }
                return this.ReadAttributeProperty<DisplayFormatAttribute, string>(this.displayFormatAttributeValue, reader, null);
            }
        }

        internal static Type MaxLengthAttributeType =>
            maxLengthAttributeType;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataColumnAttributes.<>c <>9 = new DataColumnAttributes.<>c();
            public static Func<DisplayAttribute, bool?> <>9__31_0;
            public static Func<DisplayAttribute, string> <>9__33_0;
            public static Func<DisplayAttribute, string> <>9__35_0;
            public static Func<DisplayAttribute, int?> <>9__37_0;
            public static Func<DisplayAttribute, string> <>9__39_0;
            public static Func<DisplayAttribute, string> <>9__41_0;
            public static Func<DisplayFormatAttribute, bool> <>9__43_0;
            public static Func<DisplayFormatAttribute, bool> <>9__45_0;
            public static Func<DisplayFormatAttribute, string> <>9__47_0;
            public static Func<DisplayFormatAttribute, string> <>9__49_0;
            public static Func<TypeConverter> <>9__54_0;
            public static Func<ScaffoldColumnAttribute, bool> <>9__54_1;
            public static Func<ReadOnlyAttribute, bool?> <>9__54_2;
            public static Func<EditableAttribute, bool?> <>9__54_3;
            public static Func<Attribute, int> <>9__54_4;
            public static Func<StringLengthAttribute, int> <>9__54_5;
            public static Func<DisplayFormatAttribute, DisplayFormatAttribute> <>9__54_6;
            public static Func<RequiredAttribute, bool> <>9__54_7;
            public static Func<DisplayAttribute, DisplayAttribute> <>9__54_8;
            public static Func<DataTypeAttribute, DataType> <>9__54_9;

            internal TypeConverter <.ctor>b__54_0() => 
                null;

            internal bool <.ctor>b__54_1(ScaffoldColumnAttribute x) => 
                x.Scaffold;

            internal bool? <.ctor>b__54_2(ReadOnlyAttribute x) => 
                new bool?(x.IsReadOnly);

            internal bool? <.ctor>b__54_3(EditableAttribute x) => 
                new bool?(x.AllowEdit);

            internal int <.ctor>b__54_4(Attribute x) => 
                (int) TypeDescriptor.GetProperties(x)["Length"].GetValue(x);

            internal int <.ctor>b__54_5(StringLengthAttribute x) => 
                x.MaximumLength;

            internal DisplayFormatAttribute <.ctor>b__54_6(DisplayFormatAttribute x) => 
                x;

            internal bool <.ctor>b__54_7(RequiredAttribute x) => 
                true;

            internal DisplayAttribute <.ctor>b__54_8(DisplayAttribute x) => 
                x;

            internal DataType <.ctor>b__54_9(DataTypeAttribute x) => 
                x.DataType;

            internal bool <get_ApplyFormatInEditMode>b__43_0(DisplayFormatAttribute x) => 
                x.ApplyFormatInEditMode;

            internal bool? <get_AutoGenerateField>b__31_0(DisplayAttribute x) => 
                x.GetAutoGenerateField();

            internal bool <get_ConvertEmptyStringToNull>b__45_0(DisplayFormatAttribute x) => 
                x.ConvertEmptyStringToNull;

            internal string <get_DataFormatString>b__47_0(DisplayFormatAttribute x) => 
                x.DataFormatString;

            internal string <get_Description>b__33_0(DisplayAttribute x) => 
                x.GetDescription();

            internal string <get_GroupName>b__35_0(DisplayAttribute x) => 
                x.GetGroupName();

            internal string <get_Name>b__39_0(DisplayAttribute x) => 
                x.GetName();

            internal string <get_NullDisplayText>b__49_0(DisplayFormatAttribute x) => 
                x.NullDisplayText;

            internal int? <get_Order>b__37_0(DisplayAttribute x) => 
                x.GetOrder();

            internal string <get_ShortName>b__41_0(DisplayAttribute x) => 
                x.GetShortName();
        }
    }
}

