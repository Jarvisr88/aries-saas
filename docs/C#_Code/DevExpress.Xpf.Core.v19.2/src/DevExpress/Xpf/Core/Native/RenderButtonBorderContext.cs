namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderButtonBorderContext : RenderControlContext
    {
        private Dock? placement;
        private string mouseState;
        private string defaultState;

        public RenderButtonBorderContext(RenderButtonBorder factory);
        private void ForwardingIsPressed();
        protected virtual void OnDefaultStateChanged();
        protected virtual void OnMouseStateChanged();
        protected virtual void OnPlacementChanged();
        protected override void UpdateCommonState();
        public override void UpdateStates();

        public Dock? Placement { get; set; }

        public string MouseState { get; set; }

        public string DefaultState { get; set; }
    }
}

