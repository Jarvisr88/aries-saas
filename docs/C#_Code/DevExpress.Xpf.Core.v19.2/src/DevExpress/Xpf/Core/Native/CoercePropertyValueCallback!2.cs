namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate T CoercePropertyValueCallback<Owner, T>(Owner owner, T baseValue);
}

