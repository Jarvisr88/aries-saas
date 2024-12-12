namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;

    public interface ICardsPanelOwner
    {
        void ActualizePanels();

        List<CardsPanel> Panels { get; }
    }
}

