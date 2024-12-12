namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class GradientMultiSliderThumbSelector : Control
    {
        public static readonly DependencyProperty SelectedThumbProperty;
        private static readonly DependencyPropertyKey SelectedThumbColorPropertyKey;
        public static readonly DependencyProperty SelectedThumbColorProperty;

        static GradientMultiSliderThumbSelector()
        {
            Type ownerType = typeof(GradientMultiSliderThumbSelector);
            SelectedThumbProperty = DependencyPropertyManager.Register("SelectedThumb", typeof(GradientMultiSliderThumb), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((GradientMultiSliderThumbSelector) obj).OnSelectedThumbChanged((GradientMultiSliderThumb) args.NewValue)));
            SelectedThumbColorPropertyKey = DependencyPropertyManager.RegisterReadOnly("SelectedThumbColor", typeof(Color), ownerType, new FrameworkPropertyMetadata(Colors.Black));
            SelectedThumbColorProperty = SelectedThumbColorPropertyKey.DependencyProperty;
        }

        public GradientMultiSliderThumbSelector()
        {
            this.SetDefaultStyleKey(typeof(GradientMultiSliderThumbSelector));
            this.NextThumbCommand = DelegateCommandFactory.Create(new Action(this.NextThumb));
            this.PreviousThumbCommand = DelegateCommandFactory.Create(new Action(this.PreviousThumb));
        }

        private GradientMultiSliderThumb GetNextThumb()
        {
            if (this.SelectedThumb != null)
            {
                Func<GradientMultiSliderThumb, double> keySelector = <>c.<>9__22_0;
                if (<>c.<>9__22_0 == null)
                {
                    Func<GradientMultiSliderThumb, double> local1 = <>c.<>9__22_0;
                    keySelector = <>c.<>9__22_0 = x => x.Offset;
                }
                Func<IGrouping<double, GradientMultiSliderThumb>, double> func2 = <>c.<>9__22_1;
                if (<>c.<>9__22_1 == null)
                {
                    Func<IGrouping<double, GradientMultiSliderThumb>, double> local2 = <>c.<>9__22_1;
                    func2 = <>c.<>9__22_1 = x => x.Key;
                }
                List<IGrouping<double, GradientMultiSliderThumb>> list = this.SelectedThumb.OwnerSlider.Thumbs.GroupBy<GradientMultiSliderThumb, double>(keySelector).OrderBy<IGrouping<double, GradientMultiSliderThumb>, double>(func2).ToList<IGrouping<double, GradientMultiSliderThumb>>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Key.AreClose(this.SelectedThumb.Offset))
                    {
                        return (this.GetNextThumb(list[i]) ?? this.GetNextThumb(((i + 1) < list.Count) ? list[i + 1] : list[0]));
                    }
                }
            }
            return null;
        }

        private GradientMultiSliderThumb GetNextThumb(IGrouping<double, GradientMultiSliderThumb> group)
        {
            List<GradientMultiSliderThumb> source = group.ToList<GradientMultiSliderThumb>();
            if (!group.Key.AreClose(this.SelectedThumb.Offset))
            {
                return source.FirstOrDefault<GradientMultiSliderThumb>();
            }
            if (source.Count > 1)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    Color color = source[i].Color;
                    if (color.Equals(this.SelectedThumb.Color))
                    {
                        return (((i + 1) < source.Count) ? source[i + 1] : null);
                    }
                }
            }
            return null;
        }

        private GradientMultiSliderThumb GetPreviousThumb()
        {
            if (this.SelectedThumb != null)
            {
                Func<GradientMultiSliderThumb, double> keySelector = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Func<GradientMultiSliderThumb, double> local1 = <>c.<>9__24_0;
                    keySelector = <>c.<>9__24_0 = x => x.Offset;
                }
                Func<IGrouping<double, GradientMultiSliderThumb>, double> func2 = <>c.<>9__24_1;
                if (<>c.<>9__24_1 == null)
                {
                    Func<IGrouping<double, GradientMultiSliderThumb>, double> local2 = <>c.<>9__24_1;
                    func2 = <>c.<>9__24_1 = x => x.Key;
                }
                List<IGrouping<double, GradientMultiSliderThumb>> list = this.SelectedThumb.OwnerSlider.Thumbs.GroupBy<GradientMultiSliderThumb, double>(keySelector).OrderBy<IGrouping<double, GradientMultiSliderThumb>, double>(func2).ToList<IGrouping<double, GradientMultiSliderThumb>>();
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Key.AreClose(this.SelectedThumb.Offset))
                    {
                        return (this.GetPreviousThumb(list[i]) ?? this.GetPreviousThumb(((i - 1) >= 0) ? list[i - 1] : list[list.Count - 1]));
                    }
                }
            }
            return null;
        }

        private GradientMultiSliderThumb GetPreviousThumb(IGrouping<double, GradientMultiSliderThumb> group)
        {
            List<GradientMultiSliderThumb> source = group.ToList<GradientMultiSliderThumb>();
            if (!group.Key.AreClose(this.SelectedThumb.Offset))
            {
                return source.LastOrDefault<GradientMultiSliderThumb>();
            }
            if (source.Count > 1)
            {
                for (int i = source.Count - 1; i >= 0; i--)
                {
                    Color color = source[i].Color;
                    if (color.Equals(this.SelectedThumb.Color))
                    {
                        return (((i - 1) >= 0) ? source[i - 1] : null);
                    }
                }
            }
            return null;
        }

        private void NextThumb()
        {
            this.SelectedThumb = this.GetNextThumb();
        }

        private void OnSelectedThumbChanged(GradientMultiSliderThumb newValue)
        {
            this.SelectedThumbColor = newValue.Color;
        }

        private void PreviousThumb()
        {
            this.SelectedThumb = this.GetPreviousThumb();
        }

        public GradientMultiSliderThumb SelectedThumb
        {
            get => 
                (GradientMultiSliderThumb) base.GetValue(SelectedThumbProperty);
            set => 
                base.SetValue(SelectedThumbProperty, value);
        }

        public Color SelectedThumbColor
        {
            get => 
                (Color) base.GetValue(SelectedThumbColorProperty);
            private set => 
                base.SetValue(SelectedThumbColorPropertyKey, value);
        }

        public ICommand NextThumbCommand { get; private set; }

        public ICommand PreviousThumbCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GradientMultiSliderThumbSelector.<>c <>9 = new GradientMultiSliderThumbSelector.<>c();
            public static Func<GradientMultiSliderThumb, double> <>9__22_0;
            public static Func<IGrouping<double, GradientMultiSliderThumb>, double> <>9__22_1;
            public static Func<GradientMultiSliderThumb, double> <>9__24_0;
            public static Func<IGrouping<double, GradientMultiSliderThumb>, double> <>9__24_1;

            internal void <.cctor>b__3_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((GradientMultiSliderThumbSelector) obj).OnSelectedThumbChanged((GradientMultiSliderThumb) args.NewValue);
            }

            internal double <GetNextThumb>b__22_0(GradientMultiSliderThumb x) => 
                x.Offset;

            internal double <GetNextThumb>b__22_1(IGrouping<double, GradientMultiSliderThumb> x) => 
                x.Key;

            internal double <GetPreviousThumb>b__24_0(GradientMultiSliderThumb x) => 
                x.Offset;

            internal double <GetPreviousThumb>b__24_1(IGrouping<double, GradientMultiSliderThumb> x) => 
                x.Key;
        }
    }
}

