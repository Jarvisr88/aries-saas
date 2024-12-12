namespace DevExpress.XtraPrinting.Native.Interaction
{
    using System;

    public interface IInteractionServiceBase
    {
        void Reset();

        bool IsInteracting { get; set; }
    }
}

