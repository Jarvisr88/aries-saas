namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NewItemInstanceInitializerAttribute : InstanceInitializerAttributeBase
    {
        private Func<ITypeDescriptorContext, IEnumerable, KeyValuePair<object, object>?> createDictionaryInstanceCallback;

        public NewItemInstanceInitializerAttribute(Type type) : base(type)
        {
        }

        public NewItemInstanceInitializerAttribute(Type type, string name) : base(type, name, null)
        {
        }

        internal NewItemInstanceInitializerAttribute(Type type, string name, Func<object> createInstanceCallback) : base(type, name, createInstanceCallback)
        {
        }

        internal NewItemInstanceInitializerAttribute(Type type, string name, Func<ITypeDescriptorContext, IEnumerable, KeyValuePair<object, object>?> createDictionaryInstanceCallback) : base(type, name, null)
        {
            this.createDictionaryInstanceCallback = createDictionaryInstanceCallback;
        }

        public virtual KeyValuePair<object, object>? CreateInstance(ITypeDescriptorContext context, IEnumerable dictionary)
        {
            Func<KeyValuePair<object, object>?> fallback = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Func<KeyValuePair<object, object>?> local1 = <>c.<>9__5_1;
                fallback = <>c.<>9__5_1 = (Func<KeyValuePair<object, object>?>) (() => null);
            }
            return this.createDictionaryInstanceCallback.Return<Func<ITypeDescriptorContext, IEnumerable, KeyValuePair<object, object>?>, KeyValuePair<object, object>?>(x => x(context, dictionary), fallback);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NewItemInstanceInitializerAttribute.<>c <>9 = new NewItemInstanceInitializerAttribute.<>c();
            public static Func<KeyValuePair<object, object>?> <>9__5_1;

            internal KeyValuePair<object, object>? <CreateInstance>b__5_1() => 
                null;
        }
    }
}

