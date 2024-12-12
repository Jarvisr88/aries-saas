namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IPathInstruction : ICloneable<IPathInstruction>
    {
        void Visit(IPathInstructionWalker visitor);
    }
}

