namespace DevExpress.Printing.Core.ReportServer.Services
{
    using DevExpress.XtraPrinting.Native.DrillDown;
    using DevExpress.XtraPrinting.Native.Interaction;
    using System;
    using System.Collections.Generic;

    internal class RemoteDrillDownService : IDrillDownServiceBase, IInteractionServiceBase
    {
        private readonly Dictionary<DrillDownKey, bool> keys = new Dictionary<DrillDownKey, bool>();

        void IInteractionServiceBase.Reset()
        {
        }

        public IDictionary<DrillDownKey, bool> Keys =>
            this.keys;

        bool IDrillDownServiceBase.IsDrillDowning
        {
            get => 
                false;
            set
            {
            }
        }

        bool IInteractionServiceBase.IsInteracting
        {
            get => 
                false;
            set
            {
            }
        }
    }
}

