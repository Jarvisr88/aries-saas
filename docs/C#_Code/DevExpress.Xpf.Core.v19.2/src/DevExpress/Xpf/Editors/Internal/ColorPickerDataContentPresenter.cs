namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;

    public class ColorPickerDataContentPresenter : DataContentPresenter
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ColorPickerOwner.OnApplyTemplateInternal();
        }

        private ColorPicker ColorPickerOwner =>
            LayoutHelper.FindParentObject<ColorPicker>(this);
    }
}

