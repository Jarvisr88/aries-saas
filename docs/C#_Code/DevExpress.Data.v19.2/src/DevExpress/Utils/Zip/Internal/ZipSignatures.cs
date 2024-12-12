namespace DevExpress.Utils.Zip.Internal
{
    using System;

    public enum ZipSignatures
    {
        FileRecord = 0x4034b50,
        DataDescriptorRecord = 0x8074b50,
        MultiVolumeArchiveRecord = 0x8074b50,
        ArchiveExtraDataRecord = 0x8064b50,
        FileEntryRecord = 0x2014b50,
        DigitalSignatureRecord = 0x5054b50,
        EndOfCentralDirSignature = 0x6054b50
    }
}

