namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    public class PdfFileAttachmentListItem : INotifyPropertyChanged
    {
        private readonly PdfFileAttachment fileAttachment;
        private readonly byte[] icon;
        private readonly string size;
        private readonly string hint;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public PdfFileAttachmentListItem(PdfFileAttachment fileAttachment, byte[] icon)
        {
            this.fileAttachment = fileAttachment;
            this.icon = icon;
            this.size = PdfFileSizeConverter.ToString("{0:F} {1}", (double) fileAttachment.Size);
            this.hint = string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgAttachmentHintFileName), this.FileName);
            DateTimeOffset? modificationDate = this.ModificationDate;
            if (modificationDate != null)
            {
                this.hint = this.hint + string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgAttachmentHintModificationDate), modificationDate.Value);
            }
            this.hint = this.hint + string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgAttachmentHintSize), this.size);
            string description = this.Description;
            if (!string.IsNullOrEmpty(description))
            {
                this.hint = this.hint + string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgAttachmentHintDescription), Regex.Replace(description, @"(.{50}\s)", "$1\n", RegexOptions.None));
            }
        }

        public override string ToString() => 
            this.FileName;

        public PdfFileAttachment FileAttachment =>
            this.fileAttachment;

        public byte[] Icon =>
            this.icon;

        public string Size =>
            this.size;

        public string Hint =>
            this.hint;

        public string FileName =>
            this.fileAttachment.FileName;

        public DateTimeOffset? ModificationDate =>
            this.fileAttachment.ModificationDate;

        public string Description =>
            this.fileAttachment.Description;
    }
}

