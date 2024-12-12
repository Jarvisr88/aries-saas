namespace DevExpress.Mvvm.UI.Interactivity.Internal
{
    using System;

    internal class EventTriggerGenericHandler<TSender, TArgs>
    {
        private readonly Action<object, object> action;

        public EventTriggerGenericHandler(Action<object, object> action)
        {
            this.action = action;
        }

        public void Handler(TSender sender, TArgs args)
        {
            this.action(sender, args);
        }
    }
}

