namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.MhtExportOptions")]
    public class MhtExportOptions : HtmlExportOptionsBase
    {
        public MhtExportOptions() : this("utf-8")
        {
        }

        private MhtExportOptions(MhtExportOptions source) : base(source)
        {
        }

        public MhtExportOptions(string characterSet) : this(characterSet, "Document")
        {
        }

        public MhtExportOptions(string characterSet, string title) : this(characterSet, title, false)
        {
        }

        public MhtExportOptions(string characterSet, string title, bool removeSecondarySymbols)
        {
            base.CharacterSet = characterSet;
            base.Title = title;
            base.RemoveSecondarySymbols = removeSecondarySymbols;
        }

        internal MhtExportOptions ChangeTitle(string newTitle)
        {
            MhtExportOptions options = (MhtExportOptions) this.CloneOptions();
            options.Title = newTitle;
            return options;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new MhtExportOptions(this);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool EmbedImagesInHTML
        {
            get => 
                false;
            set
            {
            }
        }
    }
}

