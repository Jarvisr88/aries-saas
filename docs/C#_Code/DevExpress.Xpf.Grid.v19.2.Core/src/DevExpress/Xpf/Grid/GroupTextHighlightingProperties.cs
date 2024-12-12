namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class GroupTextHighlightingProperties
    {
        public GroupTextHighlightingProperties(DevExpress.Xpf.Editors.TextHighlightingProperties textHighlightingProperties, BaseEditSettings editSettings)
        {
            this.TextHighlightingProperties = textHighlightingProperties;
            this.EditSettings = editSettings;
        }

        public DevExpress.Xpf.Editors.TextHighlightingProperties TextHighlightingProperties { get; private set; }

        public BaseEditSettings EditSettings { get; private set; }
    }
}

