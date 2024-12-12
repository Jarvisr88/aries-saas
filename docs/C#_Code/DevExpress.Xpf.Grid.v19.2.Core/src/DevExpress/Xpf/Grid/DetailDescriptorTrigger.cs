namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.DXBinding;
    using System;
    using System.Windows.Markup;

    [ContentProperty("DetailDescriptor")]
    public class DetailDescriptorTrigger : DXTriggerBase
    {
        private DetailDescriptorBase detailDescriptor;

        [XtraSerializableProperty(XtraSerializationVisibility.Content), GridStoreAlwaysProperty, XtraResetProperty(ResetPropertyMode.None)]
        public DetailDescriptorBase DetailDescriptor
        {
            get => 
                this.detailDescriptor;
            set
            {
                base.WritePreamble();
                this.detailDescriptor = value;
            }
        }
    }
}

