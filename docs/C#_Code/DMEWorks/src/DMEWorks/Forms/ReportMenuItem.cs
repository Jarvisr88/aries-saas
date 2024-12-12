namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    public class ReportMenuItem : MenuItem
    {
        private string _reportfilename;

        public ReportMenuItem() : this(MenuMerge.Add, 0, Shortcut.None, null, null, null, null, null)
        {
        }

        public ReportMenuItem(string text) : this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, null)
        {
        }

        public ReportMenuItem(string text, EventHandler onClick) : this(MenuMerge.Add, 0, Shortcut.None, text, onClick, null, null, null)
        {
        }

        public ReportMenuItem(string text, MenuItem[] items) : this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, items)
        {
        }

        public ReportMenuItem(string text, EventHandler onClick, Shortcut shortcut) : this(MenuMerge.Add, 0, shortcut, text, onClick, null, null, null)
        {
        }

        public ReportMenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, MenuItem[] items) : base(mergeType, mergeOrder, shortcut, text, onClick, onPopup, onSelect, items)
        {
        }

        public string ReportFileName
        {
            get => 
                this._reportfilename;
            set => 
                this._reportfilename = value;
        }
    }
}

