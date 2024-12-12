namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;

    [CLSCompliant(false)]
    public abstract class BaseDirectoryEntry : AbstractDirectoryEntry
    {
        private readonly StructuredStorageContext context;

        internal BaseDirectoryEntry(string name, StructuredStorageContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            this.context = context;
            base.Name = name;
            this.Init();
        }

        private void Init()
        {
            base.ChildSiblingSid = uint.MaxValue;
            base.LeftSiblingSid = uint.MaxValue;
            base.RightSiblingSid = uint.MaxValue;
            base.ClsId = Guid.Empty;
            base.Color = DirectoryEntryColor.Black;
            base.StartSector = 0;
            base.ClsId = Guid.Empty;
            base.UserFlags = 0;
            base.StreamLength = 0L;
        }

        internal void Write()
        {
            OutputHandler directoryStream = this.context.DirectoryStream;
            int num = 0;
            char[] chArray2 = base.InnerName.ToCharArray();
            for (int i = 0; i < chArray2.Length; i++)
            {
                ushort num3 = chArray2[i];
                directoryStream.WriteUInt16(num3);
                num++;
            }
            while (num < 0x20)
            {
                directoryStream.WriteUInt16(0);
                num++;
            }
            directoryStream.WriteUInt16(base.LengthOfName);
            directoryStream.WriteByte((byte) base.Type);
            directoryStream.WriteByte((byte) base.Color);
            directoryStream.WriteUInt32(base.LeftSiblingSid);
            directoryStream.WriteUInt32(base.RightSiblingSid);
            directoryStream.WriteUInt32(base.ChildSiblingSid);
            directoryStream.Write(base.ClsId.ToByteArray());
            directoryStream.WriteUInt32(base.UserFlags);
            directoryStream.Write(new byte[0x10]);
            directoryStream.WriteUInt32(base.StartSector);
            directoryStream.WriteUInt64(base.StreamLength);
        }

        protected internal abstract void WriteReferencedStream();

        internal StructuredStorageContext Context =>
            this.context;
    }
}

