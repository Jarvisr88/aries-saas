namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;
    using System.ComponentModel;

    public class BackdropPlane : ISupportsCopyFrom<BackdropPlane>, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey NormalVectorPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey UpVectorPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey AnchorPointPropertyKey = new PropertyKey(2);
        private Scene3DVector normalVector;
        private Scene3DVector upVector;
        private Scene3DVector anchorPoint;
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

        public BackdropPlane(IDocumentModel documentModel)
        {
            this.notifier = new PropertyChangedNotifier(this);
            this.normalVector = new Scene3DVector(documentModel);
            this.normalVector.PropertyChanged += new PropertyChangedEventHandler(this.OnNormalVectorChanged);
            this.upVector = new Scene3DVector(documentModel);
            this.upVector.PropertyChanged += new PropertyChangedEventHandler(this.OnUpVectorChanged);
            this.anchorPoint = new Scene3DVector(documentModel);
            this.anchorPoint.PropertyChanged += new PropertyChangedEventHandler(this.OnAnchorPointChanged);
        }

        public void CopyFrom(BackdropPlane value)
        {
            this.normalVector.CopyFrom(value.normalVector);
            this.upVector.CopyFrom(value.upVector);
            this.anchorPoint.CopyFrom(value.anchorPoint);
        }

        public override bool Equals(object obj)
        {
            BackdropPlane plane = obj as BackdropPlane;
            return ((plane != null) ? (this.normalVector.Equals(plane.normalVector) && (this.anchorPoint.Equals(plane.anchorPoint) && this.upVector.Equals(plane.upVector))) : false);
        }

        public override int GetHashCode() => 
            (this.normalVector.GetHashCode() ^ this.anchorPoint.GetHashCode()) ^ this.upVector.GetHashCode();

        private void OnAnchorPointChanged(object sender, PropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(AnchorPointPropertyKey);
        }

        private void OnNormalVectorChanged(object sender, PropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(NormalVectorPropertyKey);
        }

        private void OnUpVectorChanged(object sender, PropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(UpVectorPropertyKey);
        }

        public void ResetToStyle()
        {
            this.NormalVector.X = 0L;
            this.NormalVector.Y = 0L;
            this.NormalVector.Z = 0L;
            this.UpVector.X = 0L;
            this.UpVector.Y = 0L;
            this.UpVector.Z = 0L;
            this.AnchorPoint.X = 0L;
            this.AnchorPoint.Y = 0L;
            this.AnchorPoint.Z = 0L;
        }

        public Scene3DVector NormalVector =>
            this.normalVector;

        public Scene3DVector UpVector =>
            this.upVector;

        public Scene3DVector AnchorPoint =>
            this.anchorPoint;

        public bool IsDefault =>
            this.anchorPoint.IsDefault && (this.normalVector.IsDefault && this.upVector.IsDefault);
    }
}

