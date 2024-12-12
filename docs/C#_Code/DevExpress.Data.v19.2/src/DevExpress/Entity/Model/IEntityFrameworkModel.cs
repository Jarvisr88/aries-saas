namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IEntityFrameworkModel
    {
        IDbContainerInfo GetContainer(IContainerInfo info);
        [EditorBrowsable(EditorBrowsableState.Never)]
        IDbContainerInfo GetContainer(string nameOrFullName);
        IEnumerable<IContainerInfo> GetContainersInfo();
    }
}

