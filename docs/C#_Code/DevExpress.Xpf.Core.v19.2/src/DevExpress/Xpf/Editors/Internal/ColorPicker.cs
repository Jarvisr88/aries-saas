namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ColorPicker : Control, IColorEdit
    {
        private static readonly bool CanUseDPIAwareCtor;
        private const double ZThumbHeight = 10.0;
        private const double AlphaThumbWidth = 10.0;
        private const double XYThumbHeight = 21.0;
        private const double XYThumbWidth = 21.0;
        private const double MinWidthWithEditors = 297.0;
        public static readonly DependencyProperty EditModeProperty;
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty DefaultColorProperty;
        public static readonly DependencyProperty ShowDefaultColorProperty;
        public static readonly DependencyProperty ShowEditorsProperty;
        public static readonly DependencyProperty ColorModeProperty;
        public static readonly DependencyProperty ShowAlphaChannelProperty;
        private static readonly DependencyPropertyKey ColorViewModelPropertyKey;
        public static readonly DependencyProperty ColorViewModelProperty;
        private static readonly DependencyPropertyKey ActualShowEditorsPropertyKey;
        public static readonly DependencyProperty ActualShowEditorsProperty;
        private static readonly DependencyPropertyKey HSBColorPropertyKey;
        public static readonly DependencyProperty HSBColorProperty;
        private static readonly DependencyPropertyKey ActualZThumbOffsetPropertyKey;
        public static readonly DependencyProperty ActualZThumbOffsetProperty;
        private static readonly DependencyPropertyKey ActualXYThumbXOffsetPropertyKey;
        public static readonly DependencyProperty ActualXYThumbXOffsetProperty;
        private static readonly DependencyPropertyKey ActualXYThumbYOffsetPropertyKey;
        public static readonly DependencyProperty ActualXYThumbYOffsetProperty;
        private static readonly DependencyPropertyKey ActualAlphaThumbOffsetPropertyKey;
        public static readonly DependencyProperty ActualAlphaThumbOffsetProperty;
        public static readonly RoutedEvent ColorChangedEvent;
        private Canvas zColorArea;
        private Canvas colorArea;
        private Canvas alphaChannelArea;
        private DataContentPresenter editorsContentPresenter;
        private TextEdit resultColorTextEdit;
        private readonly Locker updateColorLocker = new Locker();
        private readonly Locker updateThumbsLocker = new Locker();
        private readonly Locker pippetUpdateColorLocker = new Locker();
        private Window transparentWindow;
        private System.Windows.Media.Color pippetPreviousColor;
        private System.Windows.Media.Color colorCache;
        private static System.Windows.Media.Color transparentWindowBackgroundColor;

        public event RoutedEventHandler ColorChanged
        {
            add
            {
                base.AddHandler(ColorChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ColorChangedEvent, value);
            }
        }

        static ColorPicker()
        {
            Type[] types = new Type[] { typeof(string), typeof(bool) };
            CanUseDPIAwareCtor = typeof(Cursor).GetConstructor(types) != null;
            transparentWindowBackgroundColor = System.Windows.Media.Color.FromArgb(1, 0xff, 0xff, 0xff);
            Type ownerType = typeof(ColorPicker);
            EditModeProperty = DependencyPropertyManager.Register("EditMode", typeof(DevExpress.Xpf.Editors.EditMode), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.EditMode.Standalone, (obj, args) => ((ColorPicker) obj).OnEditModeChanged((DevExpress.Xpf.Editors.EditMode) args.NewValue)));
            ColorProperty = DependencyPropertyManager.Register("Color", typeof(System.Windows.Media.Color), ownerType, new FrameworkPropertyMetadata(Text2ColorHelper.DefaultColor, FrameworkPropertyMetadataOptions.None, (obj, args) => ((ColorPicker) obj).OnColorChangedInternal((System.Windows.Media.Color) args.OldValue, (System.Windows.Media.Color) args.NewValue)));
            DefaultColorProperty = DependencyPropertyManager.Register("DefaultColor", typeof(System.Windows.Media.Color), ownerType, new FrameworkPropertyMetadata(Text2ColorHelper.DefaultColor));
            ShowDefaultColorProperty = DependencyPropertyManager.Register("ShowDefaultColor", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            ShowEditorsProperty = DependencyPropertyManager.Register("ShowEditors", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (obj, args) => ((ColorPicker) obj).OnShowEditorsChangedInternal((bool) args.OldValue, (bool) args.NewValue)));
            ColorModeProperty = DependencyPropertyManager.Register("ColorMode", typeof(ColorPickerColorMode), ownerType, new FrameworkPropertyMetadata(ColorPickerColorMode.RGB, FrameworkPropertyMetadataOptions.None, (obj, args) => ((ColorPicker) obj).OnColorModeChangedInternal((ColorPickerColorMode) args.OldValue, (ColorPickerColorMode) args.NewValue)));
            ShowAlphaChannelProperty = DependencyPropertyManager.Register("ShowAlphaChannel", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (obj, args) => ((ColorPicker) obj).OnAllowTransparencyChanged((bool) args.NewValue)));
            ColorViewModelPropertyKey = DependencyPropertyManager.RegisterReadOnly("ColorViewModel", typeof(ColorBase), ownerType, new FrameworkPropertyMetadata(null));
            ColorViewModelProperty = ColorViewModelPropertyKey.DependencyProperty;
            ActualShowEditorsPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowEditors", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (obj, args) => ((ColorPicker) obj).OnActualShowEditorsChangedInternal((bool) args.OldValue, (bool) args.NewValue)));
            ActualShowEditorsProperty = ActualShowEditorsPropertyKey.DependencyProperty;
            HSBColorPropertyKey = DependencyPropertyManager.RegisterReadOnly("HSBColor", typeof(DevExpress.Xpf.Editors.Internal.HSBColor), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((ColorPicker) obj).OnHSBColorChangedInternal((DevExpress.Xpf.Editors.Internal.HSBColor) args.OldValue, (DevExpress.Xpf.Editors.Internal.HSBColor) args.NewValue)));
            HSBColorProperty = HSBColorPropertyKey.DependencyProperty;
            ActualZThumbOffsetPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualZThumbOffset", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            ActualZThumbOffsetProperty = ActualZThumbOffsetPropertyKey.DependencyProperty;
            ActualXYThumbXOffsetPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualXYThumbXOffset", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            ActualXYThumbXOffsetProperty = ActualXYThumbXOffsetPropertyKey.DependencyProperty;
            ActualXYThumbYOffsetPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualXYThumbYOffset", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            ActualXYThumbYOffsetProperty = ActualXYThumbYOffsetPropertyKey.DependencyProperty;
            ActualAlphaThumbOffsetPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAlphaThumbOffset", typeof(double), ownerType, new FrameworkPropertyMetadata(0.0));
            ActualAlphaThumbOffsetProperty = ActualAlphaThumbOffsetPropertyKey.DependencyProperty;
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChangedEvent", RoutingStrategy.Direct, typeof(ColorChangedEventArgs), ownerType);
            EyeDropCursor = LoadEyeDropCursor();
        }

        public ColorPicker()
        {
            this.SetDefaultStyleKey(typeof(ColorPicker));
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.EnablePippetModeCommand = new DelegateCommand(new Action(this.EnablePippetMode));
            this.SetDefaultColorCommand = new DelegateCommand(new Action(this.SetDefaultColor));
            this.ColorViewModel = new RGBColor(this.Color);
            this.ColorViewModel.Do<ColorBase>(delegate (ColorBase x) {
                x.ColorChanged += new EventHandler<ColorViewModelValueChangedEventArgs>(this.ColorViewModelColorChanged);
            });
        }

        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
        private static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        private void ColorViewModelColorChanged(object sender, ColorViewModelValueChangedEventArgs e)
        {
            if ((this.HSBColor == null) || (this.HSBColor.Color != e.Color))
            {
                this.HSBColor = new DevExpress.Xpf.Editors.Internal.HSBColor(e.Color);
            }
        }

        void IColorEdit.AddCustomColor(System.Windows.Media.Color color)
        {
        }

        private void EnablePippetMode()
        {
            this.pippetPreviousColor = this.Color;
            Rect screenRectsUnion = ScreenHelper.GetScreenRectsUnion();
            this.InitTransparentWindow((int) screenRectsUnion.Width, (int) screenRectsUnion.Height, (int) screenRectsUnion.Left, (int) screenRectsUnion.Top);
            this.transparentWindow.Show();
            this.transparentWindow.CaptureMouse();
            this.pippetUpdateColorLocker.LockOnce();
        }

        private System.Windows.Media.Color ExcludeColor(System.Windows.Media.Color resultColor, System.Windows.Media.Color toExclude)
        {
            float num = 1f;
            float num5 = ((float) toExclude.A) / 255f;
            float num6 = ((float) toExclude.R) / 255f;
            float num7 = ((float) toExclude.G) / 255f;
            float num8 = ((float) toExclude.B) / 255f;
            float num10 = (((((float) resultColor.G) / 255f) * num) - (num7 * num5)) / (1f - num5);
            float num11 = (((((float) resultColor.B) / 255f) * num) - (num8 * num5)) / (1f - num5);
            return System.Windows.Media.Color.FromArgb(0xff, Convert.ToByte((float) (((((((float) resultColor.R) / 255f) * num) - (num6 * num5)) / (1f - num5)) * 255f)), Convert.ToByte((float) (num10 * 255f)), Convert.ToByte((float) (num11 * 255f)));
        }

        private void FocusFirstEditor()
        {
            if (this.ActualShowEditors)
            {
                IList<SpinEdit> editors = this.GetEditors();
                if (editors.Count != 0)
                {
                    editors.First<SpinEdit>().Focus();
                }
            }
        }

        [SecuritySafeCritical]
        private System.Windows.Media.Color GetColorFromPoint(System.Windows.Point point)
        {
            Bitmap image = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                using (Graphics graphics2 = Graphics.FromHwnd(IntPtr.Zero))
                {
                    int num = BitBlt(graphics.GetHdc(), 0, 0, 1, 1, graphics2.GetHdc(), (int) point.X, (int) point.Y, 0xcc0020);
                    graphics.ReleaseHdc();
                    graphics2.ReleaseHdc();
                }
            }
            System.Drawing.Color pixel = image.GetPixel(0, 0);
            return System.Windows.Media.Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Win32Point pt);
        private static Stream GetCursorStream(string name) => 
            typeof(ColorPicker).Assembly.GetManifestResourceStream("DevExpress.Xpf.Core.Editors.Images.ColorEdit." + name);

        private IList<SpinEdit> GetEditors()
        {
            List<SpinEdit> editors = new List<SpinEdit>();
            if (this.ActualShowEditors)
            {
                LayoutHelper.FindElement(this.editorsContentPresenter, delegate (FrameworkElement element) {
                    if (element is SpinEdit)
                    {
                        editors.Add((SpinEdit) element);
                    }
                    return false;
                });
            }
            return editors;
        }

        [SecuritySafeCritical]
        private static System.Windows.Point GetMousePosition()
        {
            Win32Point pt = new Win32Point();
            GetCursorPos(ref pt);
            return new System.Windows.Point((double) pt.X, (double) pt.Y);
        }

        private void InitTooltips()
        {
            if (base.IsLoaded && (this.EditMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive))
            {
                switch (this.ColorMode)
                {
                    case ColorPickerColorMode.RGB:
                    {
                        Func<FrameworkElement, TextBlock> func2 = <>c.<>9__154_0;
                        if (<>c.<>9__154_0 == null)
                        {
                            Func<FrameworkElement, TextBlock> local1 = <>c.<>9__154_0;
                            func2 = <>c.<>9__154_0 = x => x as TextBlock;
                        }
                        Action<TextBlock> action2 = <>c.<>9__154_1;
                        if (<>c.<>9__154_1 == null)
                        {
                            Action<TextBlock> local2 = <>c.<>9__154_1;
                            action2 = <>c.<>9__154_1 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerRed);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_RTextBlock").With<FrameworkElement, TextBlock>(func2).Do<TextBlock>(action2);
                        Func<FrameworkElement, TextBlock> func6 = <>c.<>9__154_2;
                        if (<>c.<>9__154_2 == null)
                        {
                            Func<FrameworkElement, TextBlock> local3 = <>c.<>9__154_2;
                            func6 = <>c.<>9__154_2 = x => x as TextBlock;
                        }
                        Action<TextBlock> action3 = <>c.<>9__154_3;
                        if (<>c.<>9__154_3 == null)
                        {
                            Action<TextBlock> local4 = <>c.<>9__154_3;
                            action3 = <>c.<>9__154_3 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerGreen);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_GTextBlock").With<FrameworkElement, TextBlock>(func6).Do<TextBlock>(action3);
                        Func<FrameworkElement, TextBlock> func7 = <>c.<>9__154_4;
                        if (<>c.<>9__154_4 == null)
                        {
                            Func<FrameworkElement, TextBlock> local5 = <>c.<>9__154_4;
                            func7 = <>c.<>9__154_4 = x => x as TextBlock;
                        }
                        Action<TextBlock> action4 = <>c.<>9__154_5;
                        if (<>c.<>9__154_5 == null)
                        {
                            Action<TextBlock> local6 = <>c.<>9__154_5;
                            action4 = <>c.<>9__154_5 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerBlue);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_BTextBlock").With<FrameworkElement, TextBlock>(func7).Do<TextBlock>(action4);
                        break;
                    }
                    case ColorPickerColorMode.CMYK:
                    {
                        Func<FrameworkElement, TextBlock> func3 = <>c.<>9__154_18;
                        if (<>c.<>9__154_18 == null)
                        {
                            Func<FrameworkElement, TextBlock> local19 = <>c.<>9__154_18;
                            func3 = <>c.<>9__154_18 = x => x as TextBlock;
                        }
                        Action<TextBlock> action5 = <>c.<>9__154_19;
                        if (<>c.<>9__154_19 == null)
                        {
                            Action<TextBlock> local20 = <>c.<>9__154_19;
                            action5 = <>c.<>9__154_19 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerCyan);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_CTextBlock").With<FrameworkElement, TextBlock>(func3).Do<TextBlock>(action5);
                        Func<FrameworkElement, TextBlock> func8 = <>c.<>9__154_20;
                        if (<>c.<>9__154_20 == null)
                        {
                            Func<FrameworkElement, TextBlock> local21 = <>c.<>9__154_20;
                            func8 = <>c.<>9__154_20 = x => x as TextBlock;
                        }
                        Action<TextBlock> action6 = <>c.<>9__154_21;
                        if (<>c.<>9__154_21 == null)
                        {
                            Action<TextBlock> local22 = <>c.<>9__154_21;
                            action6 = <>c.<>9__154_21 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerMagenta);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_MTextBlock").With<FrameworkElement, TextBlock>(func8).Do<TextBlock>(action6);
                        Func<FrameworkElement, TextBlock> func9 = <>c.<>9__154_22;
                        if (<>c.<>9__154_22 == null)
                        {
                            Func<FrameworkElement, TextBlock> local23 = <>c.<>9__154_22;
                            func9 = <>c.<>9__154_22 = x => x as TextBlock;
                        }
                        Action<TextBlock> action7 = <>c.<>9__154_23;
                        if (<>c.<>9__154_23 == null)
                        {
                            Action<TextBlock> local24 = <>c.<>9__154_23;
                            action7 = <>c.<>9__154_23 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerYellow);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_YTextBlock").With<FrameworkElement, TextBlock>(func9).Do<TextBlock>(action7);
                        Func<FrameworkElement, TextBlock> func10 = <>c.<>9__154_24;
                        if (<>c.<>9__154_24 == null)
                        {
                            Func<FrameworkElement, TextBlock> local25 = <>c.<>9__154_24;
                            func10 = <>c.<>9__154_24 = x => x as TextBlock;
                        }
                        Action<TextBlock> action8 = <>c.<>9__154_25;
                        if (<>c.<>9__154_25 == null)
                        {
                            Action<TextBlock> local26 = <>c.<>9__154_25;
                            action8 = <>c.<>9__154_25 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerKeyColor);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_KTextBlock").With<FrameworkElement, TextBlock>(func10).Do<TextBlock>(action8);
                        break;
                    }
                    case ColorPickerColorMode.HLS:
                    {
                        Func<FrameworkElement, TextBlock> func4 = <>c.<>9__154_12;
                        if (<>c.<>9__154_12 == null)
                        {
                            Func<FrameworkElement, TextBlock> local13 = <>c.<>9__154_12;
                            func4 = <>c.<>9__154_12 = x => x as TextBlock;
                        }
                        Action<TextBlock> action9 = <>c.<>9__154_13;
                        if (<>c.<>9__154_13 == null)
                        {
                            Action<TextBlock> local14 = <>c.<>9__154_13;
                            action9 = <>c.<>9__154_13 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerHue);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_HTextBlock").With<FrameworkElement, TextBlock>(func4).Do<TextBlock>(action9);
                        Func<FrameworkElement, TextBlock> func11 = <>c.<>9__154_14;
                        if (<>c.<>9__154_14 == null)
                        {
                            Func<FrameworkElement, TextBlock> local15 = <>c.<>9__154_14;
                            func11 = <>c.<>9__154_14 = x => x as TextBlock;
                        }
                        Action<TextBlock> action10 = <>c.<>9__154_15;
                        if (<>c.<>9__154_15 == null)
                        {
                            Action<TextBlock> local16 = <>c.<>9__154_15;
                            action10 = <>c.<>9__154_15 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerLightness);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_LTextBlock").With<FrameworkElement, TextBlock>(func11).Do<TextBlock>(action10);
                        Func<FrameworkElement, TextBlock> func12 = <>c.<>9__154_16;
                        if (<>c.<>9__154_16 == null)
                        {
                            Func<FrameworkElement, TextBlock> local17 = <>c.<>9__154_16;
                            func12 = <>c.<>9__154_16 = x => x as TextBlock;
                        }
                        Action<TextBlock> action11 = <>c.<>9__154_17;
                        if (<>c.<>9__154_17 == null)
                        {
                            Action<TextBlock> local18 = <>c.<>9__154_17;
                            action11 = <>c.<>9__154_17 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerSaturation);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_STextBlock").With<FrameworkElement, TextBlock>(func12).Do<TextBlock>(action11);
                        break;
                    }
                    case ColorPickerColorMode.HSB:
                    {
                        Func<FrameworkElement, TextBlock> func5 = <>c.<>9__154_6;
                        if (<>c.<>9__154_6 == null)
                        {
                            Func<FrameworkElement, TextBlock> local7 = <>c.<>9__154_6;
                            func5 = <>c.<>9__154_6 = x => x as TextBlock;
                        }
                        Action<TextBlock> action12 = <>c.<>9__154_7;
                        if (<>c.<>9__154_7 == null)
                        {
                            Action<TextBlock> local8 = <>c.<>9__154_7;
                            action12 = <>c.<>9__154_7 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerHue);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_HTextBlock").With<FrameworkElement, TextBlock>(func5).Do<TextBlock>(action12);
                        Func<FrameworkElement, TextBlock> func13 = <>c.<>9__154_8;
                        if (<>c.<>9__154_8 == null)
                        {
                            Func<FrameworkElement, TextBlock> local9 = <>c.<>9__154_8;
                            func13 = <>c.<>9__154_8 = x => x as TextBlock;
                        }
                        Action<TextBlock> action13 = <>c.<>9__154_9;
                        if (<>c.<>9__154_9 == null)
                        {
                            Action<TextBlock> local10 = <>c.<>9__154_9;
                            action13 = <>c.<>9__154_9 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerSaturation);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_STextBlock").With<FrameworkElement, TextBlock>(func13).Do<TextBlock>(action13);
                        Func<FrameworkElement, TextBlock> func14 = <>c.<>9__154_10;
                        if (<>c.<>9__154_10 == null)
                        {
                            Func<FrameworkElement, TextBlock> local11 = <>c.<>9__154_10;
                            func14 = <>c.<>9__154_10 = x => x as TextBlock;
                        }
                        Action<TextBlock> action14 = <>c.<>9__154_11;
                        if (<>c.<>9__154_11 == null)
                        {
                            Action<TextBlock> local12 = <>c.<>9__154_11;
                            action14 = <>c.<>9__154_11 = delegate (TextBlock x) {
                                ToolTip toolTip = new ToolTip();
                                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerBrightness);
                                x.SetToolTip(toolTip);
                            };
                        }
                        LayoutHelper.FindElementByName(this, "PART_BTextBlock").With<FrameworkElement, TextBlock>(func14).Do<TextBlock>(action14);
                        break;
                    }
                    default:
                        break;
                }
                Func<FrameworkElement, TextBlock> evaluator = <>c.<>9__154_26;
                if (<>c.<>9__154_26 == null)
                {
                    Func<FrameworkElement, TextBlock> local27 = <>c.<>9__154_26;
                    evaluator = <>c.<>9__154_26 = x => x as TextBlock;
                }
                Action<TextBlock> action = <>c.<>9__154_27;
                if (<>c.<>9__154_27 == null)
                {
                    Action<TextBlock> local28 = <>c.<>9__154_27;
                    action = <>c.<>9__154_27 = delegate (TextBlock x) {
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerAlpha);
                        x.SetToolTip(toolTip);
                    };
                }
                LayoutHelper.FindElementByName(this, "PART_ATextBlock").With<FrameworkElement, TextBlock>(evaluator).Do<TextBlock>(action);
            }
        }

        private void InitTransparentWindow(int width, int height, int left, int top)
        {
            this.transparentWindow ??= new Window();
            if (!this.transparentWindow.IsInitialized)
            {
                this.transparentWindow.Width = width;
                this.transparentWindow.Height = height;
                this.transparentWindow.Background = new SolidColorBrush(transparentWindowBackgroundColor);
                this.transparentWindow.WindowStyle = WindowStyle.None;
                this.transparentWindow.AllowsTransparency = true;
                this.transparentWindow.Left = left;
                this.transparentWindow.Top = top;
                this.transparentWindow.Topmost = true;
                this.transparentWindow.ShowInTaskbar = false;
                this.transparentWindow.Focusable = false;
                this.transparentWindow.ShowActivated = false;
                this.transparentWindow.Cursor = EyeDropCursor;
                this.transparentWindow.PreviewMouseDown += new MouseButtonEventHandler(this.OnTransparentWindowMouseDown);
                this.transparentWindow.PreviewMouseMove += new MouseEventHandler(this.OnPreviewMouseMove);
            }
        }

        private bool IsFirstEditor() => 
            this.GetEditors().First<SpinEdit>().IsKeyboardFocusWithin;

        private bool IsLastEditor() => 
            this.resultColorTextEdit.IsKeyboardFocusWithin;

        private static Cursor LoadEyeDropCursor()
        {
            if (!CanUseDPIAwareCtor)
            {
                return new Cursor(GetCursorStream("EyeDropCursor.cur"));
            }
            object[] args = new object[] { GetCursorStream("EyeDropCursor.cur"), true };
            return (Cursor) Activator.CreateInstance(typeof(Cursor), args);
        }

        private void MoveAlphaThumb(MouseEventArgs e)
        {
            double num = Math.Max(Math.Min(e.GetPosition(this.alphaChannelArea).X, this.alphaChannelArea.ActualWidth), 0.0);
            this.ActualAlphaThumbOffset = num - 5.0;
            this.updateThumbsLocker.DoLockedAction(new Action(this.UpdateColorFromThumbs));
        }

        private void MoveXYThumb(MouseEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(this.colorArea);
            double num = Math.Max(Math.Min(position.X, this.colorArea.ActualWidth), 0.0);
            double num2 = Math.Max(Math.Min(position.Y, this.colorArea.ActualHeight), 0.0);
            this.ActualXYThumbXOffset = num - 10.5;
            this.ActualXYThumbYOffset = num2 - 10.5;
            this.updateThumbsLocker.DoLockedAction(new Action(this.UpdateColorFromThumbs));
        }

        private void MoveZThumb(MouseEventArgs e)
        {
            double num = Math.Max(Math.Min(e.GetPosition(this.zColorArea).Y, this.zColorArea.ActualHeight), 0.0);
            this.ActualZThumbOffset = num - 5.0;
            this.updateThumbsLocker.DoLockedAction(new Action(this.UpdateColorFromThumbs));
        }

        public bool NeedsKey(Key key, ModifierKeys modifiers) => 
            this.ActualShowEditors ? ((key != Key.Tab) || !(ModifierKeysHelper.IsShiftPressed(modifiers) ? this.IsFirstEditor() : this.IsLastEditor())) : true;

        protected virtual void OnActualShowEditorsChanged(bool oldValue, bool newValue)
        {
        }

        private void OnActualShowEditorsChangedInternal(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                this.InitTooltips();
            }
            this.OnActualShowEditorsChanged(oldValue, newValue);
        }

        protected virtual void OnAllowTransparencyChanged(bool newValue)
        {
        }

        private void OnAlphaChannelAreaLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsAlphaThumbMoving = true;
            this.alphaChannelArea.CaptureMouse();
            this.MoveAlphaThumb(e);
        }

        private void OnAlphaChannelAreaLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.IsAlphaThumbMoving = false;
            this.alphaChannelArea.ReleaseMouseCapture();
        }

        private void OnAlphaChannelAreaMouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsAlphaThumbMoving)
            {
                this.MoveAlphaThumb(e);
            }
        }

        private void OnAlphaChannelAreaSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.updateThumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateThumbs));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UnsubscribeEvents();
            this.zColorArea = (Canvas) base.GetTemplateChild("PART_ZColorArea");
            this.colorArea = (Canvas) base.GetTemplateChild("PART_ColorArea");
            this.alphaChannelArea = (Canvas) base.GetTemplateChild("PART_AlphaChannelArea");
            this.resultColorTextEdit = (TextEdit) base.GetTemplateChild("PART_ResultColor");
            this.editorsContentPresenter = (DataContentPresenter) base.GetTemplateChild("PART_EditorsContentPresenter");
            this.SubscribeEvents();
        }

        protected internal virtual void OnApplyTemplateInternal()
        {
            this.SetupFocusedEditor();
            this.InitTooltips();
        }

        private void OnColorAreaLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsXYThumbMoving = true;
            this.colorArea.CaptureMouse();
            this.MoveXYThumb(e);
        }

        private void OnColorAreaLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.IsXYThumbMoving = false;
            this.colorArea.ReleaseMouseCapture();
        }

        private void OnColorAreaMouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsXYThumbMoving)
            {
                this.MoveXYThumb(e);
            }
        }

        private void OnColorAreaSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.updateThumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateThumbs));
        }

        protected virtual void OnColorChanged(System.Windows.Media.Color oldValue, System.Windows.Media.Color newValue)
        {
        }

        private void OnColorChangedInternal(System.Windows.Media.Color oldValue, System.Windows.Media.Color newValue)
        {
            this.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateHSBColor));
            if (!this.pippetUpdateColorLocker.IsLocked)
            {
                this.colorCache = newValue;
                this.OnColorChanged(oldValue, newValue);
                this.RaiseColorChanged(newValue);
            }
        }

        protected virtual void OnColorModeChanged(ColorPickerColorMode oldValue, ColorPickerColorMode newValue)
        {
            this.ColorViewModel.Do<ColorBase>(delegate (ColorBase x) {
                x.ColorChanged -= new EventHandler<ColorViewModelValueChangedEventArgs>(this.ColorViewModelColorChanged);
            });
            switch (newValue)
            {
                case ColorPickerColorMode.RGB:
                {
                    RGBColor color1 = new RGBColor(this.Color);
                    color1.EditMode = this.EditMode;
                    this.ColorViewModel = color1;
                    break;
                }
                case ColorPickerColorMode.CMYK:
                {
                    CMYKColor color4 = new CMYKColor(this.Color);
                    color4.EditMode = this.EditMode;
                    this.ColorViewModel = color4;
                    break;
                }
                case ColorPickerColorMode.HLS:
                {
                    HLSColor color3 = new HLSColor(this.Color);
                    color3.EditMode = this.EditMode;
                    this.ColorViewModel = color3;
                    break;
                }
                case ColorPickerColorMode.HSB:
                {
                    DevExpress.Xpf.Editors.Internal.HSBColor color2 = new DevExpress.Xpf.Editors.Internal.HSBColor(this.Color);
                    color2.EditMode = this.EditMode;
                    this.ColorViewModel = color2;
                    break;
                }
                default:
                    break;
            }
            this.ColorViewModel.Do<ColorBase>(delegate (ColorBase x) {
                x.ColorChanged += new EventHandler<ColorViewModelValueChangedEventArgs>(this.ColorViewModelColorChanged);
            });
        }

        private void OnColorModeChangedInternal(ColorPickerColorMode oldValue, ColorPickerColorMode newValue)
        {
            this.OnColorModeChanged(oldValue, newValue);
        }

        private void OnEditModeChanged(DevExpress.Xpf.Editors.EditMode newValue)
        {
            this.ColorViewModel.Do<ColorBase>(x => x.EditMode = newValue);
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            this.SetupFocusedEditor();
        }

        private void OnHSBColorChangedInternal(DevExpress.Xpf.Editors.Internal.HSBColor oldValue, DevExpress.Xpf.Editors.Internal.HSBColor newValue)
        {
            this.updateColorLocker.DoLockedActionIfNotLocked(new Action(this.UpdateColor));
            this.UpdateViewModelColor();
            this.updateThumbsLocker.DoIfNotLocked(new Action(this.UpdateThumbs));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.UpdateHSBColor();
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs args)
        {
            this.InitTooltips();
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                System.Windows.Point mousePosition = GetMousePosition();
                System.Windows.Point location = this.colorArea.PointToScreen(new System.Windows.Point(0.0, 0.0));
                Rect rect = new Rect(location, this.colorArea.RenderSize);
                if (!rect.Contains(mousePosition))
                {
                    this.Color = this.ExcludeColor(this.GetColorFromPoint(mousePosition), transparentWindowBackgroundColor);
                    e.Handled = true;
                }
                else
                {
                    this.ActualXYThumbXOffset = (mousePosition.X - location.X) - 10.5;
                    this.ActualXYThumbYOffset = (mousePosition.Y - location.Y) - 10.5;
                    this.updateThumbsLocker.DoLockedAction(new Action(this.UpdateColorFromThumbs));
                    e.Handled = true;
                }
            }
            catch
            {
            }
        }

        protected virtual void OnShowEditorsChanged(bool oldValue, bool newValue)
        {
        }

        private void OnShowEditorsChangedInternal(bool oldValue, bool newValue)
        {
            this.UpdateActualShowEditors();
            this.OnShowEditorsChanged(oldValue, newValue);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateActualShowEditors();
        }

        private void OnTransparentWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.transparentWindow.Close();
            this.transparentWindow.ReleaseMouseCapture();
            this.transparentWindow = null;
            e.Handled = true;
            this.pippetUpdateColorLocker.Unlock();
            if (MouseHelper.IsMouseRightButtonPressed(e))
            {
                this.Color = this.pippetPreviousColor;
            }
            else
            {
                this.OnColorChanged(this.colorCache, this.Color);
                this.RaiseColorChanged(this.Color);
            }
        }

        private void OnZColorAreaLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsZThumbMoving = true;
            this.zColorArea.CaptureMouse();
            this.MoveZThumb(e);
        }

        private void OnZColorAreaLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.IsZThumbMoving = false;
            this.zColorArea.ReleaseMouseCapture();
        }

        private void OnZColorAreaMouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsZThumbMoving)
            {
                this.MoveZThumb(e);
            }
        }

        private void OnZColorAreaSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.updateThumbsLocker.DoLockedActionIfNotLocked(new Action(this.UpdateThumbs));
        }

        protected void RaiseColorChanged(System.Windows.Media.Color color)
        {
            base.RaiseEvent(new ColorChangedEventArgs(color));
        }

        private void SetDefaultColor()
        {
            this.Color = this.DefaultColor;
        }

        private void SetupFocusedEditor()
        {
            if (base.IsKeyboardFocused && (this.EditMode == DevExpress.Xpf.Editors.EditMode.Standalone))
            {
                this.FocusFirstEditor();
            }
        }

        protected virtual void SubscribeEvents()
        {
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.SizeChanged += new SizeChangedEventHandler(this.OnColorAreaSizeChanged);
            });
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnColorAreaLeftMouseButtonDown);
            });
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnColorAreaLeftMouseButtonUp);
            });
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseMove += new MouseEventHandler(this.OnColorAreaMouseMove);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.SizeChanged += new SizeChangedEventHandler(this.OnZColorAreaSizeChanged);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnZColorAreaLeftMouseButtonDown);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnZColorAreaLeftMouseButtonUp);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseMove += new MouseEventHandler(this.OnZColorAreaMouseMove);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.SizeChanged += new SizeChangedEventHandler(this.OnAlphaChannelAreaSizeChanged);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnAlphaChannelAreaLeftMouseButtonDown);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnAlphaChannelAreaLeftMouseButtonUp);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseMove += new MouseEventHandler(this.OnAlphaChannelAreaMouseMove);
            });
        }

        protected virtual void UnsubscribeEvents()
        {
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.SizeChanged -= new SizeChangedEventHandler(this.OnColorAreaSizeChanged);
            });
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonDown -= new MouseButtonEventHandler(this.OnColorAreaLeftMouseButtonDown);
            });
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonUp -= new MouseButtonEventHandler(this.OnColorAreaLeftMouseButtonUp);
            });
            this.colorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseMove -= new MouseEventHandler(this.OnColorAreaMouseMove);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.SizeChanged -= new SizeChangedEventHandler(this.OnZColorAreaSizeChanged);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonDown -= new MouseButtonEventHandler(this.OnZColorAreaLeftMouseButtonDown);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonUp -= new MouseButtonEventHandler(this.OnZColorAreaLeftMouseButtonUp);
            });
            this.zColorArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseMove -= new MouseEventHandler(this.OnZColorAreaMouseMove);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.SizeChanged += new SizeChangedEventHandler(this.OnAlphaChannelAreaSizeChanged);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnAlphaChannelAreaLeftMouseButtonDown);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnAlphaChannelAreaLeftMouseButtonUp);
            });
            this.alphaChannelArea.Do<Canvas>(delegate (Canvas x) {
                x.MouseMove += new MouseEventHandler(this.OnAlphaChannelAreaMouseMove);
            });
        }

        private void UpdateActualShowEditors()
        {
            this.ActualShowEditors = (base.ActualWidth >= 297.0) && this.ShowEditors;
        }

        private void UpdateColor()
        {
            this.Color = this.HSBColor.Color;
        }

        protected internal virtual void UpdateColorFromThumbs()
        {
            if ((this.colorArea != null) && (!this.colorArea.ActualWidth.IsZero() && (!this.colorArea.ActualHeight.IsZero() && ((this.zColorArea != null) && !this.zColorArea.ActualHeight.IsZero()))))
            {
                double actualWidth = this.colorArea.ActualWidth;
                double actualHeight = this.colorArea.ActualHeight;
                double num3 = this.zColorArea.ActualHeight;
                double num4 = this.alphaChannelArea.ActualWidth;
                int s = Convert.ToInt32((double) (((10.5 + this.ActualXYThumbXOffset) / actualWidth) * 100.0));
                int b = Math.Abs((int) (100 - Convert.ToInt32((double) (((10.5 + this.ActualXYThumbYOffset) / actualHeight) * 100.0))));
                int h = Convert.ToInt32((double) (((5.0 + this.ActualZThumbOffset) / num3) * 360.0));
                this.HSBColor = new DevExpress.Xpf.Editors.Internal.HSBColor(h, s, b, this.ShowAlphaChannel ? Convert.ToInt32((double) ((1.0 - ((5.0 + this.ActualAlphaThumbOffset) / num4)) * 255.0)) : this.HSBColor.A);
            }
        }

        private void UpdateHSBColor()
        {
            this.HSBColor = new DevExpress.Xpf.Editors.Internal.HSBColor(this.Color);
        }

        private void UpdateThumbs()
        {
            if (((this.HSBColor != null) && ((this.colorArea != null) && (this.zColorArea != null))) && (!this.colorArea.ActualWidth.IsZero() && (!this.colorArea.ActualHeight.IsZero() && !this.zColorArea.ActualHeight.IsZero())))
            {
                double actualWidth = this.colorArea.ActualWidth;
                double actualHeight = this.colorArea.ActualHeight;
                double num3 = this.zColorArea.ActualHeight;
                double num4 = this.alphaChannelArea.ActualWidth;
                this.ActualXYThumbXOffset = ((((double) this.HSBColor.S) / 100.0) * actualWidth) - 10.5;
                this.ActualXYThumbYOffset = ((((double) Math.Abs((int) (100 - this.HSBColor.B))) / 100.0) * actualHeight) - 10.5;
                this.ActualZThumbOffset = ((((double) this.HSBColor.H) / 360.0) * num3) - 5.0;
                if (this.ShowAlphaChannel)
                {
                    this.ActualAlphaThumbOffset = ((1.0 - (((double) this.HSBColor.A) / 255.0)) * num4) - 5.0;
                }
            }
        }

        private void UpdateViewModelColor()
        {
            this.ColorViewModel.Do<ColorBase>(x => x.Color = this.HSBColor.Color);
        }

        private double DpiScaleX =>
            ScreenHelper.GetScaleX(this);

        private double DpiScaleY =>
            this.DpiScaleX;

        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                (DevExpress.Xpf.Editors.EditMode) base.GetValue(EditModeProperty);
            set => 
                base.SetValue(EditModeProperty, value);
        }

        public System.Windows.Media.Color Color
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(ColorProperty);
            set => 
                base.SetValue(ColorProperty, value);
        }

        public System.Windows.Media.Color DefaultColor
        {
            get => 
                (System.Windows.Media.Color) base.GetValue(DefaultColorProperty);
            set => 
                base.SetValue(DefaultColorProperty, value);
        }

        public bool ShowDefaultColor
        {
            get => 
                (bool) base.GetValue(ShowDefaultColorProperty);
            set => 
                base.SetValue(ShowDefaultColorProperty, value);
        }

        public DevExpress.Xpf.Editors.Internal.HSBColor HSBColor
        {
            get => 
                (DevExpress.Xpf.Editors.Internal.HSBColor) base.GetValue(HSBColorProperty);
            private set => 
                base.SetValue(HSBColorPropertyKey, value);
        }

        public double ActualZThumbOffset
        {
            get => 
                (double) base.GetValue(ActualZThumbOffsetProperty);
            internal set => 
                base.SetValue(ActualZThumbOffsetPropertyKey, value);
        }

        public double ActualXYThumbXOffset
        {
            get => 
                (double) base.GetValue(ActualXYThumbXOffsetProperty);
            internal set => 
                base.SetValue(ActualXYThumbXOffsetPropertyKey, value);
        }

        public double ActualXYThumbYOffset
        {
            get => 
                (double) base.GetValue(ActualXYThumbYOffsetProperty);
            internal set => 
                base.SetValue(ActualXYThumbYOffsetPropertyKey, value);
        }

        public double ActualAlphaThumbOffset
        {
            get => 
                (double) base.GetValue(ActualAlphaThumbOffsetProperty);
            internal set => 
                base.SetValue(ActualAlphaThumbOffsetPropertyKey, value);
        }

        public bool ShowEditors
        {
            get => 
                (bool) base.GetValue(ShowEditorsProperty);
            set => 
                base.SetValue(ShowEditorsProperty, value);
        }

        public bool ActualShowEditors
        {
            get => 
                (bool) base.GetValue(ActualShowEditorsProperty);
            private set => 
                base.SetValue(ActualShowEditorsPropertyKey, value);
        }

        public ColorPickerColorMode ColorMode
        {
            get => 
                (ColorPickerColorMode) base.GetValue(ColorModeProperty);
            set => 
                base.SetValue(ColorModeProperty, value);
        }

        public ColorBase ColorViewModel
        {
            get => 
                (ColorBase) base.GetValue(ColorViewModelProperty);
            private set => 
                base.SetValue(ColorViewModelPropertyKey, value);
        }

        public bool ShowAlphaChannel
        {
            get => 
                (bool) base.GetValue(ShowAlphaChannelProperty);
            set => 
                base.SetValue(ShowAlphaChannelProperty, value);
        }

        public ICommand SetDefaultColorCommand { get; private set; }

        private bool IsXYThumbMoving { get; set; }

        private bool IsZThumbMoving { get; set; }

        private bool IsAlphaThumbMoving { get; set; }

        public ICommand EnablePippetModeCommand { get; private set; }

        object IColorEdit.EditValue
        {
            get => 
                this.Color;
            set
            {
            }
        }

        PaletteCollection IColorEdit.Palettes
        {
            get => 
                null;
            set
            {
            }
        }

        CircularList<System.Windows.Media.Color> IColorEdit.RecentColors =>
            null;

        private static Cursor EyeDropCursor { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColorPicker.<>c <>9 = new ColorPicker.<>c();
            public static Func<FrameworkElement, TextBlock> <>9__154_0;
            public static Action<TextBlock> <>9__154_1;
            public static Func<FrameworkElement, TextBlock> <>9__154_2;
            public static Action<TextBlock> <>9__154_3;
            public static Func<FrameworkElement, TextBlock> <>9__154_4;
            public static Action<TextBlock> <>9__154_5;
            public static Func<FrameworkElement, TextBlock> <>9__154_6;
            public static Action<TextBlock> <>9__154_7;
            public static Func<FrameworkElement, TextBlock> <>9__154_8;
            public static Action<TextBlock> <>9__154_9;
            public static Func<FrameworkElement, TextBlock> <>9__154_10;
            public static Action<TextBlock> <>9__154_11;
            public static Func<FrameworkElement, TextBlock> <>9__154_12;
            public static Action<TextBlock> <>9__154_13;
            public static Func<FrameworkElement, TextBlock> <>9__154_14;
            public static Action<TextBlock> <>9__154_15;
            public static Func<FrameworkElement, TextBlock> <>9__154_16;
            public static Action<TextBlock> <>9__154_17;
            public static Func<FrameworkElement, TextBlock> <>9__154_18;
            public static Action<TextBlock> <>9__154_19;
            public static Func<FrameworkElement, TextBlock> <>9__154_20;
            public static Action<TextBlock> <>9__154_21;
            public static Func<FrameworkElement, TextBlock> <>9__154_22;
            public static Action<TextBlock> <>9__154_23;
            public static Func<FrameworkElement, TextBlock> <>9__154_24;
            public static Action<TextBlock> <>9__154_25;
            public static Func<FrameworkElement, TextBlock> <>9__154_26;
            public static Action<TextBlock> <>9__154_27;

            internal void <.cctor>b__32_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((ColorPicker) obj).OnEditModeChanged((EditMode) args.NewValue);
            }

            internal void <.cctor>b__32_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((ColorPicker) obj).OnColorChangedInternal((System.Windows.Media.Color) args.OldValue, (System.Windows.Media.Color) args.NewValue);
            }

            internal void <.cctor>b__32_2(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((ColorPicker) obj).OnShowEditorsChangedInternal((bool) args.OldValue, (bool) args.NewValue);
            }

            internal void <.cctor>b__32_3(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((ColorPicker) obj).OnColorModeChangedInternal((ColorPickerColorMode) args.OldValue, (ColorPickerColorMode) args.NewValue);
            }

            internal void <.cctor>b__32_4(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((ColorPicker) obj).OnAllowTransparencyChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__32_5(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((ColorPicker) obj).OnActualShowEditorsChangedInternal((bool) args.OldValue, (bool) args.NewValue);
            }

            internal void <.cctor>b__32_6(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((ColorPicker) obj).OnHSBColorChangedInternal((HSBColor) args.OldValue, (HSBColor) args.NewValue);
            }

            internal TextBlock <InitTooltips>b__154_0(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_1(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerRed);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_10(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_11(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerBrightness);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_12(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_13(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerHue);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_14(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_15(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerLightness);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_16(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_17(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerSaturation);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_18(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_19(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerCyan);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_2(FrameworkElement x) => 
                x as TextBlock;

            internal TextBlock <InitTooltips>b__154_20(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_21(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerMagenta);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_22(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_23(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerYellow);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_24(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_25(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerKeyColor);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_26(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_27(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerAlpha);
                x.SetToolTip(toolTip);
            }

            internal void <InitTooltips>b__154_3(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerGreen);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_4(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_5(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerBlue);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_6(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_7(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerHue);
                x.SetToolTip(toolTip);
            }

            internal TextBlock <InitTooltips>b__154_8(FrameworkElement x) => 
                x as TextBlock;

            internal void <InitTooltips>b__154_9(TextBlock x)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = EditorLocalizer.GetString(EditorStringId.ColorPickerSaturation);
                x.SetToolTip(toolTip);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Win32Point
        {
            public int X;
            public int Y;
        }
    }
}

