namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface IPropertyChangedListener
    {
        void Flush();
        void Resume();
        void SubscribeValueChanged(object target, RenderPropertyChangedListenerContext context);
        void SubscribeValueChangedAsync(RenderPropertyChangedListenerContext context);
        void Suspend();
        void UnsubscribeValueChanged(object target, RenderPropertyChangedListenerContext context);
    }
}

