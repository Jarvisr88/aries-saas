namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    [StructLayout(LayoutKind.Sequential)]
    public struct DefaultAnimationSettings
    {
        private readonly Duration conditionalFormattingAnimationDurationCore;
        private readonly Duration triggerAnimationShowDurationCore;
        private readonly Duration triggerAnimationHoldDurationCore;
        private readonly Duration triggerAnimationHideDurationCore;
        private readonly bool useConstantDataBarAnimationSpeedCore;
        public Duration ConditionalFormattingAnimationDuration =>
            this.conditionalFormattingAnimationDurationCore;
        public Duration TriggerAnimationShowDuration =>
            this.triggerAnimationShowDurationCore;
        public Duration TriggerAnimationHoldDuration =>
            this.triggerAnimationHoldDurationCore;
        public Duration TriggerAnimationHideDuration =>
            this.triggerAnimationHideDurationCore;
        public bool UseConstantDataBarAnimationSpeed =>
            this.useConstantDataBarAnimationSpeedCore;
        public DefaultAnimationSettings(Duration conditionalFormattingAnimationDuration, Duration triggerAnimationShowDuration, Duration triggerAnimationHoldDuration, Duration triggerAnimationHideDuration, bool useConstantDataBarAnimationSpeed)
        {
            this.conditionalFormattingAnimationDurationCore = conditionalFormattingAnimationDuration;
            this.triggerAnimationShowDurationCore = triggerAnimationShowDuration;
            this.triggerAnimationHoldDurationCore = triggerAnimationHoldDuration;
            this.triggerAnimationHideDurationCore = triggerAnimationHideDuration;
            this.useConstantDataBarAnimationSpeedCore = useConstantDataBarAnimationSpeed;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DefaultAnimationSettings))
            {
                return false;
            }
            DefaultAnimationSettings settings = (DefaultAnimationSettings) obj;
            return ((settings.ConditionalFormattingAnimationDuration == this.ConditionalFormattingAnimationDuration) && ((settings.TriggerAnimationShowDuration == this.TriggerAnimationShowDuration) && ((settings.TriggerAnimationHoldDuration == this.TriggerAnimationHoldDuration) && ((settings.TriggerAnimationHideDuration == this.TriggerAnimationHideDuration) && (settings.UseConstantDataBarAnimationSpeed == this.UseConstantDataBarAnimationSpeed)))));
        }

        public override int GetHashCode() => 
            (((((((((0x11 * 0x17) + this.ConditionalFormattingAnimationDuration.GetHashCode()) * 0x17) + this.TriggerAnimationShowDuration.GetHashCode()) * 0x17) + this.TriggerAnimationHoldDuration.GetHashCode()) * 0x17) + this.TriggerAnimationHideDuration.GetHashCode()) * 0x17) + this.UseConstantDataBarAnimationSpeed.GetHashCode();
    }
}

