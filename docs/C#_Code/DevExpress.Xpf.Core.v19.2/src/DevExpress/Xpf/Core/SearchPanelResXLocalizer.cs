namespace DevExpress.Xpf.Core
{
    using System;
    using System.Resources;

    public class SearchPanelResXLocalizer : DXResXLocalizer<SearchPanelStringId>
    {
        public SearchPanelResXLocalizer() : base(new SearchPanelLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Core.Core.SearchPanelRes", typeof(SearchPanelResXLocalizer).Assembly);
    }
}

