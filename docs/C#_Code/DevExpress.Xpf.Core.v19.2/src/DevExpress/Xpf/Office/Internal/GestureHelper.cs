namespace DevExpress.Xpf.Office.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class GestureHelper
    {
        private readonly IGestureClient client;
        private bool prevIsManipulationEnabled;
        private UIElement element;
        private GestureHelperState state;

        public GestureHelper(IGestureClient client)
        {
            Guard.ArgumentNotNull(client, "client");
            this.client = client;
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            this.SwitchToDefaultState();
        }

        private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            this.State.OnManipulationDelta(e);
        }

        private void OnManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            this.State.OnManipulationInertiaStarting(e);
        }

        private void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            this.State.OnManipulationStarted(e);
        }

        private void OnManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            this.State.OnManipulationStarting(e);
        }

        private void OnTouchDown(object sender, TouchEventArgs e)
        {
            this.State.OnTouchDown(e);
        }

        private void OnTouchUp(object sender, TouchEventArgs e)
        {
            this.State.OnTouchUp(e);
        }

        public void Start(UIElement element)
        {
            this.Stop();
            this.element = element;
            if (element != null)
            {
                this.prevIsManipulationEnabled = element.IsManipulationEnabled;
                element.IsManipulationEnabled = true;
                this.SubscribeEvents();
            }
            this.SwitchToDefaultState();
        }

        public void Stop()
        {
            if (this.element != null)
            {
                this.UnsubscribeEvents();
                this.element.IsManipulationEnabled = this.prevIsManipulationEnabled;
            }
        }

        protected internal virtual void SubscribeEvents()
        {
            this.element.TouchDown += new EventHandler<TouchEventArgs>(this.OnTouchDown);
            this.element.TouchUp += new EventHandler<TouchEventArgs>(this.OnTouchUp);
            this.element.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(this.OnManipulationStarting);
            this.element.ManipulationInertiaStarting += new EventHandler<ManipulationInertiaStartingEventArgs>(this.OnManipulationInertiaStarting);
            this.element.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
            this.element.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnManipulationDelta);
            this.element.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
        }

        public void SwitchState(GestureHelperState newState)
        {
            if (this.state != null)
            {
                this.state.Finish();
            }
            this.state = newState;
            this.state.Start();
        }

        internal void SwitchToDefaultState()
        {
            this.SwitchState(new InitialGestureHelperState(this));
        }

        protected internal virtual void UnsubscribeEvents()
        {
            this.element.TouchDown -= new EventHandler<TouchEventArgs>(this.OnTouchDown);
            this.element.TouchUp -= new EventHandler<TouchEventArgs>(this.OnTouchUp);
            this.element.ManipulationStarting -= new EventHandler<ManipulationStartingEventArgs>(this.OnManipulationStarting);
            this.element.ManipulationInertiaStarting -= new EventHandler<ManipulationInertiaStartingEventArgs>(this.OnManipulationInertiaStarting);
            this.element.ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.OnManipulationDelta);
            this.element.ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
        }

        public GestureHelperState State =>
            this.state;

        public IGestureClient Client =>
            this.client;
    }
}

