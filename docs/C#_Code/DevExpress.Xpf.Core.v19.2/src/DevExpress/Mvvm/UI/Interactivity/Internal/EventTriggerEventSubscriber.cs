namespace DevExpress.Mvvm.UI.Interactivity.Internal
{
    using System;
    using System.Reflection;
    using System.Windows;

    public class EventTriggerEventSubscriber
    {
        private Action<object, object> EventHandler;
        private Delegate subscribedEventHandler;

        public EventTriggerEventSubscriber(Action<object, object> eventHandler)
        {
            this.EventHandler = eventHandler;
        }

        public Delegate CreateEventHandler(Type eventHandlerType)
        {
            if (!this.IsEventCorrect(eventHandlerType))
            {
                return null;
            }
            ParameterInfo[] parameters = this.GetParameters(eventHandlerType);
            Type[] typeArguments = new Type[] { parameters[0].ParameterType, parameters[1].ParameterType };
            object[] args = new object[] { this.EventHandler };
            object firstArgument = Activator.CreateInstance(typeof(EventTriggerGenericHandler<,>).MakeGenericType(typeArguments), args);
            return Delegate.CreateDelegate(eventHandlerType, firstArgument, firstArgument.GetType().GetMethod("Handler"));
        }

        private ParameterInfo[] GetParameters(Type eventHandlerType) => 
            eventHandlerType.GetMethod("Invoke").GetParameters();

        private bool IsEventCorrect(Type eventHandlerType)
        {
            if (!typeof(Delegate).IsAssignableFrom(eventHandlerType))
            {
                return false;
            }
            ParameterInfo[] parameters = this.GetParameters(eventHandlerType);
            return ((parameters.Length == 2) ? (typeof(object).IsAssignableFrom(parameters[0].ParameterType) ? typeof(object).IsAssignableFrom(parameters[1].ParameterType) : false) : false);
        }

        public void SubscribeToEvent(object obj, string eventName)
        {
            if ((obj != null) && !string.IsNullOrEmpty(eventName))
            {
                EventInfo info = obj.GetType().GetEvent(eventName);
                if (info != null)
                {
                    this.subscribedEventHandler = this.CreateEventHandler(info.EventHandlerType);
                    if (this.subscribedEventHandler != null)
                    {
                        info.AddEventHandler(obj, this.subscribedEventHandler);
                    }
                }
            }
        }

        public void SubscribeToEvent(object obj, RoutedEvent routedEvent)
        {
            UIElement element = obj as UIElement;
            if ((element != null) && (routedEvent != null))
            {
                this.subscribedEventHandler = this.CreateEventHandler(routedEvent.HandlerType);
                if (this.subscribedEventHandler != null)
                {
                    element.AddHandler(routedEvent, this.subscribedEventHandler);
                }
            }
        }

        public void UnsubscribeFromEvent(object obj, string eventName)
        {
            if (((obj != null) && !string.IsNullOrEmpty(eventName)) && (this.subscribedEventHandler != null))
            {
                obj.GetType().GetEvent(eventName).RemoveEventHandler(obj, this.subscribedEventHandler);
                this.subscribedEventHandler = null;
            }
        }

        public void UnsubscribeFromEvent(object obj, RoutedEvent routedEvent)
        {
            UIElement element = obj as UIElement;
            if (((element != null) && (routedEvent != null)) && (this.subscribedEventHandler != null))
            {
                element.RemoveHandler(routedEvent, this.subscribedEventHandler);
                this.subscribedEventHandler = null;
            }
        }
    }
}

