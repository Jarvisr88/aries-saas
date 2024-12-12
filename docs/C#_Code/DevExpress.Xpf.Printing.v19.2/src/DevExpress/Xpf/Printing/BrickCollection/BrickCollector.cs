namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class BrickCollector
    {
        private readonly PrintingSystemBase printingSystem;
        private readonly Dictionary<BrickStyleKey, BrickStyle> brickStyles = new Dictionary<BrickStyleKey, BrickStyle>();
        private readonly Dictionary<TargetType, IBrickCreator> brickCreators = new Dictionary<TargetType, IBrickCreator>();
        private readonly Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters = new Dictionary<IVisualBrick, IOnPageUpdater>();
        private readonly Dictionary<string, BookmarkInfo> bookmarkInfos = new Dictionary<string, BookmarkInfo>();
        private IVisualTreeWalker visualTreeWalker;

        public BrickCollector(PrintingSystemBase printingSystem)
        {
            Guard.ArgumentNotNull(printingSystem, "printingSystem");
            this.printingSystem = printingSystem;
            this.brickCreators.Add(TargetType.None, new NoneBrickCreator());
            this.brickCreators.Add(TargetType.Text, new TextBrickCreator(printingSystem, this.brickStyles, this.onPageUpdaters));
            this.brickCreators.Add(TargetType.Panel, new PanelBrickCreator(printingSystem, this.brickStyles, this.onPageUpdaters));
            this.brickCreators.Add(TargetType.Image, new ImageBrickCreator(printingSystem, this.brickStyles, this.onPageUpdaters));
            this.brickCreators.Add(TargetType.Boolean, new CheckBoxBrickCreator(printingSystem, this.brickStyles, this.onPageUpdaters));
            this.brickCreators.Add(TargetType.PageNumber, new PageInfoBrickCreator(printingSystem, this.brickStyles, this.onPageUpdaters));
            this.brickCreators.Add(TargetType.ProgressBar, new ProgressBarBrickCreator(printingSystem, this.brickStyles, this.onPageUpdaters));
            this.brickCreators.Add(TargetType.TrackBar, new TrackBarBrickCreator(printingSystem, this.brickStyles, this.onPageUpdaters));
        }

        public void Clear()
        {
            this.brickStyles.Clear();
            this.bookmarkInfos.Clear();
        }

        private void CollectBricks(DependencyObject currentItem, DependencyObject parent, PanelBrick brickContainer)
        {
            VisualBrick item = null;
            UIElement source = currentItem as UIElement;
            UIElement element2 = parent as UIElement;
            if ((source != null) && (element2 != null))
            {
                TargetType effectiveTargetType = GetEffectiveTargetType(currentItem);
                item = this.GetBrickCreator(effectiveTargetType).Create(source, element2);
                if (item != null)
                {
                    brickContainer.Bricks.Add(item);
                }
            }
            FrameworkElement element3 = currentItem as FrameworkElement;
            if (element3 != null)
            {
                string bookmark = ExportSettings.GetBookmark(element3);
                if (!string.IsNullOrEmpty(bookmark) && (item != null))
                {
                    item.BookmarkInfo = this.GetBookmarkInfo(element3, bookmark);
                }
            }
            if ((source == null) || (source.Visibility != Visibility.Collapsed))
            {
                int childrenCount = this.VisualTreeWalker.GetChildrenCount(currentItem);
                for (int i = 0; i < childrenCount; i++)
                {
                    DependencyObject child = this.VisualTreeWalker.GetChild(currentItem, i);
                    PanelBrick brick2 = item as PanelBrick;
                    if (brick2 != null)
                    {
                        this.CollectBricks(child, currentItem, brick2);
                    }
                    else if (item == null)
                    {
                        this.CollectBricks(child, parent, brickContainer);
                    }
                }
            }
        }

        internal static TargetType? GetAttachedTargetType(DependencyObject item)
        {
            if ((DependencyPropertyHelper.GetValueSource(item, ExportSettings.TargetTypeProperty).BaseValueSource > BaseValueSource.Default) || ExportSettings.GetPropertiesHintMask(item).HasFlag(ExportSettingsProperties.TargetType))
            {
                return new TargetType?(ExportSettings.GetTargetType(item));
            }
            return null;
        }

        private BookmarkInfo GetBookmarkInfo(FrameworkElement item, string bookmark)
        {
            BookmarkInfo parentBookmarkInfo = this.GetParentBookmarkInfo(item);
            BookmarkInfo info2 = new BookmarkInfo(NullBrickOwner.Instance, bookmark, parentBookmarkInfo);
            if (item.Name != null)
            {
                this.bookmarkInfos[item.Name] = info2;
            }
            return info2;
        }

        internal IBrickCreator GetBrickCreator(TargetType targetType) => 
            this.brickCreators[targetType];

        internal static TargetType GetEffectiveTargetType(DependencyObject item)
        {
            TargetType? attachedTargetType = GetAttachedTargetType(item);
            return ((attachedTargetType != null) ? attachedTargetType.GetValueOrDefault() : GetInheritedTargetType(item));
        }

        internal static TargetType GetInheritedTargetType(DependencyObject item) => 
            !(item is IPageNumberExportSettings) ? (!(item is IProgressBarExportSettings) ? (!(item is ITrackBarExportSettings) ? (!(item is ITextExportSettings) ? (!(item is IImageExportSettings) ? (!(item is IBooleanExportSettings) ? TargetType.None : TargetType.Boolean) : TargetType.Image) : TargetType.Text) : TargetType.TrackBar) : TargetType.ProgressBar) : TargetType.PageNumber;

        private BookmarkInfo GetParentBookmarkInfo(DependencyObject item)
        {
            string bookmarkParentName = ExportSettings.GetBookmarkParentName(item);
            BookmarkInfo info = null;
            if ((bookmarkParentName != null) && !this.bookmarkInfos.TryGetValue(bookmarkParentName, out info))
            {
                throw new InvalidOperationException($"No parent with the {bookmarkParentName} name was found for the bookmark.");
            }
            return info;
        }

        public BrickCollectionBase ToBricks(FrameworkElement container, out float containerBrickHeight)
        {
            Guard.ArgumentNotNull(container, "container");
            PanelBrick brick1 = new PanelBrick();
            brick1.Style = new BrickStyle();
            PanelBrick brickContainer = brick1;
            RectangleF rect = GraphicsUnitConverter2.PixelToDoc(new RectangleF(0f, 0f, (float) container.ActualWidth, (float) container.ActualHeight));
            brickContainer.Initialize(this.printingSystem, rect);
            this.CollectBricks(container, container, brickContainer);
            containerBrickHeight = brickContainer.Height;
            return brickContainer.Bricks;
        }

        internal IVisualTreeWalker VisualTreeWalker
        {
            get => 
                this.visualTreeWalker;
            set
            {
                Guard.ArgumentNotNull(value, "value");
                this.visualTreeWalker = value;
            }
        }

        internal Dictionary<IVisualBrick, IOnPageUpdater> BrickUpdaters =>
            this.onPageUpdaters;
    }
}

