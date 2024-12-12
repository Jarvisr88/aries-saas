namespace DevExpress.Utils.CommonDialogs.Internal
{
    using System;
    using System.Globalization;

    public class FileDialogCustomPlace
    {
        private string pathCore;
        private Guid knownFolderGuidCore;

        public FileDialogCustomPlace(Guid knownFolderGuid)
        {
            this.pathCore = string.Empty;
            this.knownFolderGuidCore = Guid.Empty;
            this.KnownFolderGuid = knownFolderGuid;
        }

        public FileDialogCustomPlace(string path)
        {
            this.pathCore = string.Empty;
            this.knownFolderGuidCore = Guid.Empty;
            this.Path = path;
        }

        public override string ToString()
        {
            object[] args = new object[] { base.ToString(), this.Path, this.KnownFolderGuid };
            return string.Format(CultureInfo.CurrentCulture, "{0} Path: {1} KnownFolderGuid: {2}", args);
        }

        public string Path
        {
            get => 
                !string.IsNullOrEmpty(this.pathCore) ? this.pathCore : string.Empty;
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.pathCore = text1;
                this.knownFolderGuidCore = Guid.Empty;
            }
        }

        public Guid KnownFolderGuid
        {
            get => 
                this.knownFolderGuidCore;
            set
            {
                this.pathCore = string.Empty;
                this.knownFolderGuidCore = value;
            }
        }
    }
}

