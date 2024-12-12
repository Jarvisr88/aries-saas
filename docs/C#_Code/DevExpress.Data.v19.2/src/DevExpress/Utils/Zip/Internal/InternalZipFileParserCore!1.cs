namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class InternalZipFileParserCore<T> where T: InternalZipFile, new()
    {
        private readonly IList<T> records;
        private readonly Dictionary<string, T> zipDictionary;

        public InternalZipFileParserCore()
        {
            this.records = this.CreateRecords();
            this.zipDictionary = new Dictionary<string, T>();
        }

        private void Add(T item)
        {
            this.records.Add(item);
            string fileName = item.FileName;
            if (this.ZipDictionary.ContainsKey(fileName))
            {
                this.zipDictionary[fileName] = item;
            }
            else
            {
                this.zipDictionary.Add(fileName, item);
            }
        }

        protected virtual bool CanContinueProcessZipRecord() => 
            true;

        protected abstract IList<T> CreateRecords();
        protected virtual T CreateZipFileInstance() => 
            Activator.CreateInstance<T>();

        protected virtual ZipSignatures FindNextSignature(BinaryReader reader) => 
            (ZipSignatures) reader.ReadInt32();

        protected T FindRecordByName(string name)
        {
            if (this.zipDictionary.ContainsKey(name))
            {
                return this.zipDictionary[name];
            }
            return default(T);
        }

        public virtual void Parse(Stream stream)
        {
            this.Parse(stream, DXEncoding.Default);
        }

        public void Parse(Stream stream, Encoding fileNameEncoding)
        {
            BinaryReader reader = new BinaryReader(stream);
            while (true)
            {
                try
                {
                    ZipSignatures signature = this.FindNextSignature(reader);
                    bool flag = this.ProcessZipRecord(reader, signature, fileNameEncoding);
                    if (flag)
                    {
                        int processedZipRecordCount = this.ProcessedZipRecordCount;
                        this.ProcessedZipRecordCount = processedZipRecordCount + 1;
                    }
                    if ((flag || this.IsTryToReadBadArchive) && this.CanContinueProcessZipRecord())
                    {
                        continue;
                    }
                }
                catch (EndOfStreamException)
                {
                }
                break;
            }
        }

        protected virtual void PorcessFileEntryRecord(BinaryReader reader, Encoding fileNameEncoding)
        {
        }

        protected virtual void ProcessEndOfCentralDirSignature(BinaryReader reader, Encoding fileNameEncoding)
        {
        }

        protected virtual T ProcessZipFile(BinaryReader reader, Encoding fileNameEncoding)
        {
            T local = this.CreateZipFileInstance();
            local.DefaultEncoding = fileNameEncoding;
            local.ReadLocalHeader(reader);
            return local;
        }

        private bool ProcessZipRecord(BinaryReader reader, ZipSignatures signature, Encoding fileNameEncoding)
        {
            bool flag = true;
            if (signature == ZipSignatures.FileEntryRecord)
            {
                this.PorcessFileEntryRecord(reader, fileNameEncoding);
            }
            else if (signature == ZipSignatures.FileRecord)
            {
                T item = this.ProcessZipFile(reader, fileNameEncoding);
                this.Add(item);
            }
            else if (signature != ZipSignatures.EndOfCentralDirSignature)
            {
                flag = false;
            }
            else
            {
                this.ProcessEndOfCentralDirSignature(reader, fileNameEncoding);
            }
            return flag;
        }

        protected IList<T> InnerRecords =>
            this.records;

        protected Dictionary<string, T> ZipDictionary =>
            this.zipDictionary;

        protected virtual bool IsTryToReadBadArchive =>
            false;

        public int ProcessedZipRecordCount { get; private set; }
    }
}

