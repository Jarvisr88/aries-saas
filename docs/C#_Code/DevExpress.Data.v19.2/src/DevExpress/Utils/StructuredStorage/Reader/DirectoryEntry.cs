namespace DevExpress.Utils.StructuredStorage.Reader
{
    using DevExpress.Utils;
    using DevExpress.Utils.StructuredStorage.Internal;
    using DevExpress.Utils.StructuredStorage.Internal.Reader;
    using System;

    [CLSCompliant(false)]
    public class DirectoryEntry : AbstractDirectoryEntry
    {
        private readonly InputHandler fileHandler;
        private readonly Header header;

        internal DirectoryEntry(Header header, InputHandler fileHandler, uint sid, string path) : base(sid)
        {
            Guard.ArgumentNotNull(header, "header");
            Guard.ArgumentNotNull(fileHandler, "fileHandler");
            this.header = header;
            this.fileHandler = fileHandler;
            this.ReadDirectoryEntry();
            base.InnerPath = path;
        }

        private void ReadDirectoryEntry()
        {
            string str = this.fileHandler.ReadString(0x40);
            if (str.Length >= 0x20)
            {
                base.Name = string.Empty;
            }
            else
            {
                base.Name = str;
                this.fileHandler.ReadUInt16();
                base.Type = (DirectoryEntryType) this.fileHandler.ReadByte();
                base.Color = (DirectoryEntryColor) this.fileHandler.ReadByte();
                base.LeftSiblingSid = this.fileHandler.ReadUInt32();
                base.RightSiblingSid = this.fileHandler.ReadUInt32();
                base.ChildSiblingSid = this.fileHandler.ReadUInt32();
                byte[] array = new byte[0x10];
                this.fileHandler.Read(array);
                base.ClsId = new Guid(array);
                base.UserFlags = this.fileHandler.ReadUInt32();
                this.fileHandler.ReadUInt64();
                this.fileHandler.ReadUInt64();
                base.StartSector = this.fileHandler.ReadUInt32();
                uint num = this.fileHandler.ReadUInt32();
                uint num2 = this.fileHandler.ReadUInt32();
                if ((this.header.SectorSize == 0x200) && (num2 != 0))
                {
                    num2 = 0;
                }
                base.StreamLength = (num2 << 0x20) + num;
            }
        }
    }
}

