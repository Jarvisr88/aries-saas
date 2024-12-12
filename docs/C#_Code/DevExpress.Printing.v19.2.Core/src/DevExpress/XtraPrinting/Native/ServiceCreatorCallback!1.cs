namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;

    public delegate T ServiceCreatorCallback<T>(IServiceContainer container, Type serviceType);
}

