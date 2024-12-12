namespace DevExpress.Xpf.Utils
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public abstract class BaseShaderEffect<TShaderEffect> : ShaderEffect where TShaderEffect: BaseShaderEffect<TShaderEffect>, new()
    {
        private static readonly PixelShader _pixelShader;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty InputProperty;
        private static List<DependencyProperty> properties;

        static BaseShaderEffect()
        {
            BaseShaderEffect<TShaderEffect>.InputProperty = BaseShaderEffect<TShaderEffect>.RegisterPixelShaderSamplerProperty("Input", 0);
            if (!UriParser.IsKnownScheme("pack"))
            {
                FlowDocument document1 = new FlowDocument();
            }
            TShaderEffect local = Activator.CreateInstance<TShaderEffect>();
            string uriString = "pack://application:,,,/" + $"{local.GetAssemblyName()};component/{local.GetCompiledShaderDirectory()}/{local.GetCompiledShaderName()}".Replace("//", "/");
            PixelShader shader1 = new PixelShader();
            shader1.UriSource = new Uri(uriString, UriKind.RelativeOrAbsolute);
            BaseShaderEffect<TShaderEffect>._pixelShader = shader1;
            BaseShaderEffect<TShaderEffect>.properties = new List<DependencyProperty>();
            ShaderEffect.PixelShaderProperty.OverrideMetadata(typeof(TShaderEffect), new UIPropertyMetadata(BaseShaderEffect<TShaderEffect>._pixelShader));
            if (!typeof(TShaderEffect).IsSealed)
            {
                throw new InvalidOperationException("Type should be sealed");
            }
        }

        protected BaseShaderEffect()
        {
            base.UpdateShaderValue(BaseShaderEffect<TShaderEffect>.InputProperty);
            for (int i = 0; i < BaseShaderEffect<TShaderEffect>.properties.Count; i++)
            {
                base.UpdateShaderValue(BaseShaderEffect<TShaderEffect>.properties[i]);
            }
        }

        protected virtual string GetAssemblyName() => 
            "DevExpress.Xpf.Core.v19.2";

        protected virtual string GetCompiledShaderDirectory() => 
            "Shaders/Compiled/";

        protected abstract string GetCompiledShaderName();
        protected static DependencyProperty RegisterPixelShaderConstantProperty<T>(string name, int floatRegisterIndex, T defaultValue = null)
        {
            DependencyProperty item = DependencyProperty.Register(name, typeof(T), typeof(TShaderEffect), new UIPropertyMetadata(defaultValue, PixelShaderConstantCallback(floatRegisterIndex)));
            BaseShaderEffect<TShaderEffect>.Properties.Add(item);
            return item;
        }

        protected static DependencyProperty RegisterPixelShaderSamplerProperty(string name, int samplerRegisterIndex)
        {
            DependencyProperty item = RegisterPixelShaderSamplerProperty(name, typeof(TShaderEffect), samplerRegisterIndex);
            BaseShaderEffect<TShaderEffect>.Properties.Add(item);
            return item;
        }

        public static List<DependencyProperty> Properties
        {
            get
            {
                object synchronized = ShaderEffectHelper.Synchronized;
                lock (synchronized)
                {
                    return (BaseShaderEffect<TShaderEffect>.properties ??= new List<DependencyProperty>());
                }
            }
        }

        public Brush Input
        {
            get => 
                (Brush) base.GetValue(BaseShaderEffect<TShaderEffect>.InputProperty);
            set => 
                base.SetValue(BaseShaderEffect<TShaderEffect>.InputProperty, value);
        }
    }
}

