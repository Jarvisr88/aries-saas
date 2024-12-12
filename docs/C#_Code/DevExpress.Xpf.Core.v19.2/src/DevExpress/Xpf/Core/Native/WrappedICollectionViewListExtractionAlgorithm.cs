namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class WrappedICollectionViewListExtractionAlgorithm : ComplexListExtractionAlgorithm
    {
        public WrappedICollectionViewListExtractionAlgorithm();
        protected override IList GetListFromICollectionView(ICollectionView collectionView);
    }
}

