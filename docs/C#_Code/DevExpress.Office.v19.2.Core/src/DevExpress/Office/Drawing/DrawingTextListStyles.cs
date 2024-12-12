namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Reflection;

    public class DrawingTextListStyles : ISupportsCopyFrom<DrawingTextListStyles>
    {
        public const int Count = 9;
        private readonly InvalidateProxy innerParent = new InvalidateProxy();
        private readonly DrawingTextParagraphProperties defaultParagraphStyle;
        private readonly DrawingTextParagraphProperties[] listLevelStyles;

        public DrawingTextListStyles(IDocumentModel documentModel)
        {
            DrawingTextParagraphProperties properties1 = new DrawingTextParagraphProperties(documentModel);
            properties1.Parent = this.innerParent;
            this.defaultParagraphStyle = properties1;
            this.listLevelStyles = new DrawingTextParagraphProperties[9];
            for (int i = 0; i < 9; i++)
            {
                DrawingTextParagraphProperties properties2 = new DrawingTextParagraphProperties(documentModel);
                properties2.Parent = this.innerParent;
                this.listLevelStyles[i] = properties2;
            }
        }

        public void CopyFrom(DrawingTextListStyles value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.defaultParagraphStyle.CopyFrom(value.defaultParagraphStyle);
            for (int i = 0; i < 9; i++)
            {
                this.listLevelStyles[i].CopyFrom(value.listLevelStyles[i]);
            }
        }

        public override bool Equals(object obj)
        {
            DrawingTextListStyles styles = obj as DrawingTextListStyles;
            if (styles == null)
            {
                return false;
            }
            if (!this.defaultParagraphStyle.Equals(styles.defaultParagraphStyle))
            {
                return false;
            }
            for (int i = 0; i < 9; i++)
            {
                if (!this.listLevelStyles[i].Equals(styles.listLevelStyles[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = this.defaultParagraphStyle.GetHashCode();
            for (int i = 0; i < 9; i++)
            {
                hashCode ^= this.listLevelStyles[i].GetHashCode();
            }
            return hashCode;
        }

        public void ResetToStyle()
        {
            this.defaultParagraphStyle.ResetToStyle();
            for (int i = 0; i < 9; i++)
            {
                this.listLevelStyles[i].ResetToStyle();
            }
        }

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        public DrawingTextParagraphProperties DefaultParagraphStyle =>
            this.defaultParagraphStyle;

        public DrawingTextParagraphProperties this[int index] =>
            this.listLevelStyles[index];

        public bool IsDefault
        {
            get
            {
                bool isDefault = this.DefaultParagraphStyle.IsDefault;
                for (int i = 0; i < 9; i++)
                {
                    isDefault &= this[i].IsDefault;
                }
                return isDefault;
            }
        }
    }
}

