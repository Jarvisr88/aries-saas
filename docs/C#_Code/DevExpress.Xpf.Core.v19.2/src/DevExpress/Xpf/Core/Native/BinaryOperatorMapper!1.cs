namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public delegate T BinaryOperatorMapper<T>(string propertyName, object value, BinaryOperatorType operatorType);
}

