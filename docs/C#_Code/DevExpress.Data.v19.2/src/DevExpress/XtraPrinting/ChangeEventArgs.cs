namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;

    public class ChangeEventArgs : EventArgs
    {
        private string eventName;
        private SortedList infoList;

        public ChangeEventArgs(string eventName)
        {
            this.eventName = eventName;
            this.infoList = new SortedList();
        }

        public void Add(string name, object value)
        {
            this.infoList.Add(name, EventInfo.Create(name, value));
        }

        public object ValueOf(string name)
        {
            try
            {
                EventInfo info = this.infoList[name] as EventInfo;
                return info?.Value;
            }
            catch
            {
                return null;
            }
        }

        public string EventName =>
            this.eventName;

        private class EventInfo
        {
            private string name = "";
            private object value;

            public EventInfo(string name, object value)
            {
                this.name = name;
                this.value = value;
            }

            internal static ChangeEventArgs.EventInfo Create(string name, object value) => 
                new ChangeEventArgs.EventInfo(name, value);

            public string Name =>
                this.name;

            public virtual object Value
            {
                get => 
                    this.value;
                set => 
                    this.value = value;
            }
        }
    }
}

