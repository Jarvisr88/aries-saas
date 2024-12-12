namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class MemberMetadataStorage
    {
        private Dictionary<Type, Attribute> attributes = new Dictionary<Type, Attribute>();
        private List<Attribute> multipleAttributes = new List<Attribute>();

        internal void AddAttribute(Attribute attribute)
        {
            this.multipleAttributes.Add(attribute);
        }

        internal void AddOrModifyAttribute<TAttribute>(Action<TAttribute> setAttributeValue = null) where TAttribute: Attribute, new()
        {
            Func<Attribute> createValueDelegate = <>c__2<TAttribute>.<>9__2_0;
            if (<>c__2<TAttribute>.<>9__2_0 == null)
            {
                Func<Attribute> local1 = <>c__2<TAttribute>.<>9__2_0;
                createValueDelegate = <>c__2<TAttribute>.<>9__2_0 = (Func<Attribute>) (() => Activator.CreateInstance<TAttribute>());
            }
            Attribute attribute = this.attributes.GetOrAdd<Type, Attribute>(typeof(TAttribute), createValueDelegate);
            if (setAttributeValue != null)
            {
                setAttributeValue((TAttribute) attribute);
            }
        }

        internal void AddOrReplaceAttribute<TAttribute>(TAttribute attribute) where TAttribute: Attribute
        {
            this.attributes[typeof(TAttribute)] = attribute;
        }

        internal IEnumerable<Attribute> GetAttributes()
        {
            Func<Attribute, Attribute> selector = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<Attribute, Attribute> local1 = <>c.<>9__5_0;
                selector = <>c.<>9__5_0 = x => !(x is IAttributeProxy) ? x : ((IAttributeProxy) x).CreateRealAttribute();
            }
            return this.attributes.Values.Select<Attribute, Attribute>(selector).Concat<Attribute>(this.multipleAttributes);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MemberMetadataStorage.<>c <>9 = new MemberMetadataStorage.<>c();
            public static Func<Attribute, Attribute> <>9__5_0;

            internal Attribute <GetAttributes>b__5_0(Attribute x) => 
                !(x is IAttributeProxy) ? x : ((IAttributeProxy) x).CreateRealAttribute();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<TAttribute> where TAttribute: Attribute, new()
        {
            public static readonly MemberMetadataStorage.<>c__2<TAttribute> <>9;
            public static Func<Attribute> <>9__2_0;

            static <>c__2()
            {
                MemberMetadataStorage.<>c__2<TAttribute>.<>9 = new MemberMetadataStorage.<>c__2<TAttribute>();
            }

            internal Attribute <AddOrModifyAttribute>b__2_0() => 
                Activator.CreateInstance<TAttribute>();
        }
    }
}

