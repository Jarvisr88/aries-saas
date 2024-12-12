namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System;
    using System.Runtime.CompilerServices;

    public class FormatEditorIconGroup
    {
        public FormatEditorIconGroup(string header, FormatEditorIconItemViewModel[] icons)
        {
            this.Header = header;
            this.Icons = icons;
        }

        public string Header { get; private set; }

        public FormatEditorIconItemViewModel[] Icons { get; private set; }
    }
}

