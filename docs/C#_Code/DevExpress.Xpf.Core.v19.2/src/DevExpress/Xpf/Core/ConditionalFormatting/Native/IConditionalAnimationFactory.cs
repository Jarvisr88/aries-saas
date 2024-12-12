namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections.Generic;

    public interface IConditionalAnimationFactory
    {
        IList<SequentialAnimationTimeline> CreateAnimations();
        void UpdateContext(DataUpdate update);
        void UpdateDefaultSettings(DefaultAnimationSettings? settings);
    }
}

