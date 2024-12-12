namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;
    using System.Globalization;

    public class DrawingLanguageChangedHistoryItem : DrawingHistoryItem<DrawingTextCharacterProperties, CultureInfo>
    {
        private bool alternateLanguage;

        public DrawingLanguageChangedHistoryItem(DrawingTextCharacterProperties owner, bool alternateLanguage, CultureInfo oldValue, CultureInfo newValue) : base(owner.DocumentModel.MainPart, owner, oldValue, newValue)
        {
            this.alternateLanguage = alternateLanguage;
        }

        protected override void RedoCore()
        {
            if (this.alternateLanguage)
            {
                base.Owner.SetAlternateLanguageCore(base.NewValue);
            }
            else
            {
                base.Owner.SetLanguageCore(base.NewValue);
            }
        }

        protected override void UndoCore()
        {
            if (this.alternateLanguage)
            {
                base.Owner.SetAlternateLanguageCore(base.OldValue);
            }
            else
            {
                base.Owner.SetLanguageCore(base.OldValue);
            }
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.alternateLanguage);
            writer.Write((base.OldValue != null) ? base.OldValue.Name : string.Empty);
            writer.Write((base.NewValue != null) ? base.NewValue.Name : string.Empty);
        }
    }
}

