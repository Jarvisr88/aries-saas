namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Mvvm.UI.ViewGenerator.Metadata;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security;

    [SecuritySafeCritical]
    public static class DataColumnAttributesExtensions
    {
        public static string CommandParameterName(this DataColumnAttributes attributes)
        {
            Func<CommandParameterAttribute, string> reader = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<CommandParameterAttribute, string> local1 = <>c.<>9__10_0;
                reader = <>c.<>9__10_0 = x => x.CommandParameter;
            }
            return attributes.ReadAttributeProperty<CommandParameterAttribute, string>(reader, null).Value;
        }

        public static PropertyValidator CreatePropertyValidator(PropertyDescriptor property, Type ownerType)
        {
            Type componentType = ownerType;
            if (ownerType == null)
            {
                Type local1 = ownerType;
                componentType = property.ComponentType;
            }
            return PropertyValidator.FromAttributes(DataColumnAttributesProvider.GetAttributesCore(property, componentType, null), property.Name);
        }

        public static DevExpress.Mvvm.Native.DataFormGroupAttribute DataFormGroupAttribute(this DataColumnAttributes attribute)
        {
            Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute> reader = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute> local1 = <>c.<>9__3_0;
                reader = <>c.<>9__3_0 = x => x;
            }
            return attribute.ReadAttributeProperty<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute>(reader, null).Value;
        }

        public static DXImageAttribute DXImage(this DataColumnAttributes attribute)
        {
            Func<DXImageAttribute, DXImageAttribute> reader = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<DXImageAttribute, DXImageAttribute> local1 = <>c.<>9__15_0;
                reader = <>c.<>9__15_0 = x => x;
            }
            return attribute.ReadAttributeProperty<DXImageAttribute, DXImageAttribute>(reader, null).Value;
        }

        public static TypeConverter GetActualTypeConverter(PropertyDescriptor property, Type ownerType, TypeConverter converter) => 
            DataColumnAttributesProvider.GetAttributes(property, ownerType, null).ReadAttributeProperty<TypeConverterWrapperAttribute, TypeConverter>(x => x.WrapTypeConverter(converter), converter).Value;

        public static string GetGroupName(this DataColumnAttributes attributes, LayoutType layoutType)
        {
            Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute> reader = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute> local1 = <>c.<>9__17_0;
                reader = <>c.<>9__17_0 = x => x;
            }
            Lazy<DevExpress.Mvvm.Native.DataFormGroupAttribute> lazyAttributeValue = attributes.ReadAttributeProperty<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute>(reader, null);
            Func<TableGroupAttribute, TableGroupAttribute> func2 = <>c.<>9__17_1;
            if (<>c.<>9__17_1 == null)
            {
                Func<TableGroupAttribute, TableGroupAttribute> local2 = <>c.<>9__17_1;
                func2 = <>c.<>9__17_1 = x => x;
            }
            Lazy<TableGroupAttribute> lazy2 = attributes.ReadAttributeProperty<TableGroupAttribute, TableGroupAttribute>(func2, null);
            if ((layoutType == LayoutType.DataForm) && (lazyAttributeValue.Value != null))
            {
                Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, string> func3 = <>c.<>9__17_2;
                if (<>c.<>9__17_2 == null)
                {
                    Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, string> local3 = <>c.<>9__17_2;
                    func3 = <>c.<>9__17_2 = x => x.GroupName;
                }
                return attributes.ReadAttributeProperty<DevExpress.Mvvm.Native.DataFormGroupAttribute, string>(lazyAttributeValue, func3, null);
            }
            if ((layoutType != LayoutType.Table) || (lazy2.Value == null))
            {
                return attributes.GroupName;
            }
            Func<TableGroupAttribute, string> func4 = <>c.<>9__17_3;
            if (<>c.<>9__17_3 == null)
            {
                Func<TableGroupAttribute, string> local4 = <>c.<>9__17_3;
                func4 = <>c.<>9__17_3 = x => x.GroupName;
            }
            return attributes.ReadAttributeProperty<TableGroupAttribute, string>(lazy2, func4, null);
        }

        public static int? GetOrder(this DataColumnAttributes attributes, LayoutType layoutType)
        {
            int? nullable;
            Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute> reader = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute> local1 = <>c.<>9__18_0;
                reader = <>c.<>9__18_0 = x => x;
            }
            Lazy<DevExpress.Mvvm.Native.DataFormGroupAttribute> lazyAttributeValue = attributes.ReadAttributeProperty<DevExpress.Mvvm.Native.DataFormGroupAttribute, DevExpress.Mvvm.Native.DataFormGroupAttribute>(reader, null);
            Func<TableGroupAttribute, TableGroupAttribute> func2 = <>c.<>9__18_1;
            if (<>c.<>9__18_1 == null)
            {
                Func<TableGroupAttribute, TableGroupAttribute> local2 = <>c.<>9__18_1;
                func2 = <>c.<>9__18_1 = x => x;
            }
            Lazy<TableGroupAttribute> lazy2 = attributes.ReadAttributeProperty<TableGroupAttribute, TableGroupAttribute>(func2, null);
            Func<ToolBarItemAttribute, ToolBarItemAttribute> func3 = <>c.<>9__18_2;
            if (<>c.<>9__18_2 == null)
            {
                Func<ToolBarItemAttribute, ToolBarItemAttribute> local3 = <>c.<>9__18_2;
                func3 = <>c.<>9__18_2 = x => x;
            }
            Lazy<ToolBarItemAttribute> lazy3 = attributes.ReadAttributeProperty<ToolBarItemAttribute, ToolBarItemAttribute>(func3, null);
            Func<ContextMenuItemAttribute, ContextMenuItemAttribute> func4 = <>c.<>9__18_3;
            if (<>c.<>9__18_3 == null)
            {
                Func<ContextMenuItemAttribute, ContextMenuItemAttribute> local4 = <>c.<>9__18_3;
                func4 = <>c.<>9__18_3 = x => x;
            }
            Lazy<ContextMenuItemAttribute> lazy4 = attributes.ReadAttributeProperty<ContextMenuItemAttribute, ContextMenuItemAttribute>(func4, null);
            if ((layoutType == LayoutType.DataForm) && (lazyAttributeValue.Value != null))
            {
                Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, int> func5 = <>c.<>9__18_4;
                if (<>c.<>9__18_4 == null)
                {
                    Func<DevExpress.Mvvm.Native.DataFormGroupAttribute, int> local5 = <>c.<>9__18_4;
                    func5 = <>c.<>9__18_4 = x => x.Order;
                }
                return new int?(attributes.ReadAttributeProperty<DevExpress.Mvvm.Native.DataFormGroupAttribute, int>(lazyAttributeValue, func5, 0));
            }
            if ((layoutType == LayoutType.Table) && (lazy2.Value != null))
            {
                Func<TableGroupAttribute, int> func6 = <>c.<>9__18_5;
                if (<>c.<>9__18_5 == null)
                {
                    Func<TableGroupAttribute, int> local6 = <>c.<>9__18_5;
                    func6 = <>c.<>9__18_5 = x => x.Order;
                }
                return new int?(attributes.ReadAttributeProperty<TableGroupAttribute, int>(lazy2, func6, 0));
            }
            if ((layoutType == LayoutType.ToolBar) && (lazy3.Value != null))
            {
                Func<ToolBarItemAttribute, int?> func7 = <>c.<>9__18_6;
                if (<>c.<>9__18_6 == null)
                {
                    Func<ToolBarItemAttribute, int?> local7 = <>c.<>9__18_6;
                    func7 = <>c.<>9__18_6 = x => x.GetOrder();
                }
                nullable = null;
                return attributes.ReadAttributeProperty<ToolBarItemAttribute, int?>(lazy3, func7, nullable);
            }
            if ((layoutType != LayoutType.ContextMenu) || (lazy4.Value == null))
            {
                return attributes.Order;
            }
            Func<ContextMenuItemAttribute, int?> func8 = <>c.<>9__18_7;
            if (<>c.<>9__18_7 == null)
            {
                Func<ContextMenuItemAttribute, int?> local8 = <>c.<>9__18_7;
                func8 = <>c.<>9__18_7 = x => x.GetOrder();
            }
            nullable = null;
            return attributes.ReadAttributeProperty<ContextMenuItemAttribute, int?>(lazy4, func8, nullable);
        }

        public static string GetToolBarPageGroupName(this DataColumnAttributes attributes, LayoutType layoutType)
        {
            Func<ToolBarItemAttribute, ToolBarItemAttribute> reader = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<ToolBarItemAttribute, ToolBarItemAttribute> local1 = <>c.<>9__19_0;
                reader = <>c.<>9__19_0 = x => x;
            }
            Lazy<ToolBarItemAttribute> lazyAttributeValue = attributes.ReadAttributeProperty<ToolBarItemAttribute, ToolBarItemAttribute>(reader, null);
            Func<ContextMenuItemAttribute, ContextMenuItemAttribute> func2 = <>c.<>9__19_1;
            if (<>c.<>9__19_1 == null)
            {
                Func<ContextMenuItemAttribute, ContextMenuItemAttribute> local2 = <>c.<>9__19_1;
                func2 = <>c.<>9__19_1 = x => x;
            }
            Lazy<ContextMenuItemAttribute> lazy2 = attributes.ReadAttributeProperty<ContextMenuItemAttribute, ContextMenuItemAttribute>(func2, null);
            if ((layoutType == LayoutType.ToolBar) && (lazyAttributeValue.Value != null))
            {
                Func<ToolBarItemAttribute, string> func3 = <>c.<>9__19_2;
                if (<>c.<>9__19_2 == null)
                {
                    Func<ToolBarItemAttribute, string> local3 = <>c.<>9__19_2;
                    func3 = <>c.<>9__19_2 = x => x.PageGroup;
                }
                return attributes.ReadAttributeProperty<ToolBarItemAttribute, string>(lazyAttributeValue, func3, null);
            }
            if ((layoutType != LayoutType.ContextMenu) || (lazy2.Value == null))
            {
                return null;
            }
            Func<ContextMenuItemAttribute, string> func4 = <>c.<>9__19_3;
            if (<>c.<>9__19_3 == null)
            {
                Func<ContextMenuItemAttribute, string> local4 = <>c.<>9__19_3;
                func4 = <>c.<>9__19_3 = x => x.Group;
            }
            return attributes.ReadAttributeProperty<ContextMenuItemAttribute, string>(lazy2, func4, null);
        }

        public static bool Hidden(this DataColumnAttributes attribute)
        {
            Func<HiddenAttribute, bool> reader = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<HiddenAttribute, bool> local1 = <>c.<>9__14_0;
                reader = <>c.<>9__14_0 = x => x.Hidden;
            }
            return attribute.ReadAttributeProperty<HiddenAttribute, bool>(reader, false).Value;
        }

        public static ImageAttribute Image(this DataColumnAttributes attribute)
        {
            Func<ImageAttribute, ImageAttribute> reader = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Func<ImageAttribute, ImageAttribute> local1 = <>c.<>9__16_0;
                reader = <>c.<>9__16_0 = x => x;
            }
            return attribute.ReadAttributeProperty<ImageAttribute, ImageAttribute>(reader, null).Value;
        }

        public static IInstanceInitializer InstanceInitializer(this DataColumnAttributes attrib) => 
            attrib.InstanceInitializerCore<InstanceInitializerAttribute>();

        private static IInstanceInitializer InstanceInitializerCore<TInstanceInitializerAttribute>(this DataColumnAttributes attrib) where TInstanceInitializerAttribute: InstanceInitializerAttributeBase
        {
            Func<TInstanceInitializerAttribute, TInstanceInitializerAttribute> reader = <>c__9<TInstanceInitializerAttribute>.<>9__9_0;
            if (<>c__9<TInstanceInitializerAttribute>.<>9__9_0 == null)
            {
                Func<TInstanceInitializerAttribute, TInstanceInitializerAttribute> local1 = <>c__9<TInstanceInitializerAttribute>.<>9__9_0;
                reader = <>c__9<TInstanceInitializerAttribute>.<>9__9_0 = x => x;
            }
            TInstanceInitializerAttribute[] attributeValues = attrib.GetAttributeValues<TInstanceInitializerAttribute, TInstanceInitializerAttribute>(reader);
            return ((attributeValues.Length != 0) ? new AttributeInstanceInitializer(attributeValues) : null);
        }

        public static bool IsContextMenuItem(this DataColumnAttributes attributes)
        {
            Func<ContextMenuItemAttribute, ContextMenuItemAttribute> reader = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<ContextMenuItemAttribute, ContextMenuItemAttribute> local1 = <>c.<>9__6_0;
                reader = <>c.<>9__6_0 = x => x;
            }
            Lazy<ContextMenuItemAttribute> lazyAttributeValue = attributes.ReadAttributeProperty<ContextMenuItemAttribute, ContextMenuItemAttribute>(reader, null);
            return attributes.ReadAttributeProperty<ContextMenuItemAttribute, bool>(lazyAttributeValue, <>c.<>9__6_1 ??= x => true, false);
        }

        public static MaskAttributeBase Mask(this DataColumnAttributes attribute)
        {
            Func<MaskAttributeBase, MaskAttributeBase> reader = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<MaskAttributeBase, MaskAttributeBase> local1 = <>c.<>9__4_0;
                reader = <>c.<>9__4_0 = x => x;
            }
            return attribute.ReadAttributeProperty<MaskAttributeBase, MaskAttributeBase>(reader, null).Value;
        }

        public static int MaxLength(this DataColumnAttributes attribute)
        {
            Func<DXMaxLengthAttribute, int> reader = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<DXMaxLengthAttribute, int> local1 = <>c.<>9__12_0;
                reader = <>c.<>9__12_0 = x => x.Length;
            }
            Lazy<int> lazy = attribute.ReadAttributeProperty<DXMaxLengthAttribute, int>(reader, 0);
            return ((lazy.Value <= 0) ? ((attribute.MaxLength2Value <= 0) ? attribute.MaxLengthValue : attribute.MaxLength2Value) : lazy.Value);
        }

        public static bool NeedParenthesis(this DataColumnAttributes attributes)
        {
            Func<ParenthesizePropertyNameAttribute, bool> reader = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Func<ParenthesizePropertyNameAttribute, bool> local1 = <>c.<>9__21_0;
                reader = <>c.<>9__21_0 = x => x.NeedParenthesis;
            }
            return attributes.ReadAttributeProperty<ParenthesizePropertyNameAttribute, bool>(reader, false).Value;
        }

        public static IInstanceInitializer NewItemInstanceInitializer(this DataColumnAttributes attrib) => 
            attrib.InstanceInitializerCore<NewItemInstanceInitializerAttribute>();

        public static DevExpress.Mvvm.Native.PropertyDataType PropertyDataType(this DataColumnAttributes attributes)
        {
            Func<DXDataTypeAttribute, DevExpress.Mvvm.Native.PropertyDataType> reader = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<DXDataTypeAttribute, DevExpress.Mvvm.Native.PropertyDataType> local1 = <>c.<>9__11_0;
                reader = <>c.<>9__11_0 = x => x.DataType;
            }
            Lazy<DevExpress.Mvvm.Native.PropertyDataType> lazy = attributes.ReadAttributeProperty<DXDataTypeAttribute, DevExpress.Mvvm.Native.PropertyDataType>(reader, DevExpress.Mvvm.Native.PropertyDataType.Custom);
            return ((((DevExpress.Mvvm.Native.PropertyDataType) lazy.Value) != DevExpress.Mvvm.Native.PropertyDataType.Custom) ? lazy.Value : DataAnnotationsAttributeHelper.FromDataType(attributes.DataTypeValue));
        }

        public static bool Required(this DataColumnAttributes attribute)
        {
            if (attribute.RequiredValue)
            {
                return true;
            }
            Func<DXRequiredAttribute, bool> reader = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<DXRequiredAttribute, bool> local1 = <>c.<>9__13_0;
                reader = <>c.<>9__13_0 = x => true;
            }
            return attribute.ReadAttributeProperty<DXRequiredAttribute, bool>(reader, false).Value;
        }

        public static bool ScaffoldDetailCollection(this DataColumnAttributes attributes)
        {
            Func<ScaffoldDetailCollectionAttribute, bool> reader = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                Func<ScaffoldDetailCollectionAttribute, bool> local1 = <>c.<>9__20_0;
                reader = <>c.<>9__20_0 = x => x.Scaffold;
            }
            return attributes.ReadAttributeProperty<ScaffoldDetailCollectionAttribute, bool>(reader, true).Value;
        }

        public static string ToolBarPageName(this DataColumnAttributes attributes)
        {
            Func<ToolBarItemAttribute, ToolBarItemAttribute> reader = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<ToolBarItemAttribute, ToolBarItemAttribute> local1 = <>c.<>9__5_0;
                reader = <>c.<>9__5_0 = x => x;
            }
            Lazy<ToolBarItemAttribute> lazyAttributeValue = attributes.ReadAttributeProperty<ToolBarItemAttribute, ToolBarItemAttribute>(reader, null);
            string local3 = attributes.ReadAttributeProperty<ToolBarItemAttribute, string>(lazyAttributeValue, <>c.<>9__5_1 ??= x => x.Page, null);
            string groupName = local3;
            if (local3 == null)
            {
                string local4 = local3;
                groupName = attributes.GroupName;
            }
            return groupName;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataColumnAttributesExtensions.<>c <>9 = new DataColumnAttributesExtensions.<>c();
            public static Func<DataFormGroupAttribute, DataFormGroupAttribute> <>9__3_0;
            public static Func<MaskAttributeBase, MaskAttributeBase> <>9__4_0;
            public static Func<ToolBarItemAttribute, ToolBarItemAttribute> <>9__5_0;
            public static Func<ToolBarItemAttribute, string> <>9__5_1;
            public static Func<ContextMenuItemAttribute, ContextMenuItemAttribute> <>9__6_0;
            public static Func<ContextMenuItemAttribute, bool> <>9__6_1;
            public static Func<CommandParameterAttribute, string> <>9__10_0;
            public static Func<DXDataTypeAttribute, PropertyDataType> <>9__11_0;
            public static Func<DXMaxLengthAttribute, int> <>9__12_0;
            public static Func<DXRequiredAttribute, bool> <>9__13_0;
            public static Func<HiddenAttribute, bool> <>9__14_0;
            public static Func<DXImageAttribute, DXImageAttribute> <>9__15_0;
            public static Func<ImageAttribute, ImageAttribute> <>9__16_0;
            public static Func<DataFormGroupAttribute, DataFormGroupAttribute> <>9__17_0;
            public static Func<TableGroupAttribute, TableGroupAttribute> <>9__17_1;
            public static Func<DataFormGroupAttribute, string> <>9__17_2;
            public static Func<TableGroupAttribute, string> <>9__17_3;
            public static Func<DataFormGroupAttribute, DataFormGroupAttribute> <>9__18_0;
            public static Func<TableGroupAttribute, TableGroupAttribute> <>9__18_1;
            public static Func<ToolBarItemAttribute, ToolBarItemAttribute> <>9__18_2;
            public static Func<ContextMenuItemAttribute, ContextMenuItemAttribute> <>9__18_3;
            public static Func<DataFormGroupAttribute, int> <>9__18_4;
            public static Func<TableGroupAttribute, int> <>9__18_5;
            public static Func<ToolBarItemAttribute, int?> <>9__18_6;
            public static Func<ContextMenuItemAttribute, int?> <>9__18_7;
            public static Func<ToolBarItemAttribute, ToolBarItemAttribute> <>9__19_0;
            public static Func<ContextMenuItemAttribute, ContextMenuItemAttribute> <>9__19_1;
            public static Func<ToolBarItemAttribute, string> <>9__19_2;
            public static Func<ContextMenuItemAttribute, string> <>9__19_3;
            public static Func<ScaffoldDetailCollectionAttribute, bool> <>9__20_0;
            public static Func<ParenthesizePropertyNameAttribute, bool> <>9__21_0;

            internal string <CommandParameterName>b__10_0(CommandParameterAttribute x) => 
                x.CommandParameter;

            internal DataFormGroupAttribute <DataFormGroupAttribute>b__3_0(DataFormGroupAttribute x) => 
                x;

            internal DXImageAttribute <DXImage>b__15_0(DXImageAttribute x) => 
                x;

            internal DataFormGroupAttribute <GetGroupName>b__17_0(DataFormGroupAttribute x) => 
                x;

            internal TableGroupAttribute <GetGroupName>b__17_1(TableGroupAttribute x) => 
                x;

            internal string <GetGroupName>b__17_2(DataFormGroupAttribute x) => 
                x.GroupName;

            internal string <GetGroupName>b__17_3(TableGroupAttribute x) => 
                x.GroupName;

            internal DataFormGroupAttribute <GetOrder>b__18_0(DataFormGroupAttribute x) => 
                x;

            internal TableGroupAttribute <GetOrder>b__18_1(TableGroupAttribute x) => 
                x;

            internal ToolBarItemAttribute <GetOrder>b__18_2(ToolBarItemAttribute x) => 
                x;

            internal ContextMenuItemAttribute <GetOrder>b__18_3(ContextMenuItemAttribute x) => 
                x;

            internal int <GetOrder>b__18_4(DataFormGroupAttribute x) => 
                x.Order;

            internal int <GetOrder>b__18_5(TableGroupAttribute x) => 
                x.Order;

            internal int? <GetOrder>b__18_6(ToolBarItemAttribute x) => 
                x.GetOrder();

            internal int? <GetOrder>b__18_7(ContextMenuItemAttribute x) => 
                x.GetOrder();

            internal ToolBarItemAttribute <GetToolBarPageGroupName>b__19_0(ToolBarItemAttribute x) => 
                x;

            internal ContextMenuItemAttribute <GetToolBarPageGroupName>b__19_1(ContextMenuItemAttribute x) => 
                x;

            internal string <GetToolBarPageGroupName>b__19_2(ToolBarItemAttribute x) => 
                x.PageGroup;

            internal string <GetToolBarPageGroupName>b__19_3(ContextMenuItemAttribute x) => 
                x.Group;

            internal bool <Hidden>b__14_0(HiddenAttribute x) => 
                x.Hidden;

            internal ImageAttribute <Image>b__16_0(ImageAttribute x) => 
                x;

            internal ContextMenuItemAttribute <IsContextMenuItem>b__6_0(ContextMenuItemAttribute x) => 
                x;

            internal bool <IsContextMenuItem>b__6_1(ContextMenuItemAttribute x) => 
                true;

            internal MaskAttributeBase <Mask>b__4_0(MaskAttributeBase x) => 
                x;

            internal int <MaxLength>b__12_0(DXMaxLengthAttribute x) => 
                x.Length;

            internal bool <NeedParenthesis>b__21_0(ParenthesizePropertyNameAttribute x) => 
                x.NeedParenthesis;

            internal PropertyDataType <PropertyDataType>b__11_0(DXDataTypeAttribute x) => 
                x.DataType;

            internal bool <Required>b__13_0(DXRequiredAttribute x) => 
                true;

            internal bool <ScaffoldDetailCollection>b__20_0(ScaffoldDetailCollectionAttribute x) => 
                x.Scaffold;

            internal ToolBarItemAttribute <ToolBarPageName>b__5_0(ToolBarItemAttribute x) => 
                x;

            internal string <ToolBarPageName>b__5_1(ToolBarItemAttribute x) => 
                x.Page;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__9<TInstanceInitializerAttribute> where TInstanceInitializerAttribute: InstanceInitializerAttributeBase
        {
            public static readonly DataColumnAttributesExtensions.<>c__9<TInstanceInitializerAttribute> <>9;
            public static Func<TInstanceInitializerAttribute, TInstanceInitializerAttribute> <>9__9_0;

            static <>c__9()
            {
                DataColumnAttributesExtensions.<>c__9<TInstanceInitializerAttribute>.<>9 = new DataColumnAttributesExtensions.<>c__9<TInstanceInitializerAttribute>();
            }

            internal TInstanceInitializerAttribute <InstanceInitializerCore>b__9_0(TInstanceInitializerAttribute x) => 
                x;
        }

        private class AttributeInstanceInitializer : IInstanceInitializer, IDictionaryItemInstanceInitializer
        {
            private readonly InstanceInitializerAttributeBase[] attributes;
            private readonly TypeInfo[] typeInfo;

            public AttributeInstanceInitializer(IEnumerable<InstanceInitializerAttributeBase> attributes)
            {
                this.attributes = attributes.ToArray<InstanceInitializerAttributeBase>();
                Func<InstanceInitializerAttributeBase, string> keySelector = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<InstanceInitializerAttributeBase, string> local1 = <>c.<>9__2_0;
                    keySelector = <>c.<>9__2_0 = x => x.Name;
                }
                Func<InstanceInitializerAttributeBase, TypeInfo> selector = <>c.<>9__2_1;
                if (<>c.<>9__2_1 == null)
                {
                    Func<InstanceInitializerAttributeBase, TypeInfo> local2 = <>c.<>9__2_1;
                    selector = <>c.<>9__2_1 = x => new TypeInfo(x.Type, x.Name);
                }
                this.typeInfo = attributes.OrderBy<InstanceInitializerAttributeBase, string>(keySelector).Select<InstanceInitializerAttributeBase, TypeInfo>(selector).ToArray<TypeInfo>();
            }

            KeyValuePair<object, object>? IDictionaryItemInstanceInitializer.CreateInstance(TypeInfo type, ITypeDescriptorContext context, IEnumerable dictionary)
            {
                NewItemInstanceInitializerAttribute attribute = this.attributes.OfType<NewItemInstanceInitializerAttribute>().FirstOrDefault<NewItemInstanceInitializerAttribute>(x => (x.Name == type.Name) && (x.Type == type.Type));
                if (attribute == null)
                {
                    throw new ArgumentException("type");
                }
                return attribute.CreateInstance(context, dictionary);
            }

            object IInstanceInitializer.CreateInstance(TypeInfo type)
            {
                InstanceInitializerAttributeBase base2 = this.attributes.FirstOrDefault<InstanceInitializerAttributeBase>(x => (x.Name == type.Name) && (x.Type == type.Type));
                if (base2 == null)
                {
                    throw new ArgumentException("type");
                }
                return base2.CreateInstance();
            }

            IEnumerable<TypeInfo> IInstanceInitializer.Types =>
                this.typeInfo;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DataColumnAttributesExtensions.AttributeInstanceInitializer.<>c <>9 = new DataColumnAttributesExtensions.AttributeInstanceInitializer.<>c();
                public static Func<InstanceInitializerAttributeBase, string> <>9__2_0;
                public static Func<InstanceInitializerAttributeBase, TypeInfo> <>9__2_1;

                internal string <.ctor>b__2_0(InstanceInitializerAttributeBase x) => 
                    x.Name;

                internal TypeInfo <.ctor>b__2_1(InstanceInitializerAttributeBase x) => 
                    new TypeInfo(x.Type, x.Name);
            }
        }
    }
}

