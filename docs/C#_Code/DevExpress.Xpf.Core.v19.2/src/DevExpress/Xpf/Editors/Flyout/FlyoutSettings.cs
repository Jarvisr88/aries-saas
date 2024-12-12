namespace DevExpress.Xpf.Editors.Flyout
{
    using DevExpress.Xpf.Editors.Flyout.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class FlyoutSettings : FlyoutSettingsBase
    {
        public static readonly DependencyProperty ShowIndicatorProperty;
        public static readonly DependencyProperty PlacementProperty;
        public static readonly DependencyProperty IndicatorTargetProperty;
        public static readonly DependencyProperty IndicatorHorizontalAlignmentProperty;
        public static readonly DependencyProperty IndicatorVerticalAlignmentProperty;

        static FlyoutSettings()
        {
            Type ownerType = typeof(FlyoutSettings);
            ShowIndicatorProperty = DependencyPropertyManager.Register("ShowIndicator", typeof(bool), ownerType, new PropertyMetadata(false));
            PlacementProperty = DependencyPropertyManager.Register("Placement", typeof(FlyoutPlacement), ownerType, new PropertyMetadata(FlyoutPlacement.Bottom));
            IndicatorTargetProperty = DependencyPropertyManager.Register("IndicatorTarget", typeof(UIElement), ownerType, new PropertyMetadata(null));
            IndicatorHorizontalAlignmentProperty = DependencyPropertyManager.Register("IndicatorHorizontalAlignment", typeof(HorizontalAlignment?), ownerType, new PropertyMetadata(null));
            IndicatorVerticalAlignmentProperty = DependencyPropertyManager.Register("IndicatorVerticalAlignment", typeof(VerticalAlignment?), ownerType, new PropertyMetadata(null));
        }

        public override void Apply(FlyoutPositionCalculator calculator, FlyoutBase flyout)
        {
            base.Apply(calculator, flyout);
            calculator.Placement = flyout.WrapForRTL(this.Placement);
            calculator.ActualIndicatorDirection = flyout.IndicatorDirection;
            calculator.IndicatorHorizontalAlignment = flyout.WrapForRTL((this.IndicatorHorizontalAlignment != null) ? this.IndicatorHorizontalAlignment.Value : flyout.HorizontalAlignment);
            calculator.IndicatorVerticalAlignment = (this.IndicatorVerticalAlignment != null) ? this.IndicatorVerticalAlignment.Value : flyout.VerticalAlignment;
            Rect rect = flyout.GetTargetBounds(flyout.PlacementTarget, this.IndicatorTarget, () => flyout.GetTargetBounds());
            rect.Offset(flyout.HorizontalOffset, flyout.VerticalOffset);
            calculator.IndicatorTargetBounds = flyout.GetTransformedRect(flyout.GetNormalizedRect(TranslateHelper.ToScreen(flyout.PlacementTarget, rect)), true);
        }

        public override FlyoutPositionCalculator CreatePositionCalculator() => 
            new FlyoutPositionCalculator();

        public override FlyoutBase.FlyoutStrategy CreateStrategy() => 
            new FlyoutBase.FlyoutStrategy();

        public override IndicatorDirection GetIndicatorDirection(FlyoutPlacement placement)
        {
            if (this.ShowIndicator)
            {
                switch (placement)
                {
                    case FlyoutPlacement.Left:
                        return IndicatorDirection.Right;

                    case FlyoutPlacement.Top:
                        return IndicatorDirection.Bottom;

                    case FlyoutPlacement.Right:
                        return IndicatorDirection.Left;

                    case FlyoutPlacement.Bottom:
                        return IndicatorDirection.Top;
                }
            }
            return IndicatorDirection.None;
        }

        public override void OnPropertyChanged(FlyoutBase flyout, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == ShowIndicatorProperty.Name) || ((e.PropertyName == PlacementProperty.Name) || ((e.PropertyName == IndicatorTargetProperty.Name) || ((e.PropertyName == IndicatorHorizontalAlignmentProperty.Name) || (e.PropertyName == IndicatorVerticalAlignmentProperty.Name)))))
            {
                flyout.InvalidateLocation();
            }
        }

        public FlyoutPlacement Placement
        {
            get => 
                (FlyoutPlacement) base.GetValue(PlacementProperty);
            set => 
                base.SetValue(PlacementProperty, value);
        }

        public bool ShowIndicator
        {
            get => 
                (bool) base.GetValue(ShowIndicatorProperty);
            set => 
                base.SetValue(ShowIndicatorProperty, value);
        }

        public UIElement IndicatorTarget
        {
            get => 
                (UIElement) base.GetValue(IndicatorTargetProperty);
            set => 
                base.SetValue(IndicatorTargetProperty, value);
        }

        public HorizontalAlignment? IndicatorHorizontalAlignment
        {
            get => 
                (HorizontalAlignment?) base.GetValue(IndicatorHorizontalAlignmentProperty);
            set => 
                base.SetValue(IndicatorHorizontalAlignmentProperty, value);
        }

        public VerticalAlignment? IndicatorVerticalAlignment
        {
            get => 
                (VerticalAlignment?) base.GetValue(IndicatorVerticalAlignmentProperty);
            set => 
                base.SetValue(IndicatorVerticalAlignmentProperty, value);
        }
    }
}

