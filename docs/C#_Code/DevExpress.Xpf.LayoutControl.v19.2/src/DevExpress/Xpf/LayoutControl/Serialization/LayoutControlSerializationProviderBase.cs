namespace DevExpress.Xpf.LayoutControl.Serialization
{
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Windows;

    public class LayoutControlSerializationProviderBase : SerializationProvider
    {
        private static ISerializationController GetController(object obj) => 
            SerializationController.GetSerializationController(obj as DependencyObject);

        protected override void OnClearCollection(XtraItemRoutedEventArgs e)
        {
            GetController(e.Source).OnClearCollection(e);
        }

        protected override object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e) => 
            GetController(e.Source).OnCreateCollectionItem(e);

        protected override void OnCustomGetSerializableProperties(DependencyObject dObj, CustomGetSerializablePropertiesEventArgs e)
        {
            base.OnCustomGetSerializableProperties(dObj, e);
            DXSerializable serializable = new DXSerializable();
            e.SetPropertySerializable("HorizontalAlignment", serializable);
            e.SetPropertySerializable("VerticalAlignment", serializable);
            e.SetPropertySerializable("Width", serializable);
            e.SetPropertySerializable("Height", serializable);
            e.SetPropertySerializable("Name", serializable);
        }

        protected override object OnFindCollectionItem(XtraFindCollectionItemEventArgs e) => 
            GetController(e.Source).OnFindCollectionItem(e);
    }
}

