namespace DevExpress.Utils.Zip.Internal
{
    using System;

    public class InternalZipExtraFieldFactory : IZipExtraFieldFactory
    {
        public virtual IZipExtraField Create(int headerId) => 
            (headerId != 1) ? null : new Zip64ExtraField();
    }
}

