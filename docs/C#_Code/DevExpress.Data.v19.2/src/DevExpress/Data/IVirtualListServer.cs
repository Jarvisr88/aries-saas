namespace DevExpress.Data
{
    using System;
    using System.Collections;

    public interface IVirtualListServer : IList, ICollection, IEnumerable
    {
        void ChangeConfiguration(VirtualServerModeConfigurationInfo configuration);
    }
}

