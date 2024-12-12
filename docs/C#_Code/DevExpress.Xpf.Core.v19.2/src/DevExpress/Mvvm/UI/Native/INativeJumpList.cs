namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Windows.Shell;

    public interface INativeJumpList : IList<JumpItem>, ICollection<JumpItem>, IEnumerable<JumpItem>, IEnumerable, INotifyCollectionChanged
    {
        void AddToRecentCategory(string path);
        IEnumerable<Tuple<JumpItem, JumpItemRejectionReason>> Apply();
        JumpItem Find(string id);

        bool ShowFrequentCategory { get; set; }

        bool ShowRecentCategory { get; set; }
    }
}

