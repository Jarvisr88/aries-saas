namespace DevExpress.Mvvm.UI.Interactivity
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public static class Interaction
    {
        private const string BehaviorsPropertyName = "BehaviorsInternal";
        private const string BehaviorsTemplatePropertyName = "BehaviorsTemplate";
        private const string TriggersPropertyName = "TriggersInternal";
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached("BehaviorsInternal", typeof(BehaviorCollection), typeof(Interaction), new PropertyMetadata(null, new PropertyChangedCallback(Interaction.OnCollectionChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty BehaviorsTemplateProperty = DependencyProperty.RegisterAttached("BehaviorsTemplate", typeof(DataTemplate), typeof(Interaction), new PropertyMetadata(null, new PropertyChangedCallback(Interaction.OnBehaviorsTemplateChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BehaviorsTemplateItemsProperty = DependencyProperty.RegisterAttached("BehaviorsTemplateItems", typeof(IList<Behavior>), typeof(Interaction), new PropertyMetadata(null));
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This property is obsolete. Use the Behaviors property instead."), IgnoreDependencyPropertiesConsistencyChecker, Browsable(false)]
        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached("TriggersInternal", typeof(DevExpress.Mvvm.UI.Interactivity.TriggerCollection), typeof(Interaction), new PropertyMetadata(null, new PropertyChangedCallback(Interaction.OnCollectionChanged)));

        public static BehaviorCollection GetBehaviors(DependencyObject d)
        {
            BehaviorCollection behaviors = (BehaviorCollection) d.GetValue(BehaviorsProperty);
            if (behaviors == null)
            {
                behaviors = new BehaviorCollection();
                d.SetValue(BehaviorsProperty, behaviors);
            }
            return behaviors;
        }

        public static DataTemplate GetBehaviorsTemplate(DependencyObject d) => 
            (DataTemplate) d.GetValue(BehaviorsProperty);

        [Browsable(false), Obsolete("This method is obsolete. Use the GetBehaviors method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public static DevExpress.Mvvm.UI.Interactivity.TriggerCollection GetTriggers(DependencyObject d)
        {
            DevExpress.Mvvm.UI.Interactivity.TriggerCollection triggers = (DevExpress.Mvvm.UI.Interactivity.TriggerCollection) d.GetValue(TriggersProperty);
            if (triggers == null)
            {
                triggers = new DevExpress.Mvvm.UI.Interactivity.TriggerCollection();
                d.SetValue(TriggersProperty, triggers);
            }
            return triggers;
        }

        private static void OnBehaviorsTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BehaviorCollection behaviors = GetBehaviors(d);
            IList<Behavior> list = d.GetValue(BehaviorsTemplateItemsProperty) as IList<Behavior>;
            DataTemplate newValue = e.NewValue as DataTemplate;
            if (list != null)
            {
                foreach (Behavior behavior in list)
                {
                    if (behaviors.Contains(behavior))
                    {
                        behaviors.Remove(behavior);
                    }
                }
            }
            if (newValue == null)
            {
                d.SetValue(BehaviorsTemplateItemsProperty, null);
            }
            else
            {
                IList<Behavior> list2;
                if (!newValue.IsSealed)
                {
                    newValue.Seal();
                }
                DependencyObject obj2 = newValue.LoadContent();
                if (obj2 is ContentControl)
                {
                    list2 = new List<Behavior>();
                    Behavior content = ((ContentControl) obj2).Content as Behavior;
                    ((ContentControl) obj2).Content = null;
                    if (content != null)
                    {
                        list2.Add(content);
                    }
                }
                else
                {
                    if (!(obj2 is ItemsControl))
                    {
                        throw new InvalidOperationException("Use ContentControl or ItemsControl in the template to specify Behaviors.");
                    }
                    ItemsControl control = obj2 as ItemsControl;
                    list2 = control.Items.OfType<Behavior>().ToList<Behavior>();
                    control.Items.Clear();
                    control.ItemsSource = null;
                }
                d.SetValue(BehaviorsTemplateItemsProperty, (list2.Count > 0) ? list2 : null);
                foreach (Behavior behavior3 in list2)
                {
                    behaviors.Add(behavior3);
                }
            }
        }

        private static void OnCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IAttachableObject oldValue = (IAttachableObject) e.OldValue;
            IAttachableObject newValue = (IAttachableObject) e.NewValue;
            if (!ReferenceEquals(oldValue, newValue))
            {
                if ((oldValue != null) && (oldValue.AssociatedObject != null))
                {
                    oldValue.Detach();
                }
                if ((newValue != null) && (d != null))
                {
                    if (newValue.AssociatedObject != null)
                    {
                        throw new InvalidOperationException();
                    }
                    newValue.Attach(d);
                }
            }
        }

        public static void SetBehaviorsTemplate(DependencyObject d, DataTemplate template)
        {
            d.SetValue(BehaviorsTemplateProperty, template);
        }
    }
}

