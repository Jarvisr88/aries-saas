namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplateVisualState(GroupName="RequiredStates", Name="NotRequired"), TemplateVisualState(GroupName="RequiredStates", Name="Required")]
    public class LayoutItemLabel : ContentControlBase
    {
        public static readonly DependencyProperty IsRequiredProperty;
        private double _CustomWidth = double.NaN;
        private double _DesiredWidth = double.NaN;
        public Action DesiredWidthChanged;
        private const string RequiredStates = "RequiredStates";
        private const string NotRequiredState = "NotRequired";
        private const string RequiredState = "Required";

        static LayoutItemLabel()
        {
            IsRequiredProperty = DependencyProperty.Register("IsRequired", typeof(bool), typeof(LayoutItemLabel), new PropertyMetadata((o, e) => ((LayoutItemLabel) o).OnIsRequiredChanged()));
        }

        public LayoutItemLabel()
        {
            base.DefaultStyleKey = typeof(LayoutItemLabel);
        }

        protected virtual void OnDesiredWidthChanged()
        {
            if (this.DesiredWidthChanged != null)
            {
                this.DesiredWidthChanged();
            }
        }

        protected virtual void OnIsRequiredChanged()
        {
            this.UpdateState(false);
        }

        protected override Size OnMeasure(Size availableSize)
        {
            Size size = base.OnMeasure(availableSize);
            this.DesiredWidth = UIElementExtensions.GetRoundedSize(size.Width);
            if (!double.IsNaN(this.CustomWidth))
            {
                size.Width = this.CustomWidth;
            }
            return size;
        }

        protected override void UpdateState(bool useTransitions)
        {
            base.UpdateState(useTransitions);
            this.GoToState(this.IsRequired ? "Required" : "NotRequired", useTransitions);
        }

        public double CustomWidth
        {
            get => 
                this._CustomWidth;
            set
            {
                if (!this.CustomWidth.Equals(value))
                {
                    this._CustomWidth = value;
                    base.InvalidateMeasure();
                    Action<LayoutGroup> action = <>c.<>9__6_0;
                    if (<>c.<>9__6_0 == null)
                    {
                        Action<LayoutGroup> local1 = <>c.<>9__6_0;
                        action = <>c.<>9__6_0 = x => x.InvalidateMeasure();
                    }
                    LayoutTreeHelper.GetVisualParents(this, null).OfType<LayoutGroup>().FirstOrDefault<LayoutGroup>().Do<LayoutGroup>(action);
                    Action<LayoutItem> action2 = <>c.<>9__6_1;
                    if (<>c.<>9__6_1 == null)
                    {
                        Action<LayoutItem> local2 = <>c.<>9__6_1;
                        action2 = <>c.<>9__6_1 = x => x.InvalidateMeasure();
                    }
                    LayoutTreeHelper.GetVisualParents(this, null).OfType<LayoutItem>().FirstOrDefault<LayoutItem>().Do<LayoutItem>(action2);
                    Action<LayoutItemPanel> action3 = <>c.<>9__6_2;
                    if (<>c.<>9__6_2 == null)
                    {
                        Action<LayoutItemPanel> local3 = <>c.<>9__6_2;
                        action3 = <>c.<>9__6_2 = x => x.InvalidateMeasure();
                    }
                    LayoutTreeHelper.GetVisualParents(this, null).OfType<LayoutItemPanel>().FirstOrDefault<LayoutItemPanel>().Do<LayoutItemPanel>(action3);
                }
            }
        }

        public double DesiredWidth
        {
            get => 
                this._DesiredWidth;
            private set
            {
                if (this.DesiredWidth != value)
                {
                    double d = this._DesiredWidth;
                    this._DesiredWidth = value;
                    if (!double.IsNaN(d))
                    {
                        this.OnDesiredWidthChanged();
                    }
                }
            }
        }

        public bool IsRequired
        {
            get => 
                (bool) base.GetValue(IsRequiredProperty);
            set => 
                base.SetValue(IsRequiredProperty, value);
        }

        protected override bool IsContentInLogicalTree =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemLabel.<>c <>9 = new LayoutItemLabel.<>c();
            public static Action<LayoutGroup> <>9__6_0;
            public static Action<LayoutItem> <>9__6_1;
            public static Action<LayoutItemPanel> <>9__6_2;

            internal void <.cctor>b__23_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemLabel) o).OnIsRequiredChanged();
            }

            internal void <set_CustomWidth>b__6_0(LayoutGroup x)
            {
                x.InvalidateMeasure();
            }

            internal void <set_CustomWidth>b__6_1(LayoutItem x)
            {
                x.InvalidateMeasure();
            }

            internal void <set_CustomWidth>b__6_2(LayoutItemPanel x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

