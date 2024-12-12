namespace DevExpress.Office.Internal
{
    using System;
    using System.Text;

    public class FileDialogFilter
    {
        internal static readonly FileDialogFilter AllFiles = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_AllFiles), "*");
        internal static readonly FileDialogFilter Empty = new FileDialogFilter(string.Empty, new string[0]);
        private string description;
        private FileExtensionCollection extensions;

        public FileDialogFilter()
        {
            this.description = string.Empty;
            this.extensions = new FileExtensionCollection();
        }

        public FileDialogFilter(string description, string extension) : this(description, textArray1)
        {
            string[] textArray1 = new string[] { extension };
        }

        public FileDialogFilter(string description, string[] extensions)
        {
            this.description = string.Empty;
            this.extensions = new FileExtensionCollection();
            this.description = description;
            this.extensions.AddRange(extensions);
        }

        protected internal virtual void AppendExtension(StringBuilder sb, string extension)
        {
            sb.Append("*.");
            sb.Append(extension);
        }

        protected internal virtual void AppendExtensions(StringBuilder sb)
        {
            this.AppendExtension(sb, this.extensions[0]);
            int count = this.extensions.Count;
            for (int i = 1; i < count; i++)
            {
                sb.Append("; ");
                this.AppendExtension(sb, this.extensions[i]);
            }
        }

        protected internal virtual string CreateFilterString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Description);
            sb.Append(" (");
            this.AppendExtensions(sb);
            sb.Append(")|");
            this.AppendExtensions(sb);
            return sb.ToString();
        }

        public override string ToString() => 
            (this.extensions.Count > 0) ? this.CreateFilterString() : AllFiles.ToString();

        public string Description
        {
            get => 
                this.description;
            set => 
                this.description = value;
        }

        public FileExtensionCollection Extensions =>
            this.extensions;
    }
}

