namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;

    public class RenderToggleSwitchThumbContainerContext : RenderDecoratorContext
    {
        private double offset;
        private double thumbWidth;

        public RenderToggleSwitchThumbContainerContext(RenderToggleSwitchThumbContainer factory) : base(factory)
        {
        }

        public double Offset
        {
            get => 
                this.offset;
            set => 
                base.SetProperty<double>(ref this.offset, value, FREInvalidateOptions.UpdateLayout);
        }

        public double ThumbWidth
        {
            get => 
                this.thumbWidth;
            set => 
                this.thumbWidth = value;
        }
    }
}

