namespace DevExpress.Xpf.Editors.Controls
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TrackBarEditThumbContentControl : MultiContentControl
    {
        public static readonly DependencyProperty IsDraggingProperty;

        static TrackBarEditThumbContentControl()
        {
            Type ownerType = typeof(TrackBarEditThumbContentControl);
            IsDraggingProperty = DependencyPropertyManager.Register("IsDragging", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((TrackBarEditThumbContentControl) d).PropertyChangedIsDragging()));
        }

        public TrackBarEditThumbContentControl()
        {
            base.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged);
            base.Focusable = false;
        }

        protected virtual List<string> GetVisualStateNames()
        {
            List<string> list = new List<string>();
            if (!base.IsEnabled)
            {
                list.Add("Disabled");
            }
            else if (this.IsDragging)
            {
                list.Add("IsDragging");
            }
            else if (base.IsMouseOver)
            {
                list.Add("IsMouseOver");
            }
            else
            {
                list.Add("Normal");
            }
            return list;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState(false);
        }

        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UpdateVisualState(true);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, UIElement.IsMouseOverProperty))
            {
                this.UpdateVisualState(true);
            }
        }

        protected virtual void PropertyChangedIsDragging()
        {
            this.UpdateVisualState(true);
        }

        protected void UpdateVisualState(bool useTransitions)
        {
            foreach (string str in this.GetVisualStateNames())
            {
                if (VisualStateManager.GoToState(this, str, useTransitions))
                {
                    break;
                }
            }
            if (!base.IsEnabled)
            {
                base.VisibleChildIndex = 3;
            }
            else if (this.IsDragging)
            {
                base.VisibleChildIndex = 2;
            }
            else if (base.IsMouseOver)
            {
                base.VisibleChildIndex = 1;
            }
            else
            {
                base.VisibleChildIndex = 0;
            }
        }

        public bool IsDragging
        {
            get => 
                (bool) base.GetValue(IsDraggingProperty);
            set => 
                base.SetValue(IsDraggingProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TrackBarEditThumbContentControl.<>c <>9 = new TrackBarEditThumbContentControl.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TrackBarEditThumbContentControl) d).PropertyChangedIsDragging();
            }
        }
    }
}

