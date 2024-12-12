namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows.Media;

    public class NoneBrushWrapper : BrushEditingWrapper
    {
        public NoneBrushWrapper(IBrushPicker editor) : base(editor)
        {
        }

        public override System.Windows.Media.Brush GetBrush(object baseValue) => 
            new SolidColorBrush(Text2ColorHelper.DefaultColor);

        public override void SetBrush(object editValue)
        {
        }

        public override void Subscribe()
        {
        }

        public override void Unsubscribe()
        {
        }

        public override System.Windows.Media.Brush Brush =>
            null;
    }
}

