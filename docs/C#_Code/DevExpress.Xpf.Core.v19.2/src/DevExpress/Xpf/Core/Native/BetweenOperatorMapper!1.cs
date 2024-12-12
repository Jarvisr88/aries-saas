namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate T BetweenOperatorMapper<T>(string propertyName, object beginValue, object endValue);
}

