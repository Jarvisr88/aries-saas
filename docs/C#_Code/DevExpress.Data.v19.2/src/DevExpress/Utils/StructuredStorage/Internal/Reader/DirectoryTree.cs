namespace DevExpress.Utils.StructuredStorage.Internal.Reader
{
    using DevExpress.Utils.StructuredStorage.Internal;
    using DevExpress.Utils.StructuredStorage.Reader;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    [CLSCompliant(false)]
    public class DirectoryTree
    {
        private readonly Fat fat;
        private readonly Header header;
        private readonly InputHandler fileHandler;
        private readonly List<DirectoryEntry> directoryEntries = new List<DirectoryEntry>();
        private List<uint> sectorsUsedByDirectory;
        private readonly Dictionary<string, DirectoryEntry> entryCacheByName = new Dictionary<string, DirectoryEntry>();
        private readonly Dictionary<string, DirectoryEntry> entryCacheByPath = new Dictionary<string, DirectoryEntry>();
        private readonly Dictionary<uint, DirectoryEntry> entryCacheBySid = new Dictionary<uint, DirectoryEntry>();

        public DirectoryTree(Fat fat, Header header, InputHandler fileHandler)
        {
            this.fat = fat;
            this.header = header;
            this.fileHandler = fileHandler;
            this.Init(header.DirectoryStartSector);
        }

        private void GetAllDirectoryEntriesRecursive(uint sid, string path)
        {
            DirectoryEntry item = this.ReadDirectoryEntry(sid, path);
            if (!string.IsNullOrEmpty(item.Name) && (this.GetDirectoryEntry(item.Sid) == null))
            {
                this.directoryEntries.Add(item);
                if (!this.entryCacheByName.ContainsKey(item.Name))
                {
                    this.entryCacheByName[item.Name] = item;
                }
                if (!this.entryCacheByPath.ContainsKey(item.Path))
                {
                    this.entryCacheByPath[item.Path] = item;
                }
                if (!this.entryCacheBySid.ContainsKey(item.Sid))
                {
                    this.entryCacheBySid[item.Sid] = item;
                }
                if (item.LeftSiblingSid != uint.MaxValue)
                {
                    this.GetAllDirectoryEntriesRecursive(item.LeftSiblingSid, path);
                }
                if (item.RightSiblingSid != uint.MaxValue)
                {
                    this.GetAllDirectoryEntriesRecursive(item.RightSiblingSid, path);
                }
                if (item.ChildSiblingSid != uint.MaxValue)
                {
                    this.GetAllDirectoryEntriesRecursive(item.ChildSiblingSid, path + ((sid == 0) ? string.Empty : item.Name) + @"\");
                }
            }
        }

        internal ReadOnlyCollection<DirectoryEntry> GetAllEntries() => 
            new ReadOnlyCollection<DirectoryEntry>(this.directoryEntries);

        internal ReadOnlyCollection<DirectoryEntry> GetAllStreamEntries()
        {
            List<DirectoryEntry> list = new List<DirectoryEntry>();
            int count = this.directoryEntries.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.directoryEntries[i].Type == DirectoryEntryType.Stream)
                {
                    list.Add(this.directoryEntries[i]);
                }
            }
            return new ReadOnlyCollection<DirectoryEntry>(list);
        }

        internal DirectoryEntry GetDirectoryEntry(string path)
        {
            DirectoryEntry entry;
            return ((path.Length >= 1) ? ((path[0] != '\\') ? (!this.entryCacheByName.TryGetValue(path, out entry) ? null : entry) : (!this.entryCacheByPath.TryGetValue(path, out entry) ? null : entry)) : null);
        }

        internal DirectoryEntry GetDirectoryEntry(uint sid)
        {
            DirectoryEntry entry;
            return (!this.entryCacheBySid.TryGetValue(sid, out entry) ? null : entry);
        }

        internal uint GetMiniStreamStart()
        {
            DirectoryEntry directoryEntry = this.GetDirectoryEntry((uint) 0);
            if (directoryEntry == null)
            {
                this.ThrowStreamNotFoundException("Root Entry");
            }
            uint startSector = directoryEntry.StartSector;
            return ((startSector == uint.MaxValue) ? 0xfffffffe : startSector);
        }

        internal ReadOnlyCollection<string> GetPathsOfAllEntries()
        {
            List<string> list = new List<string>();
            foreach (DirectoryEntry entry in this.directoryEntries)
            {
                list.Add(entry.Path);
            }
            return new ReadOnlyCollection<string>(list);
        }

        internal ReadOnlyCollection<string> GetPathsOfAllStreamEntries()
        {
            List<string> list = new List<string>();
            foreach (DirectoryEntry entry in this.directoryEntries)
            {
                if (entry.Type == DirectoryEntryType.Stream)
                {
                    list.Add(entry.Path);
                }
            }
            return new ReadOnlyCollection<string>(list);
        }

        internal ulong GetSizeOfMiniStream()
        {
            DirectoryEntry directoryEntry = this.GetDirectoryEntry((uint) 0);
            if (directoryEntry == null)
            {
                this.ThrowStreamNotFoundException("Root Entry");
            }
            return directoryEntry.StreamLength;
        }

        private void Init(uint startSector)
        {
            this.sectorsUsedByDirectory = (this.header.NoSectorsInDirectoryChain4KB <= 0) ? this.fat.GetSectorChain(startSector, (ulong) Math.Ceiling((double) (((double) this.fileHandler.IOStreamSize) / ((double) this.header.SectorSize))), "Directory", true) : this.fat.GetSectorChain(startSector, (ulong) this.header.NoSectorsInDirectoryChain4KB, "Directory");
            this.GetAllDirectoryEntriesRecursive(0, "");
        }

        private DirectoryEntry ReadDirectoryEntry(uint sid, string path)
        {
            this.SeekToDirectoryEntry(sid);
            return new DirectoryEntry(this.header, this.fileHandler, sid, path);
        }

        private void SeekToDirectoryEntry(uint sid)
        {
            int num = (int) ((sid * 0x80) / this.header.SectorSize);
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.fileHandler.SeekToPositionInSector((long) ((ulong) this.sectorsUsedByDirectory[num]), (long) ((sid * 0x80) % this.header.SectorSize));
        }

        internal void ThrowStreamNotFoundException(string name)
        {
            throw new Exception("Stream with name '" + name + "' not found.");
        }
    }
}

