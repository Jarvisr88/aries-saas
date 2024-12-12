namespace DevExpress.Xpf.Editors
{
    using System.Collections.Generic;

    public interface ISearchPanelColumnProvider : ISearchPanelColumnProviderBase
    {
        IEnumerable<string> Columns { get; }
    }
}

