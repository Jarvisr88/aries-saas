namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class EditorsSource
    {
        public static HorizontalAlignment CurrencyValueAlignment = HorizontalAlignment.Right;
        public static double MultilineTextMinHeight = 50.0;
        public static string PhoneNumberMask = "(000) 000-0000";
        public static readonly Type[] NumericIntegerTypes = new Type[] { typeof(int), typeof(uint), typeof(short), typeof(ushort), typeof(byte), typeof(long), typeof(ulong) };
        public static readonly Type[] NumericFloatTypes = new Type[] { typeof(double), typeof(decimal), typeof(float) };
        private static readonly string[] DisplayMemberNames;

        static EditorsSource()
        {
            string[] textArray1 = new string[11];
            textArray1[0] = "fullname";
            textArray1[1] = "lastname";
            textArray1[2] = "name";
            textArray1[3] = "title";
            textArray1[4] = "caption";
            textArray1[5] = "displaytext";
            textArray1[6] = "subject";
            textArray1[7] = "description";
            textArray1[8] = "text";
            textArray1[9] = "subj";
            textArray1[10] = "desc";
            DisplayMemberNames = textArray1;
        }

        public static void GenerateEditor(IEdmPropertyInfo property, EditorsGeneratorBase generator, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty, IEnumerable<TypeNamePropertyPair> collectionProperties = null, bool guessImageProperties = false, bool guessDisplayMembers = false, bool skipIEnumerableProperties = false)
        {
            EditorsGeneratorSelector.GenerateEditor(property, generator, getForegnKeyProperty, collectionProperties, guessImageProperties, guessDisplayMembers, skipIEnumerableProperties);
        }

        public static void GenerateEditors(IEntityProperties typeInfo, EditorsGeneratorBase generator, GenerateEditorOptions options, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty = null)
        {
            GenerateEditors(typeInfo.AllProperties, generator, options, getForegnKeyProperty);
        }

        public static void GenerateEditors(IEnumerable<IEdmPropertyInfo> properties, EditorsGeneratorBase generator, GenerateEditorOptions options, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty = null)
        {
            properties = EditorsGeneratorBase.GetFilteredAndSortedProperties(properties, options.Scaffolding, options.SortColumnsWithNegativeOrder, options.LayoutType);
            if (generator != null)
            {
                properties = generator.FilterProperties(properties);
            }
            foreach (IEdmPropertyInfo info in properties)
            {
                GenerateEditor(info, generator, getForegnKeyProperty, options.CollectionProperties, options.GuessImageProperties, options.GuessDisplayMembers, options.SkipIEnumerableProperties);
            }
        }

        public static void GenerateEditors(LayoutGroupInfo rootGroupInfo, IEnumerable<IEdmPropertyInfo> properties, IGroupGenerator generator, EditorsGeneratorBase availableItemsGenerator, GenerateEditorOptions options, bool allItemsAreAvailable, bool filterAndSortProperties = true, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty = null, bool usePreFiltering = true)
        {
            Func<IEdmPropertyInfo, bool> <>9__0;
            if (filterAndSortProperties)
            {
                if (usePreFiltering)
                {
                    properties = EditorsGeneratorBase.GetFilteredAndSortedProperties(properties, options.Scaffolding, options.SortColumnsWithNegativeOrder, options.LayoutType);
                }
                if ((generator != null) && (generator.EditorsGenerator != null))
                {
                    properties = generator.EditorsGenerator.FilterProperties(properties);
                }
            }
            Func<IEdmPropertyInfo, bool> predicate = <>9__0;
            if (<>9__0 == null)
            {
                Func<IEdmPropertyInfo, bool> local1 = <>9__0;
                predicate = <>9__0 = x => !IsAvailableItem(x, allItemsAreAvailable, availableItemsGenerator);
            }
            foreach (IEdmPropertyInfo info in properties.Where<IEdmPropertyInfo>(predicate))
            {
                LayoutGroupInfo groupInfo = rootGroupInfo.GetGroupInfo(info.Attributes.GetGroupName(options.LayoutType));
                groupInfo.Children.Add(new LayoutItemInfo(info));
            }
            if (rootGroupInfo.Children.Count > 0)
            {
                GenerateEditorsCore(rootGroupInfo, generator, options, getForegnKeyProperty);
            }
            GenerateEditors((IEnumerable<IEdmPropertyInfo>) (from x in properties
                where IsAvailableItem(x, allItemsAreAvailable, availableItemsGenerator)
                select x), availableItemsGenerator, options, null);
        }

        private static void GenerateEditorsCore(LayoutGroupInfo groupInfo, IGroupGenerator generator, GenerateEditorOptions options, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty)
        {
            generator.ApplyGroupInfo(groupInfo.Name, groupInfo.GetView(), groupInfo.GetOrientation());
            LayoutElementFactory factory = new LayoutElementFactory(generator, options, getForegnKeyProperty);
            foreach (ILayoutElementGenerator generator2 in groupInfo.Children)
            {
                generator2.CreateElement(factory);
            }
        }

        public static string GetDisplayMemberPropertyName(Type type)
        {
            PropertyDescriptor descriptor;
            Func<DisplayColumnAttribute, string> evaluator = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<DisplayColumnAttribute, string> local1 = <>c.<>9__13_0;
                evaluator = <>c.<>9__13_0 = a => a.DisplayColumn;
            }
            string str = type.GetCustomAttributes(true).OfType<DisplayColumnAttribute>().FirstOrDefault<DisplayColumnAttribute>().With<DisplayColumnAttribute, string>(evaluator);
            if (str != null)
            {
                return str;
            }
            IEnumerable<PropertyDescriptor> properties = TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>();
            Func<PropertyDescriptor, bool> predicate = <>c.<>9__13_4;
            if (<>c.<>9__13_4 == null)
            {
                Func<PropertyDescriptor, bool> local2 = <>c.<>9__13_4;
                predicate = <>c.<>9__13_4 = p => p != null;
            }
            PropertyDescriptor input = DisplayMemberNames.Select<string, PropertyDescriptor>(delegate (string d) {
                Func<PropertyDescriptor, bool> func1 = <>c.<>9__13_2;
                if (<>c.<>9__13_2 == null)
                {
                    Func<PropertyDescriptor, bool> local1 = <>c.<>9__13_2;
                    func1 = <>c.<>9__13_2 = x => x.PropertyType == typeof(string);
                }
                return properties.Where<PropertyDescriptor>(func1).FirstOrDefault<PropertyDescriptor>(x => x.Name.ToLowerInvariant().Contains(d));
            }).Where<PropertyDescriptor>(predicate).FirstOrDefault<PropertyDescriptor>();
            if (descriptor == null)
            {
                PropertyDescriptor local3 = DisplayMemberNames.Select<string, PropertyDescriptor>(delegate (string d) {
                    Func<PropertyDescriptor, bool> func1 = <>c.<>9__13_2;
                    if (<>c.<>9__13_2 == null)
                    {
                        Func<PropertyDescriptor, bool> local1 = <>c.<>9__13_2;
                        func1 = <>c.<>9__13_2 = x => x.PropertyType == typeof(string);
                    }
                    return properties.Where<PropertyDescriptor>(func1).FirstOrDefault<PropertyDescriptor>(x => x.Name.ToLowerInvariant().Contains(d));
                }).Where<PropertyDescriptor>(predicate).FirstOrDefault<PropertyDescriptor>();
                PropertyDescriptor local8 = properties.FirstOrDefault<PropertyDescriptor>(<>c.<>9__13_5 ??= x => (x.PropertyType == typeof(string)));
                if (<>c.<>9__13_6 == null)
                {
                    PropertyDescriptor local5 = properties.FirstOrDefault<PropertyDescriptor>(<>c.<>9__13_5 ??= x => (x.PropertyType == typeof(string)));
                    local8 = (PropertyDescriptor) (<>c.<>9__13_6 = x => x);
                }
                input = ((PropertyDescriptor) <>c.<>9__13_6).Return<PropertyDescriptor, PropertyDescriptor>(local8, delegate {
                    Func<PropertyDescriptor> <>9__10;
                    Func<PropertyDescriptor, bool> func2 = <>c.<>9__13_8;
                    if (<>c.<>9__13_8 == null)
                    {
                        Func<PropertyDescriptor, bool> local1 = <>c.<>9__13_8;
                        func2 = <>c.<>9__13_8 = x => Type.GetTypeCode(x.PropertyType) != TypeCode.Object;
                    }
                    Func<PropertyDescriptor, PropertyDescriptor> func3 = <>c.<>9__13_9;
                    if (<>c.<>9__13_9 == null)
                    {
                        Func<PropertyDescriptor, PropertyDescriptor> local2 = <>c.<>9__13_9;
                        func3 = <>c.<>9__13_9 = x => x;
                    }
                    Func<PropertyDescriptor> fallback = <>9__10;
                    if (<>9__10 == null)
                    {
                        Func<PropertyDescriptor> local3 = <>9__10;
                        fallback = <>9__10 = () => properties.FirstOrDefault<PropertyDescriptor>();
                    }
                    return properties.FirstOrDefault<PropertyDescriptor>(func2).Return<PropertyDescriptor, PropertyDescriptor>(func3, fallback);
                });
            }
            return input.Return<PropertyDescriptor, string>((<>c.<>9__13_11 ??= x => x.Name), (<>c.<>9__13_12 ??= () => string.Empty));
        }

        public static string GetImagePropertyName(IEntityTypeInfo type)
        {
            Func<IEdmPropertyInfo, bool> predicate = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<IEdmPropertyInfo, bool> local1 = <>c.<>9__14_0;
                predicate = <>c.<>9__14_0 = p => IsImagePropertyMostLikely(p);
            }
            Func<IEdmPropertyInfo, string> selector = <>c.<>9__14_1;
            if (<>c.<>9__14_1 == null)
            {
                Func<IEdmPropertyInfo, string> local2 = <>c.<>9__14_1;
                selector = <>c.<>9__14_1 = p => p.Name;
            }
            return type.GetProperties().Where<IEdmPropertyInfo>(predicate).Select<IEdmPropertyInfo, string>(selector).FirstOrDefault<string>();
        }

        private static bool IsAvailableItem(IEdmPropertyInfo property, bool isAvailable, EditorsGeneratorBase availableItemsGenerator) => 
            (availableItemsGenerator != null) && (isAvailable || property.Attributes.Hidden());

        private static bool IsImagePropertyMostLikely(IEdmPropertyInfo property)
        {
            PropertyDataType type = property.Attributes.PropertyDataType();
            return ((!(property.PropertyType == typeof(string)) || (type != PropertyDataType.ImageUrl)) ? ((property.PropertyType == typeof(byte[])) && IsImagePropertyName(property.Name)) : true);
        }

        internal static bool IsImagePropertyName(string name)
        {
            string[] source = new string[] { "image", "photo", "avatar", "picture", "icon", "glyph" };
            name = name.ToLower();
            return source.Any<string>(x => name.Contains(x));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorsSource.<>c <>9 = new EditorsSource.<>c();
            public static Func<DisplayColumnAttribute, string> <>9__13_0;
            public static Func<PropertyDescriptor, bool> <>9__13_2;
            public static Func<PropertyDescriptor, bool> <>9__13_4;
            public static Func<PropertyDescriptor, bool> <>9__13_5;
            public static Func<PropertyDescriptor, PropertyDescriptor> <>9__13_6;
            public static Func<PropertyDescriptor, bool> <>9__13_8;
            public static Func<PropertyDescriptor, PropertyDescriptor> <>9__13_9;
            public static Func<PropertyDescriptor, string> <>9__13_11;
            public static Func<string> <>9__13_12;
            public static Func<IEdmPropertyInfo, bool> <>9__14_0;
            public static Func<IEdmPropertyInfo, string> <>9__14_1;

            internal string <GetDisplayMemberPropertyName>b__13_0(DisplayColumnAttribute a) => 
                a.DisplayColumn;

            internal string <GetDisplayMemberPropertyName>b__13_11(PropertyDescriptor x) => 
                x.Name;

            internal string <GetDisplayMemberPropertyName>b__13_12() => 
                string.Empty;

            internal bool <GetDisplayMemberPropertyName>b__13_2(PropertyDescriptor x) => 
                x.PropertyType == typeof(string);

            internal bool <GetDisplayMemberPropertyName>b__13_4(PropertyDescriptor p) => 
                p != null;

            internal bool <GetDisplayMemberPropertyName>b__13_5(PropertyDescriptor x) => 
                x.PropertyType == typeof(string);

            internal PropertyDescriptor <GetDisplayMemberPropertyName>b__13_6(PropertyDescriptor x) => 
                x;

            internal bool <GetDisplayMemberPropertyName>b__13_8(PropertyDescriptor x) => 
                Type.GetTypeCode(x.PropertyType) != TypeCode.Object;

            internal PropertyDescriptor <GetDisplayMemberPropertyName>b__13_9(PropertyDescriptor x) => 
                x;

            internal bool <GetImagePropertyName>b__14_0(IEdmPropertyInfo p) => 
                EditorsSource.IsImagePropertyMostLikely(p);

            internal string <GetImagePropertyName>b__14_1(IEdmPropertyInfo p) => 
                p.Name;
        }

        private class LayoutElementFactory : ILayoutElementFactory
        {
            private readonly IGroupGenerator groupGenerator;
            private readonly GenerateEditorOptions options;
            private readonly Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty;

            public LayoutElementFactory(IGroupGenerator groupGenerator, GenerateEditorOptions options, Func<IEdmPropertyInfo, ForeignKeyInfo> getForegnKeyProperty)
            {
                this.groupGenerator = groupGenerator;
                this.options = options;
                this.getForegnKeyProperty = getForegnKeyProperty;
            }

            void ILayoutElementFactory.CreateGroup(LayoutGroupInfo groupInfo)
            {
                IGroupGenerator generator = this.groupGenerator.CreateNestedGroup(groupInfo.Name, groupInfo.GetView(), groupInfo.GetOrientation());
                EditorsSource.GenerateEditorsCore(groupInfo, generator, this.options, this.getForegnKeyProperty);
                generator.OnAfterGenerateContent();
            }

            void ILayoutElementFactory.CreateItem(IEdmPropertyInfo property)
            {
                EditorsSource.GenerateEditor(property, this.groupGenerator.EditorsGenerator, this.getForegnKeyProperty, this.options.CollectionProperties, this.options.GuessImageProperties, this.options.GuessDisplayMembers, this.options.SkipIEnumerableProperties);
            }
        }
    }
}

