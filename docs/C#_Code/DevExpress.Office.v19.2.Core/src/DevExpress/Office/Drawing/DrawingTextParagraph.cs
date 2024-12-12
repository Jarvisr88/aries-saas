namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class DrawingTextParagraph : ICloneable<DrawingTextParagraph>, ISupportsCopyFrom<DrawingTextParagraph>
    {
        private const int ApplyParagraphPropertiesIndex = 0;
        private const int ApplyEndRunPropertiesIndex = 1;
        private readonly InvalidateProxy innerParent = new InvalidateProxy();
        private readonly DrawingTextRunCollection runs;
        private readonly DrawingTextParagraphProperties paragraphProperties;
        private readonly DrawingTextCharacterProperties endRunProperties;
        private bool[] options;

        public DrawingTextParagraph(IDocumentModel documentModel)
        {
            DrawingTextRunCollection collection1 = new DrawingTextRunCollection(documentModel);
            collection1.Parent = this.innerParent;
            this.runs = collection1;
            DrawingTextParagraphProperties properties1 = new DrawingTextParagraphProperties(documentModel);
            properties1.Parent = this.innerParent;
            this.paragraphProperties = properties1;
            DrawingTextCharacterProperties properties2 = new DrawingTextCharacterProperties(documentModel);
            properties2.Parent = this.innerParent;
            this.endRunProperties = properties2;
            this.options = new bool[2];
        }

        private int CalcSpacingPoints(IDrawingTextSpacing spacing)
        {
            switch (spacing.Type)
            {
                case DrawingTextSpacingValueType.Automatic:
                    return 0;

                case DrawingTextSpacingValueType.Percent:
                {
                    int num = 0;
                    foreach (IDrawingTextRun run in this.runs)
                    {
                        if (run.RunProperties.Options.HasFontSize)
                        {
                            num = Math.Max(run.RunProperties.FontSize, num);
                        }
                    }
                    if (this.ApplyEndRunProperties)
                    {
                        num = Math.Max(this.endRunProperties.FontSize, num);
                    }
                    return ((num * spacing.Value) / 0x186a0);
                }
                case DrawingTextSpacingValueType.Points:
                    return spacing.Value;
            }
            throw new ArgumentOutOfRangeException();
        }

        public DrawingTextParagraph Clone()
        {
            DrawingTextParagraph paragraph = new DrawingTextParagraph(this.DocumentModel);
            paragraph.CopyFrom(this);
            return paragraph;
        }

        public DrawingTextParagraph CloneTo(IDocumentModel documentModel)
        {
            DrawingTextParagraph paragraph = new DrawingTextParagraph(documentModel);
            paragraph.CopyFrom(this);
            return paragraph;
        }

        public void CopyFrom(DrawingTextParagraph value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.runs.CopyFrom(value.runs);
            this.paragraphProperties.CopyFrom(value.paragraphProperties);
            this.endRunProperties.CopyFrom(value.endRunProperties);
            this.options[0] = value.ApplyParagraphProperties;
            this.options[1] = value.ApplyEndRunProperties;
        }

        public override bool Equals(object obj)
        {
            DrawingTextParagraph paragraph = obj as DrawingTextParagraph;
            return ((paragraph != null) ? (this.runs.Equals(paragraph.runs) ? (this.paragraphProperties.Equals(paragraph.paragraphProperties) ? (this.endRunProperties.Equals(paragraph.endRunProperties) ? ((this.ApplyParagraphProperties == paragraph.ApplyParagraphProperties) && (this.ApplyEndRunProperties == paragraph.ApplyEndRunProperties)) : false) : false) : false) : false);
        }

        public override int GetHashCode() => 
            (((this.runs.GetHashCode() ^ this.paragraphProperties.GetHashCode()) ^ this.endRunProperties.GetHashCode()) ^ this.ApplyParagraphProperties.GetHashCode()) ^ this.ApplyEndRunProperties.GetHashCode();

        public int GetLineSpacingPoints() => 
            this.CalcSpacingPoints(this.ParagraphProperties.Spacings.Line);

        public string GetPlainText()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.runs.Count; i++)
            {
                builder.Append(this.runs[i].Text);
            }
            return builder.ToString();
        }

        public int GetSpacingAfterPoints() => 
            this.CalcSpacingPoints(this.ParagraphProperties.Spacings.SpaceAfter);

        public int GetSpacingBeforePoints() => 
            this.CalcSpacingPoints(this.ParagraphProperties.Spacings.SpaceBefore);

        private void SetOptions(int index, bool value)
        {
            if (this.options[index] != value)
            {
                HistoryItem item = new DrawingTextParagraphOptionsChangedHistoryItem(this, index, this.options[index], value);
                this.DocumentModel.History.Add(item);
                item.Execute();
            }
        }

        internal void SetOptionsCore(int index, bool value)
        {
            this.options[index] = value;
            this.innerParent.Invalidate();
        }

        public void SetPlainText(string value)
        {
            this.runs.Clear();
            DrawingTextRun item = new DrawingTextRun(this.DocumentModel, value);
            this.runs.Add(item);
        }

        public void SplitRun(int runIndex, int textPosition)
        {
            this.DocumentModel.BeginUpdate();
            try
            {
                DrawingTextRun run = this.runs[runIndex] as DrawingTextRun;
                if (run != null)
                {
                    DrawingTextRun item = new DrawingTextRun(this.DocumentModel);
                    item.CopyFrom(run);
                    item.Text = run.Text.Substring(textPosition);
                    this.Runs.Insert(runIndex + 1, item);
                    run.Text = run.Text.Substring(0, textPosition);
                }
            }
            finally
            {
                this.DocumentModel.EndUpdate();
            }
        }

        public IDocumentModel DocumentModel =>
            this.runs.DocumentModel;

        protected internal ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        public DrawingTextRunCollection Runs =>
            this.runs;

        public DrawingTextParagraphProperties ParagraphProperties =>
            this.paragraphProperties;

        public DrawingTextCharacterProperties EndRunProperties =>
            this.endRunProperties;

        public bool ApplyParagraphProperties
        {
            get => 
                this.options[0];
            set => 
                this.SetOptions(0, value);
        }

        public bool ApplyEndRunProperties
        {
            get => 
                this.options[1];
            set => 
                this.SetOptions(1, value);
        }
    }
}

