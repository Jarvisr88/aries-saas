namespace DevExpress.XtraExport
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public abstract class ExportStyleManagerBase
    {
        private static readonly Dictionary<string, ExportStyleManagerBase> instances = new Dictionary<string, ExportStyleManagerBase>();
        private static readonly Dictionary<Stream, ExportStyleManagerBase> instances2 = new Dictionary<Stream, ExportStyleManagerBase>();
        private readonly IndexedDictionary<ExportCacheCellStyle> styles = new IndexedDictionary<ExportCacheCellStyle>();
        private readonly string fileName;
        private readonly Stream stream;

        protected ExportStyleManagerBase(string fileName, Stream stream)
        {
            this.fileName = fileName;
            this.stream = stream;
            this.RegisterStyle(this.CreateDefaultStyle());
        }

        public void Clear()
        {
            this.styles.Clear();
            this.RegisterStyle(this.CreateDefaultStyle());
        }

        protected abstract ExportCacheCellStyle CreateDefaultStyle();
        internal void DisposeInstance()
        {
            DisposeInstance(this.fileName, this.stream);
        }

        public static void DisposeInstance(string fileName, Stream stream)
        {
            if (ExportCustomProvider.IsValidFileName(fileName))
            {
                Dictionary<string, ExportStyleManagerBase> instances = ExportStyleManagerBase.instances;
                lock (instances)
                {
                    ExportStyleManagerBase.instances.Remove(fileName);
                }
            }
            else
            {
                if (!ExportCustomProvider.IsValidStream(stream))
                {
                    throw new ExportCacheException("Can't dispose the instance of ExportStyleManager class: Ivalid parameter values.");
                }
                Dictionary<Stream, ExportStyleManagerBase> dictionary2 = instances2;
                lock (dictionary2)
                {
                    instances2.Remove(stream);
                }
            }
        }

        private static ExportStyleManagerBase GetFileInstance(string fileName, IExportStyleManagerCreator creator)
        {
            ExportStyleManagerBase base2;
            Dictionary<string, ExportStyleManagerBase> instances = ExportStyleManagerBase.instances;
            lock (instances)
            {
                if (!ExportStyleManagerBase.instances.TryGetValue(fileName, out base2))
                {
                    base2 = creator.CreateInstance(fileName, null);
                    ExportStyleManagerBase.instances.Add(fileName, base2);
                }
            }
            return base2;
        }

        public static ExportStyleManagerBase GetInstance(string fileName, Stream stream, IExportStyleManagerCreator creator)
        {
            if (ExportCustomProvider.IsValidFileName(fileName))
            {
                return GetFileInstance(fileName, creator);
            }
            if (!ExportCustomProvider.IsValidStream(stream))
            {
                throw new ExportCacheException("Can't create the instance of ExportStyleManager class: Ivalid parameter values.");
            }
            return GetStreamInstance(stream, creator);
        }

        private static ExportStyleManagerBase GetStreamInstance(Stream stream, IExportStyleManagerCreator creator)
        {
            ExportStyleManagerBase base2;
            Dictionary<Stream, ExportStyleManagerBase> dictionary = instances2;
            lock (dictionary)
            {
                if (!instances2.TryGetValue(stream, out base2))
                {
                    base2 = creator.CreateInstance(string.Empty, stream);
                    instances2.Add(stream, base2);
                }
            }
            return base2;
        }

        internal void MarkStyleAsExported(int styleIndex, int result, ushort type)
        {
            ExportCacheCellStyle style = this[styleIndex];
            style.AddExportedType(type, result);
            this.styles[styleIndex] = style;
        }

        public int RegisterStyle(ExportCacheCellStyle style)
        {
            int index = this.styles.IndexOf(style);
            if (index >= 0)
            {
                return index;
            }
            this.styles.Add(style);
            return (this.styles.Count - 1);
        }

        public int Count =>
            this.styles.Count;

        public ExportCacheCellStyle this[int index] =>
            this.styles[index];

        public ExportCacheCellStyle DefaultStyle
        {
            get => 
                this.styles[0];
            set
            {
                if (!this.styles[0].IsEqual(value))
                {
                    this.styles[0] = value;
                }
            }
        }
    }
}

