namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class SerializableState
    {
        public SerializableState()
        {
        }

        public SerializableState(string state)
        {
            this.State = state;
        }

        [XmlIgnore]
        public string State { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlCDataSection CDATAState
        {
            get => 
                !string.IsNullOrEmpty(this.State) ? new XmlDocument().CreateCDataSection(this.State) : null;
            set => 
                this.State = value?.Value;
        }
    }
}

