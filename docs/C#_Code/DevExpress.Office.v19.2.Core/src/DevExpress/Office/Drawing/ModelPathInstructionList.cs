namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ModelPathInstructionList : UndoableCollection<IPathInstruction>, ICloneable<ModelPathInstructionList>, ISupportsCopyFrom<ModelPathInstructionList>
    {
        public ModelPathInstructionList(IDocumentModelPart documentModelPart) : base(documentModelPart)
        {
        }

        public ModelPathInstructionList Clone()
        {
            ModelPathInstructionList list = new ModelPathInstructionList(base.DocumentModelPart);
            list.CopyFrom(this);
            return list;
        }

        public void CopyFrom(ModelPathInstructionList value)
        {
            Guard.ArgumentNotNull(value, "ModelPathInstructionList");
            this.Clear();
            foreach (IPathInstruction instruction in value)
            {
                this.AddInternal(instruction.Clone());
            }
        }

        public override IPathInstruction DeserializeItem(IHistoryReader reader) => 
            reader.GetObject(reader.ReadInt32()) as IPathInstruction;
    }
}

