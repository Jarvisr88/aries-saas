namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class ModelShapeCustomGeometry : ICloneable<ModelShapeCustomGeometry>, ISupportsCopyFrom<ModelShapeCustomGeometry>, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey AdjustHandlesPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey AdjustValuesPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey ConnectionSitesPropertyKey = new PropertyKey(2);
        public static readonly PropertyKey GuidesPropertyKey = new PropertyKey(3);
        public static readonly PropertyKey PathsPropertyKey = new PropertyKey(4);
        public static readonly PropertyKey ShapeTextRectanglePropertyKey = new PropertyKey(5);
        private readonly IDocumentModelPart documentModelPart;
        private readonly PropertyChangedNotifier notifier;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        public ModelShapeCustomGeometry(IDocumentModelPart documentModelPart)
        {
            this.documentModelPart = documentModelPart;
            this.notifier = new PropertyChangedNotifier(this);
            this.AdjustHandles = new ModelAdjustHandlesList(documentModelPart);
            this.AdjustHandles.Modified += new EventHandler(this.OnAdjustHandlesModified);
            this.AdjustValues = new ModelShapeGuideList(documentModelPart);
            this.AdjustValues.Modified += new EventHandler(this.OnAdjustValuesModified);
            this.ConnectionSites = new ModelShapeConnectionList(documentModelPart);
            this.ConnectionSites.Modified += new EventHandler(this.OnConnectionSitesModified);
            this.Guides = new ModelShapeGuideList(documentModelPart);
            this.Guides.Modified += new EventHandler(this.OnGuidesModified);
            this.Paths = new ModelShapePathsList(documentModelPart);
            this.Paths.Modified += new EventHandler(this.OnPathsModified);
            this.ShapeTextRectangle = new ModelAdjustableRect(documentModelPart);
            this.ShapeTextRectangle.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnShapeTextRectangleChanged);
        }

        public ModelShapeCustomGeometry Clone()
        {
            ModelShapeCustomGeometry geometry = new ModelShapeCustomGeometry(this.documentModelPart);
            geometry.CopyFrom(this);
            return geometry;
        }

        public void CopyFrom(ModelShapeCustomGeometry value)
        {
            Guard.ArgumentNotNull(value, "ModelShapeCustomGeometry");
            this.AdjustHandles.CopyFrom(value.AdjustHandles);
            this.AdjustValues.CopyFrom(value.AdjustValues);
            this.ConnectionSites.CopyFrom(value.ConnectionSites);
            this.Guides.CopyFrom(value.Guides);
            this.Paths.CopyFrom(value.Paths);
            this.ShapeTextRectangle.CopyFrom(value.ShapeTextRectangle);
        }

        private void OnAdjustHandlesModified(object sender, EventArgs e)
        {
            this.notifier.OnPropertyChanged(AdjustHandlesPropertyKey);
        }

        private void OnAdjustValuesModified(object sender, EventArgs e)
        {
            this.notifier.OnPropertyChanged(AdjustValuesPropertyKey);
        }

        private void OnConnectionSitesModified(object sender, EventArgs e)
        {
            this.notifier.OnPropertyChanged(ConnectionSitesPropertyKey);
        }

        private void OnGuidesModified(object sender, EventArgs e)
        {
            this.notifier.OnPropertyChanged(GuidesPropertyKey);
        }

        private void OnPathsModified(object sender, EventArgs e)
        {
            this.notifier.OnPropertyChanged(PathsPropertyKey);
        }

        private void OnShapeTextRectangleChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(ShapeTextRectanglePropertyKey, sender, e);
        }

        public ModelAdjustHandlesList AdjustHandles { get; private set; }

        public ModelShapeGuideList AdjustValues { get; private set; }

        public ModelShapeConnectionList ConnectionSites { get; private set; }

        public ModelShapeGuideList Guides { get; private set; }

        public ModelShapePathsList Paths { get; private set; }

        public AdjustableRect ShapeTextRectangle { get; private set; }

        public IDocumentModelPart DocumentModelPart =>
            this.documentModelPart;
    }
}

