namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ConditionalFormattingHelper<T> where T: FrameworkElement, IConditionalFormattingClient<T>
    {
        private static bool CoerceCallbacksRegistered;
        private readonly T owner;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty backgroundProperty;
        private ConditionalFormatMask currentMask;

        public ConditionalFormattingHelper(T owner, DependencyProperty backgroundProperty = null)
        {
            this.owner = owner;
            this.backgroundProperty = backgroundProperty;
            this.Locker = new DevExpress.Xpf.Core.Locker();
        }

        public void AddMaskFlags(ConditionalFormatMask mask)
        {
            this.currentMask |= mask;
        }

        public Brush CoerceBackground(Brush background)
        {
            Func<FormatConditionBaseInfo, Func<Brush, FormatValueProvider, Brush>> coerceActionAccessor = <>c<T>.<>9__21_0;
            if (<>c<T>.<>9__21_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<Brush, FormatValueProvider, Brush>> local1 = <>c<T>.<>9__21_0;
                coerceActionAccessor = <>c<T>.<>9__21_0 = x => new Func<Brush, FormatValueProvider, Brush>(x.CoerceBackground);
            }
            return this.CoerceSelectionDependableConditionalFormatValue<Brush>(TextBlock.BackgroundProperty, background, coerceActionAccessor);
        }

        private static object CoerceBackground(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceBackground((Brush) baseValue);

        private TValue CoerceConditionalFormatValue<TValue>(TValue value, Func<FormatConditionBaseInfo, Func<TValue, FormatValueProvider, TValue>> coerceActionAccessor)
        {
            if (this.owner.IsReady && !this.Locker.IsLocked)
            {
                this.owner.Locker.DoLockedAction(delegate {
                    IList<FormatConditionBaseInfo> relatedConditions = ((ConditionalFormattingHelper<T>) this).owner.GetRelatedConditions();
                    if (relatedConditions != null)
                    {
                        foreach (FormatConditionBaseInfo info in relatedConditions)
                        {
                            FormatValueProvider? valueProvider = ((ConditionalFormattingHelper<T>) this).owner.GetValueProvider(info.ActualFieldName);
                            if (valueProvider != null)
                            {
                                value = coerceActionAccessor(info)(value, valueProvider.Value);
                            }
                        }
                    }
                });
            }
            return value;
        }

        private FontFamily CoerceFontFamily(FontFamily fontFamily)
        {
            Func<FormatConditionBaseInfo, Func<FontFamily, FormatValueProvider, FontFamily>> coerceActionAccessor = <>c<T>.<>9__16_0;
            if (<>c<T>.<>9__16_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<FontFamily, FormatValueProvider, FontFamily>> local1 = <>c<T>.<>9__16_0;
                coerceActionAccessor = <>c<T>.<>9__16_0 = x => new Func<FontFamily, FormatValueProvider, FontFamily>(x.CoerceFontFamily);
            }
            return this.CoerceConditionalFormatValue<FontFamily>(fontFamily, coerceActionAccessor);
        }

        private static object CoerceFontFamily(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceFontFamily((FontFamily) baseValue);

        private double CoerceFontSize(double fontSize)
        {
            Func<FormatConditionBaseInfo, Func<double, FormatValueProvider, double>> coerceActionAccessor = <>c<T>.<>9__14_0;
            if (<>c<T>.<>9__14_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<double, FormatValueProvider, double>> local1 = <>c<T>.<>9__14_0;
                coerceActionAccessor = <>c<T>.<>9__14_0 = x => new Func<double, FormatValueProvider, double>(x.CoerceFontSize);
            }
            return this.CoerceConditionalFormatValue<double>(fontSize, coerceActionAccessor);
        }

        private static object CoerceFontSize(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceFontSize((double) baseValue);

        private FontStretch CoerceFontStretch(FontStretch fontStretch)
        {
            Func<FormatConditionBaseInfo, Func<FontStretch, FormatValueProvider, FontStretch>> coerceActionAccessor = <>c<T>.<>9__17_0;
            if (<>c<T>.<>9__17_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<FontStretch, FormatValueProvider, FontStretch>> local1 = <>c<T>.<>9__17_0;
                coerceActionAccessor = <>c<T>.<>9__17_0 = x => new Func<FontStretch, FormatValueProvider, FontStretch>(x.CoerceFontStretch);
            }
            return this.CoerceConditionalFormatValue<FontStretch>(fontStretch, coerceActionAccessor);
        }

        private static object CoerceFontStretch(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceFontStretch((FontStretch) baseValue);

        private FontStyle CoerceFontStyle(FontStyle fontStyle)
        {
            Func<FormatConditionBaseInfo, Func<FontStyle, FormatValueProvider, FontStyle>> coerceActionAccessor = <>c<T>.<>9__15_0;
            if (<>c<T>.<>9__15_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<FontStyle, FormatValueProvider, FontStyle>> local1 = <>c<T>.<>9__15_0;
                coerceActionAccessor = <>c<T>.<>9__15_0 = x => new Func<FontStyle, FormatValueProvider, FontStyle>(x.CoerceFontStyle);
            }
            return this.CoerceConditionalFormatValue<FontStyle>(fontStyle, coerceActionAccessor);
        }

        private static object CoerceFontStyle(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceFontStyle((FontStyle) baseValue);

        private FontWeight CoerceFontWeight(FontWeight fontWeight)
        {
            Func<FormatConditionBaseInfo, Func<FontWeight, FormatValueProvider, FontWeight>> coerceActionAccessor = <>c<T>.<>9__18_0;
            if (<>c<T>.<>9__18_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<FontWeight, FormatValueProvider, FontWeight>> local1 = <>c<T>.<>9__18_0;
                coerceActionAccessor = <>c<T>.<>9__18_0 = x => new Func<FontWeight, FormatValueProvider, FontWeight>(x.CoerceFontWeight);
            }
            return this.CoerceConditionalFormatValue<FontWeight>(fontWeight, coerceActionAccessor);
        }

        private static object CoerceFontWeight(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceFontWeight((FontWeight) baseValue);

        private Brush CoerceForeground(Brush brush)
        {
            Func<FormatConditionBaseInfo, Func<Brush, FormatValueProvider, Brush>> coerceActionAccessor = <>c<T>.<>9__13_0;
            if (<>c<T>.<>9__13_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<Brush, FormatValueProvider, Brush>> local1 = <>c<T>.<>9__13_0;
                coerceActionAccessor = <>c<T>.<>9__13_0 = x => new Func<Brush, FormatValueProvider, Brush>(x.CoerceForeground);
            }
            return this.CoerceSelectionDependableConditionalFormatValue<Brush>(TextBlock.ForegroundProperty, brush, coerceActionAccessor);
        }

        private static object CoerceForeground(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceForeground(baseValue as Brush);

        private void CoerceIfNeeded(ConditionalFormatMask newMask, ConditionalFormatMask flag, Action coerceAction)
        {
            if (((newMask & flag) > ConditionalFormatMask.None) || ((this.currentMask & flag) > ConditionalFormatMask.None))
            {
                coerceAction();
            }
        }

        private TValue CoerceSelectionDependableConditionalFormatValue<TValue>(DependencyProperty property, TValue value, Func<FormatConditionBaseInfo, Func<TValue, FormatValueProvider, TValue>> coerceActionAccessor)
        {
            if (!this.owner.HasCustomAppearance && this.owner.IsSelected)
            {
                return value;
            }
            TValue conditionalValue = this.CoerceConditionalFormatValue<TValue>(value, coerceActionAccessor);
            CustomAppearanceEventArgs args = new CustomAppearanceEventArgs(property, value, conditionalValue);
            this.owner.UpdateCustomAppearance(args);
            if (!args.Handled)
            {
                args.Result = this.owner.IsSelected ? value : conditionalValue;
            }
            return (!(args.Result is TValue) ? value : ((TValue) args.Result));
        }

        private TextDecorationCollection CoerceTextDecorations(TextDecorationCollection textDecorations)
        {
            Func<FormatConditionBaseInfo, Func<TextDecorationCollection, FormatValueProvider, TextDecorationCollection>> coerceActionAccessor = <>c<T>.<>9__22_0;
            if (<>c<T>.<>9__22_0 == null)
            {
                Func<FormatConditionBaseInfo, Func<TextDecorationCollection, FormatValueProvider, TextDecorationCollection>> local1 = <>c<T>.<>9__22_0;
                coerceActionAccessor = <>c<T>.<>9__22_0 = x => new Func<TextDecorationCollection, FormatValueProvider, TextDecorationCollection>(x.CoerceTextDecorations);
            }
            return this.CoerceConditionalFormatValue<TextDecorationCollection>(textDecorations, coerceActionAccessor);
        }

        private static object CoerceTextDecorations(DependencyObject d, object baseValue) => 
            ((IConditionalFormattingClient<T>) d).FormattingHelper.CoerceTextDecorations((TextDecorationCollection) baseValue);

        private static void EnsureCoerceCallbacksRegistered(DependencyProperty backgroundProperty)
        {
            if (!ConditionalFormattingHelper<T>.CoerceCallbacksRegistered)
            {
                ConditionalFormattingHelper<T>.CoerceCallbacksRegistered = true;
                TextBlock.FontSizeProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(TextBlock.FontSizeProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceFontSize)));
                TextBlock.ForegroundProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(TextBlock.ForegroundProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceForeground)));
                TextBlock.FontFamilyProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(TextBlock.FontFamilyProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceFontFamily)));
                TextBlock.FontStyleProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(TextBlock.FontStyleProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceFontStyle)));
                TextBlock.FontStretchProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(TextBlock.FontStretchProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceFontStretch)));
                TextBlock.FontWeightProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(TextBlock.FontWeightProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceFontWeight)));
                DevExpress.Xpf.Core.ConditionalFormatting.Native.InplaceBaseEditHelper.TextDecorationsProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(DevExpress.Xpf.Core.ConditionalFormatting.Native.InplaceBaseEditHelper.TextDecorationsProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceTextDecorations)));
                if (backgroundProperty != null)
                {
                    backgroundProperty.OverrideMetadata(typeof(T), new FrameworkPropertyMetadata(backgroundProperty.DefaultMetadata.DefaultValue, null, new CoerceValueCallback(ConditionalFormattingHelper<T>.CoerceBackground)));
                }
            }
        }

        public void UpdateConditionalAppearance()
        {
            if (!this.Locker.IsLocked)
            {
                ConditionalFormatMask conditionsMask = ConditionalFormattingMaskHelper.GetConditionsMask(this.owner.GetRelatedConditions());
                if (this.owner.HasCustomAppearance)
                {
                    conditionsMask = (conditionsMask | ConditionalFormatMask.Background) | ConditionalFormatMask.Foreground;
                }
                if ((conditionsMask != ConditionalFormatMask.None) || (this.currentMask != ConditionalFormatMask.None))
                {
                    this.UpdateConditionalAppearanceCore(conditionsMask);
                }
                this.currentMask = conditionsMask;
            }
        }

        private void UpdateConditionalAppearanceCore(ConditionalFormatMask newMask)
        {
            ConditionalFormattingHelper<T>.EnsureCoerceCallbacksRegistered(this.backgroundProperty);
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.DataBarOrIcon, delegate {
                Func<FormatConditionBaseInfo, Func<DataBarFormatInfo, FormatValueProvider, DataBarFormatInfo>> coerceActionAccessor = <>c<T>.<>9__29_1;
                if (<>c<T>.<>9__29_1 == null)
                {
                    Func<FormatConditionBaseInfo, Func<DataBarFormatInfo, FormatValueProvider, DataBarFormatInfo>> local1 = <>c<T>.<>9__29_1;
                    coerceActionAccessor = <>c<T>.<>9__29_1 = x => new Func<DataBarFormatInfo, FormatValueProvider, DataBarFormatInfo>(x.CoerceDataBarFormatInfo);
                }
                base.owner.UpdateDataBarFormatInfo(this.CoerceConditionalFormatValue<DataBarFormatInfo>(null, coerceActionAccessor));
            });
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.Background, delegate {
                base.owner.UpdateBackground();
                if (base.backgroundProperty != null)
                {
                    base.owner.CoerceValue(base.backgroundProperty);
                }
            });
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.Foreground, () => base.owner.CoerceValue(TextBlock.ForegroundProperty));
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.FontSize, () => base.owner.CoerceValue(TextBlock.FontSizeProperty));
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.FontStyle, () => base.owner.CoerceValue(TextBlock.FontStyleProperty));
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.FontFamily, () => base.owner.CoerceValue(TextBlock.FontFamilyProperty));
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.FontStretch, () => base.owner.CoerceValue(TextBlock.FontStretchProperty));
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.FontWeight, () => base.owner.CoerceValue(TextBlock.FontWeightProperty));
            this.CoerceIfNeeded(newMask, ConditionalFormatMask.TextDecorations, () => base.owner.CoerceValue(DevExpress.Xpf.Core.ConditionalFormatting.Native.InplaceBaseEditHelper.TextDecorationsProperty));
        }

        public DevExpress.Xpf.Core.Locker Locker { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConditionalFormattingHelper<T>.<>c <>9;
            public static Func<FormatConditionBaseInfo, Func<Brush, FormatValueProvider, Brush>> <>9__13_0;
            public static Func<FormatConditionBaseInfo, Func<double, FormatValueProvider, double>> <>9__14_0;
            public static Func<FormatConditionBaseInfo, Func<FontStyle, FormatValueProvider, FontStyle>> <>9__15_0;
            public static Func<FormatConditionBaseInfo, Func<FontFamily, FormatValueProvider, FontFamily>> <>9__16_0;
            public static Func<FormatConditionBaseInfo, Func<FontStretch, FormatValueProvider, FontStretch>> <>9__17_0;
            public static Func<FormatConditionBaseInfo, Func<FontWeight, FormatValueProvider, FontWeight>> <>9__18_0;
            public static Func<FormatConditionBaseInfo, Func<Brush, FormatValueProvider, Brush>> <>9__21_0;
            public static Func<FormatConditionBaseInfo, Func<TextDecorationCollection, FormatValueProvider, TextDecorationCollection>> <>9__22_0;
            public static Func<FormatConditionBaseInfo, Func<DataBarFormatInfo, FormatValueProvider, DataBarFormatInfo>> <>9__29_1;

            static <>c()
            {
                ConditionalFormattingHelper<T>.<>c.<>9 = new ConditionalFormattingHelper<T>.<>c();
            }

            internal Func<Brush, FormatValueProvider, Brush> <CoerceBackground>b__21_0(FormatConditionBaseInfo x) => 
                new Func<Brush, FormatValueProvider, Brush>(x.CoerceBackground);

            internal Func<FontFamily, FormatValueProvider, FontFamily> <CoerceFontFamily>b__16_0(FormatConditionBaseInfo x) => 
                new Func<FontFamily, FormatValueProvider, FontFamily>(x.CoerceFontFamily);

            internal Func<double, FormatValueProvider, double> <CoerceFontSize>b__14_0(FormatConditionBaseInfo x) => 
                new Func<double, FormatValueProvider, double>(x.CoerceFontSize);

            internal Func<FontStretch, FormatValueProvider, FontStretch> <CoerceFontStretch>b__17_0(FormatConditionBaseInfo x) => 
                new Func<FontStretch, FormatValueProvider, FontStretch>(x.CoerceFontStretch);

            internal Func<FontStyle, FormatValueProvider, FontStyle> <CoerceFontStyle>b__15_0(FormatConditionBaseInfo x) => 
                new Func<FontStyle, FormatValueProvider, FontStyle>(x.CoerceFontStyle);

            internal Func<FontWeight, FormatValueProvider, FontWeight> <CoerceFontWeight>b__18_0(FormatConditionBaseInfo x) => 
                new Func<FontWeight, FormatValueProvider, FontWeight>(x.CoerceFontWeight);

            internal Func<Brush, FormatValueProvider, Brush> <CoerceForeground>b__13_0(FormatConditionBaseInfo x) => 
                new Func<Brush, FormatValueProvider, Brush>(x.CoerceForeground);

            internal Func<TextDecorationCollection, FormatValueProvider, TextDecorationCollection> <CoerceTextDecorations>b__22_0(FormatConditionBaseInfo x) => 
                new Func<TextDecorationCollection, FormatValueProvider, TextDecorationCollection>(x.CoerceTextDecorations);

            internal Func<DataBarFormatInfo, FormatValueProvider, DataBarFormatInfo> <UpdateConditionalAppearanceCore>b__29_1(FormatConditionBaseInfo x) => 
                new Func<DataBarFormatInfo, FormatValueProvider, DataBarFormatInfo>(x.CoerceDataBarFormatInfo);
        }
    }
}

