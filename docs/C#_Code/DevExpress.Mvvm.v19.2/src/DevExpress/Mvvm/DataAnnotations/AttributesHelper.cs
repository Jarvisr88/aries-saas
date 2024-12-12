namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class AttributesHelper
    {
        public static AttributeCollection GetAttributes<T>(Expression<Func<T, object>> property) => 
            GetAttributes(typeof(T), ExpressionHelper.GetPropertyName<T, object>(property));

        public static AttributeCollection GetAttributes(Type type) => 
            GetAttributes(null, type, null);

        public static AttributeCollection GetAttributes(Type type, string property) => 
            GetAttributes(TypeDescriptor.GetProperties(type)[property], type, null);

        public static AttributeCollection GetAttributes(PropertyDescriptor property, Type ownerType = null, IAttributesProvider attributesProvider = null)
        {
            IEnumerable<Attribute> externalAndFluentAPIAttributes;
            IEnumerable<Attribute> enumerable2;
            if (property != null)
            {
                Func<Attribute, bool> predicate = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<Attribute, bool> local1 = <>c.<>9__3_0;
                    predicate = <>c.<>9__3_0 = x => x.GetType().FullName == "DevExpress.Utils.Filtering.FilterAttribute";
                }
                if (property.Attributes.OfType<Attribute>().Any<Attribute>(predicate))
                {
                    externalAndFluentAPIAttributes = MetadataHelper.GetExternalAndFluentAPIFilteringAttributes(ownerType, property?.Name);
                    goto TR_0002;
                }
            }
            externalAndFluentAPIAttributes = MetadataHelper.GetExternalAndFluentAPIAttributes(ownerType, property?.Name);
        TR_0002:
            enumerable2 = (property != null) ? property.Attributes.Cast<Attribute>() : TypeDescriptor.GetAttributes(ownerType).Cast<Attribute>();
            IEnumerable<Attribute> local2 = attributesProvider.With<IAttributesProvider, IEnumerable<Attribute>>(x => x.GetAttributes(property?.Name));
            IEnumerable<Attribute> local4 = local2;
            if (local2 == null)
            {
                IEnumerable<Attribute> local3 = local2;
                local4 = Enumerable.Empty<Attribute>();
            }
            IEnumerable<Attribute> enumerable3 = local4;
            IEnumerable<Attribute>[] attributesInPriorityOrder = new IEnumerable<Attribute>[] { enumerable2, externalAndFluentAPIAttributes, enumerable3 };
            return new AttributeCollection(UnionAttributes(attributesInPriorityOrder).ToArray<Attribute>());
        }

        private static IEnumerable<Attribute> UnionAttributes(params IEnumerable<Attribute>[] attributesInPriorityOrder)
        {
            List<Attribute> source = new List<Attribute>();
            if (attributesInPriorityOrder.Count<IEnumerable<Attribute>>() != 0)
            {
                Func<IEnumerable<Attribute>, IEnumerable<Attribute>> selector = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<IEnumerable<Attribute>, IEnumerable<Attribute>> local1 = <>c.<>9__4_0;
                    selector = <>c.<>9__4_0 = x => x;
                }
                source.AddRange(attributesInPriorityOrder.Take<IEnumerable<Attribute>>(1).SelectMany<IEnumerable<Attribute>, Attribute>(selector));
                foreach (IEnumerable<Attribute> enumerable in attributesInPriorityOrder.Skip<IEnumerable<Attribute>>(1))
                {
                    List<Attribute> list2 = new List<Attribute>();
                    foreach (Attribute attribute in enumerable)
                    {
                        if (!(attribute is DisplayAttribute))
                        {
                            list2.Add(attribute);
                            continue;
                        }
                        DisplayAttribute highPriority = (DisplayAttribute) attribute;
                        DisplayAttribute item = source.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
                        if (item != null)
                        {
                            source.Remove(item);
                            highPriority = UnionAttributes(item, highPriority);
                        }
                        item = list2.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
                        if (item != null)
                        {
                            list2.Remove(item);
                            highPriority = UnionAttributes(item, highPriority);
                        }
                        list2.Add(highPriority);
                    }
                    source.InsertRange(0, list2);
                }
            }
            return source;
        }

        private static DisplayAttribute UnionAttributes(DisplayAttribute lowPriority, DisplayAttribute highPriority)
        {
            DisplayAttribute res = new DisplayAttribute();
            Func<DisplayAttribute, bool?> getValue = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<DisplayAttribute, bool?> local1 = <>c.<>9__5_0;
                getValue = <>c.<>9__5_0 = x => x.GetAutoGenerateField();
            }
            UnionAttributeValue<DisplayAttribute, bool?>(lowPriority, highPriority, getValue, delegate (bool? x) {
                res.AutoGenerateField = x.Value;
            });
            Func<DisplayAttribute, bool?> func2 = <>c.<>9__5_2;
            if (<>c.<>9__5_2 == null)
            {
                Func<DisplayAttribute, bool?> local2 = <>c.<>9__5_2;
                func2 = <>c.<>9__5_2 = x => x.GetAutoGenerateFilter();
            }
            UnionAttributeValue<DisplayAttribute, bool?>(lowPriority, highPriority, func2, delegate (bool? x) {
                res.AutoGenerateFilter = x.Value;
            });
            Func<DisplayAttribute, string> func3 = <>c.<>9__5_4;
            if (<>c.<>9__5_4 == null)
            {
                Func<DisplayAttribute, string> local3 = <>c.<>9__5_4;
                func3 = <>c.<>9__5_4 = x => x.GetDescription();
            }
            UnionAttributeValue<DisplayAttribute, string>(lowPriority, highPriority, func3, delegate (string x) {
                res.Description = x;
            });
            Func<DisplayAttribute, string> func4 = <>c.<>9__5_6;
            if (<>c.<>9__5_6 == null)
            {
                Func<DisplayAttribute, string> local4 = <>c.<>9__5_6;
                func4 = <>c.<>9__5_6 = x => x.GetGroupName();
            }
            UnionAttributeValue<DisplayAttribute, string>(lowPriority, highPriority, func4, delegate (string x) {
                res.GroupName = x;
            });
            Func<DisplayAttribute, string> func5 = <>c.<>9__5_8;
            if (<>c.<>9__5_8 == null)
            {
                Func<DisplayAttribute, string> local5 = <>c.<>9__5_8;
                func5 = <>c.<>9__5_8 = x => x.GetName();
            }
            UnionAttributeValue<DisplayAttribute, string>(lowPriority, highPriority, func5, delegate (string x) {
                res.Name = x;
            });
            Func<DisplayAttribute, int?> func6 = <>c.<>9__5_10;
            if (<>c.<>9__5_10 == null)
            {
                Func<DisplayAttribute, int?> local6 = <>c.<>9__5_10;
                func6 = <>c.<>9__5_10 = x => x.GetOrder();
            }
            UnionAttributeValue<DisplayAttribute, int?>(lowPriority, highPriority, func6, delegate (int? x) {
                res.Order = x.Value;
            });
            Func<DisplayAttribute, string> func7 = <>c.<>9__5_12;
            if (<>c.<>9__5_12 == null)
            {
                Func<DisplayAttribute, string> local7 = <>c.<>9__5_12;
                func7 = <>c.<>9__5_12 = x => x.GetPrompt();
            }
            UnionAttributeValue<DisplayAttribute, string>(lowPriority, highPriority, func7, delegate (string x) {
                res.Prompt = x;
            });
            Func<DisplayAttribute, string> func8 = <>c.<>9__5_14;
            if (<>c.<>9__5_14 == null)
            {
                Func<DisplayAttribute, string> local8 = <>c.<>9__5_14;
                func8 = <>c.<>9__5_14 = x => x.GetShortName();
            }
            UnionAttributeValue<DisplayAttribute, string>(lowPriority, highPriority, func8, delegate (string x) {
                res.ShortName = x;
            });
            return res;
        }

        private static void UnionAttributeValue<TAttribute, TValue>(TAttribute lowPriority, TAttribute highPriority, Func<TAttribute, TValue> getValue, Action<TValue> result) where TAttribute: Attribute
        {
            TValue local = getValue(highPriority);
            if (local != null)
            {
                result(local);
            }
            else
            {
                TValue local2 = getValue(lowPriority);
                if (local2 != null)
                {
                    result(local2);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AttributesHelper.<>c <>9 = new AttributesHelper.<>c();
            public static Func<Attribute, bool> <>9__3_0;
            public static Func<IEnumerable<Attribute>, IEnumerable<Attribute>> <>9__4_0;
            public static Func<DisplayAttribute, bool?> <>9__5_0;
            public static Func<DisplayAttribute, bool?> <>9__5_2;
            public static Func<DisplayAttribute, string> <>9__5_4;
            public static Func<DisplayAttribute, string> <>9__5_6;
            public static Func<DisplayAttribute, string> <>9__5_8;
            public static Func<DisplayAttribute, int?> <>9__5_10;
            public static Func<DisplayAttribute, string> <>9__5_12;
            public static Func<DisplayAttribute, string> <>9__5_14;

            internal bool <GetAttributes>b__3_0(Attribute x) => 
                x.GetType().FullName == "DevExpress.Utils.Filtering.FilterAttribute";

            internal IEnumerable<Attribute> <UnionAttributes>b__4_0(IEnumerable<Attribute> x) => 
                x;

            internal bool? <UnionAttributes>b__5_0(DisplayAttribute x) => 
                x.GetAutoGenerateField();

            internal int? <UnionAttributes>b__5_10(DisplayAttribute x) => 
                x.GetOrder();

            internal string <UnionAttributes>b__5_12(DisplayAttribute x) => 
                x.GetPrompt();

            internal string <UnionAttributes>b__5_14(DisplayAttribute x) => 
                x.GetShortName();

            internal bool? <UnionAttributes>b__5_2(DisplayAttribute x) => 
                x.GetAutoGenerateFilter();

            internal string <UnionAttributes>b__5_4(DisplayAttribute x) => 
                x.GetDescription();

            internal string <UnionAttributes>b__5_6(DisplayAttribute x) => 
                x.GetGroupName();

            internal string <UnionAttributes>b__5_8(DisplayAttribute x) => 
                x.GetName();
        }
    }
}

