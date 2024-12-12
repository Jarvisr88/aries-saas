namespace DevExpress.Office
{
    using System;

    public class PropertyChangedNotifier
    {
        private readonly object owner;
        public EventHandler<OfficePropertyChangedEventArgs> Handler;

        public PropertyChangedNotifier(object owner)
        {
            this.owner = owner;
        }

        public bool OnPropertyChanged(PropertyKey propertyKey) => 
            this.OnPropertyChanged(this.owner, new OfficePropertyChangedEventArgs(propertyKey));

        private bool OnPropertyChanged(object sender, OfficePropertyChangedEventArgs args)
        {
            EventHandler<OfficePropertyChangedEventArgs> handler = this.Handler;
            if (handler != null)
            {
                handler(sender, args);
            }
            return args.Handled;
        }

        public void OnPropertyChanged(PropertyKey parentPropertyKey, object sender, OfficePropertyChangedEventArgs args)
        {
            bool flag = this.OnPropertyChanged(parentPropertyKey);
            if (!flag)
            {
                this.OnPropertyChanged(sender, args);
            }
            args.Handled = flag;
        }
    }
}

