namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [Browsable(false)]
    public class Items2Panel : Panel
    {
        public static readonly DependencyProperty Content1Property;
        public static readonly DependencyProperty Content2Property;
        public static readonly DependencyProperty Content1PaddingProperty;
        public static readonly DependencyProperty Content2PaddingProperty;
        public static readonly DependencyProperty VerticalPaddingProperty;
        public static readonly DependencyProperty HorizontalPaddingProperty;
        public static readonly DependencyProperty VerticalIndentProperty;
        public static readonly DependencyProperty HorizontalIndentProperty;
        public static readonly DependencyProperty AlignmentProperty;
        public static readonly DependencyProperty FillContent1Property;
        public static readonly DependencyProperty FillContent2Property;
        public static readonly DependencyProperty EmptyPaddingProperty;
        public static readonly DependencyProperty StretchedContentProperty;
        public static readonly DependencyProperty IgnoreContent2HorizontalAlignmentProperty;

        static Items2Panel()
        {
            Content1Property = DependencyPropertyManager.Register("Content1", typeof(UIElement), typeof(Items2Panel), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((Items2Panel) d).OnContent1Changed(e)));
            Content2Property = DependencyPropertyManager.Register("Content2", typeof(UIElement), typeof(Items2Panel), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((Items2Panel) d).OnContent2Changed(e)));
            Content1PaddingProperty = DependencyPropertyManager.Register("Content1Padding", typeof(Thickness), typeof(Items2Panel), new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure));
            Content2PaddingProperty = DependencyPropertyManager.Register("Content2Padding", typeof(Thickness), typeof(Items2Panel), new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure));
            VerticalPaddingProperty = DependencyPropertyManager.Register("VerticalPadding", typeof(Thickness), typeof(Items2Panel), new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure));
            HorizontalPaddingProperty = DependencyPropertyManager.Register("HorizontalPadding", typeof(Thickness), typeof(Items2Panel), new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure));
            VerticalIndentProperty = DependencyPropertyManager.Register("VerticalIndent", typeof(double), typeof(Items2Panel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            HorizontalIndentProperty = DependencyPropertyManager.Register("HorizontalIndent", typeof(double), typeof(Items2Panel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            AlignmentProperty = DependencyPropertyManager.Register("Alignment", typeof(Dock), typeof(Items2Panel), new FrameworkPropertyMetadata(Dock.Left, FrameworkPropertyMetadataOptions.AffectsMeasure));
            FillContent1Property = DependencyPropertyManager.Register("FillContent1", typeof(bool), typeof(Items2Panel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));
            FillContent2Property = DependencyPropertyManager.Register("FillContent2", typeof(bool), typeof(Items2Panel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));
            EmptyPaddingProperty = DependencyPropertyManager.Register("EmptyPadding", typeof(Thickness), typeof(Items2Panel), new FrameworkPropertyMetadata(new Thickness(0.0), FrameworkPropertyMetadataOptions.AffectsMeasure));
            StretchedContentProperty = DependencyPropertyManager.Register("StretchedContent", typeof(StretchedContentType), typeof(Items2Panel), new FrameworkPropertyMetadata(StretchedContentType.None, FrameworkPropertyMetadataOptions.AffectsMeasure));
            IgnoreContent2HorizontalAlignmentProperty = DependencyPropertyManager.Register("IgnoreContent2HorizontalAlignment", typeof(bool), typeof(Items2Panel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = this.CalcBestSize();
            if (base.VerticalAlignment == VerticalAlignment.Stretch)
            {
                size.Height = finalSize.Height;
            }
            if (base.HorizontalAlignment == HorizontalAlignment.Stretch)
            {
                size.Width = finalSize.Width;
            }
            Rect finalRect = new Rect(new Point(0.0, 0.0), size);
            Thickness actualPadding = this.GetActualPadding();
            finalRect = new Rect(actualPadding.Left, actualPadding.Top, Math.Max((double) 0.0, (double) ((finalRect.Width - actualPadding.Left) - actualPadding.Right)), Math.Max((double) 0.0, (double) ((finalRect.Height - actualPadding.Bottom) - actualPadding.Top)));
            if (this.HasContent1 && !this.HasContent2)
            {
                this.Content1.Arrange(finalRect);
                return size;
            }
            if (!this.HasContent1 && this.HasContent2)
            {
                this.Content2.Arrange(finalRect);
                return size;
            }
            if (this.HasContent1 && this.HasContent2)
            {
                Rect rect2;
                Rect rect3;
                if (this.Alignment == Dock.Top)
                {
                    rect2 = new Rect(new Point(finalRect.X + ((finalRect.Width - this.Content1.DesiredSize.Width) / 2.0), finalRect.Top), this.Content1.DesiredSize);
                    rect3 = new Rect(new Point(finalRect.X + ((finalRect.Width - this.Content2.DesiredSize.Width) / 2.0), rect2.Bottom + this.GetActualIndent()), this.Content2.DesiredSize);
                }
                else if (this.Alignment == Dock.Bottom)
                {
                    rect3 = new Rect(new Point(finalRect.X + ((finalRect.Width - this.Content2.DesiredSize.Width) / 2.0), finalRect.Top), this.Content2.DesiredSize);
                    rect2 = new Rect(new Point(finalRect.X + ((finalRect.Width - this.Content1.DesiredSize.Width) / 2.0), rect3.Bottom + this.GetActualIndent()), this.Content1.DesiredSize);
                }
                else if (this.Alignment == Dock.Left)
                {
                    rect2 = new Rect(new Point(finalRect.X, finalRect.Y + ((finalRect.Height - this.Content1.DesiredSize.Height) / 2.0)), this.Content1.DesiredSize);
                    rect3 = new Rect(new Point(rect2.Right + this.GetActualIndent(), finalRect.Y + ((finalRect.Height - this.Content2.DesiredSize.Height) / 2.0)), this.Content2.DesiredSize);
                }
                else
                {
                    rect3 = new Rect(new Point(finalRect.X, finalRect.Y + ((finalRect.Height - this.Content2.DesiredSize.Height) / 2.0)), this.Content2.DesiredSize);
                    rect2 = new Rect(new Point(rect3.Right + this.GetActualIndent(), finalRect.Y + ((finalRect.Height - this.Content1.DesiredSize.Height) / 2.0)), this.Content1.DesiredSize);
                }
                this.CorrectBoundsByFinalRect(finalRect, ref rect2, ref rect3);
                this.StretchContentByFillType(finalRect, ref rect2, ref rect3);
                this.StretchContentStretchContentType(finalRect, ref rect2, ref rect3);
                this.StretchContentVertical(finalRect, ref rect2, ref rect3);
                this.Content1.Arrange(rect2);
                this.Content2.Arrange(rect3);
            }
            return size;
        }

        protected virtual unsafe Size CalcBestSize()
        {
            Size size = new Size(0.0, 0.0);
            Size desiredSize = new Size(0.0, 0.0);
            Size desiredSize = new Size(0.0, 0.0);
            if (this.HasContent1)
            {
                desiredSize = this.Content1.DesiredSize;
            }
            if (this.HasContent2)
            {
                desiredSize = this.Content2.DesiredSize;
            }
            if ((this.Alignment == Dock.Top) || (this.Alignment == Dock.Bottom))
            {
                size.Width = Math.Max(desiredSize.Width, desiredSize.Width);
                Size* sizePtr1 = &size;
                sizePtr1.Height += desiredSize.Height + desiredSize.Height;
                Size* sizePtr2 = &size;
                sizePtr2.Height += this.GetActualIndent();
            }
            else if ((this.Alignment == Dock.Left) || (this.Alignment == Dock.Right))
            {
                size.Height = Math.Max(desiredSize.Height, desiredSize.Height);
                Size* sizePtr3 = &size;
                sizePtr3.Width += desiredSize.Width + desiredSize.Width;
                Size* sizePtr4 = &size;
                sizePtr4.Width += this.GetActualIndent();
            }
            Thickness actualPadding = this.GetActualPadding();
            Size* sizePtr5 = &size;
            sizePtr5.Width += actualPadding.Left + actualPadding.Right;
            Size* sizePtr6 = &size;
            sizePtr6.Height += actualPadding.Top + actualPadding.Bottom;
            return size;
        }

        private void CorrectBoundsByFinalRect(Rect finalRect, ref Rect c1Rect, ref Rect c2Rect)
        {
            if ((this.Content2 is FrameworkElement) && (this.Alignment == Dock.Left))
            {
                if (this.IgnoreContent2HorizontalAlignment)
                {
                    if (((FrameworkElement) this.Content2).HorizontalAlignment == HorizontalAlignment.Right)
                    {
                        c2Rect.Width = finalRect.Width - c2Rect.X;
                    }
                }
                else
                {
                    if (((FrameworkElement) this.Content2).HorizontalAlignment == HorizontalAlignment.Right)
                    {
                        c2Rect.X = finalRect.Width - c2Rect.Width;
                    }
                    if (((FrameworkElement) this.Content2).HorizontalAlignment == HorizontalAlignment.Stretch)
                    {
                        c2Rect.Width = finalRect.Right - c2Rect.X;
                    }
                    if (((FrameworkElement) this.Content2).HorizontalAlignment == HorizontalAlignment.Center)
                    {
                        c2Rect.X += (finalRect.Right - c2Rect.Right) / 2.0;
                    }
                }
            }
        }

        protected virtual double GetActualIndent() => 
            (!this.HasContent1 || !this.HasContent2) ? 0.0 : (((this.Alignment == Dock.Top) || (this.Alignment == Dock.Bottom)) ? (((this.Content1.DesiredSize.Height == 0.0) || (this.Content2.DesiredSize.Height == 0.0)) ? 0.0 : this.VerticalIndent) : (((this.Content1.DesiredSize.Width == 0.0) || (this.Content2.DesiredSize.Width == 0.0)) ? 0.0 : this.HorizontalIndent));

        protected virtual Thickness GetActualPadding()
        {
            if ((!this.HasContent1 || (this.Content1.DesiredSize == new Size(0.0, 0.0))) && (!this.HasContent2 || (this.Content2.DesiredSize == new Size(0.0, 0.0))))
            {
                return this.EmptyPadding;
            }
            if (this.HasContent1)
            {
                if (!this.HasContent2)
                {
                    return this.Content1Padding;
                }
                if (this.IsVerticalAlignment && (this.Content2.DesiredSize.Height == 0.0))
                {
                    return this.Content1Padding;
                }
                if (this.Content2.DesiredSize.Width == 0.0)
                {
                    return ((this.Content1.DesiredSize.Width != 0.0) ? this.Content1Padding : ((this.Content1.DesiredSize.Height == 0.0) ? new Thickness(0.0) : new Thickness(0.0, this.Content1Padding.Top, 0.0, this.Content1Padding.Bottom)));
                }
            }
            if (this.HasContent2)
            {
                if (!this.HasContent1)
                {
                    return this.Content2Padding;
                }
                if (this.IsVerticalAlignment && (this.Content1.DesiredSize.Height == 0.0))
                {
                    return this.Content2Padding;
                }
                if (this.Content1.DesiredSize.Width == 0.0)
                {
                    return ((this.Content2.DesiredSize.Width != 0.0) ? this.Content2Padding : new Thickness(0.0));
                }
            }
            return (!this.IsVerticalAlignment ? this.HorizontalPadding : this.VerticalPadding);
        }

        protected virtual void GetContentPaddings(ref Thickness content1Padding, ref Thickness content2Padding)
        {
            content1Padding = new Thickness();
            content2Padding = new Thickness();
            if (!this.HasContent1 && !this.HasContent2)
            {
                content1Padding = this.EmptyPadding;
            }
            else if (this.HasContent1 && !this.HasContent2)
            {
                content1Padding = this.Content1Padding;
            }
            else if (!this.HasContent1 && this.HasContent2)
            {
                content2Padding = this.Content2Padding;
            }
            else if ((this.Alignment != Dock.Bottom) && (this.Alignment != Dock.Top))
            {
                if (this.Alignment == Dock.Left)
                {
                    content1Padding = new Thickness(this.HorizontalPadding.Left, this.HorizontalPadding.Top, this.HorizontalIndent, this.HorizontalPadding.Bottom);
                    content2Padding = new Thickness(0.0, this.HorizontalPadding.Top, this.HorizontalPadding.Right, this.HorizontalPadding.Bottom);
                }
                else
                {
                    content1Padding = new Thickness(0.0, this.HorizontalPadding.Top, this.HorizontalPadding.Right, this.HorizontalPadding.Bottom);
                    content2Padding = new Thickness(this.HorizontalPadding.Left, this.HorizontalPadding.Top, this.HorizontalIndent, this.HorizontalPadding.Bottom);
                }
            }
            else if (this.Alignment == Dock.Top)
            {
                content1Padding = new Thickness(this.VerticalPadding.Left, this.VerticalPadding.Top, this.VerticalPadding.Right, this.VerticalIndent);
                content2Padding = new Thickness(this.VerticalPadding.Left, 0.0, this.VerticalPadding.Right, this.VerticalPadding.Bottom);
            }
            else
            {
                content1Padding = new Thickness(this.VerticalPadding.Left, 0.0, this.VerticalPadding.Right, this.VerticalPadding.Bottom);
                content2Padding = new Thickness(this.VerticalPadding.Left, this.VerticalPadding.Top, this.VerticalPadding.Right, this.VerticalIndent);
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = availableSize;
            Thickness thickness = new Thickness();
            Thickness thickness2 = new Thickness();
            this.GetContentPaddings(ref thickness, ref thickness2);
            if (!double.IsPositiveInfinity(size.Width))
            {
                size.Width = Math.Max((double) 0.0, (double) ((size.Width - thickness.Left) - thickness.Right));
            }
            if (!double.IsPositiveInfinity(size.Height))
            {
                size.Height = Math.Max((double) 0.0, (double) ((size.Height - thickness.Top) - thickness.Bottom));
            }
            if (this.Content1 != null)
            {
                this.Content1.Measure(size);
            }
            if (!double.IsPositiveInfinity(size.Width))
            {
                size.Width = Math.Max((double) 0.0, (double) ((size.Width - thickness2.Left) - thickness2.Right));
            }
            if (!double.IsPositiveInfinity(size.Height))
            {
                size.Height = Math.Max((double) 0.0, (double) ((size.Height - thickness2.Top) - thickness2.Bottom));
            }
            if (this.Content2 != null)
            {
                this.Content2.Measure(size);
            }
            return this.CalcBestSize();
        }

        protected virtual void OnContent1Changed(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                base.Children.Remove((UIElement) e.OldValue);
            }
            if (e.NewValue != null)
            {
                base.Children.Insert(0, (UIElement) e.NewValue);
            }
        }

        protected virtual void OnContent2Changed(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                base.Children.Remove((UIElement) e.OldValue);
            }
            if (e.NewValue != null)
            {
                base.Children.Add((UIElement) e.NewValue);
            }
        }

        private void StretchContentByFillType(Rect finalRect, ref Rect c1Rect, ref Rect c2Rect)
        {
            if ((this.Alignment != Dock.Top) && (this.Alignment != Dock.Bottom))
            {
                if (this.FillContent1)
                {
                    c1Rect.Height = finalRect.Height;
                    c1Rect.Y = finalRect.Y;
                }
                if (this.FillContent2)
                {
                    c2Rect.Height = finalRect.Height;
                    c2Rect.Y = finalRect.Y;
                }
            }
            else
            {
                if (this.FillContent1)
                {
                    c1Rect.Width = finalRect.Width;
                    c1Rect.X = finalRect.X;
                }
                if (this.FillContent2)
                {
                    c2Rect.Width = finalRect.Width;
                    c2Rect.X = finalRect.X;
                }
            }
        }

        private void StretchContentStretchContentType(Rect finalRect, ref Rect c1Rect, ref Rect c2Rect)
        {
            if (this.StretchedContent != StretchedContentType.None)
            {
                if ((this.Alignment != Dock.Top) && (this.Alignment != Dock.Bottom))
                {
                    if (this.StretchedContent == StretchedContentType.Content1)
                    {
                        c1Rect.X = finalRect.X;
                        c2Rect.X = (finalRect.X + finalRect.Width) - c2Rect.Width;
                        c1Rect.Width = c2Rect.X - finalRect.X;
                    }
                    else
                    {
                        c2Rect.X = finalRect.X;
                        c1Rect.X = (finalRect.X + finalRect.Width) - c1Rect.Width;
                        c2Rect.Width = c1Rect.X - finalRect.X;
                    }
                }
                else if (this.StretchedContent == StretchedContentType.Content1)
                {
                    c1Rect.Y = finalRect.Y;
                    c2Rect.Y = (finalRect.Y + finalRect.Height) - c2Rect.Height;
                    c1Rect.Height = c2Rect.Y - finalRect.Y;
                }
                else
                {
                    c2Rect.Y = finalRect.Y;
                    c1Rect.Y = (finalRect.Y + finalRect.Height) - c1Rect.Height;
                    c2Rect.Height = c1Rect.Y - finalRect.Y;
                }
            }
        }

        private void StretchContentVertical(Rect finalRect, ref Rect c1Rect, ref Rect c2Rect)
        {
            FrameworkElement element = this.Content1 as FrameworkElement;
            FrameworkElement element2 = this.Content1 as FrameworkElement;
            if ((element != null) && (element2 != null))
            {
                if ((element.VerticalAlignment == VerticalAlignment.Stretch) && (this.Alignment == Dock.Top))
                {
                    c1Rect.Height = Math.Max((double) (finalRect.Height - c2Rect.Height), (double) 0.0);
                    c2Rect.Y = finalRect.Y + c1Rect.Height;
                }
                else if ((element.VerticalAlignment == VerticalAlignment.Stretch) && (this.Alignment == Dock.Bottom))
                {
                    c1Rect.Height = Math.Max((double) (finalRect.Height - c2Rect.Height), (double) 0.0);
                }
                else if ((element2.VerticalAlignment == VerticalAlignment.Stretch) && (this.Alignment == Dock.Top))
                {
                    c2Rect.Height = Math.Max((double) (finalRect.Height - c1Rect.Height), (double) 0.0);
                }
                else if ((element2.VerticalAlignment == VerticalAlignment.Stretch) && (this.Alignment == Dock.Bottom))
                {
                    c2Rect.Height = Math.Max((double) (finalRect.Height - c1Rect.Height), (double) 0.0);
                    c1Rect.Y = finalRect.Y + c2Rect.Height;
                }
            }
        }

        public UIElement Content1
        {
            get => 
                (UIElement) base.GetValue(Content1Property);
            set => 
                base.SetValue(Content1Property, value);
        }

        public UIElement Content2
        {
            get => 
                (UIElement) base.GetValue(Content2Property);
            set => 
                base.SetValue(Content2Property, value);
        }

        public Thickness EmptyPadding
        {
            get => 
                (Thickness) base.GetValue(EmptyPaddingProperty);
            set => 
                base.SetValue(EmptyPaddingProperty, value);
        }

        public Thickness Content1Padding
        {
            get => 
                (Thickness) base.GetValue(Content1PaddingProperty);
            set => 
                base.SetValue(Content1PaddingProperty, value);
        }

        public Thickness Content2Padding
        {
            get => 
                (Thickness) base.GetValue(Content2PaddingProperty);
            set => 
                base.SetValue(Content2PaddingProperty, value);
        }

        public Thickness VerticalPadding
        {
            get => 
                (Thickness) base.GetValue(VerticalPaddingProperty);
            set => 
                base.SetValue(VerticalPaddingProperty, value);
        }

        public Thickness HorizontalPadding
        {
            get => 
                (Thickness) base.GetValue(HorizontalPaddingProperty);
            set => 
                base.SetValue(HorizontalPaddingProperty, value);
        }

        public double VerticalIndent
        {
            get => 
                (double) base.GetValue(VerticalIndentProperty);
            set => 
                base.SetValue(VerticalIndentProperty, value);
        }

        public double HorizontalIndent
        {
            get => 
                (double) base.GetValue(HorizontalIndentProperty);
            set => 
                base.SetValue(HorizontalIndentProperty, value);
        }

        public Dock Alignment
        {
            get => 
                (Dock) base.GetValue(AlignmentProperty);
            set => 
                base.SetValue(AlignmentProperty, value);
        }

        public bool FillContent1
        {
            get => 
                (bool) base.GetValue(FillContent1Property);
            set => 
                base.SetValue(FillContent1Property, value);
        }

        public bool FillContent2
        {
            get => 
                (bool) base.GetValue(FillContent2Property);
            set => 
                base.SetValue(FillContent2Property, value);
        }

        public StretchedContentType StretchedContent
        {
            get => 
                (StretchedContentType) base.GetValue(StretchedContentProperty);
            set => 
                base.SetValue(StretchedContentProperty, value);
        }

        public bool IgnoreContent2HorizontalAlignment
        {
            get => 
                (bool) base.GetValue(IgnoreContent2HorizontalAlignmentProperty);
            set => 
                base.SetValue(IgnoreContent2HorizontalAlignmentProperty, value);
        }

        public virtual bool HasContent1 =>
            (this.Content1 != null) && (this.Content1.Visibility != Visibility.Collapsed);

        public virtual bool HasContent2 =>
            (this.Content2 != null) && (this.Content2.Visibility != Visibility.Collapsed);

        private bool IsVerticalAlignment =>
            (this.Alignment == Dock.Top) || (this.Alignment == Dock.Bottom);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Items2Panel.<>c <>9 = new Items2Panel.<>c();

            internal void <.cctor>b__75_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Items2Panel) d).OnContent1Changed(e);
            }

            internal void <.cctor>b__75_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Items2Panel) d).OnContent2Changed(e);
            }
        }
    }
}

