namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;

    [CLSCompliant(false)]
    public class EmptyDirectoryEntry : BaseDirectoryEntry
    {
        public EmptyDirectoryEntry(StructuredStorageContext context) : base(string.Empty, context)
        {
            base.Color = DirectoryEntryColor.MinValue;
            base.Type = DirectoryEntryType.MinValue;
        }

        protected internal override void WriteReferencedStream()
        {
        }
    }
}

