namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class TransparencyEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(TransparencyEffect), 1);
        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left", typeof(double), typeof(TransparencyEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(4)));
        public static readonly DependencyProperty RightProperty = DependencyProperty.Register("Right", typeof(double), typeof(TransparencyEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(5)));
        private PixelShader shader;

        public TransparencyEffect()
        {
            this.InitializeShader();
            base.UpdateShaderValue(InputProperty);
        }

        private void InitializeShader()
        {
            this.shader = new PixelShader();
            this.shader.UriSource = new Uri(this.UriString, UriKind.Relative);
            base.PixelShader = this.shader;
        }

        public void Invalidate(double left, double right)
        {
            base.SetValue(LeftProperty, left);
            base.UpdateShaderValue(LeftProperty);
            base.SetValue(RightProperty, right);
            base.UpdateShaderValue(RightProperty);
        }

        public double Left
        {
            get => 
                (double) base.GetValue(LeftProperty);
            set => 
                base.SetValue(LeftProperty, value);
        }

        public double Right
        {
            get => 
                (double) base.GetValue(RightProperty);
            set => 
                base.SetValue(RightProperty, value);
        }

        public Brush Input
        {
            get => 
                (Brush) base.GetValue(InputProperty);
            set => 
                base.SetValue(InputProperty, value);
        }

        private string UriString =>
            "/DevExpress.Xpf.Core.v19.2;component/editors/rangecontrol/shader/transparencyshader.ps";
    }
}

