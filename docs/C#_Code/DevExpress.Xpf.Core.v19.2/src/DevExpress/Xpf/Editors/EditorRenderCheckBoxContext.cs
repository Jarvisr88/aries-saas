namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class EditorRenderCheckBoxContext : RenderCheckBoxContext
    {
        private CheckEditDisplayMode displayMode;
        private ImageSource glyph;
        private DataTemplate glyphTemplate;

        public EditorRenderCheckBoxContext(EditorRenderCheckBox factory) : base(factory)
        {
        }

        public CheckEditDisplayMode DisplayMode
        {
            get => 
                this.displayMode;
            set => 
                base.SetProperty<CheckEditDisplayMode>(ref this.displayMode, value, FREInvalidateOptions.UpdateSubTree);
        }

        public ImageSource Glyph
        {
            get => 
                this.glyph;
            set => 
                this.SetProperty<ImageSource>(ref this.glyph, value, (this.displayMode == CheckEditDisplayMode.Image) ? FREInvalidateOptions.UpdateSubTree : FREInvalidateOptions.None);
        }

        public DataTemplate GlyphTemplate
        {
            get => 
                this.glyphTemplate;
            set => 
                this.SetProperty<DataTemplate>(ref this.glyphTemplate, value, (this.displayMode == CheckEditDisplayMode.Image) ? FREInvalidateOptions.UpdateSubTree : FREInvalidateOptions.None);
        }
    }
}

