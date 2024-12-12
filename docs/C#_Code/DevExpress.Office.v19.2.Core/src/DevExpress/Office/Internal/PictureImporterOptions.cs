namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Import;
    using System;

    public abstract class PictureImporterOptions : IImporterOptions, ISupportsCopyFrom<IImporterOptions>
    {
        private string sourceUri = string.Empty;

        protected PictureImporterOptions()
        {
        }

        public virtual void CopyFrom(IImporterOptions value)
        {
        }

        public string SourceUri
        {
            get => 
                this.sourceUri;
            set => 
                this.sourceUri = value;
        }
    }
}

