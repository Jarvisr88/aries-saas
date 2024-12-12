namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class UICommandContainerCollection : ObservableCollection<UICommandContainer>
    {
        public UICommandContainerCollection()
        {
        }

        public UICommandContainerCollection(IEnumerable<UICommandContainer> collection) : base(collection)
        {
        }
    }
}

