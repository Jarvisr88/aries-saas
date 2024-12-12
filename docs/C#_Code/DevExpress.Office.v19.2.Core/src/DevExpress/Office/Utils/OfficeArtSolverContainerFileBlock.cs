namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class OfficeArtSolverContainerFileBlock : OfficeDrawingPartBase
    {
        protected OfficeArtSolverContainerFileBlock()
        {
        }

        protected internal abstract void Read(BinaryReader reader);

        public int RuleId { get; protected set; }

        public override int HeaderInstanceInfo =>
            0;
    }
}

