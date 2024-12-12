namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class Scene3DVector : ISupportsCopyFrom<Scene3DVector>, INotifyPropertyChanged
    {
        private const int Xindex = 0;
        private const int Yindex = 1;
        private const int Zindex = 2;
        private readonly IDocumentModel documentModel;
        private readonly long[] coordinates;

        public event PropertyChangedEventHandler PropertyChanged;

        public Scene3DVector(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.documentModel = documentModel;
            this.coordinates = new long[3];
        }

        public void CopyFrom(Scene3DVector value)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
        }

        public override bool Equals(object obj)
        {
            Scene3DVector vector = obj as Scene3DVector;
            return ((vector != null) ? ((this.coordinates[0] == vector.X) && ((this.coordinates[1] == vector.Y) && (this.coordinates[2] == vector.Z))) : false);
        }

        public override int GetHashCode() => 
            (this.coordinates[0].GetHashCode() ^ this.coordinates[1].GetHashCode()) ^ this.coordinates[2].GetHashCode();

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SetCoordinate(int index, long value)
        {
            if (this.coordinates[index] != value)
            {
                Scene3DVectorCoordinateChangeHistoryItem item = new Scene3DVectorCoordinateChangeHistoryItem(this, index, this.coordinates[index], value);
                this.documentModel.History.Add(item);
                item.Execute();
            }
        }

        public void SetCoordinateCore(int index, long value)
        {
            this.coordinates[index] = value;
            this.OnPropertyChanged((index == 0) ? "X" : ((index == 1) ? "Y" : "Z"));
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        public long X
        {
            get => 
                this.coordinates[0];
            set => 
                this.SetCoordinate(0, value);
        }

        public long Y
        {
            get => 
                this.coordinates[1];
            set => 
                this.SetCoordinate(1, value);
        }

        public long Z
        {
            get => 
                this.coordinates[2];
            set => 
                this.SetCoordinate(2, value);
        }

        public bool IsDefault =>
            (this.X == 0) && ((this.Y == 0) && (this.Z == 0L));
    }
}

