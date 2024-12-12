namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class ModelShapeGuide : IDocumentModelObject, ICloneable<ModelShapeGuide>, ISupportsCopyFrom<ModelShapeGuide>
    {
        private ModelShapeGuideFormula formula;
        private string name;

        public ModelShapeGuide()
        {
        }

        public ModelShapeGuide(string name, ModelShapeGuideFormula formula)
        {
            this.Name = name;
            this.Formula = formula;
        }

        public ModelShapeGuide(string name, string formula)
        {
            this.Name = name;
            this.Formula = ModelShapeGuideFormula.FromString(formula);
        }

        public ModelShapeGuide Clone()
        {
            ModelShapeGuide guide = new ModelShapeGuide();
            guide.CopyFrom(this);
            return guide;
        }

        public void CopyFrom(ModelShapeGuide value)
        {
            Guard.ArgumentNotNull(value, "ModelShapeGuide");
            if (value.Formula == null)
            {
                this.Formula = null;
            }
            else if (this.Formula == null)
            {
                this.Formula = value.Formula.Clone();
            }
            else
            {
                this.Formula.CopyFrom(value.Formula);
            }
            this.Name = value.Name;
        }

        internal void SetFormula(ModelShapeGuideFormula formula)
        {
            this.formula = formula;
        }

        private void SetName(string name)
        {
            this.name = name;
        }

        public ModelShapeGuideFormula Formula
        {
            get => 
                this.formula;
            set
            {
                ModelShapeGuideFormula objA = this.Formula;
                if (!ReferenceEquals(objA, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetFormula(value);
                    }
                    else
                    {
                        ModelShapeGuideFormulaHistoryItem item = new ModelShapeGuideFormulaHistoryItem(this, objA, value);
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public string Name
        {
            get => 
                this.name;
            set
            {
                string name = this.Name;
                if (name != value)
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetName(value);
                    }
                    else
                    {
                        ActionStringHistoryItem item = new ActionStringHistoryItem(this.DocumentModelPart, name, value, new Action<string>(this.SetName));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public IDocumentModelPart DocumentModelPart { get; set; }
    }
}

