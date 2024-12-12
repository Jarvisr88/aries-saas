namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class EditingFieldEditorNameExtensions
    {
        private static EditingFieldEditorNameExtensions instance;
        private Dictionary<string, string> names = new Dictionary<string, string>();

        private EditingFieldEditorNameExtensions()
        {
        }

        public bool RegisterEditorName(string name, string displayName)
        {
            if (string.IsNullOrWhiteSpace(name) || this.names.ContainsKey(name))
            {
                return false;
            }
            this.names.Add(name, displayName);
            return true;
        }

        internal bool TryGetDisplayName(string name, out string displayName)
        {
            if (name != string.Empty)
            {
                return this.names.TryGetValue(name, out displayName);
            }
            displayName = string.Empty;
            return true;
        }

        internal bool TryGetName(string displayName, out string name)
        {
            name = string.Empty;
            if (displayName != string.Empty)
            {
                if (!this.names.Values.Contains<string>(displayName))
                {
                    return false;
                }
                name = this.names.FirstOrDefault<KeyValuePair<string, string>>(item => (item.Value == displayName)).Key;
            }
            return true;
        }

        public bool UnregisterEditorName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !this.names.ContainsKey(name))
            {
                return false;
            }
            this.names.Remove(name);
            return true;
        }

        public static EditingFieldEditorNameExtensions Instance
        {
            get
            {
                instance ??= new EditingFieldEditorNameExtensions();
                return instance;
            }
            set => 
                instance = value;
        }
    }
}

