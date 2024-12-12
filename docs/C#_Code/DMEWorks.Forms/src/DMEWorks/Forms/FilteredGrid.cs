namespace DMEWorks.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FilteredGrid : GridBase
    {
        private readonly FilteredGridAppearance appearance;
        private static readonly object EVENT_FILTERTEXTCHANGED = new object();
        private IContainer components;

        public event EventHandler FilterTextChanged
        {
            add
            {
                base.Events.AddHandler(EVENT_FILTERTEXTCHANGED, value);
            }
            remove
            {
                base.Events.AddHandler(EVENT_FILTERTEXTCHANGED, value);
            }
        }

        public FilteredGrid()
        {
            this.InitializeComponent();
            this.appearance = new FilteredGridAppearance(this);
        }

        public void ClearFilter()
        {
            this.FilterText = "";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "FilteredGrid";
            base.ResumeLayout(false);
        }

        protected virtual void OnFilterTextChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_FILTERTEXTCHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        [Category("Appearance")]
        public FilteredGridAppearance Appearance =>
            this.appearance;

        [Category("Data"), DefaultValue(""), Description("Text used for building filter for grid")]
        public string FilterText
        {
            get => 
                base.FilterTextCore;
            set
            {
                value ??= string.Empty;
                if (base.FilterTextCore != value)
                {
                    base.FilterTextCore = value;
                    this.OnFilterTextChanged(EventArgs.Empty);
                }
            }
        }
    }
}

