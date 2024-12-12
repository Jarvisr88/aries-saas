namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public class PathClose : IPathInstruction, ICloneable<IPathInstruction>
    {
        public IPathInstruction Clone() => 
            new PathClose();

        public void Visit(IPathInstructionWalker visitor)
        {
            visitor.Visit(this);
        }
    }
}

