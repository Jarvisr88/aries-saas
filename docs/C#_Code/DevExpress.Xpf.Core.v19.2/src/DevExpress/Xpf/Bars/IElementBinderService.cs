namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;

    public interface IElementBinderService : IRegistratorChangedListener
    {
        IEnumerable<IBarNameScopeSupport> GetMatches(object element);
    }
}

