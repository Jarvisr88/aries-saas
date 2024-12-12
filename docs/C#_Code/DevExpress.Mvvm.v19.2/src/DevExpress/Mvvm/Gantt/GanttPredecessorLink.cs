namespace DevExpress.Mvvm.Gantt
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;

    public class GanttPredecessorLink : BindableBase
    {
        private object tag;
        private object _PredecessorTaskId;
        private PredecessorLinkType _LinkType = PredecessorLinkType.FinishToStart;
        private TimeSpan _Lag;

        public object Tag
        {
            get => 
                this.tag;
            set => 
                base.SetValue<object>(ref this.tag, value, "Tag");
        }

        public object PredecessorTaskId
        {
            get => 
                this._PredecessorTaskId;
            set
            {
                if (!Equals(value, this._PredecessorTaskId))
                {
                    this._PredecessorTaskId = value;
                    base.RaisePropertyChanged("PredecessorTaskId");
                }
            }
        }

        [DefaultValue(1)]
        public PredecessorLinkType LinkType
        {
            get => 
                this._LinkType;
            set
            {
                if (value != this._LinkType)
                {
                    this._LinkType = value;
                    base.RaisePropertyChanged("LinkType");
                }
            }
        }

        public TimeSpan Lag
        {
            get => 
                this._Lag;
            set
            {
                if (value != this._Lag)
                {
                    this._Lag = value;
                    base.RaisePropertyChanged("Lag");
                }
            }
        }
    }
}

