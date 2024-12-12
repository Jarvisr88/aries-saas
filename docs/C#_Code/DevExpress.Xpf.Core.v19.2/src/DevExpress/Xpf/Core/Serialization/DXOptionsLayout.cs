namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils;
    using DevExpress.Utils.Controls;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public class DXOptionsLayout : OptionsLayoutBase
    {
        private DevExpress.Xpf.Core.Serialization.AcceptNestedObjects acceptNestedObjectsCore = DevExpress.Xpf.Core.Serialization.AcceptNestedObjects.Default;

        public override void Assign(BaseOptions options)
        {
            base.Assign(options);
            DXOptionsLayout layout = options as DXOptionsLayout;
            if (layout != null)
            {
                this.acceptNestedObjectsCore = layout.acceptNestedObjectsCore;
            }
        }

        [DefaultValue(0), Description(""), XtraSerializableProperty]
        public DevExpress.Xpf.Core.Serialization.AcceptNestedObjects AcceptNestedObjects
        {
            get => 
                this.acceptNestedObjectsCore;
            set
            {
                if (this.AcceptNestedObjects != value)
                {
                    DevExpress.Xpf.Core.Serialization.AcceptNestedObjects acceptNestedObjects = this.AcceptNestedObjects;
                    this.acceptNestedObjectsCore = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("AcceptNestedObjects", acceptNestedObjects, this.AcceptNestedObjects));
                }
            }
        }
    }
}

