namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class VmlShapeProtections : ISupportsCopyFrom<VmlShapeProtections>, ICloneable<VmlShapeProtections>
    {
        private bool? adjustHandles;
        private bool? aspectRatio;
        private bool? cropping;
        private VmlExtensionHandlingBehavior ext;
        private bool? grouping;
        private bool? position;
        private bool? rotation;
        private bool? selection;
        private bool? shapeType;
        private bool? text;
        private bool? ungrouping;
        private bool? vertices;

        public VmlShapeProtections Clone()
        {
            VmlShapeProtections protections = new VmlShapeProtections();
            protections.CopyFrom(this);
            return protections;
        }

        public void CopyFrom(VmlShapeProtections source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.AdjustHandles = source.AdjustHandles;
            this.AspectRatio = source.AspectRatio;
            this.Cropping = source.Cropping;
            this.Ext = source.Ext;
            this.Grouping = source.Grouping;
            this.Position = source.Position;
            this.Rotation = source.Rotation;
            this.Selection = source.Selection;
            this.ShapeType = source.ShapeType;
            this.Text = source.Text;
            this.Ungrouping = source.Ungrouping;
            this.Vertices = source.Vertices;
        }

        public bool? AdjustHandles
        {
            get => 
                this.adjustHandles;
            set => 
                this.adjustHandles = value;
        }

        public bool? AspectRatio
        {
            get => 
                this.aspectRatio;
            set => 
                this.aspectRatio = value;
        }

        public bool? Cropping
        {
            get => 
                this.cropping;
            set => 
                this.cropping = value;
        }

        public VmlExtensionHandlingBehavior Ext
        {
            get => 
                this.ext;
            set => 
                this.ext = value;
        }

        public bool? Grouping
        {
            get => 
                this.grouping;
            set => 
                this.grouping = value;
        }

        public bool? Position
        {
            get => 
                this.position;
            set => 
                this.position = value;
        }

        public bool? Rotation
        {
            get => 
                this.rotation;
            set => 
                this.rotation = value;
        }

        public bool? Selection
        {
            get => 
                this.selection;
            set => 
                this.selection = value;
        }

        public bool? ShapeType
        {
            get => 
                this.shapeType;
            set => 
                this.shapeType = value;
        }

        public bool? Text
        {
            get => 
                this.text;
            set => 
                this.text = value;
        }

        public bool? Ungrouping
        {
            get => 
                this.ungrouping;
            set => 
                this.ungrouping = value;
        }

        public bool? Vertices
        {
            get => 
                this.vertices;
            set => 
                this.vertices = value;
        }
    }
}

