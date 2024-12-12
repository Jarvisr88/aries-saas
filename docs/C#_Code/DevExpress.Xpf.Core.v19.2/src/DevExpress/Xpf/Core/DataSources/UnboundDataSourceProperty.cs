namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class UnboundDataSourceProperty : INotifyPropertyChanged
    {
        private string displayName;
        private string name;
        private Type propertyType;
        private object userTag;

        public event PropertyChangedEventHandler PropertyChanged;

        public UnboundDataSourceProperty()
        {
            this.userTag = string.Empty;
            this.Property = new UnboundSourceProperty();
        }

        public UnboundDataSourceProperty(string name) : this()
        {
            this.Name = name;
        }

        public UnboundDataSourceProperty(string name, Type propertyType) : this(name)
        {
            this.PropertyType = propertyType;
        }

        protected void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected internal UnboundSourceProperty Property { get; private set; }

        public string DisplayName
        {
            get => 
                this.displayName;
            set
            {
                this.displayName = value;
                this.Property.DisplayName = this.displayName;
                this.OnPropertyChanged("DisplayName");
            }
        }

        public string Name
        {
            get => 
                this.name;
            set
            {
                this.name = value;
                this.Property.Name = this.name;
                this.OnPropertyChanged("Name");
            }
        }

        public Type PropertyType
        {
            get => 
                this.propertyType;
            set
            {
                this.propertyType = value;
                this.Property.PropertyType = this.propertyType;
                this.OnPropertyChanged("PropertyType");
            }
        }

        public object UserTag
        {
            get => 
                this.userTag;
            set
            {
                this.userTag = value;
                this.Property.UserTag = this.userTag;
                this.OnPropertyChanged("UserTag");
            }
        }
    }
}

