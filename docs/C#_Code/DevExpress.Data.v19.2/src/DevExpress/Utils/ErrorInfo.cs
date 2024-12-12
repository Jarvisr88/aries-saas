namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ErrorInfo
    {
        private Dictionary<object, object> hash = new Dictionary<object, object>();
        private string errorText = null;

        public event EventHandler Changed;

        public virtual void ClearErrors()
        {
            bool flag = (this.Hash.Count > 0) || ((this.ErrorText != null) && (this.ErrorText.Length > 0));
            this.Hash.Clear();
            this.errorText = "";
            if (flag)
            {
                this.OnChanged();
            }
        }

        protected virtual void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        public virtual string this[object obj]
        {
            get
            {
                object obj2;
                if (obj == null)
                {
                    return this.ErrorText;
                }
                this.Hash.TryGetValue(obj, out obj2);
                return obj2?.ToString();
            }
            set
            {
                if (obj == null)
                {
                    this.ErrorText = value;
                }
                else
                {
                    object obj2;
                    this.Hash.TryGetValue(obj, out obj2);
                    if ((value != null) && (value.Length == 0))
                    {
                        value = null;
                    }
                    if (((value != null) || (obj2 != null)) && !Equals(obj2, value))
                    {
                        if (value == null)
                        {
                            this.Hash.Remove(obj);
                        }
                        else
                        {
                            this.Hash[obj] = value;
                        }
                        this.OnChanged();
                    }
                }
            }
        }

        public virtual string ErrorText
        {
            get => 
                this.errorText;
            set
            {
                if (this.ErrorText != value)
                {
                    this.errorText = value;
                    this.OnChanged();
                }
            }
        }

        public virtual bool HasErrors =>
            ((this.ErrorText == null) || (this.ErrorText.Length <= 0)) ? (this.Hash.Count > 0) : true;

        protected Dictionary<object, object> Hash =>
            this.hash;
    }
}

