namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_Text", Type=typeof(TextBox))]
    public class RenameHint : DockHintElement
    {
        private IDockLayoutElement LayoutElement;
        private BaseLayoutItem itemCore;
        private string OldCaption;
        private readonly Locker renamingLocker;

        static RenameHint()
        {
            new DependencyPropertyRegistrator<RenameHint>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public RenameHint() : base(DockVisualizerElement.RenameHint)
        {
            this.renamingLocker = new Locker();
        }

        protected override Rect CalcBounds(DockingHintAdornerBase adorner) => 
            this.GetItemBounds();

        protected override bool CalcVisibleState(DockingHintAdornerBase adorner) => 
            (this.Item != null) && this.Item.AllowRename;

        internal void CancelRenaming()
        {
            if (this.renamingLocker)
            {
                this.renamingLocker.Unlock();
                if (this.Manager != null)
                {
                    this.Manager.RaiseEvent(new LayoutItemEndRenamingEventArgs(this.Item, this.OldCaption, true));
                    this.Manager.CoerceValue(DockLayoutManager.IsRenamingProperty);
                }
                this.Item = null;
            }
        }

        internal void EndRenaming()
        {
            if (this.renamingLocker)
            {
                this.renamingLocker.Unlock();
                this.Item.Caption = this.PartText.Text;
                if (this.Manager != null)
                {
                    this.Manager.RaiseEvent(new LayoutItemEndRenamingEventArgs(this.Item, this.OldCaption));
                    this.Manager.CoerceValue(DockLayoutManager.IsRenamingProperty);
                }
                this.Item = null;
            }
        }

        protected internal Rect GetItemBounds()
        {
            UIElement element = (this.LayoutElement != null) ? this.LayoutElement.Element : this.Item;
            if ((this.Item != null) && ((this.Item.Manager != null) && (element != null)))
            {
                IDockLayoutElement layoutElement = this.LayoutElement;
                Predicate<FrameworkElement> predicate = <>c.<>9__31_0;
                if (<>c.<>9__31_0 == null)
                {
                    Predicate<FrameworkElement> local1 = <>c.<>9__31_0;
                    predicate = <>c.<>9__31_0 = e => e is CaptionControl;
                }
                CaptionControl control = LayoutHelper.FindElement((FrameworkElement) element, predicate) as CaptionControl;
                if (layoutElement != null)
                {
                    UIElement element3 = (control == null) ? element : (control.PartText ?? control);
                    Size renderSize = element3.RenderSize;
                    if ((renderSize.Width <= 0.0) || ((renderSize.Height <= 0.0) || ((control == null) || string.IsNullOrEmpty(this.Item.Caption as string))))
                    {
                        renderSize = this.TextMinSize;
                    }
                    return new Rect(element3.TranslatePoint(CoordinateHelper.ZeroPoint, layoutElement.View), renderSize);
                }
            }
            return Rect.Empty;
        }

        private void MeasureText()
        {
            string str = (this.Item != null) ? (this.Item.Caption as string) : null;
            this.PartText.Text = !string.IsNullOrEmpty(str) ? str : "TestText";
            this.PartText.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this.TextMinSize = this.PartText.DesiredSize;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UnsubscribeEvents();
            this.PartText = base.GetTemplateChild("PART_Text") as TextBox;
            this.SubscribeEvents();
        }

        protected virtual void OnItemChanged()
        {
            if ((this.Item != null) && ((this.Item.Manager != null) && (this.PartText != null)))
            {
                this.MeasureText();
                this.PartText.Text = this.Item.Caption as string;
            }
        }

        private void PartText_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                this.PartText.Focus();
                this.PartText.SelectAll();
            }
        }

        private void PartText_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Item != null)
            {
                this.PartText.Text = this.Item.Caption as string;
            }
            this.PartText.Focus();
            this.PartText.SelectAll();
        }

        internal void StartRenaming(IDockLayoutElement element)
        {
            if (this.renamingLocker)
            {
                this.CancelRenaming();
            }
            this.renamingLocker.Lock();
            this.LayoutElement = element;
            this.Item = element.Item;
            if (this.Manager != null)
            {
                this.Manager.CoerceValue(DockLayoutManager.IsRenamingProperty);
            }
        }

        private void SubscribeEvents()
        {
            if (this.PartText != null)
            {
                this.PartText.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.PartText_IsVisibleChanged);
                this.PartText.Loaded += new RoutedEventHandler(this.PartText_Loaded);
            }
        }

        private void UnsubscribeEvents()
        {
            if (this.PartText != null)
            {
                this.PartText.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(this.PartText_IsVisibleChanged);
                this.PartText.Loaded -= new RoutedEventHandler(this.PartText_Loaded);
            }
        }

        protected TextBox PartText { get; private set; }

        private DockLayoutManager Manager =>
            this.Item?.Manager;

        public bool IsRenamingStarted =>
            (bool) this.renamingLocker;

        internal BaseLayoutItem Item
        {
            get => 
                this.itemCore;
            set
            {
                if (!ReferenceEquals(this.Item, value))
                {
                    this.itemCore = value;
                    this.OnItemChanged();
                    this.OldCaption = (this.itemCore != null) ? (this.itemCore.Caption as string) : null;
                }
            }
        }

        internal Size TextMinSize { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenameHint.<>c <>9 = new RenameHint.<>c();
            public static Predicate<FrameworkElement> <>9__31_0;

            internal bool <GetItemBounds>b__31_0(FrameworkElement e) => 
                e is CaptionControl;
        }
    }
}

