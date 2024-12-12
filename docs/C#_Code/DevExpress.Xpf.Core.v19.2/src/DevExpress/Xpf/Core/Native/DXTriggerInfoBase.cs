namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class DXTriggerInfoBase : FrameworkElement
    {
        protected DXTriggerInfoBase();

        public ITargetPropertyResolverFactory TargetPropertyResolverFactory { get; set; }

        internal DXConditionCollection TriggersConditions { get; set; }

        public string VisualState { get; set; }

        public string VisualStateNormal { get; set; }

        public DXSetterCollection Setters { get; set; }
    }
}

