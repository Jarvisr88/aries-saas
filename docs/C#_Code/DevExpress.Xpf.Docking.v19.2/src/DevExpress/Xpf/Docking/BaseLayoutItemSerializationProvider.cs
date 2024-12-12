namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Windows;

    public class BaseLayoutItemSerializationProvider : SerializationProvider
    {
        protected override void OnCustomGetSerializableProperties(DependencyObject dObj, CustomGetSerializablePropertiesEventArgs e)
        {
            base.OnCustomGetSerializableProperties(dObj, e);
            DXSerializable serializable = new DXSerializable();
            e.SetPropertySerializable("Name", serializable);
            e.SetPropertySerializable("MinHeight", serializable);
            e.SetPropertySerializable("MinWidth", serializable);
            e.SetPropertySerializable("MaxWidth", serializable);
            e.SetPropertySerializable("MaxHeight", serializable);
            e.SetPropertySerializable("Tag", serializable);
            e.SetPropertySerializable("Visibility", serializable);
            if (((BaseLayoutItem) dObj).PreventCaptionSerialization)
            {
                e.SetPropertySerializable("Caption", null);
            }
        }
    }
}

