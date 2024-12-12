namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlDocumentProperties
    {
        private string version;
        private readonly XlDocumentCustomProperties custom = new XlDocumentCustomProperties();

        private bool IsValidVersionString(string value)
        {
            if (value.Length != 7)
            {
                return false;
            }
            for (int i = 0; i < 7; i++)
            {
                if ((i == 2) && (value[i] != '.'))
                {
                    return false;
                }
                if ((i != 2) && !char.IsDigit(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public string Application { get; set; }

        public string Manager { get; set; }

        public string Company { get; set; }

        public string Version
        {
            get => 
                this.version;
            set
            {
                if (!string.IsNullOrEmpty(value) && !this.IsValidVersionString(value))
                {
                    throw new ArgumentException("Version must be of the form XX.YYYY, where X and Y represent numerical values.");
                }
                this.version = value;
            }
        }

        public XlDocumentSecurity Security { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Author { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime Created { get; set; }

        public XlDocumentCustomProperties Custom =>
            this.custom;
    }
}

