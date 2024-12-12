namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;

    public interface IInstanceInitializer
    {
        object CreateInstance(TypeInfo type);

        IEnumerable<TypeInfo> Types { get; }
    }
}

