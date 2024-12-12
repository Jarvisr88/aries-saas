namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls;

    public class InputEventsHelper
    {
        public static readonly DependencyProperty RaiseButtonClickOnLeftMouseLeftButtonUpProperty;
        public static readonly RoutedEvent MouseLeftButtonDownEvent;
        public static readonly RoutedEvent MouseLeftButtonUpEvent;
        public static readonly RoutedEvent MouseMoveEvent;

        static InputEventsHelper()
        {
            Type ownerType = typeof(InputEventsHelper);
            MouseLeftButtonDownEvent = EventManager.RegisterRoutedEvent("MouseLeftButtonDown", RoutingStrategy.Direct, typeof(IndependentMouseButtonEventHandler), ownerType);
            MouseLeftButtonUpEvent = EventManager.RegisterRoutedEvent("MouseLeftButtonUp", RoutingStrategy.Direct, typeof(IndependentMouseButtonEventHandler), ownerType);
            MouseMoveEvent = EventManager.RegisterRoutedEvent("MouseMove", RoutingStrategy.Direct, typeof(IndependentMouseEventHandler), ownerType);
            RaiseButtonClickOnLeftMouseLeftButtonUpProperty = DependencyProperty.RegisterAttached("RaiseButtonClickOnLeftMouseLeftButtonUp", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(InputEventsHelper.OnRaiseButtonClickOnLeftMouseLeftButtonUpChanged)));
        }

        public static void AddMouseLeftButtonDownHandler(DependencyObject dObj, IndependentMouseButtonEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(MouseLeftButtonDownEvent, handler);
            }
        }

        public static void AddMouseLeftButtonUpHandler(DependencyObject dObj, IndependentMouseButtonEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(MouseLeftButtonUpEvent, handler);
            }
        }

        public static void AddMouseMoveHandler(DependencyObject dObj, IndependentMouseEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(MouseMoveEvent, handler);
            }
        }

        public static bool GetRaiseButtonClickOnLeftMouseLeftButtonUp(Button obj) => 
            (bool) obj.GetValue(RaiseButtonClickOnLeftMouseLeftButtonUpProperty);

        private static void OnButtonMouseLeftButtonUp(object sender, IndependentMouseButtonEventArgs e)
        {
            ((UIElementAutomationPeer.CreatePeerForElement((Button) sender) as ButtonBaseAutomationPeer).GetPattern(PatternInterface.Invoke) as IInvokeProvider).Invoke();
        }

        private static void OnRaiseButtonClickOnLeftMouseLeftButtonUpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button dObj = d as Button;
            bool newValue = (bool) e.NewValue;
            if (dObj != null)
            {
                if (newValue)
                {
                    AddMouseLeftButtonUpHandler(dObj, new IndependentMouseButtonEventHandler(InputEventsHelper.OnButtonMouseLeftButtonUp));
                }
                else
                {
                    RemoveMouseLeftButtonUpHandler(dObj, new IndependentMouseButtonEventHandler(InputEventsHelper.OnButtonMouseLeftButtonUp));
                }
            }
        }

        public static void RemoveMouseLeftButtonDownHandler(DependencyObject dObj, IndependentMouseButtonEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(MouseLeftButtonDownEvent, handler);
            }
        }

        public static void RemoveMouseLeftButtonUpHandler(DependencyObject dObj, IndependentMouseButtonEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(MouseLeftButtonUpEvent, handler);
            }
        }

        public static void RemoveMouseMoveHandler(DependencyObject dObj, IndependentMouseEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(MouseMoveEvent, handler);
            }
        }

        public static void SetRaiseButtonClickOnLeftMouseLeftButtonUp(Button obj, bool value)
        {
            obj.SetValue(RaiseButtonClickOnLeftMouseLeftButtonUpProperty, value);
        }
    }
}

