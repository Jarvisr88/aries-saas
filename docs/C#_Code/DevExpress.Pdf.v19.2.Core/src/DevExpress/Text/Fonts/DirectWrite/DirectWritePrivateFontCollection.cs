namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using DevExpress.Text.Fonts.DirectWrite.CCW;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DirectWritePrivateFontCollection : DirectWriteFontCollection, IDXPrivateFontCollection, IDXFontCollection, IDisposable, IDirectWriteFontFileStreamProvider
    {
        private DWriteFontCollection collection;
        private readonly ObjectHandle handle;
        private DXFontFamily[] families;
        private readonly IList<DWriteFontFile> fontFiles;
        private readonly IList<DWriteInMemoryFontFileStream> fileStreams;
        private readonly DWritePrivateFontCollectionFontFileLoader fontFileLoader;

        [SecuritySafeCritical]
        public DirectWritePrivateFontCollection(DWriteFactory factory, DirectWriteFontMeasurer measurer, DWriteFontCollectionLoader collectionLoader, IEnumerable<byte[]> fontFileList) : base(factory, measurer)
        {
            this.families = new DXFontFamily[0];
            this.fontFiles = new List<DWriteFontFile>();
            this.fileStreams = new List<DWriteInMemoryFontFileStream>();
            this.fontFileLoader = new DWritePrivateFontCollectionFontFileLoader(this);
            factory.RegisterFontFileLoader(this.fontFileLoader);
            foreach (byte[] buffer in fontFileList)
            {
                this.fileStreams.Add(new DWriteInMemoryFontFileStream(buffer));
                int num = this.fileStreams.Count - 1;
                GCHandle handle = GCHandle.Alloc(num, GCHandleType.Pinned);
                try
                {
                    this.fontFiles.Add(base.Factory.CreateCustomFontFileReference(handle.AddrOfPinnedObject(), 4, this.fontFileLoader));
                }
                finally
                {
                    handle.Free();
                }
            }
            this.handle = new ObjectHandle(this);
            this.collection = base.Factory.CreateCustomFontCollection(collectionLoader, this.handle.Ptr);
            foreach (DXFontFamily family in this.families)
            {
                family.Dispose();
            }
            this.families = base.CreateFamilies();
        }

        IDWriteFontFileStream IDirectWriteFontFileStreamProvider.GetFontFileStream(int index) => 
            this.fileStreams[index];

        public override void Dispose()
        {
            base.Factory.UnregisterFontFileLoader(this.fontFileLoader);
            foreach (DXFontFamily family in this.families)
            {
                family.Dispose();
            }
            foreach (DWriteFontFile file in this.fontFiles)
            {
                file.Dispose();
            }
            foreach (DWriteInMemoryFontFileStream stream in this.fileStreams)
            {
                stream.Dispose();
            }
            this.Collection.Dispose();
            this.handle.Dispose();
        }

        protected override DWriteFontCollection Collection =>
            this.collection;

        public override IReadOnlyList<IDXFontFamily> Families =>
            this.families;

        public IList<DWriteFontFile> Files =>
            this.fontFiles;

        private class ObjectHandle : IDisposable
        {
            private readonly GCHandle handle;
            private bool disposed;

            [SecuritySafeCritical]
            public ObjectHandle(object obj)
            {
                this.handle = GCHandle.Alloc(obj);
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            [SecuritySafeCritical]
            private void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    this.handle.Free();
                    this.disposed = true;
                }
            }

            ~ObjectHandle()
            {
                this.Dispose(false);
            }

            public IntPtr Ptr =>
                GCHandle.ToIntPtr(this.handle);
        }
    }
}

