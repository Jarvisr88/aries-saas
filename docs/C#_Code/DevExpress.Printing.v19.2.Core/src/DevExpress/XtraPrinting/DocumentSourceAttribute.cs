namespace DevExpress.XtraPrinting
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DocumentSourceAttribute : Attribute
    {
        public static readonly DocumentSourceAttribute DocumentSource = new DocumentSourceAttribute(true);
        public static readonly DocumentSourceAttribute Default = NonDocumentSource;
        public static readonly DocumentSourceAttribute NonDocumentSource = new DocumentSourceAttribute(false);
        private bool isDocumentSource;

        public DocumentSourceAttribute() : this(true)
        {
        }

        public DocumentSourceAttribute(bool isDocumentSource)
        {
            this.isDocumentSource = isDocumentSource;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            DocumentSourceAttribute attribute = obj as DocumentSourceAttribute;
            return ((attribute != null) && (attribute.IsDocumentSource == this.IsDocumentSource));
        }

        public override int GetHashCode() => 
            this.isDocumentSource.GetHashCode();

        public override bool IsDefaultAttribute() => 
            this.Equals(Default);

        public bool IsDocumentSource =>
            this.isDocumentSource;
    }
}

