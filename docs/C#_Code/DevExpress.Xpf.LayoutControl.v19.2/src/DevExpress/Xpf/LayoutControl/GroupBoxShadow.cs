namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class GroupBoxShadow : Container
    {
        public static readonly DependencyProperty CornerRadiusProperty;
        public static readonly DependencyProperty OffsetProperty;
        protected double StartOpacity = 0.07;
        protected double EndOpacity = 0.35;

        static GroupBoxShadow()
        {
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(System.Windows.CornerRadius), typeof(GroupBoxShadow), new PropertyMetadata((o, e) => ((GroupBoxShadow) o).OnCornerRadiusChanged()));
            OffsetProperty = DependencyProperty.Register("Offset", typeof(double), typeof(GroupBoxShadow), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                if (((double) e.NewValue) < 0.0)
                {
                    o.SetValue(e.Property, 0.0);
                }
                else
                {
                    ((GroupBoxShadow) o).OnOffsetChanged();
                }
            }));
        }

        public GroupBoxShadow()
        {
            base.IsHitTestVisible = false;
        }

        protected virtual FrameworkElement CreateElement(double margin, double opacity)
        {
            Border border = new Border {
                Background = new SolidColorBrush(Colors.Black)
            };
            Binding binding = new Binding("CornerRadius");
            binding.Source = this;
            border.SetBinding(Border.CornerRadiusProperty, binding);
            border.Margin = new Thickness(margin);
            border.Opacity = opacity;
            return border;
        }

        protected virtual void CreateElements()
        {
            base.Children.Clear();
            double num = 1.0;
            for (int i = 0; i < this.Offset; i++)
            {
                double opacity = 1.0 - ((1.0 - (this.StartOpacity + (((0.5 + i) * (this.EndOpacity - this.StartOpacity)) / this.Offset))) / num);
                num *= 1.0 - opacity;
                base.Children.Add(this.CreateElement((double) i, opacity));
            }
        }

        protected virtual void OnCornerRadiusChanged()
        {
        }

        protected virtual void OnOffsetChanged()
        {
            TranslateTransform transform1 = new TranslateTransform();
            transform1.X = this.Offset;
            transform1.Y = this.Offset;
            base.RenderTransform = transform1;
            this.CreateElements();
        }

        public System.Windows.CornerRadius CornerRadius
        {
            get => 
                (System.Windows.CornerRadius) base.GetValue(CornerRadiusProperty);
            set => 
                base.SetValue(CornerRadiusProperty, value);
        }

        public double Offset
        {
            get => 
                (double) base.GetValue(OffsetProperty);
            set => 
                base.SetValue(OffsetProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupBoxShadow.<>c <>9 = new GroupBoxShadow.<>c();

            internal void <.cctor>b__15_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((GroupBoxShadow) o).OnCornerRadiusChanged();
            }

            internal void <.cctor>b__15_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                if (((double) e.NewValue) < 0.0)
                {
                    o.SetValue(e.Property, 0.0);
                }
                else
                {
                    ((GroupBoxShadow) o).OnOffsetChanged();
                }
            }
        }
    }
}

