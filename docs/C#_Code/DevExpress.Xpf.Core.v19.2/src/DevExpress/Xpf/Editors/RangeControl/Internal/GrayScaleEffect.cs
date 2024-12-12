namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class GrayScaleEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(GrayScaleEffect), 0);
        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left", typeof(double), typeof(GrayScaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty TopProperty = DependencyProperty.Register("Top", typeof(double), typeof(GrayScaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty RightProperty = DependencyProperty.Register("Right", typeof(double), typeof(GrayScaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(2)));
        public static readonly DependencyProperty BottomProperty = DependencyProperty.Register("Bottom", typeof(double), typeof(GrayScaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(3)));
        public static readonly DependencyProperty RFactorProperty = DependencyProperty.Register("RFactor", typeof(double), typeof(GrayScaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(6)));
        public static readonly DependencyProperty GFactorProperty = DependencyProperty.Register("GFactor", typeof(double), typeof(GrayScaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(7)));
        public static readonly DependencyProperty BFactorProperty = DependencyProperty.Register("BFactor", typeof(double), typeof(GrayScaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(8)));
        private PixelShader shader;
        private bool isEnable = true;

        public GrayScaleEffect()
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

        public void Invalidate(double[] bounds)
        {
            if (!this.IsEnable)
            {
                double[] numArray1 = new double[4];
                numArray1[2] = 1.0;
                numArray1[3] = 1.0;
                bounds = numArray1;
            }
            base.SetValue(LeftProperty, bounds[0]);
            base.UpdateShaderValue(LeftProperty);
            base.SetValue(TopProperty, bounds[1]);
            base.UpdateShaderValue(TopProperty);
            base.SetValue(RightProperty, bounds[2]);
            base.UpdateShaderValue(RightProperty);
            base.SetValue(BottomProperty, bounds[3]);
            base.UpdateShaderValue(BottomProperty);
        }

        public Brush Input
        {
            get => 
                (Brush) base.GetValue(InputProperty);
            set => 
                base.SetValue(InputProperty, value);
        }

        public bool IsEnable
        {
            get => 
                this.isEnable;
            set => 
                this.isEnable = value;
        }

        public double Left
        {
            get => 
                (double) base.GetValue(LeftProperty);
            set => 
                base.SetValue(LeftProperty, value);
        }

        public double Top
        {
            get => 
                (double) base.GetValue(TopProperty);
            set => 
                base.SetValue(TopProperty, value);
        }

        public double Right
        {
            get => 
                (double) base.GetValue(RightProperty);
            set => 
                base.SetValue(RightProperty, value);
        }

        public double Bottom
        {
            get => 
                (double) base.GetValue(BottomProperty);
            set => 
                base.SetValue(BottomProperty, value);
        }

        public double RFactor
        {
            get => 
                (double) base.GetValue(RFactorProperty);
            set => 
                base.SetValue(RFactorProperty, value);
        }

        public double GFactor
        {
            get => 
                (double) base.GetValue(GFactorProperty);
            set => 
                base.SetValue(GFactorProperty, value);
        }

        public double BFactor
        {
            get => 
                (double) base.GetValue(BFactorProperty);
            set => 
                base.SetValue(BFactorProperty, value);
        }

        private string UriString =>
            "/DevExpress.Xpf.Core.v19.2;component/editors/rangecontrol/shader/grayscaleshader.ps";
    }
}

