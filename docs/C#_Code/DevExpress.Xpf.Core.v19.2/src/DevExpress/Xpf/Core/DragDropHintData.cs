namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    public class DragDropHintData : INotifyPropertyChanged
    {
        private IList<object> records = new object[0];
        private DragDropEffects effects;
        private FrameworkElement owner;
        private object targetRecord;
        private DevExpress.Xpf.Core.DropPosition? dropPosition;
        private bool showTargetInfoInDragDropHint;
        private bool allowShowTargetInfoInDragDropHint;
        private PropertyChangedEventHandler propertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this.propertyChanged += value;
            }
            remove
            {
                this.propertyChanged -= value;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.propertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UpdateShowTargetInfoInDragDropHint()
        {
            this.ShowTargetInfoInDragDropHint = this.AllowShowTargetInfoInDragDropHint && ((this.DropPosition != null) || (this.Effects == DragDropEffects.None));
        }

        public IList<object> Records
        {
            get => 
                this.records;
            set
            {
                if (!ReferenceEquals(this.records, value))
                {
                    this.records = value;
                    this.RaisePropertyChanged("Records");
                }
            }
        }

        public DragDropEffects Effects
        {
            get => 
                this.effects;
            set
            {
                if (this.effects != value)
                {
                    this.effects = value;
                    this.UpdateShowTargetInfoInDragDropHint();
                    this.RaisePropertyChanged("Effects");
                }
            }
        }

        public FrameworkElement Owner
        {
            get => 
                this.owner;
            set
            {
                if (!ReferenceEquals(this.owner, value))
                {
                    this.owner = value;
                    this.RaisePropertyChanged("Effects");
                }
            }
        }

        public object TargetRecord
        {
            get => 
                this.targetRecord;
            set
            {
                if (this.targetRecord != value)
                {
                    this.targetRecord = value;
                    this.RaisePropertyChanged("TargetRecord");
                }
            }
        }

        public DevExpress.Xpf.Core.DropPosition? DropPosition
        {
            get => 
                this.dropPosition;
            set
            {
                DevExpress.Xpf.Core.DropPosition? dropPosition = this.dropPosition;
                DevExpress.Xpf.Core.DropPosition? nullable2 = value;
                if ((dropPosition.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((dropPosition != null) != (nullable2 != null)) : true)
                {
                    this.dropPosition = value;
                    this.UpdateShowTargetInfoInDragDropHint();
                    this.RaisePropertyChanged("DropPosition");
                }
            }
        }

        public bool ShowTargetInfoInDragDropHint
        {
            get => 
                this.showTargetInfoInDragDropHint;
            private set
            {
                if (this.showTargetInfoInDragDropHint != value)
                {
                    this.showTargetInfoInDragDropHint = value;
                    this.RaisePropertyChanged("ShowTargetInfoInDragDropHint");
                }
            }
        }

        internal bool AllowShowTargetInfoInDragDropHint
        {
            get => 
                this.allowShowTargetInfoInDragDropHint;
            set
            {
                if (this.allowShowTargetInfoInDragDropHint != value)
                {
                    this.allowShowTargetInfoInDragDropHint = value;
                    this.UpdateShowTargetInfoInDragDropHint();
                }
            }
        }
    }
}

