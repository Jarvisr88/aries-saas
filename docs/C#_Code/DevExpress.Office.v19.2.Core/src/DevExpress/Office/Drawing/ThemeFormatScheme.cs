namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;

    public class ThemeFormatScheme
    {
        private readonly List<IDrawingFill> backgroundFillStyleList = new List<IDrawingFill>();
        private readonly List<IDrawingFill> fillStyleList = new List<IDrawingFill>();
        private readonly List<Outline> lineStyleList = new List<Outline>();
        private readonly List<DrawingEffectStyle> effectStyleList = new List<DrawingEffectStyle>();
        private string name = string.Empty;

        private bool CheckValidation() => 
            (this.backgroundFillStyleList.Count >= 3) && ((this.fillStyleList.Count >= 3) && ((this.lineStyleList.Count >= 3) && (this.effectStyleList.Count >= 3)));

        protected internal void Clear()
        {
            this.name = string.Empty;
            this.backgroundFillStyleList.Clear();
            this.fillStyleList.Clear();
            this.lineStyleList.Clear();
            this.effectStyleList.Clear();
        }

        protected internal void CopyFrom(IDocumentModel targetModel, IOfficeTheme sourceTheme)
        {
            this.Clear();
            this.name = sourceTheme.FormatScheme.name;
            this.CopyFrom(targetModel, this.backgroundFillStyleList, sourceTheme.FormatScheme.backgroundFillStyleList);
            this.CopyFrom(targetModel, this.fillStyleList, sourceTheme.FormatScheme.fillStyleList);
            this.CopyFrom(targetModel, this.lineStyleList, sourceTheme.FormatScheme.lineStyleList);
            this.CopyFrom(targetModel, this.effectStyleList, sourceTheme.FormatScheme.effectStyleList);
        }

        private void CopyFrom(IDocumentModel targetModel, List<DrawingEffectStyle> targetList, List<DrawingEffectStyle> sourceList)
        {
            int count = sourceList.Count;
            for (int i = 0; i < count; i++)
            {
                targetList.Add(sourceList[i].CloneTo(targetModel));
            }
        }

        private void CopyFrom(IDocumentModel targetModel, List<IDrawingFill> targetList, List<IDrawingFill> sourceList)
        {
            int count = sourceList.Count;
            for (int i = 0; i < count; i++)
            {
                targetList.Add(sourceList[i].CloneTo(targetModel));
            }
        }

        private void CopyFrom(IDocumentModel targetModel, List<Outline> targetList, List<Outline> sourceList)
        {
            int count = sourceList.Count;
            for (int i = 0; i < count; i++)
            {
                targetList.Add(sourceList[i].CloneTo(targetModel));
            }
        }

        public DrawingEffectStyle GetEffectStyle(StyleMatrixElementType type) => 
            this.GetElement<DrawingEffectStyle>(type, this.effectStyleList);

        public DrawingEffectStyle GetEffectStyle(int index) => 
            this.GetElement<DrawingEffectStyle>(index, this.effectStyleList);

        private T GetElement<T>(StyleMatrixElementType type, List<T> items) where T: class
        {
            if (type == StyleMatrixElementType.Subtle)
            {
                return items[0];
            }
            if (type == StyleMatrixElementType.Moderate)
            {
                return items[1];
            }
            if (type == StyleMatrixElementType.Intense)
            {
                return items[2];
            }
            return default(T);
        }

        private T GetElement<T>(int index, List<T> items) where T: class
        {
            int count = items.Count;
            if ((index >= 1) && (count != 0))
            {
                return items[Math.Min(index, count) - 1];
            }
            return default(T);
        }

        public IDrawingFill GetFill(StyleMatrixElementType type) => 
            this.GetElement<IDrawingFill>(type, this.fillStyleList);

        public IDrawingFill GetFill(int index) => 
            (index >= 0x3e8) ? this.GetElement<IDrawingFill>((int) (index - 0x3e8), this.backgroundFillStyleList) : this.GetElement<IDrawingFill>(index, this.fillStyleList);

        public Outline GetOutline(StyleMatrixElementType type) => 
            this.GetElement<Outline>(type, this.lineStyleList);

        public Outline GetOutline(int index) => 
            this.GetElement<Outline>(index, this.lineStyleList);

        public string Name
        {
            get => 
                this.name;
            set => 
                this.name = value;
        }

        public List<IDrawingFill> BackgroundFillStyleList =>
            this.backgroundFillStyleList;

        public List<IDrawingFill> FillStyleList =>
            this.fillStyleList;

        public List<Outline> LineStyleList =>
            this.lineStyleList;

        public List<DrawingEffectStyle> EffectStyleList =>
            this.effectStyleList;

        public bool IsValidate =>
            this.CheckValidation();
    }
}

