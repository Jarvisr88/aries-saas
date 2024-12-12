namespace DevExpress.Xpf.Docking
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public static class DefinitionsHelper
    {
        internal static GridLength ZeroLength = new GridLength(0.0);
        internal static GridLength ZeroStarLength = new GridLength(0.0, GridUnitType.Star);
        public static readonly DependencyProperty DefinitionProperty = DependencyProperty.RegisterAttached("Definition", typeof(DefinitionBase), typeof(DefinitionsHelper), null);

        public static double GetActualLength(this DefinitionBase def) => 
            UserActualSizeValueCache(def);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static DefinitionBase GetDefinition(DependencyObject target) => 
            (DefinitionBase) target.GetValue(DefinitionProperty);

        public static GridLength GetLength(this DefinitionBase def) => 
            UserSizeValueCache(def);

        internal static double GetMaxLength(this DefinitionBase def) => 
            (double) def.GetValue(IsColumnDefinition(def) ? ColumnDefinition.MaxWidthProperty : RowDefinition.MaxHeightProperty);

        internal static double GetMinLength(this DefinitionBase def) => 
            (double) def.GetValue(IsColumnDefinition(def) ? ColumnDefinition.MinWidthProperty : RowDefinition.MinHeightProperty);

        public static bool IsAbsolute(DefinitionBase def) => 
            UserSizeValueCache(def).IsAbsolute;

        private static bool IsColumnDefinition(DefinitionBase def) => 
            def is ColumnDefinition;

        private static bool IsConstraintValid(double value) => 
            !double.IsNaN(value) && !double.IsInfinity(value);

        public static bool IsStar(DefinitionBase def) => 
            UserSizeValueCache(def).IsStar;

        internal static bool IsZero(DefinitionBase definition) => 
            UserSizeValueCache(definition).Equals(ZeroLength);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static void SetDefinition(DependencyObject target, DefinitionBase value)
        {
            target.SetValue(DefinitionProperty, value);
        }

        internal static void SetMaxSize(this DefinitionBase def, Size maxSize)
        {
            bool flag = IsColumnDefinition(def);
            DependencyProperty dp = flag ? ColumnDefinition.MaxWidthProperty : RowDefinition.MaxHeightProperty;
            double num = flag ? maxSize.Width : maxSize.Height;
            if (IsConstraintValid(num))
            {
                def.SetValue(dp, num);
            }
        }

        internal static void SetMinSize(this DefinitionBase def, Size minSize)
        {
            bool flag = IsColumnDefinition(def);
            DependencyProperty dp = flag ? ColumnDefinition.MinWidthProperty : RowDefinition.MinHeightProperty;
            double num = flag ? minSize.Width : minSize.Height;
            if (IsConstraintValid(num))
            {
                def.SetValue(dp, num);
            }
        }

        public static double UserActualSizeValueCache(DefinitionBase def) => 
            IsColumnDefinition(def) ? ((ColumnDefinition) def).ActualWidth : ((RowDefinition) def).ActualHeight;

        public static double UserMaxSizeValueCache(DefinitionBase def) => 
            (double) def.GetValue(IsColumnDefinition(def) ? ColumnDefinition.MaxWidthProperty : RowDefinition.MaxHeightProperty);

        public static double UserMinSizeValueCache(DefinitionBase def) => 
            (double) def.GetValue(IsColumnDefinition(def) ? ColumnDefinition.MinWidthProperty : RowDefinition.MinHeightProperty);

        public static GridLength UserSizeValueCache(DefinitionBase def) => 
            (GridLength) def.GetValue(IsColumnDefinition(def) ? ColumnDefinition.WidthProperty : RowDefinition.HeightProperty);
    }
}

