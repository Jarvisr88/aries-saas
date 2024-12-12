namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class AdjustableRect : ICloneable<AdjustableRect>, ISupportsCopyFrom<AdjustableRect>, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey LeftPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey TopPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey RightPropertyKey = new PropertyKey(2);
        public static readonly PropertyKey BottomPropertyKey = new PropertyKey(3);
        private const int idxLeft = 0;
        private const int idxRight = 1;
        private const int idxTop = 2;
        private const int idxBottom = 3;
        private AdjustableCoordinate[] coordinates = new AdjustableCoordinate[4];
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

        public AdjustableRect()
        {
            this.notifier = new PropertyChangedNotifier(this);
        }

        public AdjustableRect Clone()
        {
            AdjustableRect rect = new AdjustableRect();
            rect.CopyFrom(this);
            return rect;
        }

        public void CopyFrom(AdjustableRect value)
        {
            Guard.ArgumentNotNull(value, "AdjustableRect");
            this.Left = value.Left;
            this.Right = value.Right;
            this.Top = value.Top;
            this.Bottom = value.Bottom;
        }

        public void FromDoubles(double left, double top, double right, double bottom)
        {
            this.coordinates[0] = new AdjustableCoordinate(left);
            this.coordinates[1] = new AdjustableCoordinate(right);
            this.coordinates[2] = new AdjustableCoordinate(top);
            this.coordinates[3] = new AdjustableCoordinate(bottom);
        }

        public void FromString(string left, string top, string right, string bottom)
        {
            this.coordinates[0] = AdjustableCoordinate.FromString(left);
            this.coordinates[1] = AdjustableCoordinate.FromString(right);
            this.coordinates[2] = AdjustableCoordinate.FromString(top);
            this.coordinates[3] = AdjustableCoordinate.FromString(bottom);
        }

        public bool IsEmpty() => 
            (this.Left == null) || ((this.Right == null) || ((this.Top == null) || ReferenceEquals(this.Bottom, null)));

        protected internal virtual void SetCoordinate(int index, AdjustableCoordinate value)
        {
            this.SetCoordinateCore(index, value);
        }

        protected void SetCoordinateCore(int index, AdjustableCoordinate coordinate)
        {
            this.coordinates[index] = coordinate;
            PropertyKey propertyKey = (index == 0) ? LeftPropertyKey : ((index == 2) ? TopPropertyKey : ((index == 1) ? RightPropertyKey : BottomPropertyKey));
            this.notifier.OnPropertyChanged(propertyKey);
        }

        protected AdjustableCoordinate[] Coordinates =>
            this.coordinates;

        public AdjustableCoordinate Left
        {
            get => 
                this.coordinates[0];
            set => 
                this.SetCoordinate(0, value);
        }

        public AdjustableCoordinate Right
        {
            get => 
                this.coordinates[1];
            set => 
                this.SetCoordinate(1, value);
        }

        public AdjustableCoordinate Top
        {
            get => 
                this.coordinates[2];
            set => 
                this.SetCoordinate(2, value);
        }

        public AdjustableCoordinate Bottom
        {
            get => 
                this.coordinates[3];
            set => 
                this.SetCoordinate(3, value);
        }
    }
}

