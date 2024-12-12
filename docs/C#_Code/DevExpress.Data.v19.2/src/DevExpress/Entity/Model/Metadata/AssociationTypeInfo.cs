namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class AssociationTypeInfo : EntityTypeBaseInfo
    {
        public AssociationTypeInfo(object source) : base(source)
        {
        }

        private bool EdmEquals(object firstItem, object secondItem)
        {
            if ((firstItem == null) || (secondItem == null))
            {
                return false;
            }
            if (firstItem == secondItem)
            {
                return true;
            }
            PropertyAccessor accessor2 = new PropertyAccessor(secondItem, "Name");
            if (new PropertyAccessor(firstItem, "Name").Value != accessor2.Value)
            {
                return false;
            }
            PropertyAccessor accessor4 = new PropertyAccessor(secondItem, "BuiltInTypeKind");
            return (ConvertEnum<BuiltInTypeKind>(new PropertyAccessor(firstItem, "BuiltInTypeKind").Value) == ConvertEnum<BuiltInTypeKind>(accessor4.Value));
        }

        public AssociationTypeInfo GetCSpaceAssociationType(IEntityTypeInfo declaringType)
        {
            if (declaringType == null)
            {
                return null;
            }
            EntityTypeInfo info = declaringType as EntityTypeInfo;
            return (((info == null) || (info.AssociationTypeSource == null)) ? null : info.AssociationTypeSource.GetAssociationTypeFromCSpace(base.FullName));
        }

        public IEnumerable<EdmMemberInfo> GetDependentProperties(EdmMemberInfo navProperty)
        {
            IEnumerable<EdmMemberInfo> enumerable5;
            try
            {
                if (!navProperty.IsNavigationProperty)
                {
                    enumerable5 = null;
                }
                else
                {
                    IEnumerable source = base.GetPropertyAccessor("ReferentialConstraints").Value as IEnumerable;
                    object obj2 = base.GetPropertyAccessor("ReferentialConstraints.Count").Value;
                    if ((source == null) || ((obj2 == null) || (((int) obj2) <= 0)))
                    {
                        enumerable5 = null;
                    }
                    else
                    {
                        object obj3 = source.OfType<object>().FirstOrDefault<object>();
                        if (obj3 == null)
                        {
                            enumerable5 = null;
                        }
                        else
                        {
                            PropertyAccessor accessor = new PropertyAccessor(obj3, "FromRole");
                            PropertyAccessor accessor2 = new PropertyAccessor(obj3, "ToRole");
                            if (!this.EdmEquals(navProperty.FromEndMember, accessor2.Value))
                            {
                                enumerable5 = null;
                            }
                            else
                            {
                                object obj4 = new MethodAccessor(accessor.Value, "GetEntityType").Invoke(null);
                                if (obj4 == null)
                                {
                                    enumerable5 = null;
                                }
                                else
                                {
                                    IEnumerable enumerable2 = PropertyAccessor.GetValue(obj4, "KeyMembers") as IEnumerable;
                                    if (enumerable2 == null)
                                    {
                                        enumerable5 = null;
                                    }
                                    else
                                    {
                                        List<EdmMemberInfo> list = new List<EdmMemberInfo>();
                                        IEnumerable enumerable3 = PropertyAccessor.GetValue(obj3, "FromProperties") as IEnumerable;
                                        IEnumerable enumerable4 = PropertyAccessor.GetValue(obj3, "ToProperties") as IEnumerable;
                                        if ((enumerable3 == null) || (enumerable4 == null))
                                        {
                                            enumerable5 = null;
                                        }
                                        else
                                        {
                                            List<object> list2 = enumerable3.OfType<object>().ToList<object>();
                                            List<object> list3 = enumerable4.OfType<object>().ToList<object>();
                                            foreach (object obj5 in enumerable2)
                                            {
                                                int index = list2.IndexOf(obj5);
                                                if ((index >= 0) && (list3.Count > index))
                                                {
                                                    object member = list3[index];
                                                    if (member != null)
                                                    {
                                                        list.Add(new EdmMemberInfo(member));
                                                    }
                                                }
                                            }
                                            enumerable5 = list;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                enumerable5 = null;
            }
            return enumerable5;
        }

        public IEnumerable<EdmMemberInfo> GetToEndPropertyNames(EdmMemberInfo navProperty, EntityTypeBaseInfo toEndEntityTypeInfo)
        {
            if (navProperty.IsNavigationProperty)
            {
                IEnumerable source = base.GetPropertyAccessor("ReferentialConstraints").Value as IEnumerable;
                object obj2 = base.GetPropertyAccessor("ReferentialConstraints.Count").Value;
                if ((source == null) || ((obj2 == null) || (((int) obj2) <= 0)))
                {
                    return null;
                }
                int num = (int) obj2;
                for (int i = 0; i < num; i++)
                {
                    object obj3 = source.OfType<object>().ElementAt<object>(i);
                    if (obj3 != null)
                    {
                        IEnumerable enumerable2 = PropertyAccessor.GetValue(obj3, "ToProperties") as IEnumerable;
                        if (enumerable2 != null)
                        {
                            obj2 = PropertyAccessor.GetValue(enumerable2, "Count");
                            if ((obj2 != null) && (((int) obj2) > 0))
                            {
                                IEnumerable<object> enumerable3 = enumerable2.Cast<object>();
                                if (enumerable3.Any<object>())
                                {
                                    var <>9__3;
                                    var selector = <>c.<>9__6_0;
                                    if (<>c.<>9__6_0 == null)
                                    {
                                        var local1 = <>c.<>9__6_0;
                                        selector = <>c.<>9__6_0 = p => new { 
                                            p = p,
                                            typed = new EdmMemberInfo(p)
                                        };
                                    }
                                    var predicate = <>c.<>9__6_1;
                                    if (<>c.<>9__6_1 == null)
                                    {
                                        var local2 = <>c.<>9__6_1;
                                        predicate = <>c.<>9__6_1 = <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.typed.DeclaringType != null;
                                    }
                                    var func4 = <>c.<>9__6_2;
                                    if (<>c.<>9__6_2 == null)
                                    {
                                        var local3 = <>c.<>9__6_2;
                                        func4 = <>c.<>9__6_2 = <>h__TransparentIdentifier0 => new { 
                                            <>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
                                            type = new EntityTypeBaseInfo(<>h__TransparentIdentifier0.typed.DeclaringType)
                                        };
                                    }
                                    var func5 = <>9__3;
                                    if (<>9__3 == null)
                                    {
                                        var local4 = <>9__3;
                                        func5 = <>9__3 = <>h__TransparentIdentifier1 => <>h__TransparentIdentifier1.type.Name == toEndEntityTypeInfo.Name;
                                    }
                                    var func6 = <>c.<>9__6_4;
                                    if (<>c.<>9__6_4 == null)
                                    {
                                        var local5 = <>c.<>9__6_4;
                                        func6 = <>c.<>9__6_4 = <>h__TransparentIdentifier1 => <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.typed;
                                    }
                                    List<EdmMemberInfo> list = enumerable3.Select(selector).Where(predicate).Select(func4).Where(func5).Select(func6).ToList<EdmMemberInfo>();
                                    if (list.Any<EdmMemberInfo>())
                                    {
                                        return list;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        public bool IsForeignKey
        {
            get
            {
                object obj2 = base.GetPropertyAccessor("IsForeignKey").Value;
                return ((obj2 != null) && ((bool) obj2));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AssociationTypeInfo.<>c <>9 = new AssociationTypeInfo.<>c();
            public static Func<object, <>f__AnonymousType3<object, EdmMemberInfo>> <>9__6_0;
            public static Func<<>f__AnonymousType3<object, EdmMemberInfo>, bool> <>9__6_1;
            public static Func<<>f__AnonymousType3<object, EdmMemberInfo>, <>f__AnonymousType4<<>f__AnonymousType3<object, EdmMemberInfo>, EntityTypeBaseInfo>> <>9__6_2;
            public static Func<<>f__AnonymousType4<<>f__AnonymousType3<object, EdmMemberInfo>, EntityTypeBaseInfo>, EdmMemberInfo> <>9__6_4;

            internal <>f__AnonymousType3<object, EdmMemberInfo> <GetToEndPropertyNames>b__6_0(object p) => 
                new { 
                    p = p,
                    typed = new EdmMemberInfo(p)
                };

            internal bool <GetToEndPropertyNames>b__6_1(<>f__AnonymousType3<object, EdmMemberInfo> <>h__TransparentIdentifier0) => 
                <>h__TransparentIdentifier0.typed.DeclaringType != null;

            internal <>f__AnonymousType4<<>f__AnonymousType3<object, EdmMemberInfo>, EntityTypeBaseInfo> <GetToEndPropertyNames>b__6_2(<>f__AnonymousType3<object, EdmMemberInfo> <>h__TransparentIdentifier0) => 
                new { 
                    <>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
                    type = new EntityTypeBaseInfo(<>h__TransparentIdentifier0.typed.DeclaringType)
                };

            internal EdmMemberInfo <GetToEndPropertyNames>b__6_4(<>f__AnonymousType4<<>f__AnonymousType3<object, EdmMemberInfo>, EntityTypeBaseInfo> <>h__TransparentIdentifier1) => 
                <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.typed;
        }
    }
}

