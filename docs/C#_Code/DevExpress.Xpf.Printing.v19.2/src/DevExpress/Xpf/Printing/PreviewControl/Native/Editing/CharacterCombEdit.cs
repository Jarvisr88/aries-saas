namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.CharacterComb;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    [TemplatePart(Name="PART_Caret", Type=typeof(CharacterCombCaret)), TemplatePart(Name="PART_NativeImage", Type=typeof(NativeImage))]
    public class CharacterCombEdit : TextEdit, ICharacterComb
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public const string CharacterCombResourceKey = "DevExpress_Xpf_Printing_PredefinedCharacterCombResourceKey";
        public const string PART_Caret = "PART_Caret";
        public const string PART_Editor = "PART_Editor";
        public const string PART_NativeImage = "PART_NativeImage";
        private CharacterCombCaret caret;
        private NativeImage nativeImage;
        private PointF startDrag = PointF.Empty;
        private CharacterCombTextElement startElement = CharacterCombTextElement.Empty;
        public static readonly DependencyProperty ZoomProperty;
        public static readonly DependencyProperty CharacterCombInfoProperty;
        public static readonly DependencyProperty CharacterCombTextElementsProperty;
        public static readonly DependencyPropertyKey CharacterCombTextElementsPropertyKey;

        static CharacterCombEdit()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(CharacterCombEdit), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<CharacterCombEdit> registrator1 = DependencyPropertyRegistrator<CharacterCombEdit>.New().Register<DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo>(System.Linq.Expressions.Expression.Lambda<Func<CharacterCombEdit, DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CharacterCombEdit.get_CharacterCombInfo)), parameters), out CharacterCombInfoProperty, null, d => d.OnCharacterCombInfoChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CharacterCombEdit), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<CharacterCombEdit> registrator2 = registrator1.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<CharacterCombEdit, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CharacterCombEdit.get_Zoom)), expressionArray2), out ZoomProperty, 1.0, d => d.OnZoomChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(CharacterCombEdit), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.RegisterReadOnly<CharacterCombTextElement[,]>(System.Linq.Expressions.Expression.Lambda<Func<CharacterCombEdit, CharacterCombTextElement[,]>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(CharacterCombEdit.get_CharacterCombTextElements)), expressionArray3), out CharacterCombTextElementsPropertyKey, out CharacterCombTextElementsProperty, null, frameworkOptions).OverrideMetadata(FrameworkElement.CursorProperty, Cursors.IBeam, null, FrameworkPropertyMetadataOptions.None).OverrideDefaultStyleKey();
        }

        public CharacterCombEdit()
        {
            base.Cursor = Cursors.IBeam;
            this.CharacterCombTextElements = this.GetTextElements();
        }

        private CharacterCombTextElement[,] GetTextElements()
        {
            if (!base.IsLoaded || (this.CharacterCombInfo == null))
            {
                return new CharacterCombTextElement[0, 0];
            }
            string text = (base.EditValue != null) ? base.EditValue.ToString() : base.DisplayText;
            CharacterCombTextElement[,] textElementsData = new CharacterCombHelper(this.CharacterCombInfo).GetTextElementsData(new RectangleF(new PointF(0f, 0f), this.Brick.Size), text);
            CharacterCombTextElement[,] elementArray2 = textElementsData;
            if (textElementsData == null)
            {
                CharacterCombTextElement[,] local1 = textElementsData;
                elementArray2 = new CharacterCombTextElement[0, 0];
            }
            return elementArray2;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.nativeImage = base.GetTemplateChild("PART_NativeImage") as NativeImage;
            this.nativeImage.Do<NativeImage>(x => x.Renderer = new CharacterCombRenderer(this));
            this.caret = base.GetTemplateChild("PART_Caret") as CharacterCombCaret;
            this.caret.Do<CharacterCombCaret>(delegate (CharacterCombCaret x) {
                x.SetCharacterComb(this);
                Func<DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo, Color> evaluator = <>c.<>9__34_2;
                if (<>c.<>9__34_2 == null)
                {
                    Func<DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo, Color> local1 = <>c.<>9__34_2;
                    evaluator = <>c.<>9__34_2 = i => i.Style.BackColor;
                }
                x.BackColor = DrawingConverter.FromGdiColor(this.CharacterCombInfo.Return<DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo, Color>(evaluator, <>c.<>9__34_3 ??= () => Color.White));
            });
        }

        private void OnCharacterCombInfoChanged()
        {
            if ((base.EditCore != null) && (this.CharacterCombInfo != null))
            {
                this.UpdateAll();
            }
        }

        protected override void OnDisplayTextChanged(string displayText)
        {
            this.UpdateAll();
        }

        protected override void OnEditCoreAssigned()
        {
            base.OnEditCoreAssigned();
        }

        protected override void OnLoadedInternal()
        {
            base.OnLoadedInternal();
            this.UpdateAll();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            this.CharacterCombTextElements = this.GetTextElements();
            Action<NativeImage> action = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Action<NativeImage> local1 = <>c.<>9__41_0;
                action = <>c.<>9__41_0 = x => x.Invalidate();
            }
            this.nativeImage.Do<NativeImage>(action);
            Action<CharacterCombCaret> action2 = <>c.<>9__41_1;
            if (<>c.<>9__41_1 == null)
            {
                Action<CharacterCombCaret> local2 = <>c.<>9__41_1;
                action2 = <>c.<>9__41_1 = x => x.Update();
            }
            this.caret.Do<CharacterCombCaret>(action2);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            System.Windows.Point position = e.GetPosition(this);
            PointF pt = PSUnitConverter.PixelToDoc(new PointF((float) position.X, (float) position.Y), (float) this.Zoom);
            CharacterCombTextElement empty = CharacterCombTextElement.Empty;
            CharacterCombTextElement element2 = CharacterCombTextElement.Empty;
            CharacterCombTextElement[,] characterCombTextElements = this.CharacterCombTextElements;
            int upperBound = characterCombTextElements.GetUpperBound(0);
            int num2 = characterCombTextElements.GetUpperBound(1);
            int lowerBound = characterCombTextElements.GetLowerBound(0);
            while (true)
            {
                if (lowerBound > upperBound)
                {
                    break;
                }
                int num4 = characterCombTextElements.GetLowerBound(1);
                while (true)
                {
                    if (num4 <= num2)
                    {
                        CharacterCombTextElement element3 = characterCombTextElements[lowerBound, num4];
                        RectangleF rect = element3.Rect;
                        if (rect.Contains(pt))
                        {
                            CharacterCombTextElement element4 = element3.IsEmpty ? empty : element3;
                            if (!element4.IsEmpty)
                            {
                                this.startDrag = pt;
                                this.startElement = element4;
                                base.SelectionStart = element4.TextIndex;
                                base.SelectionLength = 0;
                                Action<NativeImage> action = <>c.<>9__42_0;
                                if (<>c.<>9__42_0 == null)
                                {
                                    Action<NativeImage> local1 = <>c.<>9__42_0;
                                    action = <>c.<>9__42_0 = x => x.Invalidate();
                                }
                                this.nativeImage.Do<NativeImage>(action);
                                Action<CharacterCombCaret> action2 = <>c.<>9__42_1;
                                if (<>c.<>9__42_1 == null)
                                {
                                    Action<CharacterCombCaret> local2 = <>c.<>9__42_1;
                                    action2 = <>c.<>9__42_1 = x => x.Update();
                                }
                                this.caret.Do<CharacterCombCaret>(action2);
                                element2 = element3;
                                break;
                            }
                        }
                        else if (element3.TextIndex >= 0)
                        {
                            empty = element3;
                        }
                        num4++;
                        continue;
                    }
                    else
                    {
                        lowerBound++;
                    }
                    break;
                }
            }
            e.Handled = true;
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if ((e.LeftButton == MouseButtonState.Pressed) && !this.startElement.IsEmpty)
            {
                System.Windows.Point position = e.GetPosition(this);
                PointF pt = PSUnitConverter.PixelToDoc(new PointF((float) position.X, (float) position.Y), (float) this.Zoom);
                CharacterCombTextElement empty = CharacterCombTextElement.Empty;
                CharacterCombTextElement[,] characterCombTextElements = this.CharacterCombTextElements;
                int upperBound = characterCombTextElements.GetUpperBound(0);
                int num2 = characterCombTextElements.GetUpperBound(1);
                int lowerBound = characterCombTextElements.GetLowerBound(0);
                while (true)
                {
                    if (lowerBound > upperBound)
                    {
                        break;
                    }
                    int num4 = characterCombTextElements.GetLowerBound(1);
                    while (true)
                    {
                        if (num4 <= num2)
                        {
                            CharacterCombTextElement element2 = characterCombTextElements[lowerBound, num4];
                            RectangleF rect = element2.Rect;
                            if (!rect.Contains(pt) || element2.IsEmpty)
                            {
                                num4++;
                                continue;
                            }
                            if ((element2.TextIndex == this.startElement.TextIndex) && (Math.Abs((float) (pt.X - this.startDrag.X)) > (element2.Rect.Width / 3f)))
                            {
                                base.SelectionStart = this.startElement.TextIndex;
                                base.SelectionLength = 1;
                            }
                            else if (element2.TextIndex > this.startElement.TextIndex)
                            {
                                base.SelectionStart = this.startElement.TextIndex;
                                base.SelectionLength = (element2.TextIndex - this.startElement.TextIndex) + 1;
                            }
                            else if (element2.TextIndex < this.startElement.TextIndex)
                            {
                                base.SelectionStart = element2.TextIndex;
                                base.SelectionLength = (this.startElement.TextIndex - element2.TextIndex) + 1;
                            }
                            Action<NativeImage> action = <>c.<>9__43_0;
                            if (<>c.<>9__43_0 == null)
                            {
                                Action<NativeImage> local1 = <>c.<>9__43_0;
                                action = <>c.<>9__43_0 = x => x.Invalidate();
                            }
                            this.nativeImage.Do<NativeImage>(action);
                            Action<CharacterCombCaret> action2 = <>c.<>9__43_1;
                            if (<>c.<>9__43_1 == null)
                            {
                                Action<CharacterCombCaret> local2 = <>c.<>9__43_1;
                                action2 = <>c.<>9__43_1 = x => x.Update();
                            }
                            this.caret.Do<CharacterCombCaret>(action2);
                            empty = element2;
                            break;
                        }
                        else
                        {
                            lowerBound++;
                        }
                        break;
                    }
                }
                e.Handled = true;
            }
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            this.startDrag = PointF.Empty;
            this.startElement = CharacterCombTextElement.Empty;
            Action<NativeImage> action = <>c.<>9__44_0;
            if (<>c.<>9__44_0 == null)
            {
                Action<NativeImage> local1 = <>c.<>9__44_0;
                action = <>c.<>9__44_0 = x => x.Invalidate();
            }
            this.nativeImage.Do<NativeImage>(action);
            Action<CharacterCombCaret> action2 = <>c.<>9__44_1;
            if (<>c.<>9__44_1 == null)
            {
                Action<CharacterCombCaret> local2 = <>c.<>9__44_1;
                action2 = <>c.<>9__44_1 = x => x.Update();
            }
            this.caret.Do<CharacterCombCaret>(action2);
            e.Handled = true;
        }

        private void OnZoomChanged()
        {
            this.UpdateAll();
        }

        private void UpdateAll()
        {
            this.CharacterCombTextElements = this.GetTextElements();
            Action<NativeImage> action = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Action<NativeImage> local1 = <>c.<>9__36_0;
                action = <>c.<>9__36_0 = x => x.Invalidate();
            }
            this.nativeImage.Do<NativeImage>(action);
            Action<CharacterCombCaret> action2 = <>c.<>9__36_1;
            if (<>c.<>9__36_1 == null)
            {
                Action<CharacterCombCaret> local2 = <>c.<>9__36_1;
                action2 = <>c.<>9__36_1 = x => x.Update();
            }
            this.caret.Do<CharacterCombCaret>(action2);
        }

        string ICharacterComb.Text =>
            base.DisplayText;

        public VisualBrick Brick { get; set; }

        public DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo CharacterCombInfo
        {
            get => 
                (DevExpress.XtraPrinting.Native.CharacterComb.CharacterCombInfo) base.GetValue(CharacterCombInfoProperty);
            set => 
                base.SetValue(CharacterCombInfoProperty, value);
        }

        public CharacterCombTextElement[,] CharacterCombTextElements
        {
            get => 
                (CharacterCombTextElement[,]) base.GetValue(CharacterCombTextElementsProperty);
            private set => 
                base.SetValue(CharacterCombTextElementsPropertyKey, value);
        }

        public double Zoom
        {
            get => 
                (double) base.GetValue(ZoomProperty);
            set => 
                base.SetValue(ZoomProperty, value);
        }

        public int PageIndex { get; set; }

        int ICharacterComb.SelectionStart =>
            base.SelectionStart;

        int ICharacterComb.SelectionLength =>
            base.SelectionLength;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CharacterCombEdit.<>c <>9 = new CharacterCombEdit.<>c();
            public static Func<CharacterCombInfo, Color> <>9__34_2;
            public static Func<Color> <>9__34_3;
            public static Action<NativeImage> <>9__36_0;
            public static Action<CharacterCombCaret> <>9__36_1;
            public static Action<NativeImage> <>9__41_0;
            public static Action<CharacterCombCaret> <>9__41_1;
            public static Action<NativeImage> <>9__42_0;
            public static Action<CharacterCombCaret> <>9__42_1;
            public static Action<NativeImage> <>9__43_0;
            public static Action<CharacterCombCaret> <>9__43_1;
            public static Action<NativeImage> <>9__44_0;
            public static Action<CharacterCombCaret> <>9__44_1;

            internal void <.cctor>b__31_0(CharacterCombEdit d)
            {
                d.OnCharacterCombInfoChanged();
            }

            internal void <.cctor>b__31_1(CharacterCombEdit d)
            {
                d.OnZoomChanged();
            }

            internal Color <OnApplyTemplate>b__34_2(CharacterCombInfo i) => 
                i.Style.BackColor;

            internal Color <OnApplyTemplate>b__34_3() => 
                Color.White;

            internal void <OnPreviewKeyDown>b__41_0(NativeImage x)
            {
                x.Invalidate();
            }

            internal void <OnPreviewKeyDown>b__41_1(CharacterCombCaret x)
            {
                x.Update();
            }

            internal void <OnPreviewMouseDown>b__42_0(NativeImage x)
            {
                x.Invalidate();
            }

            internal void <OnPreviewMouseDown>b__42_1(CharacterCombCaret x)
            {
                x.Update();
            }

            internal void <OnPreviewMouseMove>b__43_0(NativeImage x)
            {
                x.Invalidate();
            }

            internal void <OnPreviewMouseMove>b__43_1(CharacterCombCaret x)
            {
                x.Update();
            }

            internal void <OnPreviewMouseUp>b__44_0(NativeImage x)
            {
                x.Invalidate();
            }

            internal void <OnPreviewMouseUp>b__44_1(CharacterCombCaret x)
            {
                x.Update();
            }

            internal void <UpdateAll>b__36_0(NativeImage x)
            {
                x.Invalidate();
            }

            internal void <UpdateAll>b__36_1(CharacterCombCaret x)
            {
                x.Update();
            }
        }
    }
}

