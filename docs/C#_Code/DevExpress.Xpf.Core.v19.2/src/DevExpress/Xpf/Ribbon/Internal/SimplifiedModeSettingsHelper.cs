namespace DevExpress.Xpf.Ribbon.Internal
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Windows;

    public static class SimplifiedModeSettingsHelper
    {
        public static readonly DependencyProperty IsInSimplifiedRibbonProperty = DependencyProperty.RegisterAttached("IsInSimplifiedRibbon", typeof(bool), typeof(SimplifiedModeSettingsHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static SimplifiedModeLocation? GetActualSimplifiedModeLocation(BarItemLinkBase linkBase)
        {
            if (linkBase != null)
            {
                SimplifiedModeLocation? location = SimplifiedModeSettings.GetLocation(linkBase);
                if (location != null)
                {
                    return SimplifiedModeSettings.GetLocation(linkBase);
                }
                if (linkBase.Item == null)
                {
                    return null;
                }
                if (linkBase.Item != null)
                {
                    return SimplifiedModeSettings.GetLocation(linkBase.Item);
                }
            }
            return null;
        }

        public static bool GetIsInSimplifiedRibbon(DependencyObject obj) => 
            (bool) obj.GetValue(IsInSimplifiedRibbonProperty);

        public static bool IsLocationCompatibleWithOverflowMenu(SimplifiedModeLocation? location)
        {
            if (location == null)
            {
                return true;
            }
            SimplifiedModeLocation? nullable = location;
            SimplifiedModeLocation classic = SimplifiedModeLocation.Classic;
            return ((((SimplifiedModeLocation) nullable.GetValueOrDefault()) == classic) ? (nullable == null) : true);
        }

        public static bool IsLocationCompatibleWithSimplifiedMode(bool isSimplifiedMode, SimplifiedModeLocation? location)
        {
            SimplifiedModeLocation? nullable = location;
            SimplifiedModeLocation overflowMenu = SimplifiedModeLocation.OverflowMenu;
            if ((((SimplifiedModeLocation) nullable.GetValueOrDefault()) == overflowMenu) ? (nullable != null) : false)
            {
                return false;
            }
            if (location == null)
            {
                location = new SimplifiedModeLocation?(SimplifiedModeLocation.All);
            }
            if (!isSimplifiedMode)
            {
                nullable = location;
                overflowMenu = SimplifiedModeLocation.Simplified;
                return ((((SimplifiedModeLocation) nullable.GetValueOrDefault()) == overflowMenu) ? (nullable == null) : true);
            }
            nullable = location;
            overflowMenu = SimplifiedModeLocation.Classic;
            if (!((((SimplifiedModeLocation) nullable.GetValueOrDefault()) == overflowMenu) ? (nullable == null) : true))
            {
                return false;
            }
            nullable = location;
            overflowMenu = SimplifiedModeLocation.ClassicAndOverflowMenu;
            return ((((SimplifiedModeLocation) nullable.GetValueOrDefault()) == overflowMenu) ? (nullable == null) : true);
        }

        public static void SetIsInSimplifiedRibbon(DependencyObject obj, bool value)
        {
            obj.SetValue(IsInSimplifiedRibbonProperty, value);
        }
    }
}

