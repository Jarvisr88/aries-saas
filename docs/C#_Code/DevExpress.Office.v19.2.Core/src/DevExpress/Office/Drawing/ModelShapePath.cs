namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class ModelShapePath : ICloneable<ModelShapePath>, ISupportsCopyFrom<ModelShapePath>
    {
        private IDocumentModelPart documentModelPart;
        private long width;
        private long height;
        private PathFillMode fillMode;
        private bool stroke;
        private bool extrusionOK;

        public ModelShapePath(IDocumentModelPart documentModelPart)
        {
            this.documentModelPart = documentModelPart;
            this.SetFillMode(PathFillMode.Norm);
            this.SetStroke(true);
            this.SetExtrusionOK(true);
            this.Instructions = new ModelPathInstructionList(documentModelPart);
        }

        public ModelShapePath Clone() => 
            this.Clone(true);

        public ModelShapePath Clone(bool copyInstructions)
        {
            ModelShapePath path = new ModelShapePath(this.documentModelPart);
            path.CopyFrom(this, copyInstructions);
            return path;
        }

        public void CopyFrom(ModelShapePath value)
        {
            this.CopyFrom(value, true);
        }

        public void CopyFrom(ModelShapePath value, bool copyInstructions)
        {
            Guard.ArgumentNotNull(value, "ModelShapePath");
            this.width = value.Width;
            this.height = value.Height;
            this.fillMode = value.FillMode;
            this.stroke = value.Stroke;
            this.extrusionOK = value.ExtrusionOK;
            if (copyInstructions)
            {
                this.Instructions.CopyFrom(value.Instructions);
            }
        }

        private void SetExtrusionOK(bool extrusionOK)
        {
            this.extrusionOK = extrusionOK;
        }

        internal void SetFillMode(PathFillMode fillMode)
        {
            this.fillMode = fillMode;
        }

        private void SetHeight(long height)
        {
            this.height = height;
        }

        private void SetStroke(bool stroke)
        {
            this.stroke = stroke;
        }

        private void SetWidth(long width)
        {
            this.width = width;
        }

        public long Width
        {
            get => 
                this.width;
            set
            {
                long width = this.Width;
                if (width != value)
                {
                    ActionLongHistoryItem item = new ActionLongHistoryItem(this.documentModelPart, width, value, new Action<long>(this.SetWidth));
                    this.documentModelPart.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public long Height
        {
            get => 
                this.height;
            set
            {
                long height = this.Height;
                if (height != value)
                {
                    ActionLongHistoryItem item = new ActionLongHistoryItem(this.documentModelPart, height, value, new Action<long>(this.SetHeight));
                    this.documentModelPart.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public PathFillMode FillMode
        {
            get => 
                this.fillMode;
            set
            {
                PathFillMode fillMode = this.fillMode;
                if (fillMode != value)
                {
                    ModelShapePathFillModeHistoryItem item = new ModelShapePathFillModeHistoryItem(this, fillMode, value);
                    this.documentModelPart.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public bool Stroke
        {
            get => 
                this.stroke;
            set
            {
                bool stroke = this.stroke;
                if (stroke != value)
                {
                    ActionBooleanHistoryItem item = new ActionBooleanHistoryItem(this.documentModelPart, stroke, value, new Action<bool>(this.SetStroke));
                    this.documentModelPart.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public bool ExtrusionOK
        {
            get => 
                this.extrusionOK;
            set
            {
                bool extrusionOK = this.ExtrusionOK;
                if (extrusionOK != value)
                {
                    ActionBooleanHistoryItem item = new ActionBooleanHistoryItem(this.documentModelPart, extrusionOK, value, new Action<bool>(this.SetExtrusionOK));
                    this.documentModelPart.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public IDocumentModelPart DocumentModelPart =>
            this.documentModelPart;

        public ModelPathInstructionList Instructions { get; private set; }
    }
}

