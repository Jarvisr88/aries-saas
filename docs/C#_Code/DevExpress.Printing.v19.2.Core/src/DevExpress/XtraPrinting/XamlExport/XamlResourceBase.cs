namespace DevExpress.XtraPrinting.XamlExport
{
    using System;

    public class XamlResourceBase
    {
        private string name;

        internal void SetName(string name)
        {
            this.name = name;
        }

        public string Name =>
            this.name;
    }
}

