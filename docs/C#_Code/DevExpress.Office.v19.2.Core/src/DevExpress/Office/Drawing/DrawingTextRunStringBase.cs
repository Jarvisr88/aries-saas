namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public abstract class DrawingTextRunStringBase : DrawingTextRunBase
    {
        private string text;

        protected DrawingTextRunStringBase(IDocumentModel documentModel) : base(documentModel)
        {
            this.text = string.Empty;
        }

        protected void CopyFrom(DrawingTextRunStringBase value)
        {
            base.CopyFrom(value);
            this.Text = value.Text;
        }

        public override bool Equals(object obj)
        {
            DrawingTextRunStringBase base2 = obj as DrawingTextRunStringBase;
            return ((base2 != null) && (string.Equals(this.text, base2.text) && base.Equals(obj)));
        }

        public override int GetHashCode() => 
            base.GetHashCode() ^ this.text.GetHashCode();

        private void SetText(string value)
        {
            DrawingTextRunTextPropertyChangedHistoryItem item = new DrawingTextRunTextPropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.text, value);
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        public void SetTextCore(string value)
        {
            this.text = value;
            base.InvalidateParent();
        }

        public string Text
        {
            get => 
                this.text;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = string.Empty;
                }
                if (this.text != value)
                {
                    this.SetText(value);
                }
            }
        }
    }
}

