namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System.Collections.Generic;

    public class InternalZipFileParser : InternalZipFileParserCore<InternalZipFile>
    {
        protected override IList<InternalZipFile> CreateRecords() => 
            new InternalZipFileCollection();

        public InternalZipFileCollection Records =>
            (InternalZipFileCollection) base.InnerRecords;
    }
}

