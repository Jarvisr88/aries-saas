namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.MailMessageExportOptions")]
    public class MailMessageExportOptions : HtmlExportOptionsBase
    {
        public MailMessageExportOptions() : this("utf-8")
        {
        }

        protected MailMessageExportOptions(MailMessageExportOptions source) : base(source)
        {
        }

        public MailMessageExportOptions(string characterSet) : this(characterSet, "Document")
        {
        }

        public MailMessageExportOptions(string characterSet, string title) : this(characterSet, title, false)
        {
        }

        public MailMessageExportOptions(string characterSet, string title, bool removeSecondarySymbols)
        {
            base.CharacterSet = characterSet;
            base.Title = title;
            base.RemoveSecondarySymbols = removeSecondarySymbols;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new MailMessageExportOptions(this);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool EmbedImagesInHTML
        {
            get => 
                false;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool InlineCss
        {
            get => 
                true;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool UseHRefHyperlinks
        {
            get => 
                true;
            set
            {
            }
        }
    }
}

