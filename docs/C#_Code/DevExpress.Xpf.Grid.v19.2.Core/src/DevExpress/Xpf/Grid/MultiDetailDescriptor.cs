namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Markup;

    [ContentProperty("DetailDescriptors")]
    public abstract class MultiDetailDescriptor : MultiDetailDescriptorBase
    {
        protected MultiDetailDescriptor()
        {
        }

        protected internal override IEnumerable<DataControlDetailDescriptor> GetDetailDescriptors(DataTreeBuilder treeBuilder, int rowHandle) => 
            from x in base.DetailDescriptorsCore select x.GetDetailDescriptors(treeBuilder, rowHandle);

        [Description(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Master-Detail"), XtraSerializableProperty(true, false, false), GridUIProperty]
        public DetailDescriptorCollection DetailDescriptors =>
            base.DetailDescriptorsCore;
    }
}

