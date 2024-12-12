namespace DevExpress.Mvvm.UI.Interactivity
{
    using System;

    public class EventTrigger : EventTriggerBase<DependencyObject>
    {
        public EventTrigger()
        {
        }

        public EventTrigger(string eventName) : this()
        {
            base.EventName = eventName;
        }
    }
}

