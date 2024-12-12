namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    public abstract class TextSearchEngineBase
    {
        public static TimeSpan DefaultTextSearchTimeout = TimeSpan.FromMilliseconds((double) (SystemInformation.DoubleClickTime * 2));
        protected readonly string BackString = "\b";
        private string prefix;
        protected List<string> charsEntered;
        private DispatcherTimer timeoutTimer;

        protected TextSearchEngineBase(TimeSpan timeout)
        {
            this.TimeOut = this.CoerceTimeSpan(timeout);
            this.ResetState();
        }

        protected void AddCharToPrefix(string newChar)
        {
            if (this.IsBackSpace(newChar) && (this.Prefix.Length > 0))
            {
                this.Prefix = this.Prefix.Substring(0, this.Prefix.Length - 1);
            }
            else
            {
                this.Prefix = this.Prefix + newChar;
                this.charsEntered.Add(newChar);
            }
        }

        private TimeSpan CoerceTimeSpan(TimeSpan timeout) => 
            (timeout >= DefaultTextSearchTimeout) ? timeout : DefaultTextSearchTimeout;

        protected bool IsBackSpace(string nextChar) => 
            nextChar == this.BackString;

        protected void OnTimeout(object sender, EventArgs e)
        {
            this.ResetState();
        }

        protected virtual void ResetState()
        {
            this.IsActive = false;
            this.Prefix = string.Empty;
            if (this.charsEntered == null)
            {
                this.charsEntered = new List<string>(10);
            }
            else
            {
                this.charsEntered.Clear();
            }
            if (this.timeoutTimer != null)
            {
                this.timeoutTimer.Stop();
            }
            this.timeoutTimer = null;
        }

        protected void ResetTimeout()
        {
            if (this.timeoutTimer != null)
            {
                this.timeoutTimer.Stop();
            }
            else
            {
                this.timeoutTimer = new DispatcherTimer(DispatcherPriority.Normal);
                this.timeoutTimer.Tick += new EventHandler(this.OnTimeout);
            }
            this.timeoutTimer.Interval = this.TimeOut;
            this.timeoutTimer.Start();
        }

        protected string StartFindMatchingPrefix(string prefix, string newChar)
        {
            string str = prefix;
            return ((!this.IsBackSpace(newChar) || (str.Length <= 0)) ? (prefix + newChar) : str.Substring(0, str.Length - 1));
        }

        public bool IsActive { get; protected set; }

        public string Prefix
        {
            get => 
                this.prefix;
            protected set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.SeachText = value;
                }
                this.prefix = value;
            }
        }

        public string SeachText { get; protected set; }

        private TimeSpan TimeOut { get; set; }
    }
}

