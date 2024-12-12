namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;

    public interface IDataUpdateAnimationProvider
    {
        IList<IList<AnimationTimeline>> GetAnimations(Func<DataUpdate> dataUpdateAccessor, IConditionalFormattingClientBase client);
    }
}

