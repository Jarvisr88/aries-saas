namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    public class AnimationTriggerContext
    {
        private readonly DevExpress.Xpf.Core.ConditionalFormatting.Native.DataUpdate dataUpdateCore;
        private readonly Func<CustomDataUpdateFormatConditionEventArgsSource, bool> raiseCustomDataUpdateFormatConditionCore;

        public AnimationTriggerContext(DevExpress.Xpf.Core.ConditionalFormatting.Native.DataUpdate dataUpdate, Func<CustomDataUpdateFormatConditionEventArgsSource, bool> raiseCustomDataUpdateFormatCondition)
        {
            if (dataUpdate == null)
            {
                throw new ArgumentNullException("dataUpdate");
            }
            if (raiseCustomDataUpdateFormatCondition == null)
            {
                throw new ArgumentNullException("customTrigger");
            }
            this.dataUpdateCore = dataUpdate;
            this.raiseCustomDataUpdateFormatConditionCore = raiseCustomDataUpdateFormatCondition;
        }

        public DevExpress.Xpf.Core.ConditionalFormatting.Native.DataUpdate DataUpdate =>
            this.dataUpdateCore;

        public Func<CustomDataUpdateFormatConditionEventArgsSource, bool> RaiseCustomDataUpdateFormatCondition =>
            this.raiseCustomDataUpdateFormatConditionCore;
    }
}

