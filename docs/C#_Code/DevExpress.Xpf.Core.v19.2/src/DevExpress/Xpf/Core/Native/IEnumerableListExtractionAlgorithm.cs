namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;

    public class IEnumerableListExtractionAlgorithm : ListExtractionAlgorithmBase
    {
        private readonly ItemPropertyNotificationMode itemPropertyNotificationMode;

        public IEnumerableListExtractionAlgorithm(ItemPropertyNotificationMode itemPropertyNotificationMode);
        public override IList Extract(object dataSource);
    }
}

