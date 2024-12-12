namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void DependencyPropertyRegistratorChangedCallback<in TOwnerType, in TPropertyType>(TOwnerType d, TPropertyType oldValue, TPropertyType newValue) where TOwnerType: DependencyObject;
}

