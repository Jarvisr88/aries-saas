namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class EditorResXLocalizer : DXResXLocalizer<EditorStringId>
    {
        public const string ResxPath = "DevExpress.Xpf.Core.Editors.LocalizationRes";

        public EditorResXLocalizer() : base(new EditorLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Core.Editors.LocalizationRes", typeof(EditorResXLocalizer).Assembly);
    }
}

