namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Printing.Native;
    using DevExpress.Utils.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class LegacyWatermarkEditorViewModel : ViewModelBase
    {
        private string pageRange = string.Empty;
        private static string[] textStandardValues = WMTextConverter.StandardValues;
        private LocalizedEnumWrapper<DirectionMode> textDirectionMode;
        private static LocalizedEnumWrapper<DirectionMode>[] directionModeStandardValues;
        private static string[] fontNameStandardValues;
        private static string[] fontSizeStandardValues;
        private TransparencyRange textTransparencyRange;
        private LocalizedEnumWrapper<ImageViewMode> pictureViewMode;
        private static LocalizedEnumWrapper<ImageViewMode>[] pictureViewModeStandardValues;
        private LocalizedEnumWrapper<HorizontalAlignment> pictureHorizontalAlignment;
        private static LocalizedEnumWrapper<HorizontalAlignment>[] pictureHorizontalAlignmentStandardValues;
        private LocalizedEnumWrapper<VerticalAlignment> pictureVerticalAlignment;
        private static LocalizedEnumWrapper<VerticalAlignment>[] pictureVerticalAlignmentStandardValues;
        private TransparencyRange pictureTransparencyRange;
        private bool isPageRange;
        private readonly DelegateCommand<object> loadImageCommand;
        private readonly DelegateCommand<object> clearAllCommand;
        private DevExpress.XtraPrinting.Page page;
        private int pageCount;
        private XpfWatermark watermark;
        private string pictureFileName;
        private readonly TransparencyRange watermarkTransparencyRange;
        private const int textTransparencySteps = 20;
        private const int textTransparencyLargeSteps = 10;
        private const int pictureTransparencySteps = 20;
        private const int pictureTransparencyLargeSteps = 10;
        private const int maxFontSize = 0xdac;
        private System.Windows.Media.ImageSource previewImage;

        public LegacyWatermarkEditorViewModel()
        {
            TransparencyRange range = new TransparencyRange {
                Max = 255.0,
                Min = 0.0
            };
            this.watermarkTransparencyRange = range;
            this.UpdatePreviewCommand = DelegateCommandFactory.Create(new Action(this.UpdateWatermarkPreview));
            this.watermark = new XpfWatermark();
            this.page = new PSPage(new ReadonlyPageData(new Margins(10, 10, 10, 10), new Margins(), System.Drawing.Printing.PaperKind.Letter, (SizeF) PageSizeInfo.GetPageSize(System.Drawing.Printing.PaperKind.Letter), false));
            this.ClearAll(null);
            this.loadImageCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.LoadImage), new Func<object, bool>(this.CanLoadImage), false);
            this.clearAllCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.ClearAll), false);
        }

        private bool CanLoadImage(object parameter) => 
            true;

        private void ClearAll(object parameter)
        {
            TransparencyRange range = new TransparencyRange {
                Max = 100.0,
                Min = 0.0
            };
            this.textTransparencyRange = range;
            range = new TransparencyRange {
                Max = 100.0,
                Min = 0.0
            };
            this.pictureTransparencyRange = range;
            this.TextTransparency = 20.0;
            directionModeStandardValues = new LocalizedEnumWrapper<DirectionMode>[] { new LocalizedEnumWrapper<DirectionMode>(DirectionMode.Horizontal, new Func<DirectionMode, string>(WatermarkLocalizers.LocalizeDirectionMode)), new LocalizedEnumWrapper<DirectionMode>(DirectionMode.Vertical, new Func<DirectionMode, string>(WatermarkLocalizers.LocalizeDirectionMode)), new LocalizedEnumWrapper<DirectionMode>(DirectionMode.ForwardDiagonal, new Func<DirectionMode, string>(WatermarkLocalizers.LocalizeDirectionMode)), new LocalizedEnumWrapper<DirectionMode>(DirectionMode.BackwardDiagonal, new Func<DirectionMode, string>(WatermarkLocalizers.LocalizeDirectionMode)) };
            fontNameStandardValues = FontManager.GetFontFamilyNames().ToArray<string>();
            int[] numArray = FontSizeManager.GetPredefinedFontSizes().ToArray<int>();
            fontSizeStandardValues = new string[numArray.Length];
            for (int i = 0; i < numArray.Length; i++)
            {
                fontSizeStandardValues[i] = numArray[i].ToString();
            }
            pictureViewModeStandardValues = new LocalizedEnumWrapper<ImageViewMode>[] { new LocalizedEnumWrapper<ImageViewMode>(ImageViewMode.Clip, new Func<ImageViewMode, string>(WatermarkLocalizers.LocalizeImageViewMode)), new LocalizedEnumWrapper<ImageViewMode>(ImageViewMode.Stretch, new Func<ImageViewMode, string>(WatermarkLocalizers.LocalizeImageViewMode)), new LocalizedEnumWrapper<ImageViewMode>(ImageViewMode.Zoom, new Func<ImageViewMode, string>(WatermarkLocalizers.LocalizeImageViewMode)) };
            pictureHorizontalAlignmentStandardValues = new LocalizedEnumWrapper<HorizontalAlignment>[] { new LocalizedEnumWrapper<HorizontalAlignment>(HorizontalAlignment.Left, new Func<HorizontalAlignment, string>(WatermarkLocalizers.LocalizeHorizontalAlignment)), new LocalizedEnumWrapper<HorizontalAlignment>(HorizontalAlignment.Center, new Func<HorizontalAlignment, string>(WatermarkLocalizers.LocalizeHorizontalAlignment)), new LocalizedEnumWrapper<HorizontalAlignment>(HorizontalAlignment.Right, new Func<HorizontalAlignment, string>(WatermarkLocalizers.LocalizeHorizontalAlignment)) };
            pictureVerticalAlignmentStandardValues = new LocalizedEnumWrapper<VerticalAlignment>[] { new LocalizedEnumWrapper<VerticalAlignment>(VerticalAlignment.Top, new Func<VerticalAlignment, string>(WatermarkLocalizers.LocalizeVerticalAlignment)), new LocalizedEnumWrapper<VerticalAlignment>(VerticalAlignment.Center, new Func<VerticalAlignment, string>(WatermarkLocalizers.LocalizeVerticalAlignment)), new LocalizedEnumWrapper<VerticalAlignment>(VerticalAlignment.Bottom, new Func<VerticalAlignment, string>(WatermarkLocalizers.LocalizeVerticalAlignment)) };
            this.pictureVerticalAlignment = pictureVerticalAlignmentStandardValues[0];
            this.pictureVerticalAlignment = new LocalizedEnumWrapper<VerticalAlignment>(ContentAlignmentHelper.VerticalAlignmentFromContentAlignment(this.watermark.ImageAlign), new Func<VerticalAlignment, string>(WatermarkLocalizers.LocalizeVerticalAlignment));
            this.pictureHorizontalAlignment = new LocalizedEnumWrapper<HorizontalAlignment>(ContentAlignmentHelper.HorizontalAlignmentFromContentAlignment(this.watermark.ImageAlign), new Func<HorizontalAlignment, string>(WatermarkLocalizers.LocalizeHorizontalAlignment));
            this.textDirectionMode = new LocalizedEnumWrapper<DirectionMode>(this.watermark.TextDirection, new Func<DirectionMode, string>(WatermarkLocalizers.LocalizeDirectionMode));
            this.pictureViewMode = new LocalizedEnumWrapper<ImageViewMode>(this.watermark.ImageViewMode, new Func<ImageViewMode, string>(WatermarkLocalizers.LocalizeImageViewMode));
            this.Watermark = new XpfWatermark();
            this.PictureFileName = "";
            base.RaisePropertyChanged<DevExpress.XtraPrinting.Page>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.XtraPrinting.Page>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_Page)), new ParameterExpression[0]));
            base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_TextIsNotEmpty)), new ParameterExpression[0]));
            this.UpdateWatermarkPreview();
        }

        private bool DoActionSave(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (OutOfMemoryException exception)
            {
                this.ProcessLoadImageError(exception.Message);
            }
            catch (ExternalException exception2)
            {
                this.ProcessLoadImageError(exception2.Message);
            }
            catch
            {
                this.ProcessLoadImageError(PrintingLocalizer.GetString(PrintingStringId.WatermarkImageLoadError));
            }
            return false;
        }

        private void LoadImage(object parameter)
        {
            string filter = "Image files (*.bmp,*.jpg,*.gif,*.png,*.dib,*.tif)|*.bmp;*.jpg;*.gif;*.png;*.dib;*.tif";
            string fileName = "";
            using (Stream stream = this.DialogService.ShowOpenFileDialog("", filter, out fileName))
            {
                if (stream != null)
                {
                    byte[] buffer = new byte[stream.Length];
                    try
                    {
                        stream.Read(buffer, 0, (int) stream.Length);
                        this.Picture = buffer;
                        this.PictureFileName = fileName;
                    }
                    catch (IOException)
                    {
                        this.Picture = null;
                        this.PictureFileName = "";
                        this.DialogService.ShowError(PrintingLocalizer.GetString(PrintingStringId.WatermarkImageLoadError), PrintingLocalizer.GetString(PrintingStringId.Error));
                    }
                    finally
                    {
                        this.UpdateWatermarkPreview();
                    }
                }
            }
        }

        private void ProcessLoadImageError(string errorMessage)
        {
            this.watermark.ImageArray = null;
            this.DialogService.ShowError(errorMessage, PrintingLocalizer.GetString(PrintingStringId.Error));
        }

        private static double RecalculateTransparencyRange(double transparency, TransparencyRange currentRange, TransparencyRange newRange) => 
            Math.Round((double) ((((transparency - currentRange.Min) / currentRange.GetRange()) * newRange.GetRange()) + newRange.Min));

        private void UpdateWatermarkPreview()
        {
            IPreviewAreaProvider service = this.GetService<IPreviewAreaProvider>();
            if (service != null)
            {
                System.Windows.Size previewArea = service.PreviewArea;
                if (!previewArea.IsEmpty && !previewArea.IsZero())
                {
                    XpfWatermark copy = new XpfWatermark();
                    if (this.DoActionSave(() => copy.CopyFrom(this.watermark)))
                    {
                        this.Page.AssignWatermark(copy);
                        this.PreviewImage = PreviewImageHelper.GetPreview(this.Page, previewArea, service.GetScaleX(), null);
                    }
                }
            }
        }

        public static DevExpress.Xpf.Printing.Native.ValidationResult ValidateFontSize(float size) => 
            ((size <= 0f) || (size >= 3500f)) ? new DevExpress.Xpf.Printing.Native.ValidationResult(false, $"The Font size should be greater than {0}, and less than {0xdac}.") : new DevExpress.Xpf.Printing.Native.ValidationResult(true);

        public static bool ValidatePageRange(string pageRange) => 
            pageRange == PageRangeParser.ValidateString(pageRange);

        public ICommand UpdatePreviewCommand { get; private set; }

        public System.Windows.Media.ImageSource PreviewImage
        {
            get => 
                this.previewImage;
            set => 
                base.SetProperty<System.Windows.Media.ImageSource>(ref this.previewImage, value, System.Linq.Expressions.Expression.Lambda<Func<System.Windows.Media.ImageSource>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PreviewImage)), new ParameterExpression[0]));
        }

        public DevExpress.Xpf.Printing.IDialogService DialogService { get; set; }

        public string Text
        {
            get => 
                this.watermark.Text;
            set
            {
                if (this.watermark.Text != value)
                {
                    this.watermark.Text = value;
                    base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_Text)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_TextIsNotEmpty)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public static string[] TextStandardValues =>
            textStandardValues;

        public LocalizedEnumWrapper<DirectionMode> TextDirectionMode
        {
            get => 
                this.textDirectionMode;
            set
            {
                if (!ReferenceEquals(this.textDirectionMode, value))
                {
                    this.textDirectionMode = value;
                    this.watermark.TextDirection = this.textDirectionMode.Value;
                    base.RaisePropertyChanged<LocalizedEnumWrapper<DirectionMode>>(System.Linq.Expressions.Expression.Lambda<Func<LocalizedEnumWrapper<DirectionMode>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_TextDirectionMode)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public static LocalizedEnumWrapper<DirectionMode>[] TextDirectionModeStandardValues =>
            directionModeStandardValues;

        public System.Windows.Media.Color TextColor
        {
            get => 
                System.Windows.Media.Color.FromArgb(this.watermark.ForeColor.A, this.watermark.ForeColor.R, this.watermark.ForeColor.G, this.watermark.ForeColor.B);
            set
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
                if (this.watermark.ForeColor != color)
                {
                    this.watermark.ForeColor = color;
                    base.RaisePropertyChanged<System.Windows.Media.Color>(System.Linq.Expressions.Expression.Lambda<Func<System.Windows.Media.Color>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_TextColor)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public string FontName
        {
            get => 
                this.watermark.Font.Name;
            set
            {
                if (this.watermark.Font.Name != value)
                {
                    this.watermark.Font = new Font(value, this.watermark.Font.Size, this.watermark.Font.Style);
                    base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_FontName)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public float FontSize
        {
            get => 
                this.watermark.Font.Size;
            set
            {
                if (this.watermark.Font.Size != value)
                {
                    DevExpress.Xpf.Printing.Native.ValidationResult result = ValidateFontSize(value);
                    if (!result.IsValid)
                    {
                        throw new ArgumentOutOfRangeException("value", result.ErrorMessage);
                    }
                    this.watermark.Font = new Font(this.watermark.Font.Name, value, this.watermark.Font.Style);
                    base.RaisePropertyChanged<float>(System.Linq.Expressions.Expression.Lambda<Func<float>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_FontSize)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public static string[] FontSizeStandardValues =>
            fontSizeStandardValues;

        public static string[] FontNameStandardValues =>
            fontNameStandardValues;

        public bool IsTextItalic
        {
            get => 
                this.watermark.Font.Italic;
            set
            {
                if (this.watermark.Font.Italic != value)
                {
                    System.Drawing.FontStyle regular = System.Drawing.FontStyle.Regular;
                    if (value)
                    {
                        regular = System.Drawing.FontStyle.Italic;
                    }
                    if (this.IsTextBold)
                    {
                        regular |= System.Drawing.FontStyle.Bold;
                    }
                    this.watermark.Font = new Font(this.watermark.Font.Name, this.watermark.Font.Size, regular);
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsTextItalic)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public bool IsTextBold
        {
            get => 
                this.watermark.Font.Bold;
            set
            {
                if (this.watermark.Font.Bold != value)
                {
                    System.Drawing.FontStyle regular = System.Drawing.FontStyle.Regular;
                    if (value)
                    {
                        regular = System.Drawing.FontStyle.Bold;
                    }
                    if (this.IsTextItalic)
                    {
                        regular |= System.Drawing.FontStyle.Italic;
                    }
                    this.watermark.Font = new Font(this.watermark.Font.Name, this.watermark.Font.Size, regular);
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsTextBold)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public int MinTextTransparency
        {
            get => 
                (int) this.textTransparencyRange.Min;
            set
            {
                if ((((int) this.textTransparencyRange.Min) != value) && (value < this.MaxTextTransparency))
                {
                    this.textTransparencyRange.Min = value;
                    base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_MinTextTransparency)), new ParameterExpression[0]));
                }
            }
        }

        public int MaxTextTransparency
        {
            get => 
                (int) this.textTransparencyRange.Max;
            set
            {
                if ((((int) this.textTransparencyRange.Max) != value) && (value > this.MinTextTransparency))
                {
                    this.textTransparencyRange.Max = value;
                    base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_MaxTextTransparency)), new ParameterExpression[0]));
                }
            }
        }

        public double TextTransparency
        {
            get => 
                RecalculateTransparencyRange((double) this.watermark.TextTransparency, this.watermarkTransparencyRange, this.textTransparencyRange);
            set
            {
                if ((this.TextTransparency != value) && ((value >= this.MinTextTransparency) && (value <= this.MaxTextTransparency)))
                {
                    this.watermark.TextTransparency = (int) RecalculateTransparencyRange(value, this.textTransparencyRange, this.watermarkTransparencyRange);
                    base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_TextTransparency)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public LocalizedEnumWrapper<ImageViewMode> PictureViewMode
        {
            get => 
                this.pictureViewMode;
            set
            {
                if (!ReferenceEquals(this.pictureViewMode, value))
                {
                    this.pictureViewMode = value;
                    this.watermark.ImageViewMode = this.pictureViewMode.Value;
                    base.RaisePropertyChanged<LocalizedEnumWrapper<ImageViewMode>>(System.Linq.Expressions.Expression.Lambda<Func<LocalizedEnumWrapper<ImageViewMode>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PictureViewMode)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public static LocalizedEnumWrapper<ImageViewMode>[] PictureViewModeStandardValues =>
            pictureViewModeStandardValues;

        public bool IsPictureTiled
        {
            get => 
                this.watermark.ImageTiling;
            set
            {
                if (this.watermark.ImageTiling != value)
                {
                    this.watermark.ImageTiling = value;
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsPictureTiled)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public LocalizedEnumWrapper<HorizontalAlignment> PictureHorizontalAlignment
        {
            get => 
                this.pictureHorizontalAlignment;
            set
            {
                if (!ReferenceEquals(this.pictureHorizontalAlignment, value))
                {
                    this.pictureHorizontalAlignment = value;
                    if ((this.pictureHorizontalAlignment != null) && (this.pictureVerticalAlignment != null))
                    {
                        this.watermark.ImageAlign = ContentAlignmentHelper.ContentAlignmentFromAlignments(this.pictureHorizontalAlignment.Value, this.pictureVerticalAlignment.Value);
                    }
                    base.RaisePropertyChanged<LocalizedEnumWrapper<HorizontalAlignment>>(System.Linq.Expressions.Expression.Lambda<Func<LocalizedEnumWrapper<HorizontalAlignment>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PictureHorizontalAlignment)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public static LocalizedEnumWrapper<HorizontalAlignment>[] PictureHorizontalAlignmentStandardValues =>
            pictureHorizontalAlignmentStandardValues;

        public LocalizedEnumWrapper<VerticalAlignment> PictureVerticalAlignment
        {
            get => 
                this.pictureVerticalAlignment;
            set
            {
                if (!ReferenceEquals(this.pictureVerticalAlignment, value))
                {
                    this.pictureVerticalAlignment = value;
                    if ((this.pictureHorizontalAlignment != null) && (this.pictureVerticalAlignment != null))
                    {
                        this.watermark.ImageAlign = ContentAlignmentHelper.ContentAlignmentFromAlignments(this.pictureHorizontalAlignment.Value, this.pictureVerticalAlignment.Value);
                    }
                    base.RaisePropertyChanged<LocalizedEnumWrapper<VerticalAlignment>>(System.Linq.Expressions.Expression.Lambda<Func<LocalizedEnumWrapper<VerticalAlignment>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PictureVerticalAlignment)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public static LocalizedEnumWrapper<VerticalAlignment>[] PictureVerticalAlignmentStandardValues =>
            pictureVerticalAlignmentStandardValues;

        public double PictureTransparency
        {
            get => 
                RecalculateTransparencyRange((double) this.watermark.ImageTransparency, this.watermarkTransparencyRange, this.pictureTransparencyRange);
            set
            {
                if ((this.PictureTransparency != value) && ((value >= this.MinPictureTransparency) && (value <= this.MaxPictureTransparency)))
                {
                    this.watermark.ImageTransparency = (int) RecalculateTransparencyRange(value, this.pictureTransparencyRange, this.watermarkTransparencyRange);
                    base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PictureTransparency)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public int MinPictureTransparency
        {
            get => 
                (int) this.pictureTransparencyRange.Min;
            set
            {
                if ((((int) this.pictureTransparencyRange.Min) != value) && (value < this.MaxPictureTransparency))
                {
                    this.pictureTransparencyRange.Min = value;
                    base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_MinPictureTransparency)), new ParameterExpression[0]));
                }
            }
        }

        public int MaxPictureTransparency
        {
            get => 
                (int) this.pictureTransparencyRange.Max;
            set
            {
                if ((((int) this.pictureTransparencyRange.Max) != value) && (value > this.MinPictureTransparency))
                {
                    this.pictureTransparencyRange.Max = value;
                    base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_MaxPictureTransparency)), new ParameterExpression[0]));
                }
            }
        }

        public bool ShowBehind
        {
            get => 
                this.watermark.ShowBehind;
            set
            {
                this.watermark.ShowBehind = value;
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_ShowBehind)), new ParameterExpression[0]));
                this.UpdateWatermarkPreview();
            }
        }

        public string PageRange
        {
            get
            {
                string str;
                if (!string.IsNullOrEmpty(this.pageRange))
                {
                    return this.pageRange;
                }
                this.pageRange = str = this.watermark.PageRange;
                return str;
            }
            set
            {
                this.pageRange = value;
                this.watermark.PageRange = PageRangeParser.ValidateString(value);
                this.IsPageRange = !string.IsNullOrEmpty(this.watermark.PageRange);
                base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PageRange)), new ParameterExpression[0]));
            }
        }

        public bool IsPageRange
        {
            get => 
                this.isPageRange;
            set
            {
                if (this.isPageRange != value)
                {
                    this.isPageRange = value;
                    if (!value)
                    {
                        this.PageRange = "";
                    }
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsPageRange)), new ParameterExpression[0]));
                }
            }
        }

        public byte[] Picture
        {
            get => 
                this.watermark.ImageArray;
            set
            {
                if (this.watermark.ImageArray != value)
                {
                    this.DoActionSave(() => this.watermark.ImageArray = value);
                    base.RaisePropertyChanged<byte[]>(System.Linq.Expressions.Expression.Lambda<Func<byte[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_Picture)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<System.Drawing.Bitmap>(System.Linq.Expressions.Expression.Lambda<Func<System.Drawing.Bitmap>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_Bitmap)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsPictureLoaded)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public System.Drawing.Bitmap Bitmap =>
            (System.Drawing.Bitmap) this.watermark.Image;

        public bool IsPictureLoaded =>
            this.watermark.Image != null;

        public ICommand LoadImageCommand =>
            this.loadImageCommand;

        public DelegateCommand<object> ClearAllCommand =>
            this.clearAllCommand;

        public DevExpress.XtraPrinting.Page Page
        {
            get => 
                this.page;
            set
            {
                if (!ReferenceEquals(this.page, value))
                {
                    this.page = value;
                    base.RaisePropertyChanged<DevExpress.XtraPrinting.Page>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.XtraPrinting.Page>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_Page)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public int PageCount
        {
            get => 
                this.pageCount;
            set
            {
                if (this.pageCount != value)
                {
                    this.pageCount = value;
                    base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PageCount)), new ParameterExpression[0]));
                }
            }
        }

        public int[] PageIndexes =>
            !this.IsPageRange ? PageRangeParser.GetIndices("", this.PageCount) : PageRangeParser.GetIndices(this.watermark.PageRange, this.PageCount);

        public XpfWatermark Watermark
        {
            get => 
                this.watermark;
            set
            {
                if (!ReferenceEquals(this.watermark, value))
                {
                    this.watermark = value;
                    this.PictureViewMode = new LocalizedEnumWrapper<ImageViewMode>(this.watermark.ImageViewMode, new Func<ImageViewMode, string>(WatermarkLocalizers.LocalizeImageViewMode));
                    ContentAlignment imageAlign = this.watermark.ImageAlign;
                    this.PictureHorizontalAlignment = new LocalizedEnumWrapper<HorizontalAlignment>(ContentAlignmentHelper.HorizontalAlignmentFromContentAlignment(imageAlign), new Func<HorizontalAlignment, string>(WatermarkLocalizers.LocalizeHorizontalAlignment));
                    this.PictureVerticalAlignment = new LocalizedEnumWrapper<VerticalAlignment>(ContentAlignmentHelper.VerticalAlignmentFromContentAlignment(imageAlign), new Func<VerticalAlignment, string>(WatermarkLocalizers.LocalizeVerticalAlignment));
                    this.TextDirectionMode = new LocalizedEnumWrapper<DirectionMode>(this.watermark.TextDirection, new Func<DirectionMode, string>(WatermarkLocalizers.LocalizeDirectionMode));
                    this.PageRange = this.watermark.PageRange;
                    this.ShowBehind = this.watermark.ShowBehind;
                    base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_Text)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsTextBold)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsTextItalic)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<System.Windows.Media.Color>(System.Linq.Expressions.Expression.Lambda<Func<System.Windows.Media.Color>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_TextColor)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_FontName)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<float>(System.Linq.Expressions.Expression.Lambda<Func<float>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_FontSize)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_TextTransparency)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsPictureTiled)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PictureTransparency)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_IsPictureLoaded)), new ParameterExpression[0]));
                    this.UpdateWatermarkPreview();
                }
            }
        }

        public string PictureFileName
        {
            get => 
                this.pictureFileName;
            set
            {
                if (this.pictureFileName != value)
                {
                    this.pictureFileName = value;
                    base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(LegacyWatermarkEditorViewModel)), (MethodInfo) methodof(LegacyWatermarkEditorViewModel.get_PictureFileName)), new ParameterExpression[0]));
                }
            }
        }

        public int TextTransparencyStep =>
            Math.Max(((int) this.textTransparencyRange.GetRange()) / 20, 1);

        public int TextTransparencyLargeStep =>
            Math.Max(((int) this.textTransparencyRange.GetRange()) / 10, 1);

        public int PictureTransparencyStep =>
            Math.Max(((int) this.pictureTransparencyRange.GetRange()) / 20, 1);

        public int PictureTransparencyLargeStep =>
            Math.Max(((int) this.pictureTransparencyRange.GetRange()) / 10, 1);

        public bool TextIsNotEmpty =>
            !string.IsNullOrEmpty(this.Text);

        [StructLayout(LayoutKind.Sequential)]
        private struct TransparencyRange
        {
            public double Min;
            public double Max;
            public double GetRange() => 
                this.Max - this.Min;
        }
    }
}

