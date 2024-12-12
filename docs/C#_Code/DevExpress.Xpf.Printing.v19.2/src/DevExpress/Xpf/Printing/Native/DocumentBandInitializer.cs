namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Windows;

    public class DocumentBandInitializer
    {
        private readonly RowViewBuilder rowViewBuilder;
        private readonly DevExpress.Xpf.Printing.BrickCollection.BrickCollector brickCollector;
        private readonly Size usablePageSize;

        public DocumentBandInitializer(DevExpress.Xpf.Printing.BrickCollection.BrickCollector brickCollector, Size usablePageSize, bool rightToLeftLayout)
        {
            Guard.ArgumentNotNull(brickCollector, "brickCollector");
            this.brickCollector = brickCollector;
            this.usablePageSize = usablePageSize;
            this.rowViewBuilder = new RowViewBuilder(rightToLeftLayout);
        }

        public void Clear()
        {
            this.rowViewBuilder.Clear();
        }

        public void Initialize(DocumentBand band, Func<bool, RowViewInfo> getRowViewInfo)
        {
            int? nodeIndex = null;
            this.Initialize(band, getRowViewInfo, nodeIndex);
        }

        public void Initialize(DocumentBand band, Func<bool, RowViewInfo> getRowViewInfo, int? nodeIndex)
        {
            Guard.ArgumentNotNull(band, "band");
            if (getRowViewInfo != null)
            {
                RowViewInfo info = getRowViewInfo(true);
                if (info != null)
                {
                    float num;
                    RowContent content1 = new RowContent();
                    content1.Content = info.Content;
                    content1.UsablePageWidth = this.usablePageSize.Width;
                    content1.UsablePageHeight = this.usablePageSize.Height;
                    RowContent rowContent = content1;
                    if (nodeIndex != null)
                    {
                        int? nullable1;
                        int? nullable2 = nodeIndex;
                        if (nullable2 != null)
                        {
                            nullable1 = new int?(nullable2.GetValueOrDefault() % 2);
                        }
                        else
                        {
                            nullable1 = null;
                        }
                        int? nullable = nullable1;
                        int num2 = 0;
                        rowContent.IsEven = (nullable.GetValueOrDefault() == num2) ? (nullable != null) : false;
                    }
                    FrameworkElement container = this.rowViewBuilder.Create(info.Template, rowContent);
                    foreach (Brick brick in this.brickCollector.ToBricks(container, out num))
                    {
                        brick.SetAttachedValue<int>(BrickAttachedProperties.ParentID, band.ID);
                        band.Bricks.Add(brick);
                    }
                    if (band.SelfHeight < num)
                    {
                        band.BottomSpan = num - band.SelfHeight;
                    }
                }
            }
        }
    }
}

