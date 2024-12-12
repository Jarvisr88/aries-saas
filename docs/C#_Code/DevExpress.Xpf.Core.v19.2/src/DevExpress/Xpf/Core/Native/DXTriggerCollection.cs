namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DXTriggerCollection : ObservableCollection<DXTrigger>
    {
        public DXTriggerCollection();
        public DXTriggerCollection(IEnumerable<DXTriggerInfoBase> collection, UIElement owner);
        private DXTrigger CreateNewTrigger(DXTriggerInfoBase triggerInfo);

        public UIElement Owner { get; set; }
    }
}

