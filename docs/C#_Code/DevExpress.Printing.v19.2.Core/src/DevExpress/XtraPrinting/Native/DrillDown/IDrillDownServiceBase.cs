namespace DevExpress.XtraPrinting.Native.DrillDown
{
    using DevExpress.XtraPrinting.Native.Interaction;
    using System;
    using System.Collections.Generic;

    public interface IDrillDownServiceBase : IInteractionServiceBase
    {
        [Obsolete("Use the IsInteracting property instead.")]
        bool IsDrillDowning { get; set; }

        IDictionary<DrillDownKey, bool> Keys { get; }
    }
}

