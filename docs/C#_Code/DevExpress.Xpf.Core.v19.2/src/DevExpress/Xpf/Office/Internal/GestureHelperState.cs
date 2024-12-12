namespace DevExpress.Xpf.Office.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Windows.Input;

    public abstract class GestureHelperState
    {
        private readonly GestureHelper helper;

        protected GestureHelperState(GestureHelper helper)
        {
            Guard.ArgumentNotNull(helper, "helper");
            this.helper = helper;
        }

        public virtual void Finish()
        {
        }

        public virtual void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
        }

        public virtual void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
        }

        public virtual void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
        }

        public virtual void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
        }

        public virtual void OnTouchDown(TouchEventArgs e)
        {
        }

        public virtual void OnTouchUp(TouchEventArgs e)
        {
        }

        public virtual void Start()
        {
        }

        protected GestureHelper Helper =>
            this.helper;

        public IGestureClient Client =>
            this.Helper.Client;
    }
}

