namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class GroupShapeProperties : ShapePropertiesBase, ICloneable<GroupShapeProperties>, ISupportsCopyFrom<GroupShapeProperties>
    {
        public static readonly PropertyKey ChildTransform2DPropertyKey = new PropertyKey(0);
        private readonly Transform2D childTransform2D;

        public GroupShapeProperties(IDocumentModel documentModel) : this(documentModel, new Transform2D(documentModel))
        {
        }

        public GroupShapeProperties(IDocumentModel documentModel, Transform2D transform2D) : base(documentModel, transform2D)
        {
            this.childTransform2D = new Transform2D(documentModel);
            this.childTransform2D.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnChildTransform2DChanged);
        }

        public GroupShapeProperties Clone()
        {
            GroupShapeProperties properties = new GroupShapeProperties(base.DocumentModel, new Transform2D(base.DocumentModel));
            properties.CopyFrom(this);
            return properties;
        }

        public void CopyFrom(GroupShapeProperties value)
        {
            Guard.ArgumentNotNull(value, "GroupShapeProperties");
            base.CopyFrom(value);
            this.childTransform2D.CopyFrom(value.ChildTransform2D);
        }

        private void OnChildTransform2DChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            base.Notifier.OnPropertyChanged(ChildTransform2DPropertyKey, sender, e);
        }

        public Transform2D ChildTransform2D =>
            this.childTransform2D;
    }
}

