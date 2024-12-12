namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class BrushEditStrategy : EditStrategyBase
    {
        public BrushEditStrategy(BrushEditBase editor) : base(editor)
        {
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
        }

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            this.BrushPicker.Do<IBrushPicker>(delegate (IBrushPicker x) {
                x.Brush = base.ValueContainer.EditValue;
                x.BrushType = this.Editor.BrushType;
            });
        }

        protected override void SyncWithEditorInternal()
        {
            this.BrushPicker.Do<IBrushPicker>(x => base.ValueContainer.SetEditValue(x.Brush, UpdateEditorSource.TextInput));
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.BrushPicker.Do<IBrushPicker>(x => x.Brush = base.ValueContainer.EditValue);
        }

        private BrushEditBase Editor =>
            base.Editor as BrushEditBase;

        private IBrushPicker BrushPicker =>
            this.Editor.BrushPicker;
    }
}

