namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TakePictureControl : Control
    {
        private CameraControl camera;

        static TakePictureControl()
        {
            Type forType = typeof(TakePictureControl);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
        }

        public TakePictureControl()
        {
            base.DataContext = new TakePictureViewModel(this);
        }

        public void Cancel()
        {
            this.Close();
        }

        public ImageSource Capture() => 
            this.camera.TakeSnapshot();

        private void Close()
        {
            Window window = this.FindWindow();
            if (window != null)
            {
                window.Close();
            }
        }

        private Window FindWindow() => 
            LayoutHelper.FindLayoutOrVisualParentObject<Window>(this, true, null);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.camera = LayoutHelper.FindElementByName(this, "PART_Camera") as CameraControl;
        }

        public void Save(ImageSource image)
        {
            this.SetImage(image);
            this.Close();
        }

        public void SetEditor(ImageEdit edit, PopupImageEdit popupOwner)
        {
            this.Editor = edit;
            this.PopupOwner = popupOwner;
        }

        private void SetImage(ImageSource image)
        {
            ImageEditStrategy editStrategy = this.Editor.EditStrategy as ImageEditStrategy;
            if (editStrategy != null)
            {
                editStrategy.SetImage(image);
                if (this.PopupOwner != null)
                {
                    this.PopupOwner.Source = image;
                }
            }
        }

        private ImageEdit Editor { get; set; }

        private PopupImageEdit PopupOwner { get; set; }
    }
}

