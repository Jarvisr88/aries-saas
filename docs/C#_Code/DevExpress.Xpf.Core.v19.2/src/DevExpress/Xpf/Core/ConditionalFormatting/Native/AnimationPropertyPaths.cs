namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;

    public static class AnimationPropertyPaths
    {
        private static Dictionary<string, AnimationMask> animationMaskStorage = new Dictionary<string, AnimationMask>();

        static AnimationPropertyPaths()
        {
            Action<AnimationMask, Func<PropertyPath>> action = (m, p) => animationMaskStorage.Add(p().Path, m);
            action(AnimationMask.Background, new Func<PropertyPath>(AnimationPropertyPaths.CreateBackgroundPath));
            action(AnimationMask.Foreground, new Func<PropertyPath>(AnimationPropertyPaths.CreateForegroundPath));
            action(AnimationMask.ValuePosition, new Func<PropertyPath>(AnimationPropertyPaths.CreateValuePositionPath));
            action(AnimationMask.Icon, new Func<PropertyPath>(AnimationPropertyPaths.CreateIconPath));
            action(AnimationMask.IconOpacity, new Func<PropertyPath>(AnimationPropertyPaths.CreateIconOpacityPath));
            action(AnimationMask.FontSize, new Func<PropertyPath>(AnimationPropertyPaths.CreateFontSizePath));
            action(AnimationMask.FontStyle, new Func<PropertyPath>(AnimationPropertyPaths.CreateFontStylePath));
            action(AnimationMask.FontFamily, new Func<PropertyPath>(AnimationPropertyPaths.CreateFontFamilyPath));
            action(AnimationMask.FontStretch, new Func<PropertyPath>(AnimationPropertyPaths.CreateFontStretchPath));
            action(AnimationMask.FontWeight, new Func<PropertyPath>(AnimationPropertyPaths.CreateFontWeightPath));
        }

        public static PropertyPath CreateBackgroundPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.BackgroundProperty, SolidColorBrush.ColorProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateFontFamilyPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.FontFamilyProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateFontSizePath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.FontSizeProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateFontStretchPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.FontStretchProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateFontStylePath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.FontStyleProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateFontWeightPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.FontWeightProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateForegroundPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.ForegroundProperty, SolidColorBrush.ColorProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateIconOpacityPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.IconOpacityProperty };
            return CreatePropertyPath(props);
        }

        public static PropertyPath CreateIconPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.IconProperty };
            return CreatePropertyPath(props);
        }

        private static PropertyPath CreatePropertyPath(params DependencyProperty[] props)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < props.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(".");
                }
                builder.Append(props[i].Name);
            }
            return new PropertyPath(builder.ToString(), new object[0]);
        }

        public static PropertyPath CreateValuePositionPath()
        {
            DependencyProperty[] props = new DependencyProperty[] { AnimationElement.ValuePositionProperty };
            return CreatePropertyPath(props);
        }

        public static AnimationMask GetAnimationMask(PropertyPath path)
        {
            AnimationMask none = AnimationMask.None;
            if ((path != null) && !string.IsNullOrEmpty(path.Path))
            {
                animationMaskStorage.TryGetValue(path.Path, out none);
            }
            return none;
        }

        internal static PropertyPath GetConditionalTimelinePropertyPath(ConditionalTargetProperty prop)
        {
            switch (prop)
            {
                case ConditionalTargetProperty.Background:
                    return CreateBackgroundPath();

                case ConditionalTargetProperty.Foreground:
                    return CreateForegroundPath();

                case ConditionalTargetProperty.ValuePosition:
                    return CreateValuePositionPath();

                case ConditionalTargetProperty.Icon:
                    return CreateIconPath();

                case ConditionalTargetProperty.IconOpacity:
                    return CreateIconOpacityPath();

                case ConditionalTargetProperty.FontSize:
                    return CreateFontSizePath();

                case ConditionalTargetProperty.FontStyle:
                    return CreateFontStylePath();

                case ConditionalTargetProperty.FontFamily:
                    return CreateFontFamilyPath();

                case ConditionalTargetProperty.FontStretch:
                    return CreateFontStretchPath();

                case ConditionalTargetProperty.FontWeight:
                    return CreateFontWeightPath();
            }
            return null;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AnimationPropertyPaths.<>c <>9 = new AnimationPropertyPaths.<>c();

            internal void <.cctor>b__1_0(AnimationMask m, Func<PropertyPath> p)
            {
                AnimationPropertyPaths.animationMaskStorage.Add(p().Path, m);
            }
        }
    }
}

