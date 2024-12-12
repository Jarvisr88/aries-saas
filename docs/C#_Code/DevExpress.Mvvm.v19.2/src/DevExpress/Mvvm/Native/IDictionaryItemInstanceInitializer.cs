namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public interface IDictionaryItemInstanceInitializer : IInstanceInitializer
    {
        KeyValuePair<object, object>? CreateInstance(TypeInfo type, ITypeDescriptorContext context, IEnumerable dictionary);
    }
}

