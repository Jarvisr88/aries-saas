namespace DevExpress.Mvvm.Gantt
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using System.Security;

    [Serializable]
    public class GanttTask : BindableBase, ISerializable
    {
        private object tag;
        private DateTime? _StartDate;
        private DateTime? _FinishDate;
        private double _Progress;
        private string _Name;
        private object _Id;
        private object _ParentId;
        private ObservableCollection<GanttPredecessorLink> _PredecessorLinks;
        private DateTime? _BaselineStartDate;
        private DateTime? _BaselineFinishDate;

        public GanttTask()
        {
            this._PredecessorLinks = new ObservableCollection<GanttPredecessorLink>();
            this.Id = this;
        }

        private GanttTask(SerializationInfo info, StreamingContext context)
        {
            this._PredecessorLinks = new ObservableCollection<GanttPredecessorLink>();
            this.DeserializeCore(info);
        }

        private void DeserializeCore(SerializationInfo info)
        {
            this.StartDate = LoadObjectValue<DateTime?>(info, "StartDate");
            this.FinishDate = LoadObjectValue<DateTime?>(info, "FinishDate");
            this.Progress = LoadObjectValue<double>(info, "Progress");
            this.Name = LoadObjectValue<string>(info, "Name");
            this.IdSerializable = LoadObjectValue<object>(info, "Id");
            this.BaselineStartDate = LoadObjectValue<DateTime?>(info, "BaselineStartDate");
            this.BaselineFinishDate = LoadObjectValue<DateTime?>(info, "BaselineFinishDate");
        }

        private static T LoadObjectValue<T>(SerializationInfo info, string name) => 
            (T) info.GetValue(name, typeof(T));

        private void SerializeCore(SerializationInfo info)
        {
            info.AddValue("StartDate", this.StartDate);
            info.AddValue("FinishDate", this.FinishDate);
            info.AddValue("Progress", this.Progress);
            info.AddValue("Name", this.Name);
            info.AddValue("Id", this.IdSerializable);
            info.AddValue("BaselineStartDate", this.BaselineStartDate);
            info.AddValue("BaselineFinishDate", this.BaselineFinishDate);
        }

        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.SerializeCore(info);
        }

        public object Tag
        {
            get => 
                this.tag;
            set => 
                base.SetValue<object>(ref this.tag, value, "Tag");
        }

        private object IdSerializable
        {
            get => 
                (this.Id == this) ? null : this.Id;
            set
            {
                object obj1 = value;
                if (value == null)
                {
                    object local1 = value;
                    obj1 = this;
                }
                this.Id = obj1;
            }
        }

        public DateTime? StartDate
        {
            get => 
                this._StartDate;
            set
            {
                DateTime? nullable = value;
                DateTime? nullable2 = this._StartDate;
                if (!(((nullable != null) == (nullable2 != null)) ? ((nullable != null) ? (nullable.GetValueOrDefault() == nullable2.GetValueOrDefault()) : true) : false))
                {
                    this._StartDate = value;
                    base.RaisePropertyChanged("StartDate");
                }
            }
        }

        public DateTime? FinishDate
        {
            get => 
                this._FinishDate;
            set
            {
                DateTime? nullable = value;
                DateTime? nullable2 = this._FinishDate;
                if (!(((nullable != null) == (nullable2 != null)) ? ((nullable != null) ? (nullable.GetValueOrDefault() == nullable2.GetValueOrDefault()) : true) : false))
                {
                    this._FinishDate = value;
                    base.RaisePropertyChanged("FinishDate");
                }
            }
        }

        public double Progress
        {
            get => 
                this._Progress;
            set
            {
                if (value != this._Progress)
                {
                    this._Progress = value;
                    base.RaisePropertyChanged("Progress");
                }
            }
        }

        public string Name
        {
            get => 
                this._Name;
            set
            {
                if (value != this._Name)
                {
                    this._Name = value;
                    base.RaisePropertyChanged("Name");
                }
            }
        }

        public object Id
        {
            get => 
                this._Id;
            set
            {
                if (!Equals(value, this._Id))
                {
                    this._Id = value;
                    base.RaisePropertyChanged("Id");
                }
            }
        }

        public object ParentId
        {
            get => 
                this._ParentId;
            set
            {
                if (!Equals(value, this._ParentId))
                {
                    this._ParentId = value;
                    base.RaisePropertyChanged("ParentId");
                }
            }
        }

        public ObservableCollection<GanttPredecessorLink> PredecessorLinks
        {
            get => 
                this._PredecessorLinks;
            set
            {
                if (!ReferenceEquals(value, this._PredecessorLinks))
                {
                    this._PredecessorLinks = value;
                    base.RaisePropertyChanged("PredecessorLinks");
                }
            }
        }

        public DateTime? BaselineStartDate
        {
            get => 
                this._BaselineStartDate;
            set
            {
                DateTime? nullable = value;
                DateTime? nullable2 = this._BaselineStartDate;
                if (!(((nullable != null) == (nullable2 != null)) ? ((nullable != null) ? (nullable.GetValueOrDefault() == nullable2.GetValueOrDefault()) : true) : false))
                {
                    this._BaselineStartDate = value;
                    base.RaisePropertyChanged("BaselineStartDate");
                }
            }
        }

        public DateTime? BaselineFinishDate
        {
            get => 
                this._BaselineFinishDate;
            set
            {
                DateTime? nullable = value;
                DateTime? nullable2 = this._BaselineFinishDate;
                if (!(((nullable != null) == (nullable2 != null)) ? ((nullable != null) ? (nullable.GetValueOrDefault() == nullable2.GetValueOrDefault()) : true) : false))
                {
                    this._BaselineFinishDate = value;
                    base.RaisePropertyChanged("BaselineFinishDate");
                }
            }
        }
    }
}

