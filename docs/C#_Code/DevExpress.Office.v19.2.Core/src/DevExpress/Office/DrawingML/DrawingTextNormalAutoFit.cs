namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public class DrawingTextNormalAutoFit : IDrawingTextAutoFit
    {
        public const int DefaultFontScale = 0x186a0;
        private readonly IDocumentModel documentModel;
        private int fontScale;
        private int lineSpaceReduction;

        public DrawingTextNormalAutoFit(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.fontScale = 0x186a0;
            this.lineSpaceReduction = 0;
        }

        public IDrawingTextAutoFit CloneTo(IDocumentModel documentModel) => 
            new DrawingTextNormalAutoFit(documentModel) { 
                fontScale = this.fontScale,
                lineSpaceReduction = this.lineSpaceReduction
            };

        public bool Equals(IDrawingTextAutoFit other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.Type != other.Type)
            {
                return false;
            }
            DrawingTextNormalAutoFit fit = other as DrawingTextNormalAutoFit;
            return ((this.FontScale == fit.FontScale) && (this.LineSpaceReduction == fit.LineSpaceReduction));
        }

        private void SetAutoFitValue(Action<int> action, int oldValue, int newValue)
        {
            ActionIntHistoryItem item = new ActionIntHistoryItem(this.DocumentModel.MainPart, oldValue, newValue, action);
            this.documentModel.History.Add(item);
            item.Execute();
        }

        public void SetFontScale(int value)
        {
            this.fontScale = value;
        }

        public void SetLineSpaceReduction(int value)
        {
            this.lineSpaceReduction = value;
        }

        public void Visit(IDrawingTextAutoFitVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        public int FontScale
        {
            get => 
                this.fontScale;
            set
            {
                if (this.fontScale != value)
                {
                    ValueChecker.CheckValue(value, 0x3e8, 0x186a0, "FontScale");
                    this.SetAutoFitValue(new Action<int>(this.SetFontScale), this.fontScale, value);
                }
            }
        }

        public int LineSpaceReduction
        {
            get => 
                this.lineSpaceReduction;
            set
            {
                if (this.lineSpaceReduction != value)
                {
                    ValueChecker.CheckValue(value, 0, 0xc96a80, "LineSpaceReduction");
                    this.SetAutoFitValue(new Action<int>(this.SetLineSpaceReduction), this.lineSpaceReduction, value);
                }
            }
        }

        public DrawingTextAutoFitType Type =>
            DrawingTextAutoFitType.Normal;
    }
}

