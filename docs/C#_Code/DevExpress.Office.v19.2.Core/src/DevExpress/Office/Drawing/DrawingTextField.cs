namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class DrawingTextField : DrawingTextRunStringBase, IDrawingTextRun, ISupportsCopyFrom<DrawingTextField>
    {
        private Guid fieldId;
        private string fieldType;
        private DrawingTextParagraphProperties paragraphProperties;

        public DrawingTextField(IDocumentModel documentModel) : base(documentModel)
        {
            this.fieldId = Guid.Empty;
            this.fieldType = string.Empty;
            DrawingTextParagraphProperties properties1 = new DrawingTextParagraphProperties(documentModel);
            properties1.Parent = base.InnerParent;
            this.paragraphProperties = properties1;
        }

        public IDrawingTextRun CloneTo(IDocumentModel documentModel)
        {
            DrawingTextField field = new DrawingTextField(documentModel);
            field.CopyFrom(this);
            return field;
        }

        public void CopyFrom(DrawingTextField value)
        {
            base.CopyFrom(value);
            this.FieldId = value.FieldId;
            this.FieldType = value.FieldType;
            this.paragraphProperties.CopyFrom(value.paragraphProperties);
        }

        public override bool Equals(object obj)
        {
            DrawingTextField field = obj as DrawingTextField;
            return ((field != null) ? (base.Equals(field) && (this.fieldId.Equals(field.fieldId) && (this.fieldType.Equals(field.fieldType) && this.paragraphProperties.Equals(field.paragraphProperties)))) : false);
        }

        public override int GetHashCode() => 
            ((base.GetHashCode() ^ this.fieldId.GetHashCode()) ^ this.fieldType.GetHashCode()) ^ this.paragraphProperties.GetHashCode();

        private void SetFieldId(Guid value)
        {
            DrawingTextFieldIdPropertyChangedHistoryItem item = new DrawingTextFieldIdPropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.fieldId, value);
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        public void SetFieldIdCore(Guid value)
        {
            this.fieldId = value;
        }

        private void SetFieldType(string value)
        {
            DrawingTextFieldTypePropertyChangedHistoryItem item = new DrawingTextFieldTypePropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.fieldType, value);
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        public void SetFieldTypeCore(string value)
        {
            this.fieldType = value;
            base.InvalidateParent();
        }

        public void Visit(IDrawingTextRunVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Guid FieldId
        {
            get => 
                this.fieldId;
            set
            {
                if (!this.fieldId.Equals(value))
                {
                    this.SetFieldId(value);
                }
            }
        }

        public string FieldType
        {
            get => 
                this.fieldType;
            set
            {
                value ??= string.Empty;
                if (!this.fieldType.Equals(value))
                {
                    this.SetFieldType(value);
                }
            }
        }

        public DrawingTextParagraphProperties ParagraphProperties =>
            this.paragraphProperties;
    }
}

