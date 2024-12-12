namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class DataObjectBase : DependencyObject, INotifyContentChanged, INotifyPropertyChanged
    {
        public static readonly DependencyProperty DataObjectProperty = DependencyPropertyManager.RegisterAttached("DataObject", typeof(DataObjectBase), typeof(DataObjectBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(DataObjectBase.OnDataObjectChanged)));
        public static readonly DependencyProperty NeedsResetEventProperty = DependencyPropertyManager.RegisterAttached("NeedsResetEvent", typeof(bool), typeof(DataObjectBase), new PropertyMetadata(false, new PropertyChangedCallback(DataObjectBase.OnNeedsResetEventChanged)));
        public static readonly DependencyProperty RaiseResetEventWhenObjectIsLoadedProperty = DependencyPropertyManager.RegisterAttached("RaiseResetEventWhenObjectIsLoaded", typeof(bool), typeof(DataObjectBase), new PropertyMetadata(false));
        public static readonly RoutedEvent ResetEvent = EventManager.RegisterRoutedEvent("Reset", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DataObjectBase));
        private List<FrameworkElement> elements = new List<FrameworkElement>();

        public event EventHandler ContentChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public static void AddResetHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.AddHandler(ResetEvent, handler, false);
            }
        }

        public static DataObjectBase GetDataObject(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (DataObjectBase) element.GetValue(DataObjectProperty);
        }

        public static bool GetNeedsResetEvent(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(NeedsResetEventProperty);
        }

        public static bool GetRaiseResetEventWhenObjectIsLoaded(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(RaiseResetEventWhenObjectIsLoadedProperty);
        }

        protected static void OnDataObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetNeedsResetEvent(d))
            {
                if (e.OldValue != null)
                {
                    ((DataObjectBase) e.OldValue).elements.Remove((FrameworkElement) d);
                }
                if (e.NewValue != null)
                {
                    ((DataObjectBase) e.NewValue).elements.Add((FrameworkElement) d);
                }
            }
        }

        private void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;
            if (item != null)
            {
                item.Loaded -= new RoutedEventHandler(this.OnElementLoaded);
                RaiseResetEvent(item);
            }
        }

        protected static void OnNeedsResetEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataObjectBase dataObject = GetDataObject(d);
            if (dataObject != null)
            {
                if (GetNeedsResetEvent(d))
                {
                    dataObject.elements.Add((FrameworkElement) d);
                }
                else
                {
                    dataObject.elements.Remove((FrameworkElement) d);
                }
            }
        }

        protected virtual void RaiseContentChanged()
        {
            if (this.ContentChanged != null)
            {
                this.ContentChanged(this, EventArgs.Empty);
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static void RaiseResetEvent(FrameworkElement item)
        {
            RoutedEventArgs e = new RoutedEventArgs();
            e.RoutedEvent = ResetEvent;
            item.RaiseEvent(e);
            IDataObjectReset reset = item as IDataObjectReset;
            if (reset != null)
            {
                reset.Reset();
            }
        }

        public void RaiseResetEvents()
        {
            foreach (FrameworkElement element in this.elements)
            {
                if (GetRaiseResetEventWhenObjectIsLoaded(element) && !FrameworkElementHelper.GetIsLoaded(element))
                {
                    element.Loaded += new RoutedEventHandler(this.OnElementLoaded);
                    continue;
                }
                RaiseResetEvent(element);
            }
        }

        public static void RemoveResetHandler(DependencyObject dObj, RoutedEventHandler handler)
        {
            UIElement element = dObj as UIElement;
            if (element != null)
            {
                element.RemoveHandler(ResetEvent, handler);
            }
        }

        public static void SetDataObject(DependencyObject element, DataObjectBase value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(DataObjectProperty, value);
        }

        public static void SetNeedsResetEvent(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(NeedsResetEventProperty, value);
        }

        public static void SetRaiseResetEventWhenObjectIsLoaded(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(RaiseResetEventWhenObjectIsLoadedProperty, value);
        }
    }
}

