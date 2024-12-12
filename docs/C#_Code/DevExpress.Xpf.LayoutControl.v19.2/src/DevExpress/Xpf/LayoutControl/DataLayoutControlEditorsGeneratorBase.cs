namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;

    public abstract class DataLayoutControlEditorsGeneratorBase : EditorsGeneratorFilteringBase
    {
        protected DataLayoutControlEditorsGeneratorBase()
        {
        }

        protected void ApplyProperties(IEdmPropertyInfo property, IModelItem layoutItem, IModelItem edit, EditorsGeneratorBase.Initializer initializer, bool customizeEditor)
        {
            initializer.SetEditProperties(layoutItem, edit);
            initializer.SetContainerProperties(layoutItem);
            AttributesApplier.ApplyBaseAttributesForLayoutItem(property, layoutItem, customizeEditor ? edit : null, null);
            AttributesApplier.ApplyDisplayFormatAttributesForEditor(property, () => customizeEditor ? edit : null, null);
        }

        protected override Type GetLookUpEditType() => 
            null;

        public override EditorsGeneratorBase.EditorsGeneratorTarget Target =>
            EditorsGeneratorBase.EditorsGeneratorTarget.LayoutControl;

        protected override EditorsGeneratorBase.EditorsGeneratorMode Mode =>
            EditorsGeneratorBase.EditorsGeneratorMode.Edit;
    }
}

