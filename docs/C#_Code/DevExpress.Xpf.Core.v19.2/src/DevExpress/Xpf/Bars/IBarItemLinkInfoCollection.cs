namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public interface IBarItemLinkInfoCollection : IList, ICollection, IEnumerable
    {
        event EventHandler HaveVisibleInfosChanged;

        void Destroy();

        bool HaveVisibleInfos { get; }
    }
}

