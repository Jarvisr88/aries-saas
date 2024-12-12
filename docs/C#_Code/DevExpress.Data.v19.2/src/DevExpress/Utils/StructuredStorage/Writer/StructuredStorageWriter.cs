namespace DevExpress.Utils.StructuredStorage.Writer
{
    using DevExpress.Utils.StructuredStorage.Internal.Writer;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    [CLSCompliant(false)]
    public class StructuredStorageWriter
    {
        private readonly StructuredStorageContext context = new StructuredStorageContext();

        public void AddStreamDirectoryEntry(string name, BinaryWriter binaryWriter)
        {
            char[] separator = new char[] { '\\' };
            string[] strArray = name.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            StorageDirectoryEntry rootDirectoryEntry = this.RootDirectoryEntry;
            for (int i = 0; i < (strArray.Length - 1); i++)
            {
                rootDirectoryEntry = rootDirectoryEntry.AddStorageDirectoryEntry(strArray[i]);
            }
            rootDirectoryEntry.AddStreamDirectoryEntry(strArray[strArray.Length - 1], binaryWriter.BaseStream);
            binaryWriter.Flush();
        }

        public void Write(Stream outputStream)
        {
            this.context.RootDirectoryEntry.RecursiveCreateRedBlackTrees();
            List<BaseDirectoryEntry> list = this.context.RootDirectoryEntry.RecursiveGetAllDirectoryEntries();
            Comparison<BaseDirectoryEntry> comparison = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Comparison<BaseDirectoryEntry> local1 = <>c.<>9__4_0;
                comparison = <>c.<>9__4_0 = (a, b) => a.Sid.CompareTo(b.Sid);
            }
            list.Sort(comparison);
            foreach (BaseDirectoryEntry entry2 in list)
            {
                if (entry2.Sid != 0)
                {
                    entry2.WriteReferencedStream();
                }
            }
            this.context.RootDirectoryEntry.WriteReferencedStream();
            foreach (BaseDirectoryEntry entry3 in list)
            {
                entry3.Write();
            }
            uint num = (uint) (this.context.Header.SectorSize / 0x80);
            uint num2 = num - ((uint) (list.Count % num));
            EmptyDirectoryEntry entry = new EmptyDirectoryEntry(this.context);
            for (int i = 0; i < num2; i++)
            {
                entry.Write();
            }
            VirtualStream stream = new VirtualStream(this.context.DirectoryStream.BaseStream, this.context.Fat, this.context.Header.SectorSize, this.context.TempOutputStream);
            stream.Write();
            this.context.Header.DirectoryStartSector = stream.StartSector;
            if (this.context.Header.SectorSize == 0x1000)
            {
                this.context.Header.NoSectorsInDirectoryChain4KB = stream.SectorCount;
            }
            this.context.MiniFat.Write();
            this.context.Header.MiniFatStartSector = this.context.MiniFat.MiniFatStart;
            this.context.Header.NoSectorsInMiniFatChain = this.context.MiniFat.NumMiniFatSectors;
            this.context.Fat.Write();
            this.context.Header.NoSectorsInDiFatChain = this.context.Fat.NumDiFatSectors;
            this.context.Header.NoSectorsInFatChain = this.context.Fat.NumFatSectors;
            this.context.Header.DiFatStartSector = this.context.Fat.DiFatStartSector;
            this.context.Header.Write();
            this.context.Header.writeToStream(outputStream);
            this.context.TempOutputStream.WriteToStream(outputStream);
        }

        public StorageDirectoryEntry RootDirectoryEntry =>
            this.context.RootDirectoryEntry;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StructuredStorageWriter.<>c <>9 = new StructuredStorageWriter.<>c();
            public static Comparison<BaseDirectoryEntry> <>9__4_0;

            internal int <Write>b__4_0(BaseDirectoryEntry a, BaseDirectoryEntry b) => 
                a.Sid.CompareTo(b.Sid);
        }
    }
}

