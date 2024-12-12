namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;
    using System.Xml;

    [TemplatePart(Name="ContentChangeStoryboard", Type=typeof(Storyboard)), DXToolboxBrowsable]
    public class Tile : MaximizableHeaderedContentControlBase, ITile, IMaximizableElement, IClickable, IWeakEventListener
    {
        public const int DefaultContentChangeInterval = 5;
        public const double ExtraLargeSize = 310.0;
        public const double ExtraSmallSize = 70.0;
        public const double LargeWidth = 310.0;
        public const double LargeHeight = 150.0;
        public const double SmallWidth = 150.0;
        public const double SmallHeight = 150.0;
        private bool _IsChangingIsMaximized;
        public static readonly DependencyProperty CalculatedBackgroundProperty = DependencyProperty.Register("CalculatedBackground", typeof(Brush), typeof(Tile), null);
        public static readonly DependencyProperty CalculatedHeaderVisibilityProperty = DependencyProperty.Register("CalculatedHeaderVisibility", typeof(Visibility), typeof(Tile), null);
        public static readonly DependencyProperty PreviousContentProperty = DependencyProperty.Register("PreviousContent", typeof(object), typeof(Tile), null);
        public static readonly DependencyProperty AnimateContentChangeProperty = DependencyProperty.Register("AnimateContentChange", typeof(bool), typeof(Tile), new PropertyMetadata(true));
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(Tile), null);
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(Tile), null);
        public static readonly DependencyProperty ContentChangeIntervalProperty;
        public static readonly DependencyProperty ContentSourceProperty;
        public static readonly DependencyProperty HorizontalHeaderAlignmentProperty;
        public static readonly DependencyProperty IsMaximizedProperty;
        public static readonly DependencyProperty SizeProperty;
        public static readonly DependencyProperty VerticalHeaderAlignmentProperty;
        private static readonly DependencyProperty BackgroundListener;
        private readonly List<object> _contentSource = new List<object>();
        private const string ContentChangeStoryboardName = "ContentChangeStoryboard";

        public event EventHandler Click;

        public event EventHandler IsMaximizedChanged;

        static Tile()
        {
            ContentChangeIntervalProperty = DependencyProperty.Register("ContentChangeInterval", typeof(TimeSpan), typeof(Tile), new PropertyMetadata(TimeSpan.FromSeconds(5.0), (o, e) => ((Tile) o).OnContentChangeIntervalChanged()));
            ContentSourceProperty = DependencyProperty.Register("ContentSource", typeof(IEnumerable), typeof(Tile), new PropertyMetadata((o, e) => ((Tile) o).OnContentSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue)));
            HorizontalHeaderAlignmentProperty = DependencyProperty.Register("HorizontalHeaderAlignment", typeof(HorizontalAlignment), typeof(Tile), null);
            IsMaximizedProperty = DependencyProperty.Register("IsMaximized", typeof(bool), typeof(Tile), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                Tile tile = (Tile) o;
                if (!tile._IsChangingIsMaximized)
                {
                    tile._IsChangingIsMaximized = true;
                    if (((bool) e.NewValue) && !(tile.Parent is IMaximizingContainer))
                    {
                        o.SetValue(e.Property, e.OldValue);
                    }
                    else
                    {
                        tile.OnIsMaximizedChanged();
                    }
                    tile._IsChangingIsMaximized = false;
                }
            }));
            SizeProperty = DependencyProperty.Register("Size", typeof(TileSize), typeof(Tile), new PropertyMetadata(TileSize.Large, (o, e) => ((Tile) o).OnSizeChanged()));
            VerticalHeaderAlignmentProperty = DependencyProperty.Register("VerticalHeaderAlignment", typeof(VerticalAlignment), typeof(Tile), null);
            BackgroundListener = RegisterPropertyListener("Background");
        }

        public Tile()
        {
            base.DefaultStyleKey = typeof(Tile);
            this.CalculateHeaderVisibility();
            base.AttachPropertyListener("Background", BackgroundListener, null);
        }

        protected virtual Color CalculateGradientEndColor(Color startColor) => 
            Color.FromArgb(startColor.A, this.CalculateGradientEndColorChannel(startColor.R), this.CalculateGradientEndColorChannel(startColor.G), this.CalculateGradientEndColorChannel(startColor.B));

        protected virtual byte CalculateGradientEndColorChannel(byte startColorChannel)
        {
            if ((startColorChannel < 8) || (startColorChannel > 0xf7))
            {
                return startColorChannel;
            }
            int num = (startColorChannel >= 0x80) ? (0x3f - (startColorChannel / 4)) : ((startColorChannel / 4) - 1);
            return (byte) (startColorChannel + num);
        }

        protected void CalculateHeaderVisibility()
        {
            this.SetValue(CalculatedHeaderVisibilityProperty, this.IsHeaderVisible() ? Visibility.Visible : Visibility.Collapsed);
        }

        protected virtual bool CanAnimateContentChange() => 
            this.AnimateContentChange && (!this.IsMaximized && (!this.IsInDesignTool() || this.IsContentChangeActive));

        private void ChangeContent()
        {
            if (!this.ContentEnumerator.MoveNext())
            {
                this.ContentEnumerator.Reset();
                if (!this.ContentEnumerator.MoveNext())
                {
                    base.Content = null;
                    return;
                }
            }
            base.Content = this.ContentEnumerator.Current;
        }

        private void ClearItemAction(object item)
        {
            base.RemoveLogicalChild(item);
        }

        private object ConvertItemAction(object item)
        {
            base.AddLogicalChild(item);
            return item;
        }

        protected override ControlControllerBase CreateController() => 
            new TileController(this);

        void IMaximizableElement.AfterNormalization()
        {
            this.IsMaximized = false;
        }

        void IMaximizableElement.BeforeMaximization()
        {
            this.IsMaximized = true;
        }

        void ITile.Click()
        {
            this.OnClick();
            if (base.Parent is ITileLayoutControl)
            {
                ((ITileLayoutControl) base.Parent).TileClick(this);
            }
        }

        protected virtual Brush GetCalculatedBackground()
        {
            SolidColorBrush background = base.Background as SolidColorBrush;
            if (background == null)
            {
                return null;
            }
            Color startColor = background.Color;
            Color color2 = this.CalculateGradientEndColor(startColor);
            if (color2 == startColor)
            {
                return null;
            }
            GradientStop stop1 = new GradientStop();
            stop1.Offset = 0.0;
            stop1.Color = startColor;
            GradientStopCollection gradientStopCollection = new GradientStopCollection();
            gradientStopCollection.Add(stop1);
            GradientStop stop2 = new GradientStop();
            stop2.Offset = 1.0;
            stop2.Color = color2;
            gradientStopCollection.Add(stop2);
            return new LinearGradientBrush(gradientStopCollection, 0.0);
        }

        protected virtual bool IsHeaderVisible() => 
            this.Size != TileSize.ExtraSmall;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ContentChangeStoryboard = base.GetTemplateChild("ContentChangeStoryboard") as Storyboard;
        }

        protected virtual void OnBackgroundChanged()
        {
            this.UpdateCalculatedBackground();
        }

        protected virtual void OnClick()
        {
            if (this.Click != null)
            {
                this.Click(this, EventArgs.Empty);
            }
            if ((this.Command != null) && this.Command.CanExecute(this.CommandParameter))
            {
                this.Command.Execute(this.CommandParameter);
            }
        }

        protected override void OnContentChanged(object oldValue, object newValue)
        {
            base.OnContentChanged(oldValue, newValue);
            this.PreviousContent = oldValue;
            if (this.CanAnimateContentChange() && (base.IsLoaded && (this.IsInVisualTree() && (this.ContentChangeStoryboard != null))))
            {
                if (this.IsContentChangeStoryboardActive)
                {
                    this.ContentChangeStoryboard.SkipToFill();
                }
                this.ContentChangeStoryboard.Begin();
                this.IsContentChangeStoryboardActive = true;
            }
            if ((oldValue != null) && !this._contentSource.Contains(oldValue))
            {
                base.RemoveLogicalChild(oldValue);
            }
            if ((newValue != null) && !this._contentSource.Contains(newValue))
            {
                base.AddLogicalChild(newValue);
            }
        }

        protected virtual void OnContentChangeIntervalChanged()
        {
            this.UpdateContentChangeInterval();
        }

        protected virtual void OnContentSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            INotifyCollectionChanged source = oldValue as INotifyCollectionChanged;
            INotifyCollectionChanged changed2 = newValue as INotifyCollectionChanged;
            if (source != null)
            {
                CollectionChangedEventManager.RemoveListener(source, this);
            }
            if (changed2 != null)
            {
                CollectionChangedEventManager.AddListener(changed2, this);
            }
            SyncCollectionHelper.PopulateCore(this._contentSource, newValue as ICollection, new Func<object, object>(this.ConvertItemAction), null, new Action<object>(this.ClearItemAction));
            if (this.ContentSource != null)
            {
                this.StartContentChange();
            }
            else
            {
                this.StopContentChange();
            }
        }

        private void OnContentSourceCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.ContentSource is ICollection)
            {
                SyncCollectionHelper.SyncCollection(e, this._contentSource, this.ContentSource as IList, new Func<object, object>(this.ConvertItemAction), null, null, new Action<object>(this.ClearItemAction));
                if (!this._contentSource.Contains(base.Content))
                {
                    this.StartContentChange();
                }
                else
                {
                    this.ContentEnumerator = this._contentSource.GetEnumerator();
                    do
                    {
                        if (!this.ContentEnumerator.MoveNext())
                        {
                            return;
                        }
                    }
                    while (this.ContentEnumerator.Current != base.Content);
                }
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new TileAutomationPeer(this);

        protected virtual void OnIsMaximizedChanged()
        {
            if (!this.IsMaximized && ((base.Parent is IMaximizingContainer) && ReferenceEquals(((IMaximizingContainer) base.Parent).MaximizedElement, this)))
            {
                ((IMaximizingContainer) base.Parent).MaximizedElement = null;
            }
            base.IsMaximizedCore = this.IsMaximized;
            if (this.IsMaximizedChanged != null)
            {
                this.IsMaximizedChanged(this, EventArgs.Empty);
            }
            if (this.IsMaximized)
            {
                ((IMaximizingContainer) base.Parent).MaximizedElement = this;
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            this.UpdateCalculatedBackground();
            this.UpdateContentChangeState();
        }

        protected override System.Windows.Size OnMeasure(System.Windows.Size availableSize)
        {
            if (this.IsMaximized)
            {
                return base.OnMeasure(availableSize);
            }
            System.Windows.Size size = new System.Windows.Size();
            switch (this.Size)
            {
                case TileSize.ExtraSmall:
                    size.Width = size.Height = 70.0;
                    break;

                case TileSize.Small:
                    size.Width = 150.0;
                    size.Height = 150.0;
                    break;

                case TileSize.Large:
                    size.Width = 310.0;
                    size.Height = 150.0;
                    break;

                case TileSize.ExtraLarge:
                    size.Width = size.Height = 310.0;
                    break;

                default:
                    break;
            }
            if (!double.IsNaN(base.Width))
            {
                size.Width = Math.Max(base.Width, base.MinWidth);
            }
            if (!double.IsNaN(base.Height))
            {
                size.Height = Math.Max(base.Height, base.MinHeight);
            }
            base.OnMeasure(size);
            return size;
        }

        protected override void OnPropertyChanged(DependencyProperty propertyListener, object oldValue, object newValue)
        {
            base.OnPropertyChanged(propertyListener, oldValue, newValue);
            if (ReferenceEquals(propertyListener, BackgroundListener))
            {
                this.OnBackgroundChanged();
            }
        }

        protected virtual void OnSizeChanged()
        {
            this.CalculateHeaderVisibility();
            base.InvalidateMeasure();
            if (base.Parent is ITileLayoutControl)
            {
                ((ITileLayoutControl) base.Parent).OnTileSizeChanged(this);
            }
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();
            this.UpdateContentChangeState();
        }

        protected internal virtual void ReadCustomizablePropertiesFromXML(XmlReader xml)
        {
            this.ReadPropertyFromXML(xml, SizeProperty, "Size", typeof(TileSize));
        }

        private void StartContentChange()
        {
            if (this.IsContentChangeActive)
            {
                this.StopContentChange();
            }
            this.ContentEnumerator = this.ContentSource.GetEnumerator();
            this.ChangeContent();
            this.ContentChangeTimer = new DispatcherTimer();
            this.ContentChangeTimer.Interval = this.ContentChangeInterval;
            this.ContentChangeTimer.Tick += (o, e) => this.ChangeContent();
            this.UpdateContentChangeState();
        }

        private void StopContentChange()
        {
            if (this.IsContentChangeActive)
            {
                this.ContentChangeTimer.Stop();
                this.ContentChangeTimer = null;
                this.ContentEnumerator = null;
                base.ClearValue(ContentControlBase.ContentProperty);
            }
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(CollectionChangedEventManager)))
            {
                return false;
            }
            this.OnContentSourceCollectionChanged((NotifyCollectionChangedEventArgs) e);
            return true;
        }

        protected void UpdateCalculatedBackground()
        {
            this.CalculatedBackground = this.GetCalculatedBackground();
        }

        private void UpdateContentChangeInterval()
        {
            if (this.IsContentChangeActive)
            {
                this.ContentChangeTimer.Interval = this.ContentChangeInterval;
            }
        }

        private void UpdateContentChangeState()
        {
            if (this.IsContentChangeActive)
            {
                if (base.IsLoaded)
                {
                    this.ContentChangeTimer.Start();
                }
                else
                {
                    this.ContentChangeTimer.Stop();
                }
            }
        }

        protected internal virtual void WriteCustomizablePropertiesToXML(XmlWriter xml)
        {
            this.WritePropertyToXML(xml, SizeProperty, "Size");
        }

        protected override bool IsContentInLogicalTree =>
            false;

        [Description("Gets or sets whether  to play the animation when the tile's content changes. This is a dependency property.")]
        public bool AnimateContentChange
        {
            get => 
                (bool) base.GetValue(AnimateContentChangeProperty);
            set => 
                base.SetValue(AnimateContentChangeProperty, value);
        }

        public Brush CalculatedBackground
        {
            get => 
                (Brush) base.GetValue(CalculatedBackgroundProperty);
            private set => 
                base.SetValue(CalculatedBackgroundProperty, value);
        }

        [Description("Gets or sets the command to invoke when the tile is clicked. This is a dependency property.")]
        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        [Description("Gets or sets the parameter to pass to the Tile.Command. This is a dependency property.")]
        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        [Description("Gets or sets the content change interval. This is a dependency property.")]
        public TimeSpan ContentChangeInterval
        {
            get => 
                (TimeSpan) base.GetValue(ContentChangeIntervalProperty);
            set => 
                base.SetValue(ContentChangeIntervalProperty, value);
        }

        [Description("Gets or sets the content source. This is a dependency property.")]
        public IEnumerable ContentSource
        {
            get => 
                (IEnumerable) base.GetValue(ContentSourceProperty);
            set => 
                base.SetValue(ContentSourceProperty, value);
        }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public TileController Controller =>
            (TileController) base.Controller;

        [Description("Gets or sets the horizontal alignment of the tile's header. This is a dependency property.")]
        public HorizontalAlignment HorizontalHeaderAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(HorizontalHeaderAlignmentProperty);
            set => 
                base.SetValue(HorizontalHeaderAlignmentProperty, value);
        }

        [Description("Gets or sets whether the tile is maximized. A maximized tile is automatically resized to fit available space in the TileLayoutControl. This is a dependency property.")]
        public bool IsMaximized
        {
            get => 
                (bool) base.GetValue(IsMaximizedProperty);
            set => 
                base.SetValue(IsMaximizedProperty, value);
        }

        public object PreviousContent
        {
            get => 
                base.GetValue(PreviousContentProperty);
            private set => 
                base.SetValue(PreviousContentProperty, value);
        }

        [Description("Gets or sets the tile's size. This is a dependency property.")]
        public TileSize Size
        {
            get => 
                (TileSize) base.GetValue(SizeProperty);
            set => 
                base.SetValue(SizeProperty, value);
        }

        [Description("Gets or sets the vertical alignment of the tile's header. This is a dependency property.")]
        public VerticalAlignment VerticalHeaderAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(VerticalHeaderAlignmentProperty);
            set => 
                base.SetValue(VerticalHeaderAlignmentProperty, value);
        }

        protected Storyboard ContentChangeStoryboard { get; private set; }

        protected bool IsContentChangeStoryboardActive { get; private set; }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                IEnumerator logicalChildren = base.LogicalChildren;
                if (logicalChildren != null)
                {
                    while (logicalChildren.MoveNext())
                    {
                        list.Add(logicalChildren.Current);
                    }
                }
                if ((base.Content != null) && !list.Contains(base.Content))
                {
                    list.Add(base.Content);
                }
                foreach (object obj2 in this._contentSource)
                {
                    if (!list.Contains(obj2))
                    {
                        list.Add(obj2);
                    }
                }
                return list.GetEnumerator();
            }
        }

        private DispatcherTimer ContentChangeTimer { get; set; }

        private IEnumerator ContentEnumerator { get; set; }

        private bool IsContentChangeActive =>
            this.ContentEnumerator != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Tile.<>c <>9 = new Tile.<>c();

            internal void <.cctor>b__122_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((Tile) o).OnContentChangeIntervalChanged();
            }

            internal void <.cctor>b__122_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((Tile) o).OnContentSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue);
            }

            internal void <.cctor>b__122_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                Tile tile = (Tile) o;
                if (!tile._IsChangingIsMaximized)
                {
                    tile._IsChangingIsMaximized = true;
                    if (((bool) e.NewValue) && !(tile.Parent is IMaximizingContainer))
                    {
                        o.SetValue(e.Property, e.OldValue);
                    }
                    else
                    {
                        tile.OnIsMaximizedChanged();
                    }
                    tile._IsChangingIsMaximized = false;
                }
            }

            internal void <.cctor>b__122_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((Tile) o).OnSizeChanged();
            }
        }
    }
}

