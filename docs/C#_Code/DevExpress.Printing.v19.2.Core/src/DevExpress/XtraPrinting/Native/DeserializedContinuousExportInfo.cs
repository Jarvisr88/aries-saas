namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using System;

    public class DeserializedContinuousExportInfo : ContinuousExportInfo, IXtraSupportDeserializeCollectionItem
    {
        public DeserializedContinuousExportInfo();
        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e);
        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e);
        protected override void PreprocessBrick(Brick brick, IPrintingSystemContext context);
    }
}

