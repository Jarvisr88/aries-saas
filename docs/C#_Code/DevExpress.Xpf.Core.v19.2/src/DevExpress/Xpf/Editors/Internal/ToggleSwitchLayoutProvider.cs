namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ToggleSwitchLayoutProvider : LayoutProvider
    {
        public ToggleSwitchLayoutProvider(ToggleSwitch owner)
        {
            this.Owner = owner;
            this.Strategy = this.CreateStrategy();
        }

        public override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context) => 
            this.Strategy.Arrange(finalSize, context);

        private ToggleSwitchLayoutStrategyBase CreateStrategy()
        {
            ToggleSwitchLayoutStrategyBase base2 = null;
            switch (this.Owner.ContentPlacement)
            {
                case ToggleSwitchContentPlacement.Far:
                    base2 = new FarPlacementLayoutStrategy();
                    break;

                case ToggleSwitchContentPlacement.Both:
                    base2 = new BothPlacementLayoutStrategy();
                    break;

                case ToggleSwitchContentPlacement.Inside:
                    base2 = new CenterPlacementLayoutStrategy();
                    break;

                default:
                    base2 = new NearPlacementLayoutStrategy();
                    break;
            }
            base2.Owner = this.Owner;
            return base2;
        }

        public override Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context) => 
            this.Strategy.Measure(availableSize, context);

        private ToggleSwitch Owner { get; set; }

        private ToggleSwitchLayoutStrategyBase Strategy { get; set; }
    }
}

