namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate object DependencyPropertyRegistratorCoerceCallback<in TOwnerType, in TPropertyType>(TOwnerType d, TPropertyType value) where TOwnerType: DependencyObject;
}

