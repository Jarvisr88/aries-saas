namespace DevExpress.XtraExport.OfficeArt
{
    using System;

    internal abstract class OfficeArtBlipBase : OfficeArtPartBase
    {
        protected OfficeArtBlipBase()
        {
        }

        public abstract byte BlipType { get; }

        public abstract byte[] Digest { get; }

        public abstract int NumberOfReferences { get; }
    }
}

