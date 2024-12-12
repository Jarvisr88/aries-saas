namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class ModelShapeGuideFormulaHistoryItem : HistoryItem
    {
        private readonly ModelShapeGuide shapeGuide;
        private readonly ModelShapeGuideFormula oldValue;
        private readonly ModelShapeGuideFormula newValue;

        public ModelShapeGuideFormulaHistoryItem(ModelShapeGuide shapeGuide, ModelShapeGuideFormula oldValue, ModelShapeGuideFormula newValue) : base(shapeGuide.DocumentModelPart)
        {
            this.shapeGuide = shapeGuide;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override object GetTargetObject() => 
            this.shapeGuide;

        protected override void RedoCore()
        {
            this.shapeGuide.SetFormula(this.newValue);
        }

        protected override void UndoCore()
        {
            this.shapeGuide.SetFormula(this.oldValue);
        }

        public override void Write(IHistoryWriter writer)
        {
            base.Write(writer);
            writer.Write(this.oldValue.ToString());
            writer.Write(this.newValue.ToString());
        }
    }
}

