namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.DragDrop.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ListBoxDragDropBehavior : Behavior<ListBoxEdit>
    {
        public static readonly DependencyProperty AllowDragDropProperty;
        public static readonly DependencyProperty ShowDragDropHintProperty;
        public static readonly DependencyProperty DropMarkerTemplateProperty;
        public static readonly DependencyProperty DragDropHintTemplateProperty;
        public static readonly DependencyProperty ShowTargetInfoInDragDropHintProperty;
        public static readonly RoutedEvent GiveRecordDragFeedbackEvent;
        public static readonly RoutedEvent ContinueRecordDragEvent;
        public static readonly RoutedEvent DragRecordOverEvent;
        public static readonly RoutedEvent DropRecordEvent;
        public static readonly RoutedEvent StartRecordDragEvent;
        public static readonly RoutedEvent CompleteRecordDragDropEvent;

        static ListBoxDragDropBehavior()
        {
            Type ownerType = typeof(ListBoxDragDropBehavior);
            AllowDragDropProperty = DependencyPropertyManager.RegisterAttached("AllowDragDrop", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(ListBoxDragDropBehavior.OnAllowDragDropChanged)));
            ShowDragDropHintProperty = DependencyPropertyManager.RegisterAttached("ShowDragDropHint", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(ListBoxDragDropBehavior.OnDragDropBehaviorChanged)));
            DropMarkerTemplateProperty = DependencyProperty.RegisterAttached("DropMarkerTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ListBoxDragDropBehavior.OnDragDropBehaviorChanged)));
            DragDropHintTemplateProperty = DependencyProperty.RegisterAttached("DragDropHintTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, new PropertyChangedCallback(ListBoxDragDropBehavior.OnDragDropBehaviorChanged)));
            ShowTargetInfoInDragDropHintProperty = DependencyProperty.RegisterAttached("ShowTargetInfoInDragDropHint", typeof(bool), ownerType, new PropertyMetadata(false));
            GiveRecordDragFeedbackEvent = EventManager.RegisterRoutedEvent("GiveRecordDragFeedback", RoutingStrategy.Direct, typeof(EventHandler<GiveRecordDragFeedbackEventArgs>), ownerType);
            ContinueRecordDragEvent = EventManager.RegisterRoutedEvent("ContinueRecordDrag", RoutingStrategy.Direct, typeof(EventHandler<ContinueRecordDragEventArgs>), ownerType);
            CompleteRecordDragDropEvent = EventManager.RegisterRoutedEvent("CompleteRecordDragDrop", RoutingStrategy.Direct, typeof(EventHandler<CompleteRecordDragDropEventArgs>), ownerType);
            DragRecordOverEvent = EventManager.RegisterRoutedEvent("DragRecordOver", RoutingStrategy.Direct, typeof(EventHandler<DragRecordOverEventArgs>), ownerType);
            DropRecordEvent = EventManager.RegisterRoutedEvent("DropRecord", RoutingStrategy.Direct, typeof(EventHandler<DropRecordEventArgs>), ownerType);
            StartRecordDragEvent = EventManager.RegisterRoutedEvent("StartRecordDrag", RoutingStrategy.Direct, typeof(EventHandler<StartRecordDragEventArgs>), ownerType);
        }

        public static void AddCompleteRecordDragDropHandler(DependencyObject d, EventHandler<CompleteRecordDragDropEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.AddHandler(CompleteRecordDragDropEvent, handler);
            }
        }

        public static void AddContinueRecordDragHandler(DependencyObject d, EventHandler<ContinueRecordDragEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.AddHandler(ContinueRecordDragEvent, handler);
            }
        }

        public static void AddDragRecordOverHandler(DependencyObject d, EventHandler<DragRecordOverEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.AddHandler(DragRecordOverEvent, handler);
            }
        }

        public static void AddDropRecordHandler(DependencyObject d, EventHandler<DropRecordEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.AddHandler(DropRecordEvent, handler);
            }
        }

        public static void AddGiveRecordDragFeedbackHandler(DependencyObject d, EventHandler<GiveRecordDragFeedbackEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.AddHandler(GiveRecordDragFeedbackEvent, handler);
            }
        }

        public static void AddStartRecordDragHandler(DependencyObject d, EventHandler<StartRecordDragEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.AddHandler(StartRecordDragEvent, handler);
            }
        }

        private DragManagerBuilder CreateDragManagerBuilder()
        {
            DragManagerBuilder builder1 = new DragManagerBuilder();
            builder1.CreateDataModifier = () => new ListDataModifier(this.ListBox);
            builder1.CreateDragDropInfoFactory = () => new ListDragDropInfoFactory(this.ListBox);
            builder1.GetDropMarkerTemplate = () => this.GetResourceTemplate(new Func<DependencyObject, DataTemplate>(ListBoxDragDropBehavior.GetDropMarkerTemplate), DragDropThemeKeys.DropMarker);
            builder1.GetDragDropHintTemplate = () => this.GetResourceTemplate(new Func<DependencyObject, DataTemplate>(ListBoxDragDropBehavior.GetDragDropHintTemplate), DragDropThemeKeys.DragDropHint);
            builder1.ShowDragDropHint = GetShowDragDropHint(this.ListBox);
            builder1.CreateDragEventFactory = () => new ListDragDataTransferEventFactory(this.ListBox);
            builder1.GetShowTargetInfoInDragDropHint = () => GetShowTargetInfoInDragDropHint(this.ListBox);
            return builder1;
        }

        public static bool GetAllowDragDrop(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(AllowDragDropProperty);
        }

        public static DataTemplate GetDragDropHintTemplate(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (DataTemplate) element.GetValue(DragDropHintTemplateProperty);
        }

        public static DataTemplate GetDropMarkerTemplate(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (DataTemplate) element.GetValue(DropMarkerTemplateProperty);
        }

        private DataTemplate GetResourceTemplate(Func<DependencyObject, DataTemplate> getCustomTemplate, DragDropThemeKeys defaultKey)
        {
            if (this.ListBox == null)
            {
                return null;
            }
            DataTemplate template = null;
            template = getCustomTemplate(this.ListBox);
            if (template == null)
            {
                DragDropThemeKeyExtension resourceKey = new DragDropThemeKeyExtension();
                resourceKey.ResourceKey = defaultKey;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this.ListBox);
                template = this.ListBox.TryFindResource(resourceKey) as DataTemplate;
            }
            return template;
        }

        public static bool GetShowDragDropHint(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(ShowDragDropHintProperty);
        }

        public static bool GetShowTargetInfoInDragDropHint(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(ShowTargetInfoInDragDropHintProperty);
        }

        private static void OnAllowDragDropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListBoxEdit edit = d as ListBoxEdit;
            if (edit != null)
            {
                BehaviorCollection source = Interaction.GetBehaviors(edit);
                Func<Behavior, bool> predicate = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<Behavior, bool> local1 = <>c.<>9__12_0;
                    predicate = <>c.<>9__12_0 = x => x is ListBoxDragDropBehavior;
                }
                Behavior behavior = source.FirstOrDefault<Behavior>(predicate);
                if (behavior != null)
                {
                    source.Remove(behavior);
                }
                if ((bool) e.NewValue)
                {
                    source.Add(new ListBoxDragDropBehavior());
                }
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.RebuildDragManager();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.RebuildDragManager(false);
        }

        private static void OnDragDropBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Func<Behavior, bool> predicate = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<Behavior, bool> local1 = <>c.<>9__13_0;
                predicate = <>c.<>9__13_0 = x => x is ListBoxDragDropBehavior;
            }
            Action<ListBoxDragDropBehavior> action = <>c.<>9__13_1;
            if (<>c.<>9__13_1 == null)
            {
                Action<ListBoxDragDropBehavior> local2 = <>c.<>9__13_1;
                action = <>c.<>9__13_1 = x => x.RebuildDragManager();
            }
            (Interaction.GetBehaviors(d).FirstOrDefault<Behavior>(predicate) as ListBoxDragDropBehavior).Do<ListBoxDragDropBehavior>(action);
        }

        internal void RebuildDragManager()
        {
            this.RebuildDragManager((this.ListBox != null) && GetAllowDragDrop(this.ListBox));
        }

        private void RebuildDragManager(bool allowDragDrop)
        {
            Action<NativeDragManager> action = <>c.<>9__45_0;
            if (<>c.<>9__45_0 == null)
            {
                Action<NativeDragManager> local1 = <>c.<>9__45_0;
                action = <>c.<>9__45_0 = x => x.IsActive = false;
            }
            this.DragDropManager.Do<NativeDragManager>(action);
            this.DragDropManager = null;
            if (((this.ListBox != null) & allowDragDrop) && !this.IsDesignTime)
            {
                this.DragDropManager = this.CreateDragManagerBuilder().Build(this.ListBox);
                this.DragDropManager.IsActive = true;
            }
        }

        public static void RemoveCompleteRecordDragDropHandler(DependencyObject d, EventHandler<CompleteRecordDragDropEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.RemoveHandler(CompleteRecordDragDropEvent, handler);
            }
        }

        public static void RemoveContinueRecordDragHandler(DependencyObject d, EventHandler<ContinueRecordDragEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.RemoveHandler(ContinueRecordDragEvent, handler);
            }
        }

        public static void RemoveDragRecordOverHandler(DependencyObject d, EventHandler<DragRecordOverEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.RemoveHandler(DragRecordOverEvent, handler);
            }
        }

        public static void RemoveDropRecordHandler(DependencyObject d, EventHandler<DropRecordEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.RemoveHandler(DropRecordEvent, handler);
            }
        }

        public static void RemoveGiveRecordDragFeedbackHandler(DependencyObject d, EventHandler<GiveRecordDragFeedbackEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.RemoveHandler(GiveRecordDragFeedbackEvent, handler);
            }
        }

        public static void RemoveStartRecordDragHandler(DependencyObject d, EventHandler<StartRecordDragEventArgs> handler)
        {
            UIElement element = d as UIElement;
            if (element != null)
            {
                element.RemoveHandler(StartRecordDragEvent, handler);
            }
        }

        public static void SetAllowDragDrop(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(AllowDragDropProperty, value);
        }

        public static void SetDragDropHintTemplate(DependencyObject element, DataTemplate value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(DragDropHintTemplateProperty, value);
        }

        public static void SetDropMarkerTemplate(DependencyObject element, DataTemplate value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(DropMarkerTemplateProperty, value);
        }

        public static void SetShowDragDropHint(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ShowDragDropHintProperty, value);
        }

        public static void SetShowTargetInfoInDragDropHint(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ShowTargetInfoInDragDropHintProperty, value);
        }

        private ListBoxEdit ListBox =>
            base.AssociatedObject;

        private bool IsDesignTime =>
            DesignerProperties.GetIsInDesignMode(this);

        internal NativeDragManager DragDropManager { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxDragDropBehavior.<>c <>9 = new ListBoxDragDropBehavior.<>c();
            public static Func<Behavior, bool> <>9__12_0;
            public static Func<Behavior, bool> <>9__13_0;
            public static Action<ListBoxDragDropBehavior> <>9__13_1;
            public static Action<NativeDragManager> <>9__45_0;

            internal bool <OnAllowDragDropChanged>b__12_0(Behavior x) => 
                x is ListBoxDragDropBehavior;

            internal bool <OnDragDropBehaviorChanged>b__13_0(Behavior x) => 
                x is ListBoxDragDropBehavior;

            internal void <OnDragDropBehaviorChanged>b__13_1(ListBoxDragDropBehavior x)
            {
                x.RebuildDragManager();
            }

            internal void <RebuildDragManager>b__45_0(NativeDragManager x)
            {
                x.IsActive = false;
            }
        }
    }
}

