namespace DevExpress.Utils.StructuredStorage.Internal.Writer
{
    using DevExpress.Utils.StructuredStorage.Internal;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [CLSCompliant(false)]
    public class StorageDirectoryEntry : BaseDirectoryEntry
    {
        private readonly List<StorageDirectoryEntry> storageDirectoryEntries;
        private readonly List<StreamDirectoryEntry> streamDirectoryEntries;
        private readonly List<BaseDirectoryEntry> allDirectoryEntries;

        public StorageDirectoryEntry(string name, StructuredStorageContext context) : base(name, context)
        {
            this.storageDirectoryEntries = new List<StorageDirectoryEntry>();
            this.streamDirectoryEntries = new List<StreamDirectoryEntry>();
            this.allDirectoryEntries = new List<BaseDirectoryEntry>();
            base.Type = DirectoryEntryType.Storage;
        }

        public StorageDirectoryEntry AddStorageDirectoryEntry(string name)
        {
            StorageDirectoryEntry item = null;
            item = this.storageDirectoryEntries.Find(a => name == a.Name);
            if (item == null)
            {
                item = new StorageDirectoryEntry(name, base.Context);
                this.storageDirectoryEntries.Add(item);
                this.allDirectoryEntries.Add(item);
            }
            return item;
        }

        public void AddStreamDirectoryEntry(string name, Stream stream)
        {
            if (this.streamDirectoryEntries.Find(a => name == a.Name) == null)
            {
                StreamDirectoryEntry item = new StreamDirectoryEntry(name, stream, base.Context);
                this.streamDirectoryEntries.Add(item);
                this.allDirectoryEntries.Add(item);
            }
        }

        private uint CreateRedBlackTree()
        {
            this.allDirectoryEntries.Sort(new Comparison<BaseDirectoryEntry>(this.DirectoryEntryComparison));
            foreach (BaseDirectoryEntry entry in this.allDirectoryEntries)
            {
                entry.Sid = base.Context.GetNewSid();
            }
            return this.SetRelationsAndColorRecursive(this.allDirectoryEntries, (int) Math.Floor(Math.Log((double) this.allDirectoryEntries.Count, 2.0)), 0);
        }

        protected int DirectoryEntryComparison(BaseDirectoryEntry a, BaseDirectoryEntry b)
        {
            if (a.Name.Length != b.Name.Length)
            {
                return a.Name.Length.CompareTo(b.Name.Length);
            }
            string str = a.Name.ToUpper();
            string str2 = b.Name.ToUpper();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != str2[i])
                {
                    return ((uint) str[i]).CompareTo((uint) str2[i]);
                }
            }
            return 0;
        }

        private static int GetMiddleIndex(List<BaseDirectoryEntry> list) => 
            (int) Math.Floor((double) (((double) (list.Count - 1)) / 2.0));

        internal void RecursiveCreateRedBlackTrees()
        {
            base.ChildSiblingSid = this.CreateRedBlackTree();
            foreach (StorageDirectoryEntry entry in this.storageDirectoryEntries)
            {
                entry.RecursiveCreateRedBlackTrees();
            }
        }

        internal List<BaseDirectoryEntry> RecursiveGetAllDirectoryEntries()
        {
            List<BaseDirectoryEntry> result = new List<BaseDirectoryEntry>();
            return this.RecursiveGetAllDirectoryEntries(result);
        }

        private List<BaseDirectoryEntry> RecursiveGetAllDirectoryEntries(List<BaseDirectoryEntry> result)
        {
            foreach (StorageDirectoryEntry entry in this.storageDirectoryEntries)
            {
                result.AddRange(entry.RecursiveGetAllDirectoryEntries());
            }
            foreach (StreamDirectoryEntry entry2 in this.streamDirectoryEntries)
            {
                result.Add(entry2);
            }
            if (!result.Contains(this))
            {
                result.Add(this);
            }
            return result;
        }

        public void setClsId(Guid clsId)
        {
            base.ClsId = clsId;
        }

        private uint SetRelationsAndColorRecursive(List<BaseDirectoryEntry> entryList, int treeHeight, int treeLevel)
        {
            if (entryList.Count < 1)
            {
                return uint.MaxValue;
            }
            if (entryList.Count == 1)
            {
                if (treeLevel == treeHeight)
                {
                    entryList[0].Color = DirectoryEntryColor.MinValue;
                }
                return entryList[0].Sid;
            }
            int middleIndex = GetMiddleIndex(entryList);
            List<BaseDirectoryEntry> range = entryList.GetRange(0, middleIndex);
            List<BaseDirectoryEntry> list = entryList.GetRange(middleIndex + 1, (entryList.Count - middleIndex) - 1);
            int num2 = GetMiddleIndex(range);
            int num3 = GetMiddleIndex(list);
            if (range.Count > 0)
            {
                entryList[middleIndex].LeftSiblingSid = range[num2].Sid;
                this.SetRelationsAndColorRecursive(range, treeHeight, treeLevel + 1);
            }
            if (list.Count > 0)
            {
                entryList[middleIndex].RightSiblingSid = list[num3].Sid;
                this.SetRelationsAndColorRecursive(list, treeHeight, treeLevel + 1);
            }
            return entryList[middleIndex].Sid;
        }

        protected internal override void WriteReferencedStream()
        {
        }

        internal List<StreamDirectoryEntry> StreamDirectoryEntries =>
            this.streamDirectoryEntries;

        internal List<StorageDirectoryEntry> StorageDirectoryEntries =>
            this.storageDirectoryEntries;
    }
}

