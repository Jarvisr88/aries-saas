namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;

    public interface IAnimationConnector
    {
        void Disconnect();
        void OnAnimationSeriesEnd();
        void UpdateAppearance();

        AnimationElement ConnectedElement { get; set; }
    }
}

