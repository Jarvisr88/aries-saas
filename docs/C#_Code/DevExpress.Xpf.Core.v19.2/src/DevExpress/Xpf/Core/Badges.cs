namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal static class Badges
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty BadgeAndShowModeProperty;

        static Badges()
        {
            BadgeAndShowModeProperty = DependencyProperty.RegisterAttached("BadgeAndShowMode", typeof(Tuple<Badge, BadgeBlendingMode>), typeof(Badge), new PropertyMetadata(null, (o, args) => BadgeWorkerBase.Update(o, args.OldValue as Tuple<Badge, BadgeBlendingMode>, args.NewValue as Tuple<Badge, BadgeBlendingMode>), (CoerceValueCallback) ((o, v) => Tuple.Create<Badge, BadgeBlendingMode>(Badge.GetBadge(o), BadgeProperties.GetBlendingMode(o)))));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Badges.<>c <>9 = new Badges.<>c();

            internal void <.cctor>b__1_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                BadgeWorkerBase.Update(o, args.OldValue as Tuple<Badge, BadgeBlendingMode>, args.NewValue as Tuple<Badge, BadgeBlendingMode>);
            }

            internal object <.cctor>b__1_1(DependencyObject o, object v) => 
                Tuple.Create<Badge, BadgeBlendingMode>(Badge.GetBadge(o), BadgeProperties.GetBlendingMode(o));
        }
    }
}

