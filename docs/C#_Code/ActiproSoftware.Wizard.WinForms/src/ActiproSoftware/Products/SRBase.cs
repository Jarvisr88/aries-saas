namespace ActiproSoftware.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Resources;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class SRBase
    {
        private Dictionary<string, string> #PCb;

        protected SRBase()
        {
        }

        protected void ClearCustomStringsCore()
        {
            if (this.#PCb != null)
            {
                this.#PCb.Clear();
            }
        }

        protected bool ContainsCustomStringCore(string name) => 
            (this.#PCb != null) ? this.#PCb.ContainsKey(name) : false;

        protected string GetCustomStringCore(string name) => 
            ((this.#PCb == null) || !this.#PCb.ContainsKey(name)) ? null : this.#PCb[name];

        protected string GetStringCore(string name, params object[] args)
        {
            string format = null;
            format = !name.ContainsCustomStringCore(name) ? this.ResourceManager.GetString(name) : this.GetCustomStringCore(name);
            return (((format == null) || ((args == null) || (args.Length == 0))) ? format : string.Format(CultureInfo.CurrentCulture, format, args));
        }

        protected void RemoveCustomStringCore(string name)
        {
            if ((this.#PCb != null) && this.#PCb.ContainsKey(name))
            {
                this.#PCb.Remove(name);
            }
        }

        protected void SetCustomStringCore(string name, string value)
        {
            this.RemoveCustomStringCore(name);
            this.CustomStringResources.Add(name, value);
        }

        private Dictionary<string, string> CustomStringResources
        {
            get
            {
                if (this.#PCb == null)
                {
                    this.#PCb = new Dictionary<string, string>();
                }
                return this.#PCb;
            }
        }

        public abstract System.Resources.ResourceManager ResourceManager { get; }
    }
}

