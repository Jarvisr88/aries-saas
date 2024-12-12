namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Images;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class HiddenItem : BaseCustomizationItem
    {
        static HiddenItem()
        {
            new DependencyPropertyRegistrator<HiddenItem>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartImage = base.GetTemplateChild("PART_Image") as Image;
            if (this.PartImage != null)
            {
                this.PartImage.Source = ImageHelper.GetImageForItem(base.LayoutItem);
            }
        }

        public Image PartImage { get; private set; }
    }
}

