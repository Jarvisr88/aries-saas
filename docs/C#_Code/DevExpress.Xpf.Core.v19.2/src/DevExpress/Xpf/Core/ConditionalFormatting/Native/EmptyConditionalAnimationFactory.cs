namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections.Generic;

    internal sealed class EmptyConditionalAnimationFactory : IConditionalAnimationFactory
    {
        public IList<SequentialAnimationTimeline> CreateAnimations() => 
            null;

        public void UpdateContext(DataUpdate update)
        {
        }

        public void UpdateDefaultSettings(DefaultAnimationSettings? settings)
        {
        }
    }
}

