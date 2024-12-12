namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Themes;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ImageInplaceEditor : DocumentInplaceEditorBase
    {
        private readonly EditingProvider editingProvider;

        public ImageInplaceEditor(DocumentInplaceEditorOwner owner, ImageInplaceEditorColumn column) : base(owner, column, column.EditingField)
        {
            this.editingProvider = new EditingProvider();
            this.UpdateBorder();
        }

        public override bool CommitEditor(bool closeEditor = false) => 
            this.IsEditorVisible ? ((this.ValidationError == null) ? base.CommitEditor(closeEditor) : false) : true;

        protected override IBaseEdit CreateEditor(BaseEditSettings settings)
        {
            IBaseEdit edit = base.CreateEditor(settings);
            edit.SetInplaceEditingProvider(this.editingProvider);
            return edit;
        }

        protected override DocumentInplaceEditorBase.InplaceEditorInitialData CreateInitialData()
        {
            ImageEditorData data1 = new ImageEditorData(this.EditingField);
            data1.ZoomFactor = base.DocumentPresenter.ActualDocumentViewer.ZoomFactor;
            ImageEditorData target = data1;
            Binding binding = new Binding("BorderBrush");
            binding.Source = this.EditorColumn;
            BindingOperations.SetBinding(target, ImageEditorData.BorderBrushProperty, binding);
            Binding binding2 = new Binding("Background");
            binding2.Source = this.EditorColumn;
            BindingOperations.SetBinding(target, ImageEditorData.BackgroundProperty, binding2);
            Binding binding3 = new Binding("BorderThickness");
            binding3.Source = this.EditorColumn;
            BindingOperations.SetBinding(target, ImageEditorData.BorderThicknessProperty, binding3);
            Binding binding4 = new Binding("EditorPadding");
            binding4.Source = this.EditorColumn;
            BindingOperations.SetBinding(target, ImageEditorData.PaddingProperty, binding4);
            return target;
        }

        protected override void InitializeBaseEdit(IBaseEdit newEdit, InplaceEditorBase.BaseEditSourceType newBaseEditSourceType)
        {
            base.InitializeBaseEdit(newEdit, newBaseEditSourceType);
            Action<BaseEdit> action = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Action<BaseEdit> local1 = <>c.<>9__24_0;
                action = <>c.<>9__24_0 = delegate (BaseEdit baseEdit) {
                };
            }
            (newEdit as BaseEdit).Do<BaseEdit>(action);
            (newEdit as ImageEdit).Do<ImageEdit>(delegate (ImageEdit x) {
                switch (((ImageBrick) this.EditingField.Brick).SizeMode)
                {
                    case ImageSizeMode.Normal:
                    case ImageSizeMode.AutoSize:
                        x.Stretch = Stretch.None;
                        x.VerticalContentAlignment = VerticalAlignment.Top;
                        x.HorizontalContentAlignment = HorizontalAlignment.Left;
                        return;

                    case ImageSizeMode.StretchImage:
                        x.Stretch = Stretch.Fill;
                        return;

                    case ImageSizeMode.CenterImage:
                    case ImageSizeMode.Tile:
                        break;

                    case ImageSizeMode.ZoomImage:
                        x.Stretch = Stretch.Uniform;
                        return;

                    case ImageSizeMode.Squeeze:
                        x.Stretch = Stretch.UniformToFill;
                        break;

                    default:
                        return;
                }
            });
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            base.editCore.Focus();
        }

        private void OnZoomFactorChanged(object sender, ZoomChangedEventArgs e)
        {
            this.InitialData.ZoomFactor = e.ZoomFactor;
            this.EditorColumn.RaiseAppearancePropertiesChanged();
        }

        protected override bool PostEditorCore()
        {
            if (!base.HasAccessToCellValue)
            {
                return true;
            }
            if (!this.InitialData.IsValueChanged)
            {
                return true;
            }
            if ((this.ValidationError == null) && string.IsNullOrEmpty(string.Empty))
            {
                try
                {
                    this.EditingField.ImageSource = this.InitialData.Value as DevExpress.XtraPrinting.Drawing.ImageSource;
                    this.EditingField.ImageSizeMode = this.InitialData.ImageSizeMode;
                    this.EditingField.ImageAlignment = this.InitialData.ImageAlignment;
                    base.DocumentPresenter.BehaviorProvider.ZoomChanged -= new EventHandler<ZoomChangedEventArgs>(this.OnZoomFactorChanged);
                    return true;
                }
                catch (Exception exception)
                {
                    this.ValidationError = new BaseValidationError(exception.Message, exception);
                    BaseEditHelper.SetValidationError((DependencyObject) base.editCore, this.ValidationError);
                }
            }
            return false;
        }

        protected override DataTemplate SelectTemplate()
        {
            if (!(this.EditingField.Brick is CharacterCombBrick))
            {
                return this.EditorColumn.EditorTemplateSelector.With<DataTemplateSelector, DataTemplate>(x => x.SelectTemplate(this.EditingField, null));
            }
            EditingFieldThemeKeyExtension resourceKey = new EditingFieldThemeKeyExtension();
            resourceKey.ResourceKey = "DevExpress_Xpf_Printing_PredefinedCharacterCombResourceKey";
            return (base.FindResource(resourceKey) as DataTemplate);
        }

        protected override void SetEdit(IBaseEdit value)
        {
            base.DocumentPresenter.BehaviorProvider.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(this.OnZoomFactorChanged);
            base.SetEdit(value);
            if (value != null)
            {
                value.EditMode = EditMode.InplaceActive;
            }
        }

        private void UpdateBorder()
        {
            base.Border.Margin = new Thickness(-1.0);
            base.Border.BorderThickness = new Thickness(1.0);
            base.Border.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x33, 0, 0, 0));
        }

        public override void ValidateEditorCore()
        {
            base.ValidateEditorCore();
            if ((base.editCore != null) && ((base.Edit != null) && !base.Edit.DoValidate()))
            {
                BaseValidationError validationError = BaseEditHelper.GetValidationError((DependencyObject) base.editCore);
                this.ValidationError = validationError;
            }
        }

        protected ImageEditingField EditingField =>
            (ImageEditingField) base.EditingField;

        private BaseValidationError ValidationError { get; set; }

        protected ImageInplaceEditorColumn EditorColumn =>
            (ImageInplaceEditorColumn) base.EditorColumn;

        protected ImageEditorData InitialData =>
            (ImageEditorData) base.InitialData;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ImageInplaceEditor.<>c <>9 = new ImageInplaceEditor.<>c();
            public static Action<BaseEdit> <>9__24_0;

            internal void <InitializeBaseEdit>b__24_0(BaseEdit baseEdit)
            {
            }
        }

        public class EditingProvider : IInplaceEditingProvider
        {
            public bool HandleScrollNavigation(Key key, ModifierKeys keys) => 
                true;

            public bool HandleTextNavigation(Key key, ModifierKeys keys) => 
                true;
        }

        protected class ImageEditorData : DocumentInplaceEditorBase.InplaceEditorInitialData, IPictureRenderer, INativeImageRenderer
        {
            private readonly List<Curve> curves;
            private readonly PaintedImageCreator imageCreator;
            private double zoomFactor;
            public static readonly DependencyProperty ImageAlignmentProperty;
            public static readonly DependencyProperty ImageSizeModeProperty;
            public static readonly DependencyProperty BrushSizeProperty;
            public static readonly DependencyProperty BrushColorProperty;
            public static readonly DependencyProperty DrawingImageProperty;
            public static readonly DependencyPropertyKey DrawingImagePropertyKey;
            public static readonly DependencyProperty BorderBrushProperty;
            public static readonly DependencyProperty BackgroundProperty;
            public static readonly DependencyProperty BorderThicknessProperty;
            public static readonly DependencyProperty PaddingProperty;
            private readonly DevExpress.XtraPrinting.ImageAlignment initialAlignment;
            private readonly DevExpress.XtraPrinting.ImageSizeMode initialSizeMode;
            private readonly ImageEditingField editingField;
            private INativeImageRendererCallback callback;
            private NativeImage editor;
            private Curve currentCurve;

            static ImageEditorData()
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                FrameworkPropertyMetadataOptions? frameworkOptions = null;
                DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData> registrator1 = DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData>.New().Register<DevExpress.XtraPrinting.ImageSizeMode>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, DevExpress.XtraPrinting.ImageSizeMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_ImageSizeMode)), parameters), out ImageSizeModeProperty, DevExpress.XtraPrinting.ImageSizeMode.Normal, delegate (ImageInplaceEditor.ImageEditorData d) {
                    d.imageCreator.Do<PaintedImageCreator>(x => x.SizeMode = d.ImageSizeMode);
                    Action<NativeImage> action = <>c.<>9__100_2;
                    if (<>c.<>9__100_2 == null)
                    {
                        Action<NativeImage> local1 = <>c.<>9__100_2;
                        action = <>c.<>9__100_2 = x => x.Invalidate();
                    }
                    d.editor.Do<NativeImage>(action);
                    CommandManager.InvalidateRequerySuggested();
                }, frameworkOptions);
                expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
                frameworkOptions = null;
                DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData> registrator2 = registrator1.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_BrushSize)), expressionArray2), out BrushSizeProperty, 1.0, frameworkOptions);
                expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
                frameworkOptions = null;
                DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData> registrator3 = registrator2.Register<System.Windows.Media.Color>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, System.Windows.Media.Color>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_BrushColor)), expressionArray3), out BrushColorProperty, Colors.Black, frameworkOptions);
                expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
                frameworkOptions = null;
                DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData> registrator4 = registrator3.Register<DevExpress.XtraPrinting.ImageAlignment>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, DevExpress.XtraPrinting.ImageAlignment>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_ImageAlignment)), expressionArray4), out ImageAlignmentProperty, DevExpress.XtraPrinting.ImageAlignment.TopLeft, delegate (ImageInplaceEditor.ImageEditorData d) {
                    d.imageCreator.Do<PaintedImageCreator>(x => x.Alignment = d.ImageAlignment);
                    Action<NativeImage> action = <>c.<>9__100_5;
                    if (<>c.<>9__100_5 == null)
                    {
                        Action<NativeImage> local1 = <>c.<>9__100_5;
                        action = <>c.<>9__100_5 = x => x.Invalidate();
                    }
                    d.editor.Do<NativeImage>(action);
                    CommandManager.InvalidateRequerySuggested();
                }, frameworkOptions);
                expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
                frameworkOptions = null;
                DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData> registrator5 = registrator4.Register<System.Windows.Media.Brush>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, System.Windows.Media.Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_BorderBrush)), expressionArray5), out BorderBrushProperty, System.Windows.Media.Brushes.Black, frameworkOptions);
                expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
                frameworkOptions = null;
                DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData> registrator6 = registrator5.Register<System.Windows.Media.Brush>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, System.Windows.Media.Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_Background)), expressionArray6), out BackgroundProperty, System.Windows.Media.Brushes.Black, frameworkOptions);
                expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
                frameworkOptions = null;
                DependencyPropertyRegistrator<ImageInplaceEditor.ImageEditorData> registrator7 = registrator6.Register<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_BorderThickness)), expressionArray7), out BorderThicknessProperty, new Thickness(0.0), frameworkOptions);
                expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageInplaceEditor.ImageEditorData), "d");
                ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
                frameworkOptions = null;
                registrator7.Register<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<ImageInplaceEditor.ImageEditorData, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageInplaceEditor.ImageEditorData.get_Padding)), expressionArray8), out PaddingProperty, new Thickness(0.0), frameworkOptions);
            }

            public ImageEditorData(ImageEditingField editingField) : base(editingField.ImageSource)
            {
                DevExpress.XtraPrinting.ImageSizeMode mode2;
                DevExpress.XtraPrinting.ImageAlignment alignment2;
                this.curves = new List<Curve>();
                this.zoomFactor = 1.0;
                DevExpress.XtraPrinting.ImageSizeMode[] modeArray1 = new DevExpress.XtraPrinting.ImageSizeMode[] { DevExpress.XtraPrinting.ImageSizeMode.Normal, DevExpress.XtraPrinting.ImageSizeMode.StretchImage, DevExpress.XtraPrinting.ImageSizeMode.ZoomImage, DevExpress.XtraPrinting.ImageSizeMode.Squeeze };
                this.<ImageSizeModes>k__BackingField = modeArray1;
                this.<ImageAlignments>k__BackingField = Enum.GetValues(typeof(DevExpress.XtraPrinting.ImageAlignment)).Cast<DevExpress.XtraPrinting.ImageAlignment>().Except<DevExpress.XtraPrinting.ImageAlignment>(DevExpress.XtraPrinting.ImageAlignment.Default.Yield<DevExpress.XtraPrinting.ImageAlignment>()).ToArray<DevExpress.XtraPrinting.ImageAlignment>();
                this.editingField = editingField;
                this.<ResetImageCommand>k__BackingField = DelegateCommandFactory.Create(new Action(this.ResetImage), new Func<bool>(this.CanReset));
                this.<ClearImageCommand>k__BackingField = DelegateCommandFactory.Create(new Action(this.ClearImage), () => base.Value != null);
                this.<OnImageLoadedCommand>k__BackingField = DelegateCommandFactory.Create<DevExpress.XtraPrinting.Drawing.ImageSource>(new Action<DevExpress.XtraPrinting.Drawing.ImageSource>(this.OnImageLoaded));
                this.<OnImageSelectedCommand>k__BackingField = DelegateCommandFactory.Create<ImageGalleryItem>(new Action<ImageGalleryItem>(this.OnImageSelected));
                this.<InitialValue>k__BackingField = editingField.ImageSource;
                DevExpress.XtraPrinting.ImageSizeMode mode = (editingField.ImageSizeMode == DevExpress.XtraPrinting.ImageSizeMode.AutoSize) ? DevExpress.XtraPrinting.ImageSizeMode.Normal : editingField.ImageSizeMode;
                DevExpress.XtraPrinting.ImageAlignment alignment = (editingField.ImageAlignment == DevExpress.XtraPrinting.ImageAlignment.Default) ? DevExpress.XtraPrinting.ImageAlignment.TopLeft : editingField.ImageAlignment;
                this.initialSizeMode = mode2 = mode;
                base.SetCurrentValue(ImageSizeModeProperty, mode2);
                this.initialAlignment = alignment2 = alignment;
                base.SetCurrentValue(ImageAlignmentProperty, alignment2);
                this.imageCreator = new PaintedImageCreator(this.ImageSizeMode, this.ImageAlignment, editingField.Brick.BackColor, ((ImageBrick) editingField.Brick).UseImageResolution);
                SizeF ef = GraphicsUnitConverter.DocToPixel(editingField.Brick.Size);
                this.<GalleryImageWidth>k__BackingField = ef.Width;
                this.<GalleryImageHeight>k__BackingField = ef.Height;
                ImageInplaceEditorInfo info = DevExpress.Xpf.Printing.EditingFieldExtensions.Instance.EditorInfoCollection.SingleOrDefault<InplaceEditorInfoBase>(x => (x.EditorName == editingField.EditorName)) as ImageInplaceEditorInfo;
                ImageEditorOptions local1 = info?.Options;
                ImageEditorOptions options1 = local1;
                if (local1 == null)
                {
                    ImageEditorOptions local2 = local1;
                    options1 = new ImageEditorOptions();
                }
                ImageEditorOptions options = options1;
                this.<AllowLoadImage>k__BackingField = options.AllowLoadImage;
                this.<AllowChangeSizeOptions>k__BackingField = options.AllowChangeSizeOptions;
                this.<AllowDraw>k__BackingField = options.AllowDraw;
                this.<AllowClear>k__BackingField = options.AllowClear;
                this.<AllowChooseImage>k__BackingField = options.PredefinedImages.Any<ImageGalleryItem>();
                this.<PredefinedImages>k__BackingField = options.PredefinedImages;
                this.<AllowSearchPredefinedImages>k__BackingField = options.AllowSearchPredefinedImages;
                Func<ImageGalleryItem, bool> predicate = <>c.<>9__102_2;
                if (<>c.<>9__102_2 == null)
                {
                    Func<ImageGalleryItem, bool> local3 = <>c.<>9__102_2;
                    predicate = <>c.<>9__102_2 = x => !string.IsNullOrEmpty(x.DisplayName);
                }
                this.<ShowPredefinedImageCaption>k__BackingField = options.PredefinedImages.Any<ImageGalleryItem>(predicate);
            }

            private bool CanReset() => 
                (base.Value != this.editingField.InitialImageSource) || ((this.ImageAlignment != this.editingField.InitialImageAlignment) || (this.ImageSizeMode != this.editingField.InitialImageSizeMode));

            private void ClearImage()
            {
                this.curves.Clear();
                base.Value = null;
            }

            void INativeImageRenderer.RegisterCallback(INativeImageRendererCallback callback)
            {
                this.callback = callback;
            }

            void INativeImageRenderer.ReleaseCallback()
            {
                this.callback = null;
            }

            void INativeImageRenderer.Render(Graphics graphics, Rect invalidateRect, System.Windows.Size renderSize)
            {
                if (this.editor != null)
                {
                    graphics.ResetTransform();
                    graphics.SmoothingMode = SmoothingMode.Default;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    DevExpress.XtraPrinting.Drawing.ImageSource imageSource = base.Value as DevExpress.XtraPrinting.Drawing.ImageSource;
                    if (!DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(imageSource))
                    {
                        SizeF clientSize = new SizeF((float) renderSize.Width, (float) renderSize.Height);
                        this.imageCreator.DrawImage(graphics, imageSource, clientSize, MathMethods.Scale(imageSource.GetImageSize(((ImageBrick) this.editingField.Brick).UseImageResolution), this.ActualZoomFactor));
                    }
                    if (this.currentCurve != null)
                    {
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        PointF[] points = (from pt in this.currentCurve.Points select PointF.Add(MathMethods.Scale(pt, this.ActualZoomFactor), new SizeF())).ToArray<PointF>();
                        float thickness = MathMethods.Scale(this.currentCurve.Thickness, (double) this.ActualZoomFactor);
                        this.imageCreator.DrawCurve(graphics, points, this.currentCurve.Color, thickness);
                    }
                }
            }

            void IPictureRenderer.AssignEditor(NativeImage editor)
            {
                this.editor.Do<NativeImage>(delegate (NativeImage x) {
                    x.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.OnMouseDown);
                    x.PreviewMouseMove -= new MouseEventHandler(this.OnMouseMove);
                    x.PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(this.OnMouseUp);
                });
                this.editor = editor;
                this.editor.Do<NativeImage>(delegate (NativeImage x) {
                    x.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.OnMouseDown);
                    x.PreviewMouseMove += new MouseEventHandler(this.OnMouseMove);
                    x.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.OnMouseUp);
                });
            }

            private void OnImageLoaded(DevExpress.XtraPrinting.Drawing.ImageSource newImage)
            {
                base.Value = newImage;
                this.curves.Clear();
                Action<NativeImage> action = <>c.<>9__122_0;
                if (<>c.<>9__122_0 == null)
                {
                    Action<NativeImage> local1 = <>c.<>9__122_0;
                    action = <>c.<>9__122_0 = x => x.Invalidate();
                }
                this.editor.Do<NativeImage>(action);
            }

            private void OnImageSelected(ImageGalleryItem item)
            {
                if (item != null)
                {
                    base.Value = new DevExpress.XtraPrinting.Drawing.ImageSource(item.Image);
                }
            }

            private void OnMouseDown(object sender, MouseButtonEventArgs e)
            {
                if (this.AllowDraw)
                {
                    this.editor.CaptureMouse();
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(this.BrushColor.A, this.BrushColor.R, this.BrushColor.G, this.BrushColor.B);
                    this.currentCurve = new Curve(color, (float) this.BrushSize);
                    System.Windows.Point position = e.GetPosition(this.editor);
                    PointF item = MathMethods.Scale(new PointF((float) position.X, (float) position.Y), (double) (1.0 / this.zoomFactor));
                    this.currentCurve.Points.Add(item);
                    this.editor.Invalidate();
                }
            }

            private void OnMouseMove(object sender, MouseEventArgs e)
            {
                if (this.AllowDraw && (this.currentCurve != null))
                {
                    System.Windows.Point position = e.GetPosition(this.editor);
                    PointF pointF = MathMethods.Scale(new PointF((float) position.X, (float) position.Y), (double) (1.0 / this.zoomFactor));
                    this.currentCurve.Do<Curve>(delegate (Curve x) {
                        x.Points.Add(pointF);
                        this.editor.Invalidate();
                    });
                }
            }

            private void OnMouseUp(object sender, MouseButtonEventArgs e)
            {
                if (this.AllowDraw && (this.currentCurve != null))
                {
                    this.editor.ReleaseMouseCapture();
                    this.currentCurve.Do<Curve>(delegate (Curve x) {
                        this.curves.Add(x);
                        this.currentCurve = null;
                        this.editor.Invalidate();
                    });
                    System.Drawing.Image image = this.imageCreator.CreatePaintedImage((DevExpress.XtraPrinting.Drawing.ImageSource) base.Value, this.curves, new SizeF((float) this.editor.RenderSize.Width, (float) this.editor.RenderSize.Height), (float) this.ZoomFactor);
                    base.Value = new DevExpress.XtraPrinting.Drawing.ImageSource(image);
                }
            }

            protected override void OnValueChanged(object oldValue)
            {
                base.OnValueChanged(oldValue);
                this.curves.Clear();
                Action<NativeImage> action = <>c.<>9__115_0;
                if (<>c.<>9__115_0 == null)
                {
                    Action<NativeImage> local1 = <>c.<>9__115_0;
                    action = <>c.<>9__115_0 = x => x.Invalidate();
                }
                this.editor.Do<NativeImage>(action);
            }

            private void ResetImage()
            {
                this.curves.Clear();
                base.Value = this.editingField.InitialImageSource;
                this.ImageAlignment = this.editingField.InitialImageAlignment;
                this.ImageSizeMode = this.editingField.InitialImageSizeMode;
            }

            public ICommand ResetImageCommand { get; }

            public ICommand OnImageLoadedCommand { get; }

            public ICommand OnImageSelectedCommand { get; }

            public ICommand ClearImageCommand { get; }

            private DevExpress.XtraPrinting.Drawing.ImageSource InitialValue { get; }

            public double ZoomFactor
            {
                get => 
                    this.zoomFactor;
                set
                {
                    this.zoomFactor = value;
                    Action<NativeImage> action = <>c.<>9__20_0;
                    if (<>c.<>9__20_0 == null)
                    {
                        Action<NativeImage> local1 = <>c.<>9__20_0;
                        action = <>c.<>9__20_0 = x => x.Invalidate();
                    }
                    this.editor.Do<NativeImage>(action);
                }
            }

            private float ActualZoomFactor
            {
                get
                {
                    Func<NativeImage, double> evaluator = <>c.<>9__22_0;
                    if (<>c.<>9__22_0 == null)
                    {
                        Func<NativeImage, double> local1 = <>c.<>9__22_0;
                        evaluator = <>c.<>9__22_0 = x => x.GetScaleX();
                    }
                    return (float) (this.ZoomFactor * this.editor.Return<NativeImage, double>(evaluator, (<>c.<>9__22_1 ??= () => 1.0)));
                }
            }

            public double GalleryImageWidth { get; }

            public double GalleryImageHeight { get; }

            public IEnumerable<DevExpress.XtraPrinting.ImageSizeMode> ImageSizeModes { get; }

            public IEnumerable<DevExpress.XtraPrinting.ImageAlignment> ImageAlignments { get; }

            public bool AllowLoadImage { get; }

            public bool AllowDraw { get; }

            public bool AllowChooseImage { get; }

            public bool AllowChangeSizeOptions { get; }

            public bool AllowResetImage =>
                this.editingField.InitialImageSource != null;

            public bool AllowClear { get; }

            public IEnumerable<ImageGalleryItem> PredefinedImages { get; }

            public bool AllowSearchPredefinedImages { get; }

            public bool ShowPredefinedImageCaption { get; }

            public DevExpress.XtraPrinting.ImageAlignment ImageAlignment
            {
                get => 
                    (DevExpress.XtraPrinting.ImageAlignment) base.GetValue(ImageAlignmentProperty);
                set => 
                    base.SetValue(ImageAlignmentProperty, value);
            }

            public DevExpress.XtraPrinting.ImageSizeMode ImageSizeMode
            {
                get => 
                    (DevExpress.XtraPrinting.ImageSizeMode) base.GetValue(ImageSizeModeProperty);
                set => 
                    base.SetValue(ImageSizeModeProperty, value);
            }

            public double BrushSize
            {
                get => 
                    (double) base.GetValue(BrushSizeProperty);
                set => 
                    base.SetValue(BrushSizeProperty, value);
            }

            public System.Windows.Media.Color BrushColor
            {
                get => 
                    (System.Windows.Media.Color) base.GetValue(BrushColorProperty);
                set => 
                    base.SetValue(BrushColorProperty, value);
            }

            public System.Windows.Media.Brush BorderBrush
            {
                get => 
                    (System.Windows.Media.Brush) base.GetValue(BorderBrushProperty);
                set => 
                    base.SetValue(BorderBrushProperty, value);
            }

            public System.Windows.Media.Brush Background
            {
                get => 
                    (System.Windows.Media.Brush) base.GetValue(BackgroundProperty);
                set => 
                    base.SetValue(BackgroundProperty, value);
            }

            public Thickness BorderThickness
            {
                get => 
                    (Thickness) base.GetValue(BorderThicknessProperty);
                set => 
                    base.SetValue(BorderThicknessProperty, value);
            }

            public Thickness Padding
            {
                get => 
                    (Thickness) base.GetValue(PaddingProperty);
                set => 
                    base.SetValue(PaddingProperty, value);
            }

            public System.Drawing.Image DrawingImage
            {
                get => 
                    (System.Drawing.Image) base.GetValue(DrawingImageProperty);
                set => 
                    base.SetValue(DrawingImagePropertyKey, value);
            }

            public override bool IsValueChanged =>
                !ReferenceEquals(this.InitialValue, base.Value) || ((this.initialAlignment != this.ImageAlignment) || (this.initialSizeMode != this.ImageSizeMode));

            public IPictureRenderer Renderer =>
                this;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ImageInplaceEditor.ImageEditorData.<>c <>9 = new ImageInplaceEditor.ImageEditorData.<>c();
                public static Action<NativeImage> <>9__20_0;
                public static Func<NativeImage, double> <>9__22_0;
                public static Func<double> <>9__22_1;
                public static Action<NativeImage> <>9__100_2;
                public static Action<NativeImage> <>9__100_5;
                public static Func<ImageGalleryItem, bool> <>9__102_2;
                public static Action<NativeImage> <>9__115_0;
                public static Action<NativeImage> <>9__122_0;

                internal void <.cctor>b__100_0(ImageInplaceEditor.ImageEditorData d)
                {
                    d.imageCreator.Do<PaintedImageCreator>(x => x.SizeMode = d.ImageSizeMode);
                    Action<NativeImage> action = <>9__100_2;
                    if (<>9__100_2 == null)
                    {
                        Action<NativeImage> local1 = <>9__100_2;
                        action = <>9__100_2 = x => x.Invalidate();
                    }
                    d.editor.Do<NativeImage>(action);
                    CommandManager.InvalidateRequerySuggested();
                }

                internal void <.cctor>b__100_2(NativeImage x)
                {
                    x.Invalidate();
                }

                internal void <.cctor>b__100_3(ImageInplaceEditor.ImageEditorData d)
                {
                    d.imageCreator.Do<PaintedImageCreator>(x => x.Alignment = d.ImageAlignment);
                    Action<NativeImage> action = <>9__100_5;
                    if (<>9__100_5 == null)
                    {
                        Action<NativeImage> local1 = <>9__100_5;
                        action = <>9__100_5 = x => x.Invalidate();
                    }
                    d.editor.Do<NativeImage>(action);
                    CommandManager.InvalidateRequerySuggested();
                }

                internal void <.cctor>b__100_5(NativeImage x)
                {
                    x.Invalidate();
                }

                internal bool <.ctor>b__102_2(ImageGalleryItem x) => 
                    !string.IsNullOrEmpty(x.DisplayName);

                internal double <get_ActualZoomFactor>b__22_0(NativeImage x) => 
                    x.GetScaleX();

                internal double <get_ActualZoomFactor>b__22_1() => 
                    1.0;

                internal void <OnImageLoaded>b__122_0(NativeImage x)
                {
                    x.Invalidate();
                }

                internal void <OnValueChanged>b__115_0(NativeImage x)
                {
                    x.Invalidate();
                }

                internal void <set_ZoomFactor>b__20_0(NativeImage x)
                {
                    x.Invalidate();
                }
            }
        }
    }
}

