namespace DevExpress.XtraPrinting.Preview
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormLayout : IXtraSerializable, IDisposable
    {
        private System.Windows.Forms.Form form;
        private Rectangle bounds = Rectangle.Empty;
        private System.Windows.Forms.FormWindowState windowState;
        private bool lockSaving;

        public FormLayout(System.Windows.Forms.Form form)
        {
            this.form = form;
            this.SubscribeEvents(form);
        }

        private unsafe void AdjustBoundsByVisibleScreen()
        {
            Rectangle bounds = Screen.GetBounds(this.bounds);
            if (this.bounds.X > bounds.Right)
            {
                int num = this.bounds.X / bounds.Width;
                Rectangle* rectanglePtr1 = &this.bounds;
                rectanglePtr1.X -= bounds.Width * num;
            }
            if (this.bounds.Y > bounds.Bottom)
            {
                int num2 = this.bounds.Y / bounds.Height;
                Rectangle* rectanglePtr2 = &this.bounds;
                rectanglePtr2.Y -= bounds.Height * num2;
            }
            if (this.bounds.Right < bounds.Left)
            {
                int num3 = (Math.Abs(this.bounds.X) / bounds.Width) + 1;
                Rectangle* rectanglePtr3 = &this.bounds;
                rectanglePtr3.X += bounds.Width * num3;
            }
            if (this.bounds.Bottom < bounds.Top)
            {
                int num4 = (Math.Abs(this.bounds.Y) / bounds.Height) + 1;
                Rectangle* rectanglePtr4 = &this.bounds;
                rectanglePtr4.Y += bounds.Height * num4;
            }
        }

        void IXtraSerializable.OnEndDeserializing(string restoredVersion)
        {
        }

        void IXtraSerializable.OnEndSerializing()
        {
        }

        void IXtraSerializable.OnStartDeserializing(LayoutAllowEventArgs e)
        {
        }

        void IXtraSerializable.OnStartSerializing()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.form != null))
            {
                this.UnsubscribeEvents(this.form);
                this.form = null;
            }
        }

        ~FormLayout()
        {
            this.Dispose(false);
        }

        private void form_LocationChanged(object sender, EventArgs e)
        {
            this.SaveFormBounds();
        }

        private void form_SizeChanged(object sender, EventArgs e)
        {
            this.SaveFormBounds();
        }

        public virtual void RestoreFormLayout()
        {
            if (this.form != null)
            {
                this.lockSaving = true;
                try
                {
                    if (!this.bounds.IsEmpty)
                    {
                        this.AdjustBoundsByVisibleScreen();
                        this.form.Bounds = this.bounds;
                    }
                    this.form.WindowState = this.windowState;
                }
                finally
                {
                    this.lockSaving = false;
                }
            }
        }

        private void SaveFormBounds()
        {
            if (((this.form != null) && !this.lockSaving) && (this.form.WindowState == System.Windows.Forms.FormWindowState.Normal))
            {
                this.bounds = this.form.Bounds;
            }
        }

        public virtual void SaveFormLayout()
        {
            if (this.form != null)
            {
                this.windowState = this.form.WindowState;
            }
        }

        private void SubscribeEvents(System.Windows.Forms.Form form)
        {
            if (form != null)
            {
                form.SizeChanged += new EventHandler(this.form_SizeChanged);
                form.LocationChanged += new EventHandler(this.form_LocationChanged);
            }
        }

        private void UnsubscribeEvents(System.Windows.Forms.Form form)
        {
            if (form != null)
            {
                form.SizeChanged -= new EventHandler(this.form_SizeChanged);
                form.LocationChanged -= new EventHandler(this.form_LocationChanged);
            }
        }

        protected System.Windows.Forms.Form Form =>
            this.form;

        [XtraSerializableProperty]
        public Rectangle Bounds
        {
            get => 
                this.bounds;
            set => 
                this.bounds = value;
        }

        [XtraSerializableProperty]
        public System.Windows.Forms.FormWindowState FormWindowState
        {
            get => 
                this.windowState;
            set => 
                this.windowState = value;
        }
    }
}

