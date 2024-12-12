namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class ComboBoxItemComparer : IComparer
    {
        private ComboBoxEditSettings comboBoxEditSettings;

        public ComboBoxItemComparer(ComboBoxEditSettings comboBoxEditSettings)
        {
            this.comboBoxEditSettings = comboBoxEditSettings;
        }

        public int Compare(object x, object y) => 
            Comparer<string>.Default.Compare(this.comboBoxEditSettings.GetDisplayText(x, true), this.comboBoxEditSettings.GetDisplayText(y, true));
    }
}

