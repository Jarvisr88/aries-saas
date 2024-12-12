namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native.Dialogs;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    [POCOViewModel]
    public class WatermarkEditorViewModel : ViewModelBase, IWatermarkViewModel
    {
        private readonly Locker updatePreviewLock = new Locker();
        private readonly Page page;
        protected DevExpress.XtraPrinting.Drawing.Watermark watermark;

        public WatermarkEditorViewModel(DevExpress.XtraPrinting.Drawing.Watermark watermark, Page page)
        {
            this.Watermark = watermark;
            this.page = page;
            this.UpdatePreviewCommand = DelegateCommandFactory.Create(new Action(this.UpdatePreview));
            this.OpenImageCommand = DelegateCommandFactory.Create(new Action(this.LoadImage));
            this.ClearAllCommand = DelegateCommandFactory.Create(new Action(this.ResetProperties));
        }

        private static System.Drawing.FontStyle ConvertToFontStyle(bool fontStyleFlagState, System.Drawing.FontStyle fontStyleFlag, System.Drawing.FontStyle baseFontStyle)
        {
            System.Drawing.FontStyle style = baseFontStyle;
            return (!fontStyleFlagState ? (style & ~fontStyleFlag) : (style | fontStyleFlag));
        }

        protected virtual DevExpress.XtraPrinting.Drawing.Watermark CreateWatermark() => 
            new DevExpress.XtraPrinting.Drawing.Watermark();

        private void LoadImage()
        {
            IOpenFileDialogService service = this.GetService<IOpenFileDialogService>();
            service.AssignImageFileFilter();
            if (service.ShowDialog(string.Empty))
            {
                string fullPath = Path.Combine(service.File.DirectoryName, service.File.Name);
                try
                {
                    this.updatePreviewLock.DoLockedAction(delegate {
                        this.ImageFileName = fullPath;
                        this.LoadImageFromFile(fullPath);
                    });
                }
                catch (OutOfMemoryException)
                {
                    this.ResetImage();
                    this.ShowErrorMessageBox(PrintingLocalizer.GetString(PrintingStringId.WatermarkImageLoadError));
                }
                catch (IOException)
                {
                    this.ResetImage();
                    this.ShowErrorMessageBox(PrintingLocalizer.GetString(PrintingStringId.WatermarkImageLoadError));
                }
                finally
                {
                    this.UpdatePreview();
                }
            }
        }

        protected virtual void LoadImageFromFile(string fullPath)
        {
            this.ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(fullPath);
        }

        protected void OnIsPageRangeChanged()
        {
            this.UpdatePreview();
            if (!this.IsPageRange)
            {
                this.PageRangePlaceholder = this.PageRange;
                this.PageRange = null;
            }
            else if (this.PageRangePlaceholder != null)
            {
                this.PageRange = this.PageRangePlaceholder;
            }
        }

        private void ResetImage()
        {
            this.ImageFileName = null;
            this.ImageSource = null;
        }

        private void ResetProperties()
        {
            this.updatePreviewLock.DoLockedAction(delegate {
                this.ResetImage();
                this.ImageHorizontalAlignment = HorizontalAlignment.Center;
                this.ImageVerticalAlignment = VerticalAlignment.Center;
                this.Text = null;
                this.TextFontFamily = "Verdana";
                this.TextFontSize = 12;
                this.TextIsBold = false;
                this.TextIsItalic = false;
                this.TextDirection = DirectionMode.ForwardDiagonal;
                this.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Clip;
                this.ImageIsTiled = false;
                this.TextTransparency = 20.0;
                this.ImageTransparency = 0.0;
                this.ShowBehind = true;
                this.PageRange = string.Empty;
                this.IsPageRange = false;
                this.TextForeground = Colors.Red;
                this.ImageFileName = string.Empty;
            });
            this.UpdatePreview();
        }

        private void SetModelProperties()
        {
            this.updatePreviewLock.DoLockedAction(delegate {
                this.Text = this.watermark.Text;
                this.TextFont = this.watermark.Font;
                this.TextDirection = this.watermark.TextDirection;
                this.ImageViewMode = this.watermark.ImageViewMode;
                this.ImageIsTiled = this.watermark.ImageTiling;
                this.ImageAlignment = this.watermark.ImageAlign;
                this.TextTransparency = this.watermark.TextTransparency;
                this.ImageTransparency = this.watermark.ImageTransparency;
                this.ShowBehind = this.watermark.ShowBehind;
                this.PageRange = this.watermark.PageRange;
                this.IsPageRange = !string.IsNullOrEmpty(this.PageRange);
                this.TextColor = this.watermark.ForeColor;
                this.ImageSource = this.watermark.ImageSource;
            });
        }

        private void ShowErrorMessageBox(string errorText)
        {
            this.GetService<IMessageBoxService>().ShowMessage(errorText, PrintingLocalizer.GetString(PrintingStringId.Error), MessageButton.OK, MessageIcon.Error);
        }

        protected void UpdatePreview()
        {
            this.updatePreviewLock.DoIfNotLocked(delegate {
                IPreviewAreaProvider service = this.GetService<IPreviewAreaProvider>();
                if (service != null)
                {
                    System.Windows.Size previewArea = service.PreviewArea;
                    if (!previewArea.IsEmpty && !previewArea.IsZero())
                    {
                        this.watermark = this.CreateWatermark();
                        this.watermark.Text = this.Text;
                        this.watermark.ImageSource = this.ImageSource;
                        this.watermark.ImageAlign = this.ImageAlignment;
                        this.watermark.ImageViewMode = this.ImageViewMode;
                        this.watermark.ImageTiling = this.ImageIsTiled;
                        this.watermark.ImageTransparency = (int) this.ImageTransparency;
                        this.watermark.TextTransparency = (int) this.TextTransparency;
                        this.watermark.TextDirection = this.TextDirection;
                        this.watermark.ForeColor = this.TextColor;
                        this.watermark.ShowBehind = this.ShowBehind;
                        this.watermark.PageRange = this.IsPageRange ? this.PageRange : string.Empty;
                        this.watermark.Font = this.TextFont;
                        this.page.AssignWatermark(this.watermark);
                        this.PreviewImage = PreviewImageHelper.GetPreview(this.page, previewArea, service.GetScaleX(), null);
                    }
                }
            });
        }

        public ICommand UpdatePreviewCommand { get; private set; }

        public ICommand OpenImageCommand { get; private set; }

        public ICommand ClearAllCommand { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual string Text { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual DirectionMode TextDirection { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual string TextFontFamily { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual int TextFontSize { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual System.Windows.Media.Color TextForeground { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual bool TextIsItalic { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual bool TextIsBold { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual DevExpress.XtraPrinting.Drawing.ImageViewMode ImageViewMode { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual bool ImageIsTiled { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual HorizontalAlignment ImageHorizontalAlignment { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual VerticalAlignment ImageVerticalAlignment { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual double ImageTransparency { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual double TextTransparency { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual bool ShowBehind { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="OnIsPageRangeChanged")]
        public virtual bool IsPageRange { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdatePreview")]
        public virtual string PageRange { get; set; }

        public virtual System.Windows.Media.ImageSource PreviewImage { get; set; }

        public virtual string PageRangePlaceholder { get; set; }

        public virtual string ImageFileName { get; set; }

        public virtual DevExpress.XtraPrinting.Drawing.ImageSource ImageSource { get; set; }

        [CLSCompliant(false)]
        public DevExpress.XtraPrinting.Drawing.Watermark Watermark
        {
            get => 
                this.watermark;
            set
            {
                if (!ReferenceEquals(this.watermark, value))
                {
                    this.watermark = value;
                    this.SetModelProperties();
                    this.UpdatePreview();
                }
            }
        }

        public ContentAlignment ImageAlignment
        {
            get => 
                ContentAlignmentHelper.ContentAlignmentFromAlignments(this.ImageHorizontalAlignment, this.ImageVerticalAlignment);
            set
            {
                this.ImageHorizontalAlignment = ContentAlignmentHelper.HorizontalAlignmentFromContentAlignment(value);
                this.ImageVerticalAlignment = ContentAlignmentHelper.VerticalAlignmentFromContentAlignment(value);
            }
        }

        public System.Drawing.Color TextColor
        {
            get => 
                System.Drawing.Color.FromArgb(this.TextForeground.A, this.TextForeground.R, this.TextForeground.G, this.TextForeground.B);
            set => 
                this.TextForeground = System.Windows.Media.Color.FromArgb(value.A, value.R, value.G, value.B);
        }

        public Font TextFont
        {
            get
            {
                System.Drawing.FontStyle regular = System.Drawing.FontStyle.Regular;
                regular = ConvertToFontStyle(this.TextIsBold, System.Drawing.FontStyle.Bold, regular);
                return new Font(this.TextFontFamily, (float) this.TextFontSize, ConvertToFontStyle(this.TextIsItalic, System.Drawing.FontStyle.Italic, regular), GraphicsUnit.Point);
            }
            set
            {
                // Unresolved stack state at '000000E0'
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WatermarkEditorViewModel.<>c <>9 = new WatermarkEditorViewModel.<>c();
            public static Func<Font, string> <>9__111_0;
            public static Func<Font, int> <>9__111_1;
            public static Func<int> <>9__111_2;
            public static Func<Font, bool> <>9__111_3;
            public static Func<bool> <>9__111_4;
            public static Func<Font, bool> <>9__111_5;
            public static Func<bool> <>9__111_6;

            internal string <set_TextFont>b__111_0(Font x) => 
                x.Name;

            internal int <set_TextFont>b__111_1(Font x) => 
                (int) x.Size;

            internal int <set_TextFont>b__111_2() => 
                12;

            internal bool <set_TextFont>b__111_3(Font x) => 
                x.Style.IsBold();

            internal bool <set_TextFont>b__111_4() => 
                false;

            internal bool <set_TextFont>b__111_5(Font x) => 
                x.Style.IsItalic();

            internal bool <set_TextFont>b__111_6() => 
                false;
        }
    }
}

