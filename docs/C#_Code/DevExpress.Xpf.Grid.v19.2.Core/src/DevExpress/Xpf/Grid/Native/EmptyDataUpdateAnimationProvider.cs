namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;

    internal sealed class EmptyDataUpdateAnimationProvider : IDataUpdateAnimationProvider
    {
        public IList<IList<AnimationTimeline>> GetAnimations(Func<DataUpdate> dataUpdateAccessor, IConditionalFormattingClientBase client) => 
            null;
    }
}

