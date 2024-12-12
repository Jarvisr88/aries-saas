namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class ImagePresenter : Control
    {
        private const string ImageName = "PART_Image";

        static ImagePresenter()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ImagePresenter), new FrameworkPropertyMetadata(typeof(ImagePresenter)));
        }

        public ImagePresenter()
        {
            base.DataContextChanged += new DependencyPropertyChangedEventHandler(this.ListBoxEditItemImageHolderDataContextChanged);
            base.Focusable = false;
        }

        private void ApplyImageProperties()
        {
            if (this.Image != null)
            {
                EnumMemberInfo dataContext = base.DataContext as EnumMemberInfo;
                if ((dataContext == null) || !dataContext.ShowImage)
                {
                    this.Image.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.Image.Source = dataContext.Image;
                    this.Image.Visibility = (dataContext.Image != null) ? Visibility.Visible : Visibility.Collapsed;
                }
                this.Image.Margin = base.Padding;
            }
        }

        private void ListBoxEditItemImageHolderDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.ApplyImageProperties();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Image = base.GetTemplateChild("PART_Image") as System.Windows.Controls.Image;
            this.ApplyImageProperties();
        }

        public System.Windows.Controls.Image Image { get; protected set; }
    }
}

