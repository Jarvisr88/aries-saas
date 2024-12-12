namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ImageEditToolButton : Button
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyPropertyManager.Register("ImageSource", typeof(System.Windows.Media.ImageSource), typeof(ImageEditToolButton), new FrameworkPropertyMetadata(null));

        public ImageEditToolButton()
        {
            this.SetDefaultStyleKey(typeof(ImageEditToolButton));
        }

        internal ImageEditToolButton(string toolTip, string imageId, ICommand command)
        {
            ToolTipService.SetToolTip(this, toolTip);
            if (!ApplicationThemeHelper.UseDefaultSvgImages)
            {
                this.ImageSource = ImageHelper.CreateImageFromCoreEmbeddedResource($"Editors.Images.ImageEdit.{imageId}.png");
            }
            else
            {
                Stream manifestResourceStream = typeof(ImageEditToolButton).Assembly.GetManifestResourceStream($"DevExpress.Xpf.Core.Editors.Images.ImageEdit.{imageId}.svg");
                this.ImageSource = (manifestResourceStream != null) ? WpfSvgRenderer.CreateImageSource(manifestResourceStream, (double) 0.5, (WpfSvgPalette) null, null, null, true) : ImageHelper.CreateImageFromCoreEmbeddedResource($"Editors.Images.ImageEdit.{imageId}.png");
            }
            base.Command = command;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.Owner != null) && (base.CommandTarget == null))
            {
                base.CommandTarget = this.Owner;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        [Description("")]
        public System.Windows.Media.ImageSource ImageSource
        {
            get => 
                (System.Windows.Media.ImageSource) base.GetValue(ImageSourceProperty);
            set => 
                base.SetValue(ImageSourceProperty, value);
        }

        protected virtual IInputElement Owner =>
            BaseEdit.GetOwnerEdit(this) as ImageEdit;
    }
}

