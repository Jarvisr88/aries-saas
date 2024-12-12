namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface IRenderPropertyContext
    {
        void Attach(Namescope scope, RenderTriggerContextBase context);
        void Detach();
        void Reset(RenderValueSource valueSource);
    }
}

