namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using DevExpress.Utils;
    using System;

    public class DrawingEffectStyle : ISupportsCopyFrom<DrawingEffectStyle>, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey ContainerEffectPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey Scene3DPropertiesPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey Shape3DPropertiesPropertyKey = new PropertyKey(2);
        private readonly DevExpress.Office.Drawing.ContainerEffect containerEffect;
        private readonly DevExpress.Office.DrawingML.Scene3DProperties scene3DProperies;
        private readonly DevExpress.Office.Drawing.Shape3DProperties shape3DProperties;
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

        public DrawingEffectStyle(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.notifier = new PropertyChangedNotifier(this);
            this.containerEffect = new DevExpress.Office.Drawing.ContainerEffect(documentModel);
            this.containerEffect.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnContainerEffectChanged);
            this.scene3DProperies = new DevExpress.Office.DrawingML.Scene3DProperties(documentModel);
            this.scene3DProperies.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnScene3DProperiesChanged);
            this.shape3DProperties = new DevExpress.Office.Drawing.Shape3DProperties(documentModel);
            this.shape3DProperties.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnShape3DPropertiesChanged);
        }

        public void ApplyEffects(IDrawingEffectVisitor visitor)
        {
            this.ContainerEffect.ApplyEffects(visitor);
        }

        public DrawingEffectStyle CloneTo(IDocumentModel documentModel)
        {
            DrawingEffectStyle style = new DrawingEffectStyle(documentModel);
            style.CopyFrom(this);
            return style;
        }

        public void CopyFrom(DrawingEffectStyle value)
        {
            this.containerEffect.CopyFrom(value.containerEffect);
            this.scene3DProperies.CopyFrom(value.scene3DProperies);
            this.shape3DProperties.CopyFrom(value.shape3DProperties);
        }

        private void OnContainerEffectChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(ContainerEffectPropertyKey, sender, e);
        }

        private void OnScene3DProperiesChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(Scene3DPropertiesPropertyKey, sender, e);
        }

        private void OnShape3DPropertiesChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(Shape3DPropertiesPropertyKey, sender, e);
        }

        public DevExpress.Office.Drawing.ContainerEffect ContainerEffect =>
            this.containerEffect;

        public DevExpress.Office.DrawingML.Scene3DProperties Scene3DProperties =>
            this.scene3DProperies;

        public DevExpress.Office.Drawing.Shape3DProperties Shape3DProperties =>
            this.shape3DProperties;

        public bool IsDefault =>
            this.containerEffect.IsEmpty && (this.scene3DProperies.IsDefault && this.shape3DProperties.IsDefault);
    }
}

