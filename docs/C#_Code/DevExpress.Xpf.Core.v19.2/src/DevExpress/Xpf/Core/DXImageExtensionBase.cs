namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.DXBinding;
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    public abstract class DXImageExtensionBase : DXMarkupExtensionBase
    {
        protected DXImageExtensionBase()
        {
        }

        private BindingBase CreateBinding()
        {
            ImageSource imageSource = this.GetImageSource();
            Binding binding1 = new Binding();
            binding1.Mode = BindingMode.OneTime;
            binding1.Source = imageSource;
            binding1.TargetNullValue = imageSource;
            return binding1;
        }

        protected abstract ImageSource GetImageSource();
        private static bool IsInImageSelector(IProvideValueTarget targetProvider) => 
            (targetProvider != null) ? (targetProvider.TargetObject is ImageSelectorExtension) : false;

        private void PatchTargetNullValue(ImageSource imageSource)
        {
            if (imageSource != null)
            {
                BindingBase targetObject = base.TargetProvider.TargetObject as BindingBase;
                if (targetObject.TargetNullValue == DependencyProperty.UnsetValue)
                {
                    targetObject.TargetNullValue = imageSource;
                }
            }
        }

        protected override object ProvideValueCore() => 
            !IsInTemplate(base.TargetProvider) ? (!IsInImageSelector(base.TargetProvider) ? (!IsInBinding(base.TargetProvider) ? (!IsInSetter(base.TargetProvider) ? ((object) this.GetImageSource()) : ((object) this.CreateBinding())) : this.GetImageSource().Do<ImageSource>(new Action<ImageSource>(this.PatchTargetNullValue))) : ((object) this)) : ((object) this);
    }
}

