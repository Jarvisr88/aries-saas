namespace DevExpress.Utils.StructuredStorage.Internal
{
    using System;
    using System.Globalization;

    [CLSCompliant(false)]
    public abstract class AbstractDirectoryEntry
    {
        private uint sid;
        private string path;
        private string name;
        private DirectoryEntryType type;
        private DirectoryEntryColor color;
        private uint leftSiblingSid;
        private uint rightSiblingSid;
        private uint childSiblingSid;
        private Guid clsId;
        private uint userFlags;
        private uint startSector;
        private ulong streamLength;
        private static readonly char[] CharsToMask = new char[] { '%', '\\' };

        protected AbstractDirectoryEntry() : this(0)
        {
        }

        protected AbstractDirectoryEntry(uint sid)
        {
            this.sid = sid;
        }

        internal static string Mask(string text)
        {
            string str = text;
            foreach (char ch in CharsToMask)
            {
                object[] args = new object[] { ch };
                str = str.Replace(new string(ch, 1), string.Format(CultureInfo.InvariantCulture, "%{0:X4}", args));
            }
            return str;
        }

        private void ThrowInvalidValueInDirectoryEntryException(string name)
        {
            throw new Exception("The value for '" + name + "' is invalid.");
        }

        public uint Sid
        {
            get => 
                this.sid;
            protected internal set => 
                this.sid = value;
        }

        public string Path =>
            this.path + this.Name;

        protected internal string InnerPath
        {
            get => 
                this.path;
            set => 
                this.path = value;
        }

        public string Name
        {
            get => 
                Mask(this.name);
            protected set
            {
                this.name = value;
                if (this.name.Length >= 0x20)
                {
                    this.ThrowInvalidValueInDirectoryEntryException("Name.Length >= 32");
                }
            }
        }

        protected internal string InnerName =>
            this.name;

        public ushort LengthOfName =>
            (this.name.Length != 0) ? ((ushort) ((this.name.Length + 1) * 2)) : 0;

        public DirectoryEntryType Type
        {
            get => 
                this.type;
            protected set
            {
                if ((value < DirectoryEntryType.MinValue) || (value > DirectoryEntryType.Root))
                {
                    this.ThrowInvalidValueInDirectoryEntryException("Type");
                }
                this.type = value;
            }
        }

        public DirectoryEntryColor Color
        {
            get => 
                this.color;
            protected internal set
            {
                if ((value < DirectoryEntryColor.MinValue) || (value > DirectoryEntryColor.Black))
                {
                    this.ThrowInvalidValueInDirectoryEntryException("Color");
                }
                this.color = value;
            }
        }

        public uint LeftSiblingSid
        {
            get => 
                this.leftSiblingSid;
            protected internal set => 
                this.leftSiblingSid = value;
        }

        public uint RightSiblingSid
        {
            get => 
                this.rightSiblingSid;
            protected internal set => 
                this.rightSiblingSid = value;
        }

        public uint ChildSiblingSid
        {
            get => 
                this.childSiblingSid;
            protected set => 
                this.childSiblingSid = value;
        }

        public Guid ClsId
        {
            get => 
                this.clsId;
            protected set => 
                this.clsId = value;
        }

        public uint UserFlags
        {
            get => 
                this.userFlags;
            protected set => 
                this.userFlags = value;
        }

        public uint StartSector
        {
            get => 
                this.startSector;
            protected set => 
                this.startSector = value;
        }

        public ulong StreamLength
        {
            get => 
                this.streamLength;
            protected set => 
                this.streamLength = value;
        }
    }
}

