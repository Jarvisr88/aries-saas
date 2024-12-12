namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public delegate T UnaryOperatorMapper<T>(string propertyName, UnaryOperatorType operatorType);
}

