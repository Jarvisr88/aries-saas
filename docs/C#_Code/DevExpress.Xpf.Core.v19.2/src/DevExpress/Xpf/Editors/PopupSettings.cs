namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class PopupSettings
    {
        private EditorPopupBase popup;
        private PopupResizingStrategyBase popupResizingStrategy;
        private IInputElement capturedSource;
        private Size oldSize;

        public PopupSettings(PopupBaseEdit editor)
        {
            this.<OwnerEdit>k__BackingField = editor;
        }

        public virtual void AcceptPopupValue()
        {
            if (this.IsCustomPopup)
            {
                ((IPopupSource) this.PopupValue.Source).AcceptEditableValue(this.PopupValue);
            }
        }

        private void AssignPopupSizeFromSettings()
        {
            if ((this.OwnerEdit.EditMode != EditMode.Standalone) && this.Settings.IsSharedPopupSize)
            {
                this.OwnerEdit.PopupMinHeight = this.Settings.PopupMinHeight;
                this.OwnerEdit.PopupHeight = this.Settings.PopupHeight;
                this.OwnerEdit.PopupMaxHeight = this.Settings.PopupMaxHeight;
                this.OwnerEdit.PopupMinWidth = this.Settings.PopupMinWidth;
                this.OwnerEdit.PopupWidth = this.Settings.PopupWidth;
                this.OwnerEdit.PopupMaxWidth = this.Settings.PopupMaxWidth;
            }
        }

        private void CapturePopup()
        {
            if (this.ShouldCapturePopup())
            {
                Mouse.Capture(this.Popup.Child, CaptureMode.SubTree);
            }
        }

        public void ClosePopup()
        {
            if (this.Popup != null)
            {
                this.Popup.IsOpen = false;
            }
            this.Popup = null;
        }

        protected virtual EditorPopupBase CreatePopup() => 
            new EditorPopupBase();

        protected virtual PopupResizingStrategyBase CreatePopupResizingStrategy() => 
            new PopupResizingStrategyBase(this.OwnerEdit);

        protected virtual void DestroyPopupContent(EditorPopupBase popup)
        {
            ContentControl child = popup.Child as ContentControl;
            if (child != null)
            {
                Predicate<FrameworkElement> predicate = <>c.<>9__96_0;
                if (<>c.<>9__96_0 == null)
                {
                    Predicate<FrameworkElement> local1 = <>c.<>9__96_0;
                    predicate = <>c.<>9__96_0 = element => element is PopupContentContainer;
                }
                PopupContentContainer container = LayoutHelper.FindElement(child, predicate) as PopupContentContainer;
                if (container != null)
                {
                    NonLogicalDecorator content = container.Content as NonLogicalDecorator;
                    if (content != null)
                    {
                        content.Child = null;
                    }
                    container.Content = null;
                }
                child.Content = null;
                popup.PopupContent = null;
            }
        }

        protected void EnsurePopup()
        {
            if (this.Popup == null)
            {
                this.Popup = this.CreatePopup();
                this.SetupPopup(this.Popup);
            }
        }

        private IPopupSource GetBehavior(DependencyObject element)
        {
            Behavior local2;
            BehaviorCollection source = (BehaviorCollection) element.GetValue(Interaction.BehaviorsProperty);
            if (source == null)
            {
                local2 = null;
            }
            else
            {
                Func<Behavior, bool> predicate = <>c.<>9__104_0;
                if (<>c.<>9__104_0 == null)
                {
                    Func<Behavior, bool> local1 = <>c.<>9__104_0;
                    predicate = <>c.<>9__104_0 = x => x is IPopupSource;
                }
                local2 = source.FirstOrDefault<Behavior>(predicate);
            }
            return (local2 as IPopupSource);
        }

        public DevExpress.Xpf.Editors.PopupCloseMode GetClosePopupOnClickMode() => 
            this.IsCustomPopup ? DevExpress.Xpf.Editors.PopupCloseMode.Normal : DevExpress.Xpf.Editors.PopupCloseMode.Cancel;

        private bool GetIsParentPopupClosed()
        {
            System.Windows.Controls.Primitives.Popup popup = LayoutHelper.FindLayoutOrVisualParentObject<System.Windows.Controls.Primitives.Popup>(this.OwnerEdit, true, null);
            return ((popup != null) && !popup.IsOpen);
        }

        protected virtual FrameworkElement GetPopupContent()
        {
            NonLogicalDecorator decorator = new NonLogicalDecorator();
            PopupContentControl element = null;
            if (!this.OwnerEdit.AllowRecreatePopupContent)
            {
                element = (PopupContentControl) this.OwnerEdit.PopupContentOwner.Child;
            }
            if (element == null)
            {
                element = new PopupContentControl();
                FocusHelper.SetFocusable(element, false);
            }
            element.Editor = this.OwnerEdit;
            decorator.Child = element;
            this.OwnerEdit.PopupContentOwner.Child = element;
            BaseEdit.SetOwnerEdit(element, this.OwnerEdit);
            element.Tag = this.OwnerEdit;
            element.Template = this.OwnerEdit.PopupContentTemplate;
            IPopupSource source = this.SelectPopupSource();
            if (source == null)
            {
                this.IsCustomPopup = false;
                this.PopupValue = null;
            }
            else
            {
                this.IsCustomPopup = true;
                this.PopupValue = source.GetEditableValue(this.OwnerEdit, this.OwnerEdit.EditValue);
                element.ContentTemplate = source.ContentTemplate;
                element.ContentTemplateSelector = source.ContentTemplateSelector;
                element.Content = this.PopupValue;
            }
            return decorator;
        }

        private double GetPopupHeight(double offset) => 
            this.PopupResizingStrategy.GetPopupHeight(offset);

        [IteratorStateMachine(typeof(<GetPopupSourceSource>d__103))]
        private IEnumerable<DependencyObject> GetPopupSourceSource()
        {
            yield return this.OwnerEdit;
            yield return this.OwnerEdit.Settings;
            Func<ButtonInfoBase, bool> predicate = <>c.<>9__103_0;
            if (<>c.<>9__103_0 == null)
            {
                Func<ButtonInfoBase, bool> local1 = <>c.<>9__103_0;
                predicate = <>c.<>9__103_0 = x => x.IsDefaultButton;
            }
            IEnumerator<ButtonInfoBase> enumerator = this.OwnerEdit.ActualButtons.Where<ButtonInfoBase>(predicate).GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                ButtonInfoBase current = enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                Func<ButtonInfoBase, bool> predicate = <>c.<>9__103_1;
                if (<>c.<>9__103_1 == null)
                {
                    Func<ButtonInfoBase, bool> local2 = <>c.<>9__103_1;
                    predicate = <>c.<>9__103_1 = x => x.IsDefaultButton;
                }
                enumerator = this.OwnerEdit.Settings.Buttons.Where<ButtonInfoBase>(predicate).GetEnumerator();
            }
            if (!enumerator.MoveNext())
            {
                enumerator = null;
                yield break;
            }
            else
            {
                ButtonInfoBase current = enumerator.Current;
                yield return current;
                yield break;
            }
        }

        private double GetPopupWidth(double offset) => 
            this.PopupResizingStrategy.GetPopupWidth(offset);

        private bool IsMouseCaptureWithinPopup()
        {
            FrameworkElement child = this.Popup.Child as FrameworkElement;
            if (child == null)
            {
                return false;
            }
            if (ReferenceEquals(Mouse.Captured, child))
            {
                return true;
            }
            Predicate<FrameworkElement> predicate = <>c.<>9__58_0;
            if (<>c.<>9__58_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__58_0;
                predicate = <>c.<>9__58_0 = x => ReferenceEquals(x, Mouse.Captured);
            }
            return (LayoutHelper.FindElement(child, predicate) != null);
        }

        private bool IsPopupInVisualTree() => 
            (this.Popup != null) && ((this.Popup.PopupBorderControl != null) && this.Popup.PopupBorderControl.IsInVisualTree());

        public virtual void LostMouseCapture()
        {
            if (this.OwnerEdit.IsPopupOpen && ((this.Popup != null) && this.ShouldCapturePopup()))
            {
                this.CapturedSource = Mouse.Captured;
            }
        }

        private void OnSourceLostMouseCapture(object sender, MouseEventArgs e)
        {
            this.CapturedSource = null;
            if ((this.OwnerEdit.IsPopupOpen && ((this.Popup != null) && this.ShouldCapturePopup())) && (!ReferenceEquals(this.Popup.Child, Mouse.Captured) && !this.GetIsParentPopupClosed()))
            {
                if (Mouse.Captured != null)
                {
                    this.CapturedSource = Mouse.Captured;
                }
                else
                {
                    Mouse.Capture(this.Popup.Child, CaptureMode.SubTree);
                    e.Handled = true;
                }
            }
        }

        public void OpenPopup()
        {
            this.PopupCloseMode = DevExpress.Xpf.Editors.PopupCloseMode.Normal;
            this.EnsurePopup();
            this.OwnerEdit.BeforePopupOpened();
            this.AssignPopupSizeFromSettings();
            this.SetSizeToPopup();
            this.OpenPopupCore();
            this.CapturePopup();
            this.UpdatePopupProperties();
        }

        private void OpenPopupCore()
        {
            this.Popup.FlowDirection = this.OwnerEdit.FlowDirection;
            this.Popup.IsOpen = true;
        }

        private void PopupBorderControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!this.IsResizing && (this.IsPopupInVisualTree() && (this.OwnerEdit.IsInVisualTree() && (e.NewSize != this.oldSize))))
            {
                this.oldSize = e.PreviousSize;
                this.UpdatePopupProperties();
            }
        }

        private void PopupKeyDown(object sender, KeyEventArgs e)
        {
            if (this.OwnerEdit.ProcessVisualClientKeyDown(e))
            {
                this.OwnerEdit.ProcessPopupKeyDown(e);
            }
        }

        private void PopupPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (this.OwnerEdit.ProcessVisualClientPreviewKeyDown(e))
            {
                this.OwnerEdit.ProcessPopupPreviewKeyDown(e);
            }
        }

        protected virtual IPopupSource SelectPopupSource()
        {
            if (this.PopupSource != null)
            {
                return this.PopupSource;
            }
            Func<IPopupSource, bool> predicate = <>c.<>9__102_0;
            if (<>c.<>9__102_0 == null)
            {
                Func<IPopupSource, bool> local1 = <>c.<>9__102_0;
                predicate = <>c.<>9__102_0 = x => x != null;
            }
            return this.GetPopupSourceSource().Select<DependencyObject, IPopupSource>(new Func<DependencyObject, IPopupSource>(this.GetBehavior)).FirstOrDefault<IPopupSource>(predicate);
        }

        public void SetActualHeightToPopup()
        {
            if (this.Popup != null)
            {
                this.Popup.PopupBorderControl.ContentHeight = this.OwnerEdit.ActualPopupHeight;
            }
        }

        private void SetActualSizeToPopup()
        {
            this.SetActualWidthToPopup();
            this.SetActualHeightToPopup();
        }

        public void SetActualWidthToPopup()
        {
            if (this.Popup != null)
            {
                this.Popup.PopupBorderControl.ContentWidth = this.OwnerEdit.ActualPopupWidth;
            }
        }

        protected internal void SetHorizontalPopupSizeChange(double change)
        {
            if (this.PropertyProvider.IgnorePopupSizeConstraints)
            {
                this.OwnerEdit.PopupMaxWidth = double.PositiveInfinity;
            }
            this.OwnerEdit.PopupWidth = this.GetPopupWidth(change);
        }

        public void SetMaxHeightToPopup()
        {
            if (this.Popup != null)
            {
                this.Popup.PopupBorderControl.ContentMaxHeight = this.OwnerEdit.PopupMaxHeight;
            }
        }

        public void SetMaxWidthToPopup()
        {
            if (this.Popup != null)
            {
                this.Popup.PopupBorderControl.ContentMaxWidth = this.OwnerEdit.PopupMaxWidth;
            }
        }

        public void SetMinHeightToPopup()
        {
            if ((this.Popup != null) && this.OwnerEdit.ShouldApplyPopupSize)
            {
                this.Popup.PopupBorderControl.ContentMinHeight = this.OwnerEdit.PopupMinHeight;
            }
        }

        public void SetMinWidthToPopup()
        {
            if (this.Popup != null)
            {
                this.Popup.PopupBorderControl.ContentMinWidth = this.OwnerEdit.ActualPopupMinWidth;
            }
        }

        private void SetPopupBindings(EditorPopupBase popup)
        {
            FrameworkElement visual = LayoutHelper.FindRoot(this.OwnerEdit, false) as FrameworkElement;
            TransformGroup group = new TransformGroup();
            GeneralTransform transform = this.OwnerEdit.TransformToVisual(visual);
            group.Children.Add(transform as Transform);
            if ((this.OwnerEdit.FlowDirection == FlowDirection.RightToLeft) && (LayoutHelper.GetTopLevelVisual(this.OwnerEdit).GetType().FullName != "System.Windows.Controls.Primitives.PopupRoot"))
            {
                group.Children.Add(new ScaleTransform(-1.0, 1.0));
            }
            this.Popup.RenderTransform = group;
        }

        public void SetPopupCloseMode(DevExpress.Xpf.Editors.PopupCloseMode closeMode)
        {
            this.PopupCloseMode = closeMode;
        }

        private void SetPopupContent(EditorPopupBase popup)
        {
            PopupContentContainer container1 = new PopupContentContainer(popup);
            container1.Focusable = false;
            container1.Template = this.OwnerEdit.PopupContentContainerTemplate;
            container1.Content = this.GetPopupContent();
            PopupContentContainer container = container1;
            popup.PopupContent = container;
        }

        private void SetPopupPlacementTarget(EditorPopupBase popup)
        {
            popup.PlacementTarget = this.OwnerEdit;
        }

        public void SetPopupSource(IPopupSource popupSource)
        {
            this.PopupSource = popupSource;
        }

        private void SetSizeRestrictionsToPopup()
        {
            if (this.Popup != null)
            {
                this.Popup.PopupBorderControl.RestrictHeight = this.Settings.RestrictPopupHeight;
                this.Popup.PopupBorderControl.ScreenRestrictions = this.PopupResizingStrategy.Restrictions;
            }
            this.SetMinWidthToPopup();
            this.SetMaxWidthToPopup();
            this.SetMinHeightToPopup();
            this.SetMaxHeightToPopup();
        }

        private void SetSizeToPopup()
        {
            this.SetSizeRestrictionsToPopup();
            this.SetActualSizeToPopup();
        }

        protected virtual void SetupPopup(EditorPopupBase popup)
        {
            BaseEdit.SetOwnerEdit(popup, this.OwnerEdit);
            this.SetPopupContent(popup);
            this.SetPopupPlacementTarget(popup);
            this.SetPopupBindings(popup);
            this.SubscribePopup(popup);
            this.SetupPopupPlacement(popup);
        }

        private void SetupPopupPlacement(EditorPopupBase popup)
        {
            popup.CustomPopupPlacementCallback = delegate (Size popupSize, Size targetSize, Point offset) {
                CustomPopupPlacement placement;
                bool flag = this.OwnerEdit.FlowDirection == FlowDirection.RightToLeft;
                Point point = this.PopupResizingStrategy.IsLeft ? new Point(((offset.X + targetSize.Width) - popupSize.Width) + (!flag ? 0.0 : (popupSize.Width - (2.0 * targetSize.Width))), offset.Y) : new Point(offset.X + (!flag ? 0.0 : -popupSize.Width), offset.Y);
                CustomPopupPlacement[] placementArray = new CustomPopupPlacement[1];
                if (this.PopupResizingStrategy.GetPlacement() == PlacementMode.Top)
                {
                    placement = new CustomPopupPlacement {
                        Point = new Point(point.X, point.Y - popupSize.Height),
                        PrimaryAxis = PopupPrimaryAxis.Horizontal
                    };
                    placementArray[0] = placement;
                }
                else
                {
                    placement = new CustomPopupPlacement {
                        Point = new Point(point.X, point.Y + targetSize.Height),
                        PrimaryAxis = PopupPrimaryAxis.Horizontal
                    };
                    placementArray[0] = placement;
                }
                return placementArray;
            };
            popup.Placement = PlacementMode.Custom;
        }

        protected internal void SetVerticalPopupSizeChange(double change)
        {
            if (this.PropertyProvider.IgnorePopupSizeConstraints)
            {
                this.OwnerEdit.PopupMaxHeight = double.PositiveInfinity;
            }
            this.OwnerEdit.PopupHeight = this.GetPopupHeight(change);
        }

        private bool ShouldCapturePopup()
        {
            UIElement child;
            EditorPopupBase popup = this.Popup;
            if (popup != null)
            {
                child = popup.Child;
            }
            else
            {
                EditorPopupBase local1 = popup;
                child = null;
            }
            return ((child != null) ? ((PopupBaseEditStyleSettings) this.PropertyProvider.StyleSettings).ShouldCaptureMouseOnPopup : false);
        }

        private void SubscribePopup(EditorPopupBase popup)
        {
            this.SubscribePopupBorderControlSizeChanged();
            popup.PreviewKeyDown += new KeyEventHandler(this.PopupPreviewKeyDown);
            popup.KeyDown += new KeyEventHandler(this.PopupKeyDown);
            Window topLevelVisual = LayoutHelper.GetTopLevelVisual(this.OwnerEdit) as Window;
            if (topLevelVisual != null)
            {
                Action<PopupSettings, object, CancelEventArgs> onEventAction = <>c.<>9__59_0;
                if (<>c.<>9__59_0 == null)
                {
                    Action<PopupSettings, object, CancelEventArgs> local1 = <>c.<>9__59_0;
                    onEventAction = <>c.<>9__59_0 = (x, sender, args) => x.WindowClosing(sender, args);
                }
                this.WindowClosingHandler = new WeakEventHandler<PopupSettings, CancelEventArgs, CancelEventHandler>(this, onEventAction, <>c.<>9__59_1 ??= delegate (WeakEventHandler<PopupSettings, CancelEventArgs, CancelEventHandler> h, object sender) {
                    ((Window) sender).Closing -= h.Handler;
                }, <>c.<>9__59_2 ??= h => new CancelEventHandler(h.OnEvent));
                topLevelVisual.Closing += this.WindowClosingHandler.Handler;
                Action<PopupSettings, object, EventArgs> action2 = <>c.<>9__59_3;
                if (<>c.<>9__59_3 == null)
                {
                    Action<PopupSettings, object, EventArgs> local4 = <>c.<>9__59_3;
                    action2 = <>c.<>9__59_3 = (x, sender, args) => x.WindowDeactivated(sender, args);
                }
                <>c.<>9__59_3.DeactivateWindowEventHandler = new WeakEventHandler<PopupSettings, EventArgs, EventHandler>((PopupSettings) <>c.<>9__59_3, action2, <>c.<>9__59_4 ??= delegate (WeakEventHandler<PopupSettings, EventArgs, EventHandler> h, object sender) {
                    ((Window) sender).Deactivated -= h.Handler;
                }, <>c.<>9__59_5 ??= h => new EventHandler(h.OnEvent));
                topLevelVisual.Deactivated += this.DeactivateWindowEventHandler.Handler;
            }
        }

        private void SubscribePopupBorderControlSizeChanged()
        {
            if ((this.Popup != null) && (this.Popup.PopupBorderControl != null))
            {
                this.Popup.PopupBorderControl.SizeChanged += new SizeChangedEventHandler(this.PopupBorderControlSizeChanged);
            }
        }

        protected virtual void UnCaptureChild()
        {
            this.CapturedSource = null;
            if ((this.Popup != null) && this.IsMouseCaptureWithinPopup())
            {
                Mouse.Capture(null);
            }
        }

        private void UncapturePopup(EditorPopupBase editorPopupBase)
        {
            this.CapturedSource = null;
            if ((this.Popup != null) && this.IsMouseCaptureWithinPopup())
            {
                Mouse.Capture(null);
            }
        }

        protected virtual void UnsetupPopup(EditorPopupBase popup)
        {
            this.UnsubscribePopupBorderControlSizeChanged();
            this.UncapturePopup(popup);
            this.DestroyPopupContent(popup);
            if (this.OwnerEdit.AllowRecreatePopupContent)
            {
                this.OwnerEdit.PopupContentOwner.Child = null;
            }
            this.UnsubcribePopup(popup);
            this.OwnerEdit.DestroyPopupContent(popup);
        }

        private void UnsubcribePopup(EditorPopupBase popup)
        {
            popup.PreviewKeyDown -= new KeyEventHandler(this.PopupPreviewKeyDown);
            popup.KeyDown -= new KeyEventHandler(this.PopupKeyDown);
            this.WindowClosingHandler = null;
            this.DeactivateWindowEventHandler = null;
        }

        private void UnsubscribePopupBorderControlSizeChanged()
        {
            if ((this.Popup != null) && (this.Popup.PopupBorderControl != null))
            {
                this.Popup.PopupBorderControl.SizeChanged -= new SizeChangedEventHandler(this.PopupBorderControlSizeChanged);
            }
        }

        public void UpdateActualPopupHeight(double height)
        {
            if (this.OwnerEdit.IsLoaded && this.OwnerEdit.IsInVisualTree())
            {
                this.OwnerEdit.ActualPopupHeight = Math.Min(height, this.PopupResizingStrategy.ActualAvailableHeight);
            }
            else
            {
                this.OwnerEdit.ActualPopupHeight = height;
            }
        }

        public void UpdateActualPopupMinWidth()
        {
            if (!this.OwnerEdit.ShouldApplyPopupSize)
            {
                this.OwnerEdit.ActualPopupMinWidth = this.OwnerEdit.PopupMinWidth;
            }
            else if (this.OwnerEdit.PopupMinWidth > this.OwnerEdit.ActualWidth)
            {
                this.OwnerEdit.ActualPopupMinWidth = this.OwnerEdit.PopupMinWidth;
            }
            else
            {
                this.OwnerEdit.ActualPopupMinWidth = this.OwnerEdit.ActualWidth;
            }
        }

        internal void UpdateActualPopupSize()
        {
            this.UpdateActualPopupWidth(this.OwnerEdit.PopupWidth);
            this.UpdateActualPopupHeight(this.OwnerEdit.PopupHeight);
        }

        public void UpdateActualPopupWidth(double width)
        {
            if (this.OwnerEdit.IsLoaded && this.OwnerEdit.IsInVisualTree())
            {
                this.OwnerEdit.ActualPopupWidth = Math.Min(width, this.PopupResizingStrategy.ActualAvailableWidth);
            }
            else
            {
                this.OwnerEdit.ActualPopupWidth = width;
            }
        }

        private void UpdateDropOpposite()
        {
            this.PopupResizingStrategy.UpdateDropOpposite();
            this.PropertyProvider.ResizeGripViewModel.Update();
            this.PropertyProvider.PopupViewModel.UpdateDropOpposite();
        }

        public void UpdatePopupProperties()
        {
            if (this.OwnerEdit.IsLoaded)
            {
                this.UpdateDropOpposite();
                this.UpdateActualPopupSize();
                EditorPopupBase popup = this.Popup;
                if (popup == null)
                {
                    EditorPopupBase local1 = popup;
                }
                else
                {
                    popup.RepositionInternal();
                }
            }
        }

        private void WindowClosing(object sender, EventArgs args)
        {
            this.OwnerEdit.CancelPopup();
        }

        private void WindowDeactivated(object sender, EventArgs args)
        {
            this.OwnerEdit.CancelPopup();
        }

        private WeakEventHandler<PopupSettings, EventArgs, EventHandler> DeactivateWindowEventHandler { get; set; }

        private WeakEventHandler<PopupSettings, CancelEventArgs, CancelEventHandler> WindowClosingHandler { get; set; }

        protected internal UITypeEditorValue PopupValue { get; private set; }

        protected bool IsCustomPopup { get; private set; }

        public DevExpress.Xpf.Editors.PopupCloseMode PopupCloseMode { get; private set; }

        protected PopupBaseEdit OwnerEdit { get; }

        protected PopupBaseEditPropertyProvider PropertyProvider =>
            (PopupBaseEditPropertyProvider) this.OwnerEdit.PropertyProvider;

        protected PopupBaseEditSettings Settings =>
            this.OwnerEdit.Settings;

        protected IPopupSource PopupSource { get; private set; }

        protected internal EditorPopupBase Popup
        {
            get => 
                this.popup;
            private set
            {
                if (this.Popup != null)
                {
                    this.UnsetupPopup(this.Popup);
                }
                this.popup = value;
            }
        }

        public bool IsResizing { get; set; }

        protected internal PopupResizingStrategyBase PopupResizingStrategy
        {
            get
            {
                PopupResizingStrategyBase popupResizingStrategy = this.popupResizingStrategy;
                if (this.popupResizingStrategy == null)
                {
                    PopupResizingStrategyBase local1 = this.popupResizingStrategy;
                    popupResizingStrategy = this.popupResizingStrategy = this.CreatePopupResizingStrategy();
                }
                return popupResizingStrategy;
            }
        }

        protected IInputElement CapturedSource
        {
            get => 
                this.capturedSource;
            set
            {
                if (!ReferenceEquals(this.CapturedSource, value))
                {
                    if (this.CapturedSource != null)
                    {
                        this.CapturedSource.LostMouseCapture -= new MouseEventHandler(this.OnSourceLostMouseCapture);
                    }
                    this.capturedSource = value;
                    if (this.CapturedSource != null)
                    {
                        this.CapturedSource.LostMouseCapture += new MouseEventHandler(this.OnSourceLostMouseCapture);
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupSettings.<>c <>9 = new PopupSettings.<>c();
            public static Predicate<FrameworkElement> <>9__58_0;
            public static Action<PopupSettings, object, CancelEventArgs> <>9__59_0;
            public static Action<WeakEventHandler<PopupSettings, CancelEventArgs, CancelEventHandler>, object> <>9__59_1;
            public static Func<WeakEventHandler<PopupSettings, CancelEventArgs, CancelEventHandler>, CancelEventHandler> <>9__59_2;
            public static Action<PopupSettings, object, EventArgs> <>9__59_3;
            public static Action<WeakEventHandler<PopupSettings, EventArgs, EventHandler>, object> <>9__59_4;
            public static Func<WeakEventHandler<PopupSettings, EventArgs, EventHandler>, EventHandler> <>9__59_5;
            public static Predicate<FrameworkElement> <>9__96_0;
            public static Func<IPopupSource, bool> <>9__102_0;
            public static Func<ButtonInfoBase, bool> <>9__103_0;
            public static Func<ButtonInfoBase, bool> <>9__103_1;
            public static Func<Behavior, bool> <>9__104_0;

            internal bool <DestroyPopupContent>b__96_0(FrameworkElement element) => 
                element is PopupContentContainer;

            internal bool <GetBehavior>b__104_0(Behavior x) => 
                x is IPopupSource;

            internal bool <GetPopupSourceSource>b__103_0(ButtonInfoBase x) => 
                x.IsDefaultButton;

            internal bool <GetPopupSourceSource>b__103_1(ButtonInfoBase x) => 
                x.IsDefaultButton;

            internal bool <IsMouseCaptureWithinPopup>b__58_0(FrameworkElement x) => 
                ReferenceEquals(x, Mouse.Captured);

            internal bool <SelectPopupSource>b__102_0(IPopupSource x) => 
                x != null;

            internal void <SubscribePopup>b__59_0(PopupSettings x, object sender, CancelEventArgs args)
            {
                x.WindowClosing(sender, args);
            }

            internal void <SubscribePopup>b__59_1(WeakEventHandler<PopupSettings, CancelEventArgs, CancelEventHandler> h, object sender)
            {
                ((Window) sender).Closing -= h.Handler;
            }

            internal CancelEventHandler <SubscribePopup>b__59_2(WeakEventHandler<PopupSettings, CancelEventArgs, CancelEventHandler> h) => 
                new CancelEventHandler(h.OnEvent);

            internal void <SubscribePopup>b__59_3(PopupSettings x, object sender, EventArgs args)
            {
                x.WindowDeactivated(sender, args);
            }

            internal void <SubscribePopup>b__59_4(WeakEventHandler<PopupSettings, EventArgs, EventHandler> h, object sender)
            {
                ((Window) sender).Deactivated -= h.Handler;
            }

            internal EventHandler <SubscribePopup>b__59_5(WeakEventHandler<PopupSettings, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);
        }

    }
}

