namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DXTabControlSerializationInfo
    {
        public DXTabControlSerializationInfo();
        private void AddNewTabs(DXTabControl tabControl);
        public void Apply(DXTabControl tabControl);
        private void ApplyAndUnassignInfos();
        private void AssignInfos(DXTabControl tabControl);
        public void Clear();
        public void Init(DXTabControl tabControl);
        private void MoveTab(DXTabControl tabControl, DXTabItem tab, int index);
        private void RemoveOldTabs(DXTabControl tabControl);
        private void ReorderExistingTabs(DXTabControl tabControl);

        [XtraSerializableProperty]
        public int SelectedIndex { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false)]
        public List<DXTabSerializationInfo> Infos { get; private set; }
    }
}

