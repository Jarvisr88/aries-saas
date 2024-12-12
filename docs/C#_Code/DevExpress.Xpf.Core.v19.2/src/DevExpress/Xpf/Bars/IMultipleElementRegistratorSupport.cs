namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public interface IMultipleElementRegistratorSupport : IBarNameScopeSupport, IInputElement
    {
        object GetName(object registratorKey);

        IEnumerable<object> RegistratorKeys { get; }
    }
}

