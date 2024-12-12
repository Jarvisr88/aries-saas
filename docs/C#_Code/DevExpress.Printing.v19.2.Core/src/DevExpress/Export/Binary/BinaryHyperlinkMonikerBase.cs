namespace DevExpress.Export.Binary
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public abstract class BinaryHyperlinkMonikerBase
    {
        private const int classIdSize = 0x10;
        private Guid classId;

        protected BinaryHyperlinkMonikerBase(Guid classId)
        {
            Guard.ArgumentNotNull(classId, "classId");
            this.classId = classId;
        }

        public virtual int GetSize() => 
            0x10;

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(this.classId.ToByteArray());
        }

        public Guid ClassId =>
            this.classId;
    }
}

