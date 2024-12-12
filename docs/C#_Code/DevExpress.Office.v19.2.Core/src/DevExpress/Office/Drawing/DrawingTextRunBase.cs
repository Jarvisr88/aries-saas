namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public abstract class DrawingTextRunBase
    {
        private readonly IDocumentModel documentModel;
        private readonly InvalidateProxy innerParent = new InvalidateProxy();
        private readonly DrawingTextCharacterProperties runProperties;

        protected DrawingTextRunBase(IDocumentModel documentModel)
        {
            this.documentModel = documentModel;
            DrawingTextCharacterProperties properties1 = new DrawingTextCharacterProperties(documentModel);
            properties1.Parent = this.innerParent;
            this.runProperties = properties1;
        }

        protected virtual void CopyFrom(DrawingTextRunBase value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.runProperties.CopyFrom(value.runProperties);
        }

        public override bool Equals(object obj)
        {
            DrawingTextRunBase base2 = obj as DrawingTextRunBase;
            return ((base2 != null) && this.runProperties.Equals(base2.runProperties));
        }

        public override int GetHashCode() => 
            this.runProperties.GetHashCode();

        protected void InvalidateParent()
        {
            this.innerParent.Invalidate();
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        protected ISupportsInvalidate InnerParent =>
            this.innerParent;

        public DrawingTextCharacterProperties RunProperties =>
            this.runProperties;
    }
}

